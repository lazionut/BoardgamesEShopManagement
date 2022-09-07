using BoardgamesEShopManagement.Domain.Utils;

namespace BoardgamesEShopManagement.Domain.Entities
{
    public abstract class EntityBase
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; init; } = DateTimeUtils.GetCurrentDateTimeWithoutMiliseconds();
        public DateTime UpdatedAt { get; set; } = DateTimeUtils.GetCurrentDateTimeWithoutMiliseconds();
    }
}
