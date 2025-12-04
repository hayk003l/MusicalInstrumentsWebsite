using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.DataTransferObjects;

namespace Frontend.Web.Components.Pages
{
    public partial class Register
    {
        private string firstname = "";
        private string lastname = "";
        private string username = "";
        private string email = "";
        private string password = "";
        private string phoneNumber = "";
        private bool isLoading = false;
        private string errorMessage = "";
        private string successMessage = "";

        [Inject] public HttpClient HttpClient { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public AuthenticationStateProvider AuthService { get; set; }

        private async Task HandleRegister()
        {
            errorMessage = "";
            successMessage = "";

            // Validation
            if (string.IsNullOrWhiteSpace(firstname) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(phoneNumber) ||
                string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(lastname))
            {
                errorMessage = "Խնդրում ենք լրացնել բոլոր դաշտերը";
                return;
            }


            if (password.Length < 8)
            {
                errorMessage = "Գաղտնաբառը պետք է լինի առնվազն 6 նիշ";
                return;
            }
            isLoading = true;
            StateHasChanged();

            try
            {
                var registerRequest = new UserForRegistrationDto
                {
                    FirstName = firstname,
                    LastName = lastname,
                    UserName = username,
                    Password = password,
                    Email = email,
                    PhoneNumber = phoneNumber,
                    Roles = new List<string>() { "Client" }
                };

                var response = await HttpClient.PostAsJsonAsync("https://localhost:7028/api/authentication", registerRequest);

                if (response.IsSuccessStatusCode)
                {
                    successMessage = "Գրանցումը հաջող է։ Վերահղվում ենք մուտքի էջ...";
                    await Task.Delay(2000);
                    NavigationManager.NavigateTo("/login");
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    errorMessage = "Գրանցումը ձախողվեց։ Խնդրում ենք փորձել այլ մուտքանուն";
                }
            }
            catch (Exception)
            {
                errorMessage = "Սերվերի հետ կապի խնդիր։ Խնդրում ենք փորձել ավելի ուշ։";
            }
            finally
            {
                isLoading = false;
                StateHasChanged();
            }
        }
    }
}
