using System;
using System.Collections.Generic;
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
            var node = html.;
        }

        private void addToTable()
        {
            SqlConnection AniConn = new SqlConnection();
        }
    }
}
