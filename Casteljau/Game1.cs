using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Casteljau
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Texture2D SimpleTexture;
        List<Point> points = new List<Point>();
        Int32[] pixel = { 0xFFFFFF };
        private SpriteFont font;
        private int height = 2;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            this.IsMouseVisible = true;
            SimpleTexture = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            SimpleTexture.SetData<Int32>(pixel, 0, SimpleTexture.Width * SimpleTexture.Height);

            
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
            font = Content.Load<SpriteFont>("text"); // Use the name of your sprite font file here instead of 'Score'.
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
            MouseState mouseState = Mouse.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                points.Add(mouseState.Position);
//                System.Threading.Thread.Sleep(100);
                }

            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            for (int i = 0; i < points.Count-1;i++)
            {
                Vector2 rect = new Vector2(points[i+1].X, points[i + 1].Y) - new Vector2(points[i].X,points[i].Y);
                float angle = (float)Math.Atan2(rect.Y, rect.X);
                spriteBatch.Draw(SimpleTexture, new Rectangle(points[i].X, points[i].Y, (int)rect.Length(), height), null,Color.White,angle, new Vector2(0, 0),SpriteEffects.None, 0F);
            }
            spriteBatch.Draw(SimpleTexture, Vector2.Add(new Vector2(10,10), new Vector2(20, 20)));
            //drawCasteljau(points, spriteBatch);
            spriteBatch.Draw(SimpleTexture,new Rectangle(0,10,100,1),null,Color.AliceBlue);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void drawCasteljau(List<Point> cpoints, SpriteBatch sb)
        {
            Point tmp;
            for (double t = 0; t <= 1 && cpoints.Count > 0; t += 0.01)
            {
                tmp = getCasteljauPoint(cpoints.Count - 1, 0, t);
                sb.Draw(SimpleTexture, new Vector2(tmp.X, tmp.Y), Color.Yellow);
            }
        }


        private Point getCasteljauPoint(int r, int i, double t)
        {
            if (r < 1) return points[i];

            Point p1 = getCasteljauPoint(r - 1, i, t);
            Point p2 = getCasteljauPoint(r - 1, i + 1, t);

            return new Point((int)((1 - t) * p1.X + t * p2.X), (int)((1 - t) * p1.Y + t * p2.Y));
        }
    }
}
