using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;
using MVCSmallFarm.Models.dbs;
using MVCSmallFarm.Repositories;
using MVCSmallFarm.ViewModels;


namespace MVCSmallFarm.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICategoriesRepository _catrepo;
        private readonly IProductRepository _prdrepo;
        public ProductController(ICategoriesRepository catrepo, IProductRepository prdrepo)
        {
            _catrepo = catrepo;
            _prdrepo = prdrepo;

        }

        public async Task<IActionResult> Index()
        {
            ViewBag.ID = 0;
            ViewData["Category"] = new SelectList(await _catrepo.GetAllCategory(), "CategoryId", "CategoryName");
            return View();
         
        }

        [HttpGet]
        public IActionResult ReloadEventViewComponent()
        {
            return ViewComponent("ProductAll", new { showPrevious = true, showUpcoming = true });
        }


        [HttpGet]
        public IActionResult ReloadProductAddEditViewComponent(bool showPrevious, int showUpcoming)
        {
            ViewBag.ID = showUpcoming;
            return ViewComponent("ProductAddEdit", new { showPrevious = showPrevious, showUpcoming = showUpcoming });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PartialAddOrEdit(ProductCatViewModel pc,IFormFile files)
        {
  
            if (!ModelState.IsValid)
            {
                ViewData["Category"] = new SelectList(await _catrepo.GetAllCategory(), "CategoryId", "CategoryName", pc.CategoryId);
                return View(pc);
            }
            else
            {
                if (pc.ProductId == 0)
                {
                    _prdrepo.CreateProduct(pc, files: pc.fileUpload);
                   
                }
                ViewBag.ID = 0;
                ModelState.Clear();
                //_prdrepo.CreateProduct(pc, files);
            }


            return RedirectToAction("Index");

        }

       
    }
}
