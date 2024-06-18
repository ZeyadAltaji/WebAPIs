using ECommerce.Application.Abstractions;
using ECommerce.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.DataAccessLayer.Repositories
{
    public class MessagesRepository : IRepository<Messages>
    {
        private readonly DBContext Dc;


        public MessagesRepository (DBContext dc)
        {
            Dc = dc;
        }
        public void Active (int Id , Messages entity)
        {
            throw new NotImplementedException();
        }

        public void Create (Messages entity)
        {
            throw new NotImplementedException();
        }

        public void Delete (int Id , Messages entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Messages>> GetAll () => await Dc.Messages.ToListAsync();


        public IList<Messages> GetAllViewFrontClinet ()
        {
            throw new NotImplementedException();
        }

        public Task<Messages> GetByID (int id)
        {
            throw new NotImplementedException();
        }

        public void Update (int Id , Messages entity)
        {
            throw new NotImplementedException();
        }
    }
}
