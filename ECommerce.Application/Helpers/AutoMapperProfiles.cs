using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Helpers
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserDTOs>().ReverseMap();
            CreateMap<Category, CategoryDTOs>().ReverseMap();
             CreateMap<Brands,BrandsDTOs>().ReverseMap();
            CreateMap<Car, CarDTOs>().ReverseMap();
            CreateMap<Coupon, CouponDTOs>().ReverseMap();
            CreateMap<Order, OrderDTOs>().ReverseMap();
            CreateMap<Slider, SliderDTOs>().ReverseMap();
            CreateMap<Special, SpecialDTOs>().ReverseMap();
            CreateMap<Sub_Slider, Sub_SliderDTOs>().ReverseMap();
            CreateMap<Vehicles,VehiclesDTOs>().ReverseMap();
            CreateMap<Photo, PhotoDTOs>().ReverseMap();


            CreateMap<Product, PorductsListDto>()
                .ForMember(dest => dest.mainImage, opt => opt.MapFrom(src => src.Image.FirstOrDefault(p => p.main_Image).main_ImageUrl))
                .ForMember(dest => dest.subImage1, opt => opt.MapFrom(src => src.Image.FirstOrDefault(p => p.sub_Image1).sub_Image1Url))
                .ForMember(dest => dest.subImage2, opt => opt.MapFrom(src => src.Image.FirstOrDefault(p => p.sub_Image2).sub_Image2Url));
            CreateMap<Product, ProductDTOs>()
            .ForMember(dest => dest.Brands_Id, opt => opt.MapFrom(src => src.BrandsId))
            .ForMember(dest => dest.Car_Id, opt => opt.MapFrom(src => src.CarId))
            .ForMember(dest => dest.Category_Id, opt => opt.MapFrom(src => src.CategoryId));

            CreateMap<ProductDTOs, Product>()
                .ForMember(dest => dest.BrandsId, opt => opt.MapFrom(src => src.Brands_Id))
                .ForMember(dest => dest.CarId, opt => opt.MapFrom(src => src.Car_Id))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category_Id));
        }
    }
}
