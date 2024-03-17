using AutoMapper;
using CustomersOrderOtomation.Data.Models;
using CustomersOrderOtomation.Data.Repository.Abstract;
using CustomersOrderOtomation.Data.UnitOfWork.Abstract;
using CustomersOrderOtomation.Dto.Dtos;
using CustomersOrderOtomation.Service.Abstract;
using CustomersOrderOtomation.ViewModel.Product;
using System.Data;

namespace CustomersOrderOtomation.Service.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> genericRepository;
        private readonly IProductRepository productRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IHelper helper;

        public ProductService(IGenericRepository<Product> genericRepository, IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper,
            IHelper helper)
        {
            this.genericRepository = genericRepository;
            this.productRepository = productRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.helper = helper;
        }

        public async Task AddProductAsync(ProductDto updateResource)
        {
            var product = mapper.Map<ProductDto, Product>(updateResource);

            await genericRepository.InsertAsync(product);
            await unitOfWork.CompleteAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await genericRepository.GetByIdAsync(id);

            if (product is null)
                throw new InvalidOperationException("product not found");

            genericRepository.RemoveAsync(product);
            await unitOfWork.CompleteAsync();
        }

        public async Task<List<ProductViewModel>> GetAllProducts()
        {
            var products = await productRepository.GetAllAsync();

            List<ProductViewModel> vm = mapper.Map<List<ProductViewModel>>(products);

            return vm;
        }

        public async Task<List<ProductViewModelManagement>> GetAllProductsForManagement()
        {
            var products = await productRepository.GetAllAsync();

            List<ProductViewModelManagement> vm = mapper.Map<List<ProductViewModelManagement>>(products);

            return vm;
        }

        public async Task<ProductDetailViewModel> GetSingleProductByIdAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);

            ProductDetailViewModel vm = mapper.Map<ProductDetailViewModel>(product);

            return vm;
        }

        public async Task UpdateProductAsync(int id, ProductDto updateResource)
        {
            var product = await genericRepository.GetByIdAsync(id);

            if (product is null)
                throw new InvalidOperationException("product not found");

            var mapped = mapper.Map(updateResource, product);

            genericRepository.Update(mapped);
            await unitOfWork.CompleteAsync();
        }

        public async Task<List<ProductViewModel>> GetProductsByCategory(int categoryId)
        {
            var products = await productRepository.GetAllProductsByCategory(categoryId);
            var vm = await helper.ProductListToProductViewModel(products, mapper);

            return vm;
        }

        public async Task<List<ProductForGetShopListDto>> GetShoppingCardProducts(List<ProductForAddShopListDto> prod)
        {
            List<ProductForGetShopListDto> productDetailViewModels = new List<ProductForGetShopListDto>();

            var prodList = prod
                .GroupBy(p => p.productId)
                 .Select(group => new
                 {
                     productId = group.Key,
                     piece = group.Sum(p => p.piece)
                 }).ToList();

            foreach (var product in prodList)
            {
                var getProduct = await GetSingleProductByIdAsync(product.productId);

                ProductForGetShopListDto productForGetShopListDto = new ProductForGetShopListDto()
                {
                    Price = getProduct.Price,
                    Name = getProduct.Name,
                    Id = getProduct.Id,
                    Piece = product.piece

                };

                productDetailViewModels.Add(productForGetShopListDto);
            }

            return productDetailViewModels;
        }



    }


}
