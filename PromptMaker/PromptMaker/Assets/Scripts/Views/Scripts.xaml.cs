using PromptMaker.Assets.Scripts.Models;
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

namespace PromptMaker.Assets.Scripts.Views
{
    /// <summary>
    /// Interaction logic for Script.xaml
    /// </summary>
    public partial class Scripts : Page
    {
        Script currentScript;
        public Scripts(Script _script)
        {
            currentScript = new Script();
            this.DataContext = currentScript;
            InitializeComponent();
        }

        private void ButtonPlus_Click(object sender, RoutedEventArgs e)
        {

            SubDirectory newSubDirectory = new SubDirectory("Change Text Here", (currentScript.Prompts.Count()) % 2);
            currentScript.Prompts.Add(newSubDirectory);
        }

        private void ButtonMinus_Click(object sender, RoutedEventArgs e)
        {
            if (currentScript.Prompts.Count() > 0)
            {
                currentScript.Prompts.RemoveAt(currentScript.Prompts.Count() - 1);
                Button cmd = (Button)sender;
                if (cmd.DataContext is SubDirectory)
                {

                    SubDirectory deleteme = (SubDirectory)cmd.DataContext;
                    currentScript.Prompts.Remove(deleteme);

                }
            }
        }

        private void ButtonUp_Click(object sender, RoutedEventArgs e)
        {
            Button cmd = (Button)sender;
            if (cmd.DataContext is SubDirectory)
            {
                SubDirectory moveUp = (SubDirectory)cmd.DataContext;
                int i = currentScript.Prompts.IndexOf(moveUp);
                if (i > 0)
                    currentScript.Prompts.Move(i, i - 1);
            }
        }

        private void ButtonDown_Click(object sender, RoutedEventArgs e)
        {
            Button cmd = (Button)sender;
            if (cmd.DataContext is SubDirectory)
            {
                SubDirectory moveDown = (SubDirectory)cmd.DataContext;
                int i = currentScript.Prompts.IndexOf(moveDown);
                if (i < currentScript.Prompts.Count())
                    currentScript.Prompts.Move(i, i + 1);
            }
        }
    }
}
