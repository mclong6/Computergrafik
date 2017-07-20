using DMS.Geometry;
using OpenTK;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Computergrafik
{
    class Player
    {

        private int playerNr;
        private Model model;
        private float speed = 0.006f;
        private Box2D player;
        private Box2D gun;
        private float JS1x;
        private float JS1y;
        private GamePadState currentControllerState;
        private GamePadThumbSticks thumber;
        private Vector2 direcction;
        private Vector2 gunDirection;
        private Boolean shootcontrol = true;


        public Player(Model model, int playerNr) {

            this.model          = model;
            this.playerNr       = playerNr;
            this.player         = model.Player[playerNr];
            this.gun            = model.PlayerGun[playerNr];
            this.direcction     = new Vector2(0f, 0f);
            this.gunDirection   = new Vector2(0f,0f);
        }


        public void updatePosition()        //Updates the position of the player
        {
            getControllerState();
            boostPressed();
            shootPressed();
            calculatePlayerPosition();
            moveGun();
        }

        private void getControllerState() {         // takes the current controller state 

            currentControllerState = GamePad.GetState(playerNr);
            thumber = currentControllerState.ThumbSticks;

            direcction.X = thumber.Left.X;          // movemend dierection
            direcction.Y = thumber.Left.Y;

            gunDirection.X = thumber.Right.X;       // gun direction
            gunDirection.Y = thumber.Right.Y;

            if (direcction.X < 0.2f && direcction.X > -0.2f) direcction.X = 0; // da Joysticks nie genau 0
            if (direcction.Y < 0.2f && direcction.Y > -0.2f) direcction.Y = 0; // da Joysticks nie genau 

            if (gunDirection.X < 0.2f && gunDirection.X > -0.2f) gunDirection.X = 0; // da Joysticks nie genau 0
            if (gunDirection.Y < 0.2f && gunDirection.Y > -0.2f) gunDirection.Y = 0; // da Joysticks nie genau 0

        }

        private void calculatePlayerPosition() {

            if (direcction.Length > 1f)
            {
                direcction.NormalizeFast();
            }

            player.CenterX = player.CenterX + (direcction.X * speed);
            player.CenterY = player.CenterY + (direcction.Y * speed);

        }

        private void boostPressed()             // if post is presst speed will rise 
        {

            if (currentControllerState.Triggers.Right == 1.0f)
            {

                speed = 0.02f;
            }

            if (currentControllerState.Triggers.Right == 0.0f)
            {
                speed = 0.006f;
            }


        }

        private void shootPressed()             // if post is presst speed will rise 
        {

            if (currentControllerState.Triggers.Left == 1.0f && shootcontrol == true)
            {
                shoot(1,1);
                shootcontrol = false;
            }

            if (currentControllerState.Triggers.Left == 0.0f)
            {
                shootcontrol = true;
            }


        }



        private void moveGun() {


            
        }

        private void shoot(int x, int y)
        {
            Bullet bullet = new Bullet(x,y);
            Thread bulledThread = new Thread(bullet.fligh);
            bulledThread.Start();

        }


    }
}
