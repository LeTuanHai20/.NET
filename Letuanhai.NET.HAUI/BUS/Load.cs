using DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class Load
    {
        DataProvider db = new DataProvider();
        public DataTable LoadRole()
        {
            string query = "select * from Role";
            return db.ExecuteQuery(query);
        }

    }
}
