using BUS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
    public partial class fUserManagement : Form
    {
        
        DataTable dataCurrent;
        UserManagementBus bus = new UserManagementBus();
        Load load = new Load();
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
                "Email",
                "Last Name",
                "First Name",
                "Role Name"
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
            var data = bus.GetData();
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
            string roleName = ListUser.Rows[e.RowIndex].Cells["RoleName"].Value.ToString();
            if (roleName == "Admin")
            {
                editAUser.cbxRole.SelectedValue = 1;
            }
            else if (roleName == "User")
            {
                editAUser.cbxRole.SelectedValue = 2;
            }
            
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
                case "Email":
                    {
                        SortUsers("Email");
                        break;
                    }
                case "Role Name":
                    {
                        SortUsers("RoleName");
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
            var data = bus.Filter(value);
            this.dataCurrent = data;
            ListUser.DataSource = data;
            if(data.Rows.Count == 0)
            {
                DialogResult result = MessageBox.Show("Vai trò này không tìm thấy", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            }
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
            filterByRole.SelectedItem = "All Roles";
            SortBy.SelectedItem = "Last Name";
            ListUser.DataSource = bus.SearchByLastName(value);
            if(bus.SearchByLastName(value).Rows.Count == 0)
            {
                DialogResult result = MessageBox.Show("Nội dung này không tìm thấy", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            }
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


            DialogResult result = MessageBox.Show("Bạn có muốn thoát chương trình", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }


    }
}
