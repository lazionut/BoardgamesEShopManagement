﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Application.RepositoryInterfaces
{
    public interface IBoardgameRepository : IGenericRepository<Boardgame>
    {
        public List<Boardgame> GetBoardgamesPerCategory(int categoryId);
        Boardgame Update(int boardgameId, Boardgame boardgame);
        public void WriteBoardgamesNames(string filePath, List<Boardgame> boardgamesList);
    }
}
