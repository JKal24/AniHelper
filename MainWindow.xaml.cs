using AniHelper.AniClasses;
using System;
using System.Windows;
using System.Windows.Controls;

namespace AniHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Design elements = new Design();

        public MainWindow()
        {
            InitializeComponent();

            elements.setMainPanel(MainPanel);

            elements.addGenreButtons(genreGrid);

            elements.addSearchFunction();
        }
    }
}
