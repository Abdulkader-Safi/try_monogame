using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1;

public class ModelRenderer
{
    private Model _model;
    private GraphicsDevice _graphicsDevice;

    public ModelRenderer(GraphicsDevice graphicsDevice)
    {
        _graphicsDevice = graphicsDevice;
    }

    public void LoadModel(Model model)
    {
        _model = model;

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

    public void Initialize()
    {
        _graphicsDevice.DepthStencilState = DepthStencilState.Default;
        _graphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
        _graphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
    }

    public void Draw(Matrix world, Matrix view, Matrix projection)
    {
        if (_model == null) return;

        foreach (var mesh in _model.Meshes)
        {
            foreach (var effect in mesh.Effects)
            {
                if (effect is BasicEffect basicEffect)
                {
                    basicEffect.EnableDefaultLighting();
                    basicEffect.PreferPerPixelLighting = true;
                    basicEffect.World = mesh.ParentBone.Transform * world;
                    basicEffect.View = view;
                    basicEffect.Projection = projection;
                }
            }
            mesh.Draw();
        }
    }
}