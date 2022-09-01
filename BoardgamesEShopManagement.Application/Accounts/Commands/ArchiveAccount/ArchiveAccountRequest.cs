﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Boardgames.Commands.ArchiveAccount
{
    public class ArchiveAccountRequest : IRequest<Account>
    {
        public int AccountId { get; set; }
    }
}