﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AniHelper.AniClasses
{
    class Error
    {
        private StackPanel MainPanel;
        private StackPanel exceededChoices = new StackPanel();
        private bool errorMessage = false;

        public Error(StackPanel myPanel)
        {
            MainPanel = myPanel;
        }
        
        /* Simple error message, tells you when you've selected too many checkboxes */

        public void error()
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
        }
        void ContinueChoosing_Click(object sender, RoutedEventArgs e)
        {
            MainPanel.Children.Remove(exceededChoices);
            exceededChoices = new StackPanel();
            errorMessage = false;
        }
    }
}
