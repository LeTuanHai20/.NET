using BUS;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GUI
{
    public partial class fEditAUser : Form
    {
        EditAUserBus bus = new  EditAUserBus();
        Load load = new Load();
        public fEditAUser()
        {
            InitializeComponent();
            LoadRoleName();
        }
        #region placeholder
        private void Password_Enter(object sender, EventArgs e)
        {
            if (this.txtPassword.Text == "Password")
            {
                txtPassword.Text = "";
                txtPassword.ForeColor = Color.Black;
                txtPassword.UseSystemPasswordChar = true;
                if (this.txtPassword.Text == null)
                {
                    txtPassword.UseSystemPasswordChar = false;
                }
            }
        }

        private void Password_Leave(object sender, EventArgs e)
        {
            if (this.txtPassword.Text == "")
            {
                txtPassword.Text = "Password";
                txtPassword.ForeColor = Color.Silver;
                txtPassword.UseSystemPasswordChar = false;
            }
        }

        private void RePassword_Enter(object sender, EventArgs e)
        {
            if (this.txtRePassword.Text == "Re-enter password")
            {
                txtRePassword.Text = "";
                txtRePassword.ForeColor = Color.Black;
                txtRePassword.UseSystemPasswordChar = true;
                if (this.txtRePassword.Text == null)
                {
                    txtRePassword.UseSystemPasswordChar = false;
                }
            }
        }

        private void RePassword_Leave(object sender, EventArgs e)
        {
            if (this.txtRePassword.Text == "")
            {
                txtRePassword.Text = "Re-enter password";
                txtRePassword.ForeColor = Color.Silver;
                txtRePassword.UseSystemPasswordChar = false;
            }
        }
        #endregion
        public void LoadRoleName()
        {      
            cbxRole.DataSource = load.LoadRole();
            cbxRole.DisplayMember = "RoleName";
            cbxRole.ValueMember = "RoleId";
        }
        private void Save_Click(object sender, EventArgs e)
        {
            if(!(string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtLastName.Text) ||
                string.IsNullOrEmpty(txtFirstName.Text) || string.IsNullOrEmpty(txtPassword.Text) ||
                string.IsNullOrEmpty(txtRePassword.Text)))
            {
                string query = null;
                if (!string.IsNullOrEmpty(txtPassword.Text) && (txtPassword.Text != "Password" && txtRePassword.Text != "Re-enter password"))
                {
                    if (txtPassword.Text == txtRePassword.Text && txtPassword.TextLength >= 6 && txtPassword.Text.Any(char.IsUpper) &&
                        txtPassword.Text.Any(char.IsLower) && txtPassword.Text.Any(char.IsDigit)
                        && txtPassword.Text.Any(ch => !Char.IsLetterOrDigit(ch)))
                    {
                        query = string.Format(@"update Users set  Password = '{0}', FirstName = '{1}',
                    LastName = '{2}' , RoleId = '{3}' where Email = '{4}'",
                        txtPassword.Text, txtFirstName.Text, txtLastName.Text, cbxRole.SelectedValue, txtEmail.Text);
                    }
                    else if (txtPassword.Text != txtRePassword.Text)
                    {
                        lblError.Text = "Two password cannot match";
                        lblError.ForeColor = Color.Red;
                        return;
                    }
                    else if (txtPassword.TextLength < 6)
                    {
                        lblError.Text = "The password must at least 6 characters";
                        lblError.ForeColor = Color.Red;
                        return;
                    }
                    else if (!(txtPassword.Text.Any(char.IsUpper) && txtPassword.Text.Any(char.IsLower) && txtPassword.Text.Any(char.IsDigit)
                       && txtPassword.Text.Any(ch => !Char.IsLetterOrDigit(ch))))
                    {
                        lblError.Text = "The password is not strong enough";
                        lblError.ForeColor = Color.Red;
                        return;
                    }
                }
                else
                {
                    query = string.Format(@"update Users set FirstName = '{0}',
                    LastName = '{1}' , RoleId = '{2}' where Email = '{3}'",
                    txtFirstName.Text, txtLastName.Text, cbxRole.SelectedValue, txtEmail.Text);
                }
                bus.ExecuteQueryNotReturn(query);
                fUserManagement listUsers = new fUserManagement();
                this.Hide();
                listUsers.ShowDialog();
            }
            else
            {
                lblError.Text = "All fields are required";
                lblError.ForeColor = Color.Red;
                return;
            }
          
           

        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            fUserManagement fUserManagement = new fUserManagement();
            this.Hide();
            fUserManagement.ShowDialog();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            Cancel_Click(sender, e);
        }

        private void Logout_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("Bạn có muốn thoát chương trình", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
            
        }
    }

}
