using System.Linq;
using AtnWeb.Data;
using AtnWeb.Models;
using AtnWeb.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AtnWeb.Areas.Authenticated.Controllers
{
    [Area("Authenticated")]
    [Authorize(Roles = SD.Role_Manager)]
    public class ShopsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ShopsController(ApplicationDbContext db)
        {
            _db = db;
        }
        // GET
        public IActionResult Index()
        {
            return View(_db.Shops.ToList());
        }
        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            
            if (id == null)
            {
                return View(new Shop());
            }

            Shop shop = _db.Shops.Find(id);
            if (shop == null)
            {
                return NotFound();
            }
            
            return View(shop);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Shop shop)
        {
            if (shop == null)
            {
                return RedirectToAction(nameof(Index), "Shops");
            }

            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            
            if (shop.Id == 0)
            {
                _db.Shops.Add(shop);
            } else
            {
                _db.Shops.Update(shop);
            }

            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index), "Shops");
            } 

            var shop = _db.Shops.Find(id);
            _db.Shops.Remove(shop);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index), "Shops");
        }
    }
}