using DMS.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computergrafik
{
    class Model
    {
        Box2D window;
        Box2D startScreen;
        Box2D startButton;
        Box2D endButton;
        Box2D selectButton;
        Box2D[] obstacles   = new Box2D[5];
        Box2D[] player      = new Box2D[2];
        Box2D[] playerGun   = new Box2D[2];
        Box2D[] opponent    = new Box2D[2];
        private float minus = -1.0f;
        

        Box2D playerInfoOne;
        Box2D playerInfoTwo;
        public Model()
        {
            Window          = new Box2D(-1.0f, -1.0f, 2.0f, 2.0f);

            /*Obstacle*/
            obstacles[0] = new Box2D(-0.4f, -1.0f, 0.2f, 0.4f);
            obstacles[1] = new Box2D(0.6f, -0.7f, 0.1f, 0.2f);
            obstacles[2] = new Box2D(0.4f, 0.4f, 0.1f, 0.2f);
            obstacles[3] = new Box2D(-0.4f, 0.4f, 0.05f, 0.2f);
            obstacles[4] = new Box2D(-0.1f, -0.2f, 0.3f, 0.3f);
          
            /*Alle Boxen fürs Menü*/
            startScreen     = new Box2D(-1.0f, -1.0f, 2.0f, 2.0f);
            startButton     = new Box2D(-0.3f, 0.3f, 0.6f, 0.2f);
            endButton       = new Box2D(-0.3f, -0.1f, 0.6f, 0.2f);
            selectButton    = new Box2D(-0.4f, 0.2f, 0.8f, 0.4f);

            /*PlayerInfoBox*/

            playerInfoOne = new Box2D(-1.0f, 0.7f, 0.4f, 0.3f);
            playerInfoTwo = new Box2D(0.6f, 0.7f, 0.4f, 0.3f);
            /*Boxen fürs Spiel*/
            opponent[0]     = new Box2D(0.0f, 0.7f, 0.2f, 0.2f);

            for(int index = 0; index<2; index++)
            {
                float xTerm = 0.7f * minus;
                player[index] = new Box2D(xTerm, 0f, 0.1f, 0.1f);
                PlayerGun[index] = new Box2D(xTerm, 0f, 0.1f, 0.05f);
                minus = minus * minus;
            }

            
        }

      

        public Box2D[] Obstacles
        {
            get
            {return obstacles;}

            set
            {obstacles = value;}
        }

        public Box2D[] Player
        {
            get
            {return player;}

            set
            {player = value;}
        }

        public Box2D[] Opponent
        {
            get
            {return opponent;}

            set
            {opponent = value;}
        }


        /*Getter und Setter für Menü*/
        public Box2D EndButton
        {
            get
            { return endButton;}

            set
            {endButton = value;}
        }

        public Box2D SelectButton
        {
            get
            {return selectButton;}

            set
            {selectButton = value;}
        }
        public Box2D StartScreen
        {
            get
            { return startScreen; }

            set
            { startScreen = value; }
        }

        public Box2D StartButton
        {
            get
            { return startButton; }

            set
            { startButton = value; }
        }

        public Box2D[] PlayerGun
        {
            get
            {return playerGun;}

            set
            { playerGun = value;}
        }

        /*Player Info */
        public Box2D PlayerInfoOne
        {
            get
            {return playerInfoOne;}

            set
            {playerInfoOne = value;}
        }

        public Box2D PlayerInfoTwo
        {
            get
            { return playerInfoTwo;}

            set
            { playerInfoTwo = value; }
        }

        public Box2D Window
        {
            get
            {
                return window;
            }

            set
            {
                window = value;
            }
        }

     
    }
}
