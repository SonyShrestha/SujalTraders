using Microsoft.AspNetCore.Mvc;
using SujalTraders.DataAccess.Data;
using SujalTraders.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SujalTraders.Repository;
using SujalTraders.Repository.Interface;
using SujalTraders.Repository.UnitOfWork;

namespace SujalTraders.Controllers
{
    [Area("Admin")]
    public class ProductCategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductCategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable <ProductCategory> productCategories = _unitOfWork.ProductCategoryRepsitory.GetAll();
            return View(productCategories);
            
        }
        

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            ProductCategory productCategory = new();
            if (id==null || id == 0)
            {
                return View(productCategory);
            }
            
            else
            {
                var productCategoryFromDb = _unitOfWork.ProductCategoryRepsitory.GetFirstOrDefault((int)id);
                if (productCategoryFromDb == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(productCategoryFromDb);
                }
            }          
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                if (productCategory.Id == 0) {
                    _unitOfWork.ProductCategoryRepsitory.Insert(productCategory);
                    _unitOfWork.Save();
                    TempData["success"] = "Product Category inserted successfully!";
                    return RedirectToAction("Index", "ProductCategory");
                }
                else
                {
                    _unitOfWork.ProductCategoryRepsitory.Update(productCategory);
                    _unitOfWork.Save();
                    TempData["success"] = "Product Category updated successfully!";
                    return RedirectToAction("Index", "ProductCategory");
                }
            }
            return View(productCategory);        
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id==null || id == 0)
            {
                return NotFound();
            }
            else
            {
               ProductCategory productCategoryFromDb = _unitOfWork.ProductCategoryRepsitory.GetFirstOrDefault((int)id);
                if (productCategoryFromDb == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(productCategoryFromDb);
                }

            }
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            ProductCategory productCategoryFromDb = _unitOfWork.ProductCategoryRepsitory.GetFirstOrDefault((int)id);
            if (productCategoryFromDb == null)
            {
                return NotFound();
            }
            else
            {
                _unitOfWork.ProductCategoryRepsitory.Delete(id);
                _unitOfWork.Save();
                TempData["success"] = "Product Category deleted successfully!";
                return RedirectToAction("Index", "ProductCategory");

            }
        }

    }
}
