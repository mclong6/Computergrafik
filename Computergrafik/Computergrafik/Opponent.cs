using DMS.Geometry;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;

namespace Computergrafik
{
    class Opponent
    {
        private Vector2 opponentVector = new Vector2(0.0f, -1.0f);
        private Model myModel;

        public Opponent(Model model) {
            myModel = model;
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
            for (int i=0; i<myModel.Player.Length; i++)
            {
                if (myModel.Player[i].Intersects(opponent))
                {
                    if (opponent.CenterX >= (myModel.Player[i].CenterX - (myModel.Player[i].MaxX / 2))||
                        opponent.CenterX <= (myModel.Player[i].CenterX +(myModel.Player[i].MaxX / 2)))
                    {
                        opponentVector.Y = opponentVector.Y * (-1.0f);
                        Console.WriteLine(opponentVector.Y);
                    }
                    
                            /*opponentVector.Y = obstacleOponentResponseY(myModel.Player[i], opponent);
                    //opponentVector.X = obstacleOponentResponseY(myModel.Player[i], opponent);
                    opponentVector.X = 1.0f;*/
                }

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
        private float obstacleOponentResponseY(Box2D player, Box2D opponent)
        {
            float vectorY = (player.CenterY - opponent.CenterY) / (0.5f * player.SizeY);
            vectorY = OpenTK.MathHelper.Clamp(vectorY, -2.0f, 2.0f);
            return vectorY;
        }
        private float obstacleOponentResponseX(Box2D player, Box2D opponent)
        {
            float vectorX = (player.CenterX - opponent.CenterX) / (0.5f * player.SizeX);
            vectorX = OpenTK.MathHelper.Clamp(vectorX, -2.0f, 2.0f);
            return vectorX;
        }
    }
}

