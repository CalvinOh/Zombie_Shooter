using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGameLibrary.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Util;

namespace ZombieSurvivalShooter
{
    class BulletManager : DrawableGameComponent
    {
        PlayerController Controller;
        Gun Gun;
        Game Game1;
        MouseState MouseState;
        ZombieManager ZombieManager;
        Bullets Bullets;

        List<Bullets> bullets;
        List<Bullets> UsedBullets;
        Vector2 MousePostion;

        Random random;

        public BulletManager(Game game, ZombieManager zombieManager) : base(game)
        {
            ZombieManager = zombieManager;
            Controller = new PlayerController(game);
  
            Gun = GunManager.Guns[GunManager.Gun];
            Game1 = game;
            bullets = new List<Bullets>();
            UsedBullets = new List<Bullets>();

            random = new Random();

        }

        public override void Initialize()
        {

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (!ScoreManager.Lose)
            {
                Gun = GunManager.Guns[GunManager.Gun];
             

                foreach (var a in bullets)
                {
                    a.Update(gameTime);
                }
                UpdateCheckHit(gameTime);
                CheckOffScreenAndRemoveBullets();
                Controller.HandleInput(gameTime);
                CheckShoot();
            }
        }


        public override void Draw(GameTime gameTime)
        {
            foreach (var a in bullets)
            {
                a.Draw(gameTime);
            }
            base.Draw(gameTime);
        }

        private void CheckShoot()
        {
            if (Controller.ShootGun && Gun._Weapons != Weapons.Empty)
            {
                ShootGun(Gun);
                Gun.Shot = true;
            }
            else
            {
                Gun.Shot = false;
            }
        }

        public void CheckOffScreenAndRemoveBullets()
        {
            foreach (var a in bullets)
            {
                if (a.Location.X >= this.Game.GraphicsDevice.Viewport.Width || a.Location.X <= 0 || a.Location.Y <= 0 || a.Location.Y >= this.Game.GraphicsDevice.Viewport.Height)
                    UsedBullets.Add(a);
            }
            Clearbullet();

        }
        private void Clearbullet()
        {
            foreach (var a in UsedBullets)
            {
                bullets.Remove(a);

            }
           // UsedBullets.Clear();
        }
        private void UpdateCheckHit(GameTime gameTime)
        {
            foreach (Zombie z in ZombieManager.Zombies)
            {
                foreach (var b in bullets)
                    if (b.Intersects(z))
                    {

                        z.GetHit(b);
                        if (!b.Penetrate)
                        {
                            UsedBullets.Add(b);
                        }
                    }

            }
        }

        
        public void ShootGun(Gun BMGun)
        {
            MouseState = Mouse.GetState();
            MousePostion.X = MouseState.X;
            MousePostion.Y = MouseState.Y;
           

            if(BMGun.CoolDown <= 0 && BMGun.RemainingClip > 0)
            {
                //Semis
                if(!BMGun.Shot)
                {
                    if(BMGun._Weapons == Weapons.Glock || BMGun._Weapons == Weapons.DualGlock)
                    {
                        Bullets = new Bullets(Game1, MousePostion, BMGun.Location, BMGun.Attack, BMGun.Spread, false);
                        Bullets.Initialize();
                        bullets.Add(Bullets);
                        BMGun.RemainingClip--;
                        BMGun.CoolDown = Gun.TimeBetweenShots;
                    }
                    //Shotgun
                    if (BMGun._Weapons == Weapons.M4ShotGun)
                    {
                        for(int i = 0; i < 20; i++)
                        {
                            Bullets = new Bullets(Game1, MousePostion, BMGun.Location, BMGun.Attack, random.Next(BMGun.Spread - 50) + 1, false);
                            Bullets.Initialize();
                            bullets.Add(Bullets);
                        }
                            BMGun.RemainingClip--;
                            BMGun.CoolDown = Gun.TimeBetweenShots;
                    }
                    //Sniper
                    if (BMGun._Weapons == Weapons.M40A3)
                    {
                        Bullets = new Bullets(Game1, MousePostion, BMGun.Location, BMGun.Attack, BMGun.Spread, true);
                        Bullets.Initialize();
                        bullets.Add(Bullets);
                        BMGun.RemainingClip--;
                        BMGun.CoolDown = Gun.TimeBetweenShots;
                    }
                    if(BMGun._Weapons == Weapons.Empty)
                    {
                        //Nothing in Hand, Do nothing;
                    }
                }
                //Autos
                if (BMGun._Weapons == Weapons.P90 || BMGun._Weapons == Weapons.ScarL || BMGun._Weapons == Weapons.Minigun)
                {
                    Bullets = new Bullets(Game1, MousePostion, BMGun.Location, BMGun.Attack, BMGun.Spread, false);
                    Bullets.Initialize();
                    bullets.Add(Bullets);
                    BMGun.RemainingClip--;
                    BMGun.CoolDown = Gun.TimeBetweenShots;
                }

                if (BMGun.RemainingClip == 0)
                {
                    if (BMGun._Weapons == Weapons.Glock || BMGun._Weapons == Weapons.DualGlock || BMGun._Weapons == Weapons.P90)
                    { GunManager.PistolAmmo -= BMGun.Reload(GunManager.PistolAmmo); }
                    if (BMGun._Weapons == Weapons.M4ShotGun)
                    { GunManager.ShotgunAmmo -= BMGun.Reload(GunManager.ShotgunAmmo); }
                    if (BMGun._Weapons == Weapons.ScarL || BMGun._Weapons == Weapons.M40A3 || BMGun._Weapons == Weapons.Minigun)
                    { GunManager.HeavyAmmo -= BMGun.Reload(GunManager.HeavyAmmo); }
                }


            }




        }


    }
}
