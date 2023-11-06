using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAO.MySQL
{ 
    internal class OrderItemDaoImpl : IOrderItem
    {

        public List<OrderItem> GetAllByOrderNumber(int orderNumber)
        {
            using (var _db = new restoranContext())
            {
                var result = _db.OrderItems.Where(el => el.OrderNumber == orderNumber).ToList();
                foreach(var el in result)
                {
                    Article article = _db.Articles.Find(el.IdArticle);
                    if(article != null)
                    {
                        el.IdArticleNavigation = article;
                    }
                }
                return result;
            }
        }
    }
}
