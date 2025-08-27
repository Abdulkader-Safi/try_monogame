using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Model _model;
    private SpriteFont _font;

    // Camera
    private Matrix _view;
    private Matrix _projection;
    private float _angle;
    private float _pitch;

    // Keyboard state tracking for toggle functionality
    private KeyboardState _previousKeyboardState;

    // FPS tracking
    private int _frameCount;
    private TimeSpan _elapsedTime = TimeSpan.Zero;
    private int _fps;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        // High quality graphics settings
        _graphics.GraphicsProfile = GraphicsProfile.HiDef;
        _graphics.PreferMultiSampling = true;

        // Set higher resolution for better quality
        _graphics.PreferredBackBufferWidth = 1920;
        _graphics.PreferredBackBufferHeight = 1080;
    }

    protected override void Initialize()
    {
        // Set up a simple camera
        var cameraPos = new Vector3(0, 2, 6);
        var target = Vector3.Zero;
        var up = Vector3.Up;

        _view = Matrix.CreateLookAt(cameraPos, target, up);
        _projection = Matrix.CreatePerspectiveFieldOfView(
            MathHelper.ToRadians(45f),
            GraphicsDevice.Viewport.AspectRatio,
            0.1f,
            100f
        );

        // Good defaults for 3D
        GraphicsDevice.DepthStencilState = DepthStencilState.Default;
        GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;

        // Set sampler state for smoother textures
        GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _model = Content.Load<Model>("Models/ship"); // ship.fbx → "Models/ship"
        _font = Content.Load<SpriteFont>("Font");

        // Debug: Check if model loaded and print mesh info
        Console.WriteLine($"Model loaded successfully: {_model != null}");
        if (_model != null)
        {
            Console.WriteLine($"Number of meshes: {_model.Meshes.Count}");
            foreach (var mesh in _model.Meshes)
            {
                Console.WriteLine($"Mesh: {mesh.Name}, BoundingSphere radius: {mesh.BoundingSphere.Radius}");
            }
        }
    }

    protected override void Update(GameTime gameTime)
    {
        var keyboardState = Keyboard.GetState();

        if (keyboardState.IsKeyDown(Keys.Escape))
            Exit();

        // Toggle fullscreen with F key (only on key press, not hold)
        if (keyboardState.IsKeyDown(Keys.F) && !_previousKeyboardState.IsKeyDown(Keys.F))
        {
            _graphics.IsFullScreen = !_graphics.IsFullScreen;
            _graphics.ApplyChanges();
        }

        float rotationSpeed = 3.5f; // radians per second

        // WASD rotation controls
        if (keyboardState.IsKeyDown(Keys.A))
            _angle -= rotationSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (keyboardState.IsKeyDown(Keys.D))
            _angle += rotationSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (keyboardState.IsKeyDown(Keys.W))
            _pitch -= rotationSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (keyboardState.IsKeyDown(Keys.S))
            _pitch += rotationSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Calculate FPS
        _frameCount++;
        _elapsedTime += gameTime.ElapsedGameTime;
        if (_elapsedTime >= TimeSpan.FromSeconds(1))
        {
            _fps = _frameCount;
            _frameCount = 0;
            _elapsedTime = TimeSpan.Zero;
        }

        _previousKeyboardState = keyboardState;
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // Try different scales to make model visible
        var world =
            Matrix.CreateScale(0.01f) * // Very small scale first
            Matrix.CreateRotationX(_pitch) *
            Matrix.CreateRotationY(_angle) *
            Matrix.CreateTranslation(0, 0, 0);

        // Draw each mesh with BasicEffect
        foreach (var mesh in _model.Meshes)
        {
            foreach (var effect in mesh.Effects)
            {
                if (effect is BasicEffect basicEffect)
                {
                    basicEffect.EnableDefaultLighting(); // quick lighting
                    basicEffect.PreferPerPixelLighting = true;
                    basicEffect.World = mesh.ParentBone.Transform * world;
                    basicEffect.View = _view;
                    basicEffect.Projection = _projection;
                }
            }
            mesh.Draw();
        }

        // Draw FPS text at top of screen
        _spriteBatch.Begin();
        _spriteBatch.DrawString(_font, $"FPS: {_fps}", new Vector2(10, 10), Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
