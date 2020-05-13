using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AniHelper.AniClasses
{
    class GenreCollect
    {

        public List<String> Genres = new List<string>();

        private String[] availableGenres = {"Action", "Adventure", "Romance", "Comedy", "Isekai", 
            "Horror", "Drama", "Ecchi", "Fantasy", "Magic", "NSFW", "Ecchi", "Historical", "Psychological",
            "Slice of Life", "School", "Shounen", "Sports", "Sci-Fi", "Supernatural", "Parody", "Mystery",
            "Shoujo", "Space", "Military"
        };

        public List<String> Get_genres { get; set; }

        public void Add_genre(String val)
        {
            Genres.Add(val);
        }

        public void getData()
        {
            String url = "https://myanimelist.net/anime/genre/1/Action";

            var http = new HttpClient();
            var html = http.GetStringAsync(url);

            Console.WriteLine(html.Result);

            Console.ReadLine();
        }

        public void makeGenreButtons(String[] buttonNames)
        {
            /* initialize starting screen with the check buttons to choose from */

            foreach (String button in buttonNames)
            {
                CheckBox box = new CheckBox();

                box.Name = button;
                box.Content = button;
                box.Width = 140;

                
            }
        }
    }
}
