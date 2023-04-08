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
            CreateMap<Product, ProductDTOs>().ReverseMap();
            CreateMap<Brands,BrandsDTOs>().ReverseMap();
            CreateMap<Car, CarDTOs>().ReverseMap();
            CreateMap<Coupon, CouponDTOs>().ReverseMap();
            CreateMap<Order, OrderDTOs>().ReverseMap();
            CreateMap<Slider, SliderDTOs>().ReverseMap();
            CreateMap<Special, SpecialDTOs>().ReverseMap();
            CreateMap<Sub_Slider, Sub_SliderDTOs>().ReverseMap();
            CreateMap<Vehicles,VehiclesDTOs>().ReverseMap();
            CreateMap<Photo, PhotoDTOs>().ReverseMap();
        }
    }
}
