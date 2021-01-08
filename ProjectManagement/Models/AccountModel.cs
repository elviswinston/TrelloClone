using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjectManagement.Models
{
    public class AccountModel
    {
        private DbProjectManagement db = new DbProjectManagement();
        
        public bool Login(string email, string password)
        {
            var res = db.Database.SqlQuery<bool>("SELECT dbo.func_AccountLogin(@email, @password)", 
                new SqlParameter("@email", email),
                new SqlParameter("@password", password)).SingleOrDefault();
            return res;
        }

        public bool Register(string email, string password, string name)
        {
            var res = db.sp_RegisterAccount(email, password, name).SingleOrDefault() ?? false;
            return res;
        }
    }
}