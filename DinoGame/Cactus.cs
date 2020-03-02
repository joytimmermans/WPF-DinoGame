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
    class Cactus
    {
        //Positie cactus
        private double _x, _y, _grondY, BreedteScherm, HoogteScherm, _cactusBreedte;
        Random rnd = new Random();
        //Vorm cactus
        private int _cactusHoogte;
        private Image _cactusModel;
        private double _vorigeCactus;

        //Snelheid
        private int _snelheid=15;

        //Constructor
        public Cactus(Canvas Gamevenster)
        {
            //Grootte cactus
            BreedteScherm = Gamevenster.Width;
            HoogteScherm = Gamevenster.Height;
            _cactusBreedte = Gamevenster.Width / 19;

            //Positie
            _grondY = Gamevenster.Height * 0.75;
            _x = BreedteScherm;
            _y = _grondY * 0.75;

            //Cactus inspawnen
            Spawn(Gamevenster);

        }

        //Eigenschappen
        public double X { set => _x = value; get => _x; }
        public double Y { set => _y = value; get => _y; }
        public int Snelheid { set => _snelheid = value; }
        public int Hoogte { get => _cactusHoogte; set => _cactusHoogte = value; }
        public double Breedte { get => _cactusBreedte; set => _cactusBreedte = value; }
        
        public Image CactusModel => _cactusModel;

        //Methode
        public void Spawn(Canvas Gamevenster)
        {
            //Foto aanmaken om dino te tonen
            _cactusModel = new Image();
            _cactusModel.Width = _cactusBreedte;

            //Cactus in de canvas plaatsen
            _cactusModel.Margin = new Thickness(_x, _y, 0, 0);
            Gamevenster.Children.Add(_cactusModel);
        }

        //Procedure - Boom beweegt naar links
        public void CactusLinks()
        {
            _x -= _snelheid;
            _cactusModel.Margin = new Thickness(_x, _grondY, 0, 0);
           
        }

        //Als cactus uit het venster is deze terug in het venster stoppen
        public void CactusRestart(Canvas Gamevenster, int randomPlaats)
        {

            //Controleren of de cactussen te dicht bij elkaar staan
            if (_x + _cactusBreedte < 0)
            {
                _x = randomPlaats;
                if(_x - (Gamevenster.Width * 0.25) >= _vorigeCactus * 0.8){
                randomPlaats += Convert.ToInt32(Gamevenster.Width * 1.3);
                _x = randomPlaats;
                }
                _vorigeCactus = _x;
            }
        }

    }
}