using DMS.Geometry;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Drawing;

namespace Computergrafik
{

    class StartMenu
    {


        int GameState;

        public event EventHandler OnKlick;
        const int StateMenu = 0;
        const int StateStart = 1;

        int joyStickUp = 0;
        int joyStickDown = 0;



        public StartMenu(Box2D rect) {
            OnKlick.Target.Equals(rect);
            this.changeMenu();
           

    }
        private void changeMenu() {

            Box2D startScreen;
            Box2D startButton;
            Box2D endButton;
            Box2D selectButton;
            /*GameLogic*/
            if (GameState == StateMenu)
            {
                /*Bei drücken der Taste A*/
                /*currentState.Buttons.LeftStick == ButtonState.Pressed*/
                if (Keyboard.GetState()[Key.A])
                {
                    if (startButton.Inside(selectButton))
                    {
                        GameState = StateStart;
                    }

                    if (endButton.Inside(selectButton))
                    {
                        //Exit Game
                    }
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
                    if (selectButton.MaxY < (startButton.MaxY))
                    {
                        selectButton.Y = selectButton.Y + 0.4f;
                    }
                }

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


            /*GameRender*/
            if (GameState == StateMenu)
            {
                this.DrawStartScreen(startScreen);
                this.DrawStartButton(startButton);
                this.DrawEndButton(endButton);
                this.DrawSelectButton(selectButton);

            }
            else {


            }


        }

        private void DrawStartScreen(Box2D rect) { 
            GL.Begin(PrimitiveType.LineLoop);
            GL.Vertex2(rect.X, rect.Y);
            GL.Color3(Color.YellowGreen);
            GL.Vertex2(rect.MaxX, rect.Y);
            GL.Color3(Color.YellowGreen);
            GL.Vertex2(rect.MaxX, rect.MaxY);
            GL.Color3(Color.YellowGreen);
            GL.Vertex2(rect.X, rect.MaxY);
            GL.Color3(Color.YellowGreen);
            GL.End();
        }


        private void DrawStartButton(Box2D rect) { 
            GL.Begin(PrimitiveType.LineLoop);
            GL.Vertex2(rect.X, rect.Y);
            GL.Color3(Color.Blue);
            GL.Vertex2(rect.MaxX, rect.Y);
            GL.Color3(Color.Blue);
            GL.Vertex2(rect.MaxX, rect.MaxY);
            GL.Color3(Color.Blue);
            GL.Vertex2(rect.X, rect.MaxY);
            GL.Color3(Color.Blue);
            GL.End();
        }

        private void DrawEndButton(Box2D rect)
        {
            GL.Begin(PrimitiveType.LineLoop);
            GL.Vertex2(rect.X, rect.Y);
            GL.Color3(Color.Blue);
            GL.Vertex2(rect.MaxX, rect.Y);
            GL.Color3(Color.Blue);
            GL.Vertex2(rect.MaxX, rect.MaxY);
            GL.Color3(Color.Blue);
            GL.Vertex2(rect.X, rect.MaxY);
            GL.Color3(Color.Blue);
            GL.End();
        }

        private void DrawSelectButton(Box2D rect)
        {
            GL.Begin(PrimitiveType.LineLoop);
            GL.Vertex2(rect.X, rect.Y);
            GL.Color3(Color.White);
            GL.Vertex2(rect.MaxX, rect.Y);
            GL.Color3(Color.White);
            GL.Vertex2(rect.MaxX, rect.MaxY);
            GL.Color3(Color.White);
            GL.Vertex2(rect.X, rect.MaxY);
            GL.Color3(Color.White);
            GL.End();
        }
    }
}
