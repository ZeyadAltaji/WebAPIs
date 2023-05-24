using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.DTOs
{
    public class ContactUsDTOs
    {
        public int Id { get; set; }
 
        public string Name { get; set; }
 
        public string Email { get; set; }
 
        public string Subject { get; set; }

 
        public string Message { get; set; }

        public bool Show { get; set; }
    }
}
