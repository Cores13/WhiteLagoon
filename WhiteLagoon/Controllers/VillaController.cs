using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;

namespace WhiteLagoon.Controllers
{
    public class VillaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VillaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: VillaController
        public IActionResult Index()
        {
            var villas = _context.Villas.ToList();
            return View(villas);
        }

        // GET: VillaController/Details/5
        public IActionResult Details(int id)
        {
            var villa = _context.Villas.FindAsync(id);

            return View();
        }

        // GET: VillaController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VillaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Villa request)
        {
            try
            {
                if(request.Name == request.Description)
                {
                    ModelState.AddModelError("", "The Description cannot exactly match the Name.");
                }
                if (ModelState.IsValid)
                {
                    var villa = new Villa
                    {
                        Name = request.Name,
                        Description = request.Description,
                        Price = request.Price,
                        Sqm = request.Sqm,
                        Occupancy = request.Occupancy,
                        ImageUrl = request.ImageUrl
                    };
                    _context.Villas.Add(villa);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }else
                {
                    return View();
                }
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: VillaController/Edit/5
        public IActionResult Edit(int id)
        {
            var villa = _context.Villas.Find(id);
            if (villa is null)
            {
                return BadRequest();
            }
            return View(villa);
        }

        // POST: VillaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Villa request)
        {
            try
            {
                if (request.Name == request.Description)
                {
                    ModelState.AddModelError("", "The Description cannot exactly match the Name.");
                }
                if (ModelState.IsValid)
                {
                    var villa = await _context.Villas.FindAsync(id);
                    if(villa is null)
                    {
                        return BadRequest();
                    }
                    //villa = {
                    villa.Name = request.Name;
                    villa.Description = request.Description;
                    villa.Price = request.Price;
                    villa.Sqm = request.Sqm;
                    villa.Occupancy = request.Occupancy;
                    villa.ImageUrl = request.ImageUrl;
                    //};
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: VillaController/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: VillaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var villa = _context.Villas.FindAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
