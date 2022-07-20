using CreativPage.Areas.dashboard.DTOs;
using CreativPage.Data;
using CreativPage.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CreativPage.Areas.dashboard.Controllers
{
    [Area("dashboard")]
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

       

        public IActionResult Index()
        {

            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return View();
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if(result.Succeeded)
                return RedirectToAction("Index", "Home");

            return View();  
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            User user = new()
            {
                UserName = model.Email,
                FirstName = model.FirstName,    
                LastName = model.LastName,
                Fullname = model.FirstName + " " + model.LastName,
                Email = model.Email,
            };
            IdentityResult result = await _userManager.CreateAsync(user, model.Password );
            if (result.Succeeded)
            {
                return RedirectToAction("Index");

            }

            return View();
        }

    }
}
