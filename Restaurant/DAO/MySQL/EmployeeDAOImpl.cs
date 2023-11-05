using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAO.MySQL
{
    internal class EmployeeDAOImpl : IEmployee
    {
        public Employee FindByUsernameAndPassword(string username, string password)
        {
            byte[] bytesToEncode = Encoding.UTF8.GetBytes(password);
            string base64String = Convert.ToBase64String(bytesToEncode);
            using (var _db = new restoranContext())
            {
                var employee = _db.Employees.FirstOrDefault(e => e.Username == username && e.Password == base64String);
                return employee;
            }
        }

        public Employee GetById(int id)
        {
            using (var _db = new restoranContext())
            {
                return _db.Employees.Find(id);
            }
        }

    }
}
