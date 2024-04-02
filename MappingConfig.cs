using AutoMapper;
using ECommerce.Data;
using ECommerce.DTO.Role;
using ECommerce.DTOS.Cart;
using ECommerce.DTOS.Category;
using ECommerce.DTOS.Order;
using ECommerce.DTOS.Order.ShowOrderDTOs;
using ECommerce.DTOS.Payment;
using ECommerce.DTOS.Product;
using ECommerce.DTOS.ProductInCart;
using ECommerce.DTOS.SubCategory;
using ECommerce.DTOS.Supplier;
using ECommerce.DTOS.User;
using ECommerce.Models;
using Microsoft.AspNetCore.Identity;

namespace ECommerce
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<Product, ProductDetailsDTO>();
            CreateMap<ProductCreateDTO, Product>();
            CreateMap<ProductUpdateDTO, Product>();



            CreateMap<ProductInCartWriteDto, ProductInCart>().ReverseMap();
            CreateMap<ProductInCartReadDto, ProductInCart>().ReverseMap();


            CreateMap<SubCategory, SubCategoryReadDto>().ReverseMap();
            CreateMap<SubCategory, SubCategoryWriteDto>().ReverseMap();
            CreateMap<Category, ChildCategoryReadDto>().ReverseMap();
                    
    
            CreateMap<ChildApplicationUserDto, ApplicationUser>().ReverseMap();

            CreateMap<CartUpdateDto, Cart>().ReverseMap();
            CreateMap<CartCreateDto, Cart>().ReverseMap();
            CreateMap<IdentityRole, RoleDTO>();

            CreateMap<Cart, CartDto>().ReverseMap();
            CreateMap<CartUpdateDto, Cart>().ReverseMap();
            CreateMap<CartCreateDto, Cart>().ReverseMap();
            CreateMap<ProductInCart, ChildProductInCartDto>();


            CreateMap<Supplier, SupplierDTO>().ReverseMap();
            CreateMap<Product, ChildProductDTO>().ReverseMap();
            CreateMap<SupplierCreateDTO, Supplier>().ReverseMap();
            CreateMap<SupplierUpdateDTO, Supplier>().ReverseMap();


            CreateMap<Payment, PaymentDTO>().ReverseMap();
            CreateMap<PaymentCreateDTO, Payment>().ReverseMap();
            CreateMap<Order, ChildOrderDTO>().ReverseMap();

            CreateMap<SubCategoryChildReadDTO , SubCategory>().ReverseMap();
            CreateMap<CategoryReadDTO, Category>().ReverseMap();
            CreateMap<CategoryCreateDTO, Category>().ReverseMap();


            CreateMap<Order, OrderDTO>()
                        .ForMember(dest => dest.CardType, opt => opt.MapFrom(src => src.payment.CardType))
                        .ForMember(dest => dest.OrderProducts, opt => opt.MapFrom(src => src.OrderProducts));

            CreateMap<OrderProduct, ProductInShowOrder>()
                .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.Product.Stock))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Product.Image));
        }

        private AmazonDB dbContext;

        public MappingConfig(AmazonDB _dbcontext)
        {
            this.dbContext = _dbcontext;
        }
    }
}