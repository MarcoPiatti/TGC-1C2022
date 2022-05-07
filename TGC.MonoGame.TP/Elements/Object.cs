using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TGC.MonoGame.TP.Geometries;

namespace TGC.MonoGame.TP.Elements
{

   public class Object
    {
        public Matrix World { get; set; }

        public Vector3 Position { get; set; }

        public GeometricPrimitive Body { get; set; }

        public void Draw(Matrix view, Matrix projection)
        {
            Body.Draw(World, view, projection);
        }
    } 

    public class Cube : Object
    {
        public Cube(GraphicsDevice graphicsDevice, ContentManager content, Vector3 Position, Color color)
        {
            Body = new CubePrimitive(graphicsDevice, content, 1, color);
            this.Position = Position;
        }

        public Cube(GraphicsDevice graphicsDevice, ContentManager content, Vector3 Position)
        {
            Body = new CubePrimitive(graphicsDevice, content, 1, Color.White);
            this.Position = Position;
        }
    }

    public class Sphere: Object
    {
        public Sphere(GraphicsDevice graphicsDevice, ContentManager content, float diameter, int tessellation, Color color)
        {
            Body = new SpherePrimitive(graphicsDevice, content, diameter, tessellation,color);
        }

        public Sphere(GraphicsDevice graphicsDevice, ContentManager content, float diameter, int tessellation)
        {
            Body = new SpherePrimitive(graphicsDevice, content, diameter, tessellation);
        }

    }

    public class Cylinder : Object
    {
        public Cylinder(GraphicsDevice graphicsDevice, ContentManager content, float height = 1, float diameter = 1,int tessellation = 32)
        {
            Body = new CylinderPrimitive(graphicsDevice, content, height, diameter, tessellation);
        }

    }

}