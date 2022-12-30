using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;
using System.Net.Mail;
using BC = BCrypt.Net.BCrypt;

namespace AplikasiTesOnline
{
    public partial class Form1 : Form
    {
        _utils utilities = new _utils();
        _sql sql = new _sql();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            utilities.IniFile();

            if(!sql.CheckServer())
            {
                MessageBox.Show("Check connection server / File Configuration.ini", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                Disable();
                if (txtEmail.Text == "" || txtPass.Text == "")
                {
                    MessageBox.Show("username / password field id required", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string username = txtEmail.Text;
                string password = txtPass.Text;

                DataTable data = sql.GetData("CALL login('" + username + "')");
                if (data.Rows.Count == 0)
                {
                    MessageBox.Show("User not found", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                foreach (DataRow row in data.Rows)
                {
                    if (!BC.Verify(password, row[1].ToString()))
                    {
                        MessageBox.Show("Password doesn't match", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                MessageBox.Show("Login successfully", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Reset();

            } catch (Exception error)
            {
                throw new Exception(error.Message.ToString());
            } finally
            {
                Enable();
            }
            
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Form form = new FormRegister();
            Hide();
            form.ShowDialog();
            Show();
        }

        private void Enable()
        {
            txtEmail.Enabled = true;
            txtPass.Enabled = true;
            btnLogin.Enabled = true;
            btnRegister.Enabled = true;
            btnView.Enabled = true;
        }

        private void Disable()
        {
            txtEmail.Enabled = false;
            txtPass.Enabled = false;
            btnLogin.Enabled = false;
            btnRegister.Enabled = false;
            btnView.Enabled = false;
        }

        private void Reset()
        {
            txtEmail.Text = "";
            txtPass.Text = "";
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            Form form = new FormView();
            Hide();
            form.ShowDialog();
            Show();
        }
    }
}
