using Newtonsoft.Json.Serialization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MVCSmallFarm.Common;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace MVCSmallFarm.ViewModels
{
    public class ProductCatViewModel_1
    {
        [Key]
        public int ProductId { get; set; }


        public string ProductName { get; set; } = null!;

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }


        public int CategoryId { get; set; }


        public decimal? Cost { get; set; }


        public decimal? Price { get; set; }


        public int? InStock { get; set; }


        //[Compare("InStocks", ErrorMessage= "The password and confirmation password do not match.")]
        public int? SoldTotals { get; set; }

        public int? ViewTotals { get; set; }

        public int? ShipDateDuration { get; set; }

        public int? OnePoint { get; set; }

        public int? TwoPoint { get; set; }

        public int? ThreePoint { get; set; }

        public int? FourPoint { get; set; }

        public int? FivePoint { get; set; }

        public int? PointTotals { get; set; }

        public int? CommentTotals { get; set; }

        public double? AveragePoint { get; set; }

        public bool IsNormal { get; set; }

        public bool IsPromotion { get; set; }

        public string? CategoryName { get; set; }

        public IFormFile? fileUpload { get; set; }
    }
}
