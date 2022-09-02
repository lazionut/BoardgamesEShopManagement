using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesListPerCategory
{
    public class GetBoardgamesListPerCategoryQuery : IRequest<List<Boardgame>>
    {
        public int CategoryId { get; set; }
        public int BoardgamePageIndex { get; set; }
        public int BoardgamePageSize { get; set; }
        public string BoardgameSortOrder { get; set; } = "name_ascending" ?? "name_ascending";
    }
}