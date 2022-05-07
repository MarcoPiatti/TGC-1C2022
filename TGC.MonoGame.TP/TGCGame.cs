using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TGC.MonoGame.TP.Niveles;
using TGC.MonoGame.TP.Geometries;

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
        private FreeCamera Camera { get; set; }


        /// <summary>
        ///     Se llama una sola vez, al principio cuando se ejecuta el ejemplo.
        ///     Escribir aqui el codigo de inicializacion: el procesamiento que podemos pre calcular para nuestro juego.
        /// </summary>
        protected override void Initialize()
        {
            var screenSize = new Point(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            //Camera = new FollowCamera(GraphicsDevice.Viewport.AspectRatio, new Vector3(-30, 30, 0), screenSize);
            Camera = new FreeCamera(GraphicsDevice.Viewport.AspectRatio, new Vector3(-30, 30, 0), screenSize);

            Player = new Player(GraphicsDevice,Content);
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
            // Aca es donde deberiamos cargar todos los contenido necesarios antes de iniciar el juego.
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            // Cargo el modelo del logo.
            Model = Content.Load<Model>(ContentFolder3D + "tgc-logo/tgc-logo");

            // Cargo un efecto basico propio declarado en el Content pipeline.
            // En el juego no pueden usar BasicEffect de MG, deben usar siempre efectos propios.
            Effect = Content.Load<Effect>(ContentFolderEffects + "BasicShader");

            // Asigno el efecto que cargue a cada parte del mesh.
            // Un modelo puede tener mas de 1 mesh internamente.
            foreach (var mesh in Model.Meshes)
                // Un mesh puede tener mas de 1 mesh part (cada 1 puede tener su propio efecto).
            foreach (var meshPart in mesh.MeshParts)
                meshPart.Effect = Effect;

            base.LoadContent();
        }

        /// <summary>
        ///     Se llama en cada frame.
        ///     Se debe escribir toda la logica de computo del modelo, asi como tambien verificar entradas del usuario y reacciones
        ///     ante ellas.
        /// </summary>
        protected override void Update(GameTime gameTime)
        {
            // Aca deberiamos poner toda la logica de actualizacion del juego.

            //Camera.Update(gameTime, Player.Position);
            Camera.Update(gameTime);

            var keyboardState = Keyboard.GetState();
            Nivel.Update(gameTime);
            // Capturar Input teclado
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                //Salgo del juego.
                Exit();

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                Player.MoveRight();
            }
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                Player.MoveLeft();
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                Player.MoveForward();
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                Player.MoveBackwards();
            }
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                Player.Jump();
            }

            if (Player.Intersects(Nivel.Salas[0].Piso.Collider))
            {
                Player.VectorSpeed = new Vector3(Player.VectorSpeed.X,0, Player.VectorSpeed.Z);
                Player.Body.World *= Matrix.CreateTranslation(new Vector3(0,5,0));
            }

            Player.Update(gameTime);
            Rotation += Convert.ToSingle(gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        /// <summary>
        ///     Se llama cada vez que hay que refrescar la pantalla.
        ///     Escribir aqui el codigo referido al renderizado.
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            // Aca deberiamos poner toda la logia de renderizado del juego.
            GraphicsDevice.Clear(Color.Cyan);
            
            // Para dibujar le modelo necesitamos pasarle informacion que el efecto esta esperando.
            Effect.Parameters["View"].SetValue(Camera.View);
            Effect.Parameters["Projection"].SetValue(Camera.Projection);
            Effect.Parameters["DiffuseColor"].SetValue(Color.DarkBlue.ToVector3());
            var rotationMatrix = Matrix.CreateRotationY(Rotation);
            Nivel.Draw(gameTime, Camera.View, Camera.Projection);
            Player.Draw(Camera.View, Camera.Projection);
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