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
        public MainWindow()
        {
            InitializeComponent();

            GenreCollect collector = new GenreCollect();

            collector.getData();

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

                genreGrid.Children.Add(box);
                Grid.SetRow(box, row_column[0]);
                Grid.SetColumn(box, row_column[1]);
                row_column = grid_next_position(row_column);
            }
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
