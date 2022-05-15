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

namespace TGC.MonoGame.TP.Niveles
{

    public class Sala2 : Sala
    {
        private Vector3 platformScale = new Vector3(10f, 1f, 10f);

        private Cube PisoSalida { get; set; }
        private List<MovingCube> MovingPlatforms { get; set; }

        private List<Coin> Coins { get; set; }

        public Sala2(ContentManager content, GraphicsDevice graphicsDevice, Vector3 posicion) : base(content, graphicsDevice, posicion)
        {
            Piso = new Cube(graphicsDevice, content, posicion);
            Piso.WorldUpdate(platformScale, new Vector3(-45f, 0, 0) + posicion, Quaternion.Identity);
            PisoSalida = new Cube(graphicsDevice, content, posicion);
            PisoSalida.World = Matrix.CreateScale(platformScale) * Matrix.CreateTranslation(new Vector3(45f, 0, 0) + posicion);

            MovingPlatforms = new List<MovingCube>();
            MovingPlatforms.Add(new MovingCube(new List<Vector3> { new Vector3(0, 0, -40), new Vector3(0, 0, 40) }, graphicsDevice, content ,Color.White));
            MovingPlatforms.Add(new MovingCube(new List<Vector3> { new Vector3(22.5f, 0, 40), new Vector3(22.5f, 0, -40) }, graphicsDevice, content,Color.White));
            MovingPlatforms.Add(new MovingCube(new List<Vector3> { new Vector3(-22.5f, 0, 40), new Vector3(-22.5f, 0, -40) }, graphicsDevice, content,Color.White));
            //MovingCubes.Add(new MovingCube(new List<Vector3> { new Vector3(40, 20, -20), new Vector3(40, 20, 20), new Vector3(40, 40, 20), new Vector3(40, 40, -20) }, graphicsDevice, Color.Red, 2, 25f));
            
            foreach (MovingCube cube in MovingPlatforms)
            {
                cube.Body.WorldUpdate(platformScale, cube.Body.Position + posicion, Quaternion.Identity);
                cube.MovePoints(posicion);
            }
            

            Coins = new List<Coin>();

            Coins.Add(new Coin(graphicsDevice,content, new Vector3(0, 10, -40) + posicion));
            Coins.Add(new Coin(graphicsDevice, content, new Vector3(22.5f, 10, 40) + posicion));
            Coins.Add(new Coin(graphicsDevice, content, new Vector3(-22.5f, 10, 40) + posicion));

            foreach (Coin coin in Coins)
            {
                coin.World = Matrix.CreateScale(platformScale) * Matrix.CreateTranslation(coin.Position + posicion);
            }

        }

        public override void Draw(GameTime gameTime, Matrix view, Matrix projection)
        {
            base.Draw(gameTime, view, projection);
            PisoSalida.Draw(view, projection);

            foreach (MovingCube cube in MovingPlatforms)
            {
                cube.Body.Draw(view, projection);
            }

            foreach (Coin coin in Coins)
            {
                coin.Draw(view, projection);
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (MovingCube cube in MovingPlatforms)
            {
                cube.Move(gameTime);
                cube.Body.WorldUpdate(platformScale,cube.Body.Position, Quaternion.Identity);
            }

            foreach (Coin coin in Coins)
            {
                coin.Update(gameTime);
            }

        }
        public override List<TP.Elements.LogicalObject> GetLogicalObjects()
        {
            List<TP.Elements.LogicalObject> logicalObjects = base.GetLogicalObjects();
            logicalObjects.AddRange(Coins);
            return logicalObjects;
        }
    }
}

