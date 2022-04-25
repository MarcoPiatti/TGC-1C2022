using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TGC.MonoGame.TP.Geometries;
using TGC.MonoGame.TP.Elements;

namespace TGC.MonoGame.TP.Niveles
{
    public class Sala1 : Sala
    {
        private CylinderPrimitive Columna { get; set; }
        private Matrix ColumnaWorld { get; set; }

        private CubePrimitive Escalon { get; set; }
        private List<Matrix> EscalonesWorld { get; set; }
        private Matrix EscalonScale = Matrix.CreateScale(10f, 1f, 15f);
        private float velocidadAngularEscalones = 0.5f;

        private Coin Coin { get; set; }

        public Sala1(ContentManager content, GraphicsDevice graphicsDevice, Vector3 posicion) : base(content, graphicsDevice, posicion)
        {
            PisoWorld = Matrix.CreateScale(new Vector3(10f, 1f, 10f)) * Matrix.CreateTranslation(new Vector3(-45f, 0, 0) + posicion);

            Columna = new CylinderPrimitive(graphicsDevice, 1f, 1f, 8);
            ColumnaWorld = Matrix.CreateScale(40f, 80f, 40f) * Matrix.CreateTranslation(new Vector3(0, 10f, 0) + posicion);

            Escalon = new CubePrimitive(graphicsDevice);
            EscalonesWorld = new List<Matrix>();

            float angulo = -90f;
            float altura = 5f;
            for(int i = 0; i < 9; i++){
                Matrix esc = new Matrix();
                esc = EscalonScale * Matrix.CreateTranslation(new Vector3(0, altura, 25f)) 
                            * Matrix.CreateRotationY(MathHelper.ToRadians(angulo)) * Matrix.CreateTranslation(posicion);
                EscalonesWorld.Add(esc);
                angulo += 45f;
                altura += 5f;
            }

            Coin = new Coin(graphicsDevice, new Vector3(0, 60f, 0) + posicion);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Coin.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, Matrix view, Matrix projection)
        {
            base.Draw(gameTime,view,projection);
            Columna.Draw(ColumnaWorld, view, projection);
            foreach(Matrix esc in EscalonesWorld){
                Escalon.Draw(esc, view, projection);
            }
            Coin.Draw(view, projection);
        }
    }
}
