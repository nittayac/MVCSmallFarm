using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCSmallFarm.Repositories;
using MVCSmallFarm.Models;
using System.Diagnostics;
using MVCSmallFarm.Models.dbs;
using MVCSmallFarm.ViewModels;
using System.Data;

namespace MVCSmallFarm.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DBConnector _cn;
        private readonly ICategoriesRepository _catrepo;
        private readonly IProductRepository _product;
        public HomeController(ILogger<HomeController> logger, ICategoriesRepository catrepo, IProductRepository product, DBConnector cn)
        {
            _logger = logger;
            _catrepo = catrepo;
            _product = product;
            _cn = cn;
        }

        public async Task<IActionResult> Index()
        {
            // ViewData["CategoryHome"] = new SelectList(await _catrepo.GetAllCategory(), "CategoryId", "CategoryName");  //Intitial DropdownList in Product/PartialAddOrEdit.cshtml
            var items = await _catrepo.GetAllCategory();

            items[0].CategoryName = "All Category";
            ViewData["CategoryHome"] = items;


            List<ProductCatViewModel> pd;
            int categoryid =0;
            if (HttpContext.Session.GetInt32("categoryid") != null)
            {
                categoryid = (int)HttpContext.Session.GetInt32("categoryid");
                HttpContext.Session.Remove("categoryid");
            }          
            if (categoryid == 0)
            {
                pd = await _product.GetAllProduct();
                for (int i = 0; i < pd.Count; i++)
                {
                    pd[i].ImageUrl = $"/img/" + pd[i].ImageUrl;
                }
              
            }
            else {
                pd = ProductList(categoryid);
            }

            // return PartialView("_ProductCataLogView.cshtml", pd);
            return View(pd);
        }

        public IActionResult ClickCategory(int id)
        {
            HttpContext.Session.SetInt32("categoryid", id);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Index(int CategoryId)
        {
           // ViewData["CategoryHome"] = new SelectList(await _catrepo.GetAllCategory(), "CategoryId", "CategoryName", CategoryId);
            List<ProductCatViewModel> pd;
            if (CategoryId == 0)
            {
                pd =  await _product.GetAllProduct();
                for (int i = 0; i < pd.Count; i++)
                {
                    pd[i].ImageUrl = $"/img/" + pd[i].ImageUrl;
                }
            }
            else
            {
                pd = ProductList(CategoryId);
            }
            //return PartialView("_ProductCataLogView", pd);
            //return Json(pd);
            return View(pd);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet("/api/GetProduct")]
        public async Task<IActionResult> GetProduct(int classId) 
        {
            List<ProductCatViewModel> pd;
            if (classId == 0)
            {
                pd = await _product.GetAllProduct();
                for (int i = 0; i < pd.Count; i++)
                {
                    pd[i].ImageUrl = $"/img/" + pd[i].ImageUrl;
                }
            }
            else
            {
                pd = ProductList(classId);
            }
            //return PartialView("_ProductCataLogView", pd);
            //return Json(pd);
            return View(pd);
        }


        private List<ProductCatViewModel> ProductList(int id)
        {
            DataTable dt = new DataTable();
            dt = _cn.ProductList(id);

            List<ProductCatViewModel> pd = new List<ProductCatViewModel>();
            pd = (from DataRow dr in dt.Rows
                  select new ProductCatViewModel()
                  {
                      ProductName = dr["ProductName"].ToString(),
                      ProductId = Convert.ToInt32(dr["ProductId"]),
                      IsPromotion = Convert.ToBoolean(dr["ProductId"]),
                      ImageUrl = $"/img/" + dr["ImageUrl"].ToString(),
                      Price = Convert.ToDecimal(dr["Price"]),
                      CategoryId = Convert.ToInt32(dr["CategoryId"]),
                      CategoryName = dr["CategoryName"].ToString()
                  }).ToList();

            return pd;
        }
    }
}
