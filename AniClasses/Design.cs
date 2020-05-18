using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AniHelper.AniClasses
{
    class Design
    {
        public GenreCollect collector = new GenreCollect();
        public GenreError errorOverflow = new GenreError();

        private StackPanel MainPanel;
        private TextBox input = new TextBox();

        private StackPanel exceededChoices = new StackPanel();
        private bool errorMessage = false;

        /* set a direct link to the main window's panel */
        public void setMainPanel(StackPanel setPanel)
        {
            MainPanel = setPanel;
        }

        /* add search functionality that works with AniSearch and gets info from the main window */
        public void addSearchFunction()
        {
            TextBlock info = new TextBlock();
            Button inputButton = new Button();

            info.Text = "List up to 5 anime that you've watched";
            inputButton.Content = "Enter";

            info.FontSize = 18;
            info.Width = 320;

            input.Margin = new Thickness(10);
            input.Width = 200;

            inputButton.Width = 125;
            inputButton.Click += InputButton_Click;

            MainPanel.Children.Add(info);
            MainPanel.Children.Add(input);
            MainPanel.Children.Add(inputButton);
        }
        private void InputButton_Click(object sender, RoutedEventArgs e)
        {
            if (input.Text != "")
            {
                return;
            }

            collector.conductSearch(input);
        }

        public void addGenreButtons(Grid genreGrid)
        {
            /* initialize starting screen with the check buttons to choose from */
            int[] row_column = { 0, 0 };

            foreach (String button in collector.get_available_genres())
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
        }

        private void Box_Click(object sender, RoutedEventArgs e)
        {
            CheckBox box = (CheckBox)sender;

            if ((bool)box.IsChecked)
            {
                if (collector.get_selected_genres_length() >= 5)
                {
                    if (!errorMessage)
                    {
                        /* make a message stating that too many genres were selected */
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
                    }

                    box.IsChecked = false;
                    return;
                }
                collector.add_genre(box.Content.ToString());
            }
            else
            {
                collector.remove_genre(box.Content.ToString());
            }

        }
        void ContinueChoosing_Click(object sender, RoutedEventArgs e)
        {
            MainPanel.Children.Remove(exceededChoices);
            exceededChoices = new StackPanel();
            errorMessage = false;
        }

        private int[] grid_next_position(int[] r_c)
        {
            if (r_c[1] >= 4)
            {
                r_c[1] = 0;
                r_c[0] += 1;
            }
            else
            {
                r_c[1] += 1;
            }
            return r_c;
        }
    }
}
