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
        Box2D startButton;
        Box2D endButton;
        Box2D selectButton;
        GamePadState previousGamePadState;
        GamePadState gamepadState;

        public StartMenu(Model model, GamePadState pgamepadState)
        {
            gamepadState = pgamepadState;
           
            previousGamePadState = pgamepadState;
            startScreen = model.StartScreen;
            startButton = model.StartButton;
            endButton = model.EndButton;
            selectButton = model.SelectButton;
        }

        public void changeMenu(int GameState, MyWindow mywindow)
        {

            /*GameLogic*/
            if (GameState == StateMenu)
            {
                /*Bei drücken der Taste A*/
                /*currentState.Buttons.LeftStick == ButtonState.Pressed*/
          
                if (gamepadState.Buttons.Start == ButtonState.Pressed || Keyboard.GetState()[Key.Left])
                {

                    vibrationAmount = MathHelper.Clamp(vibrationAmount + 0.03f, 0.0f, 1.0f);
                    GamePad.SetVibration(1, vibrationAmount, vibrationAmount);
                    if (startButton.Intersects(selectButton))
                    {          
                        mywindow.GameState1 = StateStart;
                       mywindow.GameState1 = StateStart;
                    }

                    if (endButton.Intersects(selectButton))
                    {
                        //Exit Game
                        Environment.Exit(1);
                    }
                }
                previousGamePadState = gamepadState;

                /*Bei Hoch oder Runter*/
                if (Keyboard.GetState()[Key.Up])
                {
                    joyStickUp = 1;
                }

                if (joyStickUp == 1 & !(Keyboard.GetState()[Key.Up]))
                {
                    joyStickUp = 0;
                    /*SelectBox nach oben setzen*/
                    if (selectButton.MaxY < (startButton.MaxY))
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
                    if (selectButton.Y > (endButton.Y))
                    {
                        selectButton.Y = selectButton.Y - 0.4f;
                    }
                }
        



            }



        }
        public void DrawMenu()
        {
            this.DrawStartScreen(startScreen);
            this.DrawSelectButton(selectButton);
            this.DrawStartButton(startButton);
            this.DrawEndButton(endButton);
         

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
