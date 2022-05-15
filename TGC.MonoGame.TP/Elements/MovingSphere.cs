using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TGC.MonoGame.TP.Geometries;

namespace TGC.MonoGame.TP.Elements
{
    public class MovingSphere : MovingObject
    {
        public MovingSphere(List<Vector3> Points, GraphicsDevice graphicsDevice, ContentManager content, Color color, int movementType = 1, float speed = 10f) 
        {
            Body = new Sphere(graphicsDevice, content, 1, 16, color);
            InitializeMovingObject(Points, graphicsDevice, content, color, movementType, speed);
        }
       
        public MovingSphere(List<Vector3> Points, GraphicsDevice graphicsDevice, ContentManager content, int movementType = 1, float speed = 10f) 
        {
            Body = new Sphere(graphicsDevice, content, 1, 16);
            InitializeMovingObject(Points, graphicsDevice, content, Color.White, movementType, speed);
        }
    }
}
