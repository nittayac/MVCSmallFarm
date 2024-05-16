using System;
using System.Collections.Generic;

namespace MVCSmallFarm.Models.dbs;

public partial class EcommerceSummary
{
    public int Id { get; set; }

    public int ProductNet { get; set; }

    public int MemberNet { get; set; }

    public int LimitToComment { get; set; }
}
