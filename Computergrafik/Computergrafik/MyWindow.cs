using DMS.Application;
using OpenTK;
using OpenTK.Graphics.ES20;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computergrafik
{
    class MyWindow
    {

        private GameWindow gameWindow = new GameWindow(1920, 1080);


        public MyWindow() {
            
            gameWindow.UpdateFrame += GameWindow_UpdateFrame;
            gameWindow.RenderFrame += GameWindow_RenderFrame;
            gameWindow.RenderFrame += (sender, e) => { gameWindow.SwapBuffers(); };
            GL.ClearColor(Color.White);

        }


        public void GameWindow_UpdateFrame(object sender, FrameEventArgs e)
        {

        }

        public void GameWindow_RenderFrame(object sender, FrameEventArgs e)
        {



        }



        [STAThread]
        public static void Main()
        {
            MyWindow app;
            app = new MyWindow();
           
            app.gameWindow.Run(60.0);
        }
    }
}

