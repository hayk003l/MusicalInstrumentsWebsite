using Microsoft.AspNetCore.Components;
using Shared.DataTransferObjects;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Shared.RequestFeatures;
using System.Text;

namespace Frontend.Web.Components.Pages
{
    public partial class Items
    {
        [Inject]
        public ApiClient ApiClient { get; set; }

        public List<ItemDto> ItemDtos {  get; set; }
        private bool _isLoaded;

        private uint _minPrice = 0;
        private uint _maxPrice;
        private string _searchTerm = string.Empty;
        private StringBuilder query = new StringBuilder();

        private ItemsParameters _parameters;

        public Items()
        {
            _parameters = new ItemsParameters();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _isLoaded = true;
                query.Append("api/items");
                await LoadFilteredItemsAsync();
                _maxPrice = (uint)ItemDtos.Select(i => i.Amount).Max();
                StateHasChanged();
            }
        }

        private async Task LoadItemsAsync()
        {
            try
            {
                if (!_isLoaded)
                {
                    ItemDtos = await ApiClient.GetCollectionAsync<ItemDto>("api/items");
                }
                else
                {
                    await LoadFilteredItemsAsync();
                }
            }
            catch (HttpRequestException ex)
            {
                // Handle 401/403 or network errors
            }
        }

        protected override async Task OnInitializedAsync()
        {
            //ItemDtos = await ApiClient.GetCollectionAsync<ItemDto>("api/items");
            
            await base.OnInitializedAsync();
        }

        private async Task LoadFilteredItemsAsync()
        {
            try
            {
                ItemDtos = await ApiClient.GetCollectionAsync<ItemDto>(query.ToString());
            }
            catch(HttpRequestException ex) 
            {

            }
        }

        private async Task ApplyFilters()
        {
            query.Clear();
            query.Append("api/items?");

            query.Append($"minPrice={_minPrice}");
            query.Append($"&maxPrice={_maxPrice}");

            if (_searchTerm != string.Empty)
            {
                query.Append($"&searchTerm={_searchTerm}");
            }

            await LoadFilteredItemsAsync();

            StateHasChanged();
        }
        private async Task ResetFilters()
        {
            query.Clear();
            query.Append("api/items?");

            _minPrice = 0;
            _maxPrice = (uint)ItemDtos.Select(i => i.Amount).Max();
            _searchTerm = string.Empty;

            await LoadFilteredItemsAsync();

            StateHasChanged();
        }
    }
}
