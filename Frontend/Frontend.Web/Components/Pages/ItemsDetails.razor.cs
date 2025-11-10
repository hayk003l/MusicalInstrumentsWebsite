using Microsoft.AspNetCore.Components;
using Shared.DataTransferObjects;

namespace Frontend.Web.Components.Pages
{
    public partial class ItemsDetails
    {
        [Inject]
        ApiClient ApiClient { get; set; }
        [Inject]
        HttpClient HttpClient { get; set; }
        public ItemDto ItemDto { get; set; }
        private bool showModel = false;
        private Guid selectedItemId;

        private string _address;
        private OrderForCreationDto _orderForCreation;
        private ShippingDetailsForCreationDto _shippingDetailsForCreation;

        public ItemsDetails()
        {
            _orderForCreation = new OrderForCreationDto();
            _shippingDetailsForCreation = new ShippingDetailsForCreationDto();
        }

        private void ShowModel()
        {
            showModel = true;
        }
        private void CloseModel()
        {
            showModel = false;
        }

        private async Task SaveAsync()
        {
            _orderForCreation.ItemId = ItemDto.Id;
            _shippingDetailsForCreation.Address = _address;
            _orderForCreation.ShippingDetails = _shippingDetailsForCreation;
            await ApiClient.PostAsync<OrderForCreationDto>("https://localhost:7028/api/orders/user", _orderForCreation);

            CloseModel();
        }
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        protected  override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadItemAsync();
                StateHasChanged();
            }
        }

        private async Task LoadItemAsync()
        {
            try
            {
                ItemDto = await ApiClient.GetAsync<ItemDto>($"api/items/{Id}");
            }
            catch
            {

            }
        }

    }
}
