using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TGC.MonoGame.TP.Geometries;

namespace TGC.MonoGame.TP.Elements
{
    public class CheckPointWall : LogicalCube
    {

        public Effect effect;

        public CheckPointWall(GraphicsDevice graphicsDevice, ContentManager content, Vector3 Position) 
        : base(graphicsDevice, content, Position, Color.Red)
        {
            effect = content.Load<Effect>("Effects/BasicShader");
            Body.Effect = effect;
        }

        public override void logicalAction(Player player)
        {
            player.checkpoint = new Vector3(this.Position.X + 5f, 10f, 0);
            base.logicalAction(player);
        }

        public override void Draw(Matrix view, Matrix projection)
        {
            effect.Parameters["DiffuseColor"].SetValue(Color.Red.ToVector3());
            effect.Parameters["Alpha"].SetValue(1f);
            Body.Draw(World, view, projection, this.effect);
        }
    }
}