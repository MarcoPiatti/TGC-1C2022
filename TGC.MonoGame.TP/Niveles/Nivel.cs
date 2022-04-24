using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TGC.MonoGame.TP.Geometries;

namespace TGC.MonoGame.TP.Niveles
{

    public class Nivel
    {
        public const string ContentFolderEffects = "Effects/";

        private Model Model { get; set; }
        private List<Matrix> WorldMatrices { get; set; }
        private Effect Effect { get; set; }
        private GraphicsDevice graphicsDevice { get; }

        private List<Sala> Salas { get; set; }
       
        public Nivel(ContentManager content, GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;

            Salas = new List<Sala>();
            //pendiente for para procedural
            Salas.Add(new Sala0(content, graphicsDevice, new Vector3(0 * Sala.Size,0,0)));
            Salas.Add(new Sala1(content, graphicsDevice, new Vector3(1 * Sala.Size, 0, 0)));
            Salas.Add(new Sala2(content, graphicsDevice, new Vector3(2 * Sala.Size, 0, 0)));
            Salas.Add(new Sala4(content, graphicsDevice, new Vector3(3 * Sala.Size, 0, 0)));

            // Load an effect that will be used to draw the scene
            Effect = content.Load<Effect>(ContentFolderEffects + "BasicShader");
        }

        public void Draw(GameTime gameTime, Matrix view, Matrix projection)
        {

            // Set the View and Projection matrices, needed to draw every 3D model
            Effect.Parameters["View"].SetValue(view);
            Effect.Parameters["Projection"].SetValue(projection);
            
            foreach (Sala s in Salas){
                s.Draw(gameTime, view, projection);
            }


        }

        public void Update(GameTime gameTime) {
            foreach (Sala s in Salas)
            {
                s.Update(gameTime);
            }
        }

    }
}
