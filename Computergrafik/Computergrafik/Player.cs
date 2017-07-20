using DMS.Geometry;
using OpenTK;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computergrafik
{
    class Player
    {

        private int playerNr;
        private Model model;
        private float speed = 0.006f;
        private Box2D player;
        private float JS1x;
        private float JS1y;
        private GamePadState currentControllerState;
        private GamePadThumbSticks thumber;
        private Vector2 direcction;


        public Player(Model model, int playerNr) {

            this.model      = model;
            this.playerNr   = playerNr;
            this.player     = model.Player[playerNr];
            this.direcction = new Vector2(0f, 0f);
        }


        public void updatePosition()
        {
            getControllerState();
            calculatePlayerPosition();

        }

        private void getControllerState() {

            currentControllerState = GamePad.GetState(playerNr);
            thumber = currentControllerState.ThumbSticks;

            direcction.X = thumber.Left.X;
            direcction.Y = thumber.Left.Y;

            if (direcction.X < 0.2f && direcction.X > -0.2f) direcction.X = 0; // da Joysticks nie genau 0
            if (direcction.Y < 0.2f && direcction.Y > -0.2f) direcction.Y = 0; // da Joysticks nie genau 0

        }

        private void calculatePlayerPosition() {

            if (direcction.Length > 1f)
            {
                direcction.NormalizeFast();
            }

           if(currentControllerState.Buttons.X == ButtonState.Pressed){

                speed = 0.02f;
            }

            if (currentControllerState.Buttons.X == ButtonState.Released)
            {
                speed = 0.006f;
            }

            player.CenterX = player.CenterX + (direcction.X * speed);
            player.CenterY = player.CenterY + (direcction.Y * speed);

        }


    }
}
