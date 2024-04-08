using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PAWS
{
    public partial class CustomerModule : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DbConnect dbcon = new DbConnect();
        string title = "PAWS";

        bool check = false;
        CustomerForm customer;
        public CustomerModule(CustomerForm form)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.connection());
            customer = form;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                CheckField();
                if (check)
                {
                    if (MessageBox.Show("Are you sure you want to register this customer?", "Customer Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cm = new SqlCommand("INSERT INTO tbCustomer(name,address,phone)VALUES(@name,@address,@phone)", cn);
                        cm.Parameters.AddWithValue("@name", txtName.Text);
                        cm.Parameters.AddWithValue("@address", txtAddress.Text);
                        cm.Parameters.AddWithValue("@phone", txtPhone.Text);

                        cn.Open();
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Customer has been successfully registered!", title);
                        Clear();
                        customer.LoadCustomer();
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
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }
        #region method
        public void CheckField()
        {
            if (txtName.Text == "" | txtAddress.Text == "" | txtPhone.Text == "")
            {
                MessageBox.Show("Required data field!", "Warning");
                return;
            }

            check = true;
        }

        public void Clear()
        {
            txtName.Clear();
            txtAddress.Clear();
            txtPhone.Clear();

            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }
        #endregion method

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {


            try
            {
                CheckField();
                if (check)
                {
                    if (MessageBox.Show("Are you sure you want to Edit this record?", "Record Edit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cm = new SqlCommand("UPDATE tbCustomer SET name=@name, address=@address, phone=@phone WHERE id=@id", cn);
                        cm.Parameters.AddWithValue("@id", lblcid.Text);
                        cm.Parameters.AddWithValue("@name", txtName.Text);
                        cm.Parameters.AddWithValue("@address", txtAddress.Text);
                        cm.Parameters.AddWithValue("@phone", txtPhone.Text);

                        cn.Open();
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Customer data has been successfully updated!", title);
                        Clear();
                        customer.LoadCustomer();
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

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
