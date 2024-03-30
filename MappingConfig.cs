using AutoMapper;
using ECommerce.Data;
using ECommerce.DTO.Role;
using ECommerce.DTOS.Cart;
using ECommerce.DTOS.Order;
using ECommerce.DTOS.Payment;
using ECommerce.DTOS.Product;
using ECommerce.DTOS.ProductInCart;
using ECommerce.DTOS.Supplier;
using ECommerce.DTOS.User;
using ECommerce.Models;
using Microsoft.AspNetCore.Identity;

namespace ECommerce
{
    public class MappingConfig:Profile
    {
        public MappingConfig() 
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<Product, ProductDetailsDTO>();
            CreateMap<ProductCreateDTO, Product>();
            CreateMap<ProductUpdateDTO, Product>();


            CreateMap<ChildProductInCartDto, ProductInCart>().ReverseMap();
            

            CreateMap<ChildApplicationUserDto, ApplicationUser>().ReverseMap();

            CreateMap<CartUpdateDto, Cart>().ReverseMap();
            CreateMap<CartCreateDto, Cart>().ReverseMap();
            CreateMap<IdentityRole, RoleDTO>();

            CreateMap<Cart, CartDto>().ReverseMap();
            CreateMap<CartUpdateDto, Cart>().ReverseMap();
            CreateMap<CartCreateDto, Cart>().ReverseMap();


            CreateMap<Supplier, SupplierDTO>().ReverseMap();
            CreateMap<Product, ChildProductDTO>().ReverseMap();
            CreateMap<SupplierCreateDTO, Supplier>().ReverseMap();
            CreateMap<SupplierUpdateDTO, Supplier>().ReverseMap();

            CreateMap<Payment, PaymentDTO>().ReverseMap();
            CreateMap<Order, ChildOrderDTO>().ReverseMap();

            CreateMap<Order, OrderDTO>().ForMember(o => o.OrderProductsID, src => src.MapFrom(p => p.OrderProducts.Select(id => id.ProductId)));
        }
       private AmazonDB dbContext;
        public MappingConfig(AmazonDB _dbcontext)
        {
            this.dbContext = _dbcontext;
        }

        }
}
