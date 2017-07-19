
using DMS.Geometry;
using DMS.OpenGL;

using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Drawing;

namespace Computergrafik
{
  
    class XBoxSteuerung
    {
        /*Holt sich den Aktuellen Kontroller GetState(0) -> erster Kontroller  GetState(1) -> zweiter Kontroller*/
      static GamePadState currentState = GamePad.GetState(0);

        /*DPad ist das Steuerkreuz-> NICHT DIE JOYSTICKS*/
        GamePadDPad curState = currentState.DPad;
        

        // GamePadState currentState1 = GamePad.GetState(3);
        //GamePadDPad curState1 = currentState1.DPad;

            /*Holt sich die JoyStick,, thumber.Left -> linker Joystick ,  thumber.Right -> rechter Joystick*/
        GamePadThumbSticks thumber = currentState.ThumbSticks;

        player.X =thumber.Left.X;
        player.Y =thumber.Left.Y;
            


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
                    obstacle.Y -= 0.5f * updatePeriod;
                }

                if (curState.IsLeft) {

                    player.X -= updatePeriod;
                }
              

                if (curState.IsRight)
                {

                    player.X += updatePeriod;

                }
                if (curState.IsDown)
                {


                    player.Y -= updatePeriod;
                }

                if (curState.IsUp)
                {

                    player.Y += updatePeriod;

                }

                /**//*
                if (curState1.IsLeft)
                {

                    player1.X -= updatePeriod;
                }


                if (curState1.IsRight)
                {

                    player1.X += updatePeriod;

                }
                if (curState1.IsDown)
                {


                    player1.Y -= updatePeriod;
                }

                if (curState1.IsUp)
                {

                    player1.Y += updatePeriod;

                }
                */

    }
}
