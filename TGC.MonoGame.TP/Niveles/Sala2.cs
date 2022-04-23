using System;
using System.Collections.Generic;
using System.Text;

namespace TGC.MonoGame.TP.Niveles
{
    
    public class Sala2 : Sala
    {
        public Sala(GraphicsDevice graphicsDevice) : base(graphicsDevice)
        {
            
        }

        public void Draw(GameTime gameTime, Matrix view, Matrix projection)
        {
            base.Draw(gameTime,view,projection);
        }
    }
}
