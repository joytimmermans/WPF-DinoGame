using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace DinoGame
{
    internal class Dino
    {
        //Positie van dino
        private double _x, _y, _grondY, BreedteScherm, HoogteScherm;
        
        //Vorm dino
        private int _dinoHoogte, _dinoBreedte;
        private Image _dinoModel;

        //Constructor
        public Dino(Canvas Gamevenster)
        {
            //Locatie van de dino bepalen
            BreedteScherm = Gamevenster.Width;
            HoogteScherm = Gamevenster.Height;
            _x = BreedteScherm / 8;
            _grondY = HoogteScherm * 0.81;
            _y = _grondY;

            //Dimenties van de Dino bepalen
            _dinoBreedte = Convert.ToInt16(BreedteScherm / 15);
            _dinoHoogte = _dinoBreedte;

            Spawn(Gamevenster);
        }

        //Eigenschappen
        public double X { get => _x; set => _x = value; }
        public double Y { get => _y; set => _y = value; }
        public int Hoogte { set => _dinoHoogte = value; }
        public int Breedte { get => _dinoBreedte; set => _dinoBreedte = value; }
        //Maximale hoogte dat de dino kan springen
        public double maxHoogteSprong => HoogteScherm * 0.5;
        //Geeft de Y coördinaten van de grond door
        public double GrondY => _grondY;
        public Image DinoModel
        {
            get
            {
                _dinoModel.Margin = new Thickness(_x, _y, 0, 0);
                return _dinoModel;
            }
        }

        //Laad de dino in
        public void Spawn(Canvas Gamevenster)
        {
            //Foto aanmaken om dino te tonen
            _dinoModel = new Image();
            _dinoModel.Width = _dinoBreedte;

            //Pad naar dino bestand geven
            _dinoModel.Source = new BitmapImage(new Uri("/Images/Dino1.png", UriKind.Relative));
            _dinoModel.Margin = new Thickness(_x, _y, 0, 0);
            Gamevenster.Children.Add(_dinoModel);
        }

        //zorgt dat de dino springt
        public void Springen()
        {
            //De dino Omhoog laten springen
            _y -= 15;
            _dinoModel.Margin = new Thickness(_x, _y, 0, 0);

        }

        //Zorgt dat de dino terug naar de grond gaat
        public void Dalen()
        {
            //De dino terug naar onder laten komen op een trager tempo om dit meer aangenaam te maken
            if (_y != _grondY)
            {
                //deelbaar door 5
                _y += 12;

                _dinoModel.Margin = new Thickness(_x, _y, 0, 0);

                //Moest de dino lager komen dan de grond
                if (_y > _grondY)
                {
                    _y = _grondY;
                    _dinoModel.Margin = new Thickness(_x, _y, 0, 0);
                }
            }
        }

        //Dino animaties
        public void Animatie(int teller, bool jump)
        {
            //Dino springt animatie
            if (jump = false || _y != _grondY)
            {
                _dinoModel.Source = new BitmapImage(new Uri("/Images/DinoSpring.png", UriKind.Relative));
            }
            else
            { 
            //Dino linkervoet animatie
            if (teller % 2 == 0)
            {
                _dinoModel.Source = new BitmapImage(new Uri("/Images/Dino1.png", UriKind.Relative));
            }
            //Dino rechtervoet animatie
            else
            {
                _dinoModel.Source = new BitmapImage(new Uri("/Images/Dino2.png", UriKind.Relative));
            }
            }
        }
    }
}