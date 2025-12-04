using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.DataTransferObjects;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json.Nodes;

namespace Frontend.Web.Components.Services
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public CustomAuthStateProvider(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;

        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var anonymous = new ClaimsPrincipal(new ClaimsIdentity());

            try
            {
                var token = await _localStorage.GetItemAsync<string>("accessToken");
                if (string.IsNullOrWhiteSpace(token))
                    return new AuthenticationState(anonymous);

                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var handler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwt;

                try
                {
                    jwt = handler.ReadJwtToken(token);
                }
                catch
                {
                    await _localStorage.RemoveItemAsync("accessToken");
                    return new AuthenticationState(anonymous);
                }

                var exp = jwt.Claims.FirstOrDefault(c => c.Type == "expires")?.Value;
                if (exp != null && DateTimeOffset.FromUnixTimeSeconds(long.Parse(exp)) < DateTimeOffset.UtcNow)
                {
                    await _localStorage.RemoveItemAsync("accessToken");
                    return new AuthenticationState(anonymous);
                }
                                                        
                var claims = jwt.Claims.Select(c =>
                c.Type == "role" ? new Claim(ClaimTypes.Role, c.Value) : c);

                var identity = new ClaimsIdentity(claims, "jwt");
                var user = new ClaimsPrincipal(identity);

                var response = await _httpClient.GetAsync("http://production.eba-i8qxebcm.eu-north-1.elasticbeanstalk.com/api/authentication/info");
                if (response.IsSuccessStatusCode)
                {
                    return new AuthenticationState(user);
                }

                await _localStorage.RemoveItemAsync("accessToken");
                return new AuthenticationState(anonymous);
            }
            catch
            {
                return new AuthenticationState(anonymous);
            }
        }

        public async Task<FormatResults> LoginAsync(string username, string password)
        {
            UserForAuthenticationDto userForAuthentication = new UserForAuthenticationDto
            {
                UserName = username,
                Password = password
            };

            try
            {
                var response = await _httpClient.PostAsJsonAsync("http://production.eba-i8qxebcm.eu-north-1.elasticbeanstalk.com/api/authentication/login", userForAuthentication);
                if (response.IsSuccessStatusCode)
                {
                    var strResponse = await response.Content.ReadAsStringAsync();
                    var jsonResponse = JsonNode.Parse(strResponse);
                    var accessToken = jsonResponse["token"].ToString();

                    await _localStorage.SetItemAsync("accessToken", accessToken);

                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

                    return new FormatResults { Succeeded = true };
                }
                else
                {
                    var errorResponse = await response.Content.ReadFromJsonAsync<FormatResults>();
                    var errors = new List<string>();
                    foreach(var error in errorResponse.Errors)
                    {
                        errors.Add(error);
                    }
                    return new FormatResults
                    {
                        Succeeded = false,
                        Errors = errors.ToArray()
                    };
                }
            }
            catch (Exception) 
            {
                throw new Exception("Exception thrown");
            }


        }
        
        public async Task LogOut()
        {
            await _localStorage.RemoveItemAsync("accessToken");
            _httpClient.DefaultRequestHeaders.Authorization = null;

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task<bool> OrdersPage()
        {
            var token = await _localStorage.GetItemAsync<string>("accessToken");
            if (string.IsNullOrWhiteSpace(token))
            {
                return false;
            }
            return true;
        }
    }

    

    public class FormatResults
    {
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; }
    }
}
