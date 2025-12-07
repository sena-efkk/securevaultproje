// HomeController.cs içine ekleyebilirsin

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; // Bunu eklemeyi unutma
namespace SecureVault.Web.Controllers
{
    
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    // BU SAYFAYA SADECE IT DEPARTMANI GİREBİLİR
    [Authorize(Policy = "ITOnly")] 
    public IActionResult SecretITPage()
    {
        return Content("Tebrikler! IT Departmanındasın ve bu gizli yazıyı görüyorsun.");
    }
}
}