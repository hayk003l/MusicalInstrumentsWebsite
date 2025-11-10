

using Blazored.LocalStorage;
using Frontend.Web.Components.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Frontend.Web.Components.Layout
{
    public partial class MainLayout
    {
        private ClaimsPrincipal _user;
        [Inject]
        public AuthenticationStateProvider Provider { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }



        protected async override Task OnInitializedAsync()
        {
            var authProvider = (CustomAuthStateProvider)Provider;
            var authState = await authProvider.GetAuthenticationStateAsync();
            _user = authState.User;
            await base.OnInitializedAsync();
        }

        private bool IsAdmin =>_user != null && _user.IsInRole("Admin") ? true : false;
        public void Logout()
        {
            var authProvider = (CustomAuthStateProvider)Provider;
            authProvider.LogOut();
            NavigationManager.NavigateTo("/");
        }
        public async Task OrdersNav()
        {
            var authProvider = (CustomAuthStateProvider)Provider;
            if (await authProvider.OrdersPage())
            {
                NavigationManager.NavigateTo("/orders");
            }
            else
            {
                NavigationManager.NavigateTo("/auth");
            }
        }
    }
}
