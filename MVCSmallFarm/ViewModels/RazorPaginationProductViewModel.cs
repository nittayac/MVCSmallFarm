using X.PagedList;

namespace MVCSmallFarm.ViewModels;

public class RazorPaginationProductViewModel
{
    public int? Page { get; init; }
    public int? CategoryId { get; init; }
    public IPagedList<ProductCatViewModel>? ProductList { get; init; }
}
