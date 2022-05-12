using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TGC.MonoGame.TP.Elements
{
    public abstract class Collider
    {
        public abstract bool Intersects(Sphere s);
        public abstract Vector3 GetDirectionFromCollision(Sphere s); 
    }
}
