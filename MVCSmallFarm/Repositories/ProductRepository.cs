using MVCSmallFarm.ViewModels;
using MVCSmallFarm.Models.dbs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System;
using System.Linq;
using Microsoft.Extensions.Internal;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.VisualStudio.TextTemplating;

namespace MVCSmallFarm.Repositories
{
    public class ProductRepository(SmallFarmContext db) : IProductRepository
    {
        private readonly SmallFarmContext _db = db;
        private List<ErrorsMsg> msg = new List<ErrorsMsg>();
        

        public async Task<List<ProductCatViewModel>> GetAllProduct()
        {           
            var p = await (from pd in _db.Products
                           from c in _db.Categories
                           where pd.CategoryId == c.CategoryId
                           orderby pd.CreateDate descending,pd.UpdateDate descending
                           select new ProductCatViewModel
                           {
                               ProductId = pd.ProductId,
                               ProductName = pd.ProductName,
                               Description = pd.Description,
                               ImageUrl = string.IsNullOrEmpty(pd.ImageUrl) ? null : pd.ImageUrl,
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

            try
            {
                var p = await (from pd in _db.Products
                               from c in _db.Categories
                               where pd.CategoryId == c.CategoryId
                               && (pd.ProductId == id)
                               select new ProductCatViewModel
                               {
                                   ProductId = pd.ProductId,
                                   ProductName = pd.ProductName,
                                   Description = pd.Description,
                                   ImageUrl = string.IsNullOrEmpty(pd.ImageUrl) ? "" : pd.ImageUrl,
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

                var fileUpload = string.IsNullOrEmpty(p.ImageUrl) ? null : ToIFormFile(p.ImageUrl);

                p.fileUpload = fileUpload;

                return p;
            }
            catch(Exception ex)
            {
                ProductCatViewModel p = new ProductCatViewModel();
                return p;
            }
            //return p;
        }

        public async Task<List<ErrorsMsg>> CreateProduct(ProductCatViewModel data, IFormFile files)
        {
            try
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
                    p.CreateDate = DateTime.Now;
                    p.UpdateDate = null;
                    p.ImageUrl = ImgPath(files);
                    p.SoldTotals = (data.SoldTotals == null ? 0 : data.SoldTotals);
                    p.ViewTotals = (data.ViewTotals == null ? 0 : data.ViewTotals);
                    p.OnePoint = (data.OnePoint == null ? 0 : data.OnePoint);
                    p.TwoPoint = (data.TwoPoint == null ? 0 : data.TwoPoint);
                    p.ThreePoint = (data.ThreePoint == null ? 0 : data.ThreePoint);
                    p.FourPoint = (data.FourPoint == null ? 0 : data.FourPoint);
                    p.FivePoint = (data.FivePoint == null ? 0 : data.FivePoint);
                    p.PointTotals = (data.PointTotals == null ? 0 : data.PointTotals);
                    p.CommentTotals = (data.CommentTotals == null ? 0 : data.CommentTotals);
                    p.AveragePoint = (data.AveragePoint == null ? 0 : data.AveragePoint);


                    _db.Add(p);
                    await _db.SaveChangesAsync();

                    //private ErrorsMsg items = new ErrorsMsg();
                    //items = new ErrorsMsg() { IsSuccess = true, IsError = false, Message = "success" };
                      msg.Add(new ErrorsMsg() { IsSuccess = true, IsError = false, Message = "Add success" });


                }
            }
            catch (Exception ex)
            {
                    msg.Add(new ErrorsMsg() { IsSuccess = false, IsError = true, Message = ex.Message });

            }

            return  msg;
        }

        public async Task<List<ErrorsMsg>> UpdateProduct(ProductCatViewModel data, IFormFile files)
        {
            try
            {
                if (data != null)
                {
                    var p = await (from ppd in _db.Products
                                   where (ppd.ProductId == data.ProductId)
                                   select ppd).FirstOrDefaultAsync();
                    if (p != null)
                    {
                        p.ProductName = data.ProductName;
                        p.Description = data.Description;
                        p.CategoryId = data.CategoryId;
                        p.InStock = (data.InStock == null ? 0 : (int)data.InStock);
                        p.Cost = data.Cost;
                        p.Price = data.Price;
                        p.IsNormal = data.IsNormal;
                        p.IsPromotion = data.IsPromotion;
                        p.UpdateDate = DateTime.Now;
                        p.SoldTotals = (data.SoldTotals == null ? 0 : data.SoldTotals);
                        p.ViewTotals = (data.ViewTotals == null ? 0 : data.ViewTotals);
                        p.OnePoint = (data.OnePoint == null ? 0 : data.OnePoint);
                        p.TwoPoint = (data.TwoPoint == null ? 0 : data.TwoPoint);
                        p.ThreePoint = (data.ThreePoint == null ? 0 : data.ThreePoint);
                        p.FourPoint = (data.FourPoint == null ? 0 : data.FourPoint);
                        p.FivePoint = (data.FivePoint == null ? 0 : data.FivePoint);
                        p.PointTotals = (data.PointTotals == null ? 0 : data.PointTotals);
                        p.CommentTotals = (data.CommentTotals == null ? 0 : data.CommentTotals);
                        p.AveragePoint = (data.AveragePoint == null ? 0 : data.AveragePoint);
                        if (files != null)
                        {
                            p.ImageUrl = ImgPath(files);
                          
                        }


                        _db.Update(p);
                        await _db.SaveChangesAsync();

                        msg.Add(new ErrorsMsg() { IsSuccess = true, IsError = false, Message = "Update success" });

                    }
                }
            }
            catch (Exception ex)
            {
                msg.Add(new ErrorsMsg() { IsSuccess = false, IsError = true, Message = ex.Message });

            }

            return msg; 

        }

        public async Task<List<ErrorsMsg>> DeleteProduct(int id)
        {

            var pd = new Product { ProductId = id };
            try
            {
                if (pd != null)
                {
                    if (!string.IsNullOrEmpty(pd.ImageUrl))
                       {
                            ImgDelete(pd.ImageUrl);
                       }

                    _db.Products.Remove(pd);
                   await  _db.SaveChangesAsync();

                    msg.Add(new ErrorsMsg() { IsSuccess = true, IsError = false, Message = "Delete success" });
                }
            }
            catch (Exception ex)
            {
                msg.Add(new ErrorsMsg() { IsSuccess = false, IsError = true, Message = ex.Message });
            }

            return msg;
        }

        public string ImgPath(IFormFile files) 
        {
            string rs = "";
            if (files != null)
            {
                if (files.Length > 0)
                {
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
                    rs = newFileName.Trim();
                }
            }
            return rs;
        }


        public  bool ImgDelete(string filename) 
        {

            bool chk = false;
            var filepath = new PhysicalFileProvider(
                        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img")
                        ).Root + $@"\{filename}";


            try
            {
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                    chk  = true;
                }
            }
            catch (Exception ex)
            { 
            
            }
            return chk;
        }

        public IFormFile ToIFormFile(string newFileName) {

            var filepath = new PhysicalFileProvider(
                     Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img")
                     ).Root + $@"{newFileName}";

            using FileStream stream = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Read);
            IFormFile formFile = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/*"
            };
            return formFile;
        }

        public async Task CreateComment(ProductWithComment data, string ip)
        {
            var cs =await _db.ProductWithComments
                 .OrderByDescending(i => i.RunningNumber)
                 .Where(p => p.ProductId == data.ProductId)
                 .FirstOrDefaultAsync();

            int currentNo;
            if (cs != null)
            {
                currentNo = cs.RunningNumber;
                currentNo ++;
            }
            else {
                currentNo = 1;
            }

            data.RunningNumber = currentNo;
            data.CommentDate = DateTime.Now;
            data.UserIp = ip;
            data.IsShow = true;
            _db.Add(data);



            var ps = await (from ppd in _db.Products
                           where (ppd.ProductId == data.ProductId)
                           select ppd).FirstOrDefaultAsync();
            var total = ps.CommentTotals;
            ps.CommentTotals = total++; 

            _db.Update(ps);
            await _db.SaveChangesAsync();
        }
    }
}
