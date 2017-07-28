﻿using DMS.Geometry;
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
        Box2D[] menuButton = new Box2D[4];
        Box2D selectButton;

        Box2D[] obstacles   = new Box2D[5];
        Box2D[] player      = new Box2D[2];
     
        Box2D[] opponent    = new Box2D[2];

        Box2D[] saveZone = new Box2D[2];
        private float minus = -1.0f;
        

        Box2D playerInfoOne;
        Box2D playerInfoTwo;
        public Model()
        {
            Window       = new Box2D(-1.0f, -1.0f, 2.0f, 2.0f);

            /*Default Settings*/
            obstacles[0] = new Box2D(-0.4f, -1.0f, 0.2f, 0.4f);
            obstacles[1] = new Box2D(0.6f, -0.7f, 0.1f, 0.2f);
            obstacles[2] = new Box2D(0.4f, 0.4f, 0.1f, 0.2f);
            obstacles[3] = new Box2D(-0.4f, 0.4f, 0.05f, 0.2f);
            obstacles[4] = new Box2D(-0.1f, -0.2f, 0.3f, 0.3f);

            /*Alle Boxen fürs Menü*/
            startScreen     = new Box2D(-1.0f, -1.0f, 2.0f, 2.0f);
            float high = 0.5f;

            for (int i = 0; i < menuButton.Length; i++) {
                menuButton[i] = new Box2D(-0.3f, high - (i * 0.4f), 0.6f, 0.2f);
            }

            selectButton    = new Box2D(-0.4f, high -0.1f, 0.8f, 0.4f);

            /*PlayerInfoBox*/

            playerInfoOne = new Box2D(-1.0f, 0.7f, 0.4f, 0.3f);
            playerInfoTwo = new Box2D(0.6f, 0.7f, 0.4f, 0.3f);
            /*Boxen fürs Spiel*/
            opponent[0]     = new Box2D(0.0f, 0.7f, 0.2f, 0.2f);

            for(int index = 0; index<2; index++)
            {
                float xTerm = 0.7f * minus;
                float xTerm2 = -0.975f + index * 1.85f;
                player[index] = new Box2D(xTerm2, 0f-0.03f, 0.06f, 0.06f);
              
                minus = minus * minus;
            }

            for (int i = 0; i < saveZone.Length; i++)
            {
                saveZone[i] = new Box2D(-1.0f + i * 1.8f,-0.1f, 0.2f, 0.2f);
            }

        }
        public void createLevel(int level)
        {
            if (level == 0)
            {             /*Level 1*/
                obstacles[0] = new Box2D(-0.6f, -1.0f, 0.2f, 0.4f);
                obstacles[1] = new Box2D(0.3f, -0.7f, 0.1f, 0.2f);
                obstacles[2] = new Box2D(0.2f, 0.5f, 0.1f, 0.2f);
                obstacles[3] = new Box2D(-0.5f, 0.2f, 0.05f, 0.2f);
                obstacles[4] = new Box2D(-0.1f, -0.2f, 0.3f, 0.3f);
            }

            if (level == 1)
            {
                /*Level 2*/
                obstacles[0] = new Box2D(-0.4f, -1.0f, 0.1f, 0.1f);
                obstacles[1] = new Box2D(0.6f, -0.7f, 0.1f, 0.1f);
                obstacles[2] = new Box2D(0.4f, 0.4f, 0.1f, 0.1f);
                obstacles[3] = new Box2D(-0.5f, 0.3f, 0.1f, 0.1f);
                obstacles[4] = new Box2D(-0.1f, -0.2f, 0.1f, 0.1f);
            }
            if (level == 2)
            {
                /*Level 3*/
                obstacles[0] = new Box2D(-0.4f, -1.0f, 0.1f, 0.1f);
                obstacles[1] = new Box2D(0.6f, -0.7f, 0.1f, 0.1f);
                obstacles[2] = new Box2D(0.4f, 0.4f, 0.01f, 0.5f);
                obstacles[3] = new Box2D(-0.4f, 0.4f, 0.1f, 0.1f);
                obstacles[4] = new Box2D(-0.1f, -0.2f, 0.01f, 0.7f);
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

        public Box2D[] MenuButton
        {
            get
            {
                return menuButton;
            }

            set
            {
                menuButton = value;
            }
        }

        public Box2D[] SaveZone
        {
            get
            {
                return saveZone;
            }

            set
            {
                saveZone = value;
            }
        }
    }
}
