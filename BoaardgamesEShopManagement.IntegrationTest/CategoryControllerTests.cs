using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

using BoardgamesEShopManagement.API.Dto;

namespace BoardgamesEShopManagement.IntegrationTest
{
    [TestClass]
    public class CategoryControllerTests
    {
        private static WebApplicationFactory<Program> _factory;

        [TestMethod]
        public async void Get_Categories_List_GetCategoriesListQueryIsCalled()
        {
            HttpClient client = _factory.CreateClient();
            HttpResponseMessage response = await client.GetAsync("api/categories");

            string result = await response.Content.ReadAsStringAsync();
            CategoryGetDto categories = JsonConvert.DeserializeObject<CategoryGetDto>(result);

            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }

    }
}
