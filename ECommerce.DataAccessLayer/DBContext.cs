using ECommerce.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccessLayer
{
    public class DBContext :DbContext
    {
        public DBContext(DbContextOptions option):base(option)
        {
            
        }
        public DbSet<User>Users { get; set; }
    }

}
