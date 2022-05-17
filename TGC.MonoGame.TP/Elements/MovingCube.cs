using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TGC.MonoGame.TP.Geometries;

namespace TGC.MonoGame.TP.Elements
{
    public class MovingCube : MovingObject
    {
        public MovingCube(List<Vector3> Points, GraphicsDevice graphicsDevice, ContentManager content, Color color, int movementType = 1, float speed = 10f) //: base(Points, graphicsDevice, content, color, movementType, speed)
        {
            Body = new Cube(graphicsDevice, content, Vector3.Zero, color);
            InitializeMovingObject(Points, graphicsDevice, content, movementType, speed);
        }

        public MovingCube(List<Vector3> Points, GraphicsDevice graphicsDevice, ContentManager content, int movementType = 1, float speed = 10f) //: base(Points, graphicsDevice, content, movementType, speed)
        {
            Body = new Cube(graphicsDevice, content, Vector3.Zero);
            InitializeMovingObject(Points, graphicsDevice, content, movementType, speed);
        }
    }

    public class MovingKillerCube : MovingLogicalObject
    {
        public MovingKillerCube(List<Vector3> Points, GraphicsDevice graphicsDevice, ContentManager content, int movementType = 1, float speed = 10f) //: base(Points, graphicsDevice, content, movementType, speed)
        {
            Body = new KillerCube(graphicsDevice, content, Vector3.Zero);
            InitializeMovingObject(Points, graphicsDevice, content, movementType, speed);
        }
    }
}
