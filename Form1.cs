using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WindowsFormsApp_TARpv23
{
    public partial class Form1 : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WindowsFormsApp_TARpv23;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter adapter;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'windowsFormsApp_TARpv23DataSet1.Toode' table. You can move, or remove it, as needed.
            this.toodeTableAdapter.Fill(this.windowsFormsApp_TARpv23DataSet1.Toode);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Nimetus_txt.Text.Trim() != string.Empty && Kogus_txt.Text.Trim() != string.Empty && Hind_txt.Text.Trim()
               != string.Empty)
            {
                    conn.Open();
                    cmd = new SqlCommand("INSERT INTO Toode(Nimetus, Kogus, Hind) VALUES (@toode, @kogus, @hind)", conn);
                    cmd.Parameters.AddWithValue("@toode", Nimetus_txt.Text);
                    cmd.Parameters.AddWithValue("@kogus", Kogus_txt.Text);
                    cmd.Parameters.AddWithValue("@hind", Hind_txt.Text);
                    cmd.Parameters.AddWithValue("@pilt", Nimetus_txt.Text+extension);
                    cmd.ExecuteNonQuery();

                    conn.Close();
                    this.toodeTableAdapter.Fill(this.windowsFormsApp_TARpv23DataSet1.Toode);
            }
            else
            {
                MessageBox.Show("Sisesta andmed!");
            }
        }
        // buttonDelete_Click
        // this.toodeTableAdapter.Fill(this.windowsFormsApp_TARpv23DataSet1.Toode);
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (Nimetus_txt.Text.Trim() != string.Empty && Kogus_txt.Text.Trim() != string.Empty && Hind_txt.Text.Trim() != string.Empty)
            {
                try
                {
                    conn.Open();

                    string query = "DELETE FROM ToodeBaas WHERE Nimetus = @toode AND Kogus = @kogus AND Hind = @hind";
                    cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@toode", Nimetus_txt.Text);
                    cmd.Parameters.AddWithValue("@kogus", Kogus_txt.Text);
                    cmd.Parameters.AddWithValue("@hind", Hind_txt.Text);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Kirje kustutamine 천nnestus!");
                    }
                    else
                    {
                        MessageBox.Show("Kirjet ei leitud.");
                    }

                    conn.Close();

                    this.toodeTableAdapter.Fill(this.windowsFormsApp_TARpv23DataSet1.Toode);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Viga kirje kustutamisel: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Kustutamiseks t채itke k천ik v채ljad!");
            }
        }
        int ID = 0;

        private void dataGridViewl_RolHeaderMouseClick(object sender,
            DataGridViewCellMouseEventArgs e)
        {
            ID = (int)dataGridView1.Rows[e.RowIndex].Cells["Id"].Value;
            Nimetus_txt.Text = dataGridView1.Rows[e.RowIndex + 1].Cells["Nimetus"].Value.ToString();
            Kogus_txt.Text = dataGridView1.Rows[e.RowIndex + 1].Cells["Kogus"].Value.ToString();
            Hind_txt.Text = dataGridView1.Rows[e.RowIndex + 1].Cells["Hind"].Value.ToString();
        }

        private void Nimetus_txt_TextChanged(object sender, EventArgs e)
        {

        }

        OpenFileDialog open;
        SaveFileDialog save;

        private void Pild_txt_Click(object sender, EventArgs e)
        {
            open = new OpenFileDialog();
            open.InitialDirectory = @"C:\Users\opilane\Pictures\";
            open.Multiselect = false;
            open.Filter = "Images Files(*.jpeg;*.png;*.bmp;*.jpg)[*.jpeg;*.png;*.bmp;*.jpg]";
            FileInfo openfile = new FileInfo(@"C:\Users\opilane\Pictures\" + open.FileName);
            if (open.ShowDialog() == DialogResult.OK && Nimetus_txt.Text != null)
            {
                save = new SaveFileDialog();
                save.InitialDirectory = Path.GetFullPath(@"..\..\..\Pildid");
                string extension = Path.GetExtension(open.FileName);
                save.FileName = Nimetus_txt.Text + extension;
                save.Filter = "Images" + Path.GetExtension(open.FileName) + "|" + Path.GetExtension(open.FileName);
                if (save.ShowDialog() == DialogResult.OK && Nimetus_txt != null)
                {
                    File.Copy(open.FileName, save.FileName);
                    pictureBox1.Image = Image.FromFile(save.FileName);
                }

            }
            else
            {
                MessageBox.Show("Puudub toode nimetus vÃµi ole Cancel vajatatud");
            }
        }

        private void Toode_txt_Click(object sender, EventArgs e)
        {

        }

        private void Uuenda(object sender, EventArgs e)
        {
            if (Nimetus_txt.Text.Trim() != string.Empty && Kogus_txt.Text.Trim() != string.Empty && Hind_txt.Text.Trim() !=
                string.Empty)
            {
                try
                {
                    conn.Open();
                    cmd = new SqlCommand("UPDATE Toode SET Nimetus=@toode,,Kogus=@kogus,,Hind=@hind Where Id=@id", conn);
                    cmd.Parameters.AddWithValue("@id", ID);
                    cmd.Parameters.AddWithValue("@toode", Nimetus_txt.Text);
                    cmd.Parameters.AddWithValue("@kogus", Kogus_txt.Text);
                    cmd.Parameters.AddWithValue("@hind", Hind_txt.Text);
                    cmd.ExecuteNonQuery();

                    conn.Close();
                    this.toodeTableAdapter.Fill(this.windowsFormsApp_TARpv23DataSet1.Toode);
                }
                catch (Exception)
                {
                    MessageBox.Show("Andmebaasiga viga!");
                }
            }
            else
            {
                MessageBox.Show("Sisesta andmeid!");
            }
        }
    }
}
