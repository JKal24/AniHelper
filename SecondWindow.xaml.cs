using AniHelper.AniClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AniHelper
{
    /// <summary>
    /// Interaction logic for SecondWindow.xaml
    /// </summary>
    public partial class SecondWindow : Window
    {
        Parser parse;
        List<Results> results;

        public SecondWindow()
        {
            InitializeComponent();
            parse = new Parser();
            results = parse.getRecommendationTbl();
            this.myLimit = results.Count;
            this.index = 0;

            this.SizeToContent = SizeToContent.WidthAndHeight;
            this.MaxHeight = 1500;
            this.MaxWidth = 2000;
            makeButton();
            designWindow();
        }

        public int index { get; set; }

        public int myLimit { get; set; }

        public Button displayMore { get; set; }

        private void designWindow()
        {
            /* Makes a title and button to parse through recommendations */

            makeHeader();
            makeButton();

            index = createRecommendation();
        }

        private void makeButton()
        {
            displayMore = new Button();
            displayMore.Content = "More Recommendations";
            displayMore.Click += DisplayMore_Click;
            displayMore.Margin = new Thickness(15);
            Grid.SetColumn(displayMore, 1);
            Grid.SetRow(displayMore, 2);
            mainGrid.Children.Add(displayMore);
        }

        private void DisplayMore_Click(object sender, RoutedEventArgs e)
        {
            updateRecommendation();
        }

        private int createRecommendation(int numListing = 3)
        {
            /* Access SQL Table and display the next 3 listed recommendations */

            numListing = Math.Min(numListing, myLimit - index);

            for (int parse = index; parse < index + numListing; parse++)
            {
                createIndividualRecommendation(results[parse], parse - index);
            }
            index += numListing;
            if (repeatRecommendation())
            {
                return 0;
            }
            return index;
        }

        private void updateRecommendation()
        {
            mainGrid.Children.Clear();

            designWindow();
        }

        private void makeHeader()
        {
            TextBlock title = new TextBlock();
            title.Text = "Anime Recommendations";
            title.Background = Brushes.LightBlue;
            title.HorizontalAlignment = HorizontalAlignment.Center;
            title.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetRow(title, 0);
            Grid.SetColumnSpan(title, 3);

            mainGrid.Children.Add(title);
        }

        private void createIndividualRecommendation(Results result, int position)
        {
            StackPanel infoPanel = new StackPanel();

            TextBlock nameAndScore = new TextBlock();
            TextBlock description = new TextBlock();

            nameAndScore.Text = editWords(result.Name) + " Score: " + result.Score.ToString();
            nameAndScore.Margin = new Thickness(10);
            nameAndScore.Background = Brushes.LightBlue;

            description.Text = editWords(result.Info);
            description.Margin = new Thickness(10);
            description.Background = Brushes.LightBlue;
            description.TextWrapping = TextWrapping.WrapWithOverflow;

            infoPanel.Children.Add(nameAndScore);
            infoPanel.Children.Add(description);
            Grid.SetRow(infoPanel, 1);
            Grid.SetColumn(infoPanel, position);

            mainGrid.Children.Add(infoPanel);
        } 

        private bool repeatRecommendation()
        {
            if (myLimit <= index)
            {
                displayMore.Content = "Repeat";
                return true;
            }
            return false;
        }

        private String editWords(String description)
        {
            description = Regex.Replace(description, @"&#039;", "'");
            description = Regex.Replace(description, @"&quot;", "\"");
            description = Regex.Replace(description, @"&mdash;", "-");
            description = Regex.Replace(description, @"&amp;", "&");
            return description;
        }
    }
}
