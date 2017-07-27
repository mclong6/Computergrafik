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

        Box2D playerInfoOne;
        Box2D playerInfoTwo;
        float sizeBalken = 0.28f;
        float sizeBalkenLeer = 0.3f;

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

        /**W steht für den weißen hintergrund*/
        Box2D oneLifeW;
        Box2D oneAmmoW;
        Box2D oneBoostW;

        Box2D twoLifeW;
        Box2D twoAmmoW;
        Box2D twoBoostW;


        Texture texLife;
        Texture texAmmo;
        Texture texBoost;


        Texture texLifeW;
        Texture texAmmoW;
        Texture texBoostW;



        public Lebensleiste(Model model)
        {
            
            playerInfoOne = model.PlayerInfoOne;
            playerInfoTwo = model.PlayerInfoTwo;

            texLife = TextureLoader.FromBitmap(Resource2.health_bar); 
            texAmmo = TextureLoader.FromBitmap(Resource2.reload_bar); 
            texBoost = TextureLoader.FromBitmap(Resource2.jet_bar);

            texLifeW = TextureLoader.FromBitmap(Resource2.health);
            texAmmoW = TextureLoader.FromBitmap(Resource2.ammo);
            texBoostW = TextureLoader.FromBitmap(Resource2.jet);

            nachladen[0] = false;
            nachladen[1] = false;

            float randVoll = -0.93f;
            Life[0] = new Box2D(randVoll, 0.925f, sizeBalken, 0.05f);
            Ammo[0] = new Box2D(randVoll, 0.825f, sizeBalken, 0.05f);
            Boost[0] = new Box2D(randVoll, 0.725f, sizeBalken, 0.05f);

            Life[1] = new Box2D(0.65f, 0.925f, sizeBalken, 0.05f);
            Ammo[1] = new Box2D(0.65f, 0.825f, sizeBalken, 0.05f);
            Boost[1] = new Box2D(0.65f, 0.725f, sizeBalken, 0.05f);

            float rand = -0.99f;
            float Ysize = 0.1f;
            oneLifeW = new Box2D(rand, 0.925f, sizeBalkenLeer, Ysize);
            oneAmmoW = new Box2D(rand, 0.825f, sizeBalkenLeer, Ysize);
            oneBoostW = new Box2D(rand, 0.725f, sizeBalkenLeer, Ysize);

            twoLifeW = new Box2D(0.65f, 0.925f, sizeBalkenLeer, Ysize);
            twoAmmoW = new Box2D(0.65f, 0.825f, sizeBalkenLeer, Ysize);
            twoBoostW = new Box2D(0.65f, 0.725f, sizeBalkenLeer, Ysize);

         

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
            DrawPlayerInfoOne(playerInfoOne);
            DrawPlayerInfoTwo(playerInfoTwo);

            /*
                        DrawWhite(oneLifeW);
                        DrawWhite(oneAmmoW);
                        DrawWhite(oneBoostW);

                        DrawWhite(twoLifeW);
                        DrawWhite(twoAmmoW);
                        DrawWhite(twoBoostW);*/
            GL.Enable(EnableCap.Blend);


            GL.Color3(Color.White);

            DrawWAmmo(oneLifeW, texLifeW);
            DrawWAmmo(oneAmmoW, texAmmoW);
            DrawWAmmo(oneBoostW, texBoostW);

            DrawWAmmo(twoLifeW, texLifeW);
            DrawWAmmo(twoAmmoW, texAmmoW);
            DrawWAmmo(twoBoostW,texBoostW);
            GL.Disable(EnableCap.Blend);
            for (int i = 0; i <= 1; i++) {


                GL.Enable(EnableCap.Blend);


                GL.Color3(Color.White);
                DrawLife(Life[i]);
                
                DrawAmmo(Ammo[i]);
            DrawBoost(Boost[i]);
                GL.Disable(EnableCap.Blend);
            }

        }


        private void DrawPlayerInfoOne(Box2D rect)
        {
          

            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Blue);
            GL.Vertex2(rect.X, rect.Y);
            GL.Color3(Color.Blue);
            GL.Vertex2(rect.MaxX, rect.Y);
            GL.Color3(Color.Blue);
            GL.Vertex2(rect.MaxX, rect.MaxY);
            GL.Color3(Color.Blue);
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

        private void DrawWhite(Box2D rect)
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.White);
            GL.Vertex2(rect.X, rect.Y);
            GL.Color3(Color.White);
            GL.Vertex2(rect.MaxX, rect.Y);
            GL.Color3(Color.White);
            GL.Vertex2(rect.MaxX, rect.MaxY);
            GL.Color3(Color.White);
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
        private void DrawWAmmo(Box2D rect, Texture tex)
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
        private void DrawWLife(Box2D rect, Texture tex)
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
        private void DrawWBoost(Box2D rect, Texture tex)
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
        private void DrawPlayerInfoTwo(Box2D rect)
        {

            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Red);
            GL.Vertex2(rect.X, rect.Y);
            GL.Color3(Color.Red);
            GL.Vertex2(rect.MaxX, rect.Y);
            GL.Color3(Color.Red);
            GL.Vertex2(rect.MaxX, rect.MaxY);
            GL.Color3(Color.Red);
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

            texLife.Activate();
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(rect.X, rect.Y);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(rect.MaxX, rect.Y);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(rect.MaxX, rect.MaxY);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(rect.X, rect.MaxY);
            GL.End();
            texLife.Deactivate();
            /*
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Red);
            GL.Vertex2(rect.X, rect.Y);
            GL.Color3(Color.Red);
            GL.Vertex2(rect.MaxX, rect.Y);
            GL.Color3(Color.Red);
            GL.Vertex2(rect.MaxX, rect.MaxY);
            GL.Color3(Color.Red);
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
            GL.End();*/
        }
        private void DrawAmmo(Box2D rect)
        {
            texAmmo.Activate();
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(rect.X, rect.Y);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(rect.MaxX, rect.Y);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(rect.MaxX, rect.MaxY);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(rect.X, rect.MaxY);
            GL.End();
            texAmmo.Deactivate();
            /*
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Yellow);
            GL.Vertex2(rect.X, rect.Y);
            GL.Color3(Color.Yellow);
            GL.Vertex2(rect.MaxX, rect.Y);
            GL.Color3(Color.Yellow);
            GL.Vertex2(rect.MaxX, rect.MaxY);
            GL.Color3(Color.Yellow);
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
            GL.End();*/
        }
        private void DrawBoost(Box2D rect)
        {
            texBoost.Activate();
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(rect.X, rect.Y);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(rect.MaxX, rect.Y);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(rect.MaxX, rect.MaxY);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(rect.X, rect.MaxY);
            GL.End();
            texBoost.Deactivate();
            /*
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Turquoise);
            GL.Vertex2(rect.X, rect.Y);
            GL.Color3(Color.Turquoise);
            GL.Vertex2(rect.MaxX, rect.Y);
            GL.Color3(Color.Turquoise);
            GL.Vertex2(rect.MaxX, rect.MaxY);
            GL.Color3(Color.Turquoise);
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
            GL.End();*/
        }



    }
}
