using Frontend.Web.Components.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Frontend.Web.Components.Pages
{
    public partial class Login
    {
        private string username = "";
        private string password = "";
        private bool isLoading = false;
        private string errorMessage = "";

        [Inject]
        public HttpClient HttpClient { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public AuthenticationStateProvider AuthService { get; set; }

        private async Task HandleLogin()
        {
            errorMessage = "";

            // Basic validation
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                errorMessage = "Խնդրում ենք լրացնել բոլոր դաշտերը";
                return;
            }

            isLoading = true;
            StateHasChanged();
            var customAuth = (CustomAuthStateProvider)AuthService;
            try
            {
                var formResults = await customAuth.LoginAsync(username, password);

                if (formResults.Succeeded)
                {
                    NavigationManager.NavigateTo("/items");
                }
                else
                {
                    errorMessage = formResults.Errors[0];
                }

            }
            catch (Exception ex)
            {
                errorMessage = "Սերվերի հետ կապի խնդիր։ Խնդրում ենք փորձել ավելի ուշ։";
                // Log exception
            }
            finally
            {
                isLoading = false;
                StateHasChanged();
            }
        }
    }
}
