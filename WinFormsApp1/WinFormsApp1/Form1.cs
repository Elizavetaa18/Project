using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=project");

        Dictionary<string, int> degree = new Dictionary<string, int>();
        Dictionary<string, int> position = new Dictionary<string, int>();
        Dictionary<string, int> title = new Dictionary<string, int>();


        public Form1()
        {
            InitializeComponent();
            LoadData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void LoadData()
        {
            DB db = new DB();
            db.openConnection();
         
            string query = "SELECT id_staff AS 'Id', family AS 'Фамилия', name AS 'Имя', surname AS 'Отчество', p.position AS 'Должность', t.academic_title AS 'Ученое звание', ad.small_academic_degree AS 'Ученая степень' " +
                "FROM staff join position p ON staff.id_position = p.id_position " +
                "JOIN degree ad ON staff.id_academic_degree = ad.id_academic_degree " +
                "JOIN title t ON staff.id_academic_title = t.id_academic_title ";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
            
            DataSet table = new DataSet();
            
            adapter.Fill(table);

            dataGridView1.ColumnCount = 7;
            dataGridView1.Columns[0].Name = "Id";
            dataGridView1.Columns[1].Name = "Фамилия";
            dataGridView1.Columns[1].Width = 250;
            dataGridView1.Columns[2].Name = "Имя";
            dataGridView1.Columns[2].Width = 250;
            dataGridView1.Columns[3].Name = "Отчество";
            dataGridView1.Columns[3].Width = 250;
            dataGridView1.Columns[4].Name = "Должность";
            dataGridView1.Columns[4].Width = 260;
            dataGridView1.Columns[5].Name = "Ученое звание";
            dataGridView1.Columns[5].Width = 260;
            dataGridView1.Columns[6].Name = "Ученая степень";
            dataGridView1.Columns[6].Width = 252;

            foreach (DataRow item in table.Tables[0].Rows)
            {
                string[] buf = { item["Id"].ToString(), item["Фамилия"].ToString(), item["Имя"].ToString(), item["Отчество"].ToString(), item["Должность"].ToString(), item["Ученое звание"].ToString(), item["Ученая степень"].ToString() };
                dataGridView1.Rows.Add(buf);
            }


            string query1 = "SELECT small_academic_degree, id_academic_degree FROM degree ORDER BY id_academic_degree";
            MySqlDataAdapter adapter1 = new MySqlDataAdapter(query1, connection);
            DataSet table1 = new DataSet();
            adapter1.Fill(table1);
            foreach (DataRow item in table1.Tables[0].Rows)
            {
                this.degree.Add(item["small_academic_degree"].ToString(), int.Parse(item["id_academic_degree"].ToString()));
            }

            string query2 = "SELECT position , id_position FROM position  ORDER BY id_position ";
            MySqlDataAdapter adapter2 = new MySqlDataAdapter(query2, connection);
            DataSet table2 = new DataSet();
            adapter2.Fill(table2);
            foreach (DataRow item in table2.Tables[0].Rows)
            {
                this.position.Add(item["position"].ToString(), int.Parse(item["id_position"].ToString()));
            }

            string query3 = "SELECT academic_title , id_academic_title FROM title  ORDER BY id_academic_title ";
            MySqlDataAdapter adapter3 = new MySqlDataAdapter(query3, connection);
            DataSet table3 = new DataSet();
            adapter3.Fill(table3);
            foreach (DataRow item in table3.Tables[0].Rows)
            {
                this.title.Add(item["academic_title"].ToString(), int.Parse(item["id_academic_title"].ToString()));
            }

            db.closeConnection();
        }

        

        private void DataGridVie_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.Hide();
            int id = int.Parse(this.dataGridView1.CurrentRow.Cells[0].Value.ToString()); this.Hide();
            Information f2 = new Information(id);
            f2.ShowDialog();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Add f3 = new Add();
            f3.Show();
        }

        private void dataGridView1_SortCompare_1(object sender, DataGridViewSortCompareEventArgs e)
        {
            if (e.Column.Name == "Ученая степень")
            {
                int a = degree[e.CellValue1.ToString()];
                int b = degree[e.CellValue2.ToString()];
                e.SortResult = a.CompareTo(b);
            }
            
            else if(e.Column.Name == "Должность")
            {
                int q = position[e.CellValue1.ToString()];
                int d = position[e.CellValue2.ToString()];
                e.SortResult = d.CompareTo(q);
            }

            else if(e.Column.Name == "Ученое звание")
            {
                int f = title[e.CellValue1.ToString()];
                int g = title[e.CellValue2.ToString()];
                e.SortResult = f.CompareTo(g);
            }
            else
            {
                e.SortResult = System.String.Compare(
                e.CellValue1.ToString(), e.CellValue2.ToString());
            }


            e.Handled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
