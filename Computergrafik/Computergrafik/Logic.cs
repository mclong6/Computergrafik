using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computergrafik
{
    class Logic
    {
        Model model;
        Player[] player     = new Player[2];
        Opponent[] opponent = new Opponent[2];

    

        public Logic(Model model) {

            this.model = model;
            this.Player[0] = new Player(model, 0);
            this.Player[1] = new Player(model, 1);
            this.opponent[0] = new Opponent(model);

        }

        public void updateLogic()
        {
            this.Player[0].updatePosition();
            this.Player[1].updatePosition();

        }

        public void updateOpponent()
        {
            this.opponent[0].updatePosition(model.Opponent[0]);
        }

        internal Player[] Player
        {
            get
            {
                return player;
            }

            set
            {
                player = value;
            }
        }
    }
}
