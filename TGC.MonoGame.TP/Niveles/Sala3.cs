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
    public class Sala3 : Sala
    {
        private List<MovingCube> obstacles { get; set; }
        private static Vector3 obstacleScale = new Vector3(1f, 0.1f * Size, 0.1f * Size);
        private static float angle = (float)Math.PI / 12f;

        public Sala3(ContentManager content, GraphicsDevice graphicsDevice, Vector3 posicion) : base(content, graphicsDevice, posicion)
        {
            
            PisoWorld = Matrix.CreateScale(Size / (float)Math.Cos(angle), 1f, Size) * Matrix.CreateRotationZ(angle) * Matrix.CreateTranslation(new Vector3(0, (float)Math.Sin(angle)*Size/2, 0) + Posicion);

            obstacles = new List<MovingCube>();

            obstacles.Add(new MovingCube(new List<Vector3>() { new Vector3(-Size * 0.4f, (float)Math.Tan(angle) * Size * 0.1f, -0.05f * Size), new Vector3(Size * 0.4f, (float)Math.Tan(angle) * Size, -0.05f * Size) }, graphicsDevice, Color.Red));
            obstacles.Add(new MovingCube(new List<Vector3>() { new Vector3(Size * 0.4f, (float)Math.Tan(angle)*Size, -0.15f*Size), new Vector3(-Size * 0.4f,(float)Math.Tan(angle)*Size*0.1f,-0.15f * Size)}, graphicsDevice,Color.Red));
            obstacles.Add(new MovingCube(new List<Vector3>() { new Vector3(-Size * 0.4f, (float)Math.Tan(angle) * Size * 0.1f, -0.25f * Size), new Vector3(Size * 0.4f, (float)Math.Tan(angle) * Size, -0.25f * Size)}, graphicsDevice, Color.Red));
            obstacles.Add(new MovingCube(new List<Vector3>() { new Vector3(Size * 0.4f, (float)Math.Tan(angle) * Size, -0.35f * Size), new Vector3(-Size * 0.4f, (float)Math.Tan(angle) * Size * 0.1f, -0.35f * Size)}, graphicsDevice, Color.Red));
            obstacles.Add(new MovingCube(new List<Vector3>() { new Vector3(-Size * 0.4f, (float)Math.Tan(angle) * Size * 0.1f, -0.45f * Size), new Vector3(Size * 0.4f, (float)Math.Tan(angle) * Size, -0.45f * Size)}, graphicsDevice, Color.Red));
            
            obstacles.Add(new MovingCube(new List<Vector3>() { new Vector3(Size * 0.4f, (float)Math.Tan(angle) * Size, 0.05f * Size), new Vector3(-Size * 0.4f, (float)Math.Tan(angle) * Size * 0.1f, 0.05f * Size)}, graphicsDevice, Color.Red));
            obstacles.Add(new MovingCube(new List<Vector3>() { new Vector3(-Size * 0.4f, (float)Math.Tan(angle) * Size * 0.1f, 0.15f * Size), new Vector3(Size * 0.4f, (float)Math.Tan(angle) * Size, 0.15f * Size)}, graphicsDevice, Color.Red));
            obstacles.Add(new MovingCube(new List<Vector3>() { new Vector3(Size * 0.4f, (float)Math.Tan(angle) * Size, 0.25f * Size), new Vector3(-Size * 0.4f, (float)Math.Tan(angle) * Size * 0.1f, 0.25f * Size)}, graphicsDevice, Color.Red));
            obstacles.Add(new MovingCube(new List<Vector3>() { new Vector3(-Size * 0.4f, (float)Math.Tan(angle) * Size * 0.1f, 0.35f * Size), new Vector3(Size * 0.4f, (float)Math.Tan(angle) * Size, 0.35f * Size)}, graphicsDevice, Color.Red));
            obstacles.Add(new MovingCube(new List<Vector3>() { new Vector3(Size * 0.4f, (float)Math.Tan(angle) * Size, 0.45f * Size), new Vector3(-Size * 0.4f, (float)Math.Tan(angle) * Size * 0.1f, 0.45f * Size)}, graphicsDevice, Color.Red));



            foreach (MovingCube cube in obstacles)
            {
                cube.World = Matrix.CreateScale(obstacleScale) * Matrix.CreateTranslation(cube.Position + posicion);
            }

        }

        public override void Draw(GameTime gameTime, Matrix view, Matrix projection)
        {
            base.Draw(gameTime, view, projection);
            foreach (MovingCube cube in obstacles)
            {
                cube.Draw(view, projection);
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (MovingCube cube in obstacles)
            {
                cube.Move(gameTime);
                cube.World = Matrix.CreateScale(obstacleScale) * Matrix.CreateTranslation(cube.Position + Posicion);
            }
        }
    }
}
