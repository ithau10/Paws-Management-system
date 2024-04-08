using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace PAWS
{
    public partial class UserModule : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DbConnect dbcon = new DbConnect();
        string title = "PAWS";

        bool check = false;
        UserForm userForm;
        public UserModule(UserForm user)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.connection());
            userForm = user;
            cbRole.SelectedIndex = 1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                CheckField();
                if (check)
                {
                    if (MessageBox.Show("Are you sure you want to register this user?", "User Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string encryptedPassword = cryptography.Encrypt(txtPass.Text); // Encrypt the password

                        cm = new SqlCommand("INSERT INTO tbUser(name,address,phone,role,dob,password)VALUES(@name,@address,@phone,@role,@dob,@password)", cn);
                        cm.Parameters.AddWithValue("@name", txtName.Text);
                        cm.Parameters.AddWithValue("@address", txtAddress.Text);
                        cm.Parameters.AddWithValue("@phone", txtPhone.Text);
                        cm.Parameters.AddWithValue("@role", cbRole.Text);
                        cm.Parameters.AddWithValue("@dob", dtDob.Value);
                        cm.Parameters.AddWithValue("@password", encryptedPassword); // Insert the encrypted password into the database

                        cn.Open();
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("User has been successfully registered!", title);
                        Clear();
                        userForm.LoadUser();
                    }

                }

            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, title);
            }
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                CheckField();
                if (check)
                {
                    if (MessageBox.Show("Are you sure you want to update this record?", "Edit Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        // Encrypt the password
                        string encryptedPassword = cryptography.Encrypt(txtPass.Text);

                        cm = new SqlCommand("UPDATE tbUser SET name=@name, address=@address, phone=@phone, role=@role, dob=@dob, password=@password WHERE id=@id", cn);
                        cm.Parameters.AddWithValue("@id", lbluid.Text);
                        cm.Parameters.AddWithValue("@name", txtName.Text);
                        cm.Parameters.AddWithValue("@address", txtAddress.Text);
                        cm.Parameters.AddWithValue("@phone", txtPhone.Text);
                        cm.Parameters.AddWithValue("@role", cbRole.Text);
                        cm.Parameters.AddWithValue("@dob", dtDob.Value);
                        cm.Parameters.AddWithValue("@password", encryptedPassword); // Insert the encrypted password into the database

                        cn.Open();
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("User's data has been successfully updated!", title);
                        Clear();
                        userForm.LoadUser();
                        this.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, title);
            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbRole.Text == "Employee")
            {
                this.Height = 453 - 26;
                lblPass.Visible = false;
                txtPass.Visible = false;

            }
            else
            {
                lblPass.Visible = true;
                txtPass.Visible = true;
                this.Height = 453;

            }
        }

        #region Method

        public void Clear()
        {
            txtName.Clear();
            txtAddress.Clear();
            txtPhone.Clear();
            txtPass.Clear();
            cbRole.SelectedIndex = 0;
            dtDob.Value = DateTime.Now;

            btnUpdate.Enabled = false;
        }

        //to check field and date of birth
        public void CheckField()
        {
            if (txtName.Text == "" | txtAddress.Text == "")
            {
                MessageBox.Show("Required data field!", "Warning");
                return;
            }

            if (checkAGe(dtDob.Value) < 18)
            {
                MessageBox.Show("User is child worker!. Under 18 year", "Warning");
                return;
            }
            check = true;
        }

        // to Calculate Age for under 18 
        private static int checkAGe(DateTime dateofBirth)
        {
            int age = DateTime.Now.Year - dateofBirth.Year;
            if (DateTime.Now.DayOfYear < dateofBirth.DayOfYear)
                age = age - 1;
            return age;
        }
        #endregion Method

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {

            // Check if the text starts with "0"
            if (txtPhone.Text.StartsWith("0"))
            {
                // If the text starts with "07", enforce a limit of 10 characters
                if (txtPhone.Text.Length > 10)
                {
                    MessageBox.Show("Maximum 10 characters allowed for phone number starting with '07'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPhone.Text = txtPhone.Text.Substring(0, 10);
                    txtPhone.SelectionStart = txtPhone.Text.Length;
                }
            }
            // Check if the text starts with "+"
            else if (txtPhone.Text.StartsWith("+"))
            {
                // If the text starts with "+", enforce a limit of 13 characters
                if (txtPhone.Text.Length > 13)
                {
                    MessageBox.Show("Maximum 13 characters allowed for phone number starting with '+'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPhone.Text = txtPhone.Text.Substring(0, 13);
                    txtPhone.SelectionStart = txtPhone.Text.Length;
                }
            }
            // For any other case, enforce a default limit of 10 characters
            else
            {
                if (txtPhone.Text.Length > 10)
                {
                    MessageBox.Show("Maximum 10 characters allowed for phone number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPhone.Text = txtPhone.Text.Substring(0, 10);
                    txtPhone.SelectionStart = txtPhone.Text.Length;
                }
            }
        }
    }
}
