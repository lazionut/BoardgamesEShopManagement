using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

using BoardgamesEShopManagement.API.Dto;
using BoardgamesEShopManagement.Application.Categories.Commands.CreateCategory;
using BoardgamesEShopManagement.Application.Categories.Commands.UpdateCategory;
using BoardgamesEShopManagement.Application.Categories.Queries.GetCategory;

namespace BoardgamesEShopManagement.IntegrationTest
{
    public class CategoriesControllerTests
    {
        private static readonly WebApplicationFactory<Program> _factory = new WebApplicationFactory<Program>();
        private static readonly HttpClient _client = _factory.CreateClient();

        [Fact]
        public async Task Create_Category_ShouldReturnCreatedCategory()
        {
            CreateCategoryRequest newCategory = new CreateCategoryRequest
            {
                CategoryName = "NewlyCreatedCategory"
            };

            HttpResponseMessage response = await _client.PostAsync("api/categories",
                new StringContent(JsonConvert.SerializeObject(newCategory), Encoding.UTF8, "application/json"));

            string result = await response.Content.ReadAsStringAsync();
            CategoryGetDto category = JsonConvert.DeserializeObject<CategoryGetDto>(result);

            Assert.Equal(newCategory.CategoryName, category.Name);
        }

        [Fact]
        public async Task Create_Category_ShouldReturnCreatedResponse()
        {
            CreateCategoryRequest newCategory = new CreateCategoryRequest
            {
                CategoryName = "NewlyCreatedCategory"
            };

            HttpResponseMessage response = await _client.PostAsync("api/categories",
                new StringContent(JsonConvert.SerializeObject(newCategory), Encoding.UTF8, "application/json"));

            Assert.True(response.StatusCode == HttpStatusCode.Created);
        }

        [Fact]
        public async void Get_Categories_List_ShouldReturnOkResponse()
        {
            HttpResponseMessage response = await _client.GetAsync("api/categories");

            Assert.True(response.StatusCode == HttpStatusCode.OK);
        }

        [Fact]
        public async void Get_Categories_List_ShouldReturnExistingCategories()
        {
            GetCategoryQuery searchedCategory = new GetCategoryQuery
            {
                CategoryId = 1
            };

            HttpResponseMessage response = await _client.GetAsync("api/categories");

            string result = await response.Content.ReadAsStringAsync();
            List<CategoryGetDto> categories = JsonConvert.DeserializeObject<List<CategoryGetDto>>(result);

            CategoryGetDto? category = categories.FirstOrDefault(c => c.Id == 1);

            Assert.Equal(searchedCategory.CategoryId, category.Id);
        }

        [Fact]
        public async void Get_Category_ShouldReturnOkResponse()
        {
            HttpResponseMessage response = await _client.GetAsync("api/categories/1");

            Assert.True(response.StatusCode == HttpStatusCode.OK);
        }

        [Fact]
        public async void Get_Category_ShouldReturnExistingCategory()
        {
            GetCategoryQuery searchedCategory = new GetCategoryQuery
            {
                CategoryId = 1
            };

            HttpResponseMessage response = await _client.GetAsync("api/categories");

            string result = await response.Content.ReadAsStringAsync();
            CategoryGetDto category = JsonConvert.DeserializeObject<CategoryGetDto>(result);

            Assert.Equal(searchedCategory.CategoryId, category.Id);
        }

        [Fact]
        public async void Get_Boardgames_Per_Category_ShouldReturnOkResponse()
        {
            HttpResponseMessage response = await _client.GetAsync("api/categories/2/boardgames");

            Assert.True(response.StatusCode == HttpStatusCode.OK);
        }

        [Fact]
        public async void Update_Category_ShouldReturnNoContentResponse()
        {
            UpdateCategoryRequest updatedCategory = new UpdateCategoryRequest
            {
                CategoryId = 1,
                CategoryName = "NewlyCreatedCategory"
            };

            HttpResponseMessage response = await _client.PutAsync("api/categories/1",
                      new StringContent(JsonConvert.SerializeObject(updatedCategory), Encoding.UTF8, "application/json"));

            Assert.True(response.StatusCode == HttpStatusCode.NoContent);
        }

        [Fact]
        public async void Delete_Category_ShouldReturnOkResponse()
        {
            HttpResponseMessage response = await _client.DeleteAsync("api/categories/10");

            Assert.True(response.StatusCode == HttpStatusCode.OK);
        }
    }
}
