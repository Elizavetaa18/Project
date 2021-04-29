using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace WinFormsApp1
{
    public partial class Red : Form
    {
        MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=project");
        int id;
        private Dictionary<string, string> _famaliStatis = new Dictionary<string, string>();
        private Dictionary<string, string> _titleStatis = new Dictionary<string, string>();
        private Dictionary<string, string> _degreeStatis = new Dictionary<string, string>();

        public Red(int id)
        {

            DB db = new DB();
            db.openConnection();

            InitializeComponent();
            this.id = id;

            DataTable linkcat = new DataTable("linkcat");
            //Запрос на извлечение дисциплины из базы данных
            using (MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM position", connection))
            {
                da.Fill(linkcat);
            }
            foreach (DataRow da in linkcat.Rows)
            {
                _famaliStatis.Add(da[1].ToString(), da[0].ToString());
                comboBox3.Items.Add(da[1].ToString());
            }

            DataTable linkcat1 = new DataTable("linkcat1");
            using (MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM title", connection))
            {
                da.Fill(linkcat1);
            }
            foreach (DataRow da in linkcat1.Rows)
            {
                _titleStatis.Add(da[1].ToString(), da[0].ToString());
                comboBox4.Items.Add(da[1].ToString());
            }

            DataTable linkcat2 = new DataTable("linkcat2");
            using (MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM degree", connection))
            {
                da.Fill(linkcat2);
            }
            foreach (DataRow da in linkcat2.Rows)
            {
                _degreeStatis.Add(da[1].ToString(), da[0].ToString());
                comboBox5.Items.Add(da[1].ToString());
            }
            db.closeConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Information f3 = new Information(id);
            f3.Show();
        }

        private void Red_Load(object sender, EventArgs e)
        {
            string query1 = String.Format("SELECT id_staff, family , " +
               "name , surname, gender, birthday, double_job, p.position,  academic_title, " +
               "academic_degree, clock " +
               "FROM staff join position p ON staff.id_position = p.id_position " +
               "JOIN degree ad ON staff.id_academic_degree = ad.id_academic_degree " +
               "JOIN title t ON staff.id_academic_title = t.id_academic_title " +
               "where  id_staff={0} limit 1", id);

            MySqlDataAdapter adapter = new MySqlDataAdapter(query1, connection);
            DataSet data = new DataSet();
            adapter.Fill(data);
            DataRow item = data.Tables[0].Rows[0];

            textBox4.Text = item["family"].ToString();
            textBox3.Text = item["name"].ToString();
            textBox2.Text = item["surname"].ToString();
            comboBox1.Text = item["gender"].ToString();
            dateTimePicker1.Text = item["birthday"].ToString();
            comboBox2.Text = item["double_job"].ToString();
            comboBox3.Text = item["position"].ToString();
            comboBox4.Text = item["academic_title"].ToString();
            comboBox5.Text = item["academic_degree"].ToString();

            if (comboBox1.Text == "F")
            {
                comboBox1.Text = "Женский";
            }
            else
            {
                comboBox1.Text = "Мужской";
            }

            if (comboBox2.Text == "False")
            {
                comboBox2.Text = "Нет";
            }
            else
            {
                comboBox2.Text = "Да";
            }

        }
  
        private void button2_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            db.openConnection();

            if (textBox4.Text == "" || !Regex.IsMatch(textBox4.Text, @"^[а-яА-Я]+$"))
            {
                MessageBox.Show("Фамилия введена не корректно");
                return;
            }

            if (textBox3.Text == "" || !Regex.IsMatch(textBox3.Text, @"^[а-яА-Я]+$"))
            {
                MessageBox.Show("Имя введено не корректно");
                return;
            }

            if (!Regex.IsMatch(textBox2.Text, @"^[а-яА-Я]+$"))
            {
                MessageBox.Show("Отчество введено не корректно");
                return;
            }

            string query1 = String.Format("UPDATE `staff` SET  `family` = @family , `name` =  @name, " +
                "`surname` = @surname, `gender`= @gender, `birthday`= @birthday, " +
                "`double_job` = @double_job, `id_position` = @id_position, `id_academic_title` = @id_academic_title, " +
                "`id_academic_degree` = @id_academic_degree where  id_staff={0} limit 1", id);
            
            MySqlCommand command = new MySqlCommand(query1, db.getConnection());

            command.Parameters.Add("@family", MySqlDbType.VarChar).Value = textBox4.Text;
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = textBox3.Text;
            command.Parameters.Add("@surname", MySqlDbType.VarChar).Value = textBox2.Text;
            command.Parameters.Add("@birthday", MySqlDbType.Date).Value = dateTimePicker1.Value.Date;


            if (comboBox1.GetItemText(comboBox1.SelectedItem) == "Мужской")
            {
                command.Parameters.Add("@gender", MySqlDbType.VarChar).Value = "M";
            }
            if (comboBox1.GetItemText(comboBox1.SelectedItem) == "Женский")
            {
                command.Parameters.Add("@gender", MySqlDbType.VarChar).Value = "F";
            }

            if (comboBox2.GetItemText(comboBox2.SelectedItem) == "Да")
            {
                command.Parameters.Add("@double_job", MySqlDbType.Byte).Value = 1;
            }
            if (comboBox2.GetItemText(comboBox2.SelectedItem) == "Нет")
            {
                command.Parameters.Add("@double_job", MySqlDbType.Byte).Value = 0;
            }


            command.Parameters.Add("@id_position", MySqlDbType.Int32).Value = _famaliStatis[comboBox3.SelectedItem.ToString()];
            command.Parameters.Add("@id_academic_title", MySqlDbType.Int32).Value = _titleStatis[comboBox4.SelectedItem.ToString()];
            command.Parameters.Add("@id_academic_degree", MySqlDbType.Int32).Value = _degreeStatis[comboBox5.SelectedItem.ToString()];

            command.ExecuteNonQuery();
            db.closeConnection();


            this.Hide();
            Form1 f3 = new Form1();
            f3.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            

        }

        private void button4_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            db.openConnection();

            string query1 = String.Format("DELETE FROM staff  where  id_staff={0} limit 1", id);

            MySqlCommand command1 = new MySqlCommand(query1, db.getConnection());

            command1.ExecuteNonQuery();
            db.closeConnection();


            this.Hide();
            Form1 f3 = new Form1();
            f3.Show();

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            add_di f3 = new add_di(id);
            f3.Show();
        }
    }
}
