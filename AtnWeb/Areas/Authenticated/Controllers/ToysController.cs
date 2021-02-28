using System.Collections.Generic;
using System.Linq;
using AtnWeb.Data;
using AtnWeb.Models;
using AtnWeb.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AtnWeb.Areas.Authenticated.Controllers
{
    [Area("Authenticated")]
    [Authorize(Roles = SD.Role_ShopStaff)]
    public class ToysController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ToysController(ApplicationDbContext db)
        {
            _db = db;
        }
        // GET
        public IActionResult Index()
        {
            return View(_db.Toys.ToList());
        }
        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            IEnumerable<Shop> shopList = _db.Shops.ToList();
            ToyViewModel toyViewModel = new ToyViewModel()
            {
                Toy = new Toy(),
                Shops = shopList.Select(I => new SelectListItem
                {
                    Text = I.Location,
                    Value = I.Id.ToString()
                })
            };
            if (id == null)
            {
                return View(toyViewModel);
            }

            toyViewModel.Toy = _db.Toys.Find(id);
            if (toyViewModel.Toy == null)
            {
                return NotFound();
            }
            
            return View(toyViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ToyViewModel toyViewModel)
        {
            if (toyViewModel == null)
            {
                return RedirectToAction(nameof(Index), "Toys");
            }

            if (!ModelState.IsValid)
            {
                IEnumerable<Shop> shopList = _db.Shops.ToList();
                toyViewModel = new ToyViewModel()
                {
                    Toy = new Toy(),
                    Shops = shopList.Select(I => new SelectListItem()
                    {
                        Text = I.Location,
                        Value = I.Id.ToString()
                    })
                };
                return View(toyViewModel);
            }
            
            if (toyViewModel.Toy.Id == 0)
            {
                _db.Toys.Add(toyViewModel.Toy);
            } else
            {
                _db.Toys.Update(toyViewModel.Toy);
            }

            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index), "Toys");
            }

            var toy = _db.Toys.Find(id);
            _db.Toys.Remove(toy);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index), "Toys");
        }
    }
}