using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardgamesEShopManagement.Domain.Entities
{
    public abstract class EntityBase
    {
        public int Id { get; set; }
        public string CreationDate { get; init; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
}
