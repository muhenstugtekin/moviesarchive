using Npgsql;
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

namespace MoviesArchive
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        NpgsqlConnection connection = new NpgsqlConnection(@"server=localHost; port=5432; " +
            "Database=moviearchive; user ID=postgres; password=sss");
        internal Uri link;

        void filmler()
        {
            string sql = "Select * from tbl_movie_archive";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        public void Form1_Load(object sender, EventArgs e)
        {
            filmler();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand("insert into tbl_movie_archive  (NAME, CATEGORY, LINK) values (@P1,@P2,@P3)", connection);
            command.Parameters.AddWithValue("@P1", textBox1.Text);
            command.Parameters.AddWithValue("@P2", textBox2.Text);
            command.Parameters.AddWithValue("@P3", textBox3.Text);
            command.ExecuteNonQueryAsync();
            connection.Close();
            MessageBox.Show("add film", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            filmler();
        }

        public void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int selected = dataGridView1.SelectedCells[0].RowIndex;
            string link = dataGridView1.Rows[selected].Cells[3].Value.ToString();

            webBrowser1.Navigate(link);
        }

        public void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bu uygulama cok iyi", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void button2_Click(object sender, EventArgs e)
        {

            int w = Screen.PrimaryScreen.Bounds.Width;
            int h = Screen.PrimaryScreen.Bounds.Height;
            this.Location = new Point(0, 0);
            this.Size = new Size(w, h);

        }
    }
}
