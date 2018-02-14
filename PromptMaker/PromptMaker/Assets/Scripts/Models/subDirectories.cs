using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptMaker.Assets.Scripts.Models
{
    public class SubDirectories : INotifyPropertyChanged
    {
        #region Fields
        List<string> _name = new List<string>()
                {
                    "ProdLevel",
                    "Region",
                    "Lang"
                };
        List<bool> _inuse = new List<bool>()
        {
            false,
            false,
            false
        };
        #endregion    

        #region Properties
        public List<string> Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                NotifyPropertyChanged("Build");
            }
        }
        public List<bool> Inuse
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
        #endregion

        #region Constructors

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
