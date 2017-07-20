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

        float x;
        float y;
        Box2D bullet;

        public Bullet(float x, float y, Player player) {

            this.x = x;
            this.y = y;
            bullet = new Box2D(0f,0.5f,0.15f,0.15f);

        }

        public void fligh() {

            while (moveBulled()) { }
            
        }

        private void DrawBulled(Box2D rect)
        {
            GL.Begin(PrimitiveType.LineLoop);
            GL.Color3(Color.Yellow);
            GL.Vertex2(rect.X, rect.Y);
            GL.Color3(Color.Yellow);
            GL.Vertex2(rect.MaxX, rect.Y);
            GL.Color3(Color.Yellow);
            GL.Vertex2(rect.MaxX, rect.MaxY);
            GL.Color3(Color.Yellow);
            GL.Vertex2(rect.X, rect.MaxY);
            GL.End();

        }

        private bool moveBulled() {

            bullet.CenterX = bullet.CenterX + x;
            bullet.CenterY = bullet.CenterY + y;

            DrawBulled(bullet);

            return true;

        }


    }
}
