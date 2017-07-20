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
        /*Spielstatus*/
        int GameState;
        const int StateMenu = 0;
        const int StateStart = 1;



        private GameWindow gameWindow = new GameWindow(1024, 1024);
        private Model model;
        private Visuals visuals;
        private Logic logic;
        private Lebensleiste lebensleiste;

        /*Hauptmenu*/
        private StartMenu menu;

     

        public MyWindow() {

            this.model              = new Model();
            this.visuals            = new Visuals();
            this.logic              = new Logic(model);
            

            /*Hauptmenü*/
            this.menu = new StartMenu(model);

            /*Lebensmenü*/
            this.lebensleiste = new Lebensleiste();
           // gameWindow.WindowState = WindowState.Fullscreen;
            gameWindow.UpdateFrame  += GameWindow_UpdateFrame;
            gameWindow.RenderFrame  += GameWindow_RenderFrame;
            gameWindow.RenderFrame  += (sender, e) => { gameWindow.SwapBuffers(); };
            GL.ClearColor(Color.Black);

        }


        public void GameWindow_UpdateFrame(object sender, FrameEventArgs e)
        {
            if (GameState == StateMenu)
            {
                menu.changeMenu(GameState, this);
            }
            if (GameState == StateStart)
            {
                logic.updateLogic();
                logic.updateOpponent();
            }
        }

        public void GameWindow_RenderFrame(object sender, FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Enable(EnableCap.Blend);
            if (GameState == StateMenu)
            {
                menu.DrawMenu();
            }
            if (GameState == StateStart)
            {
               
            visuals.DrawPlayer(model.Player[0]);
            visuals.DrawPlayer(model.Player[1]);
            visuals.DrawOpponent(model.Opponent[0].CenterX, model.Opponent[0].CenterY, 0.5f * model.Opponent[0].SizeX);
                lebensleiste.DrawPlayerLife();
                lebensleiste.DrawPlayerLifeTwo();
                GL.Disable(EnableCap.Blend);
            }

        }


        public int GameState1
        {
            get
            {
                return GameState;
            }

            set
            {
                GameState = value;
            }
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

