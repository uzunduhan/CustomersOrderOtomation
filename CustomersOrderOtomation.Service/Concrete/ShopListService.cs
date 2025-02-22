﻿using AutoMapper;
using CustomersOrderOtomation.Data.Models;
using CustomersOrderOtomation.Data.Repository.Abstract;
using CustomersOrderOtomation.Data.UnitOfWork.Abstract;
using CustomersOrderOtomation.Dto.Dtos;
using CustomersOrderOtomation.Service.Abstract;
using CustomersOrderOtomation.ViewModel.ShopList;
using System.Text.Json;

namespace CustomersOrderOtomation.Service.Concrete
{
    public class ShopListService : IShopListService
    {
        private readonly IGenericRepository<ShopList> genericRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IShopListRepository shopListRepository;

        public ShopListService(IGenericRepository<ShopList> genericRepository, IShopListRepository shopListRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.genericRepository = genericRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.shopListRepository = shopListRepository;
        }

        public async Task AddShopListAsync(ShopListDto updateResource)
        {
            var shopList = mapper.Map<ShopListDto, ShopList>(updateResource);

            await genericRepository.InsertAsync(shopList);
            await unitOfWork.CompleteAsync();
        }

        public async Task DeleteShopListAsync(int id)
        {
            var shopList = await genericRepository.GetByIdAsync(id);

            if (shopList is null)
                throw new InvalidOperationException("shoplist not found");

            genericRepository.RemoveAsync(shopList);
            await unitOfWork.CompleteAsync();
        }



        public async Task<List<ShopListViewModel>> GetAllShopLists()
        {
            var tempEntity = await shopListRepository.GetAllAsync();

            List<ShopListViewModel> vm = mapper.Map<List<ShopListViewModel>>(tempEntity);

            return vm;
        }

        public async Task<ShopListDto> GetSingleShopListByIdAsync(int id)
        {
            var tempEntity = await shopListRepository.GetByIdAsync(id);

            if (tempEntity is null)
                throw new InvalidOperationException("shopList not found");


            var shopList = mapper.Map<ShopList, ShopListDto>(tempEntity);


            return shopList;
        }



        public async Task CheckIsCompleteColumnForShopList(ShopList shopList)
        {
            if (shopList.IsComplete is true)
                throw new InvalidOperationException("this list already completed");

            shopList.IsComplete = true;

            try
            {
                shopListRepository.Update(shopList);
            }
            catch (Exception ex)
            {

            }

        
            await unitOfWork.CompleteAsync();
        }

        public List<ShopListViewModel> GetAllShopListsWithSignalR()
        {
            var shopLists = shopListRepository.GetAllShopListWithSıgnalR().Where(x=>x.IsComplete == false).ToList();

            List<ShopListViewModel> vm = mapper.Map<List<ShopListViewModel>>(shopLists);

            return vm;
        }

        public async Task<ShopList> GetSingleShopListByIdAsyncPure(int id)
        {
            var tempEntity = await shopListRepository.GetByIdAsync(id);

            if (tempEntity is null)
                throw new InvalidOperationException("shopList not found");



            return tempEntity;
        }

        public async Task<List<ShopListViewModel>> GetShopLists(int page, int pageSize)
        {
            var dataList = GetAllShopListsWithSignalR().Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return dataList;
        }

        public async Task<bool> UpdateShopListIsCompleteTrue(int orderNumber)
        {
            try
            {
                var shop = await GetSingleShopListByIdAsyncPure(orderNumber);

                if (shop != null)
                {
                    CheckIsCompleteColumnForShopList(shop);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<bool> AddShoppingCardProductsToShopList(string cusName, string cusTableNo, List<ShopListAddShoppingCardProductsDto> dto)
        {

            ShopListDto shopListDto = new ShopListDto()
            {
                OrderCustomerName = cusName,
                OrderTableNumber = cusTableNo,
                ShopListProductsData = JsonSerializer.Serialize(dto)
            };

            try
            {
                await AddShopListAsync(shopListDto);
            }
            catch (Exception ex)
            {
                return false;
            }


            return true;
        }


    }
}
