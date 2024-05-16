using MVCSmallFarm.ViewModels;
using MVCSmallFarm.Models.dbs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System;

namespace MVCSmallFarm.Repositories
{
    public class ProductRepository(SmallFarmContext db) : IProductRepository
    {
        private readonly SmallFarmContext _db = db;
    

        public async Task<List<ProductCatViewModel>> GetAllProduct()
        {           
            var p = await (from pd in _db.Products
                           from c in _db.Categories
                           where pd.CategoryId == c.CategoryId
                           select new ProductCatViewModel
                           {
                               ProductId = pd.ProductId,
                               ProductName = pd.ProductName,
                               Description = pd.Description,
                               ImageUrl = pd.ImageUrl,
                               CategoryId = c.CategoryId,
                               Cost = pd.Cost,
                               Price = (pd.Price == null ? 0 : (decimal)pd.Price),
                               InStock = (pd.InStock == null ? 0 : (int)pd.InStock),
                               SoldTotals = (pd.SoldTotals == null ? 0 : (int)pd.SoldTotals),
                               ViewTotals = (pd.ViewTotals == null ? 0 : (int)pd.ViewTotals),
                               ShipDateDuration = (pd.ShipDateDuration == null ? 0 : (int)pd.ShipDateDuration),
                               OnePoint = (pd.OnePoint == null ? 0 : (int)pd.OnePoint),
                               TwoPoint = (pd.TwoPoint == null ? 0 : (int)pd.TwoPoint),
                               ThreePoint = (pd.ThreePoint == null ? 0 : (int)pd.ThreePoint),
                               FourPoint = (pd.FourPoint == null ? 0 : (int)pd.FourPoint),
                               FivePoint = (pd.FivePoint == null ? 0 : (int)pd.FivePoint),
                               PointTotals = (pd.PointTotals == null ? 0 : (int)pd.PointTotals),
                               CommentTotals = (pd.CommentTotals == null ? 0 : (int)pd.CommentTotals),
                               AveragePoint = (pd.AveragePoint == null ? 0 : (int)pd.AveragePoint),
                               IsNormal = pd.IsNormal,
                               IsPromotion = pd.IsPromotion,
                               CategoryName = c.CategoryName
                           }).ToListAsync();

            return p;
        }

        public async Task<ProductCatViewModel> GetAllProductById(int? id)
        {


            var p = await(from pd in _db.Products
                          from c in _db.Categories
                          where pd.CategoryId == c.CategoryId
                          && (pd.ProductId == id)
                          select new ProductCatViewModel
                          {
                              ProductId = pd.ProductId,
                              ProductName = pd.ProductName,
                              Description = pd.Description,
                              ImageUrl = pd.ImageUrl,
                              CategoryId = c.CategoryId,
                              Cost = pd.Cost,
                              Price = (pd.Price == null ? 0 : (decimal)pd.Price),
                              InStock = (pd.InStock == null ? 0 : (int)pd.InStock),
                              SoldTotals = (pd.SoldTotals == null ? 0 : (int)pd.SoldTotals),
                              ViewTotals = (pd.ViewTotals == null ? 0 : (int)pd.ViewTotals),
                              ShipDateDuration = (pd.ShipDateDuration == null ? 0 : (int)pd.ShipDateDuration),
                              OnePoint = (pd.OnePoint == null ? 0 : (int)pd.OnePoint),
                              TwoPoint = (pd.TwoPoint == null ? 0 : (int)pd.TwoPoint),
                              ThreePoint = (pd.ThreePoint == null ? 0 : (int)pd.ThreePoint),
                              FourPoint = (pd.FourPoint == null ? 0 : (int)pd.FourPoint),
                              FivePoint = (pd.FivePoint == null ? 0 : (int)pd.FivePoint),
                              PointTotals = (pd.PointTotals == null ? 0 : (int)pd.PointTotals),
                              CommentTotals = (pd.CommentTotals == null ? 0 : (int)pd.CommentTotals),
                              AveragePoint = (pd.AveragePoint == null ? 0 : (int)pd.AveragePoint),
                              IsNormal = pd.IsNormal,
                              IsPromotion = pd.IsPromotion,
                              CategoryName = c.CategoryName
                          }).FirstOrDefaultAsync();

            return p;
        }

        public void CreateProduct(ProductCatViewModel data, IFormFile files)
        {
            if (data != null)
            {
                Product p = new Product();
                p.ProductName = data.ProductName;
                p.Description = data.Description;
                p.CategoryId = data.CategoryId;
                p.InStock = (data.InStock == null ? 0 : (int)data.InStock);
                p.Cost = data.Cost;
                p.Price = data.Price;
                p.IsNormal = data.IsNormal;
                p.IsPromotion = data.IsPromotion;



                if (files != null)
                {
                    if (files.Length > 0) { }
                    var fileName = Path.GetFileName(files.FileName);
                    var fileExt = Path.GetExtension(fileName);

                    var tmpName = Guid.NewGuid().ToString();
                    var newFileName = string.Concat(tmpName, fileExt);

                    var filepath = new PhysicalFileProvider(
                        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img")
                        ).Root + $@"\{newFileName}";

                    using (FileStream fs = File.Create(filepath))
                    {
                        files.CopyTo(fs);
                        fs.Flush();
                    }
                    p.ImageUrl = newFileName.Trim();
                }
                else
                {
                    p.ImageUrl = "";
                }

            
               
                p.SoldTotals = 0;
                p.ViewTotals = 0;
                p.OnePoint = 0;
                p.TwoPoint = 0;
                p.ThreePoint = 0;
                p.FourPoint = 0;
                p.FivePoint = 0;
                p.PointTotals = 0;
                p.CommentTotals = 0;
                p.AveragePoint = 0;
             

                _db.Add(p);
                _db.SaveChanges();
            }
        }

    }
}
