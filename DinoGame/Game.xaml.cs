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
using System.Windows.Threading;
using System.Data.SqlClient;

namespace DinoGame
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        //Decleratie
        int score = 0;
        int teller = 0;
        int RandomPositie;
        bool Jump = false;
        Random rnd = new Random();


        //objecten declaren
        Dino objDino;
        //Cactus objCactus;
        Omgeving objOmgeving;
        GameOver objGameOver;
        Cactus[] objCactus;
        //Timer en label van de timer
        DispatcherTimer moveObjectTmr = new DispatcherTimer();
        DispatcherTimer ScoreTimer = new DispatcherTimer();
        DispatcherTimer AnimatieTimer = new DispatcherTimer();

        ////Database wordt opgeroepen
        SqlConnection snnScore = new SqlConnection(Properties.Settings.Default.DinoConnection);


        public Game()
        {
            // initializeren
            InitializeComponent();
            GameVenster.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
            GameVenster.Height = System.Windows.SystemParameters.PrimaryScreenHeight;

            lblScore.FontSize = GameVenster.Width / 80;
            lblScore.Margin = new Thickness(GameVenster.Width * 0.8, GameVenster.Height * 0.1, 0, 0);

            //objecten initializeren
            objOmgeving = new Omgeving(GameVenster);
            objDino = new Dino(GameVenster);
            objCactus = new Cactus[4];
            objGameOver = new GameOver(GameVenster);

            //Timer starten voor objecten
            moveObjectTmr.Tick += new EventHandler(ObjectMovement);
            moveObjectTmr.Interval = new TimeSpan(0, 0, 0, 0, 3);
            moveObjectTmr.Start();

            //Timer voor score
            ScoreTimer.Tick += new EventHandler(Score);
            ScoreTimer.Interval = new TimeSpan(0, 0, 0, 0, 80);
            ScoreTimer.Start();

            //Timer voor Animaties
            AnimatieTimer.Tick += new EventHandler(Aniematies);
            AnimatieTimer.Interval = new TimeSpan(0, 0, 0, 0, 475);
            AnimatieTimer.Start();

            //Cactus omvang geven
            for (int i = 0; i < 3; i++)
            {
                
                RandomPositie = rnd.Next(1000, 6000);

                if (i % 2 == 0)
                { 
                objCactus[i] = new Cactus(GameVenster);
                objCactus[i].X += RandomPositie;
                }
                else
                {
                  objCactus[i] = new Cactus(GameVenster);
                  objCactus[i].X += RandomPositie + GameVenster.Width * 0.8;
                }
            }

            objCactus[0].CactusModel.Source = new BitmapImage(new Uri("/Images/Cactus.png", UriKind.Relative));
            objCactus[1].CactusModel.Source = new BitmapImage(new Uri("/Images/Cactus2.png", UriKind.Relative));
            objCactus[2].CactusModel.Source = new BitmapImage(new Uri("/Images/Cactus3.png", UriKind.Relative));
        }

        //Controleren of toetsen worden ingedrukt
        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space || e.Key == Key.Up)
            {
                //Zorgen dat je niet in de lucht kan springen
                if (objDino.Y >= objDino.GrondY)
                {
                    Jump = true;
                }
            }
        }

        //Timerevent voor bewegingen
        private void ObjectMovement(object sender, EventArgs e)
        {
            //Dino logic
            DinoSprong();

            //Dino vloeiend laten lopen
            objDino.Animatie(teller, Jump);
            
            //Cactus posities
            RandomPositie = rnd.Next(Convert.ToInt16(GameVenster.Width) + Convert.ToInt16(objCactus[0].Breedte), (Convert.ToInt16(GameVenster.Width) + Convert.ToInt16(objCactus[0].Breedte)) * 2);
            for (int i = 0; i < 3; i++)
            {
                //Cactus logic
                objCactus[i].CactusLinks();
                objCactus[i].CactusRestart(GameVenster, RandomPositie);
            }
            

            //Grond Logic
            objOmgeving.OmgevingBewegen(GameVenster);

           
            
            for (int y = 0; y < 3; y++)
            { 
            //Controleren of dino tegen cactus collide
            if (objGameOver.botsen(GameVenster, objDino, objCactus[y], btnRestart, txtGebruiker, lblTypeNaam) == true)
                {
                    //Game animaties stoppen
                    AnimatieTimer.Stop();
                    ScoreTimer.Stop();
                    moveObjectTmr.Stop();

                    //Dino sterft gezichtje
                    objDino.DinoModel.Source = new BitmapImage(new Uri("/Images/DinoDood.png", UriKind.Relative));
                }
            }
        }

        //Timerevent voor Dino beweging animatie
        private void Aniematies(object sender, EventArgs e)
        {
            teller += 1;
        }

        //Telt score op
        private void Score(object sender, EventArgs e)
        {
                score += 1;
                lblScore.Content = "Score: " + score;

        }

        //Dino laten springen en dalen
        private void DinoSprong()
        {
            //Dino springt
            if (Jump == true)
            {
                objDino.Springen();
                if (objDino.Y <= objDino.maxHoogteSprong)
                {
                    Jump = false;
                }
            }

            //Dino daalt
            if (Jump == false)
            {
                objDino.Dalen();
            }
        }

        //Exit/restart button
        private void BtnRestart_Click(object sender, RoutedEventArgs e)
        {
            ////Query dat de score en naam toevoegd wordt aangemaakt en uitgevoerd
            SqlCommand cmdScoreToevoegen = new SqlCommand("INSERT INTO Highscore (Naam, Score) VALUES ('" + txtGebruiker.Text + "','" + score + "')", snnScore);
            
            ////Database opent
            snnScore.Open();
            cmdScoreToevoegen.ExecuteNonQuery();

            //Database sluit
            snnScore.Close();

            //Windows sluiten
            MainWindow mainwindowview = new MainWindow();
            mainwindowview.Show();
            Close();
        }
    }

}
