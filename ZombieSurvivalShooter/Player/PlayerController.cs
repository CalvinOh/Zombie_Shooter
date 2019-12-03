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
    class PlayerController
    {
        InputHandler input;
        MouseState state;
        public Vector2 Direction { get; private set; }
        public bool ShootGun, Reload, BuyShield;
        public bool ShiftPressed, BuyPistol, BuyShotgun, BuyHeavy, Spacepressed;
        public static bool EnterPressed;
        public bool _1Pressed, _2Pressed, _3Pressed, _4Pressed, _5Pressed, _6Pressed, _7Pressed;




        public PlayerController(Game game)
        {
            input = (InputHandler)game.Services.GetService(typeof(IInputHandler));
            this.Direction = Vector2.Zero;


        }
        public void HandleInput(GameTime gameTime)
        {
            this.Direction = Vector2.Zero;
            state = Mouse.GetState();
            //Movement
            if (input.KeyboardState.IsKeyDown(Keys.A)) { this.Direction += new Vector2(-1, 0); }
            if (input.KeyboardState.IsKeyDown(Keys.D)) { this.Direction += new Vector2(1, 0); }
            if (input.KeyboardState.IsKeyDown(Keys.W)) { this.Direction += new Vector2(0, -1); }
            if (input.KeyboardState.IsKeyDown(Keys.S)) { this.Direction += new Vector2(0, 1); }
            //Reload
            if (input.KeyboardState.IsKeyDown(Keys.R)) { Reload = true; } else { Reload = false; }
            //Shoot
            if (state.LeftButton == ButtonState.Pressed) { ShootGun = true; } else { ShootGun = false; }
            //Restore Tower HP
            if (input.KeyboardState.IsKeyDown(Keys.Space)) { Spacepressed = true; } else { Spacepressed = false; }
            //Buy Shield for Tower
            if (input.KeyboardState.IsKeyDown(Keys.U)) { BuyShield = true; } else { BuyShield = false; }
            //RestartGame
            if (input.KeyboardState.IsKeyDown(Keys.Enter)) { EnterPressed = true; } else { EnterPressed = false; }
            //Switch Weapons
            if (input.KeyboardState.IsKeyDown(Keys.D1)) { _1Pressed = true; } else { _1Pressed = false; }
            if (input.KeyboardState.IsKeyDown(Keys.D2)) { _2Pressed = true; } else { _2Pressed = false; }
            if (input.KeyboardState.IsKeyDown(Keys.D3)) { _3Pressed = true; } else { _3Pressed = false; }
            if (input.KeyboardState.IsKeyDown(Keys.D4)) { _4Pressed = true; } else { _4Pressed = false; }
            if (input.KeyboardState.IsKeyDown(Keys.D5)) { _5Pressed = true; } else { _5Pressed = false; }
            if (input.KeyboardState.IsKeyDown(Keys.D6)) { _6Pressed = true; } else { _6Pressed = false; }
            if (input.KeyboardState.IsKeyDown(Keys.D7)) { _7Pressed = true; } else { _7Pressed = false; }
            //Buy Ammo
            if (input.KeyboardState.IsKeyDown(Keys.LeftShift)) { ShiftPressed = true; if (input.KeyboardState.IsKeyDown(Keys.D1)) { BuyPistol = true; } else { { BuyPistol = false; } } } else { ShiftPressed = false; }
            if (input.KeyboardState.IsKeyDown(Keys.LeftShift)) { ShiftPressed = true; if (input.KeyboardState.IsKeyDown(Keys.D2)) { BuyShotgun = true; } else { { BuyShotgun = false; } } } else { ShiftPressed = false; }
            if (input.KeyboardState.IsKeyDown(Keys.LeftShift)) { ShiftPressed = true; if (input.KeyboardState.IsKeyDown(Keys.D3)) { BuyHeavy = true; } else { { BuyHeavy = false; } } } else { ShiftPressed = false; }
        }
    }
}
