using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoardgamesEShopManagement.Domain.Entities;

namespace BoardgamesEShopManagement.Infrastructure.Services
{
    public class BoardgameService
    {
        private static BoardgameService _instance;

        private BoardgameService()
        {

        }

        public static BoardgameService Instance
        {
            get { return _instance ??= new BoardgameService(); }
            private set { }
        }

        public void GetBoardgames(List<Boardgame> boardgamesList)
        {
            boardgamesList.ForEach(item => Console.WriteLine(item));
        }
    }
}
