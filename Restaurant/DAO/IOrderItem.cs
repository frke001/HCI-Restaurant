using Restaurant.Models;
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
    }
}
