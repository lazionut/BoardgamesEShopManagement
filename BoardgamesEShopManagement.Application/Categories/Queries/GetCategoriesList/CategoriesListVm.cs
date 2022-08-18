using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardgamesEShopManagement.Application.Categories.Queries.GetCategoriesList
{
    public class CategoriesListVm
    {
        public int Id { get; set; }
        public string BoardgameName { get; set; } = null!;
    }
}
