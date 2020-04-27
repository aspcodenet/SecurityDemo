using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecurityDemo.Data;
using SecurityDemo.ViewModels;

namespace SecurityDemo.Controllers
{
    
    public class PlayerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlayerController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin,Manager")]
        public IActionResult Index()
        {
            var viewModel = _context.Players.Select(p => new PlayerListViewModel { Id = p.Id, Name = p.Name }).ToList();
            return View(viewModel);
        }

        [Authorize(Roles = "RegularUser")]
        public IActionResult Test()
        {
            return View();
        }

        [Authorize]
        public IActionResult Hejhopp()
        {
            return View();
        }


        public IActionResult Blabla()
        {
            return View();
        }


    }
}