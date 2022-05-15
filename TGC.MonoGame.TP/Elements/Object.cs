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

        public virtual void Draw(Matrix view, Matrix projection)
        {
            Body.Draw(World, view, projection);
        }

        public virtual void WorldUpdate(Vector3 scale, Vector3 newPosition, Quaternion rotation)
        {
            World = Matrix.CreateScale(scale) * Matrix.CreateTranslation(newPosition) * Matrix.CreateFromQuaternion(rotation);
            Position = newPosition;
        }
        

        public virtual void WorldUpdate(Vector3 scale, Quaternion rotation)
        {
            World = Matrix.CreateScale(scale) * Matrix.CreateTranslation(Position) * Matrix.CreateFromQuaternion(rotation);
        }

        public virtual void WorldUpdate(Vector3 scale, Vector3 newPosition, Matrix rotationMatrix)
        {
            World = Matrix.CreateScale(scale) * rotationMatrix * Matrix.CreateTranslation(newPosition);
            Position = newPosition;
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

        public override void WorldUpdate(Vector3 scale, Vector3 newPosition, Quaternion rotation)
        {
            base.WorldUpdate(scale, newPosition, rotation);
            Collider.Center = newPosition; 
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
            float Xdistance = Math.Abs(Collider.Center.X - s.Collider.Center.X);
            float Ydistance = Math.Abs(Collider.Center.Y - s.Collider.Center.Y);
            float Zdistance = Math.Abs(Collider.Center.Z - s.Collider.Center.Z);
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
            return new Vector3(-1, -1, -1);
        }
    }

    public class TrianglePrism : Object
    {

        public TrianglePrism(GraphicsDevice graphicsDevice, ContentManager content, Vector3 Position, Color color)
        {
            Body = new TrianglePrismPrimitive(graphicsDevice, content, 1, color);
            this.Position = Position;
        }

        public TrianglePrism(GraphicsDevice graphicsDevice, ContentManager content, Vector3 Position)
        {
            Body = new TrianglePrismPrimitive(graphicsDevice, content, 1, Color.White);
            this.Position = Position;
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

    public class Sphere : Object
    {

        public BoundingSphere Collider { get; set; }
        private SpherePrimitive currentBody { get; set; }

        public Sphere(GraphicsDevice graphicsDevice, ContentManager content, float diameter, int tessellation, Color color)
        {
            Collider = new BoundingSphere(Vector3.Zero, 1f);
 
            currentBody = new SpherePrimitive(graphicsDevice, content, diameter, tessellation, color);
            Body = currentBody;
        }

        public Sphere(GraphicsDevice graphicsDevice, ContentManager content, float diameter, int tessellation)
        {
            Collider = new BoundingSphere(Vector3.Zero, 1f);
            Body = new SpherePrimitive(graphicsDevice, content, diameter, tessellation);
        }

        public override void WorldUpdate(Vector3 scale, Vector3 newPosition, Quaternion rotation)
        {
            base.WorldUpdate(scale, newPosition, rotation);
            BoundingSphere collider = Collider;
            collider.Radius = scale.X / 2;
            collider.Center = newPosition;
            Collider = collider;
        }

        public override void WorldUpdate(Vector3 scale, Vector3 newPosition, Matrix rotationMatrix)
        {
            base.WorldUpdate(scale, newPosition, rotationMatrix);
            BoundingSphere collider = Collider;
            collider.Radius = scale.X / 2;
            collider.Center = newPosition;
            Collider = collider;
        }

        public override bool Intersects(Sphere s)
        {
            var boundingSphere = new BoundingSphere(Position, currentBody.diameter/2);
            return boundingSphere.Intersects(s.Collider);
        }
        public override Vector3 GetDirectionFromCollision(Sphere s)
        {
            return Vector3.One;
        }
    }



    public class Cylinder : Object
    {
        public Cylinder(GraphicsDevice graphicsDevice, ContentManager content, Color color, float height = 1, float diameter = 1,int tessellation = 32)
        {
            Body = new CylinderPrimitive(graphicsDevice, content, color, height, diameter, tessellation);
        }

        public override void WorldUpdate(Vector3 scale, Vector3 newPosition, Quaternion rotation)
        {
            base.WorldUpdate(scale, newPosition, rotation);
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
        public bool flagCollide { get; set; } = false;
        public virtual void logicalAction(Player player) { }

        public override Vector3 GetDirectionFromCollision(Sphere s)
        {
            return Vector3.One;
        }
        public virtual void destroyItself() {
            World = Matrix.CreateScale(new Vector3(0, 0, 0)) * Matrix.CreateTranslation(Position + new Vector3(0, 100, 0));
            //Collider = new BoundingSphere(new Vector3(0f, 1000f, 0f), 0f);
        }
    }
    public class LogicalSphere : LogicalObject
    {

        public BoundingSphere Collider { get; set; }

        public LogicalSphere(GraphicsDevice graphicsDevice, ContentManager content, float diameter, int tessellation, Color color)
        {
            Collider = new BoundingSphere(Position, diameter/2);
            Body = new SpherePrimitive(graphicsDevice, content, diameter, tessellation, color);
        }

        public LogicalSphere(GraphicsDevice graphicsDevice, ContentManager content, float diameter, int tessellation)
        {
            Collider = new BoundingSphere(Position, diameter / 2);
            Body = new SpherePrimitive(graphicsDevice, content, diameter, tessellation);
        }

        public override void WorldUpdate(Vector3 scale, Vector3 newPosition, Quaternion rotation)
        {
            base.WorldUpdate(scale, newPosition, rotation);
            BoundingSphere collider = Collider;
            collider.Radius = scale.X / 2;
            collider.Center = newPosition;
            Collider = collider;
        }

        public override void WorldUpdate(Vector3 scale, Vector3 newPosition, Matrix rotationMatrix)
        {
            base.WorldUpdate(scale, newPosition, rotationMatrix);
            BoundingSphere collider = Collider;
            collider.Radius = scale.X / 2;
            collider.Center = newPosition;
            Collider = collider;
        }

        public override bool Intersects(Sphere player)
        {
           // var BoundingSphere = new BoundingSphere(player.Collider.Center, player.Collider.Radius);
            var playerCenter = player.Collider.Center;
            var playerRadius = player.Collider.Radius;
            return Math.Pow(Vector3.Dot((Collider.Center - playerCenter),(Collider.Center - playerCenter)), 2) <= Math.Pow(Collider.Radius - playerRadius, 2);

        }


    }

    public class LogicalCyllinder : LogicalObject
    {

        public BoundingCylinder Collider { get; set; }

        public LogicalCyllinder(GraphicsDevice graphicsDevice, ContentManager content, Color color, float height = 1, float diameter = 1, int tessellation = 32)
        {
            Collider = new BoundingCylinder(Position, diameter/2, height);
            Body = new CylinderPrimitive(graphicsDevice, content, color, height, diameter, tessellation);
            this.Position = Position;
        }

        public LogicalCyllinder(GraphicsDevice graphicsDevice, ContentManager content, float height = 1, float diameter = 1, int tessellation = 1)
        {
            Collider = new BoundingCylinder(Position, diameter / 2, height);
            Body = new CylinderPrimitive(graphicsDevice, content, Color.White, height, diameter, tessellation);
            this.Position = Position;
        }

        public override bool Intersects(Sphere player)
        {
            var BoundingSphere = new BoundingSphere(player.Collider.Center, player.Collider.Radius);
            return Collider.Intersects(BoundingSphere);
        }

         public override void destroyItself(){
            World = Matrix.CreateScale(new Vector3(0, 0, 0)) * Matrix.CreateTranslation(Position + new Vector3(0, 100, 0));
            Collider = new BoundingCylinder(Position + new Vector3(0f, 100f, 0f), 0f, 0f);
        }
}


}