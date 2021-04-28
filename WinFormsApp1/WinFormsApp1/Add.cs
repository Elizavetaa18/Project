using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WinFormsApp1
{
    public partial class Add : Form
    {
        MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=project");
        private Dictionary<string, string> _famaliStatis = new Dictionary<string, string>();
        private Dictionary<string, string> _titleStatis = new Dictionary<string, string>();
        private Dictionary<string, string> _degreeStatis = new Dictionary<string, string>();

        public Add()
        {
            DB db = new DB();
            db.openConnection();
            InitializeComponent();

            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox4.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox5.DropDownStyle = ComboBoxStyle.DropDownList;

            DataTable linkcat = new DataTable("linkcat");
            //Запрос на извлечение семейного положения из базы данных
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

        

        private void button2_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            db.openConnection();

            if (family.Text == "" || !Regex.IsMatch(family.Text, @"^[а-яА-Я]+$"))
            {
                MessageBox.Show("Фамилия введена не корректно");
                return;
            }

            if (name.Text == "" || !Regex.IsMatch(name.Text, @"^[а-яА-Я]+$"))
            {
                MessageBox.Show("Имя введено не корректно");
                return;
            }

            if (!Regex.IsMatch(surname.Text, @"^[А-яёЁ]") && surname.Text.Length > 0)
            {
                MessageBox.Show("Отчество введено не корректно");
                return;
            }


            MySqlCommand command = new MySqlCommand("INSERT INTO `staff` " +
                "(`family`, `name`, `surname`, `gender`, `birthday`, `double_job`," +
                " `id_position`, `id_academic_title`, `id_academic_degree`) " +
                "VALUES ( @family, @name, @surname, @gender, @birthday, @double_job," +
                " @id_position, @id_academic_title, @id_academic_degree);", db.getConnection());

            command.Parameters.Add("@family",MySqlDbType.VarChar).Value = family.Text;
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = name.Text;
            command.Parameters.Add("@surname", MySqlDbType.VarChar).Value = surname.Text;
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

            this.Hide();
            Form1 f3 = new Form1();
            f3.Show();


            db.closeConnection();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f3 = new Form1();
            f3.Show();
        }
    }
}
