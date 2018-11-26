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
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections;
using System.Speech.Synthesis;
using System.Reflection;
using System.Speech.AudioFormat;

namespace PromptMaker.Assets.Scripts.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int x = 0;
        int prodloop = 0;
        string savePath = "";
        string ENV = "";
        string originalBaseDirectory = "";
        string recordingSubdirectories = "";
        public ObservableCollection<Page> pages;
        System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        System.Windows.Forms.DialogResult mySaveDialog;
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
        // Delete current Tab (only scripts, not Settings or Add Button)
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

        // Export data to XML files to be imported into studio
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (theSetting.UserID != "" && theSetting.BusinessNumber != "" && theSetting.BusinessNumber != "0")
            {
                if (tabController.Items.Count > 2)
                {
                    if (prodloop != 1)
                    {
                        folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
                        mySaveDialog = folderBrowserDialog.ShowDialog();
                    }                    
                    if (mySaveDialog == System.Windows.Forms.DialogResult.OK)
                    {
                        savePath = folderBrowserDialog.SelectedPath + "\\";
                        if (theSetting.PlaceHolder)
                        {
                            Directory.CreateDirectory(savePath + "\\Prompts\\");
                        }
                        // Set file name for regular and prod / dev options
                        if (theSetting.Environment == true && prodloop == 0)
                        {
                            savePath = savePath + "Record_Prompts_Here_Dev.XML";
                            ENV = "Dev";
                            originalBaseDirectory = theSetting.BaseDirectory;
                            theSetting.BaseDirectory = originalBaseDirectory + "Dev\\";
                        }
                        else if (theSetting.Environment == true && prodloop == 1)
                        {
                            savePath = savePath + "Record_Prompts_Here_Prod.XML";
                            ENV = "Prod";
                            theSetting.BaseDirectory = originalBaseDirectory + "Prod\\";
                        }
                        else
                        {
                            savePath = savePath + "Record_Prompts_Here.XML";
                            ENV = "";
                        }
                        // Load in all the XMLs
                        XmlDocument ActionsXML = new XmlDocument();
                        string pathBase = @"C:\Users\ethan.jensen\Source\Repos\PromptMaker\PromptMaker\PromptMaker\Assets\Scripts\Models\XML Template\";
                        string path = System.IO.Path.Combine(pathBase, "Actions.XML");
                        // string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Assets\Scripts\Models\XML Template\", "Actions.XML");
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
                        //PlaySetting(PlayXML, "test ID", "Test Caption", "Test Sequence", "Test Phrase", "Test X Cord", "Test Y Cord");
                        //MenuSetting(MenuXML, "test ID", "Test Caption", "Test Sequence", "Test Phrase", "Test X Cord", "Test Y Cord");
                        AnotationSetting(AnnotationXML, "2", "Quick Link to here", "148", "33", "16", "32"); // default 148 and 33 dem
                        AddAction(ActionsXML, AnnotationXML);
                        int iy = 0;
                        int i = 3;
                        int xstart = 208;
                        int ystart = 40;
                        int spacing = 80;
                        int seperator = 128;
                        List<string> usedAnnotations = new List<string>();
                        recordingSubdirectories = "";
                        List<string> listUsedDirectory = new List<string>();
                        List<string> listUsedSubDirectory = new List<string>();
                        foreach (SubDirectory tempSettingTuple in theSetting.SubDirectories)
                        {
                            listUsedDirectory.Add(tempSettingTuple.Path);
                            foreach (SubDirectory tempVariations in tempSettingTuple.Variations)
                            {
                                listUsedSubDirectory.Add(tempVariations.Path);
                            }
                        }
                        

                        foreach (TabItem tempItem in tabController.Items)
                        {
                            if (tabController.Items.IndexOf(tempItem) > 0 && tabController.Items.IndexOf(tempItem) != tabController.Items.Count && tempItem.Content != null)
                            {                              
                                Frame frame = tempItem.Content as Frame;
                                Scripts page = frame.Content as Scripts;
                                Script tempScript = page.DataContext as Script;
                                ListBox tempListBoxTest = page.listViewSubDirectories as ListBox;
                                AnotationSetting(AnnotationXML, i.ToString(), tempScript.ScriptName, "148", "33", "16", (ystart + (iy * seperator)).ToString());
                                AddAction(ActionsXML, AnnotationXML);
                                i++;
                                //if (page.usedDirectories.Count == 1)
                                //{
                                //    AnotationSetting(AnnotationXML, i.ToString(), page.usedDirectories[1], "59", "910", "336", "96");
                                //    AddAction(ActionsXML, AnnotationXML);
                                //    i++;
                                //}
                                //if (page.usedDirectories.Count == 2)
                                //{
                                //    AnotationSetting(AnnotationXML, i.ToString(), page.usedDirectories[1], "59", "910", "336", "96");
                                //    AddAction(ActionsXML, AnnotationXML);
                                //    i++;
                                //    AnotationSetting(AnnotationXML, i.ToString(), page.usedDirectories[2], "774", "39", "416", "32");
                                //    AddAction(ActionsXML, AnnotationXML);
                                //    i++;
                                //}
                                string PromptPrefixCoding = "";
                                string PromptSuffixCoding = "";
                                string SpokenSuffix = "";
                                //This secition is for cleaning up usedVariation and usedDirectories this can be streamlined
                                #region cleanup 
                                List<string> badDirectories = page.usedDirectories.Except(listUsedDirectory).ToList();
                                page.usedDirectories = page.usedDirectories.Except(badDirectories).ToList();

                                List<int> badVariations = new List<int>();
                                List<Tuple<int, string>> badSubVariations = new List<Tuple<int, string>>();
                                for (int v = 0; v < page.usedVariations.Count; v++)
                                {
                                    if (!listUsedDirectory.Contains(page.usedVariations[v].Item1))
                                    {
                                        badVariations.Add(v);
                                    }
                                    for (int sv = 0; sv < page.usedVariations[v].Item2.Count; sv++)
                                    {
                                        if (!listUsedSubDirectory.Contains(page.usedVariations[v].Item2[sv]))
                                        {
                                            Tuple<int, string> tempVariationTuple = new Tuple<int, string>(v, page.usedVariations[v].Item2[sv]);
                                            badSubVariations.Add(tempVariationTuple);
                                        }
                                    }
                                }
                                foreach (Tuple<int, string> tempSubVariationRemoval in badSubVariations)
                                {
                                    while (page.usedVariations[tempSubVariationRemoval.Item1].Item2.Contains(tempSubVariationRemoval.Item2))
                                    {
                                        page.usedVariations[tempSubVariationRemoval.Item1].Item2.Remove(tempSubVariationRemoval.Item2);
                                    }
                                }
                                foreach (int tempVariationRemoval in badVariations)
                                {
                                    page.usedVariations.RemoveAt(tempVariationRemoval);
                                }
                                #endregion

                                foreach (string tempDirectoryName in page.usedDirectories)
                                {
                                    if (theSetting.SubDirectory)
                                    {
                                        PromptPrefixCoding = PromptPrefixCoding + "{" + tempDirectoryName + "}\\";
                                    }
                                    if(theSetting.PromptName)
                                    {
                                        PromptSuffixCoding = PromptSuffixCoding + "_{" + tempDirectoryName + "}";
                                    }                                    
                                }
                                int ix = 0;
                                PlaySetting(PlayXML, i.ToString(), tempScript.ScriptName + " Coding", ((char)34).ToString() + theSetting.BaseDirectory + PromptPrefixCoding + String.Join(PromptSuffixCoding + ".wav" + ((char)34).ToString() + " " + ((char)34).ToString() + theSetting.BaseDirectory + PromptPrefixCoding, tempScript.Sequence) + PromptSuffixCoding + ".wav" + ((char)34).ToString(), tempScript.Phrase, (xstart + (ix * spacing)).ToString(), (ystart + (iy * seperator)).ToString());
                                AddAction(ActionsXML, PlayXML);
                                i++;
                                ix++;
                                MenuSetting(MenuXML, i.ToString(), tempScript.ScriptName + " Coding", ((char)34).ToString() + theSetting.BaseDirectory + PromptPrefixCoding + String.Join(PromptSuffixCoding + ".wav" + ((char)34).ToString() + " " + ((char)34).ToString() + theSetting.BaseDirectory + PromptPrefixCoding, tempScript.Sequence) + PromptSuffixCoding + ".wav" + ((char)34).ToString(), tempScript.Phrase, (xstart + (ix * spacing)).ToString(), (ystart + (iy * seperator)).ToString());
                                AddAction(ActionsXML, MenuXML);
                                i++;
                                ix++;
                                //page.usedVariations.RemoveAt(2);
                                //page.usedVariations[1].Item2.RemoveRange(9, 9);

                                

                                if (page.usedDirectories.Count != 0 && page.usedVariations.Count != 0)
                                {
                                    List<List<string>> posibilitiesInput = new List<List<string>>();
                                    foreach (Tuple<string, List<string>> temptumple in page.usedVariations)
                                        posibilitiesInput.Add(temptumple.Item2);
                                    var possibilitesOutput = GetAllPossibleCombos(posibilitiesInput);
                                    foreach (string s in possibilitesOutput)
                                    {
                                        PromptPrefixCoding = "";
                                        PromptSuffixCoding = "";
                                        SpokenSuffix = "";
                                        string ss = s.Substring(1);
                                        List<string> l = ss.Split(',').ToList();
                                        foreach (string sss in l)
                                        {
                                            if (theSetting.SubDirectory)
                                            {
                                                PromptPrefixCoding = PromptPrefixCoding + sss + "\\";
                                            }
                                            if (theSetting.PromptName)
                                            {
                                                PromptSuffixCoding = PromptSuffixCoding + "_" + sss;
                                            }   
                                            SpokenSuffix = SpokenSuffix + sss + ". ";                                            
                                        }                                                                            
                                        recordingSubdirectories = recordingSubdirectories + PromptPrefixCoding + "|";
                                        if (theSetting.PlaceHolder)
                                        {
                                            foreach (Models.Prompt tempPrompt in tempScript.Prompts)
                                            {
                                                Directory.CreateDirectory(folderBrowserDialog.SelectedPath + "\\" + theSetting.BaseDirectory + PromptPrefixCoding);
                                                CreateWAV(folderBrowserDialog.SelectedPath + "\\" + theSetting.BaseDirectory + PromptPrefixCoding + tempPrompt.PromptName + PromptSuffixCoding + ".wav", SpokenSuffix + " " + tempPrompt.PromptVerbiage);
                                            }
                                        }
                                        PlaySetting(PlayXML, i.ToString(), tempScript.ScriptName + " " + String.Join(" ", l), ((char)34).ToString() + theSetting.BaseDirectory + PromptPrefixCoding + String.Join(PromptSuffixCoding + ".wav" + ((char)34).ToString() + " " + ((char)34).ToString() + theSetting.BaseDirectory + PromptPrefixCoding, tempScript.Sequence) + PromptSuffixCoding + ".wav" + ((char)34).ToString(), tempScript.Phrase, (xstart + seperator + (ix * spacing)).ToString(), (ystart + (iy * seperator)).ToString());
                                        AddAction(ActionsXML, PlayXML);
                                        i++;
                                        ix++;
                                    }
                                }
                                else
                                {
                                    PlaySetting(PlayXML, i.ToString(), tempScript.ScriptName, ((char)34).ToString() + theSetting.BaseDirectory + String.Join(".wav" + ((char)34).ToString() + " " + ((char)34).ToString() + theSetting.BaseDirectory, tempScript.Sequence) + ".wav" + ((char)34).ToString(), tempScript.Phrase, (xstart + seperator + (ix * spacing)).ToString(), (ystart + (iy * seperator)).ToString());
                                    AddAction(ActionsXML, PlayXML);
                                    i++;
                                    ix++;
                                }
                            }
                            iy++;
                        }
                        // Loop through Scripts 
                        //PlaySetting(PlayXML);
                        if (recordingSubdirectories.Length > 1)
                        {
                            recordingSubdirectories.Substring(0, recordingSubdirectories.Length - 1);
                        }
                        string snippetblock = "BaseDirectory = " + ((char)34).ToString() + theSetting.BaseDirectory + ((char)34).ToString() + ((char)10).ToString() +
                                              "FileName = " + ((char)34).ToString() + "DELETEME.wav" + ((char)10).ToString() +
                                              "SubDirectories = " + ((char)34).ToString() + recordingSubdirectories + ((char)34).ToString() + ((char)10).ToString() + ((char)10).ToString();
                        ActionsXML.Save(savePath);
                        if (theSetting.Environment == true && prodloop == 0)
                        {
                            prodloop = 1;
                            ButtonSave_Click(sender, e);
                            theSetting.BaseDirectory = originalBaseDirectory;
                        }
                        else if (theSetting.Environment == true && prodloop == 1)
                        {
                            prodloop = 0;
                            theSetting.BaseDirectory = originalBaseDirectory;
                        }                        
                    }
                }
                else
                    MessageBox.Show("Please add some scripts.");
            }
            else
                MessageBox.Show("Please add UserID and/or BU Number to Settings.");
        }
         


        private void ActionsSetting(XmlDocument XML)
        {
            XML.SelectSingleNode("/ScriptContainer").Attributes["BusNo"].Value = theSetting.BusinessNumber;
            XML.SelectSingleNode("/ScriptContainer").Attributes["UserID"].Value = theSetting.UserID;
            XML.SelectSingleNode("/ScriptContainer/LibraryItem").Attributes["BusNo"].Value = theSetting.BusinessNumber;
            XML.SelectSingleNode("/ScriptContainer/LibraryItem/ModifyUserID").InnerText = theSetting.UserID;
            XML.SelectSingleNode("/ScriptContainer/LibraryItem/URI").InnerText = "/custom/" + theSetting.BusinessNumber + "/Record_Prompts_Here";
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

        private void CreateWAV (string fileName, string verbiage)
        {
            using (Stream ret = new MemoryStream())
            using (SpeechSynthesizer synth = new SpeechSynthesizer())
            {
                var mi = synth.GetType().GetMethod("SetOutputStream", BindingFlags.Instance | BindingFlags.NonPublic);
                //   var fmt = new SpeechAudioFormatInfo(8000, AudioBitsPerSample.Eight, AudioChannel.Mono);
                var fmt = new SpeechAudioFormatInfo(EncodingFormat.ULaw, 8000, 8, 1, 16000, 2, null);
                mi.Invoke(synth, new object[] { ret, fmt, true, true });
                synth.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult);
                synth.Speak(verbiage);
                // Testing code:                
                using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    ret.Position = 0;
                    byte[] buffer = new byte[4096];
                    for (; ; )
                    {
                        int len = ret.Read(buffer, 0, buffer.Length);
                        if (len == 0) break;
                        fs.Write(buffer, 0, len);
                    }
                }
            }
        }

        public static List<string> GetAllPossibleCombos(List<List<string>> strings)
        {
            IEnumerable<string> combos = new[] { "" };

            foreach (var inner in strings)
                combos = from c in combos
                         from i in inner
                         select c + "," +i;

            return combos.ToList();
        }
    }
}
