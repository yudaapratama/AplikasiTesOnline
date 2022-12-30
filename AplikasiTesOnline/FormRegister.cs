using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using BC = BCrypt.Net.BCrypt;

namespace AplikasiTesOnline
{
    public partial class FormRegister : Form
    {
        _sql sql = new _sql();

        public FormRegister()
        {
            InitializeComponent();
        }

        private bool ValidEmail(string email)
        {
            string pattern = "^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$";
            Regex validated = new Regex(pattern);
            if (validated.IsMatch(email))
            {
                return true;
            }

            return false;
        }

        private bool ValidPassword(string password)
        {
            string pattern = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{6,}$";
            Regex validated = new Regex(pattern);
            if (validated.IsMatch(password))
            {
                return true;
            }

            return false;
        }

        private bool ValidAge(int age)
        {
            int value = 18;
            if(age < value)
            {
                return false;
            }
            return true;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                Disable();
                
                if (txtEmail.Text == "")
                {
                    MessageBox.Show("email field is required", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (txtPass.Text == "")
                {
                    MessageBox.Show("password field is required", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (txtConfirmPass.Text == "")
                {
                    MessageBox.Show("confirmation password field is required", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (txtAge.Text == "")
                {
                    MessageBox.Show("age field is required", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!ValidEmail(txtEmail.Text))
                {
                    MessageBox.Show("email invalid", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!ValidPassword(txtPass.Text))
                {
                    MessageBox.Show("password invalid", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!ValidAge(Convert.ToInt32(txtAge.Text)))
                {
                    MessageBox.Show("age can't be less than 18", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                if (txtPass.Text != txtConfirmPass.Text)
                {
                    MessageBox.Show("Password and confirm password doesn't match", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string username = txtEmail.Text;
                string password = BC.HashPassword(txtPass.Text);
                int age = Convert.ToInt32(txtAge.Text);

                DataTable data = sql.GetData("CALL get_user_by_email('" + username + "')");
                if(data.Rows.Count > 0)
                {
                    MessageBox.Show("email has already exists in database", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                
                if (sql.ExecuteData("CALL register('" + username + "','" + password + "'," + age + ")"))
                {
                    MessageBox.Show("Register succcessfully", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                    return;
                }
                

                MessageBox.Show("Error while saving to server", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

            } catch (Exception error)
            {
                throw new Exception(error.ToString());
            } finally
            {
                Enable();
            }
            

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Reset()
        {
            txtEmail.Text = "";
            txtPass.Text = "";
            txtConfirmPass.Text = "";
            txtAge.Text = "";
        }

        private void Enable()
        {
            txtEmail.Enabled = true;
            txtAge.Enabled = true;
            txtPass.Enabled = true;
            txtConfirmPass.Enabled = true;
            btnBack.Enabled = true;
            btnRegister.Enabled = true;
        }

        private void Disable()
        {
            txtEmail.Enabled = false;
            txtAge.Enabled = false;
            txtPass.Enabled = false;
            txtConfirmPass.Enabled = false;
            btnBack.Enabled = false;
            btnRegister.Enabled = false;
        }
    }
}
