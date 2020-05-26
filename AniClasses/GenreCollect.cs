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
        private Dictionary<String, int> selectedGenres = new Dictionary<string, int>();
        private String[] availableGenres;
        public int checkedBoxes = 0;

        public GenreCollect()
        {
            assignGenres();
        }

        public String[] get_available_genres()
        {
            return availableGenres;
        }

        public int get_checkbox_selected_genres_length()
        {
            return checkedBoxes;
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
            if (selectedGenres[val] > 1)
            {
                selectedGenres[val] -= 1;
                return;
            }
            selectedGenres.Remove(val);
        }

        /* condenses the list of selected genres to the ones most applicable to the recommendation */
        public List<String> sort_remove_select_gen()
        {
            var mylist = from pair in selectedGenres orderby pair.Value descending select pair;

            List<String> topPicks = new List<String>();

            foreach (KeyValuePair<String, int> myPair in mylist)
            {
                topPicks.Add(myPair.Key);
                if (topPicks.Count >= 3)
                {
                    break;
                }
            }
            return topPicks;
        }

    }
}
