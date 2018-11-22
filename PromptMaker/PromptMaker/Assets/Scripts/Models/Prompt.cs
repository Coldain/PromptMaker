using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptMaker.Assets.Scripts.Models
{
    public class Prompt : INotifyPropertyChanged
    {
        #region Fields
        string _promptName;
        string _promptVerbiage;
        string _sequence;
        string _phrase;
        bool _TTS;
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
        public string PromptVerbiage
        {
            get
            {
                return _promptVerbiage;
            }
            set
            {
                _promptVerbiage = value;
                NotifyPropertyChanged("PromptVerbiage");
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
        public bool TTS
        {
            get
            {
                return _TTS;
            }
            set
            {
                _TTS = value;
                NotifyPropertyChanged("TTS");
            }
        }
        #endregion

        #region Constructors
        public Prompt()
        {
            PromptName = "Prompt Name";
            PromptVerbiage = "Type verbiage here.";

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
