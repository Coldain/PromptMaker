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
using PromptMaker.Assets.Scripts.Models;

namespace PromptMaker.Assets.Scripts.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Settings frameSettings = new Settings();

            InitializeComponent();

            mainFrame.Content = frameSettings;
        }

        private void ButtonAddScript_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonDeleteScript_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonNextPage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonPreviousPage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonSelectPage_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
