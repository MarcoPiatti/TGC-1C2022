using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TGC.MonoGame.TP.Geometries;

namespace TGC.MonoGame.TP.Elements
{
    public class SpinningPillar
    {
        private Cylinder Columna { get; set; }

        private List<Escalon> Escalones { get; set; }
        private float velocidadAngular = -90f;

        public SpinningPillar(GraphicsDevice graphicsDevice, ContentManager content, Vector3 posicion){
            Columna = new Cylinder(graphicsDevice,content, Color.White, 1f, 1f, 32);
            Columna.World = Matrix.CreateScale(40f, 80f, 40f) * Matrix.CreateTranslation(new Vector3(0, 10f, 0) + posicion);

            Escalones = new List<Escalon>();

            float angulo = 0f;
            float altura = 5f;
            for(int i = 0; i < 9; i++){
                Escalones.Add(new Escalon(graphicsDevice, content, new Vector3(0, altura, 0f)+posicion, angulo));
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
            Columna.Draw(view, projection);
            foreach(Escalon e in Escalones){
                e.Draw(view, projection);
            }
        }
    }

    internal class Escalon{
        private Matrix ColumnCenterTranslation { get; set; }
        private Cube Cuerpo { get; set; }
        private Matrix EscalonWorld { get; set; }
        private Matrix EscalonScale = Matrix.CreateScale(10f, 1f, 15f);
        private Matrix DisplacementFromColumnCenter = Matrix.CreateTranslation(0f, 0f, 25f);
        private Matrix OrbitAroundColumn { get; set; }
        private float degreesAroundColumn { get; set; }

        internal Escalon(GraphicsDevice graphicsDevice, ContentManager content, Vector3 columnCenterAndHeight, float rotationAroundColumn){
            Cuerpo = new Cube(graphicsDevice, content, new Vector3(0,0,0));
            degreesAroundColumn = rotationAroundColumn;
            ColumnCenterTranslation = Matrix.CreateTranslation(columnCenterAndHeight);

            OrbitAroundColumn = DisplacementFromColumnCenter
                                        * Matrix.CreateRotationY(MathHelper.ToRadians(rotationAroundColumn));
            Cuerpo.World = EscalonScale * OrbitAroundColumn
                                        * ColumnCenterTranslation;
        }

        internal void Update(float angleAddition){
            degreesAroundColumn += angleAddition;
            OrbitAroundColumn = DisplacementFromColumnCenter
                                        * Matrix.CreateRotationY(MathHelper.ToRadians(degreesAroundColumn));
            Cuerpo.World = EscalonScale * OrbitAroundColumn
                                        * ColumnCenterTranslation;
        }

        internal void Draw(Matrix view, Matrix projection){
            Cuerpo.Draw( view, projection);
        }
    }
}