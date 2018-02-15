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
using System.Collections.ObjectModel;

namespace PromptMaker.Assets.Scripts.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Page> pages;
        public MainWindow()
        {
            Settings frameSettings = new Settings();
            pages = new ObservableCollection<Page>();
            pages.Add(frameSettings);
            this.DataContext = pages;
            InitializeComponent();
            mainFrame.Content = frameSettings;
            //tStack.ItemsSource = pages;
        }

        private void ButtonAddScript_Click(object sender, RoutedEventArgs e)
        {
            Script newScriptData = new Script();
            Scripts newScript = new Scripts(newScriptData);
            newScript.Name=("Script" + pages.Count());
            pages.Add(newScript);
            mainFrame.Content = newScript;
        }

        private void ButtonInsertScript_Click(object sender, RoutedEventArgs e)
        {
            ButtonAddScript_Click(sender, e);
            pages.Move(pages.IndexOf((Page)mainFrame.Content) + 1, pages.Count());
        }

        private void ButtonDeleteScript_Click(object sender, RoutedEventArgs e)
        {
            if (pages.Count() > 1)
            {
                int i = pages.IndexOf((Page)mainFrame.Content);
                ButtonPreviousPage_Click(sender, e);
                pages.RemoveAt(i);
                Button cmd = (Button)sender;
                if (cmd.DataContext is Scripts)
                {
                    Scripts deleteme = (Scripts)cmd.DataContext;
                    pages.Remove(deleteme);
                }
            }
        }

        private void ButtonNextPage_Click(object sender, RoutedEventArgs e)
        {
            int i = pages.IndexOf((Page)mainFrame.Content);
            if (i < pages.Count() - 1)
                mainFrame.Content = pages[i + 1];
        }

        private void ButtonPreviousPage_Click(object sender, RoutedEventArgs e)
        {
            int i = pages.IndexOf((Page)mainFrame.Content);
                if (i > 0)
                    mainFrame.Content = pages[i - 1];            
        }

        private void ButtonSelectPage_Click(object sender, RoutedEventArgs e)
        {
            Button cmd = (Button)sender;
            if (cmd.DataContext is Scripts || cmd.DataContext is Settings)
            {
                Page gotoPage = (Page)cmd.DataContext;
                mainFrame.Content = pages.IndexOf(gotoPage);
            }
        }
    }
}
