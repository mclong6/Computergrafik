using DMS.Geometry;
using DMS.OpenGL;
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
        int enter = 0;
        int joyStickUp = 0;
        int joyStickDown = 0;

        Box2D startScreen;
        Box2D selectButton;
        private Sound sound;
        Box2D[] menuButton = new Box2D[4];
        private GamePadState currentControllerState;
        private GamePadThumbSticks thumber;
        private Texture backgroundTexture;
        private Texture blackAndYellowTexture;
        private Texture blackAndYellowTextureLight;
        private Texture level1;
        private Texture level2;
        private Texture level3;
        private Texture beenden;

        public StartMenu(Model model)
        {
            sound = new Sound();
            menuButton = model.MenuButton;
            selectButton = model.SelectButton;
            startScreen = model.StartScreen;

            backgroundTexture = TextureLoader.FromBitmap(Resource2.karierter_background);
            blackAndYellowTexture = TextureLoader.FromBitmap(Resource2.old_hazard_stripes_texture);
            blackAndYellowTextureLight = TextureLoader.FromBitmap(Resource2.old_hazard_stripes_texture_light);
            level1 = TextureLoader.FromBitmap(Resource2.level1);
            level2 = TextureLoader.FromBitmap(Resource2.Level2);
            level3 = TextureLoader.FromBitmap(Resource2.level3);
            beenden = TextureLoader.FromBitmap(Resource2.beenden);


        }

        public void changeMenu(int GameState, MyWindow mywindow)
        {

            currentControllerState = GamePad.GetState(0);
            thumber = currentControllerState.ThumbSticks;

            /*GameLogic*/
            if (GameState == StateMenu)
            {
                /*Bei drücken der Taste A*/
                /*currentState.Buttons.LeftStick == ButtonState.Pressed*/
                if (Keyboard.GetState()[Key.Enter] || currentControllerState.Buttons.A == ButtonState.Pressed)
                {
                    enter = 1;
                }
                if (enter == 1 && !Keyboard.GetState()[Key.Enter] && currentControllerState.Buttons.A == ButtonState.Released)
                {
                    menuSelection(mywindow);
                    sound.startGame();
                    enter = 0;
                   
                  

                }




                /*Bei Hoch oder Runter*/

                if (thumber.Right.Y >0.4)
                {
                    joyStickUp = 1;
                }

                if (Keyboard.GetState()[Key.Up])
                {
                    joyStickUp = 1;
                }

                if (joyStickUp == 1 && !(Keyboard.GetState()[Key.Up]) && thumber.Right.Y < 0.4)
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
                if (thumber.Right.Y < -0.4)
                {
                    joyStickDown = 1;

                }

                if (joyStickDown == 1 && !(Keyboard.GetState()[Key.Down]) && thumber.Right.Y > -0.4 )
                {
                    Console.WriteLine(thumber.Right.Y);

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

           
            this.DrawButton1(menuButton[0]);
            this.DrawButton2(menuButton[1]);
            this.DrawButton3(menuButton[2]);
            this.BeendenButton(menuButton[3]);

        }

        private void DrawStartScreen(Box2D rect)
        {
            //the texture has to be enabled before use
            backgroundTexture.Activate();
            GL.Begin(PrimitiveType.Quads);
            //when using textures we have to set a texture coordinate for each vertex
            //by using the TexCoord command BEFORE the Vertex command

            GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(rect.X, rect.Y);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(rect.MaxX, rect.Y);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(rect.MaxX, rect.MaxY);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(rect.X, rect.MaxY);
            GL.End();

            //the texture is disabled, so no other draw calls use this texture
            backgroundTexture.Deactivate();
        }

        private void BeendenButton(Box2D rect)
        {
            //the texture has to be enabled before use
            beenden.Activate();
            GL.Begin(PrimitiveType.Quads);
            //when using textures we have to set a texture coordinate for each vertex
            //by using the TexCoord command BEFORE the Vertex command

            GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(rect.X, rect.Y);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(rect.MaxX, rect.Y);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(rect.MaxX, rect.MaxY);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(rect.X, rect.MaxY);
            GL.End();

            //the texture is disabled, so no other draw calls use this texture
            beenden.Deactivate();
        }

        private void DrawButton1(Box2D rect)
        {
            //the texture has to be enabled before use
            level1.Activate();
            GL.Begin(PrimitiveType.Quads);
            //when using textures we have to set a texture coordinate for each vertex
            //by using the TexCoord command BEFORE the Vertex command

            GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(rect.X, rect.Y);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(rect.MaxX, rect.Y);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(rect.MaxX, rect.MaxY);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(rect.X, rect.MaxY);
            GL.End();

            //the texture is disabled, so no other draw calls use this texture
            level1.Deactivate();
        }

        private void DrawButton2(Box2D rect)
        {
            //the texture has to be enabled before use
            level2.Activate();
            GL.Begin(PrimitiveType.Quads);
            //when using textures we have to set a texture coordinate for each vertex
            //by using the TexCoord command BEFORE the Vertex command

            GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(rect.X, rect.Y);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(rect.MaxX, rect.Y);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(rect.MaxX, rect.MaxY);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(rect.X, rect.MaxY);
            GL.End();

            //the texture is disabled, so no other draw calls use this texture
            level2.Deactivate();
        }

        private void DrawButton3(Box2D rect)
        {
            //the texture has to be enabled before use
            level3.Activate();
            GL.Begin(PrimitiveType.Quads);
            //when using textures we have to set a texture coordinate for each vertex
            //by using the TexCoord command BEFORE the Vertex command

            GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(rect.X, rect.Y);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(rect.MaxX, rect.Y);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(rect.MaxX, rect.MaxY);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(rect.X, rect.MaxY);
            GL.End();

            //the texture is disabled, so no other draw calls use this texture
            level3.Deactivate();
        }

        private void DrawSelectButton(Box2D rect)
        {
            //the texture has to be enabled before use
            blackAndYellowTexture.Activate();
            GL.Begin(PrimitiveType.Quads);
            //when using textures we have to set a texture coordinate for each vertex
            //by using the TexCoord command BEFORE the Vertex command

            GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(rect.X, rect.Y);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(rect.MaxX, rect.Y);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(rect.MaxX, rect.MaxY);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(rect.X, rect.MaxY);
            GL.End();

            //the texture is disabled, so no other draw calls use this texture
            blackAndYellowTexture.Deactivate();
        }
    }
}
