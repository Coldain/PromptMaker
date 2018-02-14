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
using PromptMaker.Assets.Scripts.Views;
using PromptMaker.Assets.Scripts.Models;

namespace PromptMaker.Assets.Scripts.Views
{
    /// <summary>
    /// Interaction logic for Setting.xaml
    /// </summary>
    public partial class Settings : Page
    {
        Setting currentSetting;
        public Settings()
        {
            currentSetting = new Setting();
            this.DataContext = currentSetting;
            //DataContext = new {
            //    setting = currentSetting,
            //};
            InitializeComponent();
        }

        private void ButtonPlus_Click(object sender, RoutedEventArgs e)
        {
            
            SubDirectory newSubDirectory = new SubDirectory("Change Text Here", (currentSetting.SubDirectories.Count()) % 2);
            currentSetting.SubDirectories.Add(newSubDirectory);
        }

        private void ButtonMinus_Click(object sender, RoutedEventArgs e)
        {
            if (currentSetting.SubDirectories.Count() > 0)
            {
                currentSetting.SubDirectories.RemoveAt(currentSetting.SubDirectories.Count() - 1);
                Button cmd = (Button)sender;
                if (cmd.DataContext is SubDirectory)
                {

                    SubDirectory deleteme = (SubDirectory)cmd.DataContext;
                    currentSetting.SubDirectories.Remove(deleteme);

                }
            }
        }

        private void ButtonUp_Click(object sender, RoutedEventArgs e)
        {
            Button cmd = (Button)sender;
            if (cmd.DataContext is SubDirectory)
            {
                SubDirectory moveUp = (SubDirectory)cmd.DataContext;
                int i = currentSetting.SubDirectories.IndexOf(moveUp);
                if (i > 0)
                    currentSetting.SubDirectories.Move(i, i - 1);
            }
        }

        private void ButtonDown_Click(object sender, RoutedEventArgs e)
        {
            Button cmd = (Button)sender;
            if (cmd.DataContext is SubDirectory)
            {
                SubDirectory moveDown = (SubDirectory)cmd.DataContext;
                int i = currentSetting.SubDirectories.IndexOf(moveDown);
                if (i < currentSetting.SubDirectories.Count())
                currentSetting.SubDirectories.Move(i,i+1);
            }
        }
    }
}
