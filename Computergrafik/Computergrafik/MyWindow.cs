using DMS.Application;
using OpenTK;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    using DMS.Geometry;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

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
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha,BlendingFactorDest.OneMinusSrcAlpha);

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
            GL.Color3(Color.White);
            GL.Clear(ClearBufferMask.ColorBufferBit);
           
            GL.Color3(Color.White);

            if (GameState == StateMenu)
            {
                GL.Color3(Color.White);
                menu.DrawMenu();
                GL.Color3(Color.White);
            }
            if (GameState == StateStart)
            {
                lebensleiste.DrawPlayerInfo();

                visuals.DrawSaveZone(model.SaveZone[0]);
                visuals.DrawSaveZone(model.SaveZone[1]);
                GL.Color3(Color.White);


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



                GL.Enable(EnableCap.Blend);
                

                GL.Color3(Color.White);

                visuals.DrawPlayerOne(model.Player[0], logic.Player[0].CurrentTexture);

                GL.Color3(Color.White);

                visuals.DrawPlayerTwo(model.Player[1], logic.Player[1].CurrentTexture);
              
                GL.Disable(EnableCap.Blend);
               
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

