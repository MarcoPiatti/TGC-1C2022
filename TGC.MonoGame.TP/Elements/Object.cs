using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TGC.MonoGame.Samples.Collisions;
using TGC.MonoGame.TP.Geometries;

namespace TGC.MonoGame.TP.Elements
{

   public abstract class Object
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

        public abstract bool Intersects(Sphere s);

        public abstract Vector3 GetDirectionFromCollision(Sphere s);
    } 

    public class Cube : Object
    {
        public OrientedBoundingBox Collider { get; set; }

        public Cube(GraphicsDevice graphicsDevice, ContentManager content, Vector3 Position, Color color)
        {
            Collider = new OrientedBoundingBox(Position, new Vector3(1, 1, 1));
            Body = new CubePrimitive(graphicsDevice, content, 1, color);
            this.Position = Position;
        }

        public Cube(GraphicsDevice graphicsDevice, ContentManager content, Vector3 Position)
        {
            Collider = new OrientedBoundingBox(Position, new Vector3(1, 1, 1));
            Body = new CubePrimitive(graphicsDevice, content, 1, Color.White);
            this.Position = Position;
        }

        public override void WorldUpdate(Vector3 scale, Vector3 traslation, Quaternion rotation)
        {
            base.WorldUpdate(scale, traslation, rotation);
            Collider.Center += traslation; 
            Collider.Extents = Collider.Center + scale/2;
            Collider.Rotate(rotation); 
        }
        public override bool Intersects(Sphere s)
        {
            var aabb = new BoundingBox(Collider.Center - Collider.Extents + Collider.Center, Collider.Extents);
            return aabb.Intersects(s.Collider);
        }
        public override Vector3 GetDirectionFromCollision(Sphere s)
        {
            float X = Math.Abs(Collider.Center.X - Collider.Extents.X);
            float Y = Math.Abs(Collider.Center.Y - Collider.Extents.Y);
            float Z = Math.Abs(Collider.Center.Z - Collider.Extents.Z);
            float Xdistance = Math.Abs(Collider.Center.X - s.Collider.Center.X) + s.Collider.Radius;
            float Ydistance = Math.Abs(Collider.Center.X - s.Collider.Center.X) + s.Collider.Radius;
            float Zdistance = Math.Abs(Collider.Center.X - s.Collider.Center.X) + s.Collider.Radius;
            if (X > Xdistance && Y > Ydistance)
                return new Vector3(1, 1, -1);
            if (X > Xdistance && Z > Zdistance)
                return new Vector3(1, -1, 1);
            if (Y > Ydistance && Z > Zdistance)
                return new Vector3(-1, 1, 1);
            if (X > Xdistance)
                return new Vector3(1, -1, -1);
            if (Y > Ydistance)
                return new Vector3(-1, 1, -1);
            if (Z > Zdistance)
                return new Vector3(-1, -1, 1);
            Vector3 v = s.Collider.Center - Collider.Center;
            v.Normalize();
            return v;
        }
    }

    public class Sphere : Object
    {

        public BoundingSphere Collider { get; set; }

        public Sphere(GraphicsDevice graphicsDevice, ContentManager content, float diameter, int tessellation, Color color)
        {
            Collider = new BoundingSphere(Vector3.Zero, 1f);
            Body = new SpherePrimitive(graphicsDevice, content, diameter, tessellation, color);
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
            collider.Radius = scale.X / 2;
            collider.Center += traslation;
            Collider = collider;
        }

        public override bool Intersects(Sphere s)
        {
            return false;
        }
        public override Vector3 GetDirectionFromCollision(Sphere s)
        {
            return Vector3.One;
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
        public override bool Intersects(Sphere s)
        {
            return false;
        }
        public override Vector3 GetDirectionFromCollision(Sphere s)
        {
            return Vector3.One;
        }
    }
    public abstract class LogicalObject : Object
    {
        public abstract void logicalAction(Sphere player);
  
        public override bool Intersects(Sphere s)
        {
            return false;
        }
        public override Vector3 GetDirectionFromCollision(Sphere s)
        {
            return Vector3.One;
        }
    }

    public class AlmostSphere : LogicalObject
    {
        private Color unColor { get; set; }
        public OrientedBoundingBox Collider { get; set; }

        public AlmostSphere(GraphicsDevice graphicsDevice, ContentManager content, float diameter, int tessellation, Color color)
        {
            unColor = color;
            Collider = new OrientedBoundingBox(Position, new Vector3(1, 1, 1));
            Body = new SpherePrimitive(graphicsDevice, content, diameter, tessellation, unColor);
            this.Position = Position;
        }

        public AlmostSphere(GraphicsDevice graphicsDevice, ContentManager content, float diameter, int tessellation)
        {
            Collider = new OrientedBoundingBox(Position, new Vector3(1, 1, 1));
            Body = new SpherePrimitive(graphicsDevice, content, diameter, tessellation, Color.White);
            this.Position = Position;
        }

        public override void WorldUpdate(Vector3 scale, Vector3 traslation, Quaternion rotation)
        {
            base.WorldUpdate(scale, traslation, rotation);
            Collider.Center += traslation;
            Collider.Extents = Collider.Center + scale / 2;
            Collider.Rotate(rotation);
        }
        public override bool Intersects(Sphere s)
        {
            var aabb = new BoundingBox(Collider.Center - Collider.Extents + Collider.Center, Collider.Extents);
            return aabb.Intersects(s.Collider);
        }
        public override void logicalAction(Sphere player) { }
    }


}