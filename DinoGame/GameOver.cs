using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace DinoGame
{
    class GameOver
    {

        //Vorm dino
        private Image GameOverImg;
        

        public GameOver(Canvas GameVenster)
        {
            //Decleratie
            GameOverImg = new Image();
            
            //Content grote
            GameOverImg.Width = GameVenster.Width / 3;
            

            //Pad naar GameOver geven
            GameOverImg.Source = new BitmapImage(new Uri("/Images/GameOver.png", UriKind.Relative));
            GameOverImg.Margin = new Thickness((GameVenster.Width / 2) - (GameOverImg.Width / 2), GameVenster.Height * 0.20, 0, 0);

        }

        //Controleren of dino botst
        public bool botsen(Canvas pGameVenster, Dino pDino, Cactus pCactus, Button pRestart, TextBox pGebruiker, Label pTypeNaam)
        {
            //Decleratie
            if(pCactus.X * 1.20 > pDino.X && pCactus.X * 1.20 < pDino.X + pDino.Breedte)
            {
                if (pDino.Y > (pCactus.Y - pCactus.Hoogte) * 1.18)
                {
                    //Foto toevoegen
                    pGameVenster.Children.Add(GameOverImg);

                    //Grootte knop, textbox en label
                    pTypeNaam.Width = 150;
                    pTypeNaam.Height = 50;
                    pRestart.Width = pGameVenster.Width / 20;
                    pRestart.Height = pRestart.Width;
                    pGebruiker.Width = pGameVenster.Width / 5;
                    pGebruiker.Height = pRestart.Height;
                    pGebruiker.FontSize = pGebruiker.Height * 0.6;
                    //Positie knop, textbox en label
                    pGebruiker.Margin = new Thickness((pGameVenster.Width / 2) + pRestart.Width * 1.10, pGameVenster.Height * 0.30, 0, 0);
                    pRestart.Margin = new Thickness((pGameVenster.Width / 2) - (pRestart.Width / 2), pGameVenster.Height * 0.30, 0, 0);
                    pTypeNaam.Margin = new Thickness((pGameVenster.Width / 2) + pTypeNaam.Width, pGameVenster.Height * 0.28, 0, 0);

                    return true;
                }
            }
                return false;
        }

        

        



    }
}
