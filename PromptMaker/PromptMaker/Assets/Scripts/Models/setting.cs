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
        string _baseDirectory;
        string _userEmail;
        string _businessNumber;
        string _URI;
        string _userID;
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
        public string BaseDirectory
        {
            get
            {
                return _baseDirectory;
            }
            set
            {
                _baseDirectory = value;
                NotifyPropertyChanged("BaseDirectory");
            }
        }
        public string UserEmail
        {
            get
            {
                return _userEmail;
            }
            set
            {
                _userEmail = value;
                NotifyPropertyChanged("UserEmail");
            }
        }        
        public string BusinessNumber
        {
            get
            {
                return _businessNumber;
            }
            set
            {
                _businessNumber = value;
                NotifyPropertyChanged("BusinessNumber");
            }
        }
        public string URI
        {
            get
            {
                return _URI;
            }
            set
            {
                _URI = value;
                NotifyPropertyChanged("URI");
            }
        }
        public string UserID
        {
            get
            {
                return _userID;
            }
            set
            {
                _userID = value;
                NotifyPropertyChanged("UserID");
            }
        }
        #endregion

        #region Constructors
        public Setting()
        {
            
            SubDirectories = new ObservableCollection<SubDirectory>();
            ObservableCollection<SubDirectory> ProdLevelSub = new ObservableCollection<SubDirectory>();
            ProdLevelSub.Add(new SubDirectory("Prod", 0, null));
            ProdLevelSub.Add(new SubDirectory("Dev", 0, null));
            ObservableCollection<SubDirectory> RegionSub = new ObservableCollection<SubDirectory>();
            RegionSub.Add(new SubDirectory("US", 1, null));
            RegionSub.Add(new SubDirectory("EMEA", 1, null));
            RegionSub.Add(new SubDirectory("CA", 1, null));
            ObservableCollection<SubDirectory> LangSub = new ObservableCollection<SubDirectory>();
            LangSub.Add(new SubDirectory("Prod", 0, null));
            LangSub.Add(new SubDirectory("Dev", 0, null));
            SubDirectories.Add(new SubDirectory("ProdLevel",0, ProdLevelSub));
            SubDirectories.Add(new SubDirectory("Region",1, RegionSub));
            SubDirectories.Add(new SubDirectory("Lang",0, LangSub));
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

