using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TGC.MonoGame.TP.Geometries;
using TGC.MonoGame.TP.Elements;
using TGC.MonoGame.Samples.Collisions;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using TGC.MonoGame.TP.Niveles;

namespace TGC.MonoGame.TP
{
    public class Player
    {
        public Vector3 PositionE { get; private set; }
        public Vector3 VectorSpeed { get; set; }
        public Vector3 roundPosition { get; set; }
        private float Gravity = 0.7f;
        private float MoveForce = 2f;
        private float MoveForceAir = 0.5f;
        private float JumpForce = 15f;
        private float friction = 0.05f;
        public float Bounce = 0.5f;
        private float CCC = 0.01f; //Collider Correction Constant
        public int totalCoins { get; set; } = 0;
        public int lifes { get; set; } = 3;
        public bool lifesZero { get; set; } = false;
        public Nivel Nivel { get; set; }
        public float speedPuTime = 0;
        public bool gladeActivated = false;
        public float gladePuTime = 0;
        public string currentPowerUp_1 { get; set; } = "N/A";
        public string currentPowerUp_2 { get; set; } = "N/A";
        public string currentPowerUp_3 { get; set; } = "N/A";

        public string typeName = "base";

        private Vector3 scale = new Vector3(5, 5, 5);
        private State estado { get; set; }

        public Sphere Body { get; set; }
        public bool flag_play { get; set; } = false;
        public bool flag_fall { get; set; } = false;
        public bool flag_longfall { get; set; } = false;
        public bool grounded = false;

        public Sphere JumpLine { get; set; }
        public Vector3 JumpLinePos = new Vector3(0, -3f, 0);
        public Vector3 JumpLineScale = new Vector3(5, 5, 5);
        public Vector3 Position { get; set; }

        public Vector3 PreFallPosition { get; set; }

        public SoundEffect dead_sound { get; set; }
        public SoundEffect jump_sound { get; set; }
        public SoundEffect fall_sound { get; set; }
        public SoundEffect longfall_sound { get; set; }
        public Player(GraphicsDevice graphics, ContentManager content)
        {
            var deadSound = "dead";
            dead_sound = content.Load<SoundEffect>("Music/" + deadSound);
            var jumpSound = "jump";
            jump_sound = content.Load<SoundEffect>("Music/" + jumpSound);
            var fallSound = "fall";
            fall_sound = content.Load<SoundEffect>("Music/" + fallSound);
            var longFallSound = "long_fall";
            longfall_sound = content.Load<SoundEffect>("Music/" + longFallSound);
            Body = new Sphere(graphics, content, 1f, 16, Color.Green);
            Body.WorldUpdate(scale, new Vector3(0, 15, 20), Quaternion.Identity);
            Position = Body.Position;
            JumpLine = new Sphere(graphics, content, 1f, 10, new Color(0f, 1f, 0f, 0.3f));
            JumpLine.WorldUpdate(JumpLineScale, Position + JumpLinePos, Quaternion.Identity);
        }

        public void Draw(Matrix view, Matrix projection)
        {
            Body.Draw(view, projection);
            //JumpLine.Draw(view, projection);
        }

        public void Update(GameTime gameTime, List<TP.Elements.Object> objects, List<TP.Elements.LogicalObject> logicalObjects)
        {
            var elapsedTime = Convert.ToSingle(gameTime.ElapsedGameTime.TotalSeconds);
            float finalGravity = handleGladePowerUp(elapsedTime);
            grounded = CanJump(objects);
            if (!grounded)
                VectorSpeed += Vector3.Down * finalGravity;
            else
                VectorSpeed -= VectorSpeed * friction;
            var scaledSpeed = VectorSpeed * elapsedTime;
            Vector3 finalScaledSpeed = handleSpeedPowerUp(scaledSpeed, elapsedTime);
            Body.WorldUpdate(scale, Position + finalScaledSpeed, Matrix.CreateRotationZ(VectorSpeed.X) * Matrix.CreateRotationX(VectorSpeed.Z));
            Position = Body.Position;
            JumpLine.WorldUpdate(new Vector3(1, 1f, 1), Position + JumpLinePos, Quaternion.Identity);
            PhyisicallyInteract(objects, elapsedTime);
            LogicalInteract(logicalObjects);
            if (Position.Y > 0) PreFallPosition = Position;
            else if(Position.Y < -10 && Position.Y > -200) {
                if (!flag_fall && !lifesZero)
                {
                    fall_sound.Play();
                    flag_fall = true;
                }
                else if(flag_longfall)
                {
                    longfall_sound.Play();
                }
            }
            else if (Position.Y < -200) {
                returnToCheckPoint();
                flag_fall = false;
                flag_longfall = false;
            }
        }

        private Vector3 handleSpeedPowerUp(Vector3 scaledSpeed, float elapsedTime)
        {
            Vector3 finalScaledSpeed;

            if (speedPuTime > 0)
            {
                currentPowerUp_1 = "Speed";
                finalScaledSpeed = scaledSpeed * 2;
                speedPuTime -= elapsedTime;
            }
            else
            {
                currentPowerUp_1 = "N/A";
                finalScaledSpeed = scaledSpeed;
            }

            return finalScaledSpeed;
        }

        private float handleGladePowerUp(float elapsedTime)
        {
            float finalGravity = Gravity;

            if (gladePuTime > 0)
            {
              if (Keyboard.GetState().IsKeyDown(Keys.Space))
              {
                  gladeActivated = true;
              }
              if (gladeActivated) {
                currentPowerUp_2 = "Glide";
                finalGravity = 0.4f;
                gladePuTime -= elapsedTime;
                }
            } else
            {
                currentPowerUp_2 = "N/A";
                gladeActivated = false;
            }

            return finalGravity;
        }

        public void PhyisicallyInteract(List<TP.Elements.Object> objects,float elapsedTime)
        {
            foreach (TP.Elements.Object o in objects)
            {
                if (o.Intersects(Body))
                {
                    var speed = VectorSpeed.Length() * elapsedTime;
                    VectorSpeed = Vector3.Reflect(VectorSpeed, o.GetDirectionFromCollision(Body));
                    VectorSpeed.Normalize();
                    VectorSpeed *= speed;
                    while (o.Intersects(Body))
                    {
                        Body.WorldUpdate(scale, Position + VectorSpeed * CCC, Quaternion.Identity);
                        Position = Body.Position;
                        JumpLine.WorldUpdate(JumpLineScale, Position + JumpLinePos, Quaternion.Identity);
                    }
                    VectorSpeed *= Bounce;
                }
            }
        }

        public bool CanJump(List<TP.Elements.Object> objects)
        {
            foreach (TP.Elements.Object o in objects)
            {
                if (o.Intersects(JumpLine))
                {
                    return true;
                }
            }
            return false;
        }

        public void LogicalInteract(List<TP.Elements.LogicalObject> logicalObjects)
        {
            foreach (TP.Elements.LogicalObject o in logicalObjects)
            {
                if (o.Intersects(Body))
                {
                    o.logicalAction(this);
                }
            }
        }

        public void Move(Vector3 direction)
        {
            if (grounded)
                VectorSpeed += direction * MoveForce;
            else
                VectorSpeed += direction * MoveForceAir;
        }

        public void Jump()
        {
            var flag_jump = false;
            if (grounded) {

                VectorSpeed += Vector3.Up * JumpForce;
                if (!flag_jump) {
                    jump_sound.Play();
                    flag_jump = true;
                }
                flag_jump = false;
            }



        }


        public void returnToCheckPoint()
        {
            if(!(lifes == 0)) {
                VectorSpeed = Vector3.Zero;
                Position = new Vector3(MathF.Truncate((PreFallPosition.X + 50) / 100) * 100, 10, 0);
                Position = Position + new Vector3(-45, 0, 0);
                Body.Position = Position;
                Body.WorldUpdate(scale, Position, Quaternion.Identity);
                JumpLine.WorldUpdate(JumpLineScale, Position + JumpLinePos, Quaternion.Identity);
                grounded = false;
                lifes--;
            }
            else
            {
                if (!flag_play) {
                    dead_sound.Play();

                    flag_play = true;
                }
                flag_longfall = true;
                lifesZero = true;

            }


        }

        public void AddCoin()
        {
            totalCoins++;
        }
        public void Restart()
        {
            VectorSpeed = Vector3.Zero;
            Position = new Vector3(0,0,0);
            Position = Position + new Vector3(0, 15, 0);
            Body.Position = Position;
            Body.WorldUpdate(scale, Position, Quaternion.Identity);
            JumpLine.WorldUpdate(JumpLineScale, Position + JumpLinePos, Quaternion.Identity);
            grounded = false;
            lifes = 3;
            lifesZero = false;
            flag_play = false;
            //totalCoins = 0;
           // Nivel.RestartLogicalObjects();
        }
    }

}
