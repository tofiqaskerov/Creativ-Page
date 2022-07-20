using CreativPage.Data;
using CreativPage.Models;
using CreativPage.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CreativPage.Controllers
{
    public class HomeController : Controller
    {

        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var banner = _context.Banners.FirstOrDefault();
            var aboutBanner  = _context.AboutBanners.FirstOrDefault();
            var services = _context.Services.ToList();
            var portfolios = _context.Portfolios.Include( x =>x.Category).ToList();

            HomeVM vm = new()
            {
                Banner = banner,
                AboutBanner = aboutBanner,
                Services = services,
                Portfolios = portfolios
            };
           
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Contact(Contact contact)
        {
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
