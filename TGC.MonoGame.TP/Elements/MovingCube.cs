﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TGC.MonoGame.TP.Geometries;

namespace TGC.MonoGame.TP.Elements
{
    public class MovingCube : MovingObject
    {
        public MovingCube(List<Vector3> Points, GraphicsDevice graphicsDevice, ContentManager content, Color color, int movementType = 1, float speed = 10f) : base(Points, graphicsDevice, content, color, movementType, speed)
        {
            Body = new CubePrimitive(graphicsDevice, content, 1, color);
        }

        public MovingCube(List<Vector3> Points, GraphicsDevice graphicsDevice, ContentManager content, int movementType = 1, float speed = 10f) : base(Points, graphicsDevice, content, Color.White, movementType, speed)
        {
            Body = new CubePrimitive(graphicsDevice, content);
        }
    }
}
