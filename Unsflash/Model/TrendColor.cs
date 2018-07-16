using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unsflash.Model
{
    public class TrendColor
    {
        public int IdColor { get; set; }
        public string HexColor { get; set; }
        public string NameColor { get; set; }
        public string LinkColor { get; set; }
    }

    public class TrendColorManage
    {
        public static List<TrendColor> GetColor()
        {
            var trends = new List<TrendColor>();

            trends.Add(new TrendColor { IdColor = 1, HexColor = "#6E7B8B", NameColor = "Dusky Blue", LinkColor = "Dusky%20Blue" });
            trends.Add(new TrendColor { IdColor = 2, HexColor = "#7AC5CD", NameColor = "Blue-Green", LinkColor = "Blue-Green" });
            trends.Add(new TrendColor { IdColor = 3, HexColor = "#FFFFE0", NameColor = "Sunshine Yellow", LinkColor = "Sunshine%20Yellow" });
            trends.Add(new TrendColor { IdColor = 4, HexColor = "#79CDCD", NameColor = "Mineral Gray", LinkColor = "Mineral%20Gray" });
            trends.Add(new TrendColor { IdColor = 5, HexColor = "#548B54", NameColor = "Night Blue-Green", LinkColor = "Night%20Blue-Green" });
            trends.Add(new TrendColor { IdColor = 6, HexColor = "#7FFFD4", NameColor = "Earthy Green", LinkColor = "Earthy%20Green" });
            trends.Add(new TrendColor { IdColor = 7, HexColor = "#A0522D", NameColor = "Taupe Beige", LinkColor = "Taupe%20Beige" });
            trends.Add(new TrendColor { IdColor = 8, HexColor = "#B0E0E6", NameColor = "Powdery Blue", LinkColor = "Powdery%20Blue" });
            trends.Add(new TrendColor { IdColor = 9, HexColor = "#FFFF00", NameColor = "Dusted Yellow", LinkColor = "Dusted%20Yellow" });
            trends.Add(new TrendColor { IdColor = 10, HexColor = "#FFC0CB", NameColor = "Pastel Pink", LinkColor = "Pastel%20Pink" });

            return trends;
        }
    }
}
