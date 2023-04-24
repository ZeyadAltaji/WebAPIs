using ECommerce.Application.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Abstractions
{
    public interface ITesting<TEntity>
    {
         Task<TEntity> EditAsyncTest(int id, object entity, IFormFile img);


    }
}
