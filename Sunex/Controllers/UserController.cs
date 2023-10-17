using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using Sunex.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;
using System.IO;
using Sunex.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sunex.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly CargoContext _sql;
        private IEnumerable<ClaimsIdentity> claimsIdentity;

        public UserController(CargoContext sql)
        {
            _sql = sql;
        }
        [AllowAnonymous]
        public IActionResult login()
        {
            ViewBag.Branchs = _sql.Branchs.ToList();
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult login(User user)
        {

            var login = _sql.Users.SingleOrDefault(x => x.UserEmail == user.UserEmail && x.UserPassword == user.UserPassword);

            if (login != null)
            {

                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Sid, login.Userid.ToString()),
                    new Claim(ClaimTypes.Email, login.UserEmail),
                    new Claim(ClaimTypes.NameIdentifier, login.UserName + " " + login.UserSurname),
                    new Claim(ClaimTypes.Version, login.UserBranchId.ToString()),
                    new Claim(ClaimTypes.Role, login.UserStatusId.ToString())
                };

                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var prins = new ClaimsPrincipal(identity);
                var props = new AuthenticationProperties();
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, prins, props);




                return RedirectToAction("Index", "home");
            }

            else
            {
                return View();

            }

        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult signup(User user)
        {

            _sql.Users.Add(user);
            user.UserStatusId = 3;
            _sql.SaveChanges();
            return RedirectToAction("login", "user");
        }
        public IActionResult neworder()
        {
            return View();
        }
        public IActionResult addorder(List<Product> products)
        {
            Order o = new Order();
            o.OrderBranchId = int.Parse(User.FindFirstValue(ClaimTypes.Version));
            o.OrderClientId = int.Parse(User.FindFirstValue(ClaimTypes.Sid));
            _sql.Orders.Add(o);
            _sql.SaveChanges();

            foreach (Product product in products)
            {
                product.ProductOrderId = o.OrderId;
            }
            _sql.Products.AddRange(products);
            _sql.SaveChanges();

            return RedirectToAction("neworder", "user");
        }
        public IActionResult myorder(int id)
        {
            using (var item = new CargoContext())
            {
                var UserOrder = item.Orders.Include(x=>x.Products).Where(x => x.OrderClientId == int.Parse(User.FindFirstValue(ClaimTypes.Sid))).ToList();
                return View(UserOrder);
            }
        }
        [Authorize(Roles = "1 , 2")]
        public IActionResult confirmation(int id = 1)
        {
            var q = _sql.Orders.Include(x => x.OrderClient).Select(x => new OpOrders
            {
                FullName = x.OrderClient.UserName + " " + x.OrderClient.UserSurname,
                Date = x.OrderDate.ToString("dd.MM.yyyy hh:mm"),
                count = x.Products.Count,
                OrderId = x.OrderId,
                levelId = x.OrderLevelId
            });
            if (id == 1)
            {
                q = q.Where(x => x.levelId == null);
            }
            else if (id == 2)
            {
                q = q.Where(x => x.levelId > 1 && x.levelId < 5);
            }
            else
            {
                q = q.Where(x => x.levelId == 5);
            }
            return View(q.ToList());
        }
        public IActionResult GetOrderProducts(int id)
        {
            var result = _sql.Products.Where(x => x.ProductOrderId == id).ToList();
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult AcceptProduct(int id, int? price, int? cargo)
        {
            var data = _sql.Products.SingleOrDefault(x => x.ProductId == id);
            if (data != null)
            {
                data.ProductPrice = price;
                data.ProductCargoAmount = cargo;
                _sql.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        public IActionResult RemoveProduct(int id)
        {
            var data = _sql.Products.SingleOrDefault(x => x.ProductId == id);
            if (data != null)
            {
                _sql.Products.Remove(data);
                _sql.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        public IActionResult RemoveOrder(int id)
        {
            var data = _sql.Orders.SingleOrDefault(x => x.OrderId == id);
            _sql.Products.RemoveRange(_sql.Products.Where(x => x.ProductOrderId == id).ToList());
            _sql.Orders.Remove(data);
                _sql.SaveChanges();
            return RedirectToAction("confirmation", "user");

        }
        [HttpPost]
        public IActionResult ChangeLevel(int id, int levelId)
        {
            Order order = _sql.Orders.SingleOrDefault(x => x.OrderId == id);
            if (order != null)
            {
                order.OrderLevelId = levelId;
                _sql.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "1 ")]
        public IActionResult users()
        {
            return View(_sql.Users.ToList());
        }
        public IActionResult settings(int id)
        {
            ViewBag.Branchs = _sql.Branchs.ToList();
            var a = _sql.Users.SingleOrDefault(x => x.Userid == id);
            return View(a);
        }

        public IActionResult longout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "home");
        }


        [HttpPost]

        public IActionResult edit(int id, User user)
        {
            var old = _sql.Users.SingleOrDefault(x => x.Userid == id);

            old.UserName = user.UserName;
            old.UserSurname = user.UserSurname;
            old.UserEmail = user.UserEmail;
            old.UserPhone = user.UserPhone;
            old.UserPassword = user.UserPassword;
            old.UserBranchId = user.UserBranchId;
            _sql.SaveChanges();
            return RedirectToAction("myorder", "user");
        }
        [Authorize(Roles = "1 , 2")]
        public IActionResult notification(int id = 1)
        {
            var q = _sql.Connections.Where(x=>1==1);
            if (id == 1)
            {
                q = q.Where(x => x.ConnectionLvl==1);
            }
            else if (id == 2)
            {
                q = q.Where(x => x.ConnectionLvl == 2);
            }
            return View(q.ToList());
        }
        [HttpPost]
        public IActionResult AddNotification(Connection Connection)
        {
            _sql.Connections.Add(Connection);
            Connection.ConnectionLvl = 1;
            _sql.SaveChanges();
            return RedirectToAction("connection", "home");
        }
    }
}
