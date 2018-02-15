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
        ObservableCollection<SubDirectory> _prompts;
        int _projectNumber;
        string _scriptName;
        #endregion    

        #region Properties
        public ObservableCollection<SubDirectory> Prompts
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
        public Script()
        {
            Prompts = new ObservableCollection<SubDirectory>();
            Prompts.Add(new SubDirectory("ProdLevel", 0));
            Prompts.Add(new SubDirectory("Region", 1));
            Prompts.Add(new SubDirectory("Lang", 0));
            ProjectNumber = 0;
            BaseDirectory = "Prompts\\";
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
