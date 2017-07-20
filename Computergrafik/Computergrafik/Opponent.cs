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
                    Console.WriteLine("Kollision");
                    float x1 = opponent.CenterX;
                    float y1 = opponent.CenterY;
                    float x2 = myModel.Player[i].CenterX;
                    float y2 = myModel.Player[i].CenterX;
                    float sum = ((x2 - x1) * (x2 - x1)) + ((y2 - y1) * (y2 - y1));
                    float radius = opponent.SizeX / 2;
                    float halfWidth = myModel.Player[i].SizeX / 2;              // Only if all sides same lenght
                    float distance = radius + halfWidth;

                   /* if (Math.Sqrt(sum)> distance &&)
                    {
                        opponentVector.Y = opponentVector.Y * minus;
                        opponentVector.X = opponentVector.X * minus;
                    }*/
                    if ((opponent.CenterX > myModel.Player[i].MaxX || opponent.CenterX < (myModel.Player[i].MaxX - myModel.Player[i].SizeX)) &&
                        (opponent.CenterY > myModel.Player[i].MaxY || opponent.CenterY < (myModel.Player[i].MaxY - myModel.Player[i].SizeY)))
                    {
                        Console.WriteLine("Kollision");
                        opponentVector.Y = opponentVector.Y * minus;
                        opponentVector.X = opponentVector.X * minus;
                        Console.WriteLine("111");
                    }
                   

                    //Collision from underneath and above
                    else if (opponent.CenterX > (myModel.Player[i].CenterX - (myModel.Player[i].SizeX / 2)) &&
                            opponent.CenterX < (myModel.Player[i].CenterX + (myModel.Player[i].SizeX / 2)))
                        {
                            opponentVector.Y = opponentVector.Y * minus;
                            Console.WriteLine("Y wert wird verändern");
                        }
                        //Collision from the left and right
                    else if (opponent.CenterY > (myModel.Player[i].CenterY - (myModel.Player[i].SizeY / 2)) &&
                            opponent.CenterY < (myModel.Player[i].CenterY + (myModel.Player[i].SizeY / 2)))
                        {
                        opponentVector.X = opponentVector.X * minus;
                        Console.WriteLine("X wert wird verändern");
                        }
                   /* else if ((
                        opponent.CenterY>myModel.Player[i].MaxY || opponent.CenterY <myModel.Player[i].MaxY) &&
                        (opponent.CenterX > myModel.Player[i].MaxX || opponent.CenterX < myModel.Player[i].MaxX))
                    {
                        Console.WriteLine("ELSEEEEEEEE");
                        opponentVector.Y = opponentVector.Y * minus;
                        opponentVector.X= opponentVector.X * minus;
                    }*/
                   
                }

                }
            }
        }
/*
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
        }*/
    }


