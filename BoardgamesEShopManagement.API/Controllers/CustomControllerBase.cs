using Microsoft.AspNetCore.Mvc;

namespace BoardgamesEShopManagement.API.Controllers
{
    public abstract class CustomControllerBase : ControllerBase
    {
        protected int GetAccountId() => int.Parse(this.User.Claims.First(claim => claim.Type == "AccountId").Value);
    }
}