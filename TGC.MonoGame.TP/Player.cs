using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TGC.MonoGame.TP.Geometries;

namespace TGC.MonoGame.TP
{
    public class Player
    {
        public Matrix World { get; set; }

        private List<Vector3> Points { get; set; }

        public SpherePrimitive Body { get; set; }

        public Vector3 Position { get; set; }

        public Player(GraphicsDevice graphics, ContentManager content)
        {
            Body = new SpherePrimitive(graphics,content);
            World = Matrix.CreateScale(5, 5, 5) * Matrix.CreateTranslation(new Vector3(0, 6, 0));
        }

        public void Draw(Matrix view, Matrix projection)
        {
            Body.Draw(World, view, projection);
        }

    }

}

