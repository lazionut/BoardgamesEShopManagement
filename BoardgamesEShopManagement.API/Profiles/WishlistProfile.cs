using AutoMapper;

using BoardgamesEShopManagement.API.Dto;
using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.API.Profiles
{
    public class WishlistProfile : Profile
    {
        public WishlistProfile()
        {
            CreateMap<Wishlist, WishlistGetDto>()
                .ForMember(w => w.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(w => w.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(w => w.AccountId, opt => opt.MapFrom(s => s.AccountId))
                .ForMember(w => w.CreationDate, opt => opt.MapFrom(s => s.CreatedAt))
                .ForMember(w => w.UpdateDate, opt => opt.MapFrom(s => s.UpdatedAt))
                .ForMember(w => w.Boardgames, opt => opt.MapFrom(s => s.Boardgames.Select(
                    b => new WishlistBoardgameDto { 
                        Id = b.Id, 
                        Image = b.Image,
                        Name = b.Name,
                        ReleaseYear = b.ReleaseYear, 
                        Description = b.Description, 
                        Price = b.Price, 
                        Link = b.Link 
                    })));

            CreateMap<Wishlist, WishlistPostDto>();

            CreateMap<WishlistItem, WishlistPutDto>();
        }
    }
}
