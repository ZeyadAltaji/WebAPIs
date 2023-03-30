using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Models
{
    public  class User
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Frist_Name { get; set; } = string.Empty;
        [Required]
        public string Last_Name { get; set; }=string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string ComfirmPassword { get; set; } = string.Empty;
        
        public string PasswordKey { get; set; } = string.Empty;
        [Required]
        public string Phone1 { get; set; } = string.Empty;
        [Required]
        public string Phone2 { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;

        public int Role { get; set; }
        [Required]
        public string Image { get; set; } = string.Empty;

    }
}
