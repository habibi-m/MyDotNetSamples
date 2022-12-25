using Microsoft.AspNetCore.Mvc;

namespace PermissionBasedAuthorization.Controllers
{
    public class ProviderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
