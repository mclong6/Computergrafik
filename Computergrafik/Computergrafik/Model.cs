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
        Box2D startScreen;
        Box2D startButton;
        Box2D endButton;
        Box2D selectButton;
        Box2D[] obstacles   = new Box2D[5];
        Box2D[] player      = new Box2D[2];
        Box2D[] opponent    = new Box2D[2];

        public Model()
        {
            /*Alle Boxen fürs Menü*/
            startScreen     = new Box2D(-1.0f, -1.0f, 2.0f, 2.0f);
            startButton     = new Box2D(-0.3f, 0.3f, 0.6f, 0.2f);
            endButton       = new Box2D(-0.3f, -0.1f, 0.6f, 0.2f);
            selectButton    = new Box2D(-0.4f, 0.2f, 0.8f, 0.4f);

            /*Boxen fürs Spiel*/
            opponent[0]     = new Box2D(0.0f, 0.0f, 0.2f, 0.2f);

            for(int index = 0; index<2; index++)
            {
                player[index] = new Box2D(0f, 0f, 0.1f, 0.1f);
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
    }
}
