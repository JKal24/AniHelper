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

            int[] scores = { 9, 8, 7 };
            List<String> ids = getInfo.TblGetIDString(collector.sort_remove_select_gen());
            List<String> AniNames = new List<String>();

            String searchUrl = "https://myanimelist.net/anime.php?q=" + "&genre%5B%5D=" + ids[0]
                + "&genre%5B%5D=" + ids[1] + "&genre%5B%5D=" + ids[2] + "&score=";

            foreach (int rank in scores)
            {
                /* Use available html to acces a site and extract some info */
                String nextSite = searchUrl + rank;
                HtmlWeb httpAccess = new HtmlWeb();
                var html = httpAccess.Load(nextSite);
                var nodes = html.DocumentNode.SelectNodes("//tr");
                nodes.RemoveAt(0);

                AniNames = AniNames.Concat(parseNode(nodes)).ToList();
            }
        }

        private List<String> parseNode(HtmlNodeCollection nodes)
        {
            List<String> trsfmNodes = new List<String>();
            foreach (HtmlNode node in nodes)
            {
                trsfmNodes.Add(node.InnerText);
            }
            return trsfmNodes;
        }

        private void extractNodes(HtmlNodeCollection nodes)
        {
            /* Implement data collection and input it into a parser to put it into a table */
            /* The second window will access the table and recommend anime */
            String aniName = nodes[1].SelectSingleNode("//a").SelectSingleNode("//strong").InnerText;
            String score;

            String info;
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
