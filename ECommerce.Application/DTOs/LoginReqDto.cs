﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.DTOs
{
    public class LoginReqDto
    {
        public string Frist_Name { get; set; } = string.Empty;
        public string Last_Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ComfirmPassword { get; set; } = string.Empty;
        public int Role { get; set; }

    }
}
