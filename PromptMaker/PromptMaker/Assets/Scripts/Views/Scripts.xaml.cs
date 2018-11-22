using PromptMaker.Assets.Scripts.Models;
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

namespace PromptMaker.Assets.Scripts.Views
{
    /// <summary>
    /// Interaction logic for Script.xaml
    /// </summary>
    public partial class Scripts : Page
    {
        public List<string> usedDirectories = new List<string>();
        public List<Tuple<string, List<string>>> usedVariations = new List<Tuple<string, List<string>>>();
        Script currentScript;
        public Scripts(Script _script)
        {
            currentScript = _script;
            this.DataContext = currentScript;
            InitializeComponent();           
        }

        private void ButtonPlus_Click(object sender, RoutedEventArgs e)
        {
            Button cmd = sender as Button;
            if (cmd.Tag.ToString() == "Directory")
            {
                ObservableCollection<SubDirectory> SampleSub = new ObservableCollection<SubDirectory>();
                SampleSub.Add(new SubDirectory("Directory 1", 1, null, "Change Text Here"));
                SampleSub.Add(new SubDirectory("Directory 1", 1, null, "Change Text Here"));
                SubDirectory newSubDirectory = new SubDirectory("Change Text Here", (currentScript.SubDirectories.Count()) % 2, SampleSub, null);
                currentScript.SubDirectories.Add(newSubDirectory);
            }
            else if (cmd.Tag.ToString() == "Prompt")
            {
                currentScript.Prompts.Add(new Prompt());
            }
        }

        private void ButtonMinus_Click(object sender, RoutedEventArgs e)
        {
            Button cmd = sender as Button;
            if (cmd.Tag.ToString() == "Directory")
            {
                if (currentScript.SubDirectories.Count() > 0)
                {
                    currentScript.SubDirectories.RemoveAt(currentScript.SubDirectories.Count() - 1);
                    if (cmd.DataContext is SubDirectory)
                    {
                        SubDirectory deleteme = (SubDirectory)cmd.DataContext;
                        currentScript.SubDirectories.Remove(deleteme);
                    }
                }
            }
            else if (cmd.Tag.ToString() == "Prompt")
            {
                if (currentScript.Prompts.Count() > 0)
                {
                    currentScript.Prompts.RemoveAt(currentScript.Prompts.Count() - 1);
                    if (cmd.DataContext is SubDirectory)
                    {
                        Prompt deleteme = (Prompt)cmd.DataContext;
                        currentScript.Prompts.Remove(deleteme);
                    }
                }
            }
        }

        private void ButtonUp_Click(object sender, RoutedEventArgs e)
        {
            Button cmd = sender as Button;
            if (cmd.Tag.ToString() == "Directory")
            {
                if (cmd.DataContext is SubDirectory)
                {
                    SubDirectory moveUp = (SubDirectory)cmd.DataContext;
                    int i = currentScript.SubDirectories.IndexOf(moveUp);
                    if (i > 0)
                        currentScript.SubDirectories.Move(i, i - 1);
                }
            }
            else if (cmd.Tag.ToString() == "Prompt")
            {
                Prompt moveUp = (Prompt)cmd.DataContext;
                int i = currentScript.Prompts.IndexOf(moveUp);
                if (i > 0)
                    currentScript.Prompts.Move(i, i - 1);
            }
        }

        private void ButtonDown_Click(object sender, RoutedEventArgs e)
        {
            Button cmd = sender as Button;
            if (cmd.Tag.ToString() == "Directory")
            {
                if (cmd.DataContext is SubDirectory)
                {
                    SubDirectory moveDown = (SubDirectory)cmd.DataContext;
                    int i = currentScript.SubDirectories.IndexOf(moveDown);
                    if (i < currentScript.SubDirectories.Count() && currentScript.SubDirectories.Count() != 0)
                        currentScript.SubDirectories.Move(i, i + 1);
                }
            }
            else if (cmd.Tag.ToString() == "Prompt")
            {
                if (cmd.DataContext is Prompt)
                {
                    Prompt moveDown = (Prompt)cmd.DataContext;
                    int i = currentScript.Prompts.IndexOf(moveDown);
                    if (i < currentScript.Prompts.Count() && currentScript.Prompts.Count() != 0)
                        currentScript.Prompts.Move(i, i + 1);
                }
            }
        }

        private void ButtonMore_Click(object sender, RoutedEventArgs e)
        {
            Button cmd = (Button)sender;
            if (cmd.Tag.ToString() == "Directory")
            {
                if (cmd.DataContext is SubDirectory)
                {
                    SubDirectory currentSubDirectory = (SubDirectory)cmd.DataContext;
                    SubSubDirectories popUp = new SubSubDirectories(currentSubDirectory);
                    //popUp.Owner = this.Parent;
                    //popUp.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    popUp.ShowDialog();
                }
            }
            else if (cmd.Tag.ToString() == "Prompt")
            {
                if (cmd.DataContext is Prompt)
                {
                    Prompt currentPrompt = (Prompt)cmd.DataContext;
                    PromptSettings popUp = new PromptSettings(currentPrompt);
                    //popUp.Owner = this.Parent;
                    //popUp.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    popUp.ShowDialog();
                }
            }
        }

        private void ButtonToggleExpand_Click(object sender, RoutedEventArgs e)
        {
            Button cmd = (Button)sender;
            Grid tempGrid = cmd.Parent as Grid;
            StackPanel tempStackPanel = tempGrid.Parent as StackPanel;
            if (cmd.Tag.ToString() == "Directory")
            {
                ListBox tempListBox = tempStackPanel.Children[1] as ListBox;
                if (tempListBox.Visibility == Visibility.Collapsed)
                    tempListBox.Visibility = Visibility.Visible;
                else
                    tempListBox.Visibility = Visibility.Collapsed;
            }
            else if (cmd.Tag.ToString() == "Prompt")
            {
                TextBox tempTextBox = tempGrid.Children[3] as TextBox;
                if (tempTextBox.TextWrapping == TextWrapping.NoWrap)
                {
                    tempTextBox.TextWrapping = TextWrapping.Wrap;
                    tempTextBox.MaxHeight = 22;
                }
                else
                {
                    tempTextBox.TextWrapping = TextWrapping.NoWrap;
                    tempTextBox.ClearValue(MaxHeightProperty);                    
                }
            }
        }

        private void UsageAll_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox tempCheckBox = sender as CheckBox;
            //StackPanel tempStackPanel = tempGrid.Parent as StackPanel;
            //ListBox tempListBox2 = tempStackPanel.Children[1] as ListBox;
            //UIElementCollection tempUIElementCollection = tempStackPanel.Children as UIElementCollection;
            //ListBox tempListBox = tempUIElementCollection[1] as ListBox;
            //CheckBox checkBox = tempListBox2.SelectedItem as CheckBox; 
            //foreach(var tempLBItem in tempListBox.Items)
            //{
            //    CheckBox tempCheckBox2 = tempLBItem as CheckBox;
            //    Usage_Checked(tempCheckBox2, null);
            //}
            if (tempCheckBox.Tag.ToString() == "Directory")
            {
                SubDirectory tempSubDirectory = tempCheckBox.DataContext as SubDirectory;
                if ((bool)tempCheckBox.IsChecked)
                {
                    if (!usedDirectories.Contains(tempSubDirectory.Path))
                    {
                        usedDirectories.Add(tempSubDirectory.Path);
                    }
                }
                else
                    usedDirectories.Remove(tempSubDirectory.Path);
            }
            else if (tempCheckBox.Tag.ToString() == "Prompt")
            {
                SubDirectory tempPrompt = tempCheckBox.DataContext as SubDirectory;
                if ((bool)tempCheckBox.IsChecked)
                {
                    bool x = false;
                    if (usedVariations.Count != 0)
                    {
                        Tuple<string, List<string>> tupleduple = new Tuple<string, List<string>>(null, null);
                        foreach (Tuple<string, List<string>> tempTuple in usedVariations.ToList())

                            if (tempTuple.Item1 == tempPrompt.Owner)
                            {
                                x = true;
                                tupleduple = tempTuple;
                            }
                        if (x)
                        {
                            tupleduple.Item2.Add(tempPrompt.Path);
                        }
                        else
                        {
                            List<string> tempList = new List<string>();
                            tempList.Add(tempPrompt.Path);
                            Tuple<string, List<string>> newTuple = new Tuple<string, List<string>>(tempPrompt.Owner, tempList);
                            usedVariations.Add(newTuple);
                        }

                    }
                    else
                    {
                        List<string> tempList = new List<string>();
                        tempList.Add(tempPrompt.Path);
                        Tuple<string, List<string>> newTuple = new Tuple<string, List<string>>(tempPrompt.Owner, tempList);
                        usedVariations.Add(newTuple);
                    }
                }
                else
                {
                    foreach (Tuple<string, List<string>> tempTuple in usedVariations)
                        if (tempTuple.Item1 == tempPrompt.Owner)
                        {
                            tempTuple.Item2.Remove(tempPrompt.Path);
                        }
                }
            }
        }
        private void Usage_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox tempCheckBox = sender as CheckBox;
            if (tempCheckBox.Tag.ToString() == "Directory")
            {
                SubDirectory tempSubDirectory = tempCheckBox.DataContext as SubDirectory;
                if ((bool)tempCheckBox.IsChecked)
                {
                    if (!usedDirectories.Contains(tempSubDirectory.Path))
                    {
                        usedDirectories.Add(tempSubDirectory.Path);
                    }
                }
                else
                    usedDirectories.Remove(tempSubDirectory.Path);
            }
            else if (tempCheckBox.Tag.ToString() == "Prompt")
            {
                SubDirectory tempPrompt = tempCheckBox.DataContext as SubDirectory;
                if ((bool)tempCheckBox.IsChecked)
                {
                    bool x = false;
                    if (usedVariations.Count != 0)
                    {
                        Tuple<string, List<string>> tupleduple = new Tuple<string, List<string>>(null,null);
                        foreach (Tuple<string, List<string>> tempTuple in usedVariations.ToList())

                            if (tempTuple.Item1 == tempPrompt.Owner)
                            {
                                x = true;
                                tupleduple = tempTuple;
                            }
                        if (x)
                        {
                            if (!tupleduple.Item2.Contains(tempPrompt.Path))
                            {
                                tupleduple.Item2.Add(tempPrompt.Path);
                            }                         
                        }
                        else
                        {
                            List<string> tempList = new List<string>();
                            tempList.Add(tempPrompt.Path);
                            Tuple<string, List<string>> newTuple = new Tuple<string, List<string>>(tempPrompt.Owner, tempList);
                            usedVariations.Add(newTuple);
                        }

                    }
                    else
                    {
                        List<string> tempList = new List<string>();
                        tempList.Add(tempPrompt.Path);
                        Tuple<string, List<string>> newTuple = new Tuple<string, List<string>>(tempPrompt.Owner, tempList);
                        usedVariations.Add(newTuple);
                    }
                }
                else
                {
                    foreach (Tuple<string, List<string>> tempTuple in usedVariations)
                        if (tempTuple.Item1 == tempPrompt.Owner)
                        {
                            while (tempTuple.Item2.Contains(tempPrompt.Path))
                            {
                                tempTuple.Item2.Remove(tempPrompt.Path);
                            }                            
                        }
                }
            }
        }    
    }
}

