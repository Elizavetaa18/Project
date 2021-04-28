using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace WinFormsApp1
{
    public partial class add_di : Form
    {
        MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=project");
        int id;

        public add_di(int id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void add_di_Load(object sender, EventArgs e)
        {
            add_di_LoadDate();       
            
        }
        private void add_di_LoadDate()
        {
            DB db = new DB();
            db.openConnection();
            

            string query = String.Format("SELECT staff_discipline.id_discipline  AS 'Id', name_discipline AS 'Дисциплина', " +
                "staff_discipline.id_staff as 'Связь'" +
                "FROM discipline join staff_discipline on discipline.id_discipline = staff_discipline.id_discipline " +
                "WHERE staff_discipline.id_staff={0} ", id);

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);

            DataSet table = new DataSet();

            adapter.Fill(table);

            Dictionary<string, int> disStaf = new Dictionary<string, int>();
            foreach (DataRow item in table.Tables[0].Rows)
            {
                disStaf.Add(item["Дисциплина"].ToString(), int.Parse(item["Id"].ToString()));
            }

            query = "SELECT id_discipline , name_discipline FROM discipline ";

            adapter = new MySqlDataAdapter(query, connection);

            table = new DataSet();

            adapter.Fill(table);

            Dictionary<string, int> dis = new Dictionary<string, int>();
            foreach (DataRow item in table.Tables[0].Rows)
            {
                dis.Add(item["name_discipline"].ToString(), int.Parse(item["id_discipline"].ToString()));
            }

            dataGridView1.ColumnCount = 3;
            dataGridView1.Columns[0].Name = "Id";
            dataGridView1.Columns[1].Name = "Дисциплина";
            dataGridView1.Columns[2].Name = "Связь";


            dataGridView1.Rows.Clear();
            foreach (var item in dis.Keys)
            {
                    string [] buf = {dis[item].ToString(), item, disStaf.ContainsKey(item) ? "Привязано":"Непривязано"};
                    dataGridView1.Rows.Add(buf);
            }
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var idx = e.RowIndex;
            var iddis = int.Parse(dataGridView1.Rows[idx].Cells["id"].Value.ToString());
            DB db = new DB();
            db.openConnection();
            try
            {
                string query1;
                if (dataGridView1.Rows[idx].Cells["Связь"].Value.ToString().Equals("Непривязано"))
                {
                    query1 = String.Format("INSERT INTO `staff_discipline` " +
                       "(`id_staff`, `id_discipline`) " +
                       "VALUES ( @id_staff, @id_discipline )", id);
                }
                else
                {
                    query1 = String.Format("DELETE FROM staff_discipline " +
                      "WHERE id_staff= @id_staff AND id_discipline = @id_discipline ", id);
                }

                MySqlCommand command = new MySqlCommand(query1, db.getConnection());
                
                command.Parameters.Add("@id_staff", MySqlDbType.Int32).Value = id;
                command.Parameters.Add("@id_discipline", MySqlDbType.Int32).Value = iddis;
              
                command.ExecuteNonQuery();
                //MessageBox.Show("Преподаватель привязан к дисциплине", "Заголовок", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch(MySqlException)
            {
               // MessageBox.Show("Преподаватель уже привязан к дисциплине", "Заголовок", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            db.closeConnection();
            add_di_LoadDate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Red f3 = new Red(id);
            f3.Show();
        }
    }
}
