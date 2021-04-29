using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WinFormsApp1;
using System.Windows.Forms;



namespace Test
{
    [TestClass]
    public class Form1Test
    {
        [TestMethod]
        public void TestConnect()
        {
            WinFormsApp1 Lb = new WinFormsApp1();
            Lb.Show();
            Assert.IsTrue(Lb.dataGridView1.Rows.Count > 0);
        }

      /*  [TestMethod]
        public void TestButtonAdd()
        {
            LaboratoryStaff Lb = new LaboratoryStaff();
            Lb.Show();
            try
            {
                Lb.buttonAdd.PerformClick();
            }
            catch
            {
                Assert.Fail();*/
            }
        }
    }
}
