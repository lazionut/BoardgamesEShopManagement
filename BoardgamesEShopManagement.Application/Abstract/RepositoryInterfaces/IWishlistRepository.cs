﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces
{
    public interface IWishlistRepository 
    {
        Task Create(int wishlistId, int boardgameId, Wishlist wishlist);
        Task<List<Wishlist>> GetWishlistsListPerAccount(int accountId);
        Task<Wishlist> GetById(int wishlistId);
        Task<Wishlist> GetByAccount(int accountId, int wishlistId);
        Task<bool> Delete(int wishlistId);
        Task Save();
    }
}
