using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WhiteLagoon.Application.Interfaces;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.ViewModels;

namespace WhiteLagoon.Controllers
{
    public class AmenityController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AmenityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: VillaNumberController
        public IActionResult Index()
        {
            var villaNumbers = _unitOfWork.Amenity.GetAll(includeProperties: "Villa");
            return View(villaNumbers);
        }

        // GET: VillaNumberController/Create
        public IActionResult Create()
        {
            AmenityVM amenityVM = new()
            {
                VillaList = _unitOfWork.Amenity.GetAll().Select(v => new SelectListItem
                {
                    Text = v.Name,
                    Value = v.Id.ToString()
                })
            };

            return View(amenityVM);
        }

        // POST: VillaNumberController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AmenityVM request)
        {
            try
            {
                bool roomNumberExists = _unitOfWork.Amenity.Any(v => v.Id == request.Amenity.Id);
                if(roomNumberExists)
                {
                    TempData["error"] = "Amenity already exists";
                    return RedirectToAction(nameof(Index));
                }
                if (ModelState.IsValid)
                {
                    _unitOfWork.Amenity.Add(request.Amenity);
                    TempData["success"] = "The amenity has been added successfully";
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

        // GET: VillaNumberController/Edit/5
        public IActionResult Edit(int id)
        {
            AmenityVM amenityVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(v => new SelectListItem
                {
                    Text = v.Name,
                    Value = v.Id.ToString()
                }),
                Amenity = _unitOfWork.Amenity.Get(v => v.Id == id)
            };
            if (amenityVM.Amenity is null)
            {
                return BadRequest();
            }
            return View(amenityVM);
        }

        // POST: VillaNumberController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AmenityVM request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.Amenity.Update(request.Amenity);

                    TempData["success"] = "The amenity has been updated successfully";

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

        // GET: VillaNumberController/Delete/5
        public IActionResult Delete(int id)
        {
            AmenityVM villaNumberVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(v => new SelectListItem
                {
                    Text = v.Name,
                    Value = v.Id.ToString()
                }),
                Amenity = _unitOfWork.Amenity.Get(v => v.Id == id)
            };
            if (villaNumberVM.Amenity is null)
            {
                return BadRequest();
            }
            return View(villaNumberVM);
        }

        // POST: VillaNumberController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(AmenityVM request)
        {
            try
            {
                var amenity = _unitOfWork.Amenity.Get(v => v.Id == request.Amenity.Id);
                if (amenity is null)
                {
                    return BadRequest();
                }

                _unitOfWork.Amenity.Remove(amenity);

                    TempData["success"] = "The amenity has been deleted successfully";
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
