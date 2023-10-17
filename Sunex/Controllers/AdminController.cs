using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using Sunex.Models;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Sunex.Controllers
{
    [Authorize (Roles = "1 ")]
    public class AdminController : Controller
    {
        private readonly CargoContext _sql;
        private IEnumerable<ClaimsIdentity> claimsIdentity;

        public AdminController(CargoContext sql)
        {
            _sql = sql;
        }
        public IActionResult AddShop()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddShop(Shop shop)
        {
            _sql.Shops.Add(shop);
            _sql.SaveChanges();
            return RedirectToAction("shops", "home");
        }
        public IActionResult delete(int id)
        {
            _sql.Products.RemoveRange(_sql.Products.Include(x=>x.ProductOrder).Where(x => x.ProductOrder.OrderClientId== id));
            _sql.Orders.RemoveRange(_sql.Orders.Where(x => x.OrderClientId == id));
            _sql.Users.Remove(_sql.Users.SingleOrDefault(x => x.Userid == id));
            _sql.SaveChanges();
            return RedirectToAction("users","user");
        }
    }
}
