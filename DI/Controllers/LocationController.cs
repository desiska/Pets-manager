using DI.Data;
using DI.Data.Entities;
using DI.Models.Location;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DI.Controllers
{
    public class LocationController : Controller
    {
        private readonly ApplicationDbContext db;

        public LocationController()
        {
            db = new ApplicationDbContext();
        }

        public IActionResult Index()
        {
            List<Location> locations = db.Locations.ToList();
            List<LocationVM> items = new List<LocationVM>();
            locations.ForEach(l => items.Add(new LocationVM()
            {
                ID = l.ID,
                Name = l.Name,
                Address = l.Address
            }));

            LocationIndexVM model = new LocationIndexVM()
            {
                Items = items
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            LocationCreateVM model = new LocationCreateVM();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(LocationCreateVM model)
        {
            if (ModelState.IsValid)
            {
                Location location = new Location()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = model.Name,
                    Address = model.Address
                };

                db.Locations.Add(location);
                await db.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(string? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            Location location = db.Locations.Find(id);

            if(location == null)
            {
                return NotFound();
            }

            LocationEditVM model = new LocationEditVM()
            {
                ID = location.ID,
                Name = location.Name,
                Address = location.Address
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(LocationEditVM model)
        {
            if (ModelState.IsValid)
            {
                Location location = db.Locations.Find(model.ID);

                location.Name = model.Name;
                location.Address = model.Address;

                db.Locations.Update(location);
                await db.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(string? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            Location location = db.Locations.Find(id);

            if(location == null)
            {
                return NotFound();
            }

            db.Locations.Remove(location);

            db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
