using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TGC.MonoGame.TP.Elements;
using TGC.MonoGame.TP.Geometries;

namespace TGC.MonoGame.TP.Niveles
{
    class Sala4 : Sala
    {
        private Vector3 platformScale = new Vector3(10f, 1, 10f);
        private Vector3 arrowScale = new Vector3(4f, 4f, 4f);

        public Cube PisoSalida { get; set; }
        public List<MovingSphere> Spheres { get; set; }
        public List<Cube> Platforms { get; set; }
        public List<Coin> Coins { get; set; }

        public Sala4(ContentManager content, GraphicsDevice graphicsDevice, Vector3 posicion) : base(content, graphicsDevice, posicion)
        {
            Piso = new Cube(graphicsDevice, content, posicion);
            Piso.WorldUpdate(platformScale, new Vector3(-45f, 0, 0) + posicion, Quaternion.Identity);
            PisoSalida = new Cube(graphicsDevice, content, posicion);

            Platforms = new List<Cube>();
            Platforms.Add(new Cube(graphicsDevice, content, new Vector3(-22.5f, 0, 0)));
            Platforms.Add(new Cube(graphicsDevice, content, new Vector3(0, 0, 0)));
            Platforms.Add(new Cube(graphicsDevice, content, new Vector3(0, 0, 22.5f)));
            Platforms.Add(new Cube(graphicsDevice, content, new Vector3(0, 0, -22.5f)));
            Platforms.Add(new Cube(graphicsDevice, content, new Vector3(22.5f, 0, 0)));

            Spheres = new List<MovingSphere>();
            Spheres.Add(new MovingSphere(new List<Vector3> { new Vector3(-33.75f, 5, -20), new Vector3(-33.75f, 5, 20) }, graphicsDevice, content,Color.Red, -2, 45f));
            Spheres.Add(new MovingSphere(new List<Vector3> { new Vector3(-12f, 5, 20), new Vector3(-12f, 5, -20) }, graphicsDevice, content, Color.Red, -2, 45f));
            Spheres.Add(new MovingSphere(new List<Vector3> { new Vector3(12f, 5, -20), new Vector3(12f, 5, 20) }, graphicsDevice, content, Color.Red, -2, 45f));
            Spheres.Add(new MovingSphere(new List<Vector3> { new Vector3(33.75f, 5, 20), new Vector3(33.75f, 5, -20) }, graphicsDevice, content, Color.Red, -2, 45f));
            // esferas con movimiento vertical
            Spheres.Add(new MovingSphere(new List<Vector3> { new Vector3(0, 35, 11.25f), new Vector3(0, -35, 11.25f) }, graphicsDevice, content, Color.Red, -2, 100f));
            Spheres.Add(new MovingSphere(new List<Vector3> { new Vector3(0, -35, -11.25f), new Vector3(0, 35, -11.25f) }, graphicsDevice, content, Color.Red, -2, 100f));


            Coins = new List<Coin>();
            Coins.Add(new Coin(graphicsDevice, content, new Vector3(0, 10, 22.5f) + posicion));
            Coins.Add(new Coin(graphicsDevice, content, new Vector3(0, 10, -22.5f) + posicion));

            foreach (MovingSphere sphere in Spheres)
            {
                sphere.Body.WorldUpdate(arrowScale, sphere.Body.Position + posicion, Quaternion.Identity);
                sphere.MovePoints(Posicion);
            }

            foreach (Cube cube in Platforms)
            {
                cube.WorldUpdate(platformScale, cube.Position + posicion, Quaternion.Identity);
            }

        }

        public override void Draw(GameTime gameTime, Matrix view, Matrix projection)
        {
            base.Draw(gameTime, view, projection);
            PisoSalida.Body.Draw(PisoSalida.World, view, projection);

            foreach(Cube cube in Platforms)
            {
                cube.Body.Draw(cube.World, view, projection);
            }
            
            foreach(MovingSphere sphere in Spheres)
            {
                sphere.Body.Draw(view, projection);
            }

            foreach(Coin coin in Coins)
            {
                coin.Draw(view, projection);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (MovingSphere sphere in Spheres)
            {
                sphere.Move(gameTime);
                sphere.Body.WorldUpdate(arrowScale, sphere.Body.Position,Quaternion.Identity); 
            }

            foreach(Coin coin in Coins)
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

        public override List<TP.Elements.Object> GetPhysicalObjects()
        {
            List<TP.Elements.Object> l = base.GetPhysicalObjects();
            l.Add(PisoSalida);
            l.AddRange(Platforms);
            for (int i = 0; i < Spheres.Count; i++)
            {
                l.Add(Spheres[i].Body);
            }
            return l;
        }
    }
}
