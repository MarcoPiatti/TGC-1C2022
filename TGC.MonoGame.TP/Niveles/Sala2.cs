using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TGC.MonoGame.TP.Geometries;

namespace TGC.MonoGame.TP.Niveles
{
    
    public class Sala2 : Sala
    {
        public Sala2(ContentManager content, GraphicsDevice graphicsDevice, Vector3 posicion) : base(content, graphicsDevice,posicion)
        {
            PisoWorld = Matrix.CreateScale(1f/50f, 1f, 1f/50f) * Matrix.CreateTranslation(new Vector3(0, 0, 0) + posicion);
        }

        public override void Draw(GameTime gameTime, Matrix view, Matrix projection)
        {
            base.Draw(gameTime,view,projection);
        }
    }
}
