using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoardgamesEShopManagement.Domain.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BoardgamesEShopManagement.Domain.Entities
{
    public abstract class EntityBase
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; init; } = DateTimeUtils.GetCurrentDateTimeWithoutMiliseconds();
        public DateTime UpdatedAt { get; set; } = DateTimeUtils.GetCurrentDateTimeWithoutMiliseconds();
    }
}
