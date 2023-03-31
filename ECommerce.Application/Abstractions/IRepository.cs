using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Abstractions
{
    public interface IRepository<TEntity>
    {
        public void Create(TEntity entity);
        public void Update(int Id, TEntity entity);
        public void Delete(int Id, TEntity entity);
        void Active(int Id, TEntity entity);
        IList<TEntity> GetAllViewFrontClinet();

        TEntity GetByID(int id);

        List<TEntity> GetAll();
    }
}
