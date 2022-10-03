using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Domain.Enumerations;

namespace BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesListByNameCounter
{
    public class GetBoardgamesListByNameCounterQuery : IRequest<int>
    {
        public string BoardgameNameCharacters { get; set; } = null!;
    }
}