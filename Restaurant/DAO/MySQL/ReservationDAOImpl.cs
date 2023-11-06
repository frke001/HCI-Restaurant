using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAO.MySQL
{
    class ReservationDAOImpl : IReservation
    {
        public List<Reservation> GetAll()
        {
            using (var _db = new restoranContext())
            {
                return _db.Reservations.ToList();
            }
        }
        public Reservation Add(Reservation reservation)
        {
            using (var _db = new restoranContext())
            {
                if (_db.Reservations.Any(el => el.PersonName == reservation.PersonName))
                    return null;
                _db.Reservations.Add(reservation);
                _db.SaveChanges();
            }
            return reservation;
        }
        public Reservation Update(Reservation reservation)
        {
            if (reservation != null)
            {
                using (var _db = new restoranContext())
                {
                    Reservation temp = _db.Reservations.Find(reservation.IdReservation);
                    if (temp != null)
                    {
                        if (_db.Reservations.Any(el => el.PersonName == reservation.PersonName && el.IdReservation != reservation.IdReservation))
                            return null;
                        temp.DateAndTime = reservation.DateAndTime;
                        temp.PersonName = reservation.PersonName;
                        
                        _db.SaveChanges();
                    }
                }
            }
            return reservation;

        }
        public void Delete(Reservation reservation)
        {

            using (var _db = new restoranContext())
            {
                _db.Reservations.Remove(reservation);
                _db.SaveChanges();
            }

        }
    }
}
