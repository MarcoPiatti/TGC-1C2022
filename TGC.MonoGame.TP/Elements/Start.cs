using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TGC.MonoGame.TP.Geometries;

namespace TGC.MonoGame.TP.Elements
{
    public class StartEnd : Cylinder
    {

        public Effect effect;
        public Color color;

        public StartEnd(GraphicsDevice graphicsDevice, ContentManager content, Color color) 
        : base(graphicsDevice, content, color)
        {
            effect = content.Load<Effect>("Effects/BasicShader");
            Body.Effect = effect;
            this.color = color;
        }

        public void Draw(Matrix view, Matrix projection, float time)
        {
            effect.Parameters["DiffuseColor"].SetValue(color.ToVector3());
            effect.Parameters["Alpha"]?.SetValue(1f);
            effect.Parameters["Time"]?.SetValue(time);
            Body.Draw(World, view, projection, effect);
        }
    }
}