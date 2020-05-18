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

        public String currentUrl;

        public async Task getSearchData(String searchExtension)
        {
            String searchUrl = "https://myanimelist.net/search/all?q=";
            var http = new HttpClient();
            String html;

            try
            {
                html = await http.GetStringAsync((searchUrl + transformSearchExtension(searchExtension)));
                currentUrl = getMainSite(html);
            } catch
            {
                currentUrl = "Data not found.";
            }
        }

        private String transformSearchExtension(String extension)
        {
            return (String.Join("%20", extension.Split(' ')));
        }

        private String getMainSite(String url)
        {
            Match getSiteInfo = Regex.Match(url, @"https?://myanimelist.net/anime/\d*\/\w*");
            return getSiteInfo.Value;
        }
    }
}
