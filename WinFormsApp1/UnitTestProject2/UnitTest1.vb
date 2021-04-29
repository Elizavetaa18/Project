Using Microsoft.VisualStudio.TestTools.UnitTesting;
Using System;
Using WinFormsApp1;
Using System.Windows.Forms;


Namespace UnitTestProject1
{
    [TestClass]
    Public Class LaboratoryStaffTest
    {
        [TestMethod]
        Public void TestConnect()
        {
            WinFormsApp1 Lb = New WinFormsApp1();
            Lb.Show();
            Assert.IsTrue(Lb.WinFormsAdd1.Rows.Count > 0);
        }

        [TestMethod]
        Public void TestButtonAdd()
        {
            WinFormsAdd1 Lb = New WinFormsAdd1();
            Lb.Show();
            Try
            {
                Lb.buttonAdd.PerformClick();
            }
            Catch
            {
                Assert.Fail();
            }
        }
    }
}
