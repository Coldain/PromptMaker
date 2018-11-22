using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for SubSubDirectories.xaml
    /// </summary>
    public partial class SubSubDirectories : Window
    {
        SubDirectory currentSubDirectory;
        public SubSubDirectories(SubDirectory _currentSubDirectory)
        {
            currentSubDirectory = _currentSubDirectory;
            this.DataContext = currentSubDirectory;
            InitializeComponent();
        }


        private void ButtonPlus_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<SubDirectory> SampleSub = new ObservableCollection<SubDirectory>();
            SampleSub.Add(new SubDirectory("Directory 1", 1, null, "Change Text Here"));
            SampleSub.Add(new SubDirectory("Directory 1", 1, null, "Change Text Here"));
            SubDirectory newSubDirectory = new SubDirectory("Change Text Here", (currentSubDirectory.Variations.Count()) % 2, SampleSub, currentSubDirectory.Path);
            currentSubDirectory.Variations.Add(newSubDirectory);
        }

        private void ButtonMinus_Click(object sender, RoutedEventArgs e)
        {
            if (currentSubDirectory.Variations.Count() > 0)
            {
                currentSubDirectory.Variations.RemoveAt(currentSubDirectory.Variations.Count() - 1);
                Button cmd = (Button)sender;
                if (cmd.DataContext is SubDirectory)
                {

                    SubDirectory deleteme = (SubDirectory)cmd.DataContext;
                    currentSubDirectory.Variations.Remove(deleteme);

                }
            }
        }

        private void ButtonUp_Click(object sender, RoutedEventArgs e)
        {
            Button cmd = (Button)sender;
            if (cmd.DataContext is SubDirectory)
            {
                SubDirectory moveUp = (SubDirectory)cmd.DataContext;
                int i = currentSubDirectory.Variations.IndexOf(moveUp);
                if (i > 0)
                    currentSubDirectory.Variations.Move(i, i - 1);
            }
        }

        private void ButtonDown_Click(object sender, RoutedEventArgs e)
        {
            Button cmd = (Button)sender;
            if (cmd.DataContext is SubDirectory)
            {
                SubDirectory moveDown = (SubDirectory)cmd.DataContext;
                int i = currentSubDirectory.Variations.IndexOf(moveDown);
                if (i < currentSubDirectory.Variations.Count() && currentSubDirectory.Variations.Count() != 0)
                    currentSubDirectory.Variations.Move(i, i + 1);
            }
        }
        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
