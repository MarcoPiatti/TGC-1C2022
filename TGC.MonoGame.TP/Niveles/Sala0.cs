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

        public Sala0(ContentManager content, GraphicsDevice graphicsDevice, Vector3 posicion) : base(content,graphicsDevice,posicion)
        {
            ParedSur = new CubePrimitive(graphicsDevice);
            ParedSurWorld = Matrix.CreateScale(1f, Size, Size) * Matrix.CreateTranslation(new Vector3(-Size/2, 0, 0) + posicion);
        }

        public override void Draw(GameTime gameTime, Matrix view, Matrix projection)
        {
            base.Draw(gameTime, view, projection);
            ParedSur.Draw(ParedSurWorld, view, projection);
        }
    }
}
