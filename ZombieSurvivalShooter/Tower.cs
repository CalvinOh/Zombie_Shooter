using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Sprite;

namespace ZombieSurvivalShooter
{
    class Tower : DrawableSprite
    {
        public static float Health, Shield, Opacity;
        public static int ShieldCooldown;
        public static bool HaveShield;

       
        Texture2D Background;


        public Tower(Game game) : base(game)
        {
            Health = 3000;
            Shield = 1000;
            Opacity = (float)(Shield / 1000);
            HaveShield = false;
           
        }
        public void Load()
        {
            LoadContent();
        }
        protected override void LoadContent()
        {


            this.spriteTexture = this.Game.Content.Load<Texture2D>("WoodenTower");
            this.Background = this.Game.Content.Load<Texture2D>("ZombieGameBackground");

#if DEBUG
            this.ShowMarkers = false;
#endif
            //this.Origin = new Vector2(this.spriteTexture.Width / 2, this.spriteTexture.Height / 2);
            this.Location = new Vector2(this.GraphicsDevice.Viewport.Width / 2 - this.spriteTexture.Width / 2, this.GraphicsDevice.Viewport.Height / 2 - this.spriteTexture.Width / 2 - 10);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (!ScoreManager.Lose)
            {

              

                if (HaveShield)
                {
                    if (ShieldCooldown <= 0)
                    {
                        Shield++;
                        Opacity = (float)(Shield / 1000);
                    }
                    ShieldCooldown--;
                }
                else
                {
                    Shield = 0;
                }



                Health = MathHelper.Clamp(Health, 0, 3000);
                Shield = MathHelper.Clamp(Shield, 0, 1000);
                ShieldCooldown = MathHelper.Clamp(ShieldCooldown, 0, 100);
                base.Update(gameTime);

            }
            if (Health == 0)
            {
                ScoreManager.Lose = true;
            }
            if (ScoreManager.Lose && PlayerController.EnterPressed)
            {
                Program.restart = true;
                Game.Exit();
            }
        }
        public void GettingHit(int hit)
        {
            if (Shield <= 0)
            {
                Health -= hit;

            }
            else
            {
                Shield -= hit;
                ShieldCooldown = 100;
                Opacity = (float)(Shield / 1000);
            }
        }



        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(Background, new Vector2(0, 0), Color.White);

            spriteBatch.End();


            base.Draw(gameTime);
        }

    }
}
