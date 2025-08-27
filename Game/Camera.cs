using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1;

public class Camera
{
    private Matrix _view;
    private Matrix _projection;
    private float _angle;
    private float _pitch;

    public Matrix View => _view;
    public Matrix Projection => _projection;
    public float Angle => _angle;
    public float Pitch => _pitch;

    public void Initialize(GraphicsDevice graphicsDevice)
    {
        var cameraPos = new Vector3(0, 2, 6);
        var target = Vector3.Zero;
        var up = Vector3.Up;

        _view = Matrix.CreateLookAt(cameraPos, target, up);
        _projection = Matrix.CreatePerspectiveFieldOfView(
            MathHelper.ToRadians(45f),
            graphicsDevice.Viewport.AspectRatio,
            0.1f,
            100f
        );
    }

    public void UpdateRotation(float deltaAngle, float deltaPitch)
    {
        _angle += deltaAngle;
        _pitch += deltaPitch;
    }

    public Matrix CreateWorldMatrix(float scale = 0.01f)
    {
        return Matrix.CreateScale(scale) *
               Matrix.CreateRotationX(_pitch) *
               Matrix.CreateRotationY(_angle) *
               Matrix.CreateTranslation(0, 0, 0);
    }
}