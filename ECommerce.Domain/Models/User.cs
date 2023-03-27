using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Models
{
    public  class User
    {
        public int ID { get; set; }
        public string Frist_Name { get; set; } = string.Empty;
        public string Last_Name { get; set; }=string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ComfirmPassword { get; set; } = string.Empty;
        public string PasswordKey { get; set; } = string.Empty;
        public string Phone1 { get; set; } = string.Empty;
        public string Phone2 { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int Role { get; set; }
        public string Image { get; set; }=string.Empty
            //زيد ابطيخة;

    }
}
