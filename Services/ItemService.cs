using Service.Contracts;
using Contracts;
using AutoMapper;
using Shared.DataTransferObjects;
using Entities.Models;
using Shared.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.Extensions.Configuration;
using Entities.Exceptions;

namespace Services { 

    public class ItemService : IItemService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAmazonS3 _s3Client;
        private readonly IConfiguration _configuration;
        public ItemService(IRepositoryManager repository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IAmazonS3 s3Client,
            IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _s3Client = s3Client;
            _configuration = configuration;
        }

        public async Task<IEnumerable<ItemDto>> GetItemsAsync(ItemsParameters itemsParameters, bool trackingChanges)
        {
            if (!itemsParameters.ValidPriceRange)
            {
                throw new ArgumentOutOfRangeException("Wrong given range");
            }
            var result = await _repository.ItemRepository.GetItemsAsync(itemsParameters, trackingChanges);

            if (result is null)
            {
                throw new ItemsNotFoundException();
            }

            var itemsDtos = _mapper.Map<IEnumerable<ItemDto>>(result);
            foreach (var item in itemsDtos)
            {
                var description = await _repository.DescriptionRepository.GetDescriptionForItem(item.Id, trackingChanges);
                if (description is null)
                {
                    throw new DescriptionNotFoundException(item.Id);
                }
                item.Description = _mapper.Map<DescriptionDto>(description);
            }

            return itemsDtos;
        }

        public async Task<ItemDto> GetItemAsync(Guid id, bool trackingChanges)
        {
            var result = await _repository.ItemRepository.GetItemAsync(id, trackingChanges);
            if (result is null)
            {
                throw new ItemNotFoundException(id);
            }
            var itemDto = _mapper.Map<ItemDto>(result);
            var description = await _repository.DescriptionRepository.GetDescriptionForItem(id, trackingChanges);
            if (description is null)
            {
                throw new DescriptionNotFoundException(itemDto.Id);
            }
            itemDto.Description = _mapper.Map<DescriptionDto>(description);

            return itemDto;
        }

        public async Task<ItemDto> CreateItemAsync(ItemForCreationDto itemForCreation)
        {
            var item = _mapper.Map<Item>(itemForCreation);

            if (itemForCreation.ImageFile != null && itemForCreation.ImageFile.Length > 0)
            {
                if (!itemForCreation.ImageFile.ContentType.StartsWith("image"))
                {
                    throw new ArgumentException("Only image files are allowed.");
                }

                var awsSection = _configuration.GetSection("AWS");
                var bucketName = awsSection["BucketName"];

                var region = _configuration["AWS:Region"];
                var fileName = $"{Guid.NewGuid()}_{itemForCreation.ImageFile.FileName}";
                var key = $"images/{fileName}";

                try
                {
                    using (var stream = itemForCreation.ImageFile.OpenReadStream())
                    {
                        var uploadRequest = new TransferUtilityUploadRequest
                        {
                            InputStream = stream,
                            Key = key,
                            BucketName = bucketName,
                            ContentType = itemForCreation.ImageFile.ContentType,
                        };

                        Console.WriteLine($"BUCKET NAME IS ----- {uploadRequest.BucketName}");

                        var transferUtility = new TransferUtility(_s3Client);
                        await transferUtility.UploadAsync(uploadRequest);
                    }
                    item.ImageUrl = $"https://{bucketName}.s3.{region}.amazonaws.com/{key}";
                }
                catch (Exception)
                {
                    throw;
                }
            }

            _repository.ItemRepository.CreateItem(item);
            await _repository.SaveAsync();

            var itemDto = _mapper.Map<ItemDto>(item);

            return itemDto;
        }

        public async Task DeleteItemAsync(Guid id, bool trackingChanges)
        {
            var item = await GetItemAndCheckIfItExistAsync(id, trackingChanges);

            _repository.ItemRepository.DeleteItem(item);
            await _repository.SaveAsync();
        }

        public async Task UpdateItemAsync(Guid id, ItemForUpdatingDto itemForUpdating, bool trackingChanges)
        {
            var item = await GetItemAndCheckIfItExistAsync(id, trackingChanges);

            _mapper.Map(itemForUpdating, item);
            await _repository.SaveAsync();  
        }
        private async Task<Item> GetItemAndCheckIfItExistAsync(Guid id, bool trackingChanges)
        {
            var item = await _repository.ItemRepository.GetItemAsync(id, trackingChanges);
            if (item is null)
            {
                throw new ItemNotFoundException(id);
            }
            return item;
        }

        public async Task<(ItemForUpdatingDto itemToPatch, Item itemEntity)> GetItemForPatch(Guid id, bool trackingChanges)
        {
            var itemEntity = await _repository.ItemRepository.GetItemAsync(id, trackingChanges);

            if (itemEntity is null)
            {
                throw new ItemNotFoundException(id); 
            }

            var itemToPatch = _mapper.Map<ItemForUpdatingDto>(itemEntity);

            return (itemToPatch: itemToPatch, itemEntity: itemEntity);
        }

        public async Task SaveChangesForPatch(ItemForUpdatingDto itemToPatch, Item itemEntity)
        {
            _mapper.Map(itemToPatch, itemEntity);
            await _repository.SaveAsync();
        }


    }
}
