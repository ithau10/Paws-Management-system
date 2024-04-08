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
    public partial class ReportForm : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DbConnect dbcon = new DbConnect();
        string title = "PAWS";
        public ReportForm()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.connection());
        }
        private void ReportForm_Load(object sender, EventArgs e)
        {


            reportViewer.RefreshReport();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            DateTime fromDate = dtFromDate.Value.Date; // Get the selected fromDate
            DateTime toDate = dtToDate.Value.Date;     // Get the selected toDate

            // Fetch data based on the selected fromDate and toDate
            cm = new SqlCommand("SELECT transno, pcode, pspecies, qty, price, total FROM tbPayment WHERE CAST(CashMadeDate AS DATE) BETWEEN @FromDate AND @ToDate", cn);
            cm.Parameters.AddWithValue("@FromDate", fromDate);
            cm.Parameters.AddWithValue("@ToDate", toDate);

            SqlDataAdapter d = new SqlDataAdapter(cm);
            DataTable dt = new DataTable();
            d.Fill(dt);

            // Clear previous data sources and add new data source with updated parameters
            reportViewer.LocalReport.DataSources.Clear();
            ReportDataSource source = new ReportDataSource("Payment", dt);
            reportViewer.LocalReport.ReportPath = @"C:\Users\ithau.DESKTOP-1P0GCC5\Desktop\DENNIS MUTUNGI\PAWS\PAWS\PaymentReport.rdlc";
            reportViewer.LocalReport.DataSources.Add(source);

            // Set parameters for the report
            ReportParameter[] parameters = new ReportParameter[2];
            parameters[0] = new ReportParameter("fromDate", fromDate.ToString("yyyy-MM-dd"));
            parameters[1] = new ReportParameter("toDate", toDate.ToString("yyyy-MM-dd"));
            reportViewer.LocalReport.SetParameters(parameters);

            // Refresh the report
            reportViewer.RefreshReport();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Dispose();

        }

        private void reportViewer_Load(object sender, EventArgs e)
        {

        }
        private void dtFromDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtFromDate.Value > DateTime.Today)
            {
                dtFromDate.Value = DateTime.Today;
                MessageBox.Show("From date cannot be greater than today's date", "Warning");
                return;
            }

            // Ensure fromDate is not greater than toDate
            if (dtFromDate.Value > dtToDate.Value)
            {
                dtFromDate.Value = dtToDate.Value;
                MessageBox.Show("From date cannot be greater than to date", "Warning");
                return;
            }
        }

        private void dtToDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtToDate.Value > DateTime.Today)
            {
                dtToDate.Value = DateTime.Today;
                MessageBox.Show("To date cannot be greater than today's date", "Warning");
                return;
            }

            // Ensure fromDate is not greater than toDate
            if (dtFromDate.Value > dtToDate.Value)
            {
                dtToDate.Value = dtFromDate.Value;
                MessageBox.Show("To date cannot be less than from date", "Warning");
                return;
            }
        }




    }
}


