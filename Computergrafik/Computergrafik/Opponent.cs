using DMS.Geometry;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;

namespace Computergrafik
{
    class Opponent
    {
        private Vector2 opponentVector = new Vector2(1.0f, -1.0f);
        private Model myModel;
        private float minus = -1.0f;
        private bool intersectsIsTrue = false;

        public Opponent(Model model)
        {
            myModel = model;
        }

        /*
         * Position of Opponent
         */
        public void updatePosition(Box2D opponent)
        {
            //move Opponent
           
            opponent.X += 1.0f / 200.0f * opponentVector.X;
            opponent.Y += 1.0f / 200.0f * opponentVector.Y;

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

            /*
             *!!!!!!!!!!!!!!!!!!!!!! Query to follow Player!!!! Vector is needed!!!!!!!!!!!!!!!!!!!!!!!!
             * 
             * if (((myModel.Player[0].CenterX-opponent.CenterX)<0.3f)&&((myModel.Player[0].CenterY - opponent.CenterY) < 0.3f)&&!intersectsIsTrue)
            {
                opponentVector.X = myModel.Player[0].CenterX;
                opponentVector.Y = myModel.Player[0].CenterY;
            }
            */
        }

        private void controlIntersects(Box2D obstacle, Box2D opponent)
        {
            intersectsIsTrue = true;
            bool under = opponent.CenterY < obstacle.CenterY;
            bool above = opponent.CenterY > obstacle.CenterY;
            bool right = opponent.CenterX > obstacle.MaxX;
            bool left = opponent.CenterX < (obstacle.CenterX - (obstacle.SizeX / 2));

            //Query if it is an Corner-Collision
            if ((opponent.CenterX > obstacle.MaxX || opponent.CenterX < (obstacle.MaxX - obstacle.SizeX)) &&
                           (opponent.CenterY > obstacle.MaxY || opponent.CenterY < (obstacle.MaxY - obstacle.SizeY)))
            {
                //Corner-Collision above the centrum
                if (above)
                {
                    //Vector minus and plus
                    if ((opponentVector.Y < 0 && opponentVector.X > 0))
                    {
                        if (left)
                        {opponentVector.Y = opponentVector.Y * minus;
                            opponentVector.X = opponentVector.X * minus;}
                        if (right)
                        {opponentVector.Y = opponentVector.Y * minus;}
                    }
                    //Vector minus and minus
                    else if ((opponentVector.Y < 0 && opponentVector.X < 0))
                    {
                        if (right)
                        {   opponentVector.Y = opponentVector.Y * minus;
                            opponentVector.X = opponentVector.X * minus;}

                        if (left)
                        {opponentVector.Y = opponentVector.Y * minus;}
                    }
                    //Vector plus and plus
                    else if (opponentVector.Y > 0 && opponentVector.X > 0)
                    {
                        if (left)
                        { opponentVector.X = opponentVector.X * minus; }
                    }
                    //Vector plus and minus
                    else if (opponentVector.Y > 0 && opponentVector.X < 0)
                    {
                        if (right)
                        { opponentVector.X = opponentVector.X * minus;}
                    }
                }

                //Corner-Collision under the centrum
                else if (under)
                {
                    //Vector minus and plus
                    if ((opponentVector.Y < 0 && opponentVector.X > 0))
                    {
                        if (left)
                        { opponentVector.X = opponentVector.X * minus;}
                       
                    }
                    //Vector minus and minus
                    else if ((opponentVector.Y < 0 && opponentVector.X < 0))
                    {
                        if (right)
                        {opponentVector.X = opponentVector.X * minus;}
                    }
                    //Vector plus and plus
                    else if (opponentVector.Y > 0 && opponentVector.X > 0)
                    {
                        if (left)
                        {opponentVector.X = opponentVector.X * minus;
                            opponentVector.Y = opponentVector.Y * minus;}

                        if (right)
                        {opponentVector.Y = opponentVector.Y * minus;}
                    }
                    //Vector plus and minus
                    else if (opponentVector.Y > 0 && opponentVector.X < 0)
                    {
                        if (right)
                        {opponentVector.X = opponentVector.X * minus;
                            opponentVector.Y = opponentVector.Y * minus;}

                        if (left)
                        {opponentVector.Y = opponentVector.Y * minus;}
                    }
                }
            }

            //Collision from underneath and above
            else if (opponent.CenterX > (obstacle.CenterX - (obstacle.SizeX / 2)) &&
                    opponent.CenterX < (obstacle.CenterX + (obstacle.SizeX / 2)))
            {
                Console.WriteLine("Kollision oben oder unten:" + opponentVector.Y);
                opponentVector.Y = opponentVector.Y * minus;
            }

            //Collision from the left and right
            else if (opponent.CenterY > (obstacle.CenterY - (obstacle.SizeY / 2)) &&
                    opponent.CenterY < (obstacle.CenterY + (obstacle.SizeY / 2)))
            {
                opponentVector.X = opponentVector.X * minus;
                Console.WriteLine("Kollision rechts oder links:" + opponentVector.X);
            }
            intersectsIsTrue = false;
        }
    }
}


