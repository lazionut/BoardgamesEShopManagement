﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Reviews.Commands.UpdateReview
{
    public class UpdateReviewRequest : IRequest<Review>
    {
        public int ReviewId { get; set; }
        public Review Review { get; set; } = null!;
    }
}
