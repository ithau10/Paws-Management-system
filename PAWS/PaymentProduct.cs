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
    public partial class PaymentProduct : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DbConnect dbcon = new DbConnect();
        SqlDataReader dr;
        string title = "PAWS";
        public string uname;
        PaymentForm cash;
        public PaymentProduct(PaymentForm form)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.connection());
            cash = form;
            LoadProduct();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }

        private void submit_Click(object sender, EventArgs e)
        {

            foreach (DataGridViewRow dr in dgvProduct.Rows)
            {
                bool chkbox = Convert.ToBoolean(dr.Cells["Select"].Value);
                if (chkbox)
                {
                    try
                    {
                        cm = new SqlCommand("INSERT INTO tbCash(transno, pcode, pname, qty, price, cashier, CashMadeDate) VALUES (@transno, @pcode, @pname, @qty, @price, @cashier, @CashMadeDate)", cn);
                        cm.Parameters.AddWithValue("@transno", cash.lblTransno.Text);
                        cm.Parameters.AddWithValue("@pcode", dr.Cells[1].Value.ToString());
                        cm.Parameters.AddWithValue("@pname", dr.Cells[2].Value.ToString());
                        cm.Parameters.AddWithValue("@qty", 1);
                        cm.Parameters.AddWithValue("@price", Convert.ToDouble(dr.Cells[5].Value.ToString()));
                        cm.Parameters.AddWithValue("@cashier", uname);
                        cm.Parameters.AddWithValue("@CashMadeDate", DateTime.Now); // Assuming you want to use the current date and time

                        cn.Open();
                        cm.ExecuteNonQuery();
                        cn.Close();


                    }
                    catch (Exception ex)
                    {
                        cn.Close();
                        MessageBox.Show(ex.Message, title);
                    }
                }
            }

            cash.loadCash();
            this.Dispose();

        }


        #region Method

        public void LoadProduct()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            cm = new SqlCommand("SELECT pcode, pname, ptype, pcategory, pprice FROM tbProduct WHERE CONCAT(pname,ptype,pcategory) LIKE '%" + txtSearch.Text + "%' AND pqty > " + 0 + "", cn);
            cn.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvProduct.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString());
            }
            dr.Close();
            cn.Close();
        }
        #endregion Method

    }
}
