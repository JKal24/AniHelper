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
using System.Windows.Shapes;

namespace AniHelper
{
    /// <summary>
    /// Interaction logic for SecondWindow.xaml
    /// </summary>
    public partial class SecondWindow : Window
    {
        AniSearch searcher;

        public SecondWindow()
        {
            InitializeComponent();
            searcher = new AniSearch();
        }

        private void updateRecommendation()
        {
            /* Access SQL Table and display the next 3 listed recommendations */
        }
    }
}
