using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TGC.MonoGame.TP.Geometries;

namespace TGC.MonoGame.TP.Elements
{
    public class MathC
    {
        public static float ToRadians(float angle)
        {
            return MathF.PI * 2 * angle / 360;
        }
    }
}
