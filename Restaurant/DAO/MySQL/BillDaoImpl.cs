using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAO.MySQL
{
    internal class BillDaoImpl : IBill
    {

        public List<Order> GetAll()
        {
            using (var _db = new restoranContext())
            {
                return _db.Orders.ToList();
            }
        }
        public Order Delete(Order order)
        {
            using (var _db = new restoranContext())
            {

                var temp = _db.Orders.Find(order.OrderNumber);
                if (temp != null)
                {
                    temp.IsCanceled = 1;
                    _db.SaveChanges();
                }

            }
            return order;
        }
    }
}
