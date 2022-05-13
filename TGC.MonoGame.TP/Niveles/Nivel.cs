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

    public class Nivel
    {
        public const string ContentFolderEffects = "Effects/";
        public const int CantidadSalas = 12;
        private Model Model { get; set; }
        private List<Matrix> WorldMatrices { get; set; }
        private Effect Effect { get; set; }
        private GraphicsDevice graphicsDevice { get; }
        public List<Sala> Salas { get; set; }

        public List<TP.Elements.Object> PhysicalObjects { get; set; }
        public List<TP.Elements.LogicalObject> LogicalObjects { get; set; }
        public Nivel(ContentManager content, GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            PhysicalObjects = new List<TP.Elements.Object>();
            LogicalObjects = new List<TP.Elements.LogicalObject>();
            Salas = new List<Sala>();
            Random rnd = new Random();
            Salas.Add(new Sala0(content, graphicsDevice, new Vector3(0 * Sala.Size, 0, 0)));
            int i;
            for ( i = 1; i<= CantidadSalas; i++) {
                switch (rnd.Next(1, 5)) { 
                    case 1:
                    Salas.Add(new Sala1(content, graphicsDevice, new Vector3(i * Sala.Size, 0, 0)));
                    break;
                case 2:
                    Salas.Add(new Sala2(content, graphicsDevice, new Vector3(i * Sala.Size, 0, 0)));
                    break;
                case 3:
                    Salas.Add(new Sala3(content, graphicsDevice, new Vector3(i * Sala.Size, 0, 0)));
                    break;
                case 4:
                    Salas.Add(new Sala4(content, graphicsDevice, new Vector3(i * Sala.Size, 0, 0)));
                    break;
                }
            }
            Salas.Add(new SalaFin(content, graphicsDevice, new Vector3(i * Sala.Size, 0, 0)));
            foreach (Sala s in Salas) {
                PhysicalObjects.AddRange(s.GetPhysicalObjects());
            }
            foreach (Sala s in Salas)
            {
                LogicalObjects.AddRange(s.GetLogicalObjects());
            }
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
