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



        int joyStickUp = 0;
        int joyStickDown = 0;

        Box2D startScreen;
        Box2D startButton;
        Box2D endButton;
        Box2D selectButton;


        public StartMenu(Model model)
        {
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
                if (Keyboard.GetState()[Key.Left])
                {
                    if (startButton.Intersects(selectButton))
                    {          
                        mywindow.GameState1 = StateStart;
                       mywindow.GameState1 = StateStart;
                    }

                    if (endButton.Intersects(selectButton))
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
            /*Rendern im Menü*/
            if (GameState == StateMenu)
            {
                this.DrawStartScreen(startScreen);
                this.DrawStartButton(startButton);
                this.DrawEndButton(endButton);
                this.DrawSelectButton(selectButton);

            }
            /*Rendern beim GameRun und GameStart*/
            if (GameState == StateStart)
            {


            }


        }
        public void DrawMenu()
        {
            this.DrawStartScreen(startScreen);
            this.DrawStartButton(startButton);
            this.DrawEndButton(endButton);
            this.DrawSelectButton(selectButton);

        }

        private void DrawStartScreen(Box2D rect)
        {
            GL.Begin(PrimitiveType.Quads);
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


        private void DrawStartButton(Box2D rect)
        {
            GL.Begin(PrimitiveType.Quads);
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
            GL.Begin(PrimitiveType.Quads);
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
            GL.Begin(PrimitiveType.Quads);
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
