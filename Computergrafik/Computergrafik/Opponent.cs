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
                  
                   
                }

                }
            }
        }

    }


