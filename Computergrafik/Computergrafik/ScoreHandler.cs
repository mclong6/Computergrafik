using DMS.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computergrafik
{
    class ScoreHandler
    {
        bool NewGame = false;
        Player[] players;
        int[] score = new int[2];

        int maxScore = 3;
        int noLife = 0;
        int scorePlus = 1;
        Model model;
      
        private TextureFont font;


        /*sore[0] Punktzahl für Player 1 */
        public ScoreHandler(Player[] pplayers, Model pmodel)
        {
            model = pmodel;
            score[0] = 0;
            score[1] = 0;
            players = pplayers;
            font = new TextureFont(TextureLoader.FromBitmap(Resource2.Big_Cheese), 10, 32);  
        }

        public void scoreLogic() {

         


            for (int i = 0; i < players.Length; i++)
            {
                if (score[i] < maxScore)
                {
                    if (players[i].Life <= noLife)
                    {

                        score[i] = score[i] + scorePlus;
                        newGamePosition();

                    }
                }
                else {
                    newGamePosition();
                    NewGame = true;

                }

            }

        
        }

        private void newGamePosition() {


            for (int p = 0; p < players.Length; p++)
            {
                players[p].Bullets = new List<Bullet>();

                players[p].pplayer.X = -0.975f + p * 1.85f;
                players[p].pplayer.Y = -0.05f;

                float xTerm2 = model.SaveZone[p].CenterX;
               

                players[p].pplayer.X = xTerm2 - 0.045f;
                players[p].pplayer.Y = -0.045f;


                players[p].Life = players[p].StartLife;
                players[p].Ammo = players[p].StartAmmo;
                players[p].Boost = players[p].StartBoost;
            }
         
        }

        public void drawScore() {

            string scoreString = score[1].ToString() + '-' + score[0].ToString();
          
                font.Print(-0.5f * font.Width(scoreString, 0.1f), 0.9f, 0.0f, 0.1f, scoreString);
            
        }

      

        public int[] Score
        {
            get
            {
                return score;
            }

            set
            {
                score = value;
            }
        }

        public bool NewGame1
        {
            get
            {
                return NewGame;
            }

            set
            {
                NewGame = value;
            }
        }

        public int MaxScore
        {
            get
            {
                return maxScore;
            }

            set
            {
                maxScore = value;
            }
        }


    }
}
