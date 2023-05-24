using ECommerce.Application.Abstractions;
using ECommerce.Application.Abstractions.Command;
using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IRepository<Brands> repositoryBrands { get; }
        IRepository<Car> repositoryCar { get; }
        IRepository<Category> repositoryCategory { get; }
        IRepository<Coupon> repositoryCoupon { get; }
        IRepository<Product> repositoryProduct { get; }
        IRepository<Slider> repositorySlider { get; }
        IRepository<Special> repositorySpecial { get; }
        IRepository<Sub_Slider> repositorySub_Slider { get; }
        IRepository<Vehicles> repositoryVehicles { get; }
        IRepository<PhotoLogo> SettingRepository { get; }
        IRepository<SubProducts> repositorySubProducts { get; }
        IRepository<ContactUs> repositoryContactUs{ get; }

        //delete after testing
        ITesting<Brands> repositoryBrand { get; }
        ITesting<Car> repositoryCars { get; }
        ITesting<PhotoLogo> settingRepository { get; }
        ITesting<SubProducts> RepositorySubproducts { get; }


        IListImage<Product> ProductRepository { get; }
        ITesting<Slider> RepositorySlider { get; }
        IListImage<Sub_Slider> RepositorySub_Slider { get; }

        IGetData<Product> RepositoryProducts { get; }
        IGetData<Car> RepositoryCars { get; }
        IGetDataByProducts<SubProducts> RepositorySubProducts { get; }
        IGetById<SubProducts> RepositoryProductsById { get; }
 
        ICartRepository cartRepository { get; }
        IOrderRepository OrderRepository { get; }
        IOrderItemRepository OrderItemRepository { get; }
        ICartItemRepository CartItemRepository { get; }
        Task<bool> SaveChanges();

    }

}
