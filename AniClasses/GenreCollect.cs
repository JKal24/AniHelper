using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Dapper;
using HtmlAgilityPack;

namespace AniHelper.AniClasses
{
    public class GenreCollect
    {

        public GenreCollect()
        {
            assignGenres();
        }

        public AniSearch searcher = new AniSearch();
        private Dictionary<String, int> selectedGenres = new Dictionary<string, int>();

        private String[] availableGenres;

        public String[] get_available_genres()
        {
            return availableGenres;
        }

        public int get_selected_genres_length()
        {
            return selectedGenres.Count;
        }

        private void assignGenres()
        {
            Parser parse = new Parser();
            availableGenres = parse.getGenres();
        }

        public void add_genre(String inputKey)
        {
            if (selectedGenres.ContainsKey(inputKey))
            {
                selectedGenres[inputKey] += 1;
            } else
            {
                selectedGenres.Add(inputKey, 1);
            }
        }

        public void add_named_genres(HtmlNodeCollection items)
        {
            foreach (HtmlNode item in items)
            {
                add_genre(item.InnerText);
            }
        }

        public void remove_genre(String val)
        {
            selectedGenres.Remove(val);
        }

    }
}
