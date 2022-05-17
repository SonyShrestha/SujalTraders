using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SujalTraders.DataAccess.ViewModels;
using SujalTraders.Models.Models;
using SujalTraders.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SujalTraders.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public CompanyController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Company> companyUsers = _unitOfWork.CompanyRepository.GetAll();
            return View(companyUsers);

        }


        [HttpGet]
        public IActionResult Upsert(int? id)
        {

            if (id == null || id == 0)
            {
                Company companyUser = new();
                return View(companyUser);
            }

            else
            {
                var companyUsersFromDb = _unitOfWork.CompanyRepository.GetFirstOrDefault((int)id);

                if (companyUsersFromDb == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(companyUsersFromDb);
                }
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company companyUser)
        {
            if (ModelState.IsValid)
            {

                if (companyUser.Id == 0)
                {
                    _unitOfWork.CompanyRepository.Insert(companyUser);
                    _unitOfWork.Save();
                    TempData["success"] = "Company User inserted successfully!";
                    return RedirectToAction("Index", "Company");
                }
                else
                {
                    _unitOfWork.CompanyRepository.Update(companyUser);
                    _unitOfWork.Save();
                    TempData["success"] = "Company User updated successfully!";
                    return RedirectToAction("Index", "Company");
                }
            }
            return View(companyUser);
        }

        public IActionResult GetAll()
        {
            var companyUserList = _unitOfWork.CompanyRepository.GetAll();
            return Json(new { data = companyUserList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            Company companyUserFromDb = _unitOfWork.CompanyRepository.GetFirstOrDefault((int)id);
            if (companyUserFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }


            _unitOfWork.CompanyRepository.Delete(id);
            _unitOfWork.Save();
            TempData["success"] = "Company User deleted successfully!";
            return Json(new { success = true, message = "Delete Successful" });
        }
    }
}
