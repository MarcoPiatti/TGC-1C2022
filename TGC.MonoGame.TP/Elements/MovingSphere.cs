using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TGC.MonoGame.TP.Geometries;

namespace TGC.MonoGame.TP.Elements
{
    public class MovingSphere : MovingObject
    {
        public SpherePrimitive Sphere { get; set; }
        public MovingSphere(List<Vector3> Points, GraphicsDevice graphicsDevice, Color color, int movementType = 1, float speed = 10f) : base(Points, graphicsDevice, color, movementType, speed)
        {
            Sphere = new SpherePrimitive(graphicsDevice, 1, 16, color);
        }

        public MovingSphere(List<Vector3> Points, GraphicsDevice graphicsDevice, int movementType = 1, float speed = 10f) : base(Points, graphicsDevice, Color.White, movementType, speed)
        {
            Sphere = new SpherePrimitive(graphicsDevice, 1, 16);
        }
    }
}
