using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using TGC.MonoGame.Samples.Collisions;
using TGC.MonoGame.TP;
using TGC.MonoGame.TP.Elements;
using TGC.MonoGame.TP.Geometries;

namespace TGC.MonoGame.TP.Elements
{
    public class Coin : AlmostSphere
    {
        private float CoinAngle { get; set; }
        private bool flagCollide {get; set;} = false;
        private ContentManager localContent {get; set;}
        private GraphicsDevice localGraphics { get; set; }

        public Coin(GraphicsDevice graphicsDevice, ContentManager content, Vector3 posicion): base(graphicsDevice,content, 1f, 16, Color.Gold)
        {
            localContent = content;
            localGraphics = graphicsDevice;
            Collider = new OrientedBoundingBox(posicion,new Vector3(5,30,5));
            Position = posicion;
            World = Matrix.CreateScale(1f, 5f, 5f) * Matrix.CreateTranslation(posicion);
        }
        public override void logicalAction(Sphere player)
        {
            flagCollide = true;
        }
        public void Update(GameTime gameTime)
        {      
            if(flagCollide == true) {
                CoinAngle = 10f;
            } else
            {
                var elapsedTime = Convert.ToSingle(gameTime.ElapsedGameTime.TotalSeconds);
                CoinAngle += 1.5f * elapsedTime;
                Matrix rotation = Matrix.CreateRotationY(CoinAngle);
                World = Matrix.CreateScale(1f, 10f, 10f) * rotation * Matrix.CreateTranslation(Position);
            }

        }

    }
}
