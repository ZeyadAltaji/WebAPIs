﻿using ECommerce.Application.Abstractions;
using ECommerce.Application.Abstractions.Command;
using ECommerce.Application.UnitOfWork;
using ECommerce.DataAccessLayer.Repositories;
using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Hosting;

namespace ECommerce.DataAccessLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DBContext DC;
        private readonly IWebHostEnvironment _en;

        public UnitOfWork (DBContext dc , IWebHostEnvironment _environment)
        {
            DC = dc;
            _en = _environment;


        }
        public IUserRepository UserRepository => new UserRepository(DC , _en);

        public IRepository<Brands> repositoryBrands => new BrandsRepository(DC);

        public IRepository<Car> repositoryCar => new CarRepository(DC);

        public IRepository<Category> repositoryCategory => new CategoryRepository(DC);

        public IRepository<Coupon> repositoryCoupon => new CouponRepository(DC);


        public IRepository<Product> repositoryProduct => new ProductRepository(DC);

        public IRepository<Slider> repositorySlider => new SliderRepository(DC);

        public ITesting<Slider> RepositorySlider => new SliderRepository(DC);

        public IRepository<Special> repositorySpecial => new SpecialRepository(DC);

        public IRepository<Sub_Slider> repositorySub_Slider => new Sub_SliderRepository(DC);

        public IListImage<Sub_Slider> RepositorySub_Slider => new Sub_SliderRepository(DC);

        public IRepository<Vehicles> repositoryVehicles => new VehiclesRepository(DC);
        public IRepository<SubProducts> repositorySubProducts => new SubProductRepository(DC);
        public IRepository<Language> repositoryLanguage => new LanguageRepository(DC);
        public IRepository<Messages> repositoryMessages => new MessagesRepository(DC);


        //delete after testing

        public ITesting<Brands> repositoryBrand => new BrandsRepository(DC);
        public ITesting<Car> repositoryCars => new CarRepository(DC);

        public IGetData<Car> RepositoryCars => throw new NotImplementedException();

        public IListImage<Product> ProductRepository => new ProductRepository(DC);

        public IRepository<PhotoLogo> SettingRepository => new SettingRepository(DC);

        public ITesting<PhotoLogo> settingRepository => new SettingRepository(DC);

        public IGetData<Product> RepositoryProducts => new ProductRepository(DC);


        public IGetDataByProducts<SubProducts> RepositorySubProducts => new SubProductRepository(DC);

        public ITesting<SubProducts> RepositorySubproducts => new SubProductRepository(DC);

        public IGetById<SubProducts> RepositoryProductsById => new SubProductRepository(DC);



        public IOrderRepository OrderRepository => new OrderRepository(DC);

        public IOrderItemRepository OrderItemRepository => new OrderItemRepository(DC);


        public ICartRepository cartRepository => new CartRepository(DC);

        public ICartItemRepository CartItemRepository => new CartItemRepository(DC);

        public IRepository<ContactUs> repositoryContactUs => new ContactUsRepository(DC);

        public async Task<bool> SaveChanges ()
        {
            return await DC.SaveChangesAsync() > 0;
        }
    }
}
