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
    enum SpawnStates { Spawn, SpawnCoolDown, SpawnedZombies }
    class ZombieSpawner : DrawableGameComponent
    {
        public SpawnStates SpawnStates;
        ZombieManager zombieManager;
        public static int CoolDownTimer;
        Zombie zombie;
        Random Random, Random4;
        int LocationX, LocationY, RandomZombies;
        public ZombieSpawner(Game game, ZombieManager ZombieManager) : base(game)
        {
            zombieManager = ZombieManager;
            CoolDownTimer = 0;
            SpawnStates = SpawnStates.SpawnedZombies;

            Random = new Random();
            Random4 = new Random();

           
            
        }

        public override void Update(GameTime gameTime)
        {
            switch (SpawnStates)
            {
                case SpawnStates.Spawn:
                    ScoreManager.Level++;
                    SpawnZombies(ScoreManager.Level);
                    this.SpawnStates = SpawnStates.SpawnCoolDown;
                    CoolDownTimer = 1000 + ScoreManager.Level * 200;
                    break;
                case SpawnStates.SpawnCoolDown:
                    if (CoolDownTimer <= 0 || zombieManager.Zombies.Count == 0)
                    {
                        this.SpawnStates = SpawnStates.SpawnedZombies;

                    }
                    else
                    {
                        CoolDownTimer--;
                    }
                    break;
                case SpawnStates.SpawnedZombies:
                    if (zombieManager.Zombies.Count == 0)
                    {
                        this.SpawnStates = SpawnStates.Spawn;
                    }
                    break;
            }



            base.Update(gameTime);
        }

        public void SpawnZombies(int Level)
        {
            
           

            RandomZombies = Random.Next(Level);

            for (int i = 0; i < Math.Truncate((5 + Level + RandomZombies) + Math.Round(zombieManager.ZombieMultiplier)); i++) //Growing Amount
            //for (int i = 0; i < 1; i++) //Set Number of Zombies
            {
                zombie = new Zombie(this.Game);
                zombie.Initialize();
                LocationX = Random.Next(-10, this.GraphicsDevice.Viewport.Width + 10);
                LocationY = Random.Next(-10, this.GraphicsDevice.Viewport.Height + 10);
                int x = Random4.Next(1, 5);
                switch (x)
                {
                    case 1:
                        LocationX += this.GraphicsDevice.Viewport.Width / 2 + 400; //Right Side
                        break;
                    case 2:
                        LocationX -= this.GraphicsDevice.Viewport.Width / 2 + 400; //Left Side
                        break;
                    case 3:
                        LocationY += this.GraphicsDevice.Viewport.Height / 2 + 400; //Bottom
                        break;
                    case 4:
                        LocationY -= this.GraphicsDevice.Viewport.Height / 2 + 400; //Top
                        break;
                    default:
                        break;

                }

                zombie.Location = new Vector2(LocationX, LocationY);
                zombieManager.Zombies.Add(zombie);

            }
        }

    }
}
