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
            ProjectNumber = 0;
            ScriptName = "Example: Script will add _s for you";

            // If didn't want the values to persist into new scripts and update
            //foreach (SubDirectory directory in setting.SubDirectories)
            //{
            //    SubDirectories.Add(directory);
            //}
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
