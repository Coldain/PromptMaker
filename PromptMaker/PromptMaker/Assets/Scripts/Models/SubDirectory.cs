using System;
using System.Collections.Generic;
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
        bool _inuse;
        int _filled;
        #endregion    

        #region Properties
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
                _path = value;
                NotifyPropertyChanged("_path");
            }
        }
        #endregion

        #region Constructors
        public SubDirectory(string tempName, int i)
        {
            Path = tempName;
            Inuse = false;
            Filled = i;
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
