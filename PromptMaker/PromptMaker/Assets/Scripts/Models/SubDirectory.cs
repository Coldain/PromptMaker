using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptMaker.Assets.Scripts.Models
{
    public class SubDirectory: INotifyPropertyChanged
    {
        #region Fields
        string _path;
        string _owner;
        ObservableCollection<SubDirectory> _variations;
        bool _inuse;
        int _filled;
        #endregion    

        #region Properties
        public string Owner
        {
            get
            {
                return _owner;
            }
            set
            {
                _owner = value;
                NotifyPropertyChanged("Owner");
            }
        }
        public bool Inuse
        {
            get
            {
                return _inuse;
            }
            set
            {
                _inuse = value;
                NotifyPropertyChanged("Inuse");
                NotifyPropertyChanged("SubDirectories");
            }
        }
        public int Filled
        {
            get
            {
                return _filled;
            }
            set
            {
                _filled = value;
                NotifyPropertyChanged("Filled");
                NotifyPropertyChanged("SubDirectories");
            }
        }
        public string Path
        {
            get
            {
                return _path;
            }
            set
            {
                if (Variations != null)
                    foreach (SubDirectory tempSub in Variations)
                    {
                        tempSub.Owner = Path;
                    }
                _path = value;
                NotifyPropertyChanged("Path");
                NotifyPropertyChanged("SubDirectories");
            }
        }
        public ObservableCollection<SubDirectory> Variations
        {
            get
            {
                return _variations;
            }
            set
            {
                _variations = value;
                NotifyPropertyChanged("Variations");
                NotifyPropertyChanged("SubDirectories");
            }
        }
        #endregion

        #region Constructors
        public SubDirectory(string tempName, int i, ObservableCollection<SubDirectory> templist, string tempOwner)       
        {
            Owner = tempOwner;
            Path = tempName;
            Inuse = false;
            Filled = i;
            Variations = templist;
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
