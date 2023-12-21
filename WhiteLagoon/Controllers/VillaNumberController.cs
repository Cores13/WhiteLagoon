using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WhiteLagoon.Application.Interfaces;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;
using WhiteLagoon.ViewModels;

namespace WhiteLagoon.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public VillaNumberController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: VillaNumberController
        public IActionResult Index()
        {
            var villaNumbers = _unitOfWork.VillaNumber.GetAll(includeProperties: "Villa");
            return View(villaNumbers);
        }

        // GET: VillaNumberController/Create
        public IActionResult Create()
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(v => new SelectListItem
                {
                    Text = v.Name,
                    Value = v.Id.ToString()
                })
            };

            return View(villaNumberVM);
        }

        // POST: VillaNumberController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(VillaNumberVM request)
        {
            try
            {
                bool roomNumberExists = _unitOfWork.VillaNumber.Any(v => v.Villa_Number == request.VillaNumber.Villa_Number);
                if(roomNumberExists)
                {
                    TempData["error"] = "Villa number already exists";
                    return RedirectToAction(nameof(Index));
                }
                if (ModelState.IsValid)
                {
                    _unitOfWork.VillaNumber.Add(request.VillaNumber);
                    _unitOfWork.VillaNumber.Save();
                    TempData["success"] = "The villa number has been added successfully";
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
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(v => new SelectListItem
                {
                    Text = v.Name,
                    Value = v.Id.ToString()
                }),
                VillaNumber = _unitOfWork.VillaNumber.Get(v => v.Villa_Number == id)
            };
            if (villaNumberVM.VillaNumber is null)
            {
                return BadRequest();
            }
            return View(villaNumberVM);
        }

        // POST: VillaNumberController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(VillaNumberVM request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.VillaNumber.Update(request.VillaNumber);
                    _unitOfWork.VillaNumber.Save();

                    TempData["success"] = "The villa number has been updated successfully";

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
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(v => new SelectListItem
                {
                    Text = v.Name,
                    Value = v.Id.ToString()
                }),
                VillaNumber = _unitOfWork.VillaNumber.Get(v => v.Villa_Number == id)
            };
            if (villaNumberVM.VillaNumber is null)
            {
                return BadRequest();
            }
            return View(villaNumberVM);
        }

        // POST: VillaNumberController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(VillaNumberVM request)
        {
            try
            {
                var villaNumber = _unitOfWork.VillaNumber.Get(v => v.Villa_Number == request.VillaNumber.Villa_Number);
                if (villaNumber is null)
                {
                    return BadRequest();
                }

                _unitOfWork.VillaNumber.Remove(villaNumber);
                _unitOfWork.VillaNumber.Save();

                    TempData["success"] = "The villa number has been deleted successfully";
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
