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

namespace I1JM39_GUI_2022232.WpfClient
{
    /// <summary>
    /// Interaction logic for DeveloperWindow.xaml
    /// </summary>
    public partial class DeveloperWindow : Window
    {
        public DeveloperWindow()
        {
            InitializeComponent();
        }

        private void bt_back_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
