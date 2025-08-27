using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Game1;

public class InputHandler
{
    private KeyboardState _previousKeyboardState;
    private float _rotationSpeed = 3.5f;

    public bool ShouldExit { get; private set; }
    public bool ShouldToggleFullscreen { get; private set; }
    public float AngleDelta { get; private set; }
    public float PitchDelta { get; private set; }

    public void Update(GameTime gameTime)
    {
        var keyboardState = Keyboard.GetState();

        ShouldExit = keyboardState.IsKeyDown(Keys.Escape);

        ShouldToggleFullscreen = keyboardState.IsKeyDown(Keys.F) &&
                                !_previousKeyboardState.IsKeyDown(Keys.F);

        var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        AngleDelta = 0f;
        PitchDelta = 0f;

        if (keyboardState.IsKeyDown(Keys.A))
            AngleDelta -= _rotationSpeed * deltaTime;
        if (keyboardState.IsKeyDown(Keys.D))
            AngleDelta += _rotationSpeed * deltaTime;
        if (keyboardState.IsKeyDown(Keys.W))
            PitchDelta -= _rotationSpeed * deltaTime;
        if (keyboardState.IsKeyDown(Keys.S))
            PitchDelta += _rotationSpeed * deltaTime;

        _previousKeyboardState = keyboardState;
    }
}