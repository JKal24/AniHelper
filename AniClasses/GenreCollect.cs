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

        public List<String> selectedGenres = new List<string>();

        private String[] availableGenres = {"Action", "Adventure", "Romance", "Comedy", "Isekai", 
            "Horror", "Drama", "Ecchi", "Fantasy", "Magic", "NSFW", "Ecchi", "Historical", "Psychological",
            "SliceOfLife", "School", "Shounen", "Sports", "SciFi", "Supernatural", "Parody", "Mystery",
            "Shoujo", "Space", "Military"
        };

        public String[] get_available_genres()
        {
            return availableGenres;
        }

        public void Add_genre(String val)
        {
            selectedGenres.Add(val);
        }

        public void getData()
        {
            String url = "https://myanimelist.net/anime/genre/1/Action";

            var http = new HttpClient();
            var html = http.GetStringAsync(url);

            Console.WriteLine(html.Result);

            Console.ReadLine();
        }
    }
}
