using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Test_Game_Using_Monogame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D playerFace;
        Vector2 position = Vector2.Zero;
        Vector2 velocity = Vector2.Zero;
        SpriteFont spriteFont;
        int speed = 400;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();

            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            position.X += 10;
            position.Y += 10;
            _graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            playerFace = Content.Load<Texture2D>("t");
            spriteFont = Content.Load<SpriteFont>("BasicFont");
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || state.IsKeyDown(Keys.Escape))
                Exit();

            //Movewment with WASD
            if (state.IsKeyDown(Keys.A))
                velocity.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (state.IsKeyDown(Keys.D))
                velocity.X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (state.IsKeyDown(Keys.W))
                velocity.Y -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (state.IsKeyDown(Keys.S))
                velocity.Y += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            velocity.X *= (float)Math.Pow(0.8f, (float)gameTime.ElapsedGameTime.TotalSeconds);
            velocity.Y *= (float)Math.Pow(0.8f, (float)gameTime.ElapsedGameTime.TotalSeconds);

            if (position.X < 0)
            {
                position.X = 0;
                velocity.X *= -1;
            }

            if (position.Y < 0) 
            {
                position.Y = 0;
                velocity.Y *= -1;
            }

            if (position.X > _graphics.PreferredBackBufferWidth - playerFace.Width) 
            { 
                position.X = _graphics.PreferredBackBufferWidth - playerFace.Width;
                velocity.X *= -1;
            }

            if (position.Y > _graphics.PreferredBackBufferHeight - playerFace.Height) 
            { 
                velocity.Y = _graphics.PreferredBackBufferHeight - playerFace.Height;
                velocity.Y *= -1;
            }

            position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Bisque);

            _spriteBatch.Begin();
            _spriteBatch.Draw(playerFace, position, color: Color.White);
            _spriteBatch.DrawString(spriteFont, "What is up DramaAlert Nation " + position.ToString(), Vector2.Zero, Color.Black);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
