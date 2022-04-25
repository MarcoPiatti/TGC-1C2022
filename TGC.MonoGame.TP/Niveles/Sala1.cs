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
        private CylinderPrimitive Columna { get; set; }
        private Matrix ColumnaWorld { get; set; }

        private CubePrimitive Escalon { get; set; }
        private List<Matrix> EscalonesWorld { get; set; }
        private Matrix EscalonScale = Matrix.CreateScale(15f, 1f, 20f);

        public Sala1(ContentManager content, GraphicsDevice graphicsDevice, Vector3 posicion) : base(content, graphicsDevice, posicion)
        {
            Columna = new CylinderPrimitive(graphicsDevice, 1f, 1f, 8);
            ColumnaWorld = Matrix.CreateScale(50f, 50f, 50f) * Matrix.CreateTranslation(new Vector3(0, 10f, 0) + posicion);

            Escalon = new CubePrimitive(graphicsDevice);
            EscalonesWorld = new List<Matrix>()

            float angulo = 0f;
            for(int i = 0; i < 8; i++){
                Matrix esc = new Matrix();
                esc = EscalonScale * Matrix.CreateTranslation(new Vector3(0, 0, 25f)) 
                            * Matrix.CreateRotationY(MathHelper.ToRadians(angulo)) * Matrix.CreateTranslation(position);
                EscalonesWorld.add(esc);
                angulo += 45f;
            }
        }

        public override void Draw(GameTime gameTime, Matrix view, Matrix projection)
        {
            base.Draw(gameTime,view,projection);
            Columna.Draw(ColumnaWorld, view, projection);
            foreach(Matrix esc in EscalonesWorld){
                Escalon.Draw(esc, view, projection);
            }
        }
    }
}
