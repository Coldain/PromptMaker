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
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections;

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
            int i = tabController.SelectedIndex;
            int x = tabController.Items.Count - 1;
            if (i != 0 && i != x)
            {
                if (MessageBox.Show("Delete Script?", "Please Select:", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    if (i > 0 && i != x)
                    {
                        tabController.Items.RemoveAt(i);
                    }
                    if (i == x)
                        ButtonPreviousPage_Click(sender, e);
                }
            }
            else
                MessageBox.Show("Can't delete the add tab or the settings tab.");
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

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();
            string savePath = folderBrowserDialog.SelectedPath + "\\" + "Record_Prompts_Here.XML";
            // Load in all the XMLs
            XmlDocument ActionsXML = new XmlDocument();
            string pathBase = @"C:\Users\Coldain\Source\Repos\PromptMaker\PromptMaker\PromptMaker\Assets\Scripts\Models\XML Template\";
            string path = System.IO.Path.Combine(pathBase, "Actions.XML");
            //string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Assets\Scripts\Models\XML Template\", "Actions.XML");
            ActionsXML.Load(path);
            XmlDocument RunSubXML = new XmlDocument();
            path = System.IO.Path.Combine(pathBase, "RunSubs.XML");
            RunSubXML.Load(path);
            XmlDocument PlayXML = new XmlDocument();
            path = System.IO.Path.Combine(pathBase, "Plays.XML");
            PlayXML.Load(path);
            XmlDocument MenuXML = new XmlDocument();
            path = System.IO.Path.Combine(pathBase, "Menus.XML");
            MenuXML.Load(path);
            XmlDocument AnnotationXML = new XmlDocument();
            path = System.IO.Path.Combine(pathBase, "Annotations.XML");
            AnnotationXML.Load(path);

            // Actions Details Setup
            ActionsSetting(ActionsXML);
            // Add all the Actions
            AddAction(ActionsXML, RunSubXML);
            PlaySetting(PlayXML, "test ID", "Test Caption", "Test Sequence", "Test Phrase", "Test X Cord", "Test Y Cord");
            AddAction(ActionsXML, PlayXML);

            // Loop through Scripts 
            //PlaySetting(PlayXML);
            ActionsXML.Save(savePath);
        }

        private void ActionsSetting(XmlDocument XML)
        {
            XML.SelectSingleNode("/ScriptContainer").Attributes["BusNo"].Value = theSetting.BusinessNumber;
            XML.SelectSingleNode("/ScriptContainer").Attributes["UserID"].Value = theSetting.UserID;
            XML.SelectSingleNode("/ScriptContainer/LibraryItem").Attributes["BusNo"].Value = theSetting.BusinessNumber;
            XML.SelectSingleNode("/ScriptContainer/LibraryItem/ModifyUserID").InnerText = theSetting.UserID;
            XML.SelectSingleNode("/ScriptContainer/LibraryItem/ModifyUserID").InnerText = "/custom/" + theSetting.BusinessNumber + "/Record_Prompts_Here";
        }

        private void RunSubSetting(XmlDocument XML)
        {

        }
        private void PlaySetting(XmlDocument XML, string ID, string Caption, string Sequence, string Phrase, string xCoord, string yCoord)
        {
            XML.SelectSingleNode("/ActionStruct/ActionID").InnerText = ID;
            XML.SelectSingleNode("/ActionStruct/Caption").InnerText = Caption;
            XML.SelectSingleNode("/ActionStruct/Parameters").FirstChild.InnerText = Sequence;
            XML.SelectSingleNode("/ActionStruct/Parameters").FirstChild.NextSibling.InnerText = Phrase;
            XML.SelectSingleNode("/ActionStruct/X").InnerText = xCoord;
            XML.SelectSingleNode("/ActionStruct/Y").InnerText = yCoord;
        }

        private void MenuSetting(XmlDocument XML, string ID, string Caption, string Sequence, string Phrase, string xCoord, string yCoord)
        {
            XML.SelectSingleNode("/ActionStruct/ActionID").InnerText = ID;
            XML.SelectSingleNode("/ActionStruct/Caption").InnerText = Caption;
            XML.SelectSingleNode("/ActionStruct/Parameters").FirstChild.InnerText = Sequence;
            XML.SelectSingleNode("/ActionStruct/Parameters").FirstChild.NextSibling.InnerText = Phrase;
            XML.SelectSingleNode("/ActionStruct/X").InnerText = xCoord;
            XML.SelectSingleNode("/ActionStruct/Y").InnerText = yCoord;
        }

        private void AnotationSetting(XmlDocument XML, string ID, string ScriptName, string xDeminsion, string yDeminsion, string xCoord, string yCoord)
        {
            XML.SelectSingleNode("/ActionStruct/ActionID").InnerText = ID;
            XML.SelectSingleNode("/ActionStruct/Parameters").FirstChild.InnerText = ScriptName;
            XML.SelectSingleNode("/ActionStruct/Parameters").FirstChild.NextSibling.InnerText = xDeminsion;
            XML.SelectSingleNode("/ActionStruct/Parameters").FirstChild.NextSibling.NextSibling.InnerText = yDeminsion;
            XML.SelectSingleNode("/ActionStruct/X").InnerText = xCoord;
            XML.SelectSingleNode("/ActionStruct/Y").InnerText = yCoord;
        }

        private void AddAction(XmlDocument Main, XmlDocument Action)
        {
            XmlNode currentNode = Main.SelectSingleNode("/ScriptContainer/Actions") as XmlNode;
            currentNode.AppendChild(currentNode.OwnerDocument.ImportNode(Action.DocumentElement, true));
        }
    }
}
