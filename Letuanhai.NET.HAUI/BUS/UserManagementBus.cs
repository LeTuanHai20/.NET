using DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class UserManagementBus
    {
        DataProvider db = new DataProvider();
        public DataTable GetData()
        {
            string query = @"select Users.FirstName, Users.LastName, Users.Email, Role.RoleName
            from Users inner join Role on Users.RoleId = Role.RoleId";
            return db.ExecuteQuery(query);
        }
       
        public DataTable Filter(string value)
        {
            string query = null;
            if (value == "All Roles")
            {
                query = string.Format(@"select Users.FirstName, Users.LastName, Users.Email, Role.RoleName
                from Users inner join Role on Users.RoleId = Role.RoleId");
            }
            else
            {
                query = string.Format(@"select Users.FirstName, Users.LastName, Users.Email, Role.RoleName
            from Users inner join Role on Users.RoleId = Role.RoleId where RoleName = '{0}'", value);
            }
            return  db.ExecuteQuery(query);
        }
        public DataTable SearchByLastName(string value)
        {
            string queryLastName = string.Format(@"select Users.FirstName, Users.LastName, Users.Email, Role.RoleName
            from Users inner join Role on Users.RoleId = Role.RoleId where  LastName like '%{0}%'", value);
            var dataOfLastName =  db.ExecuteQuery(queryLastName);
            if(dataOfLastName.Rows.Count == 0)
            {
                string queryEmail = string.Format(@"select Users.FirstName, Users.LastName, Users.Email, Role.RoleName
            from Users inner join Role on Users.RoleId = Role.RoleId where  Email like '%{0}%'", value);
                var dataOfEmail = db.ExecuteQuery(queryEmail);
                if(dataOfEmail.Rows.Count == 0)
                {
                    string queryFirstName = string.Format(@"select Users.FirstName, Users.LastName, Users.Email, Role.RoleName
            from Users inner join Role on Users.RoleId = Role.RoleId where  FirstName like '%{0}%'", value);
                    var dataOfFirstName = db.ExecuteQuery(queryFirstName);
                    return dataOfFirstName;
                }
                else
                {
                    return dataOfEmail;
                }
            }
            else
            {
                return dataOfLastName;
            }
        }
    }
}
