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

        int gameLevel;

        private bool doOnce = true;


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
            this.menu = new StartMenu(model,logic.Player[0].CurrentControllerState);

            /*Lebensmenü*/
            this.lebensleiste = new Lebensleiste(model);
           // gameWindow.WindowState = WindowState.Fullscreen;
            gameWindow.UpdateFrame  += GameWindow_UpdateFrame;
            gameWindow.RenderFrame  += GameWindow_RenderFrame;
            gameWindow.RenderFrame  += (sender, e) => { gameWindow.SwapBuffers(); };
            GL.ClearColor(Color.White);

        }


        public void GameWindow_UpdateFrame(object sender, FrameEventArgs e)
        {
            if (GameState == StateMenu)
            {
                menu.changeMenu(GameState, this);
            }
            if (GameState == StateStart)
            {
                if (doOnce == true) {

                    model.createLevel(gameLevel);
                    doOnce = false;
                }
                lebensleiste.OneGetBoost(logic.Player[0]);
                lebensleiste.OneGetShoot(logic.Player[0]);
                lebensleiste.OneLiveDown(logic.Player[0]);

                lebensleiste.OneGetBoost(logic.Player[1]);
                lebensleiste.OneGetShoot(logic.Player[1]);
                lebensleiste.OneLiveDown(logic.Player[1]);

              
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

                visuals.DrawSaveZone(model.SaveZone[0]);
                visuals.DrawSaveZone(model.SaveZone[1]);

                visuals.DrawPlayerOne(model.Player[0]);
               visuals.DrawPlayerTwo(model.Player[1]);

                for (int i = 0; i < logic.Player[0].Bullets.Count; i++)
                {
                    logic.Player[0].Bullets[i].DrawBulledOne();
                }

                for (int i = 0; i < logic.Player[1].Bullets.Count; i++)
                {
                    logic.Player[1].Bullets[i].DrawBulledTow();
                }

                visuals.DrawOpponent(model.Opponent[0].CenterX, model.Opponent[0].CenterY, 0.5f * model.Opponent[0].SizeX);
           
                
                for (int i = 0; i < model.Obstacles.Length; i++) {
                    
                    visuals.DrawObstacle(model.Obstacles[i]);
                }
                GL.Disable(EnableCap.Blend);
                lebensleiste.DrawPlayerInfo();
            }

        }


  

        [STAThread]
        public static void Main()
        {
            MyWindow app;
            app = new MyWindow();
            app.gameWindow.Run(60.0);
        }

        public int GameState1
        {
            get
            { return GameState; }

            set
            { GameState = value; }
        }

        internal Lebensleiste Lebensleiste
        {
            get
            { return lebensleiste; }

            set
            { lebensleiste = value; }
        }

        public int GameLevel
        {
            get
            {
                return gameLevel;
            }

            set
            {
                gameLevel = value;
            }
        }
    }
}

