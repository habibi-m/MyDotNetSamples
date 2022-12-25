using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PermissionBasedAuthorization.Constants;

namespace PermissionBasedAuthorization.Controllers
{
    public class ProductController : Controller
    {
        private IAuthorizationService _authorizationService;

        public ProductController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            if(!(await _authorizationService.AuthorizeAsync(User, Permissions.Products.Create)).Succeeded)
            {
                return RedirectToAction("AccessDenied!");
            }

            return View();
        }
    }
}
