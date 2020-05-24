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

        public async Task getAnimeList(String genre)
        {
            int score = 10;
            String searchUrl = "https://myanimelist.net/anime.php?q=&score=" + score.ToString() + 
                "&genre%5B%5D=" + "insert";
        }

        private String transformSearchExtension(String extension)
        {
            return (String.Join("%20", extension.Split(' ')));
        }

        private String getMainSite(String url)
        {
            Match getSiteInfo = Regex.Match(url, @"https://?myanimelist.net/anime/\d*\/\w*");
            return getSiteInfo.Value;
        }

        public HtmlNodeCollection addName()
        {
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
            namePanel = new StackPanel();
            namePanel.Orientation = Orientation.Horizontal;
            namePanel.HorizontalAlignment = HorizontalAlignment.Center;
            namePanel.Margin = new Thickness(10);
        }
    }
}
