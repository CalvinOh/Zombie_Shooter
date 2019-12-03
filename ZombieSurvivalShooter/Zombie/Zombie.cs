using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Sprite;
using MonoGameLibrary.Util;
using System;

namespace ZombieSurvivalShooter
{
    class Zombie : DrawableSprite
    {
        public Vector2 ZombieDirection, PlayerDirection;
        public float spin;
        public float health;
        public int attack, a;

        public Zombie(Game game) : base(game)
        {
            a = ScoreManager.Level;
            this.Speed = 100 + (1 - 1 / a) * 10 - 25;
            ZombieDirection = new Vector2(0, 0);
            attack = a / 2 - 2;
            
            health = (a * 10 / 4);
        }

        protected override void LoadContent()
        {
            this.spriteTexture = this.Game.Content.Load<Texture2D>("Blud2");
#if DEBUG
            this.ShowMarkers = false;
#endif
            attack = MathHelper.Clamp(attack, 1, 100); //Original 1, 100
            health = MathHelper.Clamp(health, 1, 999999999);
            this.Speed = MathHelper.Clamp(this.Speed, 1, 500);
            base.LoadContent();
            this.Origin = new Vector2(this.spriteTexture.Width / 2, this.spriteTexture.Height / 2);
        }

        public override void Update(GameTime gameTime)
        {

            PlayerDirection = Player.postion - this.Location;

            spin = (float)Math.Atan2( this.Direction.X,this.Direction.Y * -1);
            this.Rotate = (float)MathHelper.ToDegrees(spin - (float)(Math.PI / 2));

            this.Direction += PlayerDirection;
            this.Direction.Normalize();

            this.Location += this.Direction * (this.Speed * gameTime.ElapsedGameTime.Milliseconds / 1000);
            

            base.Update(gameTime);
        }

        public void GetHit(Bullets b)
        {
            this.health -= b.Attack;

        }
    }
}
