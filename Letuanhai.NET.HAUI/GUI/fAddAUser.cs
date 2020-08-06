using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class fAddAUser : Form
    {
        public fAddAUser()
        {
            InitializeComponent();
            LoadRoleName();
        }
        public void LoadRoleName()
        {
            List<string> roleName = new List<string>()
            {
                "Admin",
                "User"
            };
            cbxRole.DataSource = roleName;
        }

        #region placeholder
        private void Email_Enter(object sender, EventArgs e)
        {
            if (this.Email.Text == "the.email@address.com")
            {
                Email.Text = "";
                Email.ForeColor = Color.Black;
                fEditAUser E = new fEditAUser();
            }
        }

        private void Email_Leave(object sender, EventArgs e)
        {
            if (this.Email.Text == "")
            {
                Email.Text = "the.email@address.com";
                Email.ForeColor = Color.Silver;
            }
        }

        private void FirstName_Enter(object sender, EventArgs e)
        {
            if (this.FirstName.Text == "First name")
            {
                FirstName.Text = "";
                FirstName.ForeColor = Color.Black;
            }
        }

        private void FirstName_Leave(object sender, EventArgs e)
        {
            if (this.FirstName.Text == "")
            {
                FirstName.Text = "First name";
                FirstName.ForeColor = Color.Silver;
            }
        }

        private void LastName_Enter(object sender, EventArgs e)
        {
            if (this.LastName.Text == "Last Name")
            {
                LastName.Text = "";
                LastName.ForeColor = Color.Black;
            }
        }

        private void LastName_Leave(object sender, EventArgs e)
        {
            if (this.LastName.Text == "")
            {
                LastName.Text = "Last Name";
                LastName.ForeColor = Color.Silver;
            }
        }


        private void Password_Enter(object sender, EventArgs e)
        {
            if (this.Password.Text == "Password")
            {
                Password.Text = "";
                Password.ForeColor = Color.Black;
            }
        }

        private void Password_Leave(object sender, EventArgs e)
        {
            if (this.Password.Text == "")
            {
                Password.Text = "Password";
                Password.ForeColor = Color.Silver;
            }
        }

        private void RePassword_Enter(object sender, EventArgs e)
        {
            if (this.RePassword.Text == "Re-enter password")
            {
                RePassword.Text = "";
                RePassword.ForeColor = Color.Black;
            }
        }

        private void RePassword_Leave(object sender, EventArgs e)
        {
            if (this.RePassword.Text == "")
            {
                RePassword.Text = "Re-enter password";
                RePassword.ForeColor = Color.Silver;
            }
        }
        #endregion

    }
}
