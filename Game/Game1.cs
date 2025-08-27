using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1;

public class Game1 : Game
{
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SpriteFont _font;

    private Camera _camera;
    private InputHandler _inputHandler;
    private ModelRenderer _modelRenderer;
    private FpsCounter _fpsCounter;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = GameSettings.ContentRootDirectory;
        IsMouseVisible = GameSettings.IsMouseVisible;
        Window.Title = GameSettings.Title;

        _graphics.GraphicsProfile = GameSettings.GraphicsProfile;
        _graphics.PreferMultiSampling = GameSettings.PreferMultiSampling;
        _graphics.PreferredBackBufferWidth = GameSettings.PreferredBackBufferWidth;
        _graphics.PreferredBackBufferHeight = GameSettings.PreferredBackBufferHeight;
    }

    protected override void Initialize()
    {
        _camera = new Camera();
        _inputHandler = new InputHandler();
        _modelRenderer = new ModelRenderer(GraphicsDevice);
        _fpsCounter = new FpsCounter();

        _camera.Initialize(GraphicsDevice);
        _modelRenderer.Initialize();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _font = Content.Load<SpriteFont>(GameSettings.FontPath);

        var model = Content.Load<Model>(GameSettings.ModelPath);
        _modelRenderer.LoadModel(model);
    }

    protected override void Update(GameTime gameTime)
    {
        _inputHandler.Update(gameTime);
        _fpsCounter.Update(gameTime);

        if (_inputHandler.ShouldExit)
            Exit();

        if (_inputHandler.ShouldToggleFullscreen)
        {
            _graphics.IsFullScreen = !_graphics.IsFullScreen;
            _graphics.ApplyChanges();
        }

        _camera.UpdateRotation(_inputHandler.AngleDelta, _inputHandler.PitchDelta);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        var world = _camera.CreateWorldMatrix();
        _modelRenderer.Draw(world, _camera.View, _camera.Projection);

        _spriteBatch.Begin();
        _fpsCounter.Draw(_spriteBatch, _font, new Vector2(10, 10), Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
