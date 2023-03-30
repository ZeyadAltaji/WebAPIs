﻿using ECommerce.Application.Abstractions;
using ECommerce.Application.UnitOfWork;
using ECommerce.DataAccessLayer.Repositories;
using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccessLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DBContext DC;
        public UnitOfWork(DBContext dc)
        {
            DC = dc;     
        }
        public IUserRepository UserRepository => new UserRepository(DC);

        public IRepository<Brands> repositoryBrands => new BrandsRepository(DC);

        public IRepository<Car> repositoryCar => new CarRepository(DC);

        public IRepository<Category> repositoryCategory => new CategoryRepository(DC);

        public IRepository<Coupon> repositoryCoupon => new CouponRepository(DC);

        public IRepository<Order> repositoryOrder => new OrderRepository(DC);

        public IRepository<Product> repositoryProduct => new ProductRepository(DC);

        public IRepository<Slider> repositorySlider => new SliderRepository(DC);

        public IRepository<Special> repositorySpecial => new SpecialRepository(DC);

        public IRepository<Sub_Slider> repositorySub_Slider => new Sub_SliderRepository(DC);

        public IRepository<Vehicles> repositoryVehicles => new VehiclesRepository(DC);
    }
}
