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
    class Bullets : DrawableSprite
    {
        public int Attack;
        float Spin;
        public bool Penetrate, Hit;
        Random random;

        public Bullets(Game game, Vector2 TargetLocation, Vector2 GunLocation, int Damage, int Spread, bool Pen) : base(game)
        {
            Penetrate = Pen;
            this.Speed = 2000;
            this.Location = GunLocation;
            Attack = Damage;
            Hit = false;
            random = new Random();

            this.Direction = TargetLocation - GunLocation;
            this.Direction.Normalize();

            Spin = (float)Math.Atan2(this.Direction.X, this.Direction.Y * -1);
            this.Rotate = (float)MathHelper.ToDegrees(Spin - (float)(Math.PI / 2)) + (random.Next(Spread))- Spread / 2;

            this.Direction = GetDirectionVectorFromDegrees(this.Rotate);


            Spin = (float)Math.Atan2(this.Direction.X, this.Direction.Y * -1);
            this.Rotate = (float)MathHelper.ToDegrees(Spin - (float)(Math.PI / 2));

        }
        public override void Initialize()
        {

            this.spriteTexture = this.Game.Content.Load<Texture2D>("Bullet");

            base.LoadContent();
            this.Origin = new Vector2(this.spriteTexture.Width / 2, this.spriteTexture.Height / 2);
        }

        public override void Update(GameTime gameTime)
        {

            this.Location += this.Direction * (this.Speed * gameTime.ElapsedGameTime.Milliseconds / 1000);

            base.Update(gameTime);
        }

        public static Vector2 GetDirectionVectorFromDegrees(float Degrees)
        {
            Vector2 North = new Vector2(1, 0);
            float Radians = MathHelper.ToRadians(Degrees);

            var RotationMatrix = Matrix.CreateRotationZ(Radians);
            return Vector2.Transform(North, RotationMatrix);
        }
    }
}
