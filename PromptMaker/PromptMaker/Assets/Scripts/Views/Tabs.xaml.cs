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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PromptMaker.Assets.Scripts.Views
{
    /// <summary>
    /// Interaction logic for Tabs.xaml
    /// </summary>
    public partial class Tabs : UserControl
    {
        TabControl tabController;
        public Tabs(Page page, TabControl _tabController, int i)
        {            
            this.DataContext = page;
            InitializeComponent();
            tabController = _tabController;
            if (i == 0)
            {
                textName.TextDecorations = TextDecorations.Underline;
                textName.FontSize = 14;
                textName.FontWeight = FontWeights.Bold;
                ButtonX.Visibility = Visibility.Collapsed;
            }
            //ButtonX.Visibility = Visibility.Hidden;
        }
        // Remove the tab of where the X was pushed
        private void ButtonDeleteTab_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Delete Script?", "Please Select:", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                TabItem tabitem = this.Parent as TabItem;
                int i = tabController.Items.IndexOf(tabitem);
                int x = tabController.Items.Count;
                if (i != 0 && i != x)
                {
                    tabController.Items.Remove(tabitem);
                    if (i == x - 1)
                        tabController.SelectedIndex = i - 2;
                }                    
            }            
        }
    }
}
