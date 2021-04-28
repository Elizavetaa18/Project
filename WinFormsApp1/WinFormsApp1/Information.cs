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
    public partial class Information : Form
    {
        MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=project");
        int id;

        public Information(int id)
        {
            this.id = id;
            InitializeComponent();
        }

        private void Information_Load(object sender, EventArgs e)
        {
            string query1 = String.Format("SELECT s.id_staff, family , " +
                "name , surname, gender, birthday, double_job, p.position,  academic_title, " +
                "academic_degree, clock " +
                "FROM staff s join position p ON s.id_position = p.id_position " +
                "JOIN degree ad ON s.id_academic_degree = ad.id_academic_degree " +
                "JOIN title t ON s.id_academic_title = t.id_academic_title " +
               // "JOIN staff_discipline sd ON s.id_staff = sd.id_staff " +
                "where  s.id_staff={0} ", id);

            MySqlDataAdapter adapter = new MySqlDataAdapter(query1, connection);
            DataSet data = new DataSet();
            adapter.Fill(data);
            DataRow item = data.Tables[0].Rows[0];


            family.Text = item["family"].ToString();
            name.Text = item["name"].ToString();
            surname.Text = item["surname"].ToString();
            gender.Text = item["gender"].ToString();
            birthday.Text = item["birthday"].ToString();
            double_job.Text = item["double_job"].ToString();
            position.Text = item["position"].ToString();
            academic_title.Text = item["academic_title"].ToString();
            academic_degree.Text = item["academic_degree"].ToString();
            clock.Text = item["clock"].ToString();


            if (gender.Text == "F")
            {
                gender.Text = "Женский";
            }
            else
            {
                gender.Text = "Мужской";
            }

            if (double_job.Text == "False")
            {
                double_job.Text = "Нет";
            }
            else
            {
                double_job.Text = "Да";
            }
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void name_discipline_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f3 = new Form1();
            f3.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Red f3 = new Red(id);
            f3.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            dis f3 = new dis(id);
            f3.Show();
        }
    }
}
