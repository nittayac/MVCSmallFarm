using Microsoft.AspNetCore.Mvc;
using MVCSmallFarm.Repositories;
using MVCSmallFarm.ViewModels;
using System.Collections.Generic;
using System.Data;

namespace MVCSmallFarm.ViewComponents;

public class ProductCataLogViewComponent : ViewComponent
{
    private readonly IProductRepository _prdrepo;
    private readonly DBConnector _cn;
    public ProductCataLogViewComponent(IProductRepository prdrepo,DBConnector cn)
    {
        _prdrepo = prdrepo;
        _cn = cn;
    }

    public async Task<IViewComponentResult> InvokeAsync(int catid)
    {
        List<ProductCatViewModel> pd;
        if (catid == 0)
        {
            pd = await _prdrepo.GetAllProduct();
        }
        else {
            pd = ProductList(catid);
        }
        return View(pd.ToList());
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
