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
        
            //var memStream = new MemoryStream(Resource2.laser_Eins);
             //soundEngine.PlaySound(memStream);
        }

      
        public void ReloadTheGun()
        {
           // soundEngine.PlaySound(Resource2.reloadGun);
       
        }

       

        public void doBoost()
        {
         //   soundEngine.PlaySound(Resource2.boost);
          
        }

        public void doHurt()
        {
            soundEngine.PlaySound(Resource2.hurt);
        
        }

        protected override void DisposeResources()
        {
             soundEngine.Dispose();
        }

        private AudioPlaybackEngine soundEngine;
    }


}
