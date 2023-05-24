using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Application.ImageDTOs;
using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserDTOs>().ReverseMap();
            CreateMap<Category, CategoryDTOs>().ReverseMap();
 
            CreateMap<Brands, BrandsDTOs>()
            .ForMember(d => d.Image_BrandUrl, opt => opt.MapFrom(src => src.Image_BrandUrl)).ReverseMap();

            CreateMap<Car, CarDTOs>().ForMember(d => d.Image_CarUrl, opt => opt.MapFrom(src => src.Image_CarUrl)).ReverseMap();

            CreateMap<Coupon, CouponDTOs>().ReverseMap();
            CreateMap<Order, OrderDTOs>().ReverseMap();
            CreateMap<ContactUs, ContactUsDTOs>().ReverseMap();


            CreateMap<Slider, SliderDTOs>().ReverseMap();
            CreateMap<Special, SpecialDTOs>().ReverseMap();
            CreateMap<Sub_Slider, Sub_SliderDTOs>().ReverseMap();
            CreateMap<Vehicles, VehiclesDTOs>().ReverseMap();
            CreateMap<PhotoLogo, PhotoDTOs>().ReverseMap();


            //CreateMap<Product, PorductsListDto>();


            CreateMap<Product, ProductDTOs>()
                .ForMember(dest => dest.Brands_Id, opt => opt.MapFrom(src => src.BrandsId))
                .ForMember(dest => dest.Car_Id, opt => opt.MapFrom(src => src.CarId))
                .ForMember(dest => dest.Category_Id, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(d => d.Primary_Image, opt => opt.MapFrom(src => src.Primary_Image))
                .ForMember(d => d.ForeignImage1, opt => opt.MapFrom(src => src.ForeignImage1))
                .ForMember(d => d.ForeignImage2, opt => opt.MapFrom(src => src.ForeignImage2));
            

            CreateMap<ProductDTOs, Product>()
                .ForMember(dest => dest.BrandsId, opt => opt.MapFrom(src => src.Brands_Id))
                .ForMember(dest => dest.CarId, opt => opt.MapFrom(src => src.Car_Id))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category_Id))
                .ForMember(d => d.Primary_Image, opt => opt.MapFrom(src => src.Primary_Image))
                .ForMember(d => d.ForeignImage1, opt => opt.MapFrom(src => src.ForeignImage1))
                .ForMember(d => d.ForeignImage2, opt => opt.MapFrom(src => src.ForeignImage2));



            CreateMap<SubProducts, SubProductDTOs>()
               .ForMember(dest => dest.Brands_Id, opt => opt.MapFrom(src => src.BrandsId))
               .ForMember(dest => dest.Car_Id, opt => opt.MapFrom(src => src.CarId))
               .ForMember(dest => dest.Category_Id, opt => opt.MapFrom(src => src.CategoryId))
               .ForMember(d => d.Primary_Image, opt => opt.MapFrom(src => src.Primary_Image));


            CreateMap<SubProductDTOs, SubProducts>()
                .ForMember(dest => dest.BrandsId, opt => opt.MapFrom(src => src.Brands_Id))
                .ForMember(dest => dest.CarId, opt => opt.MapFrom(src => src.Car_Id))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category_Id))
                .ForMember(d => d.Primary_Image, opt => opt.MapFrom(src => src.Primary_Image));


            CreateMap<UserPhotoDtos, User>()
                .ForMember(dest => dest.Image_userUrl, opt => opt.MapFrom(src => src.Image_userUrl))
                .ForMember(dest => dest.Image_userUrl, opt => opt.MapFrom(src => src.Public_id));


            CreateMap<CarsImageDtos, Car>()
                .ForMember(d => d.Image_CarUrl, opt => opt.MapFrom(src => src.Image_CarsUrl))
                .ForMember(d => d.Image_CarUrl, opt => opt.MapFrom(src => src.Public_id));


            CreateMap<BrandsImageDtos, Brands>()
                .ForMember(d => d.Image_BrandUrl, opt => opt.MapFrom(src => src.Image_BrandUrl))
                .ForMember(d => d.Image_BrandUrl, opt => opt.MapFrom(src => src.Public_id));

            CreateMap<User, FullUserDTOs>()
                .ForMember(dest => dest.password, opt => opt.MapFrom(src => Encoding.UTF8.GetString(src.Password)))
                .ForMember(dest => dest.comfirmPassword, opt => opt.MapFrom(src => Encoding.UTF8.GetString(src.ComfirmPassword)))
                .ForMember(d => d.Image_userUrl, opt => opt.MapFrom(src => src.Image_userUrl))
                .ReverseMap()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => Encoding.UTF8.GetBytes(src.password)))
                .ForMember(dest => dest.ComfirmPassword, opt => opt.MapFrom(src => Encoding.UTF8.GetBytes(src.comfirmPassword)))
                .ForMember(d => d.Image_userUrl, opt => opt.MapFrom(src => src.Image_userUrl));


            CreateMap<Cart, CartDTOs>().ReverseMap();

            CreateMap<ItemCart, ItemCartDTOs>().ReverseMap();
            CreateMap<Order, OrderDTOs>().ReverseMap();


        }
    }
}
