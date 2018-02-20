using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptMaker.Assets.Scripts.Models
{
    public class InUse : INotifyPropertyChanged
    {
        #region Fields
        SubDirectory _subDirectory;
        bool _usage;
        #endregion    

        #region Properties
        public SubDirectory SubDirectory
        {
            get
            {
                return _subDirectory;
            }
            set
            {
                _subDirectory = value;
                NotifyPropertyChanged("SubDirectory");
            }
        }
        public bool Usage
        {
            get
            {
                return _usage;
            }
            set
            {
                _usage = value;
                NotifyPropertyChanged("Usage");
            }
        }      
        #endregion

        #region Constructors
        public InUse(SubDirectory tempSub)
        {
            SubDirectory = tempSub;
            Usage = false;
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

