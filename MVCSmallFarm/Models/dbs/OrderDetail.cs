using System;
using System.Collections.Generic;

namespace MVCSmallFarm.Models.dbs;

public partial class OrderDetail
{
    public string OrderId { get; set; }

    public int ProductId { get; set; }

    public int Amount { get; set; }

    public decimal Price { get; set; }

    public decimal DiscountPerUnit { get; set; }

    public decimal TotalDiscount { get; set; }

    public decimal Total { get; set; }

    public virtual Order Order { get; set; }

    public virtual Product Product { get; set; }
}
