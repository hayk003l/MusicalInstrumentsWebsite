using Microsoft.AspNetCore.Components;
using Shared.DataTransferObjects;

namespace Frontend.Web.Components.Pages
{
    public partial class  AdminDashboard
    {
        List<OrderForAdminDto> OrderForAdmin { get; set; }
        [Inject]
        ApiClient ApiClient { get; set; }
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
                OrderForAdmin = await ApiClient.GetCollectionAsync<OrderForAdminDto>("api/orders");
            }
            catch (HttpRequestException ex)
            {
                // Handle 401/403 or network errors
            }
        }
        protected override async Task OnInitializedAsync()
        {
            // ItemDtos = await ApiClient.GetCollectionAsync<ItemDto>("api/items");

            await base.OnInitializedAsync();
        }
    }
}
