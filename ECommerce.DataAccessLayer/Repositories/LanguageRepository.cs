using ECommerce.Application.Abstractions;
using ECommerce.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.DataAccessLayer.Repositories
{
    public class LanguageRepository : IRepository<Language>
    {
        private readonly DBContext Dc;


        public LanguageRepository (DBContext dc)
        {
            Dc = dc;
        }
        public void Active (int Id , Language entity)
        {
            throw new NotImplementedException();
        }

        public void Create (Language entity)
        {
            throw new NotImplementedException();
        }

        public void Delete (int Id , Language entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Language>> GetAll () => await Dc.Language.ToListAsync();



        public IList<Language> GetAllViewFrontClinet ()
        {
            throw new NotImplementedException();
        }

        public Task<Language> GetByID (int id)
        {
            throw new NotImplementedException();
        }

        public void Update (int Id , Language entity)
        {
            throw new NotImplementedException();
        }
    }
}
