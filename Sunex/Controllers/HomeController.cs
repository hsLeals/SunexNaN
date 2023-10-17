using Microsoft.AspNetCore.Mvc;
using Sunex.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Sunex.Controllers
{
    public class HomeController : Controller
    {
        private readonly CargoContext _sql;
        private IEnumerable<ClaimsIdentity> claimsIdentity;

        public HomeController(CargoContext sql)
        {
            _sql = sql;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult tarifs()
        {
            return View();
        }
        public IActionResult conditions()
        {
            return View();
        }
        public IActionResult services()
        {
            return View();
        }
        public IActionResult shops()
        {
            return View(_sql.Shops.ToList());
        }
        public IActionResult connection()
        {
            return View();
        }

        
    }
}
