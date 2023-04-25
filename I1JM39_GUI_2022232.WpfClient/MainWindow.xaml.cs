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

namespace I1JM39_GUI_2022232.WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //This window is for selection only, but the name is still MainWindow
        public MainWindow()
        {
            InitializeComponent();
        }

        private void bt_games_Click(object sender, RoutedEventArgs e)
        {
            GameWindow gameWindow = new GameWindow();
            this.Visibility= Visibility.Visible;
            gameWindow.Show();
        }

        private void bt_chars_Click(object sender, RoutedEventArgs e)
        {
            CharacterWindow characterWindow = new CharacterWindow();
            this.Visibility= Visibility.Visible;
            characterWindow.Show();
        }

        private void bt_devs_Click(object sender, RoutedEventArgs e)
        {
            DeveloperWindow developerWindow = new DeveloperWindow();
            this.Visibility= Visibility.Visible;
            developerWindow.Show();
        }


        //Closes the program
        private void bt_exit_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            //Application.Current.Shutdown(); //Maybe this will not work, because of the swagger API or the JS in the future
        }

        
    }
}
