using Dapper;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace AniHelper.AniClasses
{
    public class AniSearch
    {
        public String currentUrl;
        public StackPanel namePanel;
        public bool complete = true;

        public GenreCollect collector = new GenreCollect();
        private Parser getInfo = new Parser();

        public async Task getSearchData(String searchExtension)
        {
            /* Access the search url and input the Anime name to get the first result */

            String searchUrl = "https://myanimelist.net/search/all?q=";
            var http = new HttpClient();
            String html;

            try
            {
                /* Parse through the html and use regex to find the url that leads to the Anime's page */

                html = await http.GetStringAsync((searchUrl + transformSearchExtension(searchExtension)));
                currentUrl = getMainSite(html);
            } catch
            {
                currentUrl = "Data not found.";
            }
        }

        public void getAnimeList()
        {
            /* gets a list of potential anime to recommend */

            int score = 10;
            int myid;

            List<String> ids = getInfo.TblGetIDString(collector.sort_remove_select_gen());
            String searchUrl = "https://myanimelist.net/anime.php?q=&score=" + score.ToString() +
                "&genre%5B%5D="; /* + "insert id" */

            foreach (String id in ids)
            {
                myid = getInfo.getID(id);
                TextBlock idNum = new TextBlock();
                idNum.Text = id.ToString();
                namePanel.Children.Add(idNum);
                myid = 0;
            }
        }

        private String transformSearchExtension(String extension)
        {
            /* transforms so that it can be used in the url search key */

            return (String.Join("%20", extension.Split(' ')));
        }

        private String getMainSite(String url)
        {
            /* Regex matcher used when accessing the search page on MAL */

            Match getSiteInfo = Regex.Match(url, @"https://?myanimelist.net/anime/\d*\/\w*");
            return getSiteInfo.Value;
        }

        public HtmlNodeCollection addName()
        {
            /* HTMLAgilityPack used, lists the anime that the user has selected */

            HtmlWeb httpAccess = new HtmlWeb();
            var html = httpAccess.Load(currentUrl);
            var node = html.DocumentNode.SelectSingleNode("//span[@itemprop='name']//text()");
            var genreNodes = html.DocumentNode.SelectNodes("//span[@itemprop='genre']//text()");

            TextBlock name = new TextBlock();
            name.Text = node.InnerText;
            name.Margin = new Thickness(10);
            namePanel.Children.Add(name);
            complete = true;
            return genreNodes;
        }

        public void init_namepanel()
        {
            /* puts the name panel onto the main panel in the main window */

            namePanel = new StackPanel();
            namePanel.Orientation = Orientation.Horizontal;
            namePanel.HorizontalAlignment = HorizontalAlignment.Center;
            namePanel.Margin = new Thickness(10);
        }
    }
}
