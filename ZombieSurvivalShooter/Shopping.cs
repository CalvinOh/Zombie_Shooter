using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGameLibrary.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZombieSurvivalShooter
{
    class Shopping : DrawableGameComponent
    {
        PlayerController controller;
        public int NextGun;

        public Shopping(Game game) : base(game)
        {
            controller = new PlayerController(game);
        }
        protected override void LoadContent()
        {
            NextGun = 1;
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            controller.HandleInput(gameTime);

            CheckBuy();
            base.Update(gameTime);
        }

        private void CheckBuy()
        {
            if(!ScoreManager.Lose)
            {
                if (controller.BuyPistol && controller.ShiftPressed)
                {
                    if (ScoreManager.Score >= 100)
                    {
                        ScoreManager.Score -= 100;
                        GunManager.PistolAmmo += 10;
                    }
                }
                if (controller.BuyShotgun && controller.ShiftPressed)
                {
                    if (ScoreManager.Score >= 200)
                    {
                        ScoreManager.Score -= 200;
                        GunManager.ShotgunAmmo += 5;
                    }
                }
                if (controller.BuyHeavy && controller.ShiftPressed)
                {
                    if (ScoreManager.Score >= 300)
                    {
                        ScoreManager.Score -= 300;
                        GunManager.HeavyAmmo += 10;
                    }
                }

                if (controller.Spacepressed)
                {
                    if (ScoreManager.Score >= 10 && Tower.Health < 3000)
                    {
                        ScoreManager.Score -= 10;
                        Tower.Health += 10;
                    }
                }
                if (controller.BuyShield)
                {
                    if (ScoreManager.Score >= 15000)
                    {
                        ScoreManager.Score -= 15000;
                        Tower.HaveShield = true;
                    }
                    //Buy a shield
                }
                //Unlock Weapons By Score
                if (ScoreManager.Score >= 1500 && ScoreManager.Score <= 1600)
                {
                    UnlockGuns(NextGun);
                }
                if (ScoreManager.Score >= 3000 && ScoreManager.Score <= 3100)
                {
                    NextGun = 2;
                    UnlockGuns(NextGun);
                }
                if (ScoreManager.Score >= 6000 && ScoreManager.Score <= 6100)
                {
                    NextGun = 3;
                    UnlockGuns(NextGun);
                }
                if (ScoreManager.Score >= 8000 && ScoreManager.Score <= 8100)
                {
                    NextGun = 4;
                    UnlockGuns(NextGun);
                }
                if (ScoreManager.Score >= 10000 && ScoreManager.Score <= 11000)
                {
                    NextGun = 5;
                    UnlockGuns(NextGun);
                }
                if (ScoreManager.Score >= 20000 && ScoreManager.Score <= 21000)
                {
                    NextGun = 6;
                    UnlockGuns(NextGun);
                }
            }


        }
        private void UnlockGuns(int a)
        {
            if (a == 1)
            {
                GunManager.Guns[a]._Weapons = Weapons.DualGlock;

            }
            if (a == 2)
            {
                GunManager.Guns[a]._Weapons = Weapons.P90;

            }
            if (a == 3)
            {
                GunManager.Guns[a]._Weapons = Weapons.M4ShotGun;

            }
            if (a == 4)
            {
                GunManager.Guns[a]._Weapons = Weapons.ScarL;

            }
            if (a == 5)
            {
                GunManager.Guns[a]._Weapons = Weapons.M40A3;

            }
            if (a == 6)
            {
                GunManager.Guns[a]._Weapons = Weapons.Minigun;

            }
            GunManager.Guns[a].Load();

        }
    }
}
