using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace DinoGame
{
    class Omgeving
    {
        //Positie van grond en wolk
        private double _x, _y, _grondY, BreedteScherm, HoogteScherm;
        private double _wolkX, _wolkY;

        //Vorm grond en wolk
        private int _grondHoogte, _grondBreedte;
        private Image _grondModel;
        private Image _wolkModel;
        private int _grondSnelheid = 15;
        private int _wolkSnelheid = 3;

        //Constructor
        public Omgeving(Canvas Gamevenster)
        {
            BreedteScherm = Gamevenster.Width;
            HoogteScherm = Gamevenster.Height;
            _x = 0;
            _grondY = HoogteScherm * 0.9;
            _y = _grondY;
            //Grootte van de grond bepalen
            _grondBreedte = Convert.ToInt16(BreedteScherm / 7);
            _grondHoogte = _grondBreedte;

            Spawn(Gamevenster);
        }

        //Eingenschappen
        public Image GrondModel
        {
            get
            {
                _grondModel.Margin = new Thickness(_x, _y, 0, 0);
                return _grondModel;
            }
        }

        //Methodes
        public void Spawn(Canvas Gamevenster)
        {
            //Foto aanmaken om grond te tonen
            _grondModel = new Image();
            _grondModel.Width = Gamevenster.Width * 4;

            //Pad naar grond bestand geven en positie bepalen
            _grondModel.Source = new BitmapImage(new Uri("/Images/grond.png", UriKind.Relative));
            _grondModel.Margin = new Thickness(0, _y, 0, 0);
            
            //Grond in het canvas zetten
            Gamevenster.Children.Add(_grondModel);


            //Foto aanmaken om wolk te tonen
            _wolkModel = new Image();

            //Grootte wolk
            _wolkModel.Width = Gamevenster.Width * 0.05;
            _wolkX = Gamevenster.Width + _wolkModel.Width;

            //Pad naar wolk bestand geven en positie bepalen
            _wolkModel.Source = new BitmapImage(new Uri("/Images/Wolk.png", UriKind.Relative));
            _wolkModel.Margin = new Thickness(_wolkX, _wolkY, 0, 0);

            //Wolk in het canvas zetten
            Gamevenster.Children.Add(_wolkModel);
        }

        //Omgeving bewegen
        public void OmgevingBewegen(Canvas Gamevenster)
        {
            _x -= _grondSnelheid;
            _wolkX -= _wolkSnelheid;

            _grondModel.Margin = new Thickness(_x, _grondY, 0, 0);

            if (_x + _grondModel.Width < Gamevenster.Width)
            {
                _x = 0;
            }


            _wolkY = Gamevenster.Height * 0.3;
            _wolkModel.Margin = new Thickness(_wolkX, _wolkY, 0, 0);

            if (_wolkX < 0)
            {
                _wolkX = Gamevenster.Width + _wolkModel.Width;
            }

        }

    }
}
