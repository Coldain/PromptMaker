using PromptMaker.Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PromptMaker.Assets.Scripts.Views
{
    /// <summary>
    /// Interaction logic for PromptSettings.xaml
    /// </summary>
    public partial class PromptSettings : Window
    {
        public PromptSettings(Prompt _currentPrompt)
        {
            InitializeComponent();
        }
    }
}
