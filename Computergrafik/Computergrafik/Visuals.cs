
using DMS.Geometry;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Computergrafik
{
    class Visuals
    {

        public void DrawPlayer(Box2D rect){

            GL.Begin(PrimitiveType.LineLoop);
            GL.Vertex2(rect.X, rect.Y);
            GL.Vertex2(rect.MaxX, rect.Y);
            GL.Vertex2(rect.MaxX, rect.MaxY);
            GL.Vertex2(rect.X, rect.MaxY);
            GL.End();
        }


    }
}
