﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Restaurant.Models;

public partial class OrderItem
{
    public int Quantity { get; set; }

    public int IdItem { get; set; }

    public int IdArticle { get; set; }

    public int OrderNumber { get; set; }

    public decimal Price { get; set; }

    public virtual Article IdArticleNavigation { get; set; }

    public virtual Order OrderNumberNavigation { get; set; }
}