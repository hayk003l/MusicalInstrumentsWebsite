using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using Service.Contracts;
using Contracts;
using AutoMapper;
using Microsoft.Identity.Client;
using Shared.DataTransferObjects;
using Entities.Models;
using Shared.RequestFeatures;

namespace Services { 

    public class ItemService : IItemService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public ItemService(IRepositoryManager repository, IMapper mapper )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ItemDto>> GetItemsAsync(ItemsParameters itemsParameters, bool trackingChanges)
        {
            if (!itemsParameters.ValidPriceRange)
            {
                throw new Exception("Wrong given range");
            }
            var result = await _repository.ItemRepository.GetItemsAsync(itemsParameters, trackingChanges);

            var itemsDtos = _mapper.Map<IEnumerable<ItemDto>>(result);
            foreach (var item in itemsDtos)
            {
                var description = await _repository.DescriptionRepository.GetDescriptionForItem(item.Id, trackingChanges);
                item.Description = _mapper.Map<DescriptionDto>(description);
            }

            return itemsDtos;
        }

        public async Task<ItemDto> GetItemAsync(Guid id, bool trackingChanges)
        {
            var result = await _repository.ItemRepository.GetItemAsync(id, trackingChanges);

            var itemDto = _mapper.Map<ItemDto>(result);
            var description = await _repository.DescriptionRepository.GetDescriptionForItem(id, trackingChanges);

            itemDto.Description = _mapper.Map<DescriptionDto>(description);

            return itemDto;
        }

        public async Task<ItemDto> CreateItemAsync(ItemForCreationDto itemForCreation)
        {
            var item = _mapper.Map<Item>(itemForCreation);
            //var description = _mapper.Map<Description>(itemForCreation.Description);
            //item.Description = description;

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
                throw new NullReferenceException(nameof(item));
            }
            return item;
        }

        public async Task<(ItemForUpdatingDto itemToPatch, Item itemEntity)> GetItemForPatch(Guid id, bool trackingChanges)
        {
            var itemEntity = await _repository.ItemRepository.GetItemAsync(id, trackingChanges);

            if (itemEntity is null)
            {
                throw new NullReferenceException($"{nameof(itemEntity)} is null");
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
