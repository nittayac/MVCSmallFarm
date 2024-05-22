using MVCSmallFarm.ViewComponents;
using MVCSmallFarm.ViewModels;

namespace MVCSmallFarm.Repositories
{
    public interface IProductRepository
    {
        Task<List<ProductCatViewModel?>> GetAllProduct();
        Task<ProductCatViewModel> GetAllProductById(int? id);
        Task<List<ErrorsMsg>> CreateProduct(ProductCatViewModel data, IFormFile files);
        Task<List<ErrorsMsg>> UpdateProduct(ProductCatViewModel pc, IFormFile files);
        Task<List<ErrorsMsg>> DeleteProduct(int id);

    }
}

