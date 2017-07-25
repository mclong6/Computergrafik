using DMS.Geometry;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Drawing;

namespace Computergrafik
{
    class StartMenu
    {
        const int StateMenu = 0;
        const int StateStart = 1;

        float vibrationAmount = 0.0f;

        int joyStickUp = 0;
        int joyStickDown = 0;

        Box2D startScreen;
        Box2D selectButton;

        Box2D[] menuButton = new Box2D[4];
        GamePadState previousGamePadState;
        GamePadState gamepadState;

        public StartMenu(Model model, GamePadState pgamepadState)
        {
            gamepadState = pgamepadState;
            menuButton = model.MenuButton;
            previousGamePadState = pgamepadState;
            selectButton = model.SelectButton;
            startScreen = model.StartScreen;
        }

        public void changeMenu(int GameState, MyWindow mywindow)
        {

            /*GameLogic*/
            if (GameState == StateMenu)
            {
                /*Bei drücken der Taste A*/
                /*currentState.Buttons.LeftStick == ButtonState.Pressed*/
                if (Keyboard.GetState()[Key.Left])
                {
                    menuSelection(mywindow);
                }
         
               
                
           
                /*Bei Hoch oder Runter*/
                if (Keyboard.GetState()[Key.Up])
                {
                    joyStickUp = 1;
                }

                if (joyStickUp == 1 & !(Keyboard.GetState()[Key.Up]))
                {
                    joyStickUp = 0;
                    /*SelectBox nach oben setzen*/
                    if (selectButton.MaxY < (menuButton[0].MaxY))
                    {
                        selectButton.Y = selectButton.Y + 0.4f;
                    }
                }


                /*Bei Key Down muss noch mit JoyStick ersetzt werden*/
                else if (Keyboard.GetState()[Key.Down])
                {
                    joyStickDown = 1;
                }

                if (joyStickDown == 1 & !(Keyboard.GetState()[Key.Down]))
                {
                    joyStickDown = 0;
                    /*SelectBox nach unten setzen*/
                    if (selectButton.Y > (menuButton[3].Y))
                    {
                        selectButton.Y = selectButton.Y - 0.4f;
                    }
                }
            }

        }

        public void menuSelection(MyWindow mywindow)
        {
            for (int i = 0; i < menuButton.Length; i++)
            {
                if (menuButton[i].Intersects(selectButton))
                {
                    mywindow.GameState1 = StateStart;
                    mywindow.GameLevel = i ;

                    //Exit Game
                    if (i == 3) {
                        Environment.Exit(1);
                    }
                   
                }
            }
            /*Startet Level 1*/
           
        }
        public void DrawMenu()
        {
            this.DrawStartScreen(startScreen);
            this.DrawSelectButton(selectButton);

            for (int i = 0; i < menuButton.Length; i++)
            {
                this.DrawEndButton(menuButton[i]);
            }

        }

        private void DrawStartScreen(Box2D rect)
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.YellowGreen);
            GL.Vertex2(rect.X, rect.Y);
            GL.Color3(Color.YellowGreen);
            GL.Vertex2(rect.MaxX, rect.Y);
            GL.Color3(Color.YellowGreen);
            GL.Vertex2(rect.MaxX, rect.MaxY);
            GL.Color3(Color.YellowGreen);      
            GL.Vertex2(rect.X, rect.MaxY);
            GL.End();
        }


        private void DrawStartButton(Box2D rect)
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

        private void DrawEndButton(Box2D rect)
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

        private void DrawSelectButton(Box2D rect)
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.White);
            GL.Vertex2(rect.X, rect.Y);
            GL.Color3(Color.White);
            GL.Vertex2(rect.MaxX, rect.Y);
            GL.Color3(Color.White);
            GL.Vertex2(rect.MaxX, rect.MaxY);
            GL.Color3(Color.White);
            GL.Vertex2(rect.X, rect.MaxY);       
            GL.End();
        }
    }
}
