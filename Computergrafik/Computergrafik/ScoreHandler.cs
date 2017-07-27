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
        Player[] players;
        int[] score = new int[2];

        int maxScore = 2;
        int noLife = 0;

        int scorePlus = 1;

        private TextureFont font;


        /*sore[0] Punktzahl für Player 1 */
        public ScoreHandler(Player[] pplayers)
        {
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
                        players[i].Life = 100;
                        players[i].Ammo = 100;
                        players[i].Boost = 100;
                        newGame();

                    }
                }
                else {
                    players[i].Life = 100;
                    players[i].Ammo = 100;
                    players[i].Boost = 100;
                    newGame();

                }

            }

        
        }

        private void newGame() {
            
            players[0].pplayer.X = -0.975f + 0 * 1.85f;
            players[0].pplayer.Y = -0.05f;
            players[1].pplayer.X = -0.975f + 1 * 1.85f;
            players[1].pplayer.Y = -0.05f;


            for(int i = 0; i < players.Length; i++)
            {
                for(int k = 0; k < players[i].Bullets.Count; k++)
                {
                    if (players[i].Bullets[k].Bbullet != null)
                    {
                        players[i].Bullets.RemoveAt(k);
                    }
                }
              
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
    }
}
