using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesListSorted
{
    public class GetBoardgamesListSortedQuery : IRequest<List<Boardgame>>
    {
        public int BoardgamePageIndex { get; set; }
        public int BoardgamePageSize { get; set; }
        public string BoardgameSortOrder { get; set; } = "name_ascending";
    }
}