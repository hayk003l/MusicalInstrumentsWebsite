using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
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

        private IBrowserFile _selectedImageFile;
        private string _imagePreviewUrl;

        private void HandleImageSelected(InputFileChangeEventArgs e)
        {
            _selectedImageFile = e.File;

            //using var stream = _selectedImageFile.OpenReadStream(maxAllowedSize: 5_000_000);
            //var buffer = new byte[_selectedImageFile.Size];
            //stream.Read(buffer, 0, (int)_selectedImageFile.Size);

            //_imagePreviewUrl = $"data:{_selectedImageFile.ContentType};base64,{Convert.ToBase64String(buffer)}";
        }
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
                var formData = new MultipartFormDataContent();
                formData.Add(new StringContent(_name), nameof(_itemForCreation.Name));
                formData.Add(new StringContent(_price.ToString()), nameof(_itemForCreation.Amount));
                formData.Add(new StringContent(_description), "Description.DescriptionText");
                formData.Add(new StringContent(_country), "Description.Country");
                if (_selectedImageFile != null)
                {
                    var streamContent = new StreamContent(_selectedImageFile.OpenReadStream(maxAllowedSize: 5_000_000));
                    streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(_selectedImageFile.ContentType);
                    formData.Add(streamContent, nameof(_itemForCreation.ImageFile), _selectedImageFile.Name);
                }
                await ApiClient.PostWithFormAsync("api/items", formData);
            }
            CloseModel();
        }

        private async Task ConfirmDelete()
        {
            await ApiClient.DeleteAsync($"api/items/{selectedItemId}");
            CloseDeleteModel();
        }

        // private List<Item> items;
        // private string searchTerm = "";
        // private string selectedCategory = "";
    }
}
