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

        bool shoot = true;
        bool nachladen = false;

        //Xposition//YPosition//X-Länge//Y-Länge
        Box2D oneLife;
        Box2D oneAmmo;
        Box2D oneBoost;

        Box2D twoLife;
        Box2D twoAmmo;
        Box2D twoBoost;

        /**W steht für den weißen hintergrund*/
        Box2D oneLifeW;
        Box2D oneAmmoW;
        Box2D oneBoostW;

        Box2D twoLifeW;
        Box2D twoAmmoW;
        Box2D twoBoostW;

        Player[] players;
        public Lebensleiste(Model model)
        {
            playerInfoOne = model.PlayerInfoOne;
            playerInfoTwo = model.PlayerInfoTwo;
             
            oneLife = new Box2D(-0.95f, 0.925f, sizeBalken - 0.2f, 0.05f);
            oneAmmo = new Box2D(-0.95f, 0.825f, sizeBalken, 0.05f);
            oneBoost = new Box2D(-0.95f, 0.725f, sizeBalken, 0.05f);

            twoLife = new Box2D(0.65f, 0.925f, sizeBalken-0.1f, 0.05f);
            twoAmmo = new Box2D(0.65f, 0.825f, sizeBalken, 0.05f);
            twoBoost = new Box2D(0.65f, 0.725f, sizeBalken, 0.05f);

            oneLifeW = new Box2D(-0.95f, 0.925f, sizeBalken, 0.05f);
            oneAmmoW = new Box2D(-0.95f, 0.825f, sizeBalken, 0.05f);
            oneBoostW = new Box2D(-0.95f, 0.725f, sizeBalken, 0.05f);

            twoLifeW = new Box2D(0.65f, 0.925f, sizeBalken, 0.05f);
            twoAmmoW = new Box2D(0.65f, 0.825f, sizeBalken, 0.05f);
            twoBoostW = new Box2D(0.65f, 0.725f, sizeBalken, 0.05f);

         

        }


        public void OneLiveDown(int damage) {

            //getMaxLife();
            int maxLife = 2;
            damage = 5;

            float lifeAbziehen = sizeBalken / maxLife;
            oneLife.X = oneLife.X - damage;

        }

        public void OneGetShoot(Player player) {
            //getMaxShoot()
            float maxShoot = 100;
            float currentAmmo = player.Ammo;
            float ammoDazu = sizeBalken / maxShoot;
      

            if(currentAmmo == 0)
            {
                nachladen = true;
            }
            /*Leert die Anzeige*/
            if (shoot == true && nachladen == false)
            {
                oneAmmo.SizeX = (ammoDazu * currentAmmo);
                
            }
          
            if (nachladen == true) {
                 oneAmmo.SizeX = oneAmmo.SizeX + ammoDazu;
                if (oneAmmo.SizeX >= sizeBalken)
                {
                    player.Ammo = maxShoot;
                    nachladen = false;
                }
               

            }
          

        }

        public void OneGetBoost(Player player) {
            //GetMaxBoost
            float maxBoost = 100;
            float currentBoost =  player.Boost;
            float boostDazu = sizeBalken / maxBoost;

            
                oneBoost.SizeX =  (boostDazu * currentBoost);
            
            if (currentBoost <= 100 && oneBoost.SizeX <= sizeBalken) {

                player.Boost = currentBoost  +0.05f;
                oneBoost.SizeX = oneBoost.SizeX + boostDazu;
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

            DrawLife(oneLife);
            DrawAmmo(oneAmmo);
            DrawBoost(oneBoost);

            DrawLife(twoLife);
            DrawAmmo(twoAmmo);
            DrawBoost(twoBoost);

         
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
