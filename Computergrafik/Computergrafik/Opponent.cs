using DMS.Geometry;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computergrafik
{
    class Opponent
    {
        private Vector2 ballV = new Vector2(1.0f, 0.0f);

        public Opponent() {

        }   
        
        public void updateOpponent(Box2D opponent)
        {
            //move ball
            opponent.X += 1.0f / 60.0f * ballV.X;
            opponent.Y += 1.0f / 60.0f * ballV.Y;
            //reflect ball
            if (opponent.MaxY > 1.0f || opponent.Y < -1.0)
            {
                ballV.Y = -ballV.Y;
            }
            if (opponent.MaxX > 1.0f || opponent.X < -1.0)
            {
                ballV.X = -ballV.Y;
            }
            /* 
             * 
             * !!!!!!!!!!!!!!Für Obstacle Kollision!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
             * 
             * 
             //paddle vs ball
             if (paddle1.Intersects(ball))
             {
                 ballV.Y = paddleBallResponse(paddle1, ball);
                 ballV.X = 1.0f;
             }
             if (paddle2.Intersects(ball))
             {
                 ballV.Y = paddleBallResponse(paddle2, ball);
                 ballV.X = -1.0f;
             }*/
        } 
    }
}

