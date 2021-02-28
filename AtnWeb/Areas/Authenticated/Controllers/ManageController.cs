using System.Collections.Generic;
using System.Linq;
using AtnWeb.Data;
using AtnWeb.Models;
using AtnWeb.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AtnWeb.Areas.Authenticated.Controllers
{
    [Area("Authenticated")]
    [Authorize(Roles = SD.Role_Manager)]
    public class ManageController: Controller
    {
        private readonly ApplicationDbContext _db;
        public ManageController(ApplicationDbContext db)
        {
            _db = db;
        }
        // GET
        public IActionResult Index()
        {
            IEnumerable<Shop> shopList = _db.Shops.ToList();
            return View(shopList);
        }

        public IActionResult Manage(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            return View(_db.Toys.Where(t => t.ShopId == id ));
        }
    }
}