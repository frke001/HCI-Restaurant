﻿using Restaurant.Models;
using Restaurant.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAO
{
    internal interface IOrderItem
    {
        List<OrderItem> GetAllByOrderNumber(int orderNumber);
        OrderItem Add(OrderItem item);
        
    }
}
