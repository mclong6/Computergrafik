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
        /*GameStatus*/

        private GameWindow gameWindow = new GameWindow(1920, 1080);
        private Model model;
        private Visuals visuals;
        private Logic logic;

        /*HauptMenu*/
        int GameState = 0;

        const int StateMenu = 0;
        const int StateStart = 1;
        private StartMenuErsatz menu;

        public MyWindow() {

            this.model              = new Model();
            this.visuals            = new Visuals();
            this.logic              = new Logic(model);

            /*Menu*/
            this.menu = new StartMenuErsatz(model);

            gameWindow.UpdateFrame  += GameWindow_UpdateFrame;
            gameWindow.RenderFrame  += GameWindow_RenderFrame;
            gameWindow.RenderFrame  += (sender, e) => { gameWindow.SwapBuffers(); };
            GL.ClearColor(Color.Black);

        }


        public void GameWindow_UpdateFrame(object sender, FrameEventArgs e)
        {
            logic.updateLogic();
            menu.changeMenu();

        }

        public void GameWindow_RenderFrame(object sender, FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Enable(EnableCap.Blend);


            menu.DrawMenu();
            visuals.DrawPlayer(model.Player[0]);
            visuals.DrawPlayer(model.Player[1]);


            GL.Disable(EnableCap.Blend);


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

