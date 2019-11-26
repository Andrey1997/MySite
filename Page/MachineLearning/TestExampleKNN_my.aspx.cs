using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Windows;
using MyWebApplication.Properties;

namespace MyWebApplication.Page.MachineLearning
{
    public partial class TestExampleKNN_my : System.Web.UI.Page
    {   
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBoxExample.Text = "0 //начальная позиция в данных\n" +
                "100 //количество элементов для теста(максимум 15060)\n" +
                "0,1,2,3 //какие строковые признаки рассматривать всего 8 признаков\n" +
                "2,3,4 //какие числовые признаки рассматривать всего 5 признаков\n" +
                "2 //недопустимое количество строковых несовпадений(от 1 до 3)\n" +
                "12 //максимальное колчество рассматриваемых соседей (от 5 до 20)";
        }

        protected void DefaultExampleClick(object sender, EventArgs e)
        {
            TextBoxUserParameter.Text = "0\n" +
                "100\n" +
                "0,1,2,3\n" +
                "2,3,4\n" +
                "2\n" +
                "12";
        }

        protected void KNNClick(object sender, EventArgs e)
        {
            try
            {
                DataBase dataBase;

                string[] TeacherDataBase = Resources.TeacherDataBase.Split('\n');
                string[] TestDataBase = Resources.TestDataBase.Split('\n');

                int[] rules = { 2, 3, 2, 3, 2, 3, 3, 3, 3, 3, 2, 2, 2, 3 };

                dataBase = new DataBase(TeacherDataBase, TestDataBase, rules);
                var parametr = GetParameter();
                label_1.Text = (dataBase.MyMethod(parametr.position, parametr.NumberTestObject, parametr.IndexCharacterString, parametr.IndexCharacterDouble, parametr.MaxStringError, parametr.NumberNeighbours) / (double)parametr.NumberTestObject).ToString();

            }
            catch (Exception str)
            {
                Response.Write("<script>alert('" + str.Message + "')</script>");
            }            
        }

        private (int position, int NumberTestObject, List<int> IndexCharacterString, List<int> IndexCharacterDouble, int MaxStringError, int NumberNeighbours) GetParameter()
        {
            string[] TextParameter = TextBoxUserParameter.Text.Split('\n');
            if(TextParameter.Count() != 6)
            {
                throw new Exception("Некорректное количество параметров");
            }

            int pozition = Convert.ToInt32(TextParameter[0]);
            int count = Convert.ToInt32(TextParameter[1]);

            if (pozition < 0 || pozition + count > 15060)
            {
                throw new Exception("Позиция выходит за допустимые рамки");
            }

            /*if(count > 100 || count < 1)
            {
                throw new Exception("Количество объектов выходит за допустимые рамки");
            }*/

            int CountError = Convert.ToInt32(TextParameter[4]);
            int NumberNeighbours = Convert.ToInt32(TextParameter[5]);

            if (CountError > 3 || CountError < 1)
            {
                throw new Exception("Количество нестроковых ошибок выходит за допустимые рамки");
            }

            if (NumberNeighbours > 20 || NumberNeighbours < 5)
            {
                throw new Exception("Количество соседей выходит за допустимые рамки");
            }

            List<int> IndexCharacterString = new List<int>();
            List<int> IndexCharacterDouble = new List<int>();

            string[] IndexCharacterString_str = TextParameter[2].Split(',');
            string[] IndexCharacterDouble_str = TextParameter[3].Split(',');

            for (int i = 0; i < IndexCharacterString_str.Count(); i++)
            {
                int temp = Convert.ToInt32(IndexCharacterString_str[i]);
                if(temp < 0 || temp > 8)
                {
                    throw new Exception("Некорректное число во 2 строке");
                }
                IndexCharacterString.Add(temp);
            }

            for (int i = 0; i < IndexCharacterDouble_str.Count(); i++)
            {
                int temp = Convert.ToInt32(IndexCharacterDouble_str[i]);
                if (temp < 0 || temp > 5)
                {
                    throw new Exception("Некорректное числов в 3 строке");
                }
                IndexCharacterDouble.Add(temp);
            }

            return (pozition , count , IndexCharacterString , IndexCharacterDouble , CountError, NumberNeighbours);
        }
    }
}