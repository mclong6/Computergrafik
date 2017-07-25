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
        private Logic logic;
        private float speed = 0.006f;
        private Box2D player;
        private Box2D gun;

        private int joyStickShoot = 0;
        private int joyStickBoost = 0;

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
        private int playerChosen = -1;

        private float life = 100;
        private float ammo = 100;
        private float boost = 100;


        public Player(Model model,Logic logic, int playerNr) {

            
            this.model          = model;
            this.PlayerNr       = playerNr;
            this.pplayer        = model.Player[playerNr];
          
            this.direcction     = new Vector2(0f, 0f);
            this.gunDirection   = new Vector2(0f,0f);
            this.timer          = new Timer();
            this.timer.Interval = bulledInterval;
            this.timer.Elapsed += OnTimedEvent;
            this.timer.AutoReset= true;
            this.timer.Enabled  = true;
            this.logic          = logic;
        }


        public void updatePosition()        //Updates the position of the player
        {
            getControllerState();
            boostPressed();
            shootPressed(true);
            Colisuion();
            calculatePlayerPosition();
            calculateBullets();
            bulledColission();
            recycleBullets();
            moveGun();
            obstacleIntersection();
            beHard(model.PlayerInfoOne, this.pplayer);
            beHard(model.PlayerInfoTwo, this.pplayer);
            if (playerNr == 0)
            {
                beHard(logic.Player[0].pplayer, logic.Player[1].pplayer);
            }
            if (playerNr == 1)
            {
                beHard(logic.Player[1].pplayer, logic.Player[0].pplayer);
            }

        }


        /*beHard Box in diese kann der driver nicht hinein fahren*/
     
        private void beHard(Box2D beHard, Box2D driver)
        {
            float bounce = 0.00f;
            float intervall = 0.006f * 4;
            if (beHard.Intersects(driver))
                       {
                           /*Linke Seite*/
            if (driver.X <= beHard.MaxX && driver.X >= beHard.MaxX - intervall)
            {
                driver.X = beHard.MaxX + bounce;
            }

            
            /*Obere Seite*/
             if (driver.Y <= beHard.MaxY && driver.Y >= beHard.MaxY - intervall)
             {
                 driver.Y = beHard.MaxY + bounce;
             }

            /*Rechte Seite*/
            if (beHard.X <= driver.MaxX && beHard.X + intervall >= driver.MaxX)
            {
                driver.X = beHard.X - driver.SizeX - bounce;
            }



            /*Untere Seite*/
             if (beHard.Y <= driver.MaxY && beHard.Y + intervall >= driver.MaxY)
             {
                 driver.Y = beHard.Y - driver.SizeY - bounce;
             }
         }
       }

        /*ObstacleIntersection*/

        private void obstacleIntersection() {

            float bounce = 0.0f;
            float intervall = 0.01f;
            for (int i = 0; i < model.Obstacles.Length; i++)
            {

           

                if (model.Obstacles[i].Intersects(pplayer)) {

                    beHard(model.Obstacles[i], pplayer);
         
                }


               
                if (bullets.Count > 0)
                {
   
                    for (int k = 0; k < bullets.Count; k++)
                    {
                        if (bullets[k].Bbullet.Intersects(model.Obstacles[i]))
                        {
                          
                            this.Bullets.RemoveAt(k);
                        }
                    }
                }
            }
        }

        private void getControllerState() {         // takes the current controller state 

            currentControllerState = GamePad.GetState(PlayerNr);
            thumber = currentControllerState.ThumbSticks;

            if (currentControllerState.IsConnected)
            {

                direcction.X = thumber.Left.X;          // movemend dierection
                direcction.Y = thumber.Left.Y;

                gunDirection.X = thumber.Right.X;       // gun direction
                gunDirection.Y = thumber.Right.Y;

            }

         
                if (Keyboard.GetState()[Key.M])
                {
                    playerChosen = 0;
                }

                if (Keyboard.GetState()[Key.N])
                {
                    playerChosen = 1;
                }


                if (playerChosen == PlayerNr)
                {

                    if (Keyboard.GetState()[Key.W])
                    {
                        direcction.Y = 1;
                    }

                    if (Keyboard.GetState()[Key.S])
                    {
                        direcction.Y = -1;
                    }

                    if (!Keyboard.GetState()[Key.W] && !Keyboard.GetState()[Key.S]) {
                        direcction.Y = 0;
                    }

                    if (Keyboard.GetState()[Key.D])
                    {
                        direcction.X = 1;
                    }

                    if (Keyboard.GetState()[Key.A])
                    {
                        direcction.X = -1;
                    }

                    if (!Keyboard.GetState()[Key.A] && !Keyboard.GetState()[Key.D])
                    {
                        direcction.X = 0;
                    }



                /*Schießen*/

                shootPressed(false);
             
                /*Boost*/
                if (Keyboard.GetState()[Key.V] && this.boost >= 0)
                {
                    this.boost = boost - 1;
                    if (this.boost > 0)
                    {
                        speed = 0.02f;
                    }
                    else
                    {
                        speed = 0.006f;
                    }
                    joyStickBoost = 1;
                }
                if (joyStickBoost == 1 & !(Keyboard.GetState()[Key.V] && this.boost >= 0))
                {
                  
                        speed = 0.006f;
                    
                }

                

            }

           

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

                pplayer.Y = model.SaveZone[playerNr].Y + 0.05f;
                pplayer.X = model.SaveZone[playerNr].X + 0.025f;

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

            if (pplayer.X +pplayer.SizeX > 1) pplayer.X = 1-pplayer.SizeX;
            if (pplayer.X < -1) pplayer.X = -1;
          if (pplayer.Y+pplayer.SizeY >1) pplayer.Y = 1-pplayer.SizeY;
            if (pplayer.Y < -1) pplayer.Y = -1;





        }

        private void boostPressed()             // if post is presst speed will rise 
        {

            if (currentControllerState.Triggers.Right == 1.0f && this.boost >=0)
            {
                this.boost = boost - 1;
                if (this.boost > 0)
                {
                    speed = 0.02f;
                }
                else {
                    speed = 0.006f;
                }
            }

            if (currentControllerState.Triggers.Right < 0.99f)
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



        public void shootPressed(bool k)             // if post is presst speed will rise 
        {
            if (k == true)     // controler
            {
                if (currentControllerState.IsConnected)
                {

                    if (currentControllerState.Triggers.Left > 0.8f && shootcontrol == true)
                    {
                        shootcontrol = false;
                    }

                    if (currentControllerState.Triggers.Left == 0.0f)
                    {
                        shootcontrol = true;
                    }
                }
            }
            else                    // tastatur
            {

                if (Keyboard.GetState()[Key.Space] && shootcontrol == true)
                {
                    shootcontrol = false;
                    joyStickShoot = 1;

                }

                if (joyStickShoot == 1 & !(Keyboard.GetState()[Key.Space]))
                {
                    joyStickShoot = 0;
                    shootcontrol = true;
                }

            }

        }

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            if (shootcontrol == false && ammo >=0 )
            {
                this.ammo = ammo - 1;
                shoot(gunDirection.X, gunDirection.Y);
            }
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

        private void bulledColission()
        {
           for(int i= 0; i < 2; i++)
            {
                for(int j= 0; j< logic.Player[i].bullets.Count; j++)
                {
                  
                    if (logic.Player[i].Bullets[j].Bbullet.Intersects(model.Opponent[0]))
                        {
                            logic.Player[i].Bullets.RemoveAt(j);
                        }
                    
                }

                if(logic.Player[i] != this)
                {
                    for(int k = 0; k< logic.Player[i].Bullets.Count; k++)
                    {
                        if (logic.Player[i].Bullets[k].Bbullet.Intersects(this.pplayer))
                        {
                            logic.Player[i].Bullets.RemoveAt(k);
                            life = life - 10;
                        }

                    }

                }

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

        public Vector2 Direcction
        {
            get
            {
                return direcction;
            }

            set
            {
                direcction = value;
            }
        }

        public float Speed
        {
            get
            {
                return speed;
            }

            set
            {
                speed = value;
            }
        }
    }
}
