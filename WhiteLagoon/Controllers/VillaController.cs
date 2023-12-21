using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WhiteLagoon.Application.Interfaces;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;

namespace WhiteLagoon.Controllers
{
    public class VillaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public VillaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: VillaController
        public IActionResult Index()
        {
            var villas = _unitOfWork.Villa.GetAll();
            return View(villas);
        }

        // GET: VillaController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VillaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Villa request)
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
                    _unitOfWork.Villa.Add(villa);
                    _unitOfWork.Villa.Save();
                    TempData["success"] = "The villa has been added successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["error"] = "The data is invalid";
                    return View();
                }
            }
            catch
            {
                TempData["error"] = "An unexpected error ocurred";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: VillaController/Edit/5
        public IActionResult Edit(int id)
        {
            var villa = _unitOfWork.Villa.Get(v => v.Id == id);
            if (villa is null)
            {
                return BadRequest();
            }
            return View(villa);
        }

        // POST: VillaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Villa request)
        {
            try
            {
                if (request.Name == request.Description)
                {
                    ModelState.AddModelError("", "The Description cannot exactly match the Name.");
                }
                if (ModelState.IsValid)
                {
                    var villa = _unitOfWork.Villa.Get(v => v.Id == id);
                    if (villa is null)
                    {
                        return BadRequest();
                    }

                    _unitOfWork.Villa.Update(request);
                    _unitOfWork.Villa.Save();

                    TempData["success"] = "The villa has been updated successfully";

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["error"] = "The data is invalid";
                    return View();
                }
            }
            catch
            {
                TempData["error"] = "An unexpected error ocurred";
                return View();
            }
        }

        // GET: VillaController/Delete/5
        public IActionResult Delete(int id)
        {
            var villa = _unitOfWork.Villa.Get(v => v.Id == id);
            if (villa is null)
            {
                return BadRequest();
            }
            return View(villa);
        }

        // POST: VillaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var villa = _unitOfWork.Villa.Get(v => v.Id == id);
                if (villa is null)
                {
                    return BadRequest();
                }

                _unitOfWork.Villa.Remove(villa);
                _unitOfWork.Villa.Save();

                TempData["success"] = "The villa has been deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["error"] = "An unexpected error ocurred";
                return View();
            }
        }
    }
}
