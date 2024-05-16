using System;
using System.Collections.Generic;

namespace MVCSmallFarm.Models.dbs;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public int CategoryId { get; set; }

    public decimal? Cost { get; set; }

    public decimal? Price { get; set; }

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

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<ProductWithComment> ProductWithComments { get; set; } = new List<ProductWithComment>();

    public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; } = new List<ShoppingCartItem>();

    public virtual ICollection<UserWithPoint> UserWithPoints { get; set; } = new List<UserWithPoint>();
}
