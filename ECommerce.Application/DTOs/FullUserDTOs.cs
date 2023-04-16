using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.DTOs
{
    public class FullUserDTOs:BaseClass
    {
        public string UserName { get; set; } = string.Empty;
        public string Frist_Name { get; set; } = string.Empty;
        public string Last_Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte[] Password { get; set; }
        public byte[] ComfirmPassword { get; set; }
        public byte[] PasswordKey { get; set; }
        public string Phone1 { get; set; } = string.Empty;
        public string Phone2 { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int Role { get; set; }
        public string Image_userUrl { get; set; } = string.Empty;

    }
}
