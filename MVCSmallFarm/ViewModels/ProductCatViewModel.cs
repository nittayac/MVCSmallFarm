using Newtonsoft.Json.Serialization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MVCSmallFarm.Common;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace MVCSmallFarm.ViewModels
{
    public class ProductCatViewModel
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Please enter name a productname"), MaxLength(100)]
        public string ProductName { get; set; } = null!;

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        [Required]
        [Range(1, 1000000, ErrorMessage = "Please choose a category")]
        public int CategoryId { get; set; }


        public decimal? Cost { get; set; }

        [Required]
        [Range(1, 1000000, ErrorMessage = "Please enter a price")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal? Price { get; set; }

        [Required]
        [Display(Name = "InStock")]
        [Range(1, 1000000, ErrorMessage = "Please enter a product in stock")]
        public int? InStock { get; set; }

        [Required(ErrorMessage = "Please enter name a SoldTotals")]
        [Display(Name = "SoldTotals")]
        [CompareNumberLessthan(nameof(InStock),ErrorMessage = "SoldTotals  must be less than or equal to InStock value.")]
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

        public float? PercentOnePoint { get; set; }

        public float? PercentTwoPoint { get; set; }

        public float? PercentThreePoint { get; set; }

        public float? PercentFourPoint { get; set; }

        public float? PercentFivePoint { get; set; }

        public IFormFile? fileUpload { get; set; }
    }
}
