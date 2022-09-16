namespace BoardgamesEShopManagement.API.Dto
{
    public class AddressGetDto
    {
        public int Id { get; set; }
        public string Details { get; set; } = null!;

        public string City { get; set; } = null!;

        public string County { get; set; } = null!;

        public string Country { get; set; } = null!;

        public string Phone { get; set; } = null!;
    }
}
