using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
namespace MyWebApplication.Page.MachineLearning
{
    public partial class TestExampleKNN_my : System.Web.UI.Page
    {
        public DataBase dataBase;
        protected void Page_Load(object sender, EventArgs e)
        {

            //string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"TestDataBase.txt");
            //string path1 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"TeacherDataBase.txt");

            string[] TeacherDataBase = File.ReadAllLines(@"D:\LastVerionsProject\MyWebApplication\Page\MachineLearning\TeacherDataBase.txt");
            string[] TestDataBase = File.ReadAllLines(@"D:\LastVerionsProject\MyWebApplication\Page\MachineLearning\TestDataBase.txt");
            int[] rules = { 2, 3, 2, 3, 2, 3, 3, 3, 3, 3, 2, 2, 2, 3 };

            dataBase = new DataBase(TeacherDataBase, TestDataBase, rules);
        }

        protected void KNNClick(object sender, EventArgs e)
        {
            List<int> IndexCharacterString = new List<int>() { 0, 1, 2, 3 };
            List<int> IndexCharacterDouble = new List<int>() { 2, 3, 4 };

            label_1.Text = (dataBase.MyMethod(0, 500, IndexCharacterString, IndexCharacterDouble, 2, 12) / 500.0).ToString();

            
        }
    }
}