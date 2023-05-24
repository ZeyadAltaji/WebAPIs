using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Abstractions
{
    public interface IGetById<TEntity>
    {
        Task<IEnumerable<SubProducts>> GetAllById(int userId);

    }
}
