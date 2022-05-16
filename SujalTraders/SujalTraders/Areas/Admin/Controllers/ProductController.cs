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
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = _unitOfWork.ProductRepository.GetAll();
            return View(products);

        }


        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            
            if (id == null || id == 0)
            {
                ProductVM productVM = new()
                {
                    Product = new(),
                    ProductSubCategoryList = _unitOfWork.ProductSubCategoryRepsitory.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
                };
                return View(productVM);
            }

            else
            {
                var productFromDb = _unitOfWork.ProductRepository.GetFirstOrDefault((int)id);
                ProductVM productFromDbVM = new()
                {
                    Product=productFromDb,
                    ProductSubCategoryList = _unitOfWork.ProductSubCategoryRepsitory.GetAll().Select(
                    u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString()
                    })
                };
                if (productFromDb == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(productFromDbVM);
                }
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM productVM,IFormFile formFile)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (formFile != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\products");
                    var extension = Path.GetExtension(formFile.FileName);

                    if (productVM.Product.ImageURL != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageURL.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using(var fileStreams=new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        formFile.CopyTo(fileStreams);
                    }
                    productVM.Product.ImageURL = @"\images\products\" + fileName + extension;
                }
                if (productVM.Product.Id == 0)
                {
                    _unitOfWork.ProductRepository.Insert(productVM.Product);
                    _unitOfWork.Save();
                    TempData["success"] = "Product inserted successfully!";
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    _unitOfWork.ProductRepository.Update(productVM.Product);
                    _unitOfWork.Save();
                    TempData["success"] = "Product updated successfully!";
                    return RedirectToAction("Index", "Product");
                }
            }
            return View(productVM);
        }

        public IActionResult GetAll()
        {
            var ProductList = _unitOfWork.ProductRepository.GetAll();
            return Json(new { data = ProductList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            Product productFromDb = _unitOfWork.ProductRepository.GetFirstOrDefault((int)id);
            if (productFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, productFromDb.ImageURL.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitOfWork.ProductRepository.Delete(id);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted successfully!";
            return Json(new { success = true, message = "Delete Successful" });
        }
    }
}
