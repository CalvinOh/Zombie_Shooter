using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Sprite;
using MonoGameLibrary.Util;

namespace ZombieSurvivalShooter
{
    class Player : DrawableSprite
    {

        public static Vector2 postion, MousePos;

        PlayerController playerController;
        public MouseState mouseState;

        private Tower Tower;
        public float Spin = 100;


        public Player(Game game, Tower tower) : base(game)
        {
            this.Speed = 200;
            playerController = new PlayerController(game);

            Tower = tower;

        }

        protected override void LoadContent()
        {
            this.spriteTexture = this.Game.Content.Load<Texture2D>("Player");


#if DEBUG
            this.ShowMarkers = false;
#endif
            this.Location = new Vector2(this.GraphicsDevice.Viewport.Width / 2, this.GraphicsDevice.Viewport.Height / 2 - 10);


            base.LoadContent();
            this.Origin = new Vector2(this.spriteTexture.Width / 2, this.spriteTexture.Height / 2);
        }

        public override void Update(GameTime gameTime)
        {
            playerController.HandleInput(gameTime);
            if (!ScoreManager.Lose)
            {
                mouseState = Mouse.GetState();
                MousePos.X = mouseState.X;
                MousePos.Y = mouseState.Y;

                Spin = (float)Math.Atan2(MousePos.X - this.Location.X, (MousePos.Y - this.Location.Y) * -1);
                this.Rotate = (float)MathHelper.ToDegrees(Spin - (float)(Math.PI / 2));


                this.Location.X = MathHelper.Clamp(this.Location.X, Tower.Location.X + 40, Tower.Location.X + Tower.spriteTexture.Width - this.spriteTexture.Width + 20);
                this.Location.Y = MathHelper.Clamp(this.Location.Y, Tower.Location.Y + 40, Tower.Location.Y + Tower.spriteTexture.Height - this.spriteTexture.Height + 20);

                this.Direction = playerController.Direction;

                this.Location += this.Direction * (this.Speed * gameTime.ElapsedGameTime.Milliseconds / 1000);

                postion = this.Location;






                base.Update(gameTime);
            }

        }


    }
}
