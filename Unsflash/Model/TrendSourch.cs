using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unsflash.Model
{
    public class TrendSourch
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string CoverImage { get; set; }
    }

    public class TrendSourchManager
    {
        public static List<TrendSourch> GetTrends()
        {
            var trends = new List<TrendSourch>();

            trends.Add(new TrendSourch { BookId = 1, Title = "Person", Author = "Futurum", CoverImage = "https://source.unsplash.com/weekly?person" });
            trends.Add(new TrendSourch { BookId = 2, Title = "Light", Author = "Sequiter Que", CoverImage = "https://source.unsplash.com/weekly?light" });
            trends.Add(new TrendSourch { BookId = 3, Title = "Blue", Author = "Tempor", CoverImage = "https://source.unsplash.com/weekly?blue" });
            trends.Add(new TrendSourch { BookId = 4, Title = "Pink", Author = "Option", CoverImage = "https://source.unsplash.com/weekly?pink" });
            trends.Add(new TrendSourch { BookId = 5, Title = "Woman", Author = "Accumsan", CoverImage = "https://source.unsplash.com/weekly?woman" });
            trends.Add(new TrendSourch { BookId = 6, Title = "Art", Author = "Legunt Xaepius", CoverImage = "https://source.unsplash.com/weekly?art" });
            trends.Add(new TrendSourch { BookId = 7, Title = "Fashion", Author = "Eleifend", CoverImage = "https://source.unsplash.com/weekly?fashion" });
            trends.Add(new TrendSourch { BookId = 8, Title = "Glow", Author = "Vero Tation", CoverImage = "https://source.unsplash.com/weekly?glow" });
            trends.Add(new TrendSourch { BookId = 9, Title = "Green", Author = "Jack Tibbles", CoverImage = "https://source.unsplash.com/weekly?green" });
            trends.Add(new TrendSourch { BookId = 10, Title = "Glass", Author = "Tuffy Tibbles", CoverImage = "https://source.unsplash.com/weekly?glass" });
            trends.Add(new TrendSourch { BookId = 11, Title = "Sunglass", Author = "Volupat", CoverImage = "https://source.unsplash.com/weekly?sunglass" });
            trends.Add(new TrendSourch { BookId = 12, Title = "Portrait", Author = "Est Possim", CoverImage = "https://source.unsplash.com/weekly?Portrait" });
            trends.Add(new TrendSourch { BookId = 13, Title = "Business", Author = "Magna", CoverImage = "https://source.unsplash.com/weekly?business" });
            trends.Add(new TrendSourch { BookId = 14, Title = "Computer", Author = "Mickey Gast", CoverImage = "https://source.unsplash.com/weekly?computer" });
            trends.Add(new TrendSourch { BookId = 15, Title = "Nature", Author = "Matthew Kerslake", CoverImage = "https://source.unsplash.com/weekly?nature" });
            trends.Add(new TrendSourch { BookId = 16, Title = "Love", Author = "Joshua Fuller", CoverImage = "https://source.unsplash.com/weekly?love" });
            trends.Add(new TrendSourch { BookId = 17, Title = "House", Author = "Jonathan Gallegos", CoverImage = "https://source.unsplash.com/weekly?house" });
            trends.Add(new TrendSourch { BookId = 18, Title = "Flying", Author = "Cody Doherty", CoverImage = "https://source.unsplash.com/weekly?flying" });
            trends.Add(new TrendSourch { BookId = 19, Title = "Sky", Author = "Max Boettinger", CoverImage = "https://source.unsplash.com/weekly?sky" });
            trends.Add(new TrendSourch { BookId = 120, Title = "Wing", Author = "Christian Bolt", CoverImage = "https://source.unsplash.com/weekly?wings" });

            return trends;
        }
    }
}
