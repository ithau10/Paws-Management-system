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
using Microsoft.Reporting.WinForms;

namespace PAWS
{

    public partial class PaymentForm : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DbConnect dbcon = new DbConnect();
        SqlDataReader dr;
        string title = "PAWS";       
        MainForm main;
        public PaymentForm(MainForm form)
        {

            InitializeComponent();
            cn = new SqlConnection(dbcon.connection());
            main = form;
            getTransno();
            loadCash();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            PaymentPet pet = new PaymentPet(this);
            pet.uname = main.lblUsername.Text;
            pet.ShowDialog();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // Retrieve the price from the database
            decimal price = 0.00m;
            try
            {
                cn.Open();
                cm = new SqlCommand("SELECT price FROM tbPayment WHERE transno = (SELECT MAX(transno) FROM tbPayment)", cn);
                object result = cm.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    price = Convert.ToDecimal(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving price: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }

            // Check if the retrieved price is 0.00
            if (price == 0.00m)
            {
                MessageBox.Show("Cashing out is not allowed. Please consider donating instead.", "Price is 0.00", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit the event handler
            } 

            PaymentCustomer customer = new PaymentCustomer(this);
            customer.ShowDialog();

            // Check if the user wants to proceed with cashing out
            if (MessageBox.Show("Are you sure you want to cash this pet?", "Cashing", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
               

                // Proceed with cashing out process
                getTransno();
                main.loadDailySale();
                for (int i = 0; i < dgvCash.Rows.Count; i++)
                {
                    dbcon.executeQuery("UPDATE tbPet SET pqty= pqty - " + int.Parse(dgvCash.Rows[i].Cells[4].Value.ToString()) + " WHERE pcode LIKE " + dgvCash.Rows[i].Cells[2].Value.ToString() + "");
                    PaymentReceipt payReceipt = new PaymentReceipt();
                    payReceipt.ShowDialog();
                }
                dgvCash.Rows.Clear();
            }
        }


        private void dgvCash_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

            string colName = dgvCash.Columns[e.ColumnIndex].Name;
        removeitem:
            if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this cash?", "Delete Cash", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    dbcon.executeQuery("DELETE FROM tbPayment WHERE cashid LIKE '" + dgvCash.Rows[e.RowIndex].Cells[1].Value.ToString() + "'");
                    MessageBox.Show("Cash record has been successfully removed!", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else if (colName == "Increase")
            {
                int i = checkPqty(dgvCash.Rows[e.RowIndex].Cells[2].Value.ToString());
                if (int.Parse(dgvCash.Rows[e.RowIndex].Cells[4].Value.ToString()) < i)
                {
                    dbcon.executeQuery("UPDATE tbPayment SET qty = qty + " + 1 + " WHERE cashid LIKE '" + dgvCash.Rows[e.RowIndex].Cells[1].Value.ToString() + "'");
                }
                else
                {
                    MessageBox.Show("Remaining quantity on hand is " + i + "!", "Out of Stock ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else if (colName == "Decrease")
            {
                if (int.Parse(dgvCash.Rows[e.RowIndex].Cells[4].Value.ToString()) == 1)
                {
                    colName = "Delete";
                    goto removeitem;
                }
                dbcon.executeQuery("UPDATE tbPayment SET qty = qty - " + 1 + " WHERE cashid LIKE '" + dgvCash.Rows[e.RowIndex].Cells[1].Value.ToString() + "'");
            }
            loadCash();
        }

        #region method
        public void getTransno()
        {
            try
            {
                string sdate = DateTime.Now.ToString("yyyyMMdd");
                int count;
                string transno;

                cn.Open();
                cm = new SqlCommand("SELECT TOP 1 transno FROM tbPayment WHERE transno LIKE '" + sdate + "%' ORDER BY cashid DESC", cn);
                dr = cm.ExecuteReader();
                dr.Read();

                if (dr.HasRows)
                {
                    transno = dr[0].ToString();
                    count = int.Parse(transno.Substring(8, 4));
                    lblTransno.Text = sdate + (count + 1);
                }
                else
                {
                    transno = sdate + "1001";
                    lblTransno.Text = transno;
                }
                dr.Close();
                cn.Close();

            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, title);
            }
        }


        public void loadCash()
        {
            try
            {
                int i = 0;
                double total = 0;
                dgvCash.Rows.Clear();
                cm = new SqlCommand("SELECT cashid,pcode,pspecies,qty,price,total,c.name,cashier FROM tbPayment as cash LEFT JOIN tbCustomer c ON cash.cid = c.id WHERE transno LIKE " + lblTransno.Text + "", cn);
                cn.Open();
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dgvCash.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString());
                    total += double.Parse(dr[5].ToString());
                }
                dr.Close();
                cn.Close();
                lblTotal.Text = total.ToString("#,##0.00");
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, title);
            }
        }

        public int checkPqty(string pcode)
        {
            int i = 0;
            try
            {
                cn.Open();
                cm = new SqlCommand("SELECT pqty FROM tbPet WHERE pcode LIKE '" + pcode + "'", cn);
                i = int.Parse(cm.ExecuteScalar().ToString());
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, title);
            }
            return i;
        }
        #endregion method

        private void guna2Button2_Click(object sender, EventArgs e)
        {// Retrieve the price from the database
            decimal price = 0.00m;
            try
            {
                cn.Open();
                cm = new SqlCommand("SELECT price FROM tbPayment WHERE transno = (SELECT MAX(transno) FROM tbPayment)", cn);
                object result = cm.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    price = Convert.ToDecimal(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving price: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }

            // Check if the retrieved price is greater than 0.00
            if (price > 0.00m)
            {
                MessageBox.Show("Donating is not allowed. Cashing out is required.", "Price is Greater than 0.00", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit the event handler
            }

            PaymentCustomer customer = new PaymentCustomer(this);
            customer.ShowDialog();

            // Check if the user wants to proceed with donation
            if (MessageBox.Show("Are you sure you want to donate this pet?", "Donating", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Proceed with donation process
                getTransno();
                main.loadDailySale();
                for (int i = 0; i < dgvCash.Rows.Count; i++)
                {
                    dbcon.executeQuery("UPDATE tbPet SET pqty= pqty - " + int.Parse(dgvCash.Rows[i].Cells[4].Value.ToString()) + " WHERE pcode LIKE " + dgvCash.Rows[i].Cells[2].Value.ToString() + "");
                    DonationForm donReceipt = new DonationForm();
                    donReceipt.ShowDialog();
                }
                dgvCash.Rows.Clear();
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
    }
