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
    enum Weapons { Glock, DualGlock, P90, M4ShotGun, ScarL, M40A3, Minigun, Empty };
    class Gun : DrawableSprite
    {

        Player player;

        float Spin;
        public string name;
        string LoadName;
        public int Attack, Spread, CoolDown, MaxClip, RemainingClip;
        public bool Shot;
        public int TimeBetweenShots { get; private set; }

        public Weapons _Weapons;
        public int ReloadTime { get; private set; }


        public Gun(Game game, Player p, Weapons weapons) : base(game)
        {
            Shot = false;
            player = p;
            _Weapons = weapons;
        }
        public void Load()
        {
            LoadContent();
        }

        public void ConfirmWeapons()
        {
            if (_Weapons == Weapons.Glock)
            {
                name = "Glock";
                Attack = 20;
                Spread = 4;
                MaxClip = 12;
                ReloadTime = 100;
                TimeBetweenShots = 0;
            }
            if (_Weapons == Weapons.DualGlock)
            {
                name = "Dual Glock";
                Attack = 40;
                Spread = 4;
                MaxClip = 24;
                ReloadTime = 200;
                TimeBetweenShots = 0;
            }
            if (_Weapons == Weapons.P90)
            {
                name = "P90";
                Attack = 30;
                Spread = 4;
                MaxClip = 50;
                ReloadTime = 200;
                TimeBetweenShots = 5;
            }
            if (_Weapons == Weapons.M4ShotGun)
            {
                name = "M4 ShotGun";
                Attack = 50;
                Spread = 150;
                MaxClip = 8;
                ReloadTime = 250;
                TimeBetweenShots = 5;
            }
            if (_Weapons == Weapons.ScarL)
            {
                name = "Scar-L";
                Attack = 45;
                Spread = 2;
                MaxClip = 30;
                ReloadTime = 260;
                TimeBetweenShots = 7;
            }
            if (_Weapons == Weapons.M40A3)
            {
                name = "M40A3";
                Attack = 1000;
                Spread = 0;
                MaxClip = 5;
                ReloadTime = 350;
                TimeBetweenShots = 20;
            }
            if (_Weapons == Weapons.Minigun)
            {
                name = "Minigun";
                Attack = 35;
                Spread = 5;
                MaxClip = 500;
                ReloadTime = 2000;
                TimeBetweenShots = 0;
            }
            if (_Weapons == Weapons.Empty)
            {
                name = "Empty";
                Attack = 0;
                Spread = 0;
                MaxClip = 0;
                ReloadTime = 0;
                TimeBetweenShots = 0;
            }
            RemainingClip = MaxClip;
        }
        protected override void LoadContent()
        {
            ConfirmWeapons();

            if (_Weapons == Weapons.Glock) { LoadName = "Glock"; }
            if (_Weapons == Weapons.DualGlock) { LoadName = "DualGlock"; }
            if (_Weapons == Weapons.P90) { LoadName = "P90"; }
            if (_Weapons == Weapons.M4ShotGun) { LoadName = "M4Shotgun"; }
            if (_Weapons == Weapons.ScarL) { LoadName = "ScarL"; }
            if (_Weapons == Weapons.M40A3) { LoadName = "M40A3"; }
            if (_Weapons == Weapons.Minigun) { LoadName = "Minigun"; }
            if (_Weapons == Weapons.Empty) { LoadName = "Empty"; }

            this.spriteTexture = this.Game.Content.Load<Texture2D>(LoadName);
#if DEBUG
            this.ShowMarkers = false;
#endif

            base.LoadContent();
            this.Origin = new Vector2(this.spriteTexture.Width / 2, this.spriteTexture.Height / 2);

            this.Location = player.Location;
        }

        public override void Update(GameTime gameTime)
        {
            CoolDown--;
            CoolDown = MathHelper.Clamp(CoolDown, 0, 1000000);

            this.Spin = player.Spin;
            this.Rotate = (float)MathHelper.ToDegrees(Spin - (float)(Math.PI / 2));
            this.Location = player.Location;


            base.Update(gameTime);
        }

        public int Reload(int r)
        {
            if (r != 0)
            {
                r = MathHelper.Clamp(r, 0, this.MaxClip);
                r = MathHelper.Clamp(r, 0, this.MaxClip - this.RemainingClip);
                RemainingClip += r;
                this.CoolDown = this.ReloadTime;
            }
            return r;
        }
    }
}
