using System;
using System.Collections.Generic;

namespace MVCSmallFarm.Models.dbs;

public partial class ProductWithComment
{
    public int ProductId { get; set; }

    public int RunningNumber { get; set; }

    public string UserId { get; set; }

    public DateTime CommentDate { get; set; }

    public string Comment { get; set; }

    public string UserIp { get; set; }

    public bool IsShow { get; set; }

    public virtual Product Product { get; set; }
}
