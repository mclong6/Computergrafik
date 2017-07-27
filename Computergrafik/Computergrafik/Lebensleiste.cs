using DMS.Geometry;
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
        float sizeBalken = 0.3f;

        /*Speed beim Ausfladen*/
        float boostLoad = 0.075f;
        float ammoLoad = 0.0025f;

        
        bool[] nachladen = new bool[2];

        //Xposition//YPosition//X-Länge//Y-Länge
        Box2D[] Life = new Box2D[2];
        Box2D[] Ammo = new Box2D[2];
        Box2D[] Boost = new Box2D[2];



        /**W steht für den weißen hintergrund*/
        Box2D oneLifeW;
        Box2D oneAmmoW;
        Box2D oneBoostW;

        Box2D twoLifeW;
        Box2D twoAmmoW;
        Box2D twoBoostW;
      

     
        public Lebensleiste(Model model)
        {
            playerInfoOne = model.PlayerInfoOne;
            playerInfoTwo = model.PlayerInfoTwo;
            nachladen[0] = false;
            nachladen[1] = false;
            Life[0] = new Box2D(-0.95f, 0.925f, sizeBalken, 0.05f);
            Ammo[0] = new Box2D(-0.95f, 0.825f, sizeBalken, 0.05f);
            Boost[0] = new Box2D(-0.95f, 0.725f, sizeBalken, 0.05f);

            Life[1] = new Box2D(0.65f, 0.925f, sizeBalken, 0.05f);
            Ammo[1] = new Box2D(0.65f, 0.825f, sizeBalken, 0.05f);
            Boost[1] = new Box2D(0.65f, 0.725f, sizeBalken, 0.05f);

            oneLifeW = new Box2D(-0.95f, 0.925f, sizeBalken, 0.05f);
            oneAmmoW = new Box2D(-0.95f, 0.825f, sizeBalken, 0.05f);
            oneBoostW = new Box2D(-0.95f, 0.725f, sizeBalken, 0.05f);

            twoLifeW = new Box2D(0.65f, 0.925f, sizeBalken, 0.05f);
            twoAmmoW = new Box2D(0.65f, 0.825f, sizeBalken, 0.05f);
            twoBoostW = new Box2D(0.65f, 0.725f, sizeBalken, 0.05f);

         

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


            DrawWhite(oneLifeW);
            DrawWhite(oneAmmoW);
            DrawWhite(oneBoostW);

            DrawWhite(twoLifeW);
            DrawWhite(twoAmmoW);
            DrawWhite(twoBoostW);

          
            for(int i = 0; i <= 1; i++) {
            DrawLife(Life[i]);
            DrawAmmo(Ammo[i]);
            DrawBoost(Boost[i]);
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
        private void DrawAmmo(Box2D rect)
        {
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
            GL.End();
        }
        private void DrawBoost(Box2D rect)
        {
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
            GL.End();
        }



    }
}
