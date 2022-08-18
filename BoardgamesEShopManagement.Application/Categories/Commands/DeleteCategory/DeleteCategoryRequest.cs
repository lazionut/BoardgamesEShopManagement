﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryRequest : IRequest<bool>
    {
        public int CategoryId { get; set; }
    }
}