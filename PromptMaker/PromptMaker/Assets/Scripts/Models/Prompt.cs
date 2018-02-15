using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptMaker.Assets.Scripts.Models
{
    class Prompt : INotifyPropertyChanged
    {
        #region Fields
        string _promptName;
        string _sequence;
        string _phrase;
        #endregion    

        #region Properties
        public string PromptName
        {
            get
            {
                return _promptName;
            }
            set
            {
                _promptName = value;
                NotifyPropertyChanged("PromptName");
            }
        }
        public string Sequence
        {
            get
            {
                return _sequence;
            }
            set
            {
                _sequence = value;
                NotifyPropertyChanged("Sequence");
            }
        }
        public string Phrase
        {
            get
            {
                return _phrase;
            }
            set
            {
                _phrase = value;
                NotifyPropertyChanged("Phrase");
            }
        }
        #endregion

        #region Constructors
        public Prompt()
        {

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
