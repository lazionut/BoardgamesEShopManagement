using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoardgamesEShopManagement.Domain.Models;

namespace BoardgamesEShopManagement.Domain
{
    public static class BoardgameReader
    {
        public static void readBoardgamesNames (string filePath)
        {
            try
            {
                using (StreamReader boardgameStreamReader = new StreamReader(filePath))
                {
                    Console.WriteLine(boardgameStreamReader.ReadToEnd());
                }
            }
            catch 
            {
                Console.WriteLine("The file couldn't be read!");
            }
        }
    }
}
