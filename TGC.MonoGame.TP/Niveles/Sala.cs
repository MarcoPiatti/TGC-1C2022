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

    public class Sala
    {
        public const string ContentFolderEffects = "Effects/";

        private CubePrimitive Piso { get; set; }
        private CubePrimitive ParedOeste { get; set; }
        private CubePrimitive ParedEste { get; set; }
        private CubePrimitive ParedNorteIzq { get; set; }
        private CubePrimitive ParedNorteDer { get; set; }
        private Matrix PisoWorld { get; set; }
        private Matrix ParedOesteWorld { get; set; }
        private Matrix ParedEsteWorld { get; set; }
        private Matrix ParedNorteIzq { get; set; }
        private Matrix ParedNorteDerWorld { get; set; }

        public Sala(GraphicsDevice graphicsDevice)
        {
            Piso = new CubePrimitive(graphicsDevice);
            PisoWorld = Matrix.CreateScale(100f, 1f, 100f) * Matrix.CreateTranslation(new Vector3(100, 0, 100));
            
            ParedOeste = new CubePrimitive(graphicsDevice);
            ParedOesteWorld = Matrix.CreateScale(1f, 100f, 50f) * Matrix.CreateTranslation(new Vector3(50, 0, 100));

            ParedEste = new CubePrimitive(graphicsDevice);
            ParedEsteWorld = Matrix.CreateScale(1f, 100f, 50f) * Matrix.CreateTranslation(new Vector3(50, 0, -100));
            
            ParedNorteIzq = new CubePrimitive(graphicsDevice);
            ParedNorteIzqWorld = Matrix.CreateScale(20f, 100f, 1f) * Matrix.CreateTranslation(new Vector3(100, 0, 20));

            ParedNorteDer = new CubePrimitive(graphicsDevice);
            ParedNorteDerWorld = Matrix.CreateScale(20f, 100f, 1f) * Matrix.CreateTranslation(new Vector3(100, 0, -20));
        }

        public void Draw(GameTime gameTime, Matrix view, Matrix projection)
        {

            // Set the View and Projection matrices, needed to draw every 3D model
            Effect.Parameters["View"].SetValue(view);
            Effect.Parameters["Projection"].SetValue(projection);

            Piso.Draw(world, view, projection);
            ParedOeste.Draw(world, view, projection);
            ParedEste.Draw(world, view, projection);
            ParedNorteIzq.Draw(world, view, projection);
            ParedNorteDer.Draw(world, view, projection);

        }
    }
}
