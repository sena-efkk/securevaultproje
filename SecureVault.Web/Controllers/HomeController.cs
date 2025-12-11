// HomeController.cs içine ekleyebilirsin

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; // Bunu eklemeyi unutma
namespace SecureVault.Web.Controllers
{
    
public class HomeController : Controller
{
    private readonly IHttpContextAccessor _contextAccessor;

    public HomeController(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public IActionResult Index()
    {
        var profile = _contextAccessor.HttpContext?.User?.Claims;
        return View(profile);
    }

    // BU SAYFAYA SADECE IT DEPARTMANI GİREBİLİR
    [Authorize(Policy = "ITOnly")] 
    public IActionResult SecretITPage()
    {
        return Content("Tebrikler! IT Departmanındasın ve bu gizli yazıyı görüyorsun.");
    }
}
}