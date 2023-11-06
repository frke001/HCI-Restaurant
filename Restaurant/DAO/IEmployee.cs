using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAO
{
    internal interface IEmployee
    {
        Employee GetById(int id);
        Employee FindByUsernameAndPassword(string username, string password);
        Employee UpdateLanguage(string language, string JMB);
        Employee UpdateTheme(string theme, string JMB);
        Employee UpdateCredentials(string username, string password, string JMB);
        List<Employee> GetAll();
        Employee Add(Employee employee);
        Employee Update(Employee employee);
        void Delete(Employee employee);
        string GetNameAndSurname(string JMB);
    }
}
