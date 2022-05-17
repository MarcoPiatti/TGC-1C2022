using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TGC.MonoGame.TP.Elements;

namespace TGC.MonoGame.TP.Niveles
{
    public class Sala1 : Sala
    {
        private SpinningPillar Pilar { get; set; }

        private Coin Coin { get; set; }

        public Sala1(ContentManager content, GraphicsDevice graphicsDevice, Vector3 posicion) : base(content, graphicsDevice, posicion)
        {
            Piso = new Cube(graphicsDevice, content, posicion);
            Piso.WorldUpdate(new Vector3(10f, 1f, 10f), new Vector3(-45f, 0, 0) + posicion, Quaternion.Identity);
            //Piso.World = Matrix.CreateScale() * Matrix.CreateTranslation( );

            Pilar = new SpinningPillar(graphicsDevice,content, posicion);


            Coin = new Coin(graphicsDevice, content, new Vector3(0, 60f, 0) + posicion);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Coin.Update(gameTime);
            Pilar.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, Matrix view, Matrix projection)
        {
            base.Draw(gameTime, view, projection);
            Pilar.Draw(view, projection);
            Coin.Draw(view, projection);
        }

        public override List<TP.Elements.Object> GetPhysicalObjects()
        {
            List<TP.Elements.Object> l = base.GetPhysicalObjects();
            l.Add(Pilar);
            return l;
        }
        public override List<TP.Elements.LogicalObject> GetLogicalObjects()
        {
            List<TP.Elements.LogicalObject> logicalObjects = base.GetLogicalObjects();
            logicalObjects.Add(Coin);
            return logicalObjects;
        }
    }
}
