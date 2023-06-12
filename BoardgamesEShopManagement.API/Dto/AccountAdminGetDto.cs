namespace BoardgamesEShopManagement.API.Dto
{
    public class AccountAdminGetDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public AddressGetDto Address { get; set; } = null!;
    }
}