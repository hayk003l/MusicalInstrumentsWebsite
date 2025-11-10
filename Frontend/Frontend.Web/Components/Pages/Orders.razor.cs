using Microsoft.AspNetCore.Components;
using Shared.DataTransferObjects;

namespace Frontend.Web.Components.Pages
{
    public partial class Orders
    {
        [Inject]
        public ApiClient ApiClient { get; set; }

        public List<OrderForUserDto> OrderForUser { get; set; }

        private bool _isLoaded;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _isLoaded = true;
                await LoadOrdersAsync();
                StateHasChanged();
            }
        }

        private async Task LoadOrdersAsync()
        {
            try
            {
                OrderForUser = await ApiClient.GetCollectionAsync<OrderForUserDto>("api/orders/user");
            }
            catch (HttpRequestException ex)
            {
                // Handle 401/403 or network errors
            }
        }
        protected async override Task OnInitializedAsync()
        {
            //OrderForUser = await ApiClient.GetCollectionAsync<OrderForUserDto>("api/orders/user");

            await base.OnInitializedAsync();
        }
    }
}
