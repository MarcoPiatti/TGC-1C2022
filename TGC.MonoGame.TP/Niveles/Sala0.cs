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
    public class Sala0 : Sala
    {
        private CubePrimitive ParedSur { get; set; }
        private Matrix ParedSurWorld { get; set; }

        public Sala(GraphicsDevice graphicsDevice) : base(graphicsDevice)
        {
            ParedSur = new CubePrimitive(graphicsDevice);
            ParedSurWorld = Matrix.CreateScale(50f, 100f, 1f) * Matrix.CreateTranslation(new Vector3(-100, 0, 50));
        }

        public void Draw(GameTime gameTime, Matrix view, Matrix projection)
        {
            base.Draw(gameTime,view,projection);
        }
    }
}
