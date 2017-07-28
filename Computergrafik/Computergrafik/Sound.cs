using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

using DMS.Sound;
using DMS.Base;
using DMS.OpenGL;
using System.Drawing;
using Computergrafik;

namespace ConsoleApplication1.Main
{

   
    public class Sound : Disposable
    {
        public Sound()
        {
            soundEngine = new AudioPlaybackEngine();
     
        }

        public void Background()
        {
           // soundEngine.PlaySound(Resourcen.backsound, true);
            /*1.Parameter: gibt Sound-datei an
             *2.Parameter: bei true -> endlosSchleife*/
        }

        public void DestroyEnemy()
        {
           // soundEngine.PlaySound(Resourcen.collisionsschrei);
            //var memStream = new MemoryStream(Resourcen.EVAXDaughters);
            //soundEngine.PlaySound(memStream);
        }

        public void Lost()
        {
        }

        public void Shoot()
        {
            //   soundEngine.PlaySound(Resourcen.laser);
        }
        public void ShootLaserOne()
        {
              soundEngine.PlaySound(Resource2.Laser_Eins);
        }

        public void ShootLaserTwo()
        {
            soundEngine.PlaySound(Resource2.Laser_Zwei);
        }
        public void ReloadGun()
        {
            soundEngine.PlaySound(Resource2.Laser_Zwei);
        }


        protected override void DisposeResources()
        {
             soundEngine.Dispose();
        }

        private AudioPlaybackEngine soundEngine;
    }


}
