using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoardgamesEShopManagement.Domain.Models;

namespace BoardgamesEShopManagement.Domain
{
    public static class BoardgameWriter
    {
        public static void writeBoardgamesNames(string filePath, List<Boardgame> boardgamesList)
        {
            using (StreamWriter boardgameStreamWriter = new StreamWriter(filePath))
            {
                foreach (Boardgame boardgame in boardgamesList)
                {
                    boardgameStreamWriter.WriteLine(boardgame.BoardgameName);
                }
            }

        }
    }
}
