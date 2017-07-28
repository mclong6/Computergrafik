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

namespace Computergrafik
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
        
           // var memStream = new MemoryStream(Resourcen.EVAXDaughters);
            //soundEngine.PlaySound(memStream);
        }

        public void ShootLaserOne()
        {
            soundEngine.PlaySound(Resource2.laser_Eins);
        }

      
        public void ReloadTheGun()
        {
            soundEngine.PlaySound(Resource2.reloadNew);
       
        }

       

        public void doBoost()
        {
            // soundEngine.PlaySound(Resource2.UFO_Takeoff);
           // soundEngine.PlaySound(Resource2.BoostNew3);

        }

        public void endGame()
        {
         //   soundEngine.PlaySound(Resource2.Sports_Crowd);
        }



        public void startGame()
        {
            soundEngine.PlaySound(Resource2.airhorn);

        }
        public void driveOpponent()
        {
            soundEngine.PlaySound(Resource2.Balloon_Popping);

        }

        public void doHurt()
        {
            //soundEngine.PlaySound(Resource2.hurt);
            soundEngine.PlaySound(Resource2.Metal_Clang);
        }

        protected override void DisposeResources()
        {
             soundEngine.Dispose();
        }

        private AudioPlaybackEngine soundEngine;
    }


}
