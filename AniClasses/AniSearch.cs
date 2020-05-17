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
        public async Task getSearchData(String searchExtension)
        {
            String searchUrl = "https://myanimelist.net/search/all?q=";
            var http = new HttpClient();
            String html;

            try
            {
                html = await http.GetStringAsync((searchUrl + searchExtension));
            } catch
            {
                html = "Data not found.";
            }

            String newUrl = getMainSite(html, String.Join("", searchExtension.Split(' ')));
        }

        private String transformSearchExtension(String extension)
        {
            return (String.Join("%20", extension.Split(' ')));
        }

        private String getMainSite(String url, String keyword)
        {
            Match getSiteInfo = Regex.Match(url, @"<a href=(.*)> (?i)" + keyword + @"</a>");
            String siteString = "";
            if (getSiteInfo.Success)
            {
                siteString = getSiteInfo.Value;

                /* Condense the info into the url to go to the anime's main page */

                /* regex needs to be implemented (get a " in there?) */
                getSiteInfo = Regex.Match(siteString, @"(?i) https(.*)[]");
            }


            return siteString;
        }
    }
}
