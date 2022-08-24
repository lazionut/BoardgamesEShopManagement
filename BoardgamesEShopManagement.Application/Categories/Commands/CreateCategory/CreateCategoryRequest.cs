using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryRequest : IRequest<Category>
    {
        public string CategoryName { get; set; } = null!;
    }
}
