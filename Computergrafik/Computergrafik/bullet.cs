using DMS.Geometry;
using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Computergrafik
{
    class Bullet
    {

        float x_Dir;
        float y_Dir;
        Box2D bbullet;

   

        public Bullet(float x, float y, Player player) {

            this.X_Dir = x;
            this.Y_Dir = y;
            Bbullet = new Box2D(player.pplayer.CenterX,player.pplayer.CenterY,0.005f,0.005f);
            Console.WriteLine("bullet erstellt");

        }

        public void DrawBulled()
        {

            
            GL.Begin(PrimitiveType.LineLoop);
            GL.Color3(Color.Yellow);
            GL.Vertex2(Bbullet.X, Bbullet.Y);
            GL.Color3(Color.Yellow);
            GL.Vertex2(Bbullet.MaxX, Bbullet.Y);
            GL.Color3(Color.Yellow);
            GL.Vertex2(Bbullet.MaxX, Bbullet.MaxY);
            GL.Color3(Color.Yellow);
            GL.Vertex2(Bbullet.X, Bbullet.MaxY);
            GL.End();

        }

       

        public Box2D Bbullet
        {
            get
            {
                return bbullet;
            }

            set
            {
                bbullet = value;
            }
        }

        public float X_Dir
        {
            get
            {
                return x_Dir;
            }

            set
            {
                x_Dir = value;
            }
        }

        public float Y_Dir
        {
            get
            {
                return y_Dir;
            }

            set
            {
                y_Dir = value;
            }
        }
    }
}
