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
    }
}
