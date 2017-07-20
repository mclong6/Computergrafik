using DMS.Geometry;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;

namespace Computergrafik
{
    class Opponent
    {
        private Vector2 opponentVector = new Vector2(1.0f, -0.3f);
        private Model myModel;
        private float minus = -1.0f;
        private bool test = true;

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
           
            for (int i = 0; i < myModel.Player.Length; i++)
            {
                if (myModel.Player[i].Intersects(opponent))
                {
                    controlIntersects(myModel.Player[i], opponent);
                }
             }
            if (myModel.PlayerInfoOne.Intersects(opponent))
            {
                controlIntersects(myModel.PlayerInfoOne, opponent);
            }
            if (myModel.PlayerInfoTwo.Intersects(opponent))
            {
                controlIntersects(myModel.PlayerInfoTwo, opponent);
            }

        }

        private void controlIntersects(Box2D obstacle, Box2D opponent)
        {
            if ((opponent.CenterX > obstacle.MaxX || opponent.CenterX < (obstacle.MaxX - obstacle.SizeX)) &&
                           (opponent.CenterY > obstacle.MaxY || opponent.CenterY < (obstacle.MaxY - obstacle.SizeY)))
            {
                Console.WriteLine("Kollision");
                opponentVector.Y = opponentVector.Y * minus;
                opponentVector.X = opponentVector.X * minus;
                Console.WriteLine("111");
            }


            //Collision from underneath and above
            else if (opponent.CenterX > (obstacle.CenterX - (obstacle.SizeX / 2)) &&
                    opponent.CenterX < (obstacle.CenterX + (obstacle.SizeX / 2)))
            {
                opponentVector.Y = opponentVector.Y * minus;
                Console.WriteLine("Y wert wird verändern");
            }
            //Collision from the left and right
            else if (opponent.CenterY > (obstacle.CenterY - (obstacle.SizeY / 2)) &&
                    opponent.CenterY < (obstacle.CenterY + (obstacle.SizeY / 2)))
            {
                opponentVector.X = opponentVector.X * minus;
                Console.WriteLine("X wert wird verändern");
            }
        }
    }

    }


