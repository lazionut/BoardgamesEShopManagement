using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.RepositoryInterfaces;

namespace BoardgamesEShopManagement.Infrastructure.Repositories
{
    public class BoardgameRepository : GenericRepository<Boardgame>, IBoardgameRepository
    {
        
    }
}
