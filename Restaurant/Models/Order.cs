﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Restaurant.Models;

public partial class Order 
{
    public int OrderNumber { get; set; }

    public DateTime IssueDateAndTime { get; set; }

    public string Jmbemployee { get; set; }

    public sbyte WithCash { get; set; }

    public sbyte IsCanceled { get; set; }

    public virtual Employee JmbemployeeNavigation { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}