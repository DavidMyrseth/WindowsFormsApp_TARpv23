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
                    cmd.ExecuteNonQuery();

                    conn.Close();
                    this.toodeTableAdapter.Fill(this.windowsFormsApp_TARpv23DataSet1.Toode);
            }
            else
            {
                MessageBox.Show("Sisesta andmed!");
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            // Проверим, что введен ID товара
            if (Toode_txt.Text.Trim() != string.Empty)
            {
                try
                {
                    // Открытие соединения
                    conn.Open();

                    // Запрос на удаление записи по ID
                    string deleteQuery = "DELETE FROM Toode WHERE ToodeID = @toodeID";
                    cmd = new SqlCommand(deleteQuery, conn);
                    cmd.Parameters.AddWithValue("@toodeID", Toode_txt.Text);

                    // Выполнение запроса
                    cmd.ExecuteNonQuery();

                    // Закрытие соединения
                    conn.Close();

                    // Обновление данных в DataGridView (или другом контроле)
                    this.toodeTableAdapter.Fill(this.windowsFormsApp_TARpv23DataSet1.Toode);

                    MessageBox.Show("Запись успешно удалена.");
                }
                catch (Exception ex)
                {
                    // Обработка ошибок
                    MessageBox.Show("Ошибка при удалении: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Введите ID товара для удаления.");
            }
        }
        private void button2_Click(object sender, EventArgs e)
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
                        MessageBox.Show("Kirje kustutamine nnestus!");
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
                MessageBox.Show("Kustutamiseks dw!");
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }
    }
}
