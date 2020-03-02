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

namespace DinoGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        //Play
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Game gameview = new Game();
            gameview.Show();
            Close();
        }

        //Exit
        private void Stopbtn_Click(object sender, RoutedEventArgs e) => Environment.Exit(0);

        //Score
        private void Scorebtn_Click(object sender, RoutedEventArgs e)
        {
            Score scoreview = new Score();
            scoreview.Show();
            Close();
        }
    }
}
