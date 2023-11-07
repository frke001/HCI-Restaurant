using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAO.MySQL
{
    internal class RestaurantDaoImpl :IRestaurant
    {
        public Models.Restaurant GetInfo()
        {
            using( var _db = new restoranContext())
            {
                return _db.Restaurants.ToList().First();
            }
        }
    }
}
