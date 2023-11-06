using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAO
{
    interface IReservation
    {
        List<Reservation> GetAll();
        Reservation Add(Reservation reservation);
        Reservation Update(Reservation reservation);
        void Delete(Reservation reservation);
    }
}
