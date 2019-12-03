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
    class ScoreManager : DrawableGameComponent
    {
        SpriteFont font;
        public static int Level;
        public static int Score;
        public static int TowerHealthBarLength;
        public static bool Lose;
        public static bool Win;
        public Texture2D green, red, blue, HUD, ShieldTexture;
        public Game NewGame;
        public Gun Gun;

        public string[] gunsEquiped = new string[7];

        SpriteBatch sb;
        Vector2 ScoreLocation, HealthLocation, HealthBarLocation, LevelLocation, GunLocation, AmmoLocation, TimerLocation;

        public ScoreManager(Game game) : base(game)
        {

            Gun = GunManager.Guns[GunManager.Gun];
            NewGame = game;
            SetupNewGame();

        }

        private static void SetupNewGame()
        {
            Level = 0;
            Score = 0;
            Lose = false;
            Win = false;
        }


        public override void Update(GameTime gameTime)
        {
            Gun = GunManager.Guns[GunManager.Gun];


           
            if (Tower.Health >= 10)
            {
                green = new Texture2D(Game.GraphicsDevice, TowerHealthBarLength * Convert.ToInt32(Tower.Health) / 3000, 20);
                Color[] data1 = new Color[TowerHealthBarLength * Convert.ToInt32(Tower.Health) / 3000 * 20];
                for (int i = 0; i < data1.Length; i++)
                {
                    data1[i] = Color.Green;
                }
                green.SetData(data1);
            }
            else
            {
                Lose = true;
            }
            if (Tower.Shield > 1)
            {
                blue = new Texture2D(Game.GraphicsDevice, TowerHealthBarLength * Convert.ToInt32(Tower.Shield) / 1000, 20);
                Color[] data2 = new Color[TowerHealthBarLength * Convert.ToInt32(Tower.Shield) / 1000 * 20];
                for (int i = 0; i < data2.Length; i++)
                {
                    data2[i] = Color.Blue * 0.3f;
                }
                blue.SetData(data2);
            }

            for (int w = 0; w < 7; w++)
            {
                gunsEquiped[w] = " ";

            }
            gunsEquiped[GunManager.Gun] = "> ";

            base.Update(gameTime);
        }

        protected override void LoadContent()
        {


            TowerHealthBarLength = this.Game.GraphicsDevice.Viewport.Width / 2;

            red = new Texture2D(Game.GraphicsDevice, TowerHealthBarLength, 20);
            green = new Texture2D(Game.GraphicsDevice, TowerHealthBarLength * Convert.ToInt32(Tower.Health) / 3000, 20);
            blue = new Texture2D(Game.GraphicsDevice, TowerHealthBarLength * Convert.ToInt32(Tower.Health) / 3000, 20);
            this.HUD = this.Game.Content.Load<Texture2D>("HUD");

            Color[] data = new Color[TowerHealthBarLength * 20];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = Color.Red;
            }
            red.SetData(data);

            Color[] data1 = new Color[TowerHealthBarLength * Convert.ToInt32(Tower.Health) / 3000 * 20];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = Color.Green;
            }
            green.SetData(data1);

            sb = new SpriteBatch(this.Game.GraphicsDevice);
            font = this.Game.Content.Load<SpriteFont>("HighTower");
            this.ShieldTexture = this.Game.Content.Load<Texture2D>("Shield");
            HealthLocation = new Vector2(20, this.Game.GraphicsDevice.Viewport.Height - 40);
            HealthBarLocation = HealthLocation + new Vector2(170, 0);
            LevelLocation = new Vector2(this.Game.GraphicsDevice.Viewport.Width / 2 - 200, 10);
            ScoreLocation = new Vector2(this.Game.GraphicsDevice.Viewport.Width / 2 - 30, 10);
            TimerLocation = new Vector2(this.Game.GraphicsDevice.Viewport.Width / 2 + 130, 10);
            GunLocation = new Vector2(this.Game.GraphicsDevice.Viewport.Width - 160, this.GraphicsDevice.Viewport.Height - 60);
            AmmoLocation = new Vector2(GunLocation.X, GunLocation.Y - 190);

            base.LoadContent();
        }
        public override void Draw(GameTime gameTime)
        {

            sb.Begin();
            sb.Draw(red, HealthBarLocation, Color.Red);
            sb.Draw(green, HealthBarLocation, Color.Green);
            sb.Draw(blue, HealthBarLocation, Color.Blue);
            sb.Draw(HUD, new Vector2(0, 0), Color.White);


            if (Tower.Shield > 1)
            {
                sb.DrawString(font, "Tower Health: " + Convert.ToInt32(Tower.Health), HealthLocation, Color.LightBlue);
                sb.Draw(ShieldTexture, new Vector2(this.GraphicsDevice.Viewport.Width / 2 - ShieldTexture.Width / 2, this.GraphicsDevice.Viewport.Height / 2 - ShieldTexture.Width / 2 - 12), Color.White * Tower.Opacity);

            }
            else if (Convert.ToInt32(Tower.Health) <= 500 && Convert.ToInt32(Tower.Health) > 300)
            {
                sb.DrawString(font, "Tower Health: " + Convert.ToInt32(Tower.Health), HealthLocation, Color.Yellow);
            }
            else if (Convert.ToInt32(Tower.Health) <= 300 && Convert.ToInt32(Tower.Health) > 100)
            {
                sb.DrawString(font, "Tower Health: " + Convert.ToInt32(Tower.Health), HealthLocation, Color.Orange);
            }
            else if (Convert.ToInt32(Tower.Health) <= 100)
            {
                sb.DrawString(font, "Tower Health: " + Convert.ToInt32(Tower.Health), HealthLocation, Color.Red);
            }
            else
            {
                sb.DrawString(font, "Tower Health: " + Convert.ToInt32(Tower.Health), HealthLocation, Color.White);
            }
            //Draw Score, Time, Waves
            sb.DrawString(font, "Score: " + Score, ScoreLocation, Color.White);
            sb.DrawString(font, "Time: " + ZombieSpawner.CoolDownTimer, TimerLocation, Color.White);
            sb.DrawString(font, "Wave: " + Level, LevelLocation, Color.White);
            //Draw Guns 
            for (int w = 0; w < 7; w++)
            {
                sb.DrawString(font, "" + gunsEquiped[w] + GunManager.Guns[w].name, GunLocation - new Vector2(-30, 16 * (w + 2)), Color.White);

            }
            //Draw Ammo
            sb.DrawString(font, "Pistol Ammo: " + GunManager.PistolAmmo, AmmoLocation, Color.White);
            sb.DrawString(font, "Shotgun Ammo: " + GunManager.ShotgunAmmo, AmmoLocation + new Vector2(0, 16), Color.White);
            sb.DrawString(font, "Heavy Ammo: " + GunManager.HeavyAmmo, AmmoLocation + new Vector2(0, 32), Color.White);
            sb.DrawString(font, "Bullets Left: " + Gun.RemainingClip + "/" + Gun.MaxClip, GunLocation, Color.White);
            sb.DrawString(font, "Reload Time: " + Gun.CoolDown, GunLocation + new Vector2(0, 20), Color.White);
            //Draw instructions
            sb.DrawString(font, "Controls:", GunLocation + new Vector2(-270, -125), Color.White);
            sb.DrawString(font, "Change Guns: Number Keys 1 to 7", GunLocation + new Vector2(-270, -105), Color.White);
            sb.DrawString(font, "Reload Gun: Press R", GunLocation + new Vector2(-270, -85), Color.White);
            sb.DrawString(font, "Repair Tower: Press Space", GunLocation + new Vector2(-270, -65), Color.White);
            sb.DrawString(font, "Buy Ammo: ", GunLocation + new Vector2(-270, -45), Color.White);
            sb.DrawString(font, "Pistol Ammo: Shift + 1", GunLocation + new Vector2(-270, -25), Color.White);
            sb.DrawString(font, "Shotgun Ammo: Shift + 2", GunLocation + new Vector2(-270, -5), Color.White);
            sb.DrawString(font, "Heavy Ammo: Shift + 3", GunLocation + new Vector2(-270, 15), Color.White);
            sb.DrawString(font, "Purchase Shield: Press U, Cost: 15000", GunLocation + new Vector2(-270, 35), Color.White);
            //Lose
            if (Lose)
            {
                sb.DrawString(font, "FINAL SCORE: " + Score, new Vector2(this.Game.GraphicsDevice.Viewport.Width / 2 - 70, this.Game.GraphicsDevice.Viewport.Height / 2 - 50), Color.White);
                sb.DrawString(font, "      You Died! ", new Vector2(this.Game.GraphicsDevice.Viewport.Width / 2 - 50, this.Game.GraphicsDevice.Viewport.Height / 2 - 10), Color.Red);
                sb.DrawString(font, "press ENTER to restart", new Vector2(this.Game.GraphicsDevice.Viewport.Width / 2 - 70, this.Game.GraphicsDevice.Viewport.Height / 2 + 10), Color.White);
                sb.DrawString(font, "  press ESC to quit", new Vector2(this.Game.GraphicsDevice.Viewport.Width / 2 - 60, this.Game.GraphicsDevice.Viewport.Height / 2 + 30), Color.White);
            }


            sb.End();

            base.Draw(gameTime);
        }
    }
}
