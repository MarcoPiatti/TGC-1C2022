using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TGC.MonoGame.TP.Elements;
using TGC.MonoGame.TP.Geometries;

namespace TGC.MonoGame.TP.Niveles
{
    public class Sala0 : Sala
    {
        private Cube ParedSur { get; set; }
        private Cube FirstPlatform { get; set; }

        private Coin Coin { get; set; }

        public Sala0(ContentManager content, GraphicsDevice graphicsDevice, Vector3 posicion) : base(content, graphicsDevice, posicion)
        {
     
            Coin = new Coin(graphicsDevice,content,new Vector3(25, 20, 0) + posicion);

            FirstPlatform = new Cube(graphicsDevice, content, posicion);
            FirstPlatform.WorldUpdate(new Vector3(10f, 1f, 10f), new Vector3(25, 10, 0) + posicion, Quaternion.Identity);


            ParedSur = new Cube(graphicsDevice, content, posicion);
            ParedSur.WorldUpdate(new Vector3(1f, Size, Size), new Vector3(-Size / 2, Size / 2, 0) + posicion,Quaternion.Identity);
        }

        public override void Draw(GameTime gameTime, Matrix view, Matrix projection)
        {
            base.Draw(gameTime, view, projection);
            FirstPlatform.Draw( view, projection);
            ParedSur.Draw( view, projection);
            Coin.Draw( view, projection);
        }
        
        
        public override void Update(GameTime gameTime)
        {
            Coin.Update(gameTime);
            base.Update(gameTime);
        }

        public override List<TP.Elements.Object> GetPhysicalObjects()
        {
            List<TP.Elements.Object> l = base.GetPhysicalObjects();
            l.Add(ParedSur);
            l.Add(FirstPlatform);
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
