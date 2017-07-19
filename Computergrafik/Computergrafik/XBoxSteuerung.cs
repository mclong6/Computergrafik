using DMS.Geometry;
using DMS.OpenGL;

using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Drawing;

namespace Computergrafik
{
    class XboxSteuerung
    {
        XboxSteuerung()
        {
            /*Holt sich den Controller 0 Wenn GetState(1) holt sich den zweiten Controller*/
            GamePadState currentState = GamePad.GetState(0);

            /*Holt sich das DPad bzw. Steuerkreuz, ist nicht der JoyStick*/
            GamePadDPad curState = currentState.DPad;

            // GamePadState currentState1 = GamePad.GetState(3);
            //GamePadDPad curState1 = currentState1.DPad;


            /*Holt sich den JoyStick, thumber.Left -> Linker Joystick, thumber.Right -> rechter JoyStick*/
            GamePadThumbSticks thumber = currentState.ThumbSticks;


            /*     if (thumber.Left.Y >= 0.1f)
             {
                 player.X -= updatePeriod;

             }*/


            //player.X = thumber.Left.X;
            // player.Y = thumber.Left.Y;



            // Process input only if connected.
            if (currentState.IsConnected)
            {
                // Increase vibration if the player is tapping the A button.
                // Subtract vibration otherwise, even if the player holds down A
                if (currentState.Buttons.LeftStick == ButtonState.Pressed)
                {
                    //todo: let the player also move up and down
                    //todo:Limit player movements to window

                    //no intersection -> move obstacle
                    //   obstacle.Y -= 0.5f * updatePeriod;
                }

                if (curState.IsLeft)
                {

                    //    player.X -= updatePeriod;
                }


                if (curState.IsRight)
                {

                    //   player.X += updatePeriod;

                }
                if (curState.IsDown)
                {


                    //    player.Y -= updatePeriod;
                }

                if (curState.IsUp)
                {

                    //    player.Y += updatePeriod;

                }

            }
        }
    }
}

