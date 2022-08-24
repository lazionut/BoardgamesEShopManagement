using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.Accounts.Queries.GetAccountsList
{
    public class GetAccountsListQuery : IRequest<List<Account>>
    {

    }
}