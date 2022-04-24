using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TGC.MonoGame.TP.Geometries;

namespace TGC.MonoGame.TP.Elements
{
    internal class Platform
    {
        public GeometricPrimitive Geometric { get; set; }
        public Vector3 Position { get; set; }
        public Matrix World { get; set; }

        public Platform(GeometricPrimitive primitive, Vector3 geometricPosition)
        {
            Geometric = primitive;
            Position = geometricPosition;
        }   
    }
}
