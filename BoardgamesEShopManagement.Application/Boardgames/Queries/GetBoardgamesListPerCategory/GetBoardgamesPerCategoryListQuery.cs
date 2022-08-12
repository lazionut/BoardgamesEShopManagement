using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesListPerCategory
{
    public class GetBoardgamesPerCategoryListQuery : IRequest<IEnumerable<BoardgamesPerCategoryListVm>>
    {
        public int CategoryId { get; set; }
    }
}