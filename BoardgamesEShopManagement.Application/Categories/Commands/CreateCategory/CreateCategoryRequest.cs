using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardgamesEShopManagement.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryRequest : IRequest<int>
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
    }
}
