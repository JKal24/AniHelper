using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Dapper;
using HtmlAgilityPack;

namespace AniHelper.AniClasses
{
    public class Parser
    {
        private IDbConnection AniConn;

        public Parser()
        {
            AniConn = new System.Data.SqlClient.SqlConnection(connPt("AniHelper"));
        }

        public String returnInfo(String url)
        {
            HtmlWeb web = new HtmlWeb();
            var html = web.Load(url);
            return "beingimplemented";
        }

        public String[] getGenres()
        {
            AniConn.Open();

            String[] genres = AniConn.Query<String>(@"use AniHelper SELECT Genre FROM GenreList").ToArray();

            AniConn.Close();
            return genres;
        }

        public void TblUpdate(String url, String genre, int weight)
        {
            AniConn.Open();

            var result = AniConn.Query("InsertChosenGenres", new { Genre = genre, Weight = weight }, 
            commandType: CommandType.StoredProcedure);
            
        }

        public String connPt(String id)
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
