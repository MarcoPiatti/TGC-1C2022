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
    public class Sala1 : Sala
    {
        private CubePrimitive UnObstaculo { get; set; }
        private Matrix UnObstaculoWorld { get; set; }

        public Sala1(GraphicsDevice graphicsDevice) : base(graphicsDevice)
        {
            UnObstaculo = new CylinderPrimitive(graphicsDevice);
            UnObstaculoWorld = Matrix.CreateScale(10f, 1f, 10f) * Matrix.CreateTranslation(new Vector3(50, 0, 50));
        }

        public void Draw(GameTime gameTime, Matrix view, Matrix projection)
        {
            base.Draw(gameTime,view,projection);
            UnObstaculo.Draw(UnObstaculoWorld, view, projection);
        }
    }
}
