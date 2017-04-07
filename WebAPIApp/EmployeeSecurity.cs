using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DbDataAccess;

namespace WebAPIApp
{
    public class EmployeeSecurity
    {
        public static bool Login(string username, string password)
        {
            using (sampleDBEntities newentity = new sampleDBEntities())
            {
                return newentity.Users.Any(user => user.Username.Equals(username, StringComparison.OrdinalIgnoreCase)
                   && user.Password == password);
            }
        }
    }
}