﻿using DMS.Geometry;
using OpenTK;
using System;
using System.Timers;

namespace Computergrafik
{
    class Opponent
    {
        private Vector2 opponentVector = new Vector2(1.0f, -1.0f);
        private Model myModel;
        private float minus = -1.0f;
        private bool intersectsIsTrue = false;
        private Logic myLogic;
        private float interval = 1000;  //one second
        private float speedOpponent = 0.003f;
        private Timer timer;
        private int intersectsCounter = 0;
        private float timerCounter = 0;
        private bool intersectTest = false;
       

        public Opponent(Model model, Logic logic)
        {
            this.myModel = model;
            this.myLogic = logic;
            this.timer = new Timer();
            this.timer.Interval = interval;
            this.timer.Elapsed += OnTimedEvent;
            this.timer.AutoReset = true;
            this.timer.Enabled = true;
        }

        /*
         * Position of Opponent
         */
        public void updatePosition(Box2D opponent)
        {

            openIntersects(opponent);

            /*****************************Mit ARRAY***************************************/
            bool[] array = new bool[2];

            for(int i=0; i < myModel.Player.Length; i++)
            {
                if (((Math.Abs(myModel.Player[i].CenterX - opponent.CenterX)) < 0.4f) &&
                                    (Math.Abs(myModel.Player[i].CenterY - opponent.CenterY) < 0.4f) &&
                                    !queryIntersects())
                {
                    array[i] = true;
                }
            }
            if (array[0] == true && array[1] == false)
            {
                followPlayer(0, opponent);
            }

            else if (array[0] == false && array[1] == true)
            {
                followPlayer(1, opponent);
            }

            else if (array[0] == true && array[1] == true)
            {
                float differenceOne = (Math.Abs(myModel.Player[0].CenterX - opponent.CenterX)) +
                    (Math.Abs(myModel.Player[0].CenterY - opponent.CenterY));
                float differenceTwo = (Math.Abs(myModel.Player[1].CenterX - opponent.CenterX)) +
                    (Math.Abs(myModel.Player[1].CenterY - opponent.CenterY));

                if (differenceOne < differenceTwo)
                {
                    followPlayer(0, opponent);
                }
                else
                {
                    followPlayer(1, opponent);
                }

            }
            else
            {
                //move Opponent
                opponent.X += (speedOpponent * opponentVector.X);
                opponent.Y += (speedOpponent * opponentVector.Y);
            }

        }

        private void followPlayer(int i, Box2D opponent) {
            float pitch;
            float y2 = myModel.Player[i].CenterY;
            float y1 = opponent.CenterY;
            float x2 = myModel.Player[i].CenterX;
            float x1 = opponent.CenterX;

            pitch = (y2 - y1) / (x2 - x1);
            Vector2 steigungm = new Vector2((x2 - x1), (y2 - y1));
            steigungm.Normalize();

            opponent.X += speedOpponent * steigungm.X;
            opponent.Y += speedOpponent  * steigungm.Y;
        }

        private void openIntersects(Box2D opponent)
        {
            intersectTest = false;
            //reflect Opponent
            if (opponent.MaxY > 1.0f || opponent.Y < -1.0)
            {
               intersectTest = true;
                opponentVector.Y = -opponentVector.Y;
            }
            if (opponent.MaxX > 1.0f || opponent.X < -1.0)
            {
                intersectTest = true;
                opponentVector.X = -opponentVector.X;
            }

            //Collision with Player
            for (int i = 0; i < myModel.Player.Length; i++)
            {
                if (myModel.Player[i].Intersects(opponent))
                {
                    controlIntersects(myModel.Player[i], opponent);
                }
            }
            //Collision with SaveZone
            for (int i = 0; i < myModel.SaveZone.Length; i++)
            {
                if (myModel.SaveZone[i].Intersects(opponent))
                {
                    intersectTest = true;
                    controlIntersects(myModel.SaveZone[i], opponent);
                }
                
            }

            //Collision with Obstacles
            for (int i = 0; i < myModel.Obstacles.Length; i++)
            {
                if (myModel.Obstacles[i].Intersects(opponent))
                {
                    intersectTest = true;
                    
                    controlIntersects(myModel.Obstacles[i], opponent);
                }
            }

            //Collision with PlayerInfoOne-Box
            if (myModel.PlayerInfoOne.Intersects(opponent))
            {
                intersectTest = true;
                controlIntersects(myModel.PlayerInfoOne, opponent);
            }

            //Collision with PlayerInfoOne-Box
            if (myModel.PlayerInfoTwo.Intersects(opponent))
            {
                intersectTest = true;
                controlIntersects(myModel.PlayerInfoTwo, opponent);
            }
           

        }
        private bool queryIntersects()
        {
            if (intersectTest)
            {
                intersectsIsTrue = true;
                timerCounter = 0;
            }
           /* else
            {
                timerCounter = 3;
            }*/
            return intersectsIsTrue;
        }

        private void controlIntersects(Box2D obstacle, Box2D opponent)
        {
            
            intersectsCounter += 1;
            bool under = opponent.CenterY < obstacle.CenterY;
            bool above = opponent.CenterY > obstacle.CenterY;
            bool right = opponent.CenterX > obstacle.MaxX;
          //  bool left = opponent.CenterX < (obstacle.CenterX - (obstacle.SizeX / 2));
            bool left = opponent.CenterX < obstacle.CenterX;
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
                        else if (right)
                        {opponentVector.Y = opponentVector.Y * minus;}
                    }
                    //Vector minus and minus
                    else if ((opponentVector.Y < 0 && opponentVector.X < 0))
                    {
                        if (right)
                        {   opponentVector.Y = opponentVector.Y * minus;
                            opponentVector.X = opponentVector.X * minus;}

                        else if (left)
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
                //Collision Opponent above
                if (above&& !(opponentVector.Y >0))
                {
                    opponentVector.Y = opponentVector.Y * minus;
                }
                //Collision Opponent underneath
                else if (under && !(opponentVector.Y < 0))
                {
                    opponentVector.Y = opponentVector.Y * minus;
                }

            }

            //Collision from the left and right
            else if (opponent.CenterY > (obstacle.CenterY - (obstacle.SizeY / 2)) &&
                    opponent.CenterY < (obstacle.CenterY + (obstacle.SizeY / 2)))
            {
                //Collision Opponent right
                if (right && !(opponentVector.X > 0))
                {
                    opponentVector.X = opponentVector.X * minus;
                }
                //Collision Opponent left
                else if (left && !(opponentVector.X < 0))
                {
                    opponentVector.X = opponentVector.X * minus;
                }
            }
        }
       private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            if (timerCounter<=1)
            {
                intersectsIsTrue = true;
            }

            else
            {
                intersectsIsTrue = false;
            }
            timerCounter += 1;
        }
    }
}


