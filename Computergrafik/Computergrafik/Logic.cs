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
        Player[] player = new Player[2]; 

        public Logic(Model model) {

            this.model = model;
            this.player[0] = new Player(model, 0);
            this.player[1] = new Player(model, 1);

        }

        public void updateLogic()
        {
            this.player[0].updatePosition();
            this.player[1].updatePosition();

        }
    }
}
