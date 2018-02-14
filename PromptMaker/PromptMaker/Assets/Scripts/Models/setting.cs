using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptMaker.Assets.Scripts.Models
{
    public class Setting : INotifyPropertyChanged
    {
        #region Fields
        ObservableCollection<SubDirectory> _subDirectories;
        int _projectNumber;
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
                NotifyPropertyChanged("Setting");
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
                NotifyPropertyChanged("Setting");
            }
        }
        #endregion

        #region Constructors
        public Setting()
        {
            SubDirectories = new ObservableCollection<SubDirectory>();
            SubDirectories.Add(new SubDirectory("ProdLevel",0));
            SubDirectories.Add(new SubDirectory("Region",1));
            SubDirectories.Add(new SubDirectory("Lang",0));
            ProjectNumber = 0;
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

