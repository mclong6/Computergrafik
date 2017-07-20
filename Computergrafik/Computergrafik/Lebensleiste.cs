using DMS.Geometry;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Drawing;
namespace Computergrafik
{
    class Lebensleiste
    {
        
        
            Box2D playerInfoOne = new Box2D(-1.0f,0.7f,0.4f,0.3f);
            Box2D playerInfoTwo = new Box2D(0.6f, 0.7f, 0.4f, 0.3f);
            //Xposition//YPosition//X-Länge//Y-Länge
            Box2D oneLife = new Box2D(-0.95f, 0.925f, 0.3f, 0.05f);
            Box2D oneAmmo = new Box2D(-0.95f, 0.825f, 0.3f, 0.05f);
            Box2D oneBoost = new Box2D(-0.95f, 0.725f, 0.3f, 0.05f);

            Box2D twoLife = new Box2D(0.55f, 0.925f, 0.3f, 0.05f);
            Box2D twoAmmo = new Box2D(0.55f, 0.825f, 0.3f, 0.05f);
            Box2D twoBoost = new Box2D(0.55f, 0.725f, 0.3f, 0.05f);
        public Lebensleiste()
        {
        }

        public void DrawPlayerInfo()
        {
            DrawPlayerInfoOne(playerInfoOne);
            DrawPlayerInfoTwo(playerInfoTwo);
            DrawOneLife(oneLife);
                DrawOneAmmo(oneAmmo);
                DrawOneBoost(oneBoost);
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
        private void DrawPlayerInfoTwo(Box2D rect)
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
        private void DrawOneLife(Box2D rect)
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
        private void DrawOneAmmo(Box2D rect)
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
        private void DrawOneBoost(Box2D rect)
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
        }
     
      

    }
}
