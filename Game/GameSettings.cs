using Microsoft.Xna.Framework.Graphics;

namespace Game1;

public static class GameSettings
{
    public const string Title = "Game";
    public const int PreferredBackBufferWidth = 1920;
    public const int PreferredBackBufferHeight = 1080;
    public const bool PreferMultiSampling = true;
    public const bool IsMouseVisible = true;
    public const GraphicsProfile GraphicsProfile = Microsoft.Xna.Framework.Graphics.GraphicsProfile.HiDef;
    public const string ContentRootDirectory = "Content";
    public const string ModelPath = "Models/cube";
    public const string FontPath = "Font";
}