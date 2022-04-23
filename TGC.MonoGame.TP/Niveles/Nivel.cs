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

        private CubePrimitive Cubo { get; set; }
        private Matrix CubeWorld { get; set; }

        private List<Matrix> TileWorlds { get; set; }
        private float TileSize = 50f;
        private int MapSizeInTiles = 20;
        private int[,] ExisteTile = new int[,]
        {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0 },
            { 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0 },
            { 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0 },
            { 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0 }
        };

        /// <summary>
        /// Creates a City Scene with a content manager to load resources.
        /// </summary>
        /// <param name="content">The Content Manager to load resources</param>
        public Nivel(ContentManager content, GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            
            // Load an effect that will be used to draw the scene
            Effect = content.Load<Effect>(ContentFolderEffects + "BasicShader");

            Cubo = new CubePrimitive(graphicsDevice, TileSize, Color.White);
            CubeWorld = Matrix.Identity;
            // Create a list of places where the city model will be drawn
            TileWorlds = DeterminarWorldMatrices();
            /*
            WorldMatrices = new List<Matrix>()
            {
                Matrix.Identity,
                Matrix.CreateTranslation(Vector3.Right * DistanceBetweenCities),
                Matrix.CreateTranslation(Vector3.Left * DistanceBetweenCities),
                Matrix.CreateTranslation(Vector3.Forward * DistanceBetweenCities),
                Matrix.CreateTranslation(Vector3.Backward * DistanceBetweenCities),
                Matrix.CreateTranslation((Vector3.Forward + Vector3.Right) * DistanceBetweenCities),
                Matrix.CreateTranslation((Vector3.Forward + Vector3.Left) * DistanceBetweenCities),
                Matrix.CreateTranslation((Vector3.Backward + Vector3.Right) * DistanceBetweenCities),
                Matrix.CreateTranslation((Vector3.Backward + Vector3.Left) * DistanceBetweenCities),
            };
            */
        }

        /// <summary>
        /// Draws the City Scene
        /// </summary>
        /// <param name="gameTime">The Game Time for this frame</param>
        /// <param name="view">A view matrix, generally from a camera</param>
        /// <param name="projection">A projection matrix</param>
        public void Draw(GameTime gameTime, Matrix view, Matrix projection)
        {

            // Set the View and Projection matrices, needed to draw every 3D model
            Effect.Parameters["View"].SetValue(view);
            Effect.Parameters["Projection"].SetValue(projection);

            foreach(var world in TileWorlds){
                Cubo.Draw(world, view, projection);
            }
            //Cubo.Draw(Effect);
            // Get the base transform for each mesh
            // These are center-relative matrices that put every mesh of a model in their corresponding location
           // var modelMeshesBaseTransforms = new Matrix[Model.Bones.Count];
           // Model.CopyAbsoluteBoneTransformsTo(modelMeshesBaseTransforms);


        }

        private List<Matrix> DeterminarWorldMatrices()
        {
            List<Matrix> WorldMatrices = new List<Matrix>();
            for(int i=0; i < MapSizeInTiles; i++)
            {
                for(int j=0; j < MapSizeInTiles; j++)
                {
                    if (ExisteTile[i, j] == 1)
                    {
                        WorldMatrices.Add(Matrix.CreateTranslation(new Vector3(i * TileSize, 0, j * TileSize)));
                    }
                }
            }
            return WorldMatrices;
        }
    }
}
