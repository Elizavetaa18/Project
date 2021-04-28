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
    public partial class dis : Form
    {
        MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=project");
        int id;
        public dis(int id)
        {
            InitializeComponent();
            this.id = id;

        }

        private void dis_Load(object sender, EventArgs e)
        {
            DB db = new DB();
            db.openConnection();
           // dataGridView1.Height = dataGridView1.Rows.GetRowsHeight(DataGridViewElementStates.None);

            string query1 = String.Format("SELECT name_discipline AS 'Дисциплины' " +
                "FROM discipline d  " +
                "JOIN staff_discipline sd ON d.id_discipline = sd.id_discipline " +
                "JOIN staff s ON s.id_staff = sd.id_staff " +
                "where  s.id_staff={0} ", id);

            MySqlDataAdapter adapter = new MySqlDataAdapter(query1, connection);
            DataSet data = new DataSet();
            adapter.Fill(data);

            dataGridView1.ColumnCount = 1;
            dataGridView1.Columns[0].Name = "Дисциплины";

            foreach (DataRow item in data.Tables[0].Rows)
            {
                string[] buf = { item["Дисциплины"].ToString()};
                dataGridView1.Rows.Add(buf);
            }

            db.closeConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Information f2 = new Information(id);
            f2.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
