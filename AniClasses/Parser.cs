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

        StackPanel testPanel;

        public void setPanel(StackPanel appliedPanel)
        {
            this.testPanel = appliedPanel;
        }
        public String returnInfo(String url)
        {
            HtmlWeb web = new HtmlWeb();
            var html = web.Load(url);
            return "beingimplemented";
        }

        public String[] getGenres()
        {
            IDbConnection AniConn = new System.Data.SqlClient.SqlConnection(connPt("AniHelper"));
            AniConn.Open();

            String[] genres = AniConn.Query<String>(@"use AniHelper SELECT Genre FROM GenreList").ToArray();

            AniConn.Close();
            return genres;
        }

        public String connPt(String id)
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
