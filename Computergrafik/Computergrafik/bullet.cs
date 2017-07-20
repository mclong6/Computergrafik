using DMS.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computergrafik
{
    class Bullet
    {

        float x;
        float y;
        Box2D bullet;

        public Bullet(float x, float y) {

            this.x = x;
            this.y = y;
           

        }

        public void fligh() {

            Console.WriteLine("shoot");

        }


    }
}
