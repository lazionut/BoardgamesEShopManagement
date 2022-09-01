using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

using BoardgamesEShopManagement.Application.Accounts.Commands.UpdateAccount;

namespace BoardgamesEShopManagement.IntegrationTest
{
    public class AccountControllersTest
    {
        private static readonly WebApplicationFactory<Program> _factory = new WebApplicationFactory<Program>();
        private static readonly HttpClient _client = _factory.CreateClient();

        [Fact]
        public async Task Update_Account_Name_ShouldReturnNoContent()
        {
            UpdateAccountRequest updatedAccount = new UpdateAccountRequest
            {
                AccountFirstName = "FirstName",
                AccountLastName = "LastName"
            };

            HttpResponseMessage response = await _client.PatchAsync("api/accounts/1/change-name",
                new StringContent(JsonConvert.SerializeObject(updatedAccount), Encoding.UTF8, "application/json"));

            Assert.True(response.StatusCode == HttpStatusCode.NoContent);
        }
    }
}
