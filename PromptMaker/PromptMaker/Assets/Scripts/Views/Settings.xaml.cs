﻿using System;
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
using PromptMaker.Assets.Scripts.Views;
using PromptMaker.Assets.Scripts.Models;

namespace PromptMaker.Assets.Scripts.Views
{
    /// <summary>
    /// Interaction logic for Setting.xaml
    /// </summary>
    public partial class Settings : Page
    {
        public Settings()
        {
            Setting currentSetting = new Setting();
            this.DataContext = currentSetting;
            //DataContext = new {
            //    setting = currentSetting,
            //};
            InitializeComponent();
        }

        private void Button+_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
