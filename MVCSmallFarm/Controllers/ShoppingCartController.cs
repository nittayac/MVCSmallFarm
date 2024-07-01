﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCSmallFarm.Repositories;
using MVCSmallFarm.Services;
using MVCSmallFarm.ViewModels;
using Newtonsoft.Json;

namespace MVCSmallFarm.Controllers
{
    public class ShoppingCart : Controller
    {
        private IProductRepository _productRepo;
        private ShppingCartService _shoppingCart;
        public ShoppingCart(IProductRepository productRepo, ShppingCartService shoppingCart)
        {
            _productRepo = productRepo;
            _shoppingCart = shoppingCart;
        }

        public IActionResult Index()
        {
            var item = _shoppingCart.MyShoppingCart();

            var vm = new ShoppingCartViewModel()
            {
                ShoppingCartSV = _shoppingCart,
                SCTotal = _shoppingCart.Total()
            };
            return View(vm);
        }


        [HttpGet("/api/AddShoppingCart")]
        public async Task<IActionResult> AddItemToShoppingCart(int id) 
        {
            var ps = await _productRepo.GetAllProductById(id);
            if (ps != null)
            {
                _shoppingCart.Add(ps);
            }
            //return RedirectToAction("Index", "ShoppingCart");

            var count = CountItems();
            HttpContext.Session.SetString("cart", count.ToString());  // using this session in _Layout.cshtml  --> Making the amount of items in cart is still showing when clicks the cart
            return Json(count);   //Making the cart shows instantly when clicks an add to cart button.
        }


        public async Task<IActionResult> RemoveItemFromShoppingCart(int id) 
        { 
            var item = await _productRepo.GetAllProductById(id);

            if (item != null)
            {
                _shoppingCart.Remove(item);
            }
            HttpContext.Session.SetString("cart", CountItems().ToString());  // using this session in _Layout.cshtml  --> Making the amount of items in cart is still showing when clicks the arrow down
            return RedirectToAction("Index", "ShoppingCart");
        }

        public async Task<IActionResult> AddItemToShoppingCartItem(int id)
        {
            var ps = await _productRepo.GetAllProductById(id);
            if (ps != null)
            {
                _shoppingCart.Add(ps);
            }
            HttpContext.Session.SetString("cart", CountItems().ToString());  // using this session in _Layout.cshtml  --> Making the amount of items in cart is still showing when clicks the arrow up
            return RedirectToAction("Index", "ShoppingCart");
        }

        private int CountItems()
        {
            int cartitems = 0;
            var item = _shoppingCart.MyShoppingCart();
            for (int i = 0; i < item.Count; i++)
            {
                cartitems = item[i].Amount + cartitems;
            }

            return cartitems;
        }
    }
}
