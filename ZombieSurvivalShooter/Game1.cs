using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Util;

namespace ZombieSurvivalShooter
{
    /// <summary>2eme.
    /// </summary>
    /// Credit: Zombie Sprite By Jeffery Elijah Tan Xian Hong; Programming Aid by Jiening Tao
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        InputHandler input;
        GameConsole console;
        ZombieManager ZombieManager;
        ZombieSpawner ZombieSpawner;
        GunManager GunManager;
        BulletManager BulletManager;
        Tower tower;
        Player player;
        Shopping Shop;
        ScoreManager score;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            input = new InputHandler(this);
            console = new GameConsole(this);
            this.Components.Add(console);
            console.Visible = false;
            this.Components.Add(input);

            tower = new Tower(this);
            this.Components.Add(tower);

            player = new Player(this, tower);
            this.Components.Add(player);

            GunManager = new GunManager(this, player);
            this.Components.Add(GunManager);

            Shop = new Shopping(this);
            this.Components.Add(Shop);


            ZombieManager = new ZombieManager(this, tower);
            this.Components.Add(ZombieManager);

            ZombieSpawner = new ZombieSpawner(this, ZombieManager);
            this.Components.Add(ZombieSpawner);

            BulletManager = new BulletManager(this, ZombieManager);
            this.Components.Add(BulletManager);

            score = new ScoreManager(this);
            this.Components.Add(score);

        
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.graphics.PreferredBackBufferHeight = 720;
            this.graphics.PreferredBackBufferWidth = 1280;
            this.graphics.ApplyChanges();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            base.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
