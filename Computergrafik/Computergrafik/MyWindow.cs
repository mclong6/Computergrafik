using OpenTK;

using System;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using DMS.OpenGL;


namespace Computergrafik
{
    class MyWindow
    {

        private int winner = 2;
        /*Spielstatus*/
        int GameState;
        int enter = 0;
        const int StateMenu = 0;
        const int StateStart = 1;
        const int StateNewGame = 2;
        private GamePadState currentControllerState;


        int gameLevel;

        private bool doOnce = true;


        private GameWindow gameWindow = new GameWindow(1024, 1024);
        private Model model;
        private Visuals visuals;
        private Logic logic;
        private Lebensleiste lebensleiste;

        private Sound sound;


        /*Hauptmenu*/
        private StartMenu menu;

     

        public MyWindow() {

            this.sound = new Sound();
            this.model              = new Model();
            this.visuals            = new Visuals();
            this.logic              = new Logic(model, sound);
           
   
            /*Hauptmenü*/
            this.menu = new StartMenu(model);

            /*Lebensmenü*/
            this.lebensleiste = new Lebensleiste(model, sound);
           // gameWindow.WindowState = WindowState.Fullscreen;
            gameWindow.UpdateFrame  += GameWindow_UpdateFrame;
            gameWindow.RenderFrame  += GameWindow_RenderFrame;
            gameWindow.RenderFrame  += (sender, e) => { gameWindow.SwapBuffers(); };
            GL.ClearColor(Color.White);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha,BlendingFactorDest.OneMinusSrcAlpha);

        }


        public void GameWindow_UpdateFrame(object sender, FrameEventArgs e)
        {

            if (logic.Scorehandler.NewGame1 == true)
            {
                GameState = StateNewGame;
            }

            if (logic.Scorehandler.Score[0] == logic.Scorehandler.MaxScore)
            {
                /*Spieler 2. Gewinnt*/
                GameState = StateNewGame;
                winner = 2;
                logic.Scorehandler.Score[0] = 0;
                logic.Scorehandler.Score[1] = 0;
            }
            if (logic.Scorehandler.Score[1] == logic.Scorehandler.MaxScore)
            {
                /*Spieler 1. Gewinnt*/
            
                winner = 1;
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

           

            if(GameState == StateNewGame)
            {
                NewGameSetting();
                currentControllerState = GamePad.GetState(0);

                if (Keyboard.GetState()[Key.Enter] || currentControllerState.Buttons.A == ButtonState.Pressed)
                {                       
                    enter = 1;   
                }
             
                /*SelectBox nach oben setzen*/
                if (enter == 1 && !Keyboard.GetState()[Key.Enter] && currentControllerState.Buttons.A == ButtonState.Released)
                {

                    enter = 0;
                    doOnce = true;
                    GameState = StateMenu;
                }

            }
        }

        public void GameWindow_RenderFrame(object sender, FrameEventArgs e)
        {

            GL.Color3(Color.White);
            GL.Clear(ClearBufferMask.ColorBufferBit);
           
            GL.Color3(Color.White);
            visuals.DrawSquaredBackground(model.Window);

            if (GameState == StateMenu)
            {
                GL.Color3(Color.White);
                menu.DrawMenu();
                GL.Color3(Color.White);
            }
            if (GameState == StateStart)
            {
                lebensleiste.DrawPlayerInfo();
                GL.Enable(EnableCap.Blend);
                GL.Color3(Color.White);
                visuals.DrawSaveZone1(model.SaveZone[0]);
                visuals.DrawSaveZone2(model.SaveZone[1]);
                GL.Disable(EnableCap.Blend);

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


                GL.Color3(Color.White);

                visuals.DrawPlayerOne(model.Player[0], logic.Player[0].CurrentTexture);

                GL.Color3(Color.White);

                visuals.DrawPlayerTwo(model.Player[1], logic.Player[1].CurrentTexture);
                GL.Color3(Color.White);
                logic.Scorehandler.drawScore();
                GL.Disable(EnableCap.Blend);


            
            }
            if (GameState == StateNewGame)
            {
                GL.Enable(EnableCap.Blend);
                GL.Color3(Color.White);
               
                string scoreString = "Press " + '-' + " Enter";

                string winnerString = "Player " + winner.ToString() + " wins";
                TextureFont font = new TextureFont(TextureLoader.FromBitmap(Resource2.Big_Cheese), 10, 32);

                font.Print(-0.5f * font.Width(scoreString, 0.1f), 0.2f, 0.0f, 0.1f, scoreString);

                font.Print(-0.5f * font.Width(winnerString, 0.1f), 0.0f, 0.0f, 0.1f, winnerString);
                GL.Disable(EnableCap.Blend);
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
                logic.Player[i].Life = logic.Player[i].StartLife;
                logic.Player[i].Ammo = logic.Player[i].StartAmmo;
                logic.Player[i].Boost = logic.Player[i].StartBoost;

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

