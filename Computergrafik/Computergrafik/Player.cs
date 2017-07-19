using DMS.Geometry;
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
        private float maxSpeed;
        private Box2D player;
        private GamePadState currentControllerState;
        private GamePadThumbSticks thumber;



        public Player(Model model, int playerNr) {

            this.model      = model;
            this.playerNr   = playerNr;
            this.player     = model.Player[playerNr];
        }


        public void updatePosition()
        {
            getControllerState();


        }

        private void getControllerState() {

            currentControllerState = GamePad.GetState(playerNr);
            thumber = currentControllerState.ThumbSticks;

            player.CenterX = thumber.Left.X;
            player.CenterY = thumber.Left.Y;


        }


    }
}
