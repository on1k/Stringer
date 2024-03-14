using Microsoft.AspNetCore.Mvc;

namespace Stringer.Data.Controllers;

[Produces("application/json")]
public class BaseController : ControllerBase
{
    public string? UserId => Request.HttpContext.User.Claims.FirstOrDefault(i => i.Type == "UserId").Value;
}