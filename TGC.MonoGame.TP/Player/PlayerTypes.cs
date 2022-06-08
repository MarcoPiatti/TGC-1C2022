using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TGC.MonoGame.TP.Geometries;
using TGC.MonoGame.TP.Elements;
using TGC.MonoGame.Samples.Collisions;

namespace TGC.MonoGame.TP
{
    public class PlayerGum : Player
    {
        public PlayerGum(GraphicsDevice graphics, ContentManager content, Effect effect) : base(graphics, content, effect, Color.Green)
        {
            Bounce = 0.7f;
            MoveForceVariation = -0.2f;
            typeName = "PELOTA DE GOMA";
        }
    }

    public class PlayerIron : Player
    {
        public PlayerIron(GraphicsDevice graphics, ContentManager content, Effect effect) : base(graphics, content, effect, Color.Gray)
        {
            Bounce = 0.1f;
            MoveForceVariation = 0.6f;
            typeName = "PELOTA DE HIERRO";
        }
    }

    public class PlayerWood : Player
    {
        public PlayerWood(GraphicsDevice graphics, ContentManager content, Effect effect) : base(graphics, content, effect, Color.Brown)
        {
            Bounce = 0.5f;
            typeName = "PELOTA DE MADERA";
        }
    }

}

