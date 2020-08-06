using DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
    public partial class fUserManagement : Form
    {
        DataProvider provider = new DataProvider();
        DataTable dataCurrent;
        public fUserManagement()
        {
            InitializeComponent();
            LoadUserList();
            TotalUsers.Text = "Total users: " + ListUser.Rows.Count;
            LoadListSortBy();
            LoadListRoleToFilter();
        }
        #region init data
        void LoadListSortBy()
        {
            List<string> ListToSort = new List<string>()
            {
                "Last Name",
                "First Name"
            };
            SortBy.DataSource = ListToSort;
        }
        void LoadListRoleToFilter()
        {
            List<string> ListRoleToFilter = new List<string>()
            {
                "All Roles",
                "Admin",
                "User"
            };
            filterByRole.DataSource = ListRoleToFilter;
        }
        #endregion
        #region PlaceHolder
        private void SearchText_Enter(object sender, EventArgs e)
        {
            if (this.txtSearch.Text == "Search for...")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }
        private void SearchText_Leave(object sender, EventArgs e)
        {
            if (this.txtSearch.Text == "")
            {
                txtSearch.Text = "Search for...";
                txtSearch.ForeColor = Color.Silver;
            }
        }
        #endregion
        void LoadUserList()
        {
            string query = @"select Users.FirstName, Users.LastName, Users.Email, Role.RoleName
            from Users inner join Role on Users.RoleId = Role.RoleId";
            var data = provider.ExecuteQuery(query);
            dataCurrent = data;
            DataView dv = new DataView(data);
            dv.Sort = "LastName";
            ListUser.DataSource = dv;
        }

        private void AddANewUser(object sender, EventArgs e)
        {
            fAddAUser a = new fAddAUser();
            this.Hide();
            a.Show();
        }
        private void ListUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != ListUser.Columns["Edit"].Index)
            {
                return;
            }
            fEditAUser editAUser = new fEditAUser();
            editAUser.txtEmail.Text = ListUser.Rows[e.RowIndex].Cells["Email"].Value.ToString();
            editAUser.txtFirstName.Text = ListUser.Rows[e.RowIndex].Cells["FirstName"].Value.ToString();
            editAUser.txtLastName.Text = ListUser.Rows[e.RowIndex].Cells["LastName"].Value.ToString();
            editAUser.cbxRole.SelectedItem = ListUser.Rows[e.RowIndex].Cells["RoleName"].Value.ToString();
            this.Hide();
            editAUser.ShowDialog();
        }
        #region Sort
        private void SortByName_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (SortBy.SelectedItem.ToString())
            {
                case "First Name":
                    {
                        SortUsers("FirstName");
                        break;
                    }

                case "Last Name":
                    {
                        SortUsers("LastName");
                        break;
                    }
                default:
                    break;
            }


        }
        private void SortUsers(string value)
        {
            DataView dv = new DataView(dataCurrent);
            dv.Sort = value;
            ListUser.DataSource = dv;
        }
        #endregion
        #region Filter
        private void FilterUser(string value)
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
            var data = provider.ExecuteQuery(query);
            this.dataCurrent = data;
            ListUser.DataSource = data;
        }
        private void filterByRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (filterByRole.SelectedItem.ToString())
            {
                case "Admin":
                    {
                        FilterUser("Admin");
                        break;
                    }
                case "User":
                    {
                        FilterUser("User");
                        break;
                    }
                case "All Roles":
                    {
                        FilterUser("All Roles");
                        break;
                    }
                default:

                    break;
            }
        }
        #endregion
        #region Search
        private void SearchByLastName(string value)
        {
            string query = string.Format(@"select Users.FirstName, Users.LastName, Users.Email, Role.RoleName
            from Users inner join Role on Users.RoleId = Role.RoleId where  LastName like '%{0}%'", value);
            filterByRole.SelectedItem = "All Roles";
            SortBy.SelectedItem = "Last Name";
            ListUser.DataSource = provider.ExecuteQuery(query);
        }
        private void SearchLastName(object sender, EventArgs e)
        {
            if (txtSearch.Text != "Search for...")
            {
                SearchByLastName(txtSearch.Text);
            }

        }

        #endregion

        private void refresh_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "Search for...";
            txtSearch.ForeColor = Color.Silver;
            filterByRole.SelectedItem = "All Roles";
            SortBy.SelectedItem = "Last Name";
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
