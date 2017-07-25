using DMS.Geometry;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using System.Timers;

namespace Computergrafik
{
    class Opponent
    {
        private Vector2 opponentVector = new Vector2(1.0f, -1.0f);
        private Model myModel;
        private float minus = -1.0f;
        private bool intersectsIsTrue = false;
        private bool intersectsTime = false;
        private Logic myLogic;
        private float interval = 100;  //mill second
        private float speedOpponent = 0.003f;
        private Timer timer;
        private int intersectsCounter = 0;
        private int i = 0;

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
            Console.WriteLine(intersectsTime);

            if (intersectsTime == false)
            {
                openIntersects(opponent);
            }
                /*
                *!!!!!!!!!!!!!!!!!!!!!! Query to follow Player!!!! Vector is needed!!!!!!!!!!!!!!!!!!!!!!!!
                * 
                */
                if (((Math.Abs(myModel.Player[0].CenterX - opponent.CenterX)) < 0.4f) && (Math.Abs(myModel.Player[0].CenterY - opponent.CenterY) < 0.4f) && intersectsIsTrue == false && intersectsTime == false)
                {
                    float pitch;
                    float y2 = myModel.Player[0].CenterY;
                    float y1 = opponent.CenterY;
                    float x2 = myModel.Player[0].CenterX;
                    float x1 = opponent.CenterX;

                    pitch = (y2 - y1) / (x2 - x1);
                    Vector2 steigungm = new Vector2((x2 - x1), (y2 - y1));
                    steigungm.Normalize();

                    opponent.X += speedOpponent * steigungm.X;
                    opponent.Y += speedOpponent * steigungm.Y;

                    /*Console.WriteLine("X:" + Math.Abs(myModel.Player[0].CenterX - opponent.CenterX));
                    Console.WriteLine("Y:" + Math.Abs(myModel.Player[0].CenterY - opponent.CenterY));*/
                }
            
            else
            {
                //move Opponent

                opponent.X += speedOpponent * opponentVector.X;
                opponent.Y += speedOpponent * opponentVector.Y;
            }

            intersectsIsTrue = false;
        
        }

        private void openIntersects(Box2D opponent)
        {
           
            //reflect Opponent
            if (opponent.MaxY > 1.0f || opponent.Y < -1.0)
            {
                opponentVector.Y = -opponentVector.Y;
            }
            if (opponent.MaxX > 1.0f || opponent.X < -1.0)
            {
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

            //Collision with Obstacles
            for (int i = 0; i < myModel.Obstacles.Length; i++)
            {
                if (myModel.Obstacles[i].Intersects(opponent))
                {
                    Console.WriteLine("In openIntersects");
                    controlIntersects(myModel.Obstacles[i], opponent);
                }
            }

            //Collision with PlayerInfoOne-Box
            if (myModel.PlayerInfoOne.Intersects(opponent))
            {
                controlIntersects(myModel.PlayerInfoOne, opponent);
            }

            //Collision with PlayerInfoOne-Box
            if (myModel.PlayerInfoTwo.Intersects(opponent))
            {
                controlIntersects(myModel.PlayerInfoTwo, opponent);
            }

        }

        private void controlIntersects(Box2D obstacle, Box2D opponent)
        {
            intersectsIsTrue = true;
            intersectsCounter += 1;
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
                    Console.WriteLine("Kollision oben:" + opponentVector.Y);
                    opponentVector.Y = opponentVector.Y * minus;
                }
                //Collision Opponent underneath
                else if (under && !(opponentVector.Y < 0))
                {
                    Console.WriteLine("Kollision unten:" + opponentVector.Y);
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
                    Console.WriteLine("Kollision rechts:" + opponentVector.X);
                    opponentVector.X = opponentVector.X * minus;
                }
                //Collision Opponent left
                else if (left && !(opponentVector.X < 0))
                {
                    Console.WriteLine("Kollision links:" + opponentVector.Y);
                    opponentVector.X = opponentVector.X * minus;
                }
            }
        }
        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            
            
            if (intersectsCounter >=15)
            {
                Console.WriteLine("Sekunde rum:"+intersectsCounter);
                intersectsTime = true;
                intersectsIsTrue = true;

                i += 1;
            }
            
            if (i >= 10)
            {
                intersectsTime = false;
                intersectsIsTrue = false;
                intersectsCounter = 0;
                i = 0;
            }
            
            

            /* if (shootcontrol == false && ammo >= 0)
             {
                 this.ammo = ammo - 1;
                 shoot(gunDirection.X, gunDirection.Y);
             }*/
        }
    }
}


