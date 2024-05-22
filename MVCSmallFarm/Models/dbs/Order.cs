using System;
using System.Collections.Generic;

namespace MVCSmallFarm.Models.dbs;

public partial class Order
{
    public string OrderId { get; set; }

    public string UserId { get; set; }

    public string Email { get; set; }

    public string OrderNo { get; set; }

    public DateTime OrderDate { get; set; }

    public DateTime? ReceiveDate { get; set; }

    public DateTime? PaidDate { get; set; }

    public double? NetDc { get; set; }

    public double? Vatrate { get; set; }

    public double? NetVat { get; set; }

    public double? Net { get; set; }

    public bool IsReceived { get; set; }

    public bool IsPaid { get; set; }

    public bool IsNormal { get; set; }

    public string UserIp { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
