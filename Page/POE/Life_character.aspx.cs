using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyWebApplication.Page.POE
{
    public partial class Life_character : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonClickCalculateLife(object sender, EventArgs e)
        {
            try
            {
                double base_life = Convert.ToDouble(TextBox_lvl.Text) * 12 + Convert.ToDouble(TextBox_strength.Text) / 2 + Convert.ToDouble(TextBox_add_life.Text) + 38.0;
                double inc_life = Convert.ToDouble(TextBox_inc_life.Text) / 100 + 1.0;
                double more_life = Convert.ToDouble(TextBox_more_life.Text) / 100 + 1.0;
                double less_life = 1.0 - Convert.ToDouble(TextBox_less_life.Text) / 100;

                double result_life = base_life * inc_life * more_life * less_life;
                ResultCalculate.Text = "Итоговое здоровье = " + result_life.ToString("N");

                double result_1_inc = (inc_life + 0.01) * base_life / inc_life - base_life;
                ResultCalculate_1_inc.Text = "1 increased life соответствует " + result_1_inc.ToString("N");
                ResultCalculate_5_inc.Text = "5 increased life соответствует " + (5 * result_1_inc).ToString("N");
            }
            catch
            {
                ResultCalculate.Text = "Incorrect value";
                ResultCalculate_1_inc.Text = "";
                ResultCalculate_5_inc.Text = "";
            }
        }
    }
}