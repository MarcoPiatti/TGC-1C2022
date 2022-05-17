using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TGC.MonoGame.TP.Geometries;

namespace TGC.MonoGame.TP.Elements
{
    public class KillerCube : LogicalCube
    {
        public KillerCube(GraphicsDevice graphicsDevice, ContentManager content, Vector3 posicion) : base(graphicsDevice, content, posicion, Color.Red)
        {
        }

        public override void logicalAction(Player player)
        {
            player.returnToCheckPoint();

        }

        public override void Draw(Matrix view, Matrix projection) 
        {
            Body.Draw(World, view, projection);
        }

    }
}
