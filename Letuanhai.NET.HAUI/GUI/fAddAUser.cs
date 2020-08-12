using BUS;
using DTO;
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
        Load load = new Load();
        EditAUserBus bus = new EditAUserBus();
        public fAddAUser()
        {
            InitializeComponent();
            LoadRoleName();
        }
        public void LoadRoleName()
        {
            cbxRole.DataSource = load.LoadRole();
            cbxRole.DisplayMember = "RoleName";
            cbxRole.ValueMember = "RoleId";
        }

        #region placeholder
        private void Email_Enter(object sender, EventArgs e)
        {
            if (this.txtEmail.Text == "the.email@address.com")
            {
                txtEmail.Text = "";
                txtEmail.ForeColor = Color.Black;
                fEditAUser E = new fEditAUser();
            }
        }

        private void Email_Leave(object sender, EventArgs e)
        {
            if (this.txtEmail.Text == "")
            {
                txtEmail.Text = "the.email@address.com";
                txtEmail.ForeColor = Color.Silver;
            }
        }

        private void FirstName_Enter(object sender, EventArgs e)
        {
            if (this.txtFirstName.Text == "First name")
            {
                txtFirstName.Text = "";
                txtFirstName.ForeColor = Color.Black;
            }
        }

        private void FirstName_Leave(object sender, EventArgs e)
        {
            if (this.txtFirstName.Text == "")
            {
                txtFirstName.Text = "First name";
                txtFirstName.ForeColor = Color.Silver;
            }
        }

        private void LastName_Enter(object sender, EventArgs e)
        {
            if (this.txtLastName.Text == "Last Name")
            {
                txtLastName.Text = "";
                txtLastName.ForeColor = Color.Black;
            }
        }

        private void LastName_Leave(object sender, EventArgs e)
        {
            if (this.txtLastName.Text == "")
            {
                txtLastName.Text = "Last Name";
                txtLastName.ForeColor = Color.Silver;
            }
        }


        private void Password_Enter(object sender, EventArgs e)
        {
            if (this.txtPassword.Text == "Password")
            {
                txtPassword.Text = "";
                txtPassword.ForeColor = Color.Black;
            }
        }

        private void Password_Leave(object sender, EventArgs e)
        {
            if (this.txtPassword.Text == "")
            {
                txtPassword.Text = "Password";
                txtPassword.ForeColor = Color.Silver;
            }
        }

        private void RePassword_Enter(object sender, EventArgs e)
        {
            if (this.txtRePassword.Text == "Re-enter password")
            {
                txtRePassword.Text = "";
                txtRePassword.ForeColor = Color.Black;
            }
        }

        private void RePassword_Leave(object sender, EventArgs e)
        {
            if (this.txtRePassword.Text == "")
            {
                txtRePassword.Text = "Re-enter password";
                txtRePassword.ForeColor = Color.Silver;
            }
        }
        #endregion

        private void Save_Click(object sender, EventArgs e)
        {
            string query = null;
            if (!string.IsNullOrEmpty(txtPassword.Text) && (txtPassword.Text != "Password" && txtRePassword.Text != "Re-enter password"))
            {
                if (txtPassword.Text == txtRePassword.Text && txtPassword.TextLength >= 6 && txtPassword.Text.Any(char.IsUpper) &&
                    txtPassword.Text.Any(char.IsLower) && txtPassword.Text.Any(char.IsDigit)
                    && txtPassword.Text.Any(ch => !Char.IsLetterOrDigit(ch)))
                {
                    query = string.Format(@"insert into Users values('{0}','{1}','{2}','{3}', '{4}') ",
                  txtEmail.Text,txtPassword.Text, txtFirstName.Text, txtLastName.Text, cbxRole.SelectedValue);
                }
                else if (txtPassword.Text != txtRePassword.Text)
                {
                    txtError.Text = "Two password cannot match";
                    txtError.ForeColor = Color.Red;
                    return;
                }
                else if (txtPassword.TextLength < 6)
                {
                    txtError.Text = "The password must at least 6 characters";
                    txtError.ForeColor = Color.Red;
                    return;
                }
                else if (!(txtPassword.Text.Any(char.IsUpper) && txtPassword.Text.Any(char.IsLower) && txtPassword.Text.Any(char.IsDigit)
                   && txtPassword.Text.Any(ch => !Char.IsLetterOrDigit(ch))))
                {
                    txtError.Text = "The password is not strong enough";
                    txtError.ForeColor = Color.Red;
                }

            }
            else
            {
                query = string.Format(@"insert into Users values('{0}','{1}','{2}','{3}', '{4}') ",
                txtEmail.Text, txtPassword.Text, txtFirstName.Text, txtLastName.Text, cbxRole.SelectedValue);
            }
            bus.ExecuteQueryNotReturn(query);
            fUserManagement listUsers = new fUserManagement();
            this.Hide();
            listUsers.ShowDialog();

        }

        
    }
}
