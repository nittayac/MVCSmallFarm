using MVCSmallFarm.ViewModels;

namespace MVCSmallFarm.Repositories
{
    public interface IProductRepository
    {
        Task<List<ProductCatViewModel?>> GetAllProduct();
        Task<ProductCatViewModel> GetAllProductById(int? id);
        void CreateProduct(ProductCatViewModel data, IFormFile files);
    }
}

