using DMS.Geometry;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Drawing;
namespace Computergrafik
{
    class Lebensleiste
    {
        Box2D rect;
        Box2D rect1;
        public Lebensleiste() {
            Box2D playerOne = new Box2D(-1.0f,0.7f,0.4f,0.3f);
            Box2D playerTwo = new Box2D(0.6f, 0.7f, 0.4f, 0.3f);
            rect = playerOne;
            rect1 = playerTwo;
        }

        public void DrawPlayerLife()
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

        public void DrawPlayerLifeTwo()
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Blue);
            GL.Vertex2(rect1.X, rect1.Y);
            GL.Color3(Color.Blue);
            GL.Vertex2(rect1.MaxX, rect1.Y);
            GL.Color3(Color.Blue);
            GL.Vertex2(rect1.MaxX, rect1.MaxY);
            GL.Color3(Color.Blue);
            GL.Vertex2(rect1.X, rect1.MaxY);
            GL.End();
        }
    }
}
