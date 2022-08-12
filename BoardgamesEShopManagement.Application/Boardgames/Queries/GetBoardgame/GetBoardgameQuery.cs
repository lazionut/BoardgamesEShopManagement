using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgame
{
    public class GetBoardgameQuery : IRequest<Boardgame>
    {
        public int BoardgameId { get; set; }
    }
}