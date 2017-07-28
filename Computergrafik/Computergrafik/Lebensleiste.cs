
using DMS.Geometry;
using DMS.OpenGL;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Drawing;
namespace Computergrafik
{
    class Lebensleiste
    {
        private Texture saveZoneBlueTexture;
        private Texture saveZoneRedTexture;
        float strecker = 0.5f;
        private Sound sound;
        Box2D playerInfoOne;
        Box2D playerInfoTwo;

        Box2D playerInfoOneDrin;
        Box2D playerInfoTwoDrin;
        float sizeBalken = 0.28f;
        float sizeBalkenLeer = 0.279f;

        /*Speed beim Ausfladen*/
        float boostLoad = 0.09f;
        float ammoLoad = 0.0025f;

        
        bool[] nachladen = new bool[2];

        //Xposition//YPosition//X-Länge//Y-Länge
        Box2D[] Life = new Box2D[2];
        Box2D[] Ammo = new Box2D[2];
        Box2D[] Boost = new Box2D[2];

        Box2D[] LifeIcon = new Box2D[2];
        Box2D[] AmmoIcon = new Box2D[2];
        Box2D[] BoostIcon = new Box2D[2];
        /*Weiße boxen*/
        Box2D[] LifeW = new Box2D[2];
        Box2D[] AmmoW = new Box2D[2];
        Box2D[] BoostW = new Box2D[2];

        /**W steht für den weißen hintergrund*/
    

        Texture texLife;
        Texture texAmmo;
        Texture texBoost;

        Texture texLifeIcon;
        Texture texAmmoIcon;
        Texture texBoostIcon;


        public Lebensleiste(Model model, Sound psound)
        {

            saveZoneBlueTexture = TextureLoader.FromBitmap(Resource2.blue_Box);
            saveZoneRedTexture = TextureLoader.FromBitmap(Resource2.red_Box);
            sound = psound;
            playerInfoOne = model.PlayerInfoOne;
            playerInfoTwo = model.PlayerInfoTwo;

            texLife = TextureLoader.FromBitmap(Resource2.health_bar);
            texAmmo = TextureLoader.FromBitmap(Resource2.reload_bar);
            texBoost = TextureLoader.FromBitmap(Resource2.jet_bar);
       
        


            texLifeIcon = TextureLoader.FromBitmap(Resource2.lifeIcon);
            texAmmoIcon= TextureLoader.FromBitmap(Resource2.ammoIcon);
            texBoostIcon = TextureLoader.FromBitmap(Resource2.boostIcon);

            playerInfoOneDrin = new Box2D(-1.0f, 0.7f, 0.4f, 0.3f);
            playerInfoTwoDrin = new Box2D(0.6f, 0.7f, 0.4f, 0.3f);

            nachladen[0] = false;
            nachladen[1] = false;

            float randVoll = -0.9f;
            float randNeben = 0.7f;
            float wert = (-randVoll) + randNeben;

            float runter = 0.01f;
            float oben = 0.92f -runter;
            float mitte = 0.82f - runter;
            float unten = 0.72f -runter;

            float high = 0.085f;


            float randIcon = -0.99f;
            float randIconNeben = 0.61f;
            float wert1 = (-randIcon) + randIconNeben;


            float pluso = 0.01f;

            float randLeer = -0.89f - pluso;
            float randLeerNeben = 0.69f +pluso;
            float wert3 = (-randLeer) + randLeerNeben;

            float YsizeLeer = 0.066f;

            float Ysize = 0.085f;

            for (int i = 0; i < 2; i++)
            {

                Life[i] = new Box2D(randVoll + i *wert, oben, sizeBalken, high);
                Ammo[i] = new Box2D(randVoll + i * wert, mitte, sizeBalken, high);
                Boost[i] = new Box2D(randVoll + i * wert, unten, sizeBalken, high);

                LifeW[i] = new Box2D(randLeer + i * wert3, oben + runter, sizeBalkenLeer, YsizeLeer);
                AmmoW[i] = new Box2D(randLeer + i * wert3, mitte + runter, sizeBalkenLeer, YsizeLeer);
                BoostW[i] = new Box2D(randLeer + i * wert3, unten + runter, sizeBalkenLeer, YsizeLeer);

                LifeIcon[i] = new Box2D(randIcon + i * wert1, oben, Ysize, Ysize);
                AmmoIcon[i] = new Box2D(randIcon + i * wert1, mitte, Ysize, Ysize);
                BoostIcon[i] = new Box2D(randIcon + i * wert1, unten, Ysize, Ysize);

            }
   
         

        }


        public void OneLiveDown(Player player) {
            int num = player.PlayerNr;
            //getMaxLife();
            float maxLife = player.StartLife;
            float currentLife = player.Life;
            float lifeDazu = sizeBalken / maxLife;
            if (currentLife >= 0)
            {
                Life[num].SizeX = (lifeDazu * currentLife);
            }
        }

        public void OneGetShoot(Player player) {
            //getMaxShoot()
            int num = player.PlayerNr;
            float maxShoot = player.StartAmmo;
            float currentAmmo = player.Ammo;
            float ammoDazu = sizeBalken / maxShoot;
      

            if(currentAmmo == 0)
            {
                nachladen[num] = true;
            }
            /*Leert die Anzeige*/
            if (nachladen[num] == false)
            {
              
                Ammo[num].SizeX = (ammoDazu * currentAmmo);
                
            }
          
            if (nachladen[num] == true) {
                 Ammo[num].SizeX = Ammo[num].SizeX + ammoLoad;

              

                if (Ammo[num].SizeX > sizeBalken)
                {
                    sound.ReloadTheGun();
                    player.Ammo = maxShoot;
                    nachladen[num] = false;
                  
                 
                }
               

            }
          

        }

        public void OneGetBoost(Player player) {
            //GetMaxBoost
            int num = player.PlayerNr;
            float maxBoost = player.StartBoost;
            float currentBoost =  player.Boost;
            float boostDazu = sizeBalken / maxBoost;

            
                Boost[num].SizeX =  (boostDazu * currentBoost);
            
            if (currentBoost <= maxBoost && Boost[num].SizeX <= sizeBalken) {

                player.Boost = currentBoost  +boostLoad;
                Boost[num].SizeX = Boost[num].SizeX + boostDazu;
            }
        }





        public void DrawPlayerInfo()
        {
            GL.Enable(EnableCap.Blend);
            GL.Color3(Color.White);
            DrawPlayerInfoOne(playerInfoOneDrin);
            DrawPlayerInfoTwo(playerInfoTwoDrin);

            GL.Disable(EnableCap.Blend);
            for (int i = 0; i < 2; i++)
            {
                DrawW(LifeW[i]);
                DrawW(AmmoW[i]);
                DrawW(BoostW[i]);
            }



            GL.Disable(EnableCap.Blend);
            for (int i = 0; i < 2; i++) {


                GL.Enable(EnableCap.Blend);
                GL.Color3(Color.White);
          

    
                DrawLife(Life[i]);
                DrawAmmo(Ammo[i]);
                DrawBoost(Boost[i]);

            

                DrawIcon(LifeIcon[i], texLifeIcon);
                DrawIcon(AmmoIcon[i], texAmmoIcon);
                DrawIcon(BoostIcon[i], texBoostIcon);
                GL.Disable(EnableCap.Blend);
            }

        }


        private void DrawIcon(Box2D rect, Texture tex)
        {

            tex.Activate();
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(rect.X, rect.Y);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(rect.MaxX, rect.Y);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(rect.MaxX, rect.MaxY);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(rect.X, rect.MaxY);
            GL.End();
            tex.Deactivate();

        }


        private void DrawW(Box2D rect)
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.DarkGray);
            GL.Vertex2(rect.X, rect.Y);
            GL.Color3(Color.DarkGray);
            GL.Vertex2(rect.MaxX, rect.Y);
            GL.Color3(Color.DarkGray);
            GL.Vertex2(rect.MaxX, rect.MaxY);
            GL.Color3(Color.DarkGray);
            GL.Vertex2(rect.X, rect.MaxY);
            GL.End();

            GL.Begin(PrimitiveType.LineLoop);
            GL.Color3(Color.Black);
            GL.Vertex2(rect.X, rect.Y);
            GL.Color3(Color.Black);
            GL.Vertex2(rect.MaxX, rect.Y);
            GL.Color3(Color.Black);
            GL.Vertex2(rect.MaxX, rect.MaxY);
            GL.Color3(Color.Black);
            GL.Vertex2(rect.X, rect.MaxY);
            GL.End();

        }
   
     
        private void DrawLife(Box2D rect)
        {
            float maler = 1 / (sizeBalken / (rect.SizeX));
           
            texLife.Activate();
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(rect.X, rect.Y);
            GL.TexCoord2(strecker * maler, 0.0f); GL.Vertex2(rect.MaxX, rect.Y);
            GL.TexCoord2(strecker * maler, 1.0f); GL.Vertex2(rect.MaxX, rect.MaxY);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(rect.X, rect.MaxY);
            GL.End();
            texLife.Deactivate();
    
        }
        private void DrawAmmo(Box2D rect)
        {

            float maler = 1 / (sizeBalken /(rect.SizeX));

            if (rect.SizeX < 0.014f) {
                maler = 0;
            }
                texAmmo.Activate();
            GL.Begin(PrimitiveType.Quads);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(rect.X, rect.Y);
            GL.TexCoord2(strecker * maler, 0.0f); GL.Vertex2(rect.MaxX, rect.Y);
            GL.TexCoord2(strecker * maler, 1.0f); GL.Vertex2(rect.MaxX, rect.MaxY);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(rect.X, rect.MaxY);
            GL.End();
            texAmmo.Deactivate();
         
        }
        private void DrawBoost(Box2D rect)
        {
            float maler = 1 / (sizeBalken / (rect.SizeX));
            texBoost.Activate();
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(rect.X, rect.Y);
            GL.TexCoord2(strecker * maler, 0.0f); GL.Vertex2(rect.MaxX, rect.Y);
            GL.TexCoord2(strecker * maler, 1.0f); GL.Vertex2(rect.MaxX, rect.MaxY);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(rect.X, rect.MaxY);
            GL.End();
            texBoost.Deactivate();
        }

        private void DrawPlayerInfoOne(Box2D rect)
        {

            saveZoneBlueTexture.Activate();
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(rect.X, rect.Y);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(rect.MaxX, rect.Y);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(rect.MaxX, rect.MaxY);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(rect.X, rect.MaxY);
            GL.End();
            saveZoneBlueTexture.Deactivate();

           /* float value = -0.01f;
            GL.Begin(PrimitiveType.LineLoop);
            GL.Color3(Color.Black);
            GL.Vertex2(rect.X + value, rect.Y + value);
            GL.Color3(Color.Black);
            GL.Vertex2(rect.MaxX - value, rect.Y +value);
            GL.Color3(Color.Black);
            GL.Vertex2(rect.MaxX -value, rect.MaxY -value);
            GL.Color3(Color.Black);
            GL.Vertex2(rect.X +value, rect.MaxY -value);
            GL.End();*/
        }
        private void DrawPlayerInfoTwo(Box2D rect)
        {

            saveZoneRedTexture.Activate();
            GL.Begin(PrimitiveType.Quads);
            //when using textures we have to set a texture coordinate for each vertex
            //by using the TexCoord command BEFORE the Vertex command

            GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(rect.X, rect.Y);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(rect.MaxX, rect.Y);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(rect.MaxX, rect.MaxY);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(rect.X, rect.MaxY);
            GL.End();
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex2(rect.X, rect.Y);
            GL.Vertex2(rect.MaxX, rect.Y);
            GL.Vertex2(rect.MaxX, rect.MaxY);
            GL.Vertex2(rect.X, rect.MaxY);
            GL.End();
            saveZoneRedTexture.Deactivate();
        }
    }
}
