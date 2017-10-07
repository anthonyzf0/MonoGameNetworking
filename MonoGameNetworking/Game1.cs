using Encog.ML.Data;
using Encog.Neural.Networks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameNetworking.GameItself;
using System.Threading;

namespace MonoGameNetworking
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        //Graphics devises
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Renderer
        Render renderer;

        //Game Itself
        SnakeGame debugGame = new SnakeGame();
        BasicNetwork net;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

        }
        
        protected override void Initialize()
        {
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            renderer = new Render(spriteBatch, GraphicsDevice);
            graphics.PreferredBackBufferWidth = 700;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 700;   // set this value to the desired height of your window
            graphics.ApplyChanges();

            net = Trainer.train(renderer);
            debugGame = new SnakeGame();

        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            var input = debugGame.inputs();
            IMLData output = net.Compute(input);
            debugGame.update(output[0], output[1]);
            
        }

    protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            debugGame.draw(renderer);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
