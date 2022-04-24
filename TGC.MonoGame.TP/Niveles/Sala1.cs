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
        private CylinderPrimitive UnObstaculo { get; set; }
        private Matrix UnObstaculoWorld { get; set; }

        public Sala1(ContentManager content, GraphicsDevice graphicsDevice, Vector3 posicion) : base(content, graphicsDevice, posicion)
        {
            UnObstaculo = new CylinderPrimitive(graphicsDevice);
            UnObstaculoWorld = Matrix.CreateScale(50f, 50f, 50f) * Matrix.CreateTranslation(new Vector3(0, 10f, 0) + posicion);
        }

        public override void Draw(GameTime gameTime, Matrix view, Matrix projection)
        {
            base.Draw(gameTime,view,projection);
            UnObstaculo.Draw(UnObstaculoWorld, view, projection);
        }
    }
}
