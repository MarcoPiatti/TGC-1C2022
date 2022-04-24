using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TGC.MonoGame.TP.Geometries;

namespace TGC.MonoGame.TP.Elements
{

        public class MovingObject
        {
            public Matrix World { get; set; }

            private Vector3 StartPosition { get; set; }
            private Vector3 EndPosition { get; set; }

            public Vector3 Position { get; set; }

            private float speed { get; set; }

            private bool direction = true;

            public MovingObject(Vector3 StartPosition, Vector3 EndPosition, GraphicsDevice graphicsDevice, float speed = 0.1f)
            {
                this.StartPosition = StartPosition;
                this.EndPosition = EndPosition;
                Position = StartPosition;
                this.speed = speed;
            }

            public void Move(GameTime gameTime)
            {
                float deltaTime = Convert.ToSingle(gameTime.ElapsedGameTime.TotalSeconds);
                float marginError = 2 * speed * deltaTime;
                if (direction)
                {
                    Position = ConstantSpeedLerp(Position, StartPosition, EndPosition, speed * deltaTime);
                    if (Vector3.Distance(Position, EndPosition) < marginError)
                        direction = false;
                }
                else
                {
                    Position = ConstantSpeedLerp(Position, EndPosition, StartPosition, speed * deltaTime);
                    if (Vector3.Distance(Position, StartPosition) < marginError)
                        direction = true;
                }
            }

            private Vector3 ConstantSpeedLerp(Vector3 position, Vector3 start, Vector3 end, float speed) {
                return position - (start - end) * speed;
            }
        }

        public class MovingCube : MovingObject
        {
            public CubePrimitive Cube { get; set; }
            public MovingCube(Vector3 StartPosition, Vector3 EndPosition, GraphicsDevice graphicsDevice, float speed = 0.1f) : base(StartPosition, EndPosition, graphicsDevice, speed)
            {
            Cube = new CubePrimitive(graphicsDevice);
            }
        }

    }

