using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TGC.MonoGame.TP.Geometries;

namespace TGC.MonoGame.TP.Elements
{
    public class SpinningPillar
    {
        private CylinderPrimitive Columna { get; set; }
        private Matrix ColumnaWorld { get; set; }

        private CubePrimitive Escalon { get; set; }
        private List<Escalon> Escalones { get; set; }
        private float velocidadAngular = -90f;

        public SpinningPillar(GraphicsDevice graphicsDevice, Vector3 posicion){
            Columna = new CylinderPrimitive(graphicsDevice, 1f, 1f, 8);
            ColumnaWorld = Matrix.CreateScale(40f, 80f, 40f) * Matrix.CreateTranslation(new Vector3(0, 10f, 0) + posicion);

            Escalones = new List<Escalon>();

            float angulo = 0f;
            float altura = 5f;
            for(int i = 0; i < 9; i++){
                Escalones.Add(new Escalon(graphicsDevice, new Vector3(0, altura, 0f)+posicion, angulo));
                angulo += 45f;
                altura += 5f;
            }
        }

        public void Update(GameTime gameTime)
        {
            var deltaTime = Convert.ToSingle(gameTime.ElapsedGameTime.TotalSeconds);
            var deltaAngle = deltaTime * velocidadAngular;
            foreach(Escalon e in Escalones){
                e.Update(deltaAngle);
            }
        }

        public void Draw(Matrix view, Matrix projection){
            Columna.Draw(ColumnaWorld, view, projection);
            foreach(Escalon e in Escalones){
                e.Draw(view, projection);
            }
        }
    }

    internal class Escalon{
        private Matrix ColumnCenterTranslation { get; set; }
        private CubePrimitive Cuerpo { get; set; }
        private Matrix EscalonWorld { get; set; }
        private Matrix EscalonScale = Matrix.CreateScale(10f, 1f, 15f);
        private Matrix DisplacementFromColumnCenter = Matrix.CreateTranslation(0f, 0f, 25f);
        private Matrix OrbitAroundColumn { get; set; }
        private float degreesAroundColumn { get; set; }

        internal Escalon(GraphicsDevice graphicsDevice, Vector3 columnCenterAndHeight, float rotationAroundColumn){
            Cuerpo = new CubePrimitive(graphicsDevice);
            degreesAroundColumn = rotationAroundColumn;
            ColumnCenterTranslation = Matrix.CreateTranslation(columnCenterAndHeight);

            OrbitAroundColumn = DisplacementFromColumnCenter
                                        * Matrix.CreateRotationY(MathHelper.ToRadians(rotationAroundColumn));
            EscalonWorld = EscalonScale * OrbitAroundColumn
                                        * ColumnCenterTranslation;
        }

        internal void Update(float angleAddition){
            degreesAroundColumn += angleAddition;
            OrbitAroundColumn = DisplacementFromColumnCenter
                                        * Matrix.CreateRotationY(MathHelper.ToRadians(degreesAroundColumn));
            EscalonWorld = EscalonScale * OrbitAroundColumn
                                        * ColumnCenterTranslation;
        }

        internal void Draw(Matrix view, Matrix projection){
            Cuerpo.Draw(EscalonWorld, view, projection);
        }
    }
}