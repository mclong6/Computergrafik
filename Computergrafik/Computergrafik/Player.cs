using DMS.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computergrafik
{
    class Player
    {

        private int     playerNr;
        private Model   model;
        private float   maxSpeed;
        private Box2D   player;


        Player(Model model, int playerNr) {

            this.model      = model;
            this.playerNr   = playerNr;
            this.player     = model.Player[playerNr];
        }
            
        


    }
}
