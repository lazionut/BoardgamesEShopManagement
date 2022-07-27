using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoardgamesEShopManagement.Domain.Models;
using BoardgamesEShopManagement.Domain.Exceptions;

namespace BoardgamesEShopManagement.DAL.Repository
{
    public class BoardgameRepository
    {
        public static List<Boardgame> Boardgames = new List<Boardgame>();

        public static void GetBoardgames()
        {
            foreach (Boardgame boardgame in Boardgames)
            {
                Console.WriteLine(boardgame);
            }
        }

        public static void GetBoardgame(int id)
        {
            if (id >= 0)
            {
                foreach (Boardgame boardgame in Boardgames)
                {
                    if (boardgame.BoardgameId == id)
                        Console.WriteLine(boardgame);
                }
            }
            else
            {
                throw new NegativeIdException();
            }
        }


        public static void AddBoardgame(Boardgame boardgame)
        {
            if (boardgame == null)
                throw new BoardgameException();

            Boardgames.Add(boardgame);
        }

        public static void ChangeBoardgame(int id)
        {
            if (id >= 0)
            {
                int totalBoardgames = Boardgames.Count;
                for (int index = 0; index < totalBoardgames; ++index)
                {
                    if (Boardgames[index].BoardgameId == id)
                        Boardgames[index] = new Boardgame();
                }
            }
            else
            {
                throw new NegativeIdException();
            }
        }

        public static void RemoveBoardgame(int id)
        {
            if (id >= 0)
            {
                int totalBoardgames = Boardgames.Count;
                for (int index = 0; index < totalBoardgames; ++index)
                {
                    if (Boardgames[index].BoardgameId == id)
                    {
                        Boardgames.RemoveAt(index);
                        totalBoardgames = Boardgames.Count;
                    }
                }
            }
            else
            {
                throw new NegativeIdException();
            }

        }
    }
}
