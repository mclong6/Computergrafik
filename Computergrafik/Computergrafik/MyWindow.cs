using OpenTK;

using System;
using System.Drawing;
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
        const int StateNewGame = 2;

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
            if (logic.Scorehandler.Score[0] == logic.Scorehandler.MaxScore)
            {
                /*Spieler 1. Gewinnt*/
                GameState = StateNewGame;
                logic.Scorehandler.Score[0] = 0;
                logic.Scorehandler.Score[1] = 0;
            }
            if (logic.Scorehandler.Score[1] == logic.Scorehandler.MaxScore)
            {
                /*Spieler 2. Gewinnt*/
                GameState = StateNewGame;
                logic.Scorehandler.Score[0] = 0;
                logic.Scorehandler.Score[1] = 0;
            }




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


                logicLebensleiste();
                logic.Scorehandler.scoreLogic();
              
                logic.updateLogic();
                logic.updateOpponent();
            }

            if (logic.Scorehandler.NewGame1 == true) {
                GameState = StateNewGame;
            }

            if(GameState == StateNewGame)
            {
                NewGameSetting();
                
                if (Keyboard.GetState()[Key.Enter])
                {
                    doOnce = true;
                    GameState = StateMenu;
                }
                
            }
        }

        public void GameWindow_RenderFrame(object sender, FrameEventArgs e)
        {

            GL.Color3(Color.White);
            GL.Clear(ClearBufferMask.ColorBufferBit);

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


                for (int i = 0; i < model.Obstacles.Length; i++)
                {

                    visuals.DrawObstacle(model.Obstacles[i]);
                }


                GL.Enable(EnableCap.Blend);
                logic.Scorehandler.drawScore();

                GL.Color3(Color.White);

                visuals.DrawPlayerOne(model.Player[0], logic.Player[0].CurrentTexture);

                GL.Color3(Color.White);

                visuals.DrawPlayerTwo(model.Player[1], logic.Player[1].CurrentTexture);

                GL.Disable(EnableCap.Blend);


            
            }
            if (GameState == StateNewGame)
            {
                GL.Color3(Color.White);

            }

        }      



        public void logicLebensleiste() {
            lebensleiste.OneGetBoost(logic.Player[0]);
            lebensleiste.OneGetShoot(logic.Player[0]);
            lebensleiste.OneLiveDown(logic.Player[0]);

            lebensleiste.OneGetBoost(logic.Player[1]);
            lebensleiste.OneGetShoot(logic.Player[1]);
            lebensleiste.OneLiveDown(logic.Player[1]);
        }

        public void NewGameSetting() {


            for (int i = 0; i < logic.Player.Length; i++)
            {
                logic.Player[i].Life = 100;
                logic.Player[i].Ammo = 100;
                logic.Player[i].Boost = 100;

            }
            logicLebensleiste();
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

