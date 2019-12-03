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
    class GunManager : DrawableGameComponent
    {
        public static Gun[] Guns = new Gun[7];
        public static int Gun, PistolAmmo, ShotgunAmmo, HeavyAmmo;
        PlayerController Controller;
        Player player;



        public GunManager(Game game, Player p) : base(game)
        {
            Gun = 0;
            Controller = new PlayerController(game);
            player = p;
            for (int w = 0; w < 7; w++)
            {
                Guns[w] = new Gun(game, player, Weapons.Empty);
            }

            Guns[0] = new Gun(game, player, Weapons.Glock);
            PistolAmmo = 100;
            ShotgunAmmo = 0;
            HeavyAmmo = 0;
        }

        public override void Update(GameTime gameTime)
        {
            if (!ScoreManager.Lose)
            {
                Controller.HandleInput(gameTime);
                ChangeWeapons();
                CheckReload();

                Guns[Gun].Update(gameTime);

                Draw(gameTime);

                base.Update(gameTime);
            }
        }


        protected override void LoadContent()
        {
            for (int w = 0; w < 7; w++)
            {
                Guns[w].Load();
            }
            base.LoadContent();
        }
        public override void Draw(GameTime gameTime)
        {
            Guns[Gun].Draw(gameTime);

            base.Draw(gameTime);
        }
        public void CheckReload()
        {
            if (Controller.Reload == true)
            {
                if (Guns[Gun].CoolDown == 0 && Guns[Gun].RemainingClip != Guns[Gun].MaxClip)
                {
                    if (Guns[Gun]._Weapons == Weapons.Glock || Guns[Gun]._Weapons == Weapons.DualGlock || Guns[Gun]._Weapons == Weapons.P90)
                    {
                        PistolAmmo -= Guns[Gun].Reload(PistolAmmo);
                        Guns[Gun].CoolDown *= 3;
                        Guns[Gun].CoolDown /= 4;
                    }
                    if (Guns[Gun]._Weapons == Weapons.M4ShotGun)
                    {
                        ShotgunAmmo -= Guns[Gun].Reload(ShotgunAmmo);
                        Guns[Gun].CoolDown *= 3;
                        Guns[Gun].CoolDown /= 4;
                    }
                    if (Guns[Gun]._Weapons == Weapons.ScarL || Guns[Gun]._Weapons == Weapons.M40A3 || Guns[Gun]._Weapons == Weapons.Minigun)
                    {
                        HeavyAmmo -= Guns[Gun].Reload(HeavyAmmo);
                        Guns[Gun].CoolDown *= 3;
                        Guns[Gun].CoolDown /= 4;
                    }
                }
            }
        }
        private void ChangeWeapons()
        {
            if (Controller._1Pressed && !Controller.ShiftPressed) { Gun = 0; }
            if (Controller._2Pressed && !Controller.ShiftPressed) { Gun = 1; }
            if (Controller._3Pressed && !Controller.ShiftPressed) { Gun = 2; }
            if (Controller._4Pressed) { Gun = 3; }
            if (Controller._5Pressed) { Gun = 4; }
            if (Controller._6Pressed) { Gun = 5; }
            if (Controller._7Pressed) { Gun = 6; }

            Gun = MathHelper.Clamp(Gun, 0, 6);
        }

    }
}
