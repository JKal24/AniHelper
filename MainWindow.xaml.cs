using AniHelper.AniClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AniHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private GenreCollect collector = new GenreCollect();
        private StackPanel exceededChoices = new StackPanel();
        private bool errorMessage = false;

        public MainWindow()
        {
            InitializeComponent();

            addGenreButtons(collector.get_available_genres());
        }

        private void addGenreButtons(String[] buttonNames)
        {
            /* initialize starting screen with the check buttons to choose from */
            int[] row_column = { 0, 0 };

            foreach (String button in buttonNames)
            {
                CheckBox box = new CheckBox();

                box.Name = button;
                box.Content = button;
                box.Width = 140;
                box.Click += Box_Click;

                genreGrid.Children.Add(box);
                Grid.SetRow(box, row_column[0]);
                Grid.SetColumn(box, row_column[1]);
                row_column = grid_next_position(row_column);
            }

            Button nextSection = new Button();
            nextSection.Content = "Continue";
            nextSection.Click += NextPart_Click;

            MainPanel.Children.Add(nextSection);
        }

        private void Box_Click(object sender, RoutedEventArgs e)
        {
            CheckBox box = (CheckBox)sender;

            if (collector.get_selected_genres_length() >= 5)
            {
                if ((box.IsChecked == true))
                {
                    /* gives error message when first triggered */
                    if (errorMessage)
                    {
                        box.IsChecked = false;
                        return;
                    }
                    else
                    {
                        TextBlock message = new TextBlock();
                        message.Text = "Too many genres selected.";
                        exceededChoices.Children.Add(message);

                        Button continueChoosing = new Button();
                        continueChoosing.Content = "Ok";
                        continueChoosing.Click += ContinueChoosing_Click;
                        exceededChoices.Children.Add(continueChoosing);

                        exceededChoices.HorizontalAlignment = HorizontalAlignment.Center;
                        exceededChoices.VerticalAlignment = VerticalAlignment.Bottom;
                        MainPanel.Children.Add(exceededChoices);

                        errorMessage = true;
                        return;
                    }
                } else
                {
                    /* continue implementing removal of a check, in the case of 5 present 
                     * checked checkboxes */
                    errorMessage = false;
                }

            if ((bool)box.IsChecked)
            {
                collector.add_genre(box.Content.ToString());
            } else
            {
                collector.remove_genre(box.Content.ToString());
            }
        }

        private void ContinueChoosing_Click(object sender, RoutedEventArgs e)
        {
            MainPanel.Children.Remove(exceededChoices);
            exceededChoices = new StackPanel();
        }

        private void NextPart_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private int[] grid_next_position(int[] r_c)
        {
            if (r_c[1] >= 4)
            {
                r_c[1] = 0;
                r_c[0] += 1;
            } else
            {
                r_c[1] += 1;
            }
            return r_c;
        }
    }
}
