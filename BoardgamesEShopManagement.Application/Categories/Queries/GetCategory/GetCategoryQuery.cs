using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Categories.Queries.GetCategory
{
    public class GetCategoryQuery : IRequest<Category>
    {
        public int CategoryId { get; set; }
    }
}