using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SecureVault.Web.Models;
using SecureVault.Web.Services;
//Controller'ın Dili HTTP'dir: 200 OK, 404 Not Found, Cookie, Header,
//                             JSON, ViewBag. Controller internetin dilini konuşur.
namespace SecureVault.Web
{
    public class AccountController : Controller
    {
        private readonly AuthService _authService;
        public AccountController(AuthService a)
        {
            this._authService = a;
        }


        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // 1. Kullanıcıyı doğrula (AuthService işini yapıyor)
            var user = await _authService.ValidateUser(model.Username, model.Password);

            if (user == null)
            {
                // Güvenlik: Asla "Şifre yanlış" veya "Kullanıcı yok" diye ayrı ayrı söyleme.
                // Brute-force saldırganına ipucu verme.
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı.");
                return View(model);
            }

            // Kullanıcının kimliği(Claims)
            //ıdentitty kütüp. bunu otomatik yapar biz elle yapıyoruz.
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username), // User.Identity.Name için
            new Claim(ClaimTypes.Role, user.Role), // [Authorize(Roles="Admin")] için
            new Claim("Department", user.Department), // Özel claim
            new Claim("Seniority", user.Seniority.ToString()), // Özel claim
            new Claim("UserId", user.Id.ToString()) // İleride DB ilişkileri için lazım olur
        };

            //kimliği oluşturuyoruz
            var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");

            // principal = cüzdan gbi düşün
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            //Cookie Ayarları
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true, // Beni hatırla özelliği
                ExpiresUtc = DateTime.UtcNow.AddMinutes(20) 
            };

            // sisteme girdi :Cookie'nin şifrelenip Responsea yazıldığı an
            //Tarayıcıya "Set-Cookie" header'ını basan komut budur.
            await HttpContext.SignInAsync("CookieAuth", claimsPrincipal, authProperties);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            //cookieyi siliyoruz.
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _authService.RegisterUser(model.Username, model.Password, "User");

            return RedirectToAction("Login");
        }
    }
}