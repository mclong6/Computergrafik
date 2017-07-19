using DMS.Geometry;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;

namespace Computergrafik
{
    class Opponent
    {
        private Vector2 opponentVector = new Vector2(1.0f, 0.5f);

        public Opponent() {

        } 
                
        /*
         * Position of Opponent
         */
        public void updatePosition(Box2D opponent)
        {
            //move Opponent
            opponent.X += 1.0f / 120.0f * opponentVector.X;
            opponent.Y += 1.0f / 120.0f * opponentVector.Y;
            
            //reflect Opponent
            if (opponent.MaxY > 1.0f || opponent.Y < -1.0)
            {
                opponentVector.Y = -opponentVector.Y;
            }
            if (opponent.MaxX > 1.0f || opponent.X < -1.0)
            {
                opponentVector.X = -opponentVector.X;
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

