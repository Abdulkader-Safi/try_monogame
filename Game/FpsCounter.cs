using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1;

public class FpsCounter
{
    private int _frameCount;
    private TimeSpan _elapsedTime = TimeSpan.Zero;
    private int _fps;

    public int CurrentFps => _fps;

    public void Update(GameTime gameTime)
    {
        _frameCount++;
        _elapsedTime += gameTime.ElapsedGameTime;

        if (_elapsedTime >= TimeSpan.FromSeconds(1))
        {
            _fps = _frameCount;
            _frameCount = 0;
            _elapsedTime = TimeSpan.Zero;
        }
    }

    public void Draw(SpriteBatch spriteBatch, SpriteFont font, Vector2 position, Color color)
    {
        spriteBatch.DrawString(font, $"FPS: {_fps}", position, color);
    }
}