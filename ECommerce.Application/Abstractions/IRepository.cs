using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Abstractions
{
    public interface IRepository<TEntity>
    {
        void Create(TEntity entity);
        void Update(int Id, TEntity entity);
        void Delete(int Id, TEntity entity);
        void Active(int Id, TEntity entity);
        IList<TEntity> GetAllViewFrontClinet();

        Task<TEntity>GetByID(int id);

        Task<IEnumerable<TEntity>> GetAll();
    }
}
