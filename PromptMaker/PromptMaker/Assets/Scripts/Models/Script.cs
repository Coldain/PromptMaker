using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptMaker.Assets.Scripts.Models
{
    public class Script : INotifyPropertyChanged
    {
        #region Fields
        ObservableCollection<SubDirectory> _subDirectories;
        ObservableCollection<Prompt> _prompts;
        ObservableCollection<InUse> _usages;
        int _projectNumber;
        string _scriptName;
        #endregion    

        #region Properties
        public ObservableCollection<SubDirectory> SubDirectories
        {
            get
            {
                return _subDirectories;
            }
            set
            {
                _subDirectories = value;
                NotifyPropertyChanged("SubDirectories");
            }
        }
        public ObservableCollection<InUse> Usages
        {
            get
            {
                return _usages;
            }
            set
            {
                _usages = value;
                NotifyPropertyChanged("Usages");
            }
        }
        public ObservableCollection<Prompt> Prompts
        {
            get
            {
                return _prompts;
            }
            set
            {
                _prompts = value;
                NotifyPropertyChanged("Prompts");
            }
        }
        public string Phrase
        {
            get
            {
                List<string> _phrase = new List<string>();
                foreach (Prompt tempPrompt in Prompts)
                {
                    _phrase.Add(tempPrompt.PromptName);
                    _phrase.Add(tempPrompt.PromptVerbiage + "\u000a");
                }
                return String.Join("\u000a", _phrase.ToArray());
            }
        }
        public List<string> Sequence
        {
            get
            {
                List<string> _sequence = new List<string>();
                foreach (Prompt tempPrompt in Prompts)
                {
                    _sequence.Add(tempPrompt.PromptName.Replace(" ", "_"));
                }
                return _sequence;
            }
        }
        public int ProjectNumber
        {
            get
            {
                return _projectNumber;
            }
            set
            {
                _projectNumber = value;
                NotifyPropertyChanged("ProjectNumber");
            }
        }
        public string ScriptName
        {
            get
            {
                return _scriptName;
            }
            set
            {
                _scriptName = value;
                NotifyPropertyChanged("ScriptName");
            }
        }
        #endregion

        #region Constructors
        public Script(Setting setting)
        {
            SubDirectories = new ObservableCollection<SubDirectory>();
            SubDirectories = setting.SubDirectories;
            Prompts = new ObservableCollection<Prompt>();
            Prompt tempPrompt = new Prompt();
            Prompts.Add(tempPrompt);
            ProjectNumber = 0;
            ScriptName = "Example: Script will add _s for you";
            if (Usages == null)
            {
                Usages = new ObservableCollection<InUse>();
                foreach (SubDirectory tempSub in SubDirectories)
                {
                    InUse tempinUse = new InUse(tempSub);
                    Usages.Add(tempinUse);
                }
            }
        }
        #endregion

        #region Methods
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        #endregion
    }
}
