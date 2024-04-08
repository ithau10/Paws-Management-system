using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PAWS
{

    public partial class LoginForm : Form
    {
        public int UserId { get; private set; }
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DbConnect dbcon = new DbConnect();
        SqlDataReader dr;
        string title = "PAWS";
        public LoginForm()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.connection());
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Exit Application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string _name = "", _role = "";
                cn.Open();
                cm = new SqlCommand("SELECT id, name, role, password, uphoto FROM tbUser WHERE name=@name", cn);
                cm.Parameters.AddWithValue("@name", txtname.Text);
                dr = cm.ExecuteReader();

                if (dr.Read())
                {
                    int userId = Convert.ToInt32(dr["id"]);
                    string encryptedPassword = dr["password"].ToString();
                    string decryptedPassword = cryptography.Decrypt(encryptedPassword);

                    if (decryptedPassword == txtpass.Text)
                    {
                        _name = dr["name"].ToString();
                        _role = dr["role"].ToString();

                        byte[] imgData = dr["uphoto"] as byte[]; // Retrieve image data
                        Image img;

                        if (imgData == null)
                        {
                            // Create a blank white image as placeholder
                            img = new Bitmap(100, 100);
                            using (Graphics g = Graphics.FromImage(img))
                            {
                                g.Clear(Color.White);
                            }
                        }
                        else
                        {
                            // Convert byte array to Image
                            using (MemoryStream ms = new MemoryStream(imgData))
                            {
                                img = Image.FromStream(ms);
                            }
                        }

                        MainForm main = new MainForm();
                        main.UserId = userId;
                        main.lblUsername.Text = _name;
                        main.lblRole.Text = _role;

                        if (_role == "Cashier")
                            main.btnUser.Visible = false;
                        else
                            main.btnUser.Visible = true;

                        // Resize the image to fit the UserImg control
                        main.UserImg.Image = ResizeImage(img, main.UserImg.Width, main.UserImg.Height);

                        MessageBox.Show("Welcome  " + _name + " |", "ACCESS GRANTED", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Hide();
                        main.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Invalid username and password!", "ACCESS DENIED", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid username and password!", "ACCESS DENIED", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, title);
            }
            finally
            {
                if (dr != null)
                    dr.Close();
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        // Method to resize the image
        private Image ResizeImage(Image img, int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(img, 0, 0, width, height);
            }
            return bmp;
        }



        private void btnForgot_Click(object sender, EventArgs e)
        {

            MessageBox.Show("Please contact your BOSS!", "FORGET PASSWORD", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

       
    }
}
