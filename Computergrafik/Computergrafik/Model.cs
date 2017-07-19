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
        Box2D[] obstacles = new Box2D[5];
        Box2D[] player = new Box2D[2];
        Box2D[] opponent = new Box2D[2];

        public Model()
        {

            StartScreen = new Box2D(-1.0f, -1.0f, 2.0f, 2.0f);
            StartButton = new Box2D(0.3f, 0.3f, 0.6f, 0.2f);

        }

        public Box2D StartScreen
        {
            get
            { return startScreen;}

            set
            {startScreen = value;}
        }

        public Box2D StartButton
        {
            get
            {return startButton;}

            set
            { startButton = value;}
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

        public Box2D[] Oponent
        {
            get
            {return opponent;}

            set
            {opponent = value;}
        }
    }
}
