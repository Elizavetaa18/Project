using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WinFormsApp1;
using System.Windows.Forms;


namespace UnitTestProject1
{
    [TestClass]
    public class LaboratoryStaffTest
    {
        [TestMethod]
        public void TestConnect()
        {
            WinFormsApp1 Lb = new WinFormsApp1();
            Lb.Show();
            Assert.IsTrue(Lb.WinFormsAdd1.Rows.Count > 0);
        }

        [TestMethod]
        public void TestButtonAdd()
        {
            WinFormsAdd1 Lb = new WinFormsAdd1();
            Lb.Show();
            try
            {
                Lb.button_add.PerformClick();
            }
            catch
            {
                Assert.Fail();
            }
        }
    }


    [TestClass]
    public class AddTest
    {
        [TestMethod]
        public void TestAddStaff()
        {
            Add Adds = new Add();
            Adds.Show();
            Adds.family.Text = "Артамонов";
            Adds.name.Text = "Артамонов";
            Adds.surname.Text = "Олегович";
            Adds.comboBox1.SelectedItem = "Мужской(M)";
            Adds.dateTimePicker1.Value = DateTime.Now;
            Adds.comboBox2.SelectedItem = "Да";
            Adds.comboBox2.SelectedItem = "Доцент";
            Adds.comboBox2.SelectedItem = "Доцент";
            Adds.comboBox2.SelectedItem = "Доктор военных наук";

            Adds.button2.PerformClick();

            WinFormsApp1 Lb = new WinFormsApp1();
            Lb.Show();
            foreach (DataGridViewRow item in Lb.dataGridView1.Rows)
            {
                if (item.Cells["Фамилия"].Value.ToString().Equals("Артамонов") && item.Cells["Имя"].Value.ToString().Equals("Артамонов") && item.Cells["Отчество"].Value.ToString().Equals("Олегович") && item.Cells["Должность"].Value.ToString().Equals("Доцент") && item.Cells["Ученая степень"].Value.ToString().Equals("двоенн"))
                {
                    return;
                }
            }
            Assert.Fail();
        }
        [TestMethod]
        public void TestButtonBack()
        {
            Add Adds = new Add();
            Adds.Show();
            try
            {
                Adds.button1.PerformClick();
            }
            catch
            {
                Assert.Fail();
            }
        }
    }


    [TestClass]
    public class InformationTest
    {
        int id;

        [TestMethod]
        public void TestButtonRed()
        {
            Information Emp = new Information(id);
            Emp.Show();
            try
            {
                Emp.button2.PerformClick();
            }
            catch
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void TestButtonBack()
        {
            Information Emp = new Information(id);
            Emp.Show();
            try
            {
                Emp.button1.PerformClick();
            }
            catch
            {
                Assert.Fail();
            }
        }
    }

    [TestClass]
    public class RedTest
    {

        [TestMethod]
        public void TestRed()
        {
            WinFormsApp1 Lb = new WinFormsApp1();
            Lb.Show();
            DataGridViewRow item = Lb.dataGridView1.Rows[0];

            int id = int.Parse(item.Cells["id"].Value.ToString());
            Lb.Hide();

            Red red = new Red(id);
            red.Show();
            Assert.AreEqual(red.textBox4.Text, item.Cells["Фамилия"].Value.ToString());
            Assert.AreEqual(red.textBox3.Text, item.Cells["Имя"].Value.ToString());
            Assert.AreEqual(red.textBox2.Text, item.Cells["Отчество"].Value.ToString());
            Assert.AreEqual(red.comboBox3.Text, item.Cells["Должность"].Value.ToString());


            red.textBox4.Text = "Абакиров";
            red.textBox3.Text = "Дмитрий";

            red.button2.PerformClick();

            Lb.Dispose();
            Lb = new WinFormsApp1();
            Lb.Show();
            foreach (DataGridViewRow item1 in Lb.dataGridView1.Rows)
            {
                if (item1.Cells["Фамилия"].Value.ToString().Equals("Абакиров"))
                {
                    return;
                }
            }
            Assert.Fail();


        }
        [TestMethod]
        public void TestButtonDel()
        {
            WinFormsApp1 Lb = new WinFormsApp1();
            Lb.Show();
            DataGridViewRow item = Lb.dataGridView1.Rows[0];

            int id = int.Parse(item.Cells["id"].Value.ToString());
            Lb.Hide();

            Red red = new Red(id);
            red.Show();

            red.button4.PerformClick();

            Lb.Dispose();
            Lb = new WinFormsApp1();
            Lb.Show();
            foreach (DataGridViewRow item1 in Lb.dataGridView1.Rows)
            {
                if (item1.Cells["id"].Value.ToString().Equals(id.ToString()))
                {
                    Assert.Fail();
                }
            }
        }
    }

}
