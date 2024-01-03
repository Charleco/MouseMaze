using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PrimitiveBuddy;

namespace MouseMaze
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Primitive _prim;
        public Maze maze;
        private Vector2 MousePos;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            int winWidth = GraphicsDevice.Viewport.Width;
            int winHeight = GraphicsDevice.Viewport.Height;
            maze = new Maze(30, winWidth, winHeight);
            maze.InitMaze();
            MousePos = new(_graphics.GraphicsDevice.Viewport.Width / 2, _graphics.GraphicsDevice.Viewport.Height / 2);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _prim = new(_graphics.GraphicsDevice, _spriteBatch);
        }

        protected override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            MousePos.X = mouseState.X;
            MousePos.Y = mouseState.Y;
            if ((MousePos.X > 0 && MousePos.X < _graphics.GraphicsDevice.Viewport.Width) && (MousePos.Y > 0 && MousePos.Y < _graphics.GraphicsDevice.Viewport.Height))
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                    maze.AddCheese((int)MousePos.X / maze.GetScale(), (int)MousePos.Y / maze.GetScale());

                if (mouseState.RightButton == ButtonState.Pressed)
                    maze.AddMouse((int)MousePos.X / maze.GetScale(), (int)MousePos.Y / maze.GetScale());
            }
            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            maze.MouseListUpdate(dt);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            maze.DrawMaze(_prim);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
