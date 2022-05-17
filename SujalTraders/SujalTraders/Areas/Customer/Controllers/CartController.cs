using Microsoft.AspNetCore.Authorization;
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
using System.Security.Claims;
using System.Threading.Tasks;


namespace SujalTraders.Areas.Admin.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartVM ShoppingCartVM { get; set; }
        public double cartTotal { get; set; }

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var applicationUserId = claim.Value;

            ShoppingCartVM = new()
            {
                cartList = _unitOfWork.ShoppingCartRepository.GetAll(
                c => c.ApplicationUserId == applicationUserId,
                "Product")
            };
            foreach(var item in ShoppingCartVM.cartList)
            {
                ShoppingCartVM.cartTotal +=(item.Count * item.Product.UnitPrice);
            }
            return View(ShoppingCartVM);
        }
        public IActionResult plus(int routeId)
        {
            ShoppingCart cartInDb = _unitOfWork.ShoppingCartRepository.GetByID(routeId);
            _unitOfWork.ShoppingCartRepository.IncrementCount(cartInDb, 1);
            _unitOfWork.Save();
            return RedirectToAction("Index");

        }
        public IActionResult minus(int routeId)
        {
            ShoppingCart cartInDb = _unitOfWork.ShoppingCartRepository.GetByID(routeId);
            if (cartInDb.Count == 1)
            {
                _unitOfWork.ShoppingCartRepository.Delete(routeId);
            }
            else
            {
                _unitOfWork.ShoppingCartRepository.DecrementCount(cartInDb, 1);
            }
            _unitOfWork.Save();
            return RedirectToAction("Index");

        }
        public IActionResult remove(int routeId)
        {
            _unitOfWork.ShoppingCartRepository.Delete(routeId);
            _unitOfWork.Save();
            return RedirectToAction("Index");

        }
    }

}
