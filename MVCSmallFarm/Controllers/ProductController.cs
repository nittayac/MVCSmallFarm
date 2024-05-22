using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;
using MVCSmallFarm.Models.dbs;
using MVCSmallFarm.Repositories;
using MVCSmallFarm.ViewComponents;
using MVCSmallFarm.ViewModels;
using System;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace MVCSmallFarm.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICategoriesRepository _catrepo;
        private readonly IProductRepository _prdrepo;
        private List<ErrorsMsg> msg = new List<ErrorsMsg>();
        public ProductController(ICategoriesRepository catrepo, IProductRepository prdrepo)
        {
            _catrepo = catrepo;
            _prdrepo = prdrepo;

        }

        public ActionResult Index()
        {
            ViewBag.ID = 0;
            //ViewData["Category"] = new SelectList(await _catrepo.GetAllCategory(), "CategoryId", "CategoryName");
            return View(IntialValue());

        }
        private static ProductCatViewModel IntialValue()
        {
            ProductCatViewModel pc = new ProductCatViewModel();
            return pc;
        }

        [HttpGet]
        public IActionResult ReloadEventViewComponent()
        {
            return ViewComponent("ProductAll", new { pgview = 1, flg = 1, pc = IntialValue() });
        }


        [HttpGet]
        public IActionResult ReloadProductAddEditViewComponent(int pgview, int flg)
        {
            ViewBag.ID = flg;
            ProductCatViewModel pc = new ProductCatViewModel();
            return ViewComponent("ProductAll", new { pgview = pgview, flg = flg, pc = IntialValue() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<JsonResult> PartialAddOrEdit(ProductCatViewModel pc, IFormFile files)
        //public async Task<ActionResult> PartialAddOrEdit(ProductCatViewModel pc)
        public async Task<ActionResult> ProductAddEditViewComponent(ProductCatViewModel pc)
        {

            //if (pc.InStock.HasValue && pc.SoldTotals.HasValue)
            //{
            //    if (pc.InStock.Value < pc.SoldTotals.Value)
            //    {
            //        ModelState.AddModelError("", "Weight from most be smaller than Weight to. Please fix this error.");
            //    }
            //}


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
                ViewData["Category"] = new SelectList(await _catrepo.GetAllCategory(), "CategoryId", "CategoryName", pc.CategoryId);
                // return Json(new { success = false, message = "Model invalid" });


                //return RedirectToAction("Index");

                return PartialView(pc);
                //return ViewComponent("ProductAll", new { pgview = 3, flg = 0,pc });

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
    }
}
