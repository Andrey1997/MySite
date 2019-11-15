using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyWebApplication.Page.POE
{
    public partial class min_max_DPS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonClickCalculateMinMaxDPS(object sender, EventArgs e)
        {
            try
            {
                Weapons weapons = new Weapons(Convert.ToInt32(BaseMinDMG.Text), Convert.ToInt32(BaseMaxDMG.Text), double.Parse(BaseAttackSpeed.Text, CultureInfo.CreateSpecificCulture("en")));

                string[] _PreficsIncreasedPhysDMG = System.Text.RegularExpressions.Regex.Split(PreficsIncreasedPhysDMG.Text, " to ");
                Pair inc_phys = new Pair(Convert.ToInt32(_PreficsIncreasedPhysDMG[0]), Convert.ToInt32(_PreficsIncreasedPhysDMG[1]));

                string[] _PreficsHybridIncreasedPhysDMG = System.Text.RegularExpressions.Regex.Split(PreficsHybridIncreasedPhysDMG.Text, " to ");
                Pair inc_phys_hybrid = new Pair(Convert.ToInt32(_PreficsHybridIncreasedPhysDMG[0]), Convert.ToInt32(_PreficsHybridIncreasedPhysDMG[1]));

                string[] _SuffixIncreasedAPS = System.Text.RegularExpressions.Regex.Split(SuffixIncreasedAPS.Text, " to ");
                Pair inc_aps = new Pair(Convert.ToInt32(_SuffixIncreasedAPS[0]), Convert.ToInt32(_SuffixIncreasedAPS[1]));

                string[] _SuffixIncreasedCritChance = System.Text.RegularExpressions.Regex.Split(SuffixIncreasedCritChance.Text, " to ");
                Pair add_quality = new Pair(Convert.ToInt32(_SuffixIncreasedCritChance[0]), Convert.ToInt32(_SuffixIncreasedCritChance[1]));

                string[] _SuffixEldeIncreasedPhysDMG = System.Text.RegularExpressions.Regex.Split(SuffixEldeIncreasedPhysDMG.Text, " to ");
                Pair inc_phys_suffix = new Pair(Convert.ToInt32(_SuffixEldeIncreasedPhysDMG[0]), Convert.ToInt32(_SuffixEldeIncreasedPhysDMG[1]));

                int quality = Convert.ToInt32(QualityItem.Text);

                string[] _PreficsAddPhysDMG = System.Text.RegularExpressions.Regex.Split(PreficsAddPhysDMG.Text, " / ");
                string[] left_PreficsAddPhysDMG = System.Text.RegularExpressions.Regex.Split(_PreficsAddPhysDMG[0], " to ");
                string[] right_PreficsAddPhysDMG = System.Text.RegularExpressions.Regex.Split(_PreficsAddPhysDMG[1], " to ");
                Pair add_dmg_min = new Pair(Convert.ToInt32(left_PreficsAddPhysDMG[0]), Convert.ToInt32(right_PreficsAddPhysDMG[0]));
                Pair add_dmg_max = new Pair(Convert.ToInt32(left_PreficsAddPhysDMG[1]), Convert.ToInt32(right_PreficsAddPhysDMG[1]));

                weapons.CalculateDPSItem(inc_phys, inc_phys_hybrid, add_dmg_min, add_dmg_max, inc_aps, add_quality, inc_phys_suffix, quality);
                ResultCalculate.Text = "min DPS = " + weapons.min_dps.ToString("N") + " / max DPS = " + weapons.max_dps.ToString("N"); ;
            }
            catch
            {
                ResultCalculate.Text = "Incorrect value";
            }
        }
    }

    class Weapons
    {
        public int min_damadge;
        public int max_damadge;
        public double aps;

        public double min_dps;
        public double max_dps;

        public Weapons(int min, int max, double aps_)
        {
            min_damadge = min;
            max_damadge = max;
            aps = aps_;
        }
        public void CalculateDPSItem(Pair inc_phys, Pair inc_phys_hybrid, Pair add_dmg_min, Pair add_dmg_max, Pair inc_aps, Pair add_quality, Pair inc_phys_suffix, int quality)
        {
            double temp_min_increased = (inc_phys.First + inc_phys_hybrid.First + inc_phys_suffix.First + add_quality.First + quality + 100.0);
            double temp_max_increased = (inc_phys.Second + inc_phys_hybrid.Second + inc_phys_suffix.Second + add_quality.Second + quality + 100.0);

            temp_min_increased /= 100.0;
            temp_max_increased /= 100.0;

            double min_result_damadge_First = (add_dmg_min.First + min_damadge);
            double max_result_damadge_First = (add_dmg_min.Second + max_damadge);

            double min_result_damadge_Second = (add_dmg_max.First + min_damadge);
            double max_result_damadge_Second = (add_dmg_max.Second + max_damadge);

            double min_inc_aps = inc_aps.First + 100.0;
            double max_inc_aps = inc_aps.Second + 100.0;

            min_inc_aps /= 100.0;
            max_inc_aps /= 100.0;

            min_dps = (min_result_damadge_First + max_result_damadge_First) * temp_min_increased / 2.0 * (aps * min_inc_aps);
            max_dps = (min_result_damadge_Second + max_result_damadge_Second) * temp_max_increased / 2.0 * (aps * max_inc_aps);

        }
    }

    public class Pair
    {
        public Pair()
        {
        }

        public Pair(int first, int second)
        {
            this.First = first;
            this.Second = second;
        }

        public int First { get; set; }
        public int Second { get; set; }
    };
}