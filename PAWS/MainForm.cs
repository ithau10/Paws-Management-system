using System;
using System.IO;
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
    public partial class MainForm : Form
    {
        public int UserId { get; set; }
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DbConnect dbcon = new DbConnect();

        public MainForm()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.connection());
            btnDashboard.PerformClick();
            loadDailySale();  
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Exit Application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            openChildForm(new Dashboard());
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            openChildForm(new CustomerForm());

        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            openChildForm(new UserForm());

        }

        private void btnPet_Click(object sender, EventArgs e)
        {
            openChildForm(new PetForm());

        }

        private void btnCash_Click(object sender, EventArgs e)
        {
            openChildForm(new PaymentForm(this));

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Logout Application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                LoginForm login = new LoginForm();
                this.Dispose();
                login.ShowDialog();
            }

        }
       
        
        #region Method
        private Form activeForm = null;
        public void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            lblTitle.Text = childForm.Text;
            panelChild.Controls.Add(childForm);
            panelChild.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

       





        #endregion Method

        

        private void btnReport_Click(object sender, EventArgs e)
        {

            ReportForm module = new ReportForm();
            module.ShowDialog();

        }

       
        

        #region Method
       
        

        public void loadDailySale()
        {
            string sdate = DateTime.Now.ToString("yyyyMMdd");

            try
            {
                cn.Open();
                cm = new SqlCommand("SELECT ISNULL(SUM(total),0) AS total FROM tbPayment WHERE transno LIKE'" + sdate + "%'", cn);
                lblDailySale.Text = double.Parse(cm.ExecuteScalar().ToString()).ToString("#,##0.00");
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);
            }
        }



        #endregion Method
        private void UserImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg, *.jpeg, *.png, *.gif, *.bmp)|*.jpg; *.jpeg; *.png; *.gif; *.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Load the selected image file
                    Image newImage = Image.FromFile(openFileDialog.FileName);

                    // Update the image in the database for the specific user ID
                    UpdateUserImage(UserId, newImage);

                    // Display the updated image
                    UserImg.Image = newImage;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }





        private void UpdateUserImage(int userId, Image image)
        {
            try
            {

                cm = new SqlCommand("UPDATE tbUser SET uphoto = @photo WHERE id = @userId",cn);
                    
                    {
                        // Convert the Image to a byte array to store it in the database
                        MemoryStream ms = new MemoryStream();
                        image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg); // Change the format as per your requirement
                        byte[] imageData = ms.ToArray();

                        cm.Parameters.AddWithValue("@photo", imageData);
                        cm.Parameters.AddWithValue("@userId", userId);

                        cn.Open();
                        cm.ExecuteNonQuery();
                    }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to update image: " + ex.Message);
            }
        }


    }
}


