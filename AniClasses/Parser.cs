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

        public void inputAnime(String[] data)
        {
            AniConn.Open();

            AniConn.Execute("Insert_Ani_Data", new { name = data[0], score = data[1], info = data[2] },
                commandType: CommandType.StoredProcedure);

            AniConn.Close();
        }

        public void resetRecommendationTable()
        {
            AniConn.Open();

            AniConn.Execute(@"USE AniHelper DELETE FROM AnimeList");

            AniConn.Close();
        }

        public String[] getGenres()
        {
            AniConn.Open();

            String[] genres = AniConn.Query<String>(@"use AniHelper SELECT Genre FROM GenreList").ToArray();

            AniConn.Close();
            return genres;
        }

        public List<String> getSelectedGenres()
        {
            AniConn.Open();

            List<String> topGenres = AniConn.Query<String>("GetTopGenres", commandType: CommandType.StoredProcedure).ToList();

            AniConn.Close();

            return topGenres;
        }

        public int getID(String mygenre)
        {
            AniConn.Open();

            int id = AniConn.Query<int>("get_id", new { genre = mygenre }, 
                commandType: CommandType.StoredProcedure).SingleOrDefault();

            AniConn.Close();

            return id;
        }

        public List<String> TblGetIDString(List<String> topPicks)
        {
            List<String> ids = new List<String>();

            foreach (String pick in topPicks)
            {
                ids.Add(getID(pick).ToString());
            }

            return ids;
        }

        public void getRecommendationTbl()
        {
            AniConn.Open();

            var results = AniConn.Query("get_recommendation", commandType: CommandType.StoredProcedure).ToList();

            AniConn.Close();
        }

        public String connPt(String id)
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
