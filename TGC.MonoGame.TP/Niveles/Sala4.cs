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
        public List<MovingKillerSphere> Spheres { get; set; }
        public List<Cube> Platforms { get; set; }
        public List<Coin> Coins { get; set; }

        PowerUp powerUp;

        public Sala4(ContentManager content, GraphicsDevice graphicsDevice, Vector3 posicion) : base(content, graphicsDevice, posicion)
        {
            Piso = new Cube(graphicsDevice, content, posicion);
            Piso.WorldUpdate(platformScale, new Vector3(-45f, 0, 0) + posicion, Quaternion.Identity);
            PisoSalida = new Cube(graphicsDevice, content, posicion);

            Platforms = new List<Cube>();
            Platforms.Add(new Cube(graphicsDevice, content, new Vector3(-22.5f, 0, 0)));
            Platforms.Add(new Cube(graphicsDevice, content, new Vector3(0, 0, 0)));
            Platforms.Add(new Cube(graphicsDevice, content, new Vector3(0, 0, 22.5f)));

            // plataforma con powerUP
            Platforms.Add(new Cube(graphicsDevice, content, new Vector3(22.5f, 5, 22.5f)));
            //TODO: FIX rotación
            powerUp = new GladePU(graphicsDevice, content, new Vector3(22.5f, 9, 22.5f) + posicion);

            // plataforma a la que solo se llega con el powerUP
            Platforms.Add(new Cube(graphicsDevice, content, new Vector3(35, 44, -35)));

            Platforms.Add(new Cube(graphicsDevice, content, new Vector3(0, 0, -22.5f)));
            Platforms.Add(new Cube(graphicsDevice, content, new Vector3(22.5f, 0, 0)));

            Spheres = new List<MovingKillerSphere>();
            Spheres.Add(new MovingKillerSphere(new List<Vector3> { new Vector3(-33.75f, 5, -20), new Vector3(-33.75f, 5, 20) }, graphicsDevice, content, -2, 45f));
            Spheres.Add(new MovingKillerSphere(new List<Vector3> { new Vector3(-12f, 5, 20), new Vector3(-12f, 5, -20) }, graphicsDevice, content, -2, 45f));
            Spheres.Add(new MovingKillerSphere(new List<Vector3> { new Vector3(12f, 5, -20), new Vector3(12f, 5, 20) }, graphicsDevice, content, -2, 45f));
            Spheres.Add(new MovingKillerSphere(new List<Vector3> { new Vector3(33.75f, 5, 20), new Vector3(33.75f, 5, -20) }, graphicsDevice, content, -2, 45f));
            // esferas con movimiento vertical
            Spheres.Add(new MovingKillerSphere(new List<Vector3> { new Vector3(0, 35, 11.25f), new Vector3(0, -35, 11.25f) }, graphicsDevice, content, -2, 100f));
            Spheres.Add(new MovingKillerSphere(new List<Vector3> { new Vector3(0, -35, -11.25f), new Vector3(0, 35, -11.25f) }, graphicsDevice, content, -2, 100f));

            Coins = new List<Coin>();
            Coins.Add(new Coin(graphicsDevice, content, new Vector3(0, 10, 22.5f) + posicion));
            Coins.Add(new Coin(graphicsDevice, content, new Vector3(0, 10, -22.5f) + posicion));
            Coins.Add(new Coin(graphicsDevice, content, new Vector3(35, 48, -35) + posicion));

            foreach (MovingKillerSphere sphere in Spheres)
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
            
            foreach(MovingKillerSphere sphere in Spheres)
            {
                sphere.Body.Draw(view, projection);
            }

            foreach(Coin coin in Coins)
            {
                coin.Draw(view, projection);
            }
        }

        public override void DrawTranslucent(GameTime gameTime, Matrix view, Matrix projection)
        {
            powerUp.Draw(view, projection);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (MovingKillerSphere sphere in Spheres)
            {
                sphere.Move(gameTime);
                sphere.Body.WorldUpdate(arrowScale, sphere.Body.Position,Quaternion.Identity); 
            }

            foreach(Coin coin in Coins)
            {
                coin.Update(gameTime);
            }

            powerUp.Update(gameTime);
        }
        public override List<TP.Elements.LogicalObject> GetLogicalObjects()
        {
            List<TP.Elements.LogicalObject> logicalObjects = base.GetLogicalObjects();
            logicalObjects.AddRange(Coins);
            logicalObjects.Add(powerUp);
            for (int i = 0; i < Spheres.Count; i++)
            {
                logicalObjects.Add(Spheres[i].Body);
            }
            return logicalObjects;
        }

        public override List<TP.Elements.Object> GetPhysicalObjects()
        {
            List<TP.Elements.Object> l = base.GetPhysicalObjects();
            l.Add(PisoSalida);
            l.AddRange(Platforms);
            return l;
        }
    }
}
