using MediatR;

namespace BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesListByNameCounter
{
    public class GetBoardgamesListByNameCounterQuery : IRequest<int>
    {
        public string BoardgameNameCharacters { get; set; } = null!;
    }
}