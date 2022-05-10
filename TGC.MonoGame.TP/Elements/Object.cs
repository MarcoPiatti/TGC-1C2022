using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TGC.MonoGame.Samples.Collisions;
using TGC.MonoGame.TP.Geometries;

namespace TGC.MonoGame.TP.Elements
{

   public class Object
    {
        public Matrix World;

        public Vector3 Position { get; set; }

        public GeometricPrimitive Body { get; set; }

        public void Draw(Matrix view, Matrix projection)
        {
            Body.Draw(World, view, projection);
        }

        public virtual void WorldUpdate(Vector3 scale, Vector3 traslation, Quaternion rotation) { 
            World = Matrix.CreateScale(scale) * Matrix.CreateTranslation(Position + traslation) * Matrix.CreateFromQuaternion(rotation);
            Position = Position + traslation;
        }
    } 

    public class Cube : Object
    {
        public OrientedBoundingBox Collider { get; set; }

        public Cube(GraphicsDevice graphicsDevice, ContentManager content, Vector3 Position, Color color)
        {
            Collider = new OrientedBoundingBox(new Vector3(0,0,0), new Vector3(1, 1, 1));
            Body = new CubePrimitive(graphicsDevice, content, 1, color);
            this.Position = Position;
        }

        public Cube(GraphicsDevice graphicsDevice, ContentManager content, Vector3 Position)
        {
            Collider = new OrientedBoundingBox(new Vector3(0, 0, 0), new Vector3(1, 1, 1));
            Body = new CubePrimitive(graphicsDevice, content, 1, Color.White);
            this.Position = Position;
        }

        public override void WorldUpdate(Vector3 scale, Vector3 traslation, Quaternion rotation)
        {
            base.WorldUpdate(scale, traslation, rotation);
            Collider.Extents = scale;
            Collider.Center += traslation;
            Collider.Rotate(rotation); 
        }
    }

    public class Sphere: Object
    {

        public BoundingSphere Collider { get; set; }

        public Sphere(GraphicsDevice graphicsDevice, ContentManager content, float diameter, int tessellation, Color color)
        {
            Collider = new BoundingSphere(Vector3.Zero, 1f);
            Body = new SpherePrimitive(graphicsDevice, content, diameter, tessellation,color);
        }

        public Sphere(GraphicsDevice graphicsDevice, ContentManager content, float diameter, int tessellation)
        {
            Collider = new BoundingSphere(Vector3.Zero, 1f);
            Body = new SpherePrimitive(graphicsDevice, content, diameter, tessellation);
        }

        public override void WorldUpdate(Vector3 scale, Vector3 traslation, Quaternion rotation)
        {
            base.WorldUpdate(scale, traslation, rotation);
            BoundingSphere collider = Collider;
            collider.Radius = scale.X/2;
            collider.Center += traslation;
            Collider = collider;
        }
    }

    public class Cylinder : Object
    {
        public Cylinder(GraphicsDevice graphicsDevice, ContentManager content, float height = 1, float diameter = 1,int tessellation = 32)
        {
            Body = new CylinderPrimitive(graphicsDevice, content, height, diameter, tessellation);
        }

        public override void WorldUpdate(Vector3 scale, Vector3 traslation, Quaternion rotation)
        {
            base.WorldUpdate(scale, traslation, rotation);
        }
    }

}