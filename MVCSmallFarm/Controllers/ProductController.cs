﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.FileProviders;
using MVCSmallFarm.Models.dbs;
using MVCSmallFarm.Repositories;
using MVCSmallFarm.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using static System.Collections.Specialized.BitVector32;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace MVCSmallFarm.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICategoriesRepository _catrepo;
        private readonly IProductRepository _prdrepo;
        private List<ErrorsMsg> msg = new List<ErrorsMsg>();
        private const string SessionNameImg = "_imgname";
        private const string SessionNameProductCat = "ModelPd";
        public ProductController(ICategoriesRepository catrepo, IProductRepository prdrepo)
        {
            _catrepo = catrepo;
            _prdrepo = prdrepo;

        }

        public async Task<ActionResult> Index()
        {
            ViewBag.ID = 0;
            ViewData["Category"] = new SelectList(await _catrepo.GetAllCategory(), "CategoryId", "CategoryName");  //Intitial DropdownList in Product/PartialAddOrEdit.cshtml
            return View();

        }
        private static ProductCatViewModel IntialValue()
        {
            ProductCatViewModel pc = new ProductCatViewModel();
            return pc;
        }

        [HttpGet]
        public IActionResult ReloadEventViewComponent(int pgview, int flg)
        {
            return ViewComponent("ProductAll", new { pgview = 1, flg = 1, pc = IntialValue() });
        }

        [HttpGet]
        public async Task<ActionResult> ProductAddEditView(int pgview, int flg)
        {
            if (flg != 0)
            {
                var pd = await _prdrepo.GetAllProductById(flg);
                ViewData["Category"] = new SelectList(await _catrepo.GetAllCategory(), "CategoryId", "CategoryName", pd.CategoryId);
                if (pd.ImageUrl != null)
                {
                    HttpContext.Session.SetString("imgurl", pd.ImageUrl);
                }

                return PartialView("ProductAddEditView", pd);
            }
            else
            {
                var pd = IntialValue();
                ViewData["Category"] = new SelectList(await _catrepo.GetAllCategory(), "CategoryId", "CategoryName");  //Intitial DropdownList in Product/PartialAddOrEdit.cshtml
                return PartialView("ProductAddEditView", pd);
            }

        }

        [HttpGet]
        public IActionResult DetailPreview()
        {
            //ViewData["ProductDetail"] = pd;

            var value = HttpContext.Session.GetString("ModelPd");
            ProductCatViewModel bsObj2 = (ProductCatViewModel)JsonConvert.DeserializeObject<ProductCatViewModel>(value);

            ViewData["ProductDetail"] = bsObj2;
            HttpContext.Session.Remove("ModelPd");
            return View();
        }

        public async Task<ActionResult> Detail(int id, string flg, string modal)
        {
            var pd = await _prdrepo.GetAllProductById(id);

            if (pd != null)
            {
                pd.ImageUrl = Url.Content("/wwwroot/img/" + pd.ImageUrl);

                if (pd.ImageUrl == null || pd.ImageUrl.Trim() == "")
                {
                    pd.ImageUrl = DefaultValue.DefaultImg;
                }

                pd.PercentOnePoint = (pd.OnePoint / (float)pd.PointTotals) * 100;
                pd.PercentTwoPoint = (pd.TwoPoint / (float)pd.PointTotals) * 100;
                pd.PercentThreePoint = (pd.ThreePoint / (float)pd.PointTotals) * 100;
                pd.PercentFourPoint = (pd.FourPoint / (float)pd.PointTotals) * 100;
                pd.PercentFivePoint = (pd.FivePoint / (float)pd.PointTotals) * 100;

                if (flg == "nocomment")
                {
                    return PartialView("_Detail", pd);
                }
                else
                {
                    ViewData["ProductDetail"] = pd;
                    if (modal == "nomodal")
                    {
                        //return PartialView("Detail");
                        pd.fileUpload = null;
                        string json = JsonConvert.SerializeObject(pd);
                        HttpContext.Session.SetString("ModelPd", json);
                        return Json(new { success = true, message = "/Product/DetailPreview" });
                        //return RedirectToAction("DetailPreview", pd);
                    }
                    else {
                        return PartialView("_DetailWithComment", pd);
                    }

                }
            }
            else {
                return NotFound();
            }
        }

        //public async Task<JsonResult> ProductAddEditView(ProductCatViewModel pc, IFormFile files)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ProductAddEditView(ProductCatViewModel pc, IFormFile files)
        {
            if (pc.ImageUrl != null)
            {
                HttpContext.Session.SetString("imgurl", pc.ImageUrl);
            }
            if (ModelState.IsValid)
            {

                if (pc.ProductId == 0)
                {

                    msg = await _prdrepo.CreateProduct(pc, files: pc.fileUpload);
                }
                else
                {
                    msg = await _prdrepo.UpdateProduct(pc, files: pc.fileUpload);
                }

                // ViewBag.ID = 0;
                // ModelState.Clear();


                if (msg[0].IsSuccess)
                {
                    return Json(new { success = msg[0].IsSuccess, message = msg[0].Message });
                }
                else
                {
                    return Json(new { success = msg[0].IsSuccess, message = msg[0].Message });

                }
            }
            else
            {
                var imgstr = HttpContext.Session.GetString("imgurl");
                pc.ImageUrl = imgstr;
                HttpContext.Session.Remove("imgurl");

                ViewData["Category"] = new SelectList(await _catrepo.GetAllCategory(), "CategoryId", "CategoryName", pc.CategoryId);

                //return ViewComponent("ProductAll", new { pgview = 3, flg = 0, pc });

                return PartialView("ProductAddEditView", pc);

            }
        }

        [HttpPost]
        public async Task<JsonResult> DeleteProduct(int id)
        {
            if (id != 0)
            {
                msg = await _prdrepo.DeleteProduct(id);
            }

            if (msg[0].IsSuccess)
            {
                return Json(new { success = msg[0].IsSuccess, message = msg[0].Message });
            }
            else
            {
                return Json(new { success = msg[0].IsSuccess, message = msg[0].Message });

            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Comment(ProductWithComment data)
        {
            if (ModelState.IsValid)
            {
                string userip = HttpContext.Connection.RemoteIpAddress.ToString();
                await _prdrepo.CreateComment(data,userip);
                return Redirect("/Home/Index");
            }
            return View(data);
        }
    }
}
