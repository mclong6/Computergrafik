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

        }

        public void updateLogic()
        {
            this.player[0].updatePosition();
            this.player[1].updatePosition();




        }
    }
}
