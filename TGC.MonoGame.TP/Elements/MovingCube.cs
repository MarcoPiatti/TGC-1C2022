using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TGC.MonoGame.TP.Geometries;

namespace TGC.MonoGame.TP.Elements
{
    public class MovingCube : MovingObject
    {
        public CubePrimitive Cube { get; set; }
        public MovingCube(List<Vector3> Points, GraphicsDevice graphicsDevice, Color color, int movementType = 1, float speed = 10f) : base(Points, graphicsDevice, color, movementType, speed)
        {
            Cube = new CubePrimitive(graphicsDevice, 1, color);
        }

        public MovingCube(List<Vector3> Points, GraphicsDevice graphicsDevice, int movementType = 1, float speed = 10f) : base(Points, graphicsDevice, Color.White, movementType, speed)
        {
            Cube = new CubePrimitive(graphicsDevice);
        }

        public void Draw(Matrix view, Matrix projection)
        {
            Cube.Draw(World, view, projection);
        }
    }
}
