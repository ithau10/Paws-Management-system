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
    public partial class PetForm : Form
    {

        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DbConnect dbcon = new DbConnect();
        SqlDataReader dr;
        string title = "PAWS";
        public PetForm()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.connection());
            LoadPet();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            PetModule module = new PetModule(this);
            module.ShowDialog();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadPet();
        }


        private void dgvPet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the clicked cell is in the photo column
            if (e.ColumnIndex == 9 && e.RowIndex >= 0)
            {
                // Get the cell value (photo) from the clicked cell
                var photo = dgvPet.Rows[e.RowIndex].Cells[9].Value;

                // Check if the photo value is not null
                if (photo != null)
                {
                    // Create a new form to display the bigger image
                    Form imageForm = new Form();
                    PictureBox pictureBox = new PictureBox();
                    pictureBox.Image = (Image)photo; // Assuming photo is of type Image
                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBox.Dock = DockStyle.Fill;
                    imageForm.Controls.Add(pictureBox);
                    imageForm.Size = new Size(800, 600); // Set your desired size
                    imageForm.StartPosition = FormStartPosition.CenterParent; // Center the form
                    imageForm.ShowDialog(); // Show the form as a dialog
                }

            }

            string colName = dgvPet.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                PetModule module = new PetModule(this);
                module.lblPcode.Text = dgvPet.Rows[e.RowIndex].Cells[1].Value.ToString();
                module.txtspecies.Text = dgvPet.Rows[e.RowIndex].Cells[2].Value.ToString();
                module.cbCategory.Text = dgvPet.Rows[e.RowIndex].Cells[3].Value.ToString();
                module.vacCB.Text = dgvPet.Rows[e.RowIndex].Cells[4].Value.ToString();
                module.vacDate.Text = dgvPet.Rows[e.RowIndex].Cells[5].Value.ToString();
                module.txtQty.Text = dgvPet.Rows[e.RowIndex].Cells[6].Value.ToString();
                module.cbMode.Text = dgvPet.Rows[e.RowIndex].Cells[7].Value.ToString();
                module.txtPrice.Text = dgvPet.Rows[e.RowIndex].Cells[8].Value.ToString();
                module.pictureBoxPet.Text = dgvPet.Rows[e.RowIndex].Cells[9].Value.ToString();


                module.btnSave.Enabled = false;
                module.btnUpdate.Enabled = true;
                module.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this items?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    dbcon.executeQuery("DELETE FROM tbPet WHERE pcode LIKE '" + dgvPet.Rows[e.RowIndex].Cells[1].Value.ToString() + "'");
                    MessageBox.Show("Item record has been successfully removed!", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            LoadPet();
        }


        #region Method

        public void LoadPet()
        {
            int i = 0;
            dgvPet.Rows.Clear();
            try
            {
                cm = new SqlCommand("SELECT * FROM tbPet WHERE CONCAT(pspecies, pcategory) LIKE @searchText", cn);
                cm.Parameters.AddWithValue("@searchText", "%" + txtSearch.Text + "%");
                cn.Open();
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;

                    // Check if the photo column is DBNull
                    Image image = null;
                    if (dr["photo"] != DBNull.Value)
                    {
                        // Convert the binary data to an Image
                        byte[] imageData = (byte[])dr["photo"];
                        image = ConvertByteArrayToImage(imageData);
                    }

                    // Add the row to the DataGridView
                    dgvPet.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), image);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                // Close the data reader and connection
                if (dr != null && !dr.IsClosed)
                    dr.Close();
                if (cn.State != ConnectionState.Closed)
                    cn.Close();
            }
        }
        private Image ConvertByteArrayToImage(byte[] data)
        {
            if (data == null || data.Length == 0)
            {
                // Return a placeholder image or null if no image is available
                return null;
            }
            using (MemoryStream ms = new MemoryStream(data))
            {
                return Image.FromStream(ms);
            }
        }


        #endregion Method


    }
}