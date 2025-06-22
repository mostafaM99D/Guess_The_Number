using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Guess_The_Number
{
    public partial class frmMain : Form
    {
        private int _Number = 0;
        private int _RandomNumber = 0;
        private bool _IsWin = false;
        static public int tries = 0;
        public enum enMode { Equal = 0, Greater = 1, Less = 2 }
        private Random random = new Random();


        public frmMain()
        {
            InitializeComponent();
            txtNumber.Select();
            _RandomNumber = random.Next(0, 100);
            lblTrials.Text = "";
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }


        private enMode CheckNumber()
        {
            if (_Number > _RandomNumber) return enMode.Greater;
            if (_Number < _RandomNumber) return enMode.Less;
            return enMode.Equal;
        }

        private void _TryAgain()
        {
            if (_IsWin||tries==0)
                txtNumber.Enabled = false;

            if (MessageBox.Show("Do you want to play again ?", "Question", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Hide();
                frmTrails frm = new frmTrails();
                frm.ShowDialog();
                this.Close();
            }
            else
                Application.Exit();
        }

        private void _ShowTriesMessage()
        {
           lblTrials.Text=$"You have {tries} trie(s) ";
        }

        private void ShowMessageBox(enMode mode)
        {
            switch (mode)
            {
                case enMode.Equal:
                    MessageBox.Show("You Win", "Info", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    _IsWin = true;
                    _TryAgain();
                    break;
                case enMode.Greater:
                    tries--;
                    MessageBox.Show("Your number is greater than my number", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    _ShowTriesMessage();
                    txtNumber.Text = "";
                    break;
                default:
                    tries--;
                    MessageBox.Show("Your number is less than my number", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    _ShowTriesMessage();
                    txtNumber.Text = "";
                    break;
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (txtNumber.Text.Trim().Length == 0){
                MessageBox.Show("The text box is empty", "Announce", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (tries == 0){
                _ShowTriesMessage();
                _TryAgain();
                return;
            }

            _Number = int.Parse(txtNumber.Text.Trim());
            ShowMessageBox(CheckNumber());
        }

        private void txtNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnCheck_Click(sender, e);
        }
    }
}
