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
    public partial class PaymentPet : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DbConnect dbcon = new DbConnect();
        SqlDataReader dr;
        string title = "PAWS";
        public string uname;
        PaymentForm cash;
        public PaymentPet(PaymentForm form)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.connection());
            cash = form;
            LoadPet();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadPet();
        }

        private void submit_Click(object sender, EventArgs e)
        {

            foreach (DataGridViewRow dr in dgvPet.Rows)
            {
                bool chkbox = Convert.ToBoolean(dr.Cells["Select"].Value);
                if (chkbox)
                {
                    try
                    {
                        cm = new SqlCommand("INSERT INTO tbPayment(transno, pcode, pspecies, qty, price, cashier, CashMadeDate) VALUES (@transno, @pcode, @pspecies, @qty, @price, @cashier, @CashMadeDate)", cn);
                        cm.Parameters.AddWithValue("@transno", cash.lblTransno.Text);
                        cm.Parameters.AddWithValue("@pcode", dr.Cells[1].Value.ToString());
                        cm.Parameters.AddWithValue("@pspecies", dr.Cells[2].Value.ToString());
                        cm.Parameters.AddWithValue("@qty", 1);
                        cm.Parameters.AddWithValue("@price", Convert.ToDouble(dr.Cells[4].Value.ToString()));
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

        public void LoadPet()
        {
            int i = 0;
            dgvPet.Rows.Clear();
            cm = new SqlCommand("SELECT pcode, pspecies, pcategory, pprice FROM tbPet WHERE CONCAT(pspecies, pcategory) LIKE '%" + txtSearch.Text + "%' AND pqty > " + 0 + "", cn);
            cn.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvPet.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString());
            }
            dr.Close();
            cn.Close();
        }

        #endregion Method

    }
}
