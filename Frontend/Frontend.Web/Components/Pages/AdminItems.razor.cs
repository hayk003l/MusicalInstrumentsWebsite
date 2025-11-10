using Microsoft.AspNetCore.Components;
using Shared.DataTransferObjects;

namespace Frontend.Web.Components.Pages
{
    public partial class AdminItems
    {
        [Inject]
        public HttpClient HttpClient { get; set; }

        [Inject]
        public ApiClient ApiClient { get; set; }

        public List<ItemDto> ItemDtos { get; set; }

        private ItemForCreationDto _itemForCreation;
        private ItemForUpdatingDto _itemForUpdating;
        private DescriptionForCreationDto _descriptionForCreation;

        public AdminItems()
        {
            _itemForCreation = new ItemForCreationDto();
            _itemForUpdating = new ItemForUpdatingDto();
            _descriptionForCreation = new DescriptionForCreationDto();
        }

        private string _name;
        private decimal _price;
        private string _description;
        private string _country;
        private bool _isLoaded;
        //protected override async Task OnAfterRenderAsync(bool firstRender)
        //{
        //    if (firstRender)
        //    {
        //        _isLoaded = true;
        //        await LoadOrdersAsync();
        //        StateHasChanged();
        //    }
        //}

        //private async Task LoadOrdersAsync()
        //{
        //    try
        //    {
        //        ItemDtos = await ApiClient.GetCollectionAsync<ItemDto>("api/items");
        //    }
        //    catch (HttpRequestException ex)
        //    {
        //        // Handle 401/403 or network errors
        //    }
        //}
        protected override async Task OnInitializedAsync()
        {
            ItemDtos = await ApiClient.GetCollectionAsync<ItemDto>("api/items");

            await base.OnInitializedAsync();
        }
        private bool showModel = false;
        private bool showDeleteModel = false;
        private bool isEditMode = false;
        private Guid selectedItemId;

        private void ShowAddModel()
        {
            isEditMode = false;
            showModel = true;

        }

        private void ShowEditModel(Guid itemId)
        {
            isEditMode = true;
            selectedItemId = itemId;
            showModel = true;
            // Load item data for editing
        }

        private void ShowDeleteModel(Guid itemId)
        {
            selectedItemId = itemId;
            showDeleteModel = true;
        }

        private void CloseModel()
        {
            showModel = false;
        }

        private void CloseDeleteModel()
        {
            showDeleteModel = false;
        }

        private async Task SaveItem()
        {
             if (isEditMode)
            {
                _itemForUpdating.Name = _name;
                _itemForUpdating.Amount = _price;
                _descriptionForCreation.DescriptionText = _description;
                _descriptionForCreation.Country = _country;
                _itemForUpdating.Description = _descriptionForCreation;
                await ApiClient.PutAsync<ItemForUpdatingDto>($"api/items/{selectedItemId}", _itemForUpdating);
            }
            else
            {
                _itemForCreation.Name = _name;
                _itemForCreation.Amount = _price;
                _descriptionForCreation.DescriptionText = _description;
                _descriptionForCreation.Country = _country;
                _itemForCreation.Description = _descriptionForCreation;
                await ApiClient.PostAsync<ItemForCreationDto>("api/items", _itemForCreation);
            }
            CloseModel();
        }

        private async Task ConfirmDelete()
        {
            await HttpClient.DeleteAsync($"https://localhost:7028/api/items/{selectedItemId}");
            CloseDeleteModel();
        }

        // private List<Item> items;
        // private string searchTerm = "";
        // private string selectedCategory = "";
    }
}
