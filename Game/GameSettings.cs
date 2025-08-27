using Microsoft.Xna.Framework.Graphics;

namespace Game1;

public static class GameSettings
{
    public const string title = "Game";
    public const int preferredBackBufferWidth = 1920;
    public const int preferredBackBufferHeight = 1080;
    public const bool preferMultiSampling = true;
    public const bool isMouseVisible = true;
    public const GraphicsProfile graphicsProfile = GraphicsProfile.HiDef;
    public const string contentRootDirectory = "Content";
    public const string modelPath = "Models/cube";
    public const string fontPath = "Font";
}