using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Abstractions
{
    public interface IListImage<TEntity>
    {
        Task<TEntity> EditAsyncTest(int id, object entity, IFormFile PrimaryImage, IFormFile Foreign_Image1 , IFormFile Foreign_Image2);
    }
}
