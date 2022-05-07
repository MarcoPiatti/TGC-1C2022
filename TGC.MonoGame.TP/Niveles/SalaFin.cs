using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TGC.MonoGame.TP.Elements;
using TGC.MonoGame.TP.Geometries;

namespace TGC.MonoGame.TP.Niveles
{
    public class SalaFin : Sala
    {
        public SalaFin(ContentManager content, GraphicsDevice graphicsDevice, Vector3 posicion) : base(content, graphicsDevice, posicion)
        {


            ParedNorteIzq.World = Matrix.CreateScale(1f, Size, Size * 0.5f) * Matrix.CreateTranslation(new Vector3(50, Size / 2, Size * 0.25f) + Posicion);
            ParedNorteDer.World = Matrix.CreateScale(1f, Size, Size * 0.5f) * Matrix.CreateTranslation(new Vector3(50, Size / 2, -Size * 0.25f) + Posicion);

        }

        public override void Draw(GameTime gameTime, Matrix view, Matrix projection)
        {
            base.Draw(gameTime, view, projection);
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
