using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class EditAUserBus
    {
        DataProvider db = new DataProvider();
       
        public void ExecuteQueryNotReturn(string query)
        {
             db.ExecuteQueryNotReturn(query);
        }
    }
}
