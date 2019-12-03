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
    class ZombieManager : DrawableGameComponent
    {
        Tower Tower;
        public double ZombieMultiplier;
        public List<Zombie> Zombies { get; private set; }
        List<Zombie> DeadZombies;

        public ZombieManager(Game game, Tower player) : base(game)
        {
            Tower = player;
            ZombieMultiplier = 1;
            this.Zombies = new List<Zombie>();
            this.DeadZombies = new List<Zombie>();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (!ScoreManager.Lose)
            {
                foreach (var zombie in this.Zombies)
                {
                    zombie.Update(gameTime);

                    //if(zombie.PerPixelCollision(Tower))
                        if (zombie.Intersects(Tower))
                        {
                        Tower.GettingHit(zombie.attack);
                        zombie.Speed = 0;
                    }
                    if (zombie.health <= 0)
                    {
                        DeadZombies.Add(zombie);
                    }
                }
            UpdateRemoveDisabledZombies();
            base.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime)
        {

            foreach (var Zombie in this.Zombies)
            {
                Zombie.Draw(gameTime);
            }

            base.Draw(gameTime);
        }

        private void UpdateRemoveDisabledZombies()
        {

            foreach (var Zombie in DeadZombies)
            {
                Zombies.Remove(Zombie);
                ScoreManager.Score += 100;
                ZombieMultiplier += 0.05;
            }
            DeadZombies.Clear();
        }
    }
}
