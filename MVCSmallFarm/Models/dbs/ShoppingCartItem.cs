using System;
using System.Collections.Generic;

namespace MVCSmallFarm.Models.dbs;

public partial class ShoppingCartItem
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int Amount { get; set; }

    public string ShoppingCartId { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual Product Product { get; set; }
}
