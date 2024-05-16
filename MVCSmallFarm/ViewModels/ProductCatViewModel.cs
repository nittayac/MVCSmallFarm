using System.ComponentModel.DataAnnotations;

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

        public decimal? Price { get; set; }

        [Range(1, 1000000, ErrorMessage = "Please enter a product in stock")]
        public int? InStock { get; set; }

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
