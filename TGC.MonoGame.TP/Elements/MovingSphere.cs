using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TGC.MonoGame.TP.Geometries;

namespace TGC.MonoGame.TP.Elements
{
    public class MovingSphere : MovingObject
    {
        public SpherePrimitive Sphere { get; set; }
        public MovingSphere(Vector3 StartPosition, Vector3 EndPosition, GraphicsDevice graphicsDevice, float speed = 0.8f) : base(StartPosition, EndPosition, graphicsDevice, speed)
        {
            Sphere = new SpherePrimitive(graphicsDevice, 1, 16, Color.Red);
        }
    }
}
