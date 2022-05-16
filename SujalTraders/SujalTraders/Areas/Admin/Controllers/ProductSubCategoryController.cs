using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SujalTraders.DataAccess.ViewModels;
using SujalTraders.Models.Models;
using SujalTraders.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SujalTraders.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductSubCategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductSubCategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<ProductSubCategory> productSubCategories = _unitOfWork.ProductSubCategoryRepsitory.GetAll();
            return View(productSubCategories);

        }
        
        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            ProductSubCategoryVM productSubCategoryVM = new()
            {
                ProductSubCategory=new(),
                ProductCategoryList= _unitOfWork.ProductCategoryRepsitory.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }) 
            };
            if (id == null || id == 0)
            {
                return View(productSubCategoryVM);
            }

            else
            {
                var productSubCategoryFromDb = _unitOfWork.ProductSubCategoryRepsitory.GetFirstOrDefault((int)id);
                if (productSubCategoryFromDb == null)
                {
                    return NotFound();
                }
                else
                {
                    ProductSubCategoryVM prodSubCategoryVM = new()
                    {
                        ProductSubCategory = productSubCategoryFromDb,
                        ProductCategoryList = _unitOfWork.ProductCategoryRepsitory.GetAll().Select(
                            u => new SelectListItem
                            {
                                Text = u.Name,
                                Value = u.Id.ToString()
                            })
                    };
                    return View(prodSubCategoryVM);
                }
            }
        }


        [HttpPost]
        public IActionResult Upsert(ProductSubCategory productSubCategory)
        {
            if (ModelState.IsValid)
            {
                if (productSubCategory.Id == 0)
                {
                    _unitOfWork.ProductSubCategoryRepsitory.Insert(productSubCategory);
                    _unitOfWork.Save();
                    TempData["success"] = "Product Sub-Category inserted successfully!";
                    return RedirectToAction("Index", "ProductSubCategory");
                }
                else
                {
                    _unitOfWork.ProductSubCategoryRepsitory.Update(productSubCategory);
                    _unitOfWork.Save();
                    TempData["success"] = "Product Category updated successfully!";
                    return RedirectToAction("Index", "ProductSubCategory");
                }
            }
            return View(productSubCategory);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            else
            {
                ProductSubCategory productSubCategoryFromDb = _unitOfWork.ProductSubCategoryRepsitory.GetFirstOrDefault((int)id);
                if (productSubCategoryFromDb == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(productSubCategoryFromDb);
                }

            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            ProductSubCategory productSubCategoryFromDb = _unitOfWork.ProductSubCategoryRepsitory.GetFirstOrDefault((int)id);
            if (productSubCategoryFromDb == null)
            {
                return NotFound();
            }
            else
            {
                _unitOfWork.ProductSubCategoryRepsitory.Delete(id);
                _unitOfWork.Save();
                TempData["success"] = "Product Sub-Category deleted successfully!";
                return RedirectToAction("Index", "ProductSubCategory");
            }
        }
    }
}
