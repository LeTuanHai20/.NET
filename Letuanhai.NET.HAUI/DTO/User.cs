using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class User
    {
        public string Email { get; set; }
        public string Password{ get; set; }
        public string RePassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? RoleId { get; set; }
        public User()
        {

        }
        public User(string Email, string Password, string RePassword , string FirstName, string LastName, int? RoleId)
        {
            this.Email = Email;
            this.Password = Password;
            this.RePassword = RePassword;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.RoleId = RoleId;
        }
    }
}
