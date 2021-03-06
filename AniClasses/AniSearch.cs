﻿using Dapper;
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

        public event EventHandler<Window> swapScreen;
        public GenreCollect collector = new GenreCollect();
        private Parser getInfo = new Parser();
        private List<String> watchedAnime = new List<String>();

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

            /* Reset the table in case information is present from a previous attempt */

            getInfo.resetRecommendationTable();

            HtmlNodeCollection nodes = getSearchNode(ids);

            /* specifies a limit on how many recommendations you want to give */

            int limit = 15;

            if (nodes == null)
            {
                extract_seperateNodes(ids, limit / 3);
            } else
            {
                nodes.RemoveAt(0);

                extractNodes(nodes, limit);

                if (nodes.Count < limit)
                {
                    limit -= nodes.Count;
                    extract_seperateNodes(ids, (int)Math.Round((double)limit / 3));
                }
            }

            swapScreen?.Invoke(this, new SecondWindow());
        }

        private void extract_seperateNodes(List<String> ids, int limit)
        {
            List<String>[] setOfIds = seperateSearch(ids);

            foreach (List<String> indivIds in setOfIds)
            {
                HtmlNodeCollection collectNodes = getSearchNode(indivIds);
                if (collectNodes == null)
                {
                    continue;
                }

                collectNodes.RemoveAt(0);

                extractNodes(collectNodes, limit);
            }
        }

        private int extractNodes(HtmlNodeCollection nodes, int limit)
        {
            /* Implement data collection and input it into a parser to put it into a table */
            /* The second window will access the table and recommend anime */

            foreach (var node in nodes)
            {
                if (limit <= 0)
                {
                    break;
                }
                String url = getMainPrequel(node.SelectSingleNode
                    ("./td/div[@class='picSurround']/a[@href]").GetAttributeValue("href", string.Empty));
                var html = getHTMLDoc(url);

                String aniName = html.DocumentNode.SelectSingleNode("//span[@itemprop='name']//text()").InnerText;

                if (watchedAnime.Contains(aniName))
                {
                    continue;
                }
                watchedAnime.Add(aniName);

                string score;
                var scoreData = html.DocumentNode.SelectSingleNode("//span[@itemprop='ratingValue']");
                if (scoreData != null)
                {
                    score = scoreData.InnerText;
                } else
                {
                    score = "0";
                }

                String info = html.DocumentNode.SelectSingleNode("//span[@itemprop='description']").InnerText;

                /* inputs data into table */

                getInfo.inputAnime(new string[] { aniName, score, info, url });

                limit--;
            }
            return limit;
        }

        private String getMainPrequel(String url)
        {
            var html = getHTMLDoc(url);
            HtmlNode prequelNode = html.DocumentNode.SelectSingleNode("//td[contains(text(),'Prequel')]");
            if (prequelNode != null)
            {
                return getMainPrequel("https://myanimelist.net/" + prequelNode.Ancestors().FirstOrDefault().
                    SelectSingleNode("./td/a[@href]").GetAttributeValue("href", string.Empty));
            }
            return url;
        }

        private List<String>[] seperateSearch(List<String> ids)
        {
            return new List<string>[] { new List<String> { ids[0], ids[1] }, new List<String> { ids[0], ids[2] },
            new List<String> { ids[1], ids[2] }};
        }

        private HtmlNodeCollection getSearchNode(List<String> ids)
        {
            String searchUrl = "https://myanimelist.net/anime.php?q=";
            searchUrl += addSearchExtension(ids);

            /* Use available html to acces a site and extract some info */

            var html = getHTMLDoc(searchUrl);
            return html.DocumentNode.SelectNodes("//div[@class='js-categories-seasonal js-block-list list']" +
                "/table/tr");
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

            var html = getHTMLDoc(currentUrl);
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

        private HtmlDocument getHTMLDoc(String url)
        {
            HtmlWeb httpAccess = new HtmlWeb();
            return httpAccess.Load(url);
        }

        private String checkScore(string score)
        {
            if (score == null)
            {
                return "N/A";
            }
            return score;
        }
    }
}
