using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryRequest : IRequest<Category>
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
    }
}
