using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyWebApplication.Page.POE
{
    public partial class DPS_item : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonClickCalculateDPS(object sender, EventArgs e)
        {
            try
            {
                double min_damadge = Convert.ToDouble(MinDamadge.Text);
                double max_damadge = Convert.ToDouble(MaxDamadge.Text);
                double aps = double.Parse(APS.Text, CultureInfo.CreateSpecificCulture("en"));

                ResultCalculate.Text = ((min_damadge + max_damadge) / 2 * aps).ToString("N");
            }
            catch
            {
                ResultCalculate.Text = "";
            }

        }
    }
}