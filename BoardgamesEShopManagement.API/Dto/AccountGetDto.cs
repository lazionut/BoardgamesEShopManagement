namespace BoardgamesEShopManagement.API.Dto
{
    public class AccountGetDto
    {
        public int AccountId { get; set; } 
        public string AccountFirstName { get; set; } = null!;
        public string AccountLastName { get; set; } = null!;
        public string AccountEmail { get; set; } = null!;
        public string AccountPassword { get; set; } = null!;
    }
}
