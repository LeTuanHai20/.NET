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
    public partial class fAdministrator : Form
    {
        public fAdministrator()
        {
            InitializeComponent();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void goToUser_Click(object sender, EventArgs e)
        {
            fUserManagement u = new fUserManagement();
            this.Hide();
            u.ShowDialog();

        }
    }
}
