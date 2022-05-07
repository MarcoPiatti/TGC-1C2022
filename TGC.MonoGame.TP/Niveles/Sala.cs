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

    public class Sala
    {
        public const string ContentFolderEffects = "Effects/";

        private Effect Effect { get; set; }

        public Cube Piso { get; set; }
        public Cube ParedOeste { get; set; }
        public Cube ParedEste { get; set; }
        public Cube ParedNorteIzq { get; set; }
        public Cube ParedNorteDer { get; set; }
        public Cube Techo { get; set; }

        public Vector3 Posicion;
        public static float Size = 100f;

        public Sala(ContentManager content, GraphicsDevice graphicsDevice, Vector3 posicion)
        {
            Posicion = posicion;

            Effect = content.Load<Effect>(ContentFolderEffects + "BasicShader");

            Piso = new Cube(graphicsDevice, content, posicion);
            Piso.World = Matrix.CreateScale(Size, 1f, Size) * Matrix.CreateTranslation(new Vector3(0, 0, 0) + Posicion);
            
            ParedOeste = new Cube(graphicsDevice, content, posicion);
            ParedOeste.World = Matrix.CreateScale(Size, Size, 1f) * Matrix.CreateTranslation(new Vector3(0, Size / 2, Size/2) + Posicion);

            ParedEste = new Cube(graphicsDevice, content, posicion);
            ParedEste.World = Matrix.CreateScale(Size, Size, 1f) * Matrix.CreateTranslation(new Vector3(0, Size / 2, -Size / 2) + Posicion);
            
            ParedNorteIzq = new Cube(graphicsDevice, content, posicion);
            ParedNorteIzq.World = Matrix.CreateScale(1f, Size, Size*0.45f) * Matrix.CreateTranslation(new Vector3(50, Size / 2, Size * 0.275f) + Posicion);

            ParedNorteDer = new Cube(graphicsDevice, content, posicion);
            ParedNorteDer.World = Matrix.CreateScale(1f, Size, Size * 0.45f) * Matrix.CreateTranslation(new Vector3(50, Size / 2, -Size * 0.275f) + Posicion);

            Techo = new Cube(graphicsDevice, content, posicion);
            Techo.World = Matrix.CreateScale(Size, 1f, Size) * Matrix.CreateTranslation(new Vector3(0, Size, 0) + Posicion);
        }

        public virtual void Draw(GameTime gameTime, Matrix view, Matrix projection)
        {

            // Set the View and Projection matrices, needed to draw every 3D model
            Effect.Parameters["View"].SetValue(view);
            Effect.Parameters["Projection"].SetValue(projection);

            Piso.Draw(view, projection);
            ParedOeste.Draw(view, projection);
            ParedEste.Draw(view, projection);
            ParedNorteIzq.Draw(view, projection);
            ParedNorteDer.Draw(view, projection);
            Techo.Draw(view, projection);
        }

        public virtual void Update(GameTime gameTime) { }
    }
}
