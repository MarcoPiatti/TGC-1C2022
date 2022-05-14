using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TGC.MonoGame.TP.Niveles;
using TGC.MonoGame.TP.Geometries;
using Microsoft.Xna.Framework.Media;
using TGC.MonoGame.Niveles.SkyBox;

namespace TGC.MonoGame.TP
{
    /// <summary>
    ///     Esta es la clase principal  del juego.
    ///     Inicialmente puede ser renombrado o copiado para hacer más ejemplos chicos, en el caso de copiar para que se
    ///     ejecute el nuevo ejemplo deben cambiar la clase que ejecuta Program <see cref="Program.Main()" /> linea 10.
    /// </summary>
    public class TGCGame : Game
    {
        public const string ContentFolder3D = "Models/";
        public const string ContentFolderEffects = "Effects/";
        public const string ContentFolderMusic = "Music/";
        public const string ContentFolderSounds = "Sounds/";
        public const string ContentFolderSpriteFonts = "SpriteFonts/";
        public const string ContentFolderTextures = "Textures/";

        //private FreeCamera Camera { get; set; }

        private Nivel Nivel { get; set; }
        /// <summary>
        ///     Constructor del juego.
        /// </summary>
        public TGCGame()
        {
            // Maneja la configuracion y la administracion del dispositivo grafico.
            Graphics = new GraphicsDeviceManager(this);
            // Descomentar para que el juego sea pantalla completa.
            // Graphics.IsFullScreen = true;
            // Carpeta raiz donde va a estar toda la Media.
            Content.RootDirectory = "Content";
            // Hace que el mouse sea visible.
            IsMouseVisible = true;
        }
         
        private GraphicsDeviceManager Graphics { get; }
        private SpriteBatch SpriteBatch { get; set; }
        private Model Model { get; set; }
        private Effect Effect { get; set; }
        private float Rotation { get; set; }
        private Matrix World { get; set; }
        private Matrix View { get; set; }
        private Matrix Projection { get; set; }
        private Player Player { get; set; }
        //private FollowCamera Camera { get; set; }
        private Camera Camera { get; set; }

        private float CameraChangeCooldown = 0f;

        private Vector3 CameraInitPosition = new Vector3(-30, 30, 0);

        private SpriteFont SpriteFont { get; set; }
       //Canciones
        private string SongName { get; set; }
        private Song Song { get; set; }
        //Menu
        private const int mainMenu = 0; 
        private const int estado1 = 1;
        private const int estado2 = 2; 

        private int estadoMenu = mainMenu; 
        private Point screenSize { get; set; } 

        //Skybox
        private SkyBox unaSkyBox { get; set; }

        /// <summary>
        ///     Se llama una sola vez, al principio cuando se ejecuta el ejemplo.
        ///     Escribir aqui el codigo de inicializacion: el procesamiento que podemos pre calcular para nuestro juego.
        /// </summary>
        protected override void Initialize()
        {
            //originalmente variable local screenSize
            screenSize = new Point(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            //Pongo el jugador antes de la camara porque sino no hay Player.Position
            Player = new Player(GraphicsDevice, Content);
            //Camera = new FollowCamera(GraphicsDevice.Viewport.AspectRatio, new Vector3(-30, 30, 0), screenSize);
            Camera = new FollowCamera(GraphicsDevice.Viewport.AspectRatio, Player.Position, screenSize);
            //Camera = new TargetCamera(GraphicsDevice.Viewport.AspectRatio, new Vector3(-30, 30, 0), new Vector3(0,0,0));
            
            // La logica de inicializacion que no depende del contenido se recomienda poner en este metodo.

            // Configuramos nuestras matrices de la escena.
            World = Matrix.Identity;
            View = Matrix.CreateLookAt(Vector3.UnitZ * 150, Vector3.Zero, Vector3.Up);
            var viewMatrix = Matrix.CreateLookAt(new Vector3(0, 0, 50), Vector3.Forward, Vector3.Up);
            Projection =
                Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 1, 250);




            base.Initialize();
        }

        /// <summary>
        ///     Se llama una sola vez, al principio cuando se ejecuta el ejemplo, despues de Initialize.
        ///     Escribir aqui el codigo de inicializacion: cargar modelos, texturas, estructuras de optimizacion, el procesamiento
        ///     que podemos pre calcular para nuestro juego.
        /// </summary>
        protected override void LoadContent()
        {

            Nivel = new Nivel(Content, GraphicsDevice);

            SpriteBatch = new SpriteBatch(GraphicsDevice);
            SpriteFont = Content.Load<SpriteFont>(ContentFolderSpriteFonts + "Cascadia/CascadiaCodePL");

            Effect = Content.Load<Effect>(ContentFolderEffects + "ShaderBlingPhong");

            Effect.Parameters["lightPosition"].SetValue(new Vector3(0, 1000, 0));

            Effect.Parameters["ambientColor"].SetValue(Color.White.ToVector3());
            Effect.Parameters["diffuseColor"].SetValue(Color.White.ToVector3());
            Effect.Parameters["specularColor"].SetValue(Color.White.ToVector3());

            Effect.Parameters["KAmbient"].SetValue(0.7f);
            Effect.Parameters["KDiffuse"].SetValue(0.6f);
            Effect.Parameters["KSpecular"].SetValue(0.3f);

            SongName = "crystal_dolphin";
            Song = Content.Load<Song>(ContentFolderMusic + SongName);

            var skyBox = Content.Load<Model>(ContentFolder3D + "skybox/cube");
            //var skyBoxTexture = Content.Load<TextureCube>(ContentFolderTextures + "skyboxes/sunset/sunset");
            var skyBoxTexture = Content.Load<TextureCube>(ContentFolderTextures + "skyboxes/islands/islands");
            //var skyBoxTexture = Content.Load<TextureCube>(ContentFolderTextures + "/skyboxes/skybox/skybox");
            var skyBoxEffect = Content.Load<Effect>(ContentFolderEffects + "SkyBox");
            unaSkyBox = new SkyBox(skyBox, skyBoxTexture, skyBoxEffect, 100);


            //var skyBox = Content.Load<Model>(ContentFolder3D + "skybox/cube");
            //var skyBoxTexture = Content.Load<TextureCube>(ContentFolderTextures + "skyboxes/skybox/skybox");
            //var skyBoxEffect = Content.Load<Effect>(ContentFolderEffects + "SkyBox");



            base.LoadContent();
        }

        /// <summary>
        ///     Se llama en cada frame.
        ///     Se debe escribir toda la logica de computo del modelo, asi como tambien verificar entradas del usuario y reacciones
        ///     ante ellas.
        /// </summary>
        /// 
        private float flag = 0;
        void cambiarCamara()
        {
            if (flag == 0)
            {
                Camera = new FreeCamera(GraphicsDevice.Viewport.AspectRatio, Player.Position + CameraInitPosition, screenSize);
                flag = 1;
            }
            else
            {
                Camera = new FollowCamera(GraphicsDevice.Viewport.AspectRatio, Player.Position, screenSize);
                flag = 0;
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (estadoMenu == mainMenu)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.C))
                {

                    estadoMenu = estado1;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.G))
                {
                    estadoMenu = estado2;
                    MediaPlayer.Play(Song);
                }
                return;
            }
            else if (estadoMenu == estado1)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.P))
                {
                    estadoMenu = mainMenu;
                }
                return;
            }else if (estadoMenu == estado2)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.H))
                {
                    //se supone que cada segundo puedas presionar recién xd pero no supe como hacerlo
                    if(CameraChangeCooldown <= 0) { 
                        cambiarCamara(); 
                        CameraChangeCooldown = 0.25f; 
                    }
                    
                }
            }

            if(CameraChangeCooldown > 0)
                CameraChangeCooldown -= Convert.ToSingle(gameTime.ElapsedGameTime.TotalSeconds);
            // Aca deberiamos poner toda la logica de actualizacion del juego.

            Camera.UpdatePlayerPosition(Player.Position);
            Camera.Update(gameTime);
            //Camera.Update(gameTime,Player.Position);

            var keyboardState = Keyboard.GetState();
            Nivel.Update(gameTime);
            // Capturar Input teclado
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                //Salgo del juego.
                Exit();

            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                Player.Move(Camera.RightDirection);
            }
            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                Player.Move(Camera.RightDirection * -1);
            }
            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
            {
                Player.Move(Camera.FrontDirection);
            }
            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
            {
                Player.Move(Camera.FrontDirection * -1);
            }
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                Player.Jump();
            }
            Player.Update(gameTime, Nivel.PhysicalObjects, Nivel.LogicalObjects);


            Rotation += Convert.ToSingle(gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        /// <summary>
        ///     Se llama cada vez que hay que refrescar la pantalla.
        ///     Escribir aqui el codigo referido al renderizado.
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            //Logica para dibujar en pantalla posicion exacta del jugador, actualmente no funcionando

            /*
            Vector3 playerRoundPosition = Player.roundPosition;
            Vector3 playerPositionE = Player.PositionE;
            playerRoundPosition = new Vector3(MathF.Round(playerPositionE.X, 2), MathF.Round(playerPositionE.Y, 2), MathF.Round(playerPositionE.Z, 2));
            */
            
            if (estadoMenu == mainMenu)
            {
                DrawCenterText("Marble it up!", 1);
                DrawCenterTextY("Presiona G para comenzar", GraphicsDevice.Viewport.Height * 1 / 12, 1);
                return;
            }
            else if (estadoMenu == estado1)
            {

                return;
            }else if (estadoMenu == estado2)
            {
                /*
                DrawCenterTextY("Las teclas WASD se usan para moverse", GraphicsDevice.Viewport.Height * 1 / 12, 1);
                DrawCenterTextY("SPACE para saltar", GraphicsDevice.Viewport.Height * 3 / 12, 1);
                DrawCenterTextY("R para reiniciar", GraphicsDevice.Viewport.Height * 5 / 12, 1);

                DrawCenterTextY("ESC para salir del juego", GraphicsDevice.Viewport.Height * 9 / 12, 1);
                DrawCenterTextY("P para volver a la Main Menu", GraphicsDevice.Viewport.Height * 11 / 12, 1);
                */

            }

            //Despues del menu restablezco
            
            GraphicsDevice.RasterizerState = new RasterizerState { CullMode = CullMode.None };
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;


            // Aca deberiamos poner toda la logia de renderizado del juego.
            GraphicsDevice.Clear(Color.Cyan);
            
            // Para dibujar le modelo necesitamos pasarle informacion que el efecto esta esperando.
            Effect.Parameters["View"].SetValue(Camera.View);
            Effect.Parameters["Projection"].SetValue(Camera.Projection);
            Effect.Parameters["eyePosition"].SetValue(Camera.Position);
            //unaSkyBox.Draw(Camera.View, Camera.Projection, new Vector3(0,0,0));
            Player.Draw(Camera.View, Camera.Projection);
            Nivel.Draw(gameTime, Camera.View, Camera.Projection);

            //Logica para dibujar en pantalla posicion exacta del jugador, actualmente no funcionando
            //  SpriteBatch.DrawString(SpriteFont, playerRoundPosition.ToString(), new Vector2(GraphicsDevice.Viewport.Width / 2, 0), Color.White);
        }
        public void DrawCenterText(string msg, float escala)
        {
            var W = GraphicsDevice.Viewport.Width;
            var H = GraphicsDevice.Viewport.Height;
            var size = SpriteFont.MeasureString(msg) * escala;
            SpriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null,
                Matrix.CreateScale(escala) * Matrix.CreateTranslation((W - size.X) / 2, (H - size.Y) / 2, 0));
            SpriteBatch.DrawString(SpriteFont, msg, new Vector2(0, 0), Color.CornflowerBlue);
            SpriteBatch.End();
        }

        public void DrawCenterTextY(string msg, float Y, float escala)
        {
            var W = GraphicsDevice.Viewport.Width;
            var H = GraphicsDevice.Viewport.Height;
            var size = SpriteFont.MeasureString(msg) * escala;
            SpriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null,
                Matrix.CreateScale(escala) * Matrix.CreateTranslation((W - size.X) / 2, Y, 0));
            SpriteBatch.DrawString(SpriteFont, msg, new Vector2(0, 0), Color.White);
            SpriteBatch.End();
        }
        /// <summary>
        ///     Libero los recursos que se cargaron en el juego.
        /// </summary>
        protected override void UnloadContent()
        {
            // Libero los recursos.
            Content.Unload();

            base.UnloadContent();
        }
    }
}