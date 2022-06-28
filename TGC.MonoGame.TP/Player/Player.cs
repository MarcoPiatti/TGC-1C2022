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

        /// <summary>
        public float KAmbientGoma = 0.8f;
        public float KDiffuseGoma = 0.6f;
        public float KSpecularGoma = 0.8f;
        ///

        public Vector3 PositionE { get; private set; }
        public Vector3 VectorSpeed { get; set; }
        public Vector3 roundPosition { get; set; }
        private float Gravity = 0.7f;
        public float MoveForceVariation = 0f;
        private float MoveForce = 2f;
        private float MoveForceAir = 0.5f;
        private float JumpForce = 15f;
        private float friction = 0.05f;
        public float Bounce = 0.5f;
        private float CCC = 0.02f; //Collider Correction Constant
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
        public SpherePrimitiveTex drawnBody { get; set; }
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

        //Texturas
        public Texture2D Texture1 { get; set; }
        private Texture2D Texture2 { get; set; }
        private Texture2D Texture3 { get; set; }
        private Model Model { get; set; }
        public Texture2D PlayerTexture { get; set; }
        public Effect PlayerEffect { get; set; }
        private GraphicsDevice currentGraphics { get; set; }
        private RenderTargetCube EnvironmentMapRenderTarget { get; set; }

        public Vector3 checkpoint = new Vector3(-45, 10, 0);

        public float Reflection = 1f;

        public Vector3 Ks = new Vector3(0.7f, 0.6f, 0.3f); //Ambient, Diffuse, Specular

        public bool Initialized = false;

        public Player(GraphicsDevice graphics, ContentManager content, Effect Effect, Color color)
        {

            Model = content.Load<Model>("Models/" + "geometries/sphere");
            //Texture1 = content.Load<Texture2D>("Textures/" + "water");
            currentGraphics = graphics;
            PlayerEffect = content.Load<Effect>("Effects/" + "ShaderBlingPhongTex");
            //Texture2 = content.Load<Texture2D>("Textures/" + "texture1");
            //Texture3 = content.Load<Texture2D>("Textures/" + "texture1");
            PlayerTexture = Texture1;
            PlayerEffect.Parameters["ModelTexture"]?.SetValue(PlayerTexture);
            PlayerEffect.Parameters["ambientColor"].SetValue(Color.White.ToVector3());
            PlayerEffect.Parameters["diffuseColor"].SetValue(Color.White.ToVector3());
            PlayerEffect.Parameters["specularColor"].SetValue(Color.White.ToVector3());

            var deadSound = "dead";
            dead_sound = content.Load<SoundEffect>("Music/" + deadSound);
            var jumpSound = "jump";
            jump_sound = content.Load<SoundEffect>("Music/" + jumpSound);
            var fallSound = "fall";
            fall_sound = content.Load<SoundEffect>("Music/" + fallSound);
            var longFallSound = "long_fall";
            longfall_sound = content.Load<SoundEffect>("Music/" + longFallSound);
            Body = new Sphere(graphics, content, 1f, 16, color);
            drawnBody = new SpherePrimitiveTex(graphics, 1f, 16);
            Body.WorldUpdate(scale, new Vector3(0, 15, 0), Quaternion.Identity);
            Position = Body.Position;
            JumpLine = new Sphere(graphics, content, 1f, 10, new Color(0f, 1f, 0f, 0.3f));
            JumpLine.WorldUpdate(JumpLineScale, Position + JumpLinePos, Quaternion.Identity);
            foreach (var meshPart in Model.Meshes.SelectMany(mesh => mesh.MeshParts))
                meshPart.Effect = PlayerEffect;
        }
        
        public void Init()
        {
            PlayerEffect.Parameters["Reflection"].SetValue(Reflection);
            PlayerEffect.Parameters["KAmbient"].SetValue(Ks.X);
            PlayerEffect.Parameters["KDiffuse"].SetValue(Ks.Y);
            PlayerEffect.Parameters["KSpecular"].SetValue(Ks.Z);
            PlayerEffect.Parameters["ModelTexture"]?.SetValue(PlayerTexture);
            Initialized = true;
        }


        public void Draw(Matrix view, Matrix projection, Vector3 cameraPosition, RenderTarget2D ShadowMapRenderTarget, float ShadowmapSize, Camera ShadowCamera, String Tech, Vector3 LightPosition, RenderTargetCube EnvironmentMapRenderTarget)
        {
            if (!Initialized) Init();
            // Set BasicEffect parameters.
            var playerWorld = this.Body.World;
            PlayerEffect.CurrentTechnique = PlayerEffect.Techniques[Tech];
            //Effect.CurrentTechnique = Effect.Techniques["EnvironmentMapSphere"];
            PlayerEffect.Parameters["environmentMap"].SetValue(EnvironmentMapRenderTarget);
            PlayerEffect.Parameters["lightPosition"].SetValue(LightPosition);
            Matrix InverseTransposeWorld = Matrix.Transpose(Matrix.Invert(playerWorld));
            PlayerEffect.Parameters["InverseTransposeWorld"].SetValue(InverseTransposeWorld);
            PlayerEffect.Parameters["World"].SetValue(playerWorld);
            PlayerEffect.Parameters["View"].SetValue(view);
            PlayerEffect.Parameters["Projection"].SetValue(projection);
            PlayerEffect.Parameters["eyePosition"]?.SetValue(cameraPosition);
            PlayerEffect.Parameters["shadowMapSize"]?.SetValue(Vector2.One * ShadowmapSize);
            PlayerEffect.Parameters["shadowMap"]?.SetValue(ShadowMapRenderTarget);
            PlayerEffect.Parameters["LightViewProjection"]?.SetValue(ShadowCamera.View * ShadowCamera.Projection);
            drawnBody.Draw(playerWorld, view, projection, PlayerEffect);

            /*
            PlayerEffect.Parameters["KAmbient"].SetValue(KAmbientGoma);
            PlayerEffect.Parameters["KDiffuse"].SetValue(KDiffuseGoma);
            PlayerEffect.Parameters["KSpecular"].SetValue(KSpecularGoma);
            PlayerEffect.Parameters["shininess"].SetValue(1f);
            */

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
            else if (Position.Y < -10 && Position.Y > -200)
            {
                if (!flag_fall && !lifesZero)
                {
                    fall_sound.Play();
                    flag_fall = true;
                }
                else if (flag_longfall)
                {
                    longfall_sound.Play();
                }
            }
            else if (Position.Y < -200)
            {
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
                    finalGravity = 0.2f;
                }
                currentPowerUp_2 = "Glide";
                gladePuTime -= elapsedTime;
            }
            else
            {
                currentPowerUp_2 = "N/A";
                gladeActivated = false;
            }

            return finalGravity;
        }

        public void PhyisicallyInteract(List<TP.Elements.Object> objects, float elapsedTime)
        {
            foreach (TP.Elements.Object o in objects)
            {
                if (o.Intersects(Body))
                {
                    var speed = VectorSpeed.Length();
                    VectorSpeed = Vector3.Reflect(VectorSpeed, o.GetDirectionFromCollision(Body));
                    VectorSpeed = Vector3.Normalize(VectorSpeed);
                    VectorSpeed *= speed;
                    int i = 0;
                    while (o.Intersects(Body))
                    {
                        i++;
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
                VectorSpeed += direction * (MoveForce + MoveForceVariation);
            else
                VectorSpeed += direction * (MoveForceAir + MoveForceVariation);
        }

        public void Jump()
        {
            var flag_jump = false;
            if (grounded)
            {

                VectorSpeed += Vector3.Up * JumpForce;
                if (!flag_jump)
                {
                    jump_sound.Play();
                    flag_jump = true;
                }
                flag_jump = false;
            }



        }


        public void returnToCheckPoint()
        {
            if (!(lifes == 0))
            {
                VectorSpeed = Vector3.Zero;
                Position = checkpoint;
                Body.Position = Position;
                Body.WorldUpdate(scale, Position, Quaternion.Identity);
                JumpLine.WorldUpdate(JumpLineScale, Position + JumpLinePos, Quaternion.Identity);
                grounded = false;
                lifes--;
            }
            else
            {
                if (!flag_play)
                {
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
            Position = new Vector3(0, 0, 0);
            Position = Position + new Vector3(0, 15, 0);
            Body.Position = Position;
            Body.WorldUpdate(scale, Position, Quaternion.Identity);
            JumpLine.WorldUpdate(JumpLineScale, Position + JumpLinePos, Quaternion.Identity);
            grounded = false;
            lifes = 3;
            totalCoins = 0;
            lifesZero = false;
            flag_play = false;
            //totalCoins = 0;
            // Nivel.RestartLogicalObjects();
        }
    }

}
