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
    public partial class PetModule : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DbConnect dbcon = new DbConnect();
        string title = "PAWS";
        bool check = false;
        PetForm pet;
        public PetModule(PetForm form)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.connection());
            pet = form;
            cbCategory.SelectedIndex = 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {

        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                CheckField();
                if (check)
                {
                    if (MessageBox.Show("Are you sure you want to register this pet?", "Pet Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        // Convert image to byte array
                        byte[] photoBytes = getPhoto();

                        cm = new SqlCommand("INSERT INTO tbPet(pspecies, pcategory, pstatus, pdate, pqty, pmode, pprice, photo) VALUES (@pspecies, @pcategory, @pstatus, @pdate, @pqty, @pmode, @pprice, @photo)", cn);
                        cm.Parameters.AddWithValue("@pspecies", txtspecies.Text);
                        cm.Parameters.AddWithValue("@pcategory", cbCategory.Text);
                        string status = vacCB.Text;
                        DateTime date = vacDate.Value;

                        cm.Parameters.AddWithValue("@pstatus", status);

                        // Check if the status is "Vaccinated"
                        if (status == "Vaccinated")
                        {
                            // If status is "Vaccinated", update @pdate with the selected date
                            cm.Parameters.AddWithValue("@pdate", date);
                        }
                        else
                        {
                            cm.Parameters.AddWithValue("@pdate", DBNull.Value);
                        }
                        cm.Parameters.AddWithValue("@pqty", int.Parse(txtQty.Text));
                        cm.Parameters.AddWithValue("@pmode", cbMode.Text);
                        cm.Parameters.AddWithValue("@pprice", double.Parse(txtPrice.Text));
                        cm.Parameters.AddWithValue("@photo", photoBytes);

                        cn.Open();
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Pet has been successfully registered!", title);
                        Clear();
                        pet.LoadPet();
                    }
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, title);
            }
        }

        private byte[] getPhoto()
        {
            if (pictureBoxPet.Image != null)
            {
                MemoryStream stream = new MemoryStream();
                pictureBoxPet.Image.Save(stream, pictureBoxPet.Image.RawFormat);
                return stream.ToArray();
            }
            else
            {
                return null;
            }
        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg, *.jpeg, *.png, *.gif, *.bmp)|*.jpg; *.jpeg; *.png; *.gif; *.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBoxPet.Image = new Bitmap(openFileDialog.FileName);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                CheckField();
                if (check)
                {
                    if (MessageBox.Show("Are you sure you want to Edit this pet?", "Pet Edited", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cm = new SqlCommand("UPDATE tbPet SET pspecies=@pspecies,  pcategory=@pcategory,pstatus=@pstatus,pdate=@pdate, pqty=@pqty,pmode=@pmode, pprice=@pprice,photo=@photo WHERE pcode=@pcode", cn);
                        cm.Parameters.AddWithValue("@pcode", lblPcode.Text);
                        cm.Parameters.AddWithValue("@pspecies", txtspecies.Text);
                        cm.Parameters.AddWithValue("@pcategory", cbCategory.Text);
                        string status = vacCB.Text;
                        DateTime date = vacDate.Value;

                        cm.Parameters.AddWithValue("@pstatus", status);

                        // Check if the status is "Vaccinated"
                        if (status == "Vaccinated")
                        {
                            // If status is "Vaccinated", update @pdate with the selected date
                            cm.Parameters.AddWithValue("@pdate", date);
                        }
                        else
                        {
                            cm.Parameters.AddWithValue("@pdate", DBNull.Value);
                        }
                        cm.Parameters.AddWithValue("@pqty", int.Parse(txtQty.Text));
                        cm.Parameters.AddWithValue("@pmode", cbMode.Text);
                        cm.Parameters.AddWithValue("@pprice", double.Parse(txtPrice.Text));
                        cm.Parameters.AddWithValue("@photo", getPhoto());


                        cn.Open();
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Pet has been successfully updated!", title);
                        pet.LoadPet();
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





        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }



        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            // only allow digit number 
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            // only allow digit number 
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        #region Method
        public void Clear()
        {
            vacCB.SelectedIndex = 0;
            txtPrice.Clear();
            txtQty.Clear();
            txtspecies.Clear();
            cbCategory.SelectedIndex = 0;

            btnUpdate.Enabled = false;
        }

        public void CheckField()
        {
            if (vacCB.Text == "" | txtPrice.Text == "" | txtQty.Text == "" | txtspecies.Text == "")
            {
                MessageBox.Show("Required data field!", "Warning");
                return;
            }
            check = true;
        }


        #endregion Method

        private void cbMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMode.Text == "Donate")
            {
                // this.Height = 453 - 16;
                lblPrice.Visible = false;
                txtPrice.Visible = false;

            }
            else
            {
                lblPrice.Visible = true;
                txtPrice.Visible = true;
                //this.Height = 490;

            }
        }

        private void PetModule_Load(object sender, EventArgs e)
        {
            //this.Height = 453 - 16;
            label5.Location = new Point(13, 235); // You can adjust the point as necessary
            txtQty.Location = new Point(174, 230);
            label8.Location = new Point(13, 285); // You can adjust the point as necessary
            cbMode.Location = new Point(174, 279);
            lblPrice.Location = new Point(13, 335); // You can adjust the point as necessary
            txtPrice.Location = new Point(174, 328);
            txtPrice.Text = "0";
            txtQty.Text = "1";

        }

        private void vacCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (vacCB.Text == "Not Vaccinated")
            {

                lblVacDate.Visible = false;
                vacDate.Visible = false;
                label5.Location = new Point(13, 235); // You can adjust the point as necessary
                txtQty.Location = new Point(174, 230);
                label8.Location = new Point(13, 285); // You can adjust the point as necessary
                cbMode.Location = new Point(174, 279);
                lblPrice.Location = new Point(13, 335); // You can adjust the point as necessary
                txtPrice.Location = new Point(174, 328);



            }
            else
            {
                lblVacDate.Visible = true;
                vacDate.Visible = true;
                lblVacDate.Location = new Point(13, 235); // Adjust the location as necessary
                vacDate.Location = new Point(174, 230);
                label5.Location = new Point(13, 285); // You can adjust the point as necessary
                txtQty.Location = new Point(174, 279);
                label8.Location = new Point(13, 335); // You can adjust the point as necessary
                cbMode.Location = new Point(174, 328);
                lblPrice.Location = new Point(13, 385); // You can adjust the point as necessary
                txtPrice.Location = new Point(174, 379);




            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

    }
}