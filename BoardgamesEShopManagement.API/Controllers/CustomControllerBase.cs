using Microsoft.AspNetCore.Mvc;

namespace BoardgamesEShopManagement.API.Controllers
{
    public class CustomControllerBase : ControllerBase
    {
        protected virtual int GetAccountId() =>
            int.Parse(this.User.Claims.First(claim => claim.Type == "AccountId").Value);
    }
}