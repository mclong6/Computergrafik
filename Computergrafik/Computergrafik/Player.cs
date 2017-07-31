using Computergrafik;

using DMS.Geometry;
using DMS.OpenGL;
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
      

        private Sound sound;
        private int angle;
        private Texture[] texture = new Texture[24];
        private Texture[] texture1 = new Texture[24];

        private Texture currentTexture = new Texture();
       
        private int joyStickShoot = 0;
        private int joyStickBoost = 0;

        private GamePadState currentControllerState;
        private GamePadThumbSticks thumber;
        private Vector2 direcction;
        private Vector2 vector1;

        private Vector2 gunDirection;
        private bool shootcontrol = true;
        private bool colisionControl = true;
        private IList<Bullet> bullets = new List<Bullet>();
        private Bullet bullet;
        private float bulledSpeed = 0.02f;
        private float bulledInterval = 100;     // mill sec
        private int playerChosen = -1;


        private float startLife = 100;
        private float startAmmo = 100;
        private float startBoost = 100;

        private float life;
        private float ammo;
        private float boost;

        private float minusLife = 5;
        private float minusLifeOpponent = 30;
        private float minusAmmo = 5;
        private float minusBoost = 1;

        private float speedStandard = 0.006f;
        private float speedBoost = 0.013f;
     


        public Player(Model model,Logic logic, int playerNr, Sound psound) {

            sound = psound;
            this.model          = model;
            this.PlayerNr       = playerNr;
            this.pplayer        = model.Player[playerNr];
            this.life = startLife;
            this.ammo = startAmmo;
            this.boost = startBoost;
           
            Texture[0] = TextureLoader.FromBitmap(Resource2._1b);
            Texture[1] = TextureLoader.FromBitmap(Resource2._2b);
            Texture[2] = TextureLoader.FromBitmap(Resource2._3b);
            Texture[3] = TextureLoader.FromBitmap(Resource2._4b);
            Texture[4] = TextureLoader.FromBitmap(Resource2._5b);
            Texture[5] = TextureLoader.FromBitmap(Resource2._6b);
            Texture[6] = TextureLoader.FromBitmap(Resource2._7b);
            Texture[7] = TextureLoader.FromBitmap(Resource2._8b);
            Texture[8] = TextureLoader.FromBitmap(Resource2._9b);
            Texture[9] = TextureLoader.FromBitmap(Resource2._10b);
            Texture[10] = TextureLoader.FromBitmap(Resource2._11b);
            Texture[11] = TextureLoader.FromBitmap(Resource2._12b);
            Texture[12] = TextureLoader.FromBitmap(Resource2._13b);
            Texture[13] = TextureLoader.FromBitmap(Resource2._14b);
            Texture[14] = TextureLoader.FromBitmap(Resource2._15b);
            Texture[15] = TextureLoader.FromBitmap(Resource2._16b);
            Texture[16] = TextureLoader.FromBitmap(Resource2._17b);
            Texture[17] = TextureLoader.FromBitmap(Resource2._18b);
            Texture[18] = TextureLoader.FromBitmap(Resource2._19b);
            Texture[19] = TextureLoader.FromBitmap(Resource2._20b);
            Texture[20] = TextureLoader.FromBitmap(Resource2._21b);
            Texture[21] = TextureLoader.FromBitmap(Resource2._22b);
            Texture[22] = TextureLoader.FromBitmap(Resource2._23b);
            Texture[23] = TextureLoader.FromBitmap(Resource2._24b);

            Texture1[0] = TextureLoader.FromBitmap(Resource2._1);
            Texture1[1] = TextureLoader.FromBitmap(Resource2._2);
            Texture1[2] = TextureLoader.FromBitmap(Resource2._3);
            Texture1[3] = TextureLoader.FromBitmap(Resource2._4);
            Texture1[4] = TextureLoader.FromBitmap(Resource2._5);
            Texture1[5] = TextureLoader.FromBitmap(Resource2._6);
            Texture1[6] = TextureLoader.FromBitmap(Resource2._7);
            Texture1[7] = TextureLoader.FromBitmap(Resource2._8);
            Texture1[8] = TextureLoader.FromBitmap(Resource2._9);
            Texture1[9] = TextureLoader.FromBitmap(Resource2._10);
            Texture1[10] = TextureLoader.FromBitmap(Resource2._11);
            Texture1[11] = TextureLoader.FromBitmap(Resource2._12);
            Texture1[12] = TextureLoader.FromBitmap(Resource2._13);
            Texture1[13] = TextureLoader.FromBitmap(Resource2._14);
            Texture1[14] = TextureLoader.FromBitmap(Resource2._15);
            Texture1[15] = TextureLoader.FromBitmap(Resource2._16);
            Texture1[16] = TextureLoader.FromBitmap(Resource2._17);
            Texture1[17] = TextureLoader.FromBitmap(Resource2._18);
            Texture1[18] = TextureLoader.FromBitmap(Resource2._19);
            Texture1[19] = TextureLoader.FromBitmap(Resource2._20);
            Texture1[20] = TextureLoader.FromBitmap(Resource2._21);
            Texture1[21] = TextureLoader.FromBitmap(Resource2._22);
            Texture1[22] = TextureLoader.FromBitmap(Resource2._23);
            Texture1[23] = TextureLoader.FromBitmap(Resource2._24);

            this.direcction     = new Vector2(0f, 0f);
            this.gunDirection   = new Vector2(0f,0f);
            this.vector1        = new Vector2(0, 1);
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
               
            
            bulledColission();
            calculateBullets();
           
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
            recycleBullets();
            
        }


        /*beHard Box in diese kann der driver nicht hinein fahren*/
     
        private void beHard(Box2D beHard, Box2D driver)
        {
            float bounce = 0.00f;
            float intervall = speedBoost + 0.000001f;
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

            for (int i = 0; i < model.Obstacles.Length; i++)
            {

           

                if (model.Obstacles[i].Intersects(pplayer)) {

                    beHard(model.Obstacles[i], pplayer);
         
                }


               
                if (bullets.Count > 0)
                {
   
                    for (int k = 0; k < bullets.Count; k++)
                    {

                        if (bullets[k] != null) {
                            if (bullets[k].Bbullet.Intersects(model.Obstacles[i]))
                            {

                                this.Bullets.RemoveAt(k);
                            }
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
                    this.boost = boost - minusBoost;
                    logic.Player[playerChosen].speed = speedBoost;
                    if (logic.Player[playerChosen].boost > 0)
                    {
                        logic.Player[playerChosen].speed = speedBoost;
                    }
                    else
                    {

                        logic.Player[playerChosen].speed = speedStandard;
                    }
                 //   joyStickBoost = 1;

                }
                if (!(Keyboard.GetState()[Key.V] && this.boost >= 0))
                {
                    logic.Player[playerChosen].speed = speedStandard;
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


        private void getGunAngle()
        {
            AngleBetween(gunDirection);

            if (PlayerNr == 0)
            {

                if (Angle < 15 && Angle >= 0)
                {
                    CurrentTexture = Texture[0];
                }
                else if (Angle < 30 && Angle >= 15)
                {
                    CurrentTexture = Texture[1];
                }
                else if (Angle < 45 && Angle >= 30)
                {
                    CurrentTexture = Texture[2];
                }
                else if (Angle < 60 && Angle >= 45)
                {
                    CurrentTexture = Texture[3];
                }
                else if (Angle < 75 && Angle >= 60)
                {
                    CurrentTexture = Texture[4];
                }
                else if (Angle < 90 && Angle >= 75)
                {
                    CurrentTexture = Texture[5];
                }
                if (Angle < 105 && Angle >= 90)
                {
                    CurrentTexture = Texture[6];
                }
                else if (Angle < 120 && Angle >= 105)
                {
                    CurrentTexture = Texture[7];
                }
                else if (Angle < 135 && Angle >= 120)
                {
                    CurrentTexture = Texture[8];
                }
                else if (Angle < 150 && Angle >= 135)
                {
                    CurrentTexture = Texture[9];
                }
                else if (Angle < 165 && Angle >= 150)
                {
                    CurrentTexture = Texture[10];
                }
                else if (Angle < 180 && Angle >= 165)
                {
                    CurrentTexture = Texture[11];
                }
                else if (Angle < 195 && Angle >= 180)
                {
                    CurrentTexture = Texture[12];
                }
                else if (Angle < 210 && Angle >= 195)
                {
                    CurrentTexture = Texture[13];
                }
                else if (Angle < 225 && Angle >= 210)
                {
                    CurrentTexture = Texture[14];
                }
                else if (Angle < 240 && Angle >= 225)
                {
                    CurrentTexture = Texture[15];
                }
                else if (Angle < 255 && Angle >= 240)
                {
                    CurrentTexture = Texture[16];
                }
                else if (Angle < 270 && Angle >= 255)
                {
                    CurrentTexture = Texture[17];
                }
                else if (Angle < 285 && Angle >= 270)
                {
                    CurrentTexture = Texture[18];
                }
                else if (Angle < 300 && Angle >= 285)
                {
                    CurrentTexture = Texture[19];
                }
                else if (Angle < 315 && Angle >= 300)
                {
                    CurrentTexture = Texture[20];
                }
                else if (Angle < 330 && Angle >= 315)
                {
                    CurrentTexture = Texture[21];
                }
                else if (Angle < 345 && Angle >= 330)
                {
                    CurrentTexture = Texture[22];
                }
                else if (Angle < 360 && Angle >= 345)
                {
                    CurrentTexture = Texture[23];
                }
                else if (Angle == 360)
                {
                    CurrentTexture = Texture[0];
                }

            }

            if(PlayerNr==1)
            {

                if (Angle < 15 && Angle >= 0)
                {
                    CurrentTexture = Texture1[0];
                }
                else if (Angle < 30 && Angle >= 15)
                {
                    CurrentTexture = Texture1[1];
                }
                else if (Angle < 45 && Angle >= 30)
                {
                    CurrentTexture = Texture1[2];
                }
                else if (Angle < 60 && Angle >= 45)
                {
                    CurrentTexture = Texture1[3];
                }
                else if (Angle < 75 && Angle >= 60)
                {
                    CurrentTexture = Texture1[4];
                }
                else if (Angle < 90 && Angle >= 75)
                {
                    CurrentTexture = Texture1[5];
                }
                if (Angle < 105 && Angle >= 90)
                {
                    CurrentTexture = Texture1[6];
                }
                else if (Angle < 120 && Angle >= 105)
                {
                    CurrentTexture = Texture1[7];
                }
                else if (Angle < 135 && Angle >= 120)
                {
                    CurrentTexture = Texture1[8];
                }
                else if (Angle < 150 && Angle >= 135)
                {
                    CurrentTexture = Texture1[9];
                }
                else if (Angle < 165 && Angle >= 150)
                {
                    CurrentTexture = Texture1[10];
                }
                else if (Angle < 180 && Angle >= 165)
                {
                    CurrentTexture = Texture1[11];
                }
                else if (Angle < 195 && Angle >= 180)
                {
                    CurrentTexture = Texture1[12];
                }
                else if (Angle < 210 && Angle >= 195)
                {
                    CurrentTexture = Texture1[13];
                }
                else if (Angle < 225 && Angle >= 210)
                {
                    CurrentTexture = Texture1[14];
                }
                else if (Angle < 240 && Angle >= 225)
                {
                    CurrentTexture = Texture1[15];
                }
                else if (Angle < 255 && Angle >= 240)
                {
                    CurrentTexture = Texture1[16];
                }
                else if (Angle < 270 && Angle >= 255)
                {
                    CurrentTexture = Texture1[17];
                }
                else if (Angle < 285 && Angle >= 270)
                {
                    CurrentTexture = Texture1[18];
                }
                else if (Angle < 300 && Angle >= 285)
                {
                    CurrentTexture = Texture1[19];
                }
                else if (Angle < 315 && Angle >= 300)
                {
                    CurrentTexture = Texture1[20];
                }
                else if (Angle < 330 && Angle >= 315)
                {
                    CurrentTexture = Texture1[21];
                }
                else if (Angle < 345 && Angle >= 330)
                {
                    CurrentTexture = Texture1[22];
                }
                else if (Angle < 360 && Angle >= 345)
                {
                    CurrentTexture = Texture1[23];
                }
                else if (Angle == 360)
                {
                    CurrentTexture = Texture1[0];
                }
            }
        }

       

        public void AngleBetween(Vector2 vector2)
        {
            //int k = Convert.ToInt32(Math.Abs(vector2.X) *Math.Abs(vector2.Y)*10);

           
            double x = vector2.X*100;
            double y = vector2.Y*100;
            

            if (vector2.Y > 0)
            {
                if (vector2.X >0 ) //1
                {
                    x = Math.Abs(x);
                    y = Math.Abs(y);

                    Angle = Convert.ToInt32(Math.Atan(x / y)*180/Math.PI);



                }
                else if (vector2.X < 0) //2
                {
                    x = Math.Abs(x);
                    y = Math.Abs(y);

                    Angle = Convert.ToInt32((Math.Atan(y / x) * 180 / Math.PI)+270);


                }

            }
            else
            {
                if (vector2.X > 0) //3
                {

                      x = Math.Abs(x);
                    y = Math.Abs(y);

                    Angle = Convert.ToInt32(Math.Atan(y / x)*180/Math.PI+90);


                }
                else if (vector2.X < 0) //4
                {

                    x = Math.Abs(x);
                    y = Math.Abs(y);

                    Angle = Convert.ToInt32(Math.Atan(x/ y) * 180 / Math.PI + 180);

                }

            }

        }


        private void Colisuion()
        {
            if (pplayer.Intersects(model.Opponent[0]))
            {
                sound.driveOpponent();
                speed = 0;
                pplayer.X = pplayer.X + direcction.X*-0.1f;
                pplayer.Y = pplayer.Y + direcction.Y * -0.1f;

                pplayer.CenterY = model.SaveZone[playerNr].CenterY;
                pplayer.CenterX = model.SaveZone[playerNr].CenterX;

               
                this.life = life - minusLifeOpponent;
                colisionControl = false;
            }
            else if (!pplayer.Intersects(model.Opponent[0]) && colisionControl == false)
            {
                
                speed = speedStandard;
               
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
            /**Dadurch kann Spieler 2 boost an d Tastatur einssetzen, spieler 2 boost am Controller nicht möglich**/
            if (playerNr == 0)
            {
                if (currentControllerState.Triggers.Right >= 0.9f && this.boost >= 0)
                {
                    sound.doBoost();
                    this.boost = boost - minusBoost;
                    if (this.boost > 0)
                    {

                        speed = speedBoost;
                    }
                    else
                    {

                        speed = speedStandard;
                    }
                }

                if (currentControllerState.Triggers.Right < 0.90f)
                {
                    speed = speedStandard;
                }
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
            getGunAngle();


            if (shootcontrol == false && ammo >=0 )
            {
               
                this.ammo = ammo - minusAmmo;
                shoot(gunDirection.X, gunDirection.Y);
                
            }
        }

        
        private void shoot(float x, float y)
        {

            bullet = new Bullet(x, y, this);
            Bullets.Add(bullet);
            sound.ShootLaserOne();

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

            

            for (int i= 0; i < 2; i++)
            {
                for(int j= 0; j< logic.Player[i].bullets.Count; j++)
                {
                 
                        if (logic.Player[i].Bullets[j].Bbullet != null)
                        {

                            if (logic.Player[i].Bullets[j].Bbullet.Intersects(model.Opponent[0]))
                            {
                                logic.Player[i].Bullets.RemoveAt(j);

                            }
                        }
                    
                }

                if(logic.Player[i] != this)
                {
                    for(int k = 0; k< logic.Player[i].Bullets.Count; k++)
                    {
                        if (logic.Player[i].Bullets[k].Bbullet != null)
                        {

                            if (logic.Player[i].Bullets[k].Bbullet.Intersects(this.pplayer))
                            {
                                logic.Player[i].Bullets.RemoveAt(k);
                                sound.doHurt();
                                life = life - minusLife;
                            }
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

        public int Angle
        {
            get
            {
                return Angle1;
            }

            set
            {
                Angle1 = value;
            }
        }

        public int Angle1
        {
            get
            {
                return angle;
            }

            set
            {
                angle = value;
            }
        }

        public Texture[] Texture
        {
            get
            {
                return texture;
            }

            set
            {
                texture = value;
            }
        }

        public Texture CurrentTexture
        {
            get
            {
                return currentTexture;
            }

            set
            {
                currentTexture = value;
            }
        }

        public Texture[] Texture1
        {
            get
            {
                return texture1;
            }

            set
            {
                texture1 = value;
            }
        }

        public float StartLife
        {
            get
            {
                return startLife;
            }

            set
            {
                startLife = value;
            }
        }

        public float StartAmmo
        {
            get
            {
                return startAmmo;
            }

            set
            {
                startAmmo = value;
            }
        }

        public float StartBoost
        {
            get
            {
                return startBoost;
            }

            set
            {
                startBoost = value;
            }
        }

        public float StartBoost1
        {
            get
            {
                return startBoost;
            }

            set
            {
                startBoost = value;
            }
        }

        public float StartAmmo1
        {
            get
            {
                return startAmmo;
            }

            set
            {
                startAmmo = value;
            }
        }

        public bool Shootcontrol
        {
            get
            {
                return shootcontrol;
            }

            set
            {
                shootcontrol = value;
            }
        }
    }
}
