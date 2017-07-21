using DMS.Geometry;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Computergrafik
{
    class Player
    {
        private Timer timer;
        private int playerNr;
        private Model model;
        private float speed = 0.006f;
        private Box2D player;
        private Box2D opponent;
        private Box2D gun;
        private GamePadState currentControllerState;
        private GamePadThumbSticks thumber;
        private Vector2 direcction;
        private Vector2 gunDirection;
        private bool shootcontrol = true;
        private bool colisionControl = true;
        private IList<Bullet> bullets = new List<Bullet>();
        private Bullet bullet;
        private float bulledSpeed = 0.02f;
        private float bulledInterval = 100;     // mill sec


        //informations
        private float life;
        private float ammo;
        private float boost;

        public Player(Model model, int playerNr) {

          
            this.model          = model;
            this.PlayerNr       = playerNr;
            this.pplayer        = model.Player[playerNr];
            if (PlayerNr == 1) {
                this.opponent = model.Player[0];
            }
            if (PlayerNr == 0)
            {
                this.opponent = model.Player[1];
            }

            this.gun            = model.PlayerGun[playerNr];
            this.direcction     = new Vector2(0f, 0f);
            this.gunDirection   = new Vector2(0f,0f);
            this.timer          = new Timer();
            this.timer.Interval = bulledInterval;
            this.timer.Elapsed += OnTimedEvent;
            this.timer.AutoReset= true;
            this.timer.Enabled  = true;

            this.life = 100;
            this.ammo = 100;
            this.boost = 100;
        }


        public void updatePosition()        //Updates the position of the player
        {
            getControllerState();
            boostPressed();
            shootPressed();
            Colisuion();
            calculatePlayerPosition();
            calculateBullets();
            recycleBullets();
            moveGun();


        }

   

        private void getControllerState() {         // takes the current controller state 

            currentControllerState = GamePad.GetState(PlayerNr);
            thumber = currentControllerState.ThumbSticks;

            direcction.X = thumber.Left.X;          // movemend dierection
            direcction.Y = thumber.Left.Y;

            gunDirection.X = thumber.Right.X;       // gun direction
            gunDirection.Y = thumber.Right.Y;

            if(gunDirection.X < 0.2f && gunDirection.X > -0.2f && gunDirection.Y <0.2f && gunDirection.Y > -0.2f) {

                gunDirection.X = direcction.X;
                gunDirection.Y = direcction.Y;

            }


            gunDirection.NormalizeFast();

            if (direcction.X < 0.2f && direcction.X > -0.2f) direcction.X = 0; // da Joysticks nie genau 0
            if (direcction.Y < 0.2f && direcction.Y > -0.2f) direcction.Y = 0; // da Joysticks nie genau 

            if (gunDirection.X < 0.2f && gunDirection.X > -0.2f) gunDirection.X = 0; // da Joysticks nie genau 0
            if (gunDirection.Y < 0.2f && gunDirection.Y > -0.2f) gunDirection.Y = 0; // da Joysticks nie genau 0

        }

        private void Colisuion()
        {
            if (pplayer.Intersects(model.Opponent[0]))
            {
                speed = 0;
                pplayer.X = pplayer.X + direcction.X*-0.1f;
                pplayer.Y = pplayer.Y + direcction.Y * -0.1f;

                pplayer.Y = 0f;
                pplayer.X = PlayerNr - 0.25f;

                this.life = life - 10;
                colisionControl = false;
            }
            else if (!pplayer.Intersects(model.Opponent[0]) && colisionControl == false)
            {
                speed = 0.006f;
                colisionControl = true;
            }

        }

        private void calculatePlayerPosition() {

            if (direcction.Length > 1f)
            {
                direcction.NormalizeFast();
            }

            pplayer.CenterX = pplayer.CenterX + (direcction.X * speed);
            pplayer.CenterY = pplayer.CenterY + (direcction.Y * speed);

        }

        private void boostPressed()             // if post is presst speed will rise 
        {

            if(currentControllerState.Triggers.Right == 1.0f && this.boost >= 0)
            {
                this.boost = boost -1;
                if (this.boost > 0)
                {
                    speed = 0.02f;
                }
                else {

                    speed = 0.006f;
                }
            }

            if (currentControllerState.Triggers.Right == 0.0f )
            {
                
                speed = 0.006f;
            }


        }

        public void recycleBullets() {

            for(int i = 0; i<Bullets.Count; i++)
            {
                if(Bullets[i].Bbullet.X < -1 || Bullets[i].Bbullet.X > 1 || Bullets[i].Bbullet.Y < -1 || Bullets[i].Bbullet.Y > 1)
                {
                    Bullets.RemoveAt(i);
                }

                

            }
        }



        private void shootPressed()             // if post is presst speed will rise 
        {

            if (currentControllerState.Triggers.Left > 0.5f && shootcontrol == true)
            {
               
                shootcontrol = false;
            }

            if (currentControllerState.Triggers.Left == 0.0f)
            {
                shootcontrol = true;
            }


        }

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            if (shootcontrol == false && ammo >=0)
            {
                this.ammo = ammo - 1;
                shoot(gunDirection.X, gunDirection.Y);
            }
           // Console.WriteLine("The Elapsed event was raised at {0}", e.SignalTime);
        }


        private void moveGun() {


            
        }

        private void shoot(float x, float y)
        {

            bullet = new Bullet(x, y, this);
            Bullets.Add(bullet);

         

            // Thread bulledThread = new Thread(bullet.fligh);
            // bulledThread.Start();

        }


        private void calculateBullets()
        {

            for (int i = 0; i< Bullets.Count; i++)
            {
                Bullets[i].Bbullet.CenterX = Bullets[i].Bbullet.CenterX + Bullets[i].X_Dir*bulledSpeed;
                Bullets[i].Bbullet.CenterY = Bullets[i].Bbullet.CenterY + Bullets[i].Y_Dir*bulledSpeed;

            }
        }

        public Box2D pplayer
        {
            get
            {
                return player;
            }

            set
            {
                player = value;
            }
        }

        public IList<Bullet> Bullets
        {
            get
            {
                return bullets;
            }

            set
            {
                bullets = value;
            }
        }

        public int PlayerNr
        {
            get
            {
                return playerNr;
            }

            set
            {
                playerNr = value;
            }
        }

        public float Life
        {
            get
            {
                return life;
            }

            set
            {
                life = value;
            }
        }

        public float Ammo
        {
            get
            {
                return ammo;
            }

            set
            {
                ammo = value;
            }
        }

        public float Boost
        {
            get
            {
                return boost;
            }

            set
            {
                boost = value;
            }
        }

        public GamePadState CurrentControllerState
        {
            get
            {
                return currentControllerState;
            }

            set
            {
                currentControllerState = value;
            }
        }

        public Box2D PlayerWho
        {
            get
            {
                return player;
            }

            set
            {
                player = value;
            }
        }
    }
}
