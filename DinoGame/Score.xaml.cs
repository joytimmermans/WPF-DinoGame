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
using System.Data.SqlClient;

namespace DinoGame
{
    /// <summary>
    /// Interaction logic for Score.xaml
    /// </summary>
    public partial class Score : Window
    {
        //Database oproepen
        SqlConnection scnnHighscore = new SqlConnection(Properties.Settings.Default.DinoConnection);

        //Gegevens overlopen van de database
        SqlDataReader sdrHighscore;


        public Score()
        {
            InitializeComponent();

            //Sql maken om gegevens uit database te halen
            SqlCommand cmdScores = new SqlCommand("SELECT * FROM Highscore ORDER BY Score DESC", scnnHighscore);
            //Database openen
            scnnHighscore.Open();
            //Gegevens in database lezen
            sdrHighscore = cmdScores.ExecuteReader();
            //Zolang er gegevens zijn in de database zal het blijven toevoegen
            while (sdrHighscore.Read())
            {
                //Gegevens toevoegen in de listbox
                LijstScore.Items.Add("Naam speler: " + sdrHighscore["Naam"].ToString() + " Score: " + sdrHighscore["Score"].ToString());
            }
            //Database sluiten
            scnnHighscore.Close();
        }

        //Terug gaan/ Exit
        private void BtnTerug_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindowview = new MainWindow();
            mainwindowview.Show();
            Close();
        }
    }
}
