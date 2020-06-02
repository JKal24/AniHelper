using AniHelper.AniClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
        int index;

        public SecondWindow()
        {
            InitializeComponent();
            parse = new Parser();
            results = parse.getRecommendationTbl();
            this.myLimit = results.Count;

            this.SizeToContent = SizeToContent.WidthAndHeight;
            this.MaxHeight = 1500;
            this.MaxWidth = 2000;
            designWindow();
        }

        public int myLimit { get; set; }

        public Button displayMore { get; set; }

        private void designWindow(int start = 0)
        {
            makeHeader();

            /* Get a button that parses through the available recommendations */

            displayMore = new Button();
            editButton();
            displayMore.Margin = new Thickness(15);
            Grid.SetColumn(displayMore, 1);
            Grid.SetRow(displayMore, 2);
            mainGrid.Children.Add(displayMore);

            index = createRecommendation(start);
        }

        private void editButton()
        {
            displayMore.Content = "More Recommendations";
            displayMore.Click += DisplayMore_Click;
        }


        private void DisplayMore_Click(object sender, RoutedEventArgs e)
        {
            updateRecommendation();
        }

        private int createRecommendation(int index, int numListing = 3)
        {
            /* Access SQL Table and display the next 3 listed recommendations */

            numListing = Math.Min(numListing, myLimit - index);

            for (int parse = index; parse < index + numListing; parse++)
            {
                createIndividualRecommendation(results[parse], parse - index);
                if (repeatRecommendation(parse - index))
                {
                    return 0;
                }
            }

            return index + 3;
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

            nameAndScore.Text = result.Name + " Score: " + result.Score.ToString();
            nameAndScore.Margin = new Thickness(10);
            nameAndScore.Background = Brushes.LightBlue;

            description.Text = result.Info;
            description.Margin = new Thickness(10);
            description.Background = Brushes.LightBlue;
            description.TextWrapping = TextWrapping.WrapWithOverflow;

            infoPanel.Children.Add(nameAndScore);
            infoPanel.Children.Add(description);
            Grid.SetRow(infoPanel, 1);
            Grid.SetColumn(infoPanel, position);

            mainGrid.Children.Add(infoPanel);
            this.myLimit--;
        } 

        private void updateRecommendation()
        {
            mainGrid.Children.Clear();

            designWindow(index);
        }

        private bool repeatRecommendation(int position)
        {
            if (myLimit <= position)
            {
                displayMore.Content = "Repeat";
                displayMore.Click += DisplayMore_Click1;
                return true;
            }
            return false;
        }

        private void DisplayMore_Click1(object sender, RoutedEventArgs e)
        {
            editButton();
        }

        private String editWords(String description)
        {
            return "";
        }
    }
}
