using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TGC.MonoGame.TP.Geometries;

namespace TGC.MonoGame.TP.Coin
{

    public class CoinClass
    {
        public Matrix World { get; set; }


        public Vector3 Position { get; set; }
        public SpherePrimitive Coin { get; set; }

        public CoinClass(Vector3 Position, GraphicsDevice graphicsDevice)
        {
            Coins = new List<CoinClass>();
            Coins.Add(new Coin_1(content, graphicsDevice, new Vector3(0 * Sala.Size, 0, 0)));
            Coins.Add(new Coin_2(content, graphicsDevice, new Vector3(1 * Sala.Size, 0, 0)));
            Coins.Add(new Coin_3(content, graphicsDevice, new Vector3(2 * Sala.Size, 0, 0)));

            // Load an effect that will be used to draw the scene
            Effect = content.Load<Effect>(ContentFolderEffects + "BasicShader");
            Coin = new SpherePrimitive(graphicsDevice);
            World = Matrix.CreateScale(5, 5, 5) * Matrix.CreateTranslation(Position);
            this.Position = Position;
        }

    }
    public void Draw(GameTime gameTime, Matrix view, Matrix projection)
    {

        // Set the View and Projection matrices, needed to draw every 3D model
        Effect.Parameters["View"].SetValue(view);
        Effect.Parameters["Projection"].SetValue(projection);

        foreach (CoinClass s in Coins)
        {
            s.Draw(gameTime, view, projection);
        }
    }

}
