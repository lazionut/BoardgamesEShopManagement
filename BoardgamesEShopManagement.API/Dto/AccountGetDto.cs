﻿namespace BoardgamesEShopManagement.API.Dto
{
    public class AccountGetDto
    {
        public int Id { get; set; } 
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int AddressId { get; set; } 
    }
}
