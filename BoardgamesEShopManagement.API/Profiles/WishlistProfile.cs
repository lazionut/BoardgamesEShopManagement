﻿using AutoMapper;

using BoardgamesEShopManagement.API.Dto;
using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.API.Profiles
{
    public class WishlistProfile : Profile
    {
        public WishlistProfile()
        {
            CreateMap<Wishlist, WishlistGetDto>()
                .ForMember(w => w.WishlistId, opt => opt.MapFrom(s => s.Id))
                .ForMember(w => w.WishlistName, opt => opt.MapFrom(s => s.Name))
                .ForMember(w => w.WishlistAccountId, opt => opt.MapFrom(s => s.AccountId))
                .ForMember(w => w.WishlistBoardgames, opt => opt.MapFrom(s => s.Boardgames.Select(
                    b => new { b.Image, b.Name, b.Description, b.Price, b.Link })));

            CreateMap<Wishlist, WishlistPostDto>()
                .ForMember(w => w.WishlistAccountId, opt => opt.MapFrom(s => s.AccountId));
        }
    }
}
