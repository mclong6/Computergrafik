
using DMS.Geometry;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Computergrafik
{
    class Visuals
    {

        public void DrawPlayerGun(Box2D rect)
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
        }

        public void DrawPlayerOne(Box2D rect){

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

        public void DrawPlayerTwo(Box2D rect)
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
            GL.Color3(Color.Red);
            GL.Vertex2(rect.X - 0.05f, rect.Y - 0.05f);
            GL.Color3(Color.Red);
            GL.Vertex2(rect.MaxX + 0.05f, rect.Y - 0.05f);
            GL.Color3(Color.Red);
            GL.Vertex2(rect.MaxX + 0.05f, rect.MaxY + 0.05f);
            GL.Color3(Color.Red);
            GL.Vertex2(rect.X - 0.05f, rect.MaxY + 0.05f);
            GL.End();
        }

        /*
        * Draw of Opponent
        */

        public void DrawObstacle(Box2D rect)
        {

            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Green);
            GL.Vertex2(rect.X, rect.Y);
            GL.Color3(Color.Green);
            GL.Vertex2(rect.MaxX, rect.Y);
            GL.Color3(Color.Green);
            GL.Vertex2(rect.MaxX, rect.MaxY);
            GL.Color3(Color.Green);
            GL.Vertex2(rect.X, rect.MaxY);
            GL.End();
        }
        public void DrawOpponent(float centerX, float centerY, float radius)
        {
            GL.Begin(PrimitiveType.Polygon);
            GL.Color3(Color.Red);
            for (float alpha = 0.0f; alpha < 2 * Math.PI; alpha += 0.1f * (float)Math.PI)
            {
                float x = radius * (float)Math.Cos(alpha);
                float y = radius * (float)Math.Sin(alpha);
                GL.Vertex2(centerX + x, centerY + y);
            }
            GL.End();
        }


    }
}
