using DMS.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computergrafik
{
    class Hurt
    {
        Player[] player = new Player[2];
        Player[] opponent = new Player[2];
    

        Box2D[] playerBox = new Box2D[2];
        Box2D[] opponentBox = new Box2D[2];
        IList<Bullet>[] playerBullets = new IList<Bullet>[2];       

        public Hurt(Player[] pplayer) {
            player = pplayer;

            opponent[0] = player[1];
            opponent[1] = player[0];

            playerBox[0] = player[0].pplayer;
            playerBox[1] = player[1].pplayer;

            opponentBox[0] = opponent[0].pplayer;
            opponentBox[1] = opponent[1].pplayer;

            playerBullets[0] = player[0].Bullets;
            playerBullets[1] = player[1].Bullets;

        }

        public void hurtLogic() {
               hit(player[0].PlayerNr);
            hit(player[1].PlayerNr);
        }

        private void hit(int i)
        {
            if (playerBullets[i].Count > 0)
            {
            
                foreach (Bullet bullet in playerBullets[i])
                {
                    if (bullet.Bbullet.Intersects(opponentBox[i]))
                    {
                        opponent[i].Life = opponent[i].Life - 1;
                    }
                }
             
            }
        }
    }
}
