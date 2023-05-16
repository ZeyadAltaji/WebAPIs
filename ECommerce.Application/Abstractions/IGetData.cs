using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Abstractions
{
    public interface IGetData<TEntity>
    {
         Task<List<Product>> GetProductsByBrand(Brands brand);
         Task<List<Product>> GetProductsByCars(Car car);
        Task<List<Product>> GetProductsByCategory(Category category);
    }
}
