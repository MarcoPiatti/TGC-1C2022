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

        private Effect Effect { get; set; }

        private CubePrimitive Piso { get; set; }
        private CubePrimitive ParedOeste { get; set; }
        private CubePrimitive ParedEste { get; set; }
        private CubePrimitive ParedNorteIzq { get; set; }
        private CubePrimitive ParedNorteDer { get; set; }
        private CubePrimitive Techo { get; set; }
        public Matrix PisoWorld { get; set; }
        public Matrix ParedOesteWorld { get; set; }
        public Matrix ParedEsteWorld { get; set; }
        public Matrix ParedNorteIzqWorld { get; set; }
        public Matrix ParedNorteDerWorld { get; set; }
        public Matrix TechoWorld { get; set; }

        public Vector3 Posicion;
        public static float Size = 100f;

        public Sala(ContentManager content, GraphicsDevice graphicsDevice, Vector3 posicion)
        {
            Posicion = posicion;

            Effect = content.Load<Effect>(ContentFolderEffects + "BasicShader");

            Piso = new CubePrimitive(graphicsDevice);
            PisoWorld = Matrix.CreateScale(Size, 1f, Size) * Matrix.CreateTranslation(new Vector3(0, 0, 0) + Posicion);
            
            ParedOeste = new CubePrimitive(graphicsDevice);
            ParedOesteWorld = Matrix.CreateScale(Size, Size, 1f) * Matrix.CreateTranslation(new Vector3(0, Size / 2, Size/2) + Posicion);

            ParedEste = new CubePrimitive(graphicsDevice);
            ParedEsteWorld = Matrix.CreateScale(Size, Size, 1f) * Matrix.CreateTranslation(new Vector3(0, Size / 2, -Size / 2) + Posicion);
            
            ParedNorteIzq = new CubePrimitive(graphicsDevice);
            ParedNorteIzqWorld = Matrix.CreateScale(1f, Size, Size*0.45f) * Matrix.CreateTranslation(new Vector3(50, Size / 2, Size * 0.275f) + Posicion);

            ParedNorteDer = new CubePrimitive(graphicsDevice);
            ParedNorteDerWorld = Matrix.CreateScale(1f, Size, Size * 0.45f) * Matrix.CreateTranslation(new Vector3(50, Size / 2, -Size * 0.275f) + Posicion);

            Techo = new CubePrimitive(graphicsDevice);
            TechoWorld = Matrix.CreateScale(Size, 1f, Size) * Matrix.CreateTranslation(new Vector3(0, Size, 0) + Posicion);
        }

        public virtual void Draw(GameTime gameTime, Matrix view, Matrix projection)
        {

            // Set the View and Projection matrices, needed to draw every 3D model
            Effect.Parameters["View"].SetValue(view);
            Effect.Parameters["Projection"].SetValue(projection);

            Piso.Draw(PisoWorld, view, projection);
            ParedOeste.Draw(ParedOesteWorld, view, projection);
            ParedEste.Draw(ParedEsteWorld, view, projection);
            ParedNorteIzq.Draw(ParedNorteIzqWorld, view, projection);
            ParedNorteDer.Draw(ParedNorteDerWorld, view, projection);
            Techo.Draw(TechoWorld, view, projection);
        }

        public virtual void Update(GameTime gameTime) { }
    }
}
