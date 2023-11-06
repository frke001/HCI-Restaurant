using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAO.MySQL
{
    class OrderDaoImpl : IOrder
    {
        public Order Add(Order order)
        {

            using (var _db = new restoranContext())
            {
                _db.Orders.Add(order);
                _db.SaveChanges();
            }
            return order;
        }
    }
}
