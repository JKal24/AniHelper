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
            "Horror", "Drama", "Kids", "Fantasy", "Magic", "NSFW", "Ecchi", "Historical", "Psychological",
            "SliceOfLife", "School", "Shounen", "Sports", "SciFi", "Supernatural", "Parody", "Mystery",
            "Shoujo", "Space", "Military"};

        public String[] get_available_genres()
        {
            return availableGenres;
        }

        public int get_selected_genres_length()
        {
            return selectedGenres.Count;
        }

        public void add_genre(String val)
        {
            if (Array.IndexOf(availableGenres, val) != -1)
            {
                selectedGenres.Add(val);
            }
        }

        public void remove_genre(String val)
        {
            selectedGenres.Remove(val);
        }
    }
}
