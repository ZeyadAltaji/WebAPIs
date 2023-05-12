using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Abstractions
{
    public interface IGetDataByProducts<TEntity>
    {
        Task<List<SubProducts>> GetSubProductsByProducts(Product product);

    }
}
