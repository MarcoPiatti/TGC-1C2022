using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TGC.MonoGame.TP.Elements
{
    public class SpinningPillar : Object
    {
        private Cylinder Columna { get; set; }

        private List<Escalon> Escalones { get; set; }
        private float velocidadAngular = -90f;

        public SpinningPillar(GraphicsDevice graphicsDevice, ContentManager content, Vector3 posicion){
            Columna = new Cylinder(graphicsDevice,content, Color.White, 1f, 1f, 32);
            Columna.WorldUpdate(new Vector3(40f, 80f, 40f), new Vector3(0f,10f,0f)+posicion, Matrix.Identity);

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

        public override void Draw(Matrix view, Matrix projection){
            Columna.Draw(view, projection);
            foreach(Escalon e in Escalones){
                e.Draw(view, projection);
            }
        }

        public override bool Intersects(Sphere s)
        {
            return Columna.Intersects(s) || Escalones.Exists(e => e.Intersects(s));
        }

        public override Vector3 GetDirectionFromCollision(Sphere s)
        {
            if(Columna.Intersects(s)){
                return Columna.GetDirectionFromCollision(s);
            }
            return Escalones.Find(e => e.Intersects(s)).GetDirectionFromCollision(s);
        }
    }

    internal class Escalon{
        private Vector3 ColumnCenter { get; set; }
        private Matrix ColumnCenterTranslation { get; set; }
        private Cube Cuerpo { get; set; }
        private Matrix EscalonWorld { get; set; }
        private Vector3 EscalonScaleVector = new Vector3(10f, 1f, 15f);
        private Matrix EscalonScale;
        private Vector3 PosicionReal;
        private Matrix DisplacementFromColumnCenter = Matrix.CreateTranslation(0f, 0f, 25f);
        private Matrix OrbitAroundColumn { get; set; }
        private float degreesAroundColumn { get; set; }

        private Matrix rotationPure;

        internal Escalon(GraphicsDevice graphicsDevice, ContentManager content, Vector3 columnCenterAndHeight, float rotationAroundColumn){
            ColumnCenter = columnCenterAndHeight;
            Cuerpo = new Cube(graphicsDevice, content, new Vector3(0,0,0));
            degreesAroundColumn = rotationAroundColumn;
            EscalonScale = Matrix.CreateScale(EscalonScaleVector);
            ColumnCenterTranslation = Matrix.CreateTranslation(columnCenterAndHeight);

            rotationPure = Matrix.CreateRotationY(MathHelper.ToRadians(rotationAroundColumn));
            OrbitAroundColumn = DisplacementFromColumnCenter
                                        * rotationPure;
            PosicionReal = Vector3.Transform(Vector3.Transform(Vector3.Zero, OrbitAroundColumn), ColumnCenterTranslation);
            Cuerpo.WorldUpdate(EscalonScaleVector, PosicionReal, rotationPure);
        }

        internal void Update(float angleAddition){
            degreesAroundColumn += angleAddition;
            rotationPure = Matrix.CreateRotationY(MathHelper.ToRadians(degreesAroundColumn));
            OrbitAroundColumn = DisplacementFromColumnCenter
                                        * rotationPure;
            PosicionReal = Vector3.Transform(Vector3.Transform(Vector3.Zero, OrbitAroundColumn), ColumnCenterTranslation);
            Cuerpo.WorldUpdate(EscalonScaleVector, PosicionReal, rotationPure);
        }

        internal void Draw(Matrix view, Matrix projection){
            Cuerpo.Draw( view, projection);
        }

        internal bool Intersects(Sphere s)
        {
            return Cuerpo.Intersects(s);
        }

        internal Vector3 GetDirectionFromCollision(Sphere s)
        {
            return Cuerpo.GetDirectionFromCollision(s);
        }
    }
}