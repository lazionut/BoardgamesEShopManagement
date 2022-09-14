namespace BoardgamesEShopManagement.Domain.Utils
{
    public static class DateTimeUtils
    {
        public static DateTime GetCurrentDateTimeWithoutMiliseconds()
        {
            return new DateTime
            (DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, DateTime.UtcNow.Hour, DateTime.UtcNow.Minute, DateTime.UtcNow.Second);
        }
    }
}
