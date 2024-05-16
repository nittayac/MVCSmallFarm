using System;
using System.Collections.Generic;

namespace MVCSmallFarm.Models.dbs;

public partial class UserWithPoint
{
    public string UserId { get; set; } = null!;

    public int ProductId { get; set; }

    public DateTime PointDate { get; set; }

    public int Point { get; set; }

    public string UserIp { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
