using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace AniHelper.AniClasses
{
    public class Parser
    {
        public String returnInfo(String url)
        {
            HtmlWeb web = new HtmlWeb();
            var html = web.Load(url);
            return "beingimplemented";
        }

        private void getGenres()
        {
            SqlConnection AniConn = new SqlConnection();
            AniConn.Open();

            SqlCommand getGenre = new SqlCommand(@"use AniHelper SELECT Genre FROM GenreList", AniConn);
            var getData = getGenre.ExecuteReader();

            while (getData.Read())
            {

            }
        }
    }
}
