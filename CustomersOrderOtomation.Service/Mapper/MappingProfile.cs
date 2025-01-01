using AutoMapper;
using CustomersOrderOtomation.Data.Models;
using CustomersOrderOtomation.Dto.Dtos;
using CustomersOrderOtomation.ViewModel.Category;
using CustomersOrderOtomation.ViewModel.Product;
using CustomersOrderOtomation.ViewModel.ShopList;

namespace MyProductList.Service.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Category, CategoryDetailViewModel>().ForMember(destination => destination.Products, opt => opt.MapFrom(src => src.Product_Categories.Select(x => x.Product).ToList()));

            CreateMap<Category, CategoryViewModel>();

            CreateMap<Product, ProductForCategoryDetailViewModel>();


            CreateMap<Product, ProductDetailViewModel>().ForMember(destination => destination.Categories, opt => opt.MapFrom(src => src.Product_Categories.Select(x => x.Category).ToList()));

            CreateMap<Product, ProductViewModel>();

            CreateMap<Product, ProductViewModelManagement>();

            CreateMap<Category, CategoryViewModelManagement>();

            CreateMap<Category, CategoryForProductViewModel>();


            CreateMap<ShopList, ShopListViewModel>();

            CreateMap<Product, ProductForShopListDetailViewModel>();


            CreateMap<Product, ProductDto>().ReverseMap();

            CreateMap<Category, CategoryDto>().ReverseMap();


            CreateMap<ShopList, ShopListDto>().ReverseMap();


        }
    }
}
