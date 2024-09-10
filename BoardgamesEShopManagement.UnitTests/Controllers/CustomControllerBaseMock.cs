using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BoardgamesEShopManagement.API.Controllers;

namespace BoardgamesEShopManagement.UnitTests.Controllers
{
    public class CustomControllerBaseMock : CustomControllerBase
    {
        public CustomControllerBaseMock()
        {
            var claims = new List<Claim>
            {
                new Claim("AccountId", "1")
            };

            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext
            {
                User = claimsPrincipal
            };

            ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
        }

        public int PublicGetAccountId()
        {
            return GetAccountId();
        }
    }
}