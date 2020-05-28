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
        private List<String> watchedAnime = new List<String>();

        public List<String[]> recommendedAnime { get; set; }

        public AniSearch()
        {
            recommendedAnime = new List<String[]>();
        }

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

            List<String> ids = getInfo.TblGetIDString(collector.sort_remove_select_gen());

            String searchUrl = "https://myanimelist.net/anime.php?q=";
            searchUrl += addSearchExtension(ids);

            /* Use available html to acces a site and extract some info */
            HtmlWeb httpAccess = new HtmlWeb();
            var html = httpAccess.Load(searchUrl);
            var nodes = html.DocumentNode.SelectNodes("//div[@class='js-categories-seasonal js-block-list list']" +
                "/table/tr");
            
            nodes.RemoveAt(0);

            extractNodes(nodes);
        }

        private void extractNodes(HtmlNodeCollection nodes)
        {
            /* Reset the table in case information is present from a previous attempt */

            getInfo.resetRecommendationTable();

            /* Implement data collection and input it into a parser to put it into a table */
            /* The second window will access the table and recommend anime */
            foreach (var node in nodes)
            {
                String aniName = node.SelectSingleNode("./td/a/strong").InnerText;

                if (watchedAnime.Contains(aniName))
                {
                    break;
                }
                String score = node.ChildNodes[4].InnerText;
                String info = node.SelectSingleNode("./td/div[@class='pt4']").InnerText;

                /* inputs data into table */
                getInfo.inputAnime(new string[] { aniName, score, info });
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
            watchedAnime.Add(node.InnerText);

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

        private String addSearchExtension(List<String> ids)
        {
            String extension = "";
            foreach (String id in ids)
            {
                extension += "&genre%5B%5D=";
                extension += id;
            }
            return (extension += "&o=3&w=1");
        }
    }
}
