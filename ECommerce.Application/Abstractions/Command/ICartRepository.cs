﻿using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Abstractions.Command
{
    public interface ICartRepository
    {
         void Create(Cart cart);
        Task<Cart> GetById(int id);


    }
}
