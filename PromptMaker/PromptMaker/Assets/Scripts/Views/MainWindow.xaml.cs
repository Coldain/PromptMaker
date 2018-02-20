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
using PromptMaker.Assets.Scripts.Models;
using System.Collections.ObjectModel;

namespace PromptMaker.Assets.Scripts.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int x = 0;
        public ObservableCollection<Page> pages;
        Setting theSetting;
        public MainWindow()
        {
            // Initialize Main Window and add setting page
            this.DataContext = pages;
            InitializeComponent();
            ButtonAddSetting_Click(null, null);
        }
        // Insert Tab with "Script #"
        private void ButtonInsertScript_Click(object sender, RoutedEventArgs e)
        {
            int i = tabController.SelectedIndex;
            x = 1;
            ButtonAddScript_Click(sender, e);
        }
        // Delete current Tab (only scripts, not Settings or Add Button
        private void ButtonDeleteScript_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Close Application?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                TabItem tabitem = this.Parent as TabItem;
                if (tabController.Items.IndexOf(tabitem) != 0 && tabController.Items.IndexOf(tabitem) != tabController.Items.Count)
                    tabController.Items.Remove(tabitem);

                if (tabController.SelectedIndex > 0 && tabController.SelectedIndex != tabController.Items.Count)
                {
                    tabController.Items.RemoveAt(tabController.SelectedIndex);
                    //int i = pages.IndexOf((Page)mainFrame.Content);
                    //ButtonPreviousPage_Click(sender, e);
                    //pages.RemoveAt(i);
                    //Button cmd = (Button)sender;
                    //if (cmd.DataContext is Scripts)
                    //{
                    //    Scripts deleteme = (Scripts)cmd.DataContext;
                    //    pages.Remove(deleteme);
                    //}
                }
            }
        }
        // Switch current Tab to the next tab in the list, cycle if last.
        private void ButtonNextPage_Click(object sender, RoutedEventArgs e)
        {
            if (tabController.SelectedIndex != tabController.Items.Count - 2)
            {
                TabItem tabItem = tabController.Items[tabController.SelectedIndex + 1] as TabItem;
                tabItem.IsSelected = true;
            }
            else
            {
                TabItem tabItem = tabController.Items[0] as TabItem;
                tabItem.IsSelected = true;
            }
            
            //int i = pages.IndexOf((Page)mainFrame.Content);
            //if (i < pages.Count() - 1)
            //    mainFrame.Content = pages[i + 1];
        }
        // Switch current Tab to the previous tab in the list, cycle if first.
        private void ButtonPreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if (tabController.SelectedIndex != 0)
            {
                TabItem tabItem = tabController.Items[tabController.SelectedIndex - 1] as TabItem;
                tabItem.IsSelected = true;
            }
            else
            {
                TabItem tabItem = tabController.Items[tabController.Items.Count - 2] as TabItem;
                tabItem.IsSelected = true;
            }
            //int i = pages.IndexOf((Page)mainFrame.Content);
            //    if (i > 0)
            //        mainFrame.Content = pages[i - 1];            
        }
       
        //private void ButtonSelectPage_Click(object sender, RoutedEventArgs e)
        //{
        //    Button cmd = (Button)sender;
        //    if (cmd.DataContext is Scripts || cmd.DataContext is Settings)
        //    {
        //        Page gotoPage = (Page)cmd.DataContext;
        //        mainFrame.Content = pages.IndexOf(gotoPage);
        //    }
        //}

        // Add new script to the second to last tab (before the add button)
        private void ButtonAddScript_Click(object sender, RoutedEventArgs e)
        {
            Script newScriptData = new Script(theSetting);
            newScriptData.ScriptName = "Script " + (tabController.Items.Count - 2);
            Scripts newScript = new Scripts(newScriptData);
            ButtonAdd_Click((Page)newScript, sender, e, 1);
        }
        // Add Setting Tab to the first position, only needed at the start.
        private void ButtonAddSetting_Click(object sender, RoutedEventArgs e)
        {
            theSetting = new Setting();
            Settings newSetting = new Settings(theSetting);
            newSetting.Title = ("Settings");
            ButtonAdd_Click((Page)newSetting, sender, e, 0);
        }
        // Inserts new Tab and initializes the Header and the Tab Content
        private void ButtonAdd_Click(Page page, object sender, RoutedEventArgs e, int i)
        {
            Tabs newTab = new Tabs(page, tabController, i);
            Frame newFrame = new Frame()
            {
                NavigationUIVisibility = NavigationUIVisibility.Hidden,
                Content = page
            };
            TabItem tempTabItem = new TabItem
            {
                Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF444444")),
                Header = newTab
            };
            tempTabItem.Content = newFrame;
            if (i == 0)
                tabController.Items.Insert(0, tempTabItem);
            else if (x == 1)
            {
                tabController.Items.Insert(tabController.SelectedIndex + 1, tempTabItem);
                x = 0;
            }
            else
                tabController.Items.Insert(tabController.Items.Count - 1, tempTabItem);

        }        
    }
}
