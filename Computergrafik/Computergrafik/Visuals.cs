
using DMS.Geometry;
using DMS.OpenGL;
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
        private Texture backgroundTexture;
        private Texture tex;
        public Visuals()
        {
            tex = TextureLoader.FromBitmap(Resource2.old_hazard_stripes_texture);

            backgroundTexture = TextureLoader.FromBitmap(Resource2.karierter_background);
        }
       

        public void DrawSaveZone1(Box2D rect)
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

        public void DrawSaveZone2(Box2D rect)
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


            float kleiner = 0.005f;
       
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Empty);
            GL.Vertex2(rect.X + kleiner, rect.Y +kleiner);
            GL.Color3(Color.Empty);
            GL.Vertex2(rect.MaxX - kleiner, rect.Y + kleiner);
            GL.Color3(Color.Empty);
            GL.Vertex2(rect.MaxX - kleiner, rect.MaxY - kleiner);
            GL.Color3(Color.Empty);
            GL.Vertex2(rect.X + kleiner, rect.MaxY - kleiner);
            GL.End();
        }

        public void DrawPlayerOne(Box2D Rect, Texture tex)
        {
            
            //the texture has to be enabled before use
            tex.Activate();
            GL.Begin(PrimitiveType.Quads);
            //when using textures we have to set a texture coordinate for each vertex
            //by using the TexCoord command BEFORE the Vertex command

            GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(Rect.X, Rect.Y);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(Rect.MaxX, Rect.Y);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(Rect.MaxX, Rect.MaxY);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(Rect.X, Rect.MaxY);
            GL.End();

            //the texture is disabled, so no other draw calls use this texture
            tex.Deactivate();

        }
        public void DrawSquaredBackground(Box2D window)
        {
            
            //the texture has to be enabled before use
            backgroundTexture.Activate();
            GL.Begin(PrimitiveType.Quads);
            //when using textures we have to set a texture coordinate for each vertex
            //by using the TexCoord command BEFORE the Vertex command

            GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(window.X, window.Y);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(window.MaxX, window.Y);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(window.MaxX, window.MaxY);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(window.X, window.MaxY);
            GL.End();

            //the texture is disabled, so no other draw calls use this texture
            backgroundTexture.Deactivate();
        }



        public void DrawPlayerTwo(Box2D Rect, Texture tex) { 
        
            //the texture has to be enabled before use
            tex.Activate();
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.White);
            //when using textures we have to set a texture coordinate for each vertex
            //by using the TexCoord command BEFORE the Vertex command
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(Rect.X, Rect.Y);
            GL.Color3(Color.White);

            GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(Rect.MaxX, Rect.Y);
            GL.Color3(Color.White);

            GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(Rect.MaxX, Rect.MaxY);
                        GL.Color3(Color.White);

            GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(Rect.X, Rect.MaxY);
            GL.End();
            //the texture is disabled, so no other draw calls use this texture
            tex.Deactivate();
        
        }

        /*
        * Draw of Opponent
        */

        public void DrawObstacle(Box2D rect)
        {

            GL.Begin(PrimitiveType.Quads);

            GL.Vertex2(rect.X, rect.Y);
          
            GL.Vertex2(rect.MaxX, rect.Y);
           
            GL.Vertex2(rect.MaxX, rect.MaxY);
            GL.Vertex2(rect.X, rect.MaxY);
            GL.End();
            float value = 0.025f;

            GL.Begin(PrimitiveType.Quads);
            
            GL.End();


            tex.Activate();
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.White);
            //when using textures we have to set a texture coordinate for each vertex
            //by using the TexCoord command BEFORE the Vertex command
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(rect.X-value, rect.Y-value);
            GL.Color3(Color.White);

            GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(rect.MaxX+value, rect.Y-value);
            GL.Color3(Color.White);

            GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(rect.MaxX+value, rect.MaxY+value);
            GL.Color3(Color.White);

            GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(rect.X-value, rect.MaxY+value);
            GL.End();
            //the texture is disabled, so no other draw calls use this texture
            tex.Deactivate();
        }
        public void DrawOpponent(float centerX, float centerY, float radius)
        {
         

            GL.Begin(PrimitiveType.Polygon);
            GL.Color3(Color.OrangeRed);
            float radiusBig = radius + 0.005f;

            for (float alpha = 0.0f; alpha < 2 * Math.PI; alpha += 0.1f * (float)Math.PI)
                {
                    float x = radiusBig * (float)Math.Cos(alpha);
                    float y = radiusBig * (float)Math.Sin(alpha);
                    GL.Vertex2(centerX + x, centerY + y);
                }
            
            GL.End();
            GL.Begin(PrimitiveType.Polygon);
            GL.Color3(Color.DarkSlateGray);
            for (float alpha = 0.0f; alpha < 2 * Math.PI; alpha += 0.1f * (float)Math.PI)
            {
                float x = radius * (float)Math.Cos(alpha);
                float y = radius * (float)Math.Sin(alpha);
                GL.Vertex2(centerX + x, centerY + y);
            }
            GL.End();

        }

        public Texture BackgroundTexture
        {
            get
            {
                return backgroundTexture;
            }

            set
            {
                backgroundTexture = value;
            }
        }

    }
}
