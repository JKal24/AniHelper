using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AniHelper.AniClasses
{
    public class Design
    {
        private AniSearch searcher = new AniSearch();
        private Error errorOverflow;

        private StackPanel MainPanel;
        private TextBox input = new TextBox();

        public void assignPanel(StackPanel mainPanel)
        {
            this.MainPanel = mainPanel;
            errorOverflow = new Error(MainPanel);
        }

        /* add search functionality that works with AniSearch and gets info from the main window */
        public void addSearchFunction(StackPanel functions)
        {
            TextBlock info = new TextBlock();
            Button inputButton = new Button();

            info.Text = "List up to 3 anime that you've watched";
            inputButton.Content = "Enter";

            info.FontSize = 18;
            info.Width = 320;

            input.Margin = new Thickness(10);
            input.Width = 200;

            inputButton.Width = 125;
            inputButton.Margin = new Thickness(10);
            inputButton.Click += InputButton_ClickAsync;
            inputButton.KeyDown += InputButton_KeyDown;

            functions.Children.Add(info);
            functions.Children.Add(input);
            functions.Children.Add(inputButton);

            makeNameContainer();
        }

        private void InputButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                InputButton_ClickAsync(sender, e);
            }
        }

        private async void InputButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            if (input.Text == "" || searcher.complete == false || 
                searcher.namePanel.Children.Count >= 3)
            {
                return;
            }

            searcher.complete = false;
            await searcher.getSearchData(input.Text);
            searcher.collector.add_named_genres(searcher.addName());
        }

        public void addGenreButtons(Grid genreGrid)
        {
            /* initialize starting screen with the check buttons to choose from */

            int[] row_column = { 0, 0 };

            foreach (String button in searcher.collector.get_available_genres())
            {
                String buttonName = Regex.Replace(button, " |-", "");
                CheckBox box = new CheckBox();

                box.Name = buttonName;
                box.Content = buttonName;
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
                if (searcher.collector.get_checkbox_selected_genres_length() >= 3)                  
                {
                    errorOverflow.error();

                    box.IsChecked = false;
                    return;
                }
                searcher.collector.add_genre(box.Content.ToString());
                searcher.collector.checkedBoxes++;
            }
            else
            {
                searcher.collector.remove_genre(box.Content.ToString());
                searcher.collector.checkedBoxes--;
            }

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

        private void makeNameContainer()
        {
            searcher.init_namepanel();
            MainPanel.Children.Add(searcher.namePanel);
        }

        public void makeSubmitButton(StackPanel submitBox)
        {
            Button submit = new Button();
            submit.Content = "Submit";
            submit.Width = 70;
            submit.Margin = new Thickness(10);
            submit.Click += Submit_Click;
            submitBox.Children.Add(submit);
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            /* Proceed to second window */

            MainPanel.Visibility = Visibility.Collapsed;

            SecondWindow results = new SecondWindow();

            searcher.getAnimeList();
        }

        private void addLoadingBox()
        {
            Grid loadingGrid = new Grid();
            /* Instead of using a loading window, just hide the current content and display a message */
        }
    }
}
