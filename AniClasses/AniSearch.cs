using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AniHelper.AniClasses
{
    class AniSearch
    {
        public void getSearchData(String searchExtension)
        {
            String searchUrl = "https://myanimelist.net/search/all?q=";
            var http = new HttpClient();
            String html = http.GetStringAsync(searchUrl + transformSearchExtension(searchExtension));

            String newUrl = getMainSite(html, String.Join("", searchExtension.Split(' ')));
        }

        private String transformSearchExtension(String extension)
        {
            return (String.Join("%20", extension.Split(' ')));
        }

        private String getMainSite(String url, String keyword)
        {
            Match getSiteInfo = Regex.Match(url, @"<a href=[.*]>" + "(?i)" + keyword + @"</a>");
            return (String)getSiteInfo;
        }
    }
}
