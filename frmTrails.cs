using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Guess_The_Number
{
    public partial class frmTrails : Form
    {
        public frmTrails()
        {
            InitializeComponent();
            txtTrails.Select();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmMain frm2 = new frmMain();
            frmMain.tries = int.Parse(txtTrails.Text.Trim());
            frm2.ShowDialog();
            this.Close();
        }

        private void txtTrails_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }


        private void txtTrails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnStart_Click(sender, e);
        }
    }
}
