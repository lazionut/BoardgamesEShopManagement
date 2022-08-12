using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesList
{
    public class GetBoardgamesListQuery : IRequest<IEnumerable<BoardgamesListVm>>
    {

    }
}