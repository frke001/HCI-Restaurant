using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAO.MySQL
{
    internal class EmployeeDAOImpl : IEmployee
    {
        public Employee FindByUsernameAndPassword(string username, string password)
        {
            string hashedPassword = HashPassword(password);
            using (var _db = new restoranContext())
            {
                var employee = _db.Employees.FirstOrDefault(e => e.Username == username && e.Password == hashedPassword);
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
        public Employee UpdateLanguage(string language, string JMB)
        {
            using (var _db = new restoranContext())
            {
                Employee temp = _db.Employees.Find(JMB);
                if (temp != null)
                {
                    temp.Language = language;
                    _db.SaveChanges();
                    return temp;
                }
                else
                    return null;
            }
        }
        public Employee UpdateTheme(string theme, string JMB)
        {
            using (var _db = new restoranContext())
            {
                Employee temp = _db.Employees.Find(JMB);
                if (temp != null)
                {
                    temp.Theme = theme;
                    _db.SaveChanges();
                    return temp;
                }
                else
                    return null;
            }
        }
        public Employee UpdateCredentials(string username, string password,string JMB)
        {
            using (var _db = new restoranContext())
            {
                Employee temp = _db.Employees.Find(JMB);
                if (temp != null)
                {
                    temp.Username = username;
                    string hashedPassword = HashPassword(password);
                    temp.Password = hashedPassword;
                    _db.SaveChanges();
                    return temp;
                }
                else
                    return null;
            }
        }
        public List<Employee> GetAll()
        {
            using (var _db = new restoranContext())
            {
                return _db.Employees.ToList();
            }
        }
        public Employee Add(Employee employee)
        {
            using (var _db = new restoranContext())
            {
                if (_db.Employees.Any(el => el.Jmb == employee.Jmb))
                    return null;
                _db.Employees.Add(employee);
                _db.SaveChanges();
            }
            return employee;
        }
        public Employee Update(Employee employee)
        {
            if (employee != null)
            {
                using (var _db = new restoranContext())
                {

                    Employee existingEmployee = _db.Employees.Find(employee.Jmb);

                    if (existingEmployee != null)
                    {

                        existingEmployee.Name = employee.Name;
                        existingEmployee.Prezime = employee.Prezime;
                        existingEmployee.Salary = employee.Salary;
                        existingEmployee.Telephone = employee.Telephone;
                        existingEmployee.Address = employee.Address;
                        existingEmployee.IsManager = employee.IsManager;
                        _db.SaveChanges();
                    }
                }
            }

            return employee;
        }
        public void Delete(Employee employee)
        {
            using (var _db = new restoranContext())
            {

                _db.Employees.Remove(employee);
                _db.SaveChanges();
            }
        }
        public string GetNameAndSurname(string JMB)
        {
            using (var _db = new restoranContext())
            {
                Employee temp = _db.Employees.Find(JMB);
                if (temp != null)
                {
                    return temp.Name + " " + temp.Prezime;
                }
                else

                    return "";
            }
        }
        private static string HashPassword(string password)
        {
            using (var sha512 = SHA512.Create())
            {
                var hashedBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(password));

                var hashedString = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

                return hashedString;
            }
        }

      
    }
}
