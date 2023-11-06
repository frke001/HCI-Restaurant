using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAO
{
    internal interface IBill
    {
        List<Order> GetAll();
        Order Delete(Order order);
    }
}
