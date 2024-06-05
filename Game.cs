using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.GraphicsLibraryFramework;
using PacMan.Graphics;


namespace PacMan
{
    internal class Game : GameWindow
    {
        ShaderProgram program;
        Brick brick;
        Coin coin;
        Player player;
        Map map;
        int width, height;
        Camera camera;
        public Game(int width, int height) : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            this.width = width;
            this.height = height;

            // center window
            CenterWindow(new Vector2i(width, height));
        }
        // called whenever window is resized
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, e.Width, e.Height);
            this.width = e.Width;
            this.height = e.Height;
        }

        // called once when game is started
        protected override void OnLoad()
        {
            base.OnLoad();
            GL.Enable(EnableCap.DepthTest);
            VSync = VSyncMode.On;
            brick = new Brick();
            player = new Player();
            coin = new Coin();
            map = new Map("../../../Textures/map.bmp");
            program = new ShaderProgram("shader.vert", "shader.frag");
            camera = new Camera(width, height, new Vector3(0.048f*14,0.048f*15.5f,2.5f));
            CursorState = CursorState.Grabbed;
        }
        // called once when game is closed
        protected override void OnUnload()
        {
            base.OnUnload();
            coin.Delete();
            brick.Delete();
            player.Delete();
        }

        // called every frame
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.ClearColor(0.0f, 0.0f, 0.0f, 1f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            KeyboardState input = KeyboardState;
            if (map.coins==0)
            {
                Console.WriteLine("You Win!");
                this.Close();
            }
            // transformation matrices
            Matrix4 model = Matrix4.Identity;
            Matrix4 view = camera.GetViewMatrix();
            Matrix4 projection = camera.GetProjectionMatrix();
            int modelLocation = GL.GetUniformLocation(program.ID, "model");
            int viewLocation = GL.GetUniformLocation(program.ID, "view");
            int projectionLocation = GL.GetUniformLocation(program.ID, "projection");
            GL.UniformMatrix4(viewLocation, true, ref view);
            GL.UniformMatrix4(projectionLocation, true, ref projection);

            for (int i=0;i<28;++i)
            {
                for(int j=0;j<31;++j) 
                {
                    model = Matrix4.Identity;
                    model *= Matrix4.CreateTranslation((28 - i) * 0.048f, (31 - j) * 0.048f,0);
                    GL.UniformMatrix4(modelLocation,true,ref model);

                    if (map.map[i, j] == 1)
                        brick.Render(program);
                    if (map.map[i, j] == 2)
                        coin.Render(program);
                }
            }

            
            model = Matrix4.Identity;
            model *= Matrix4.CreateTranslation((28 - player.Position.X) * 0.048f, (31 - player.Position.Y) * 0.048f,0);
            GL.UniformMatrix4(modelLocation, true, ref model);
            player.Render(program);

            Context.SwapBuffers();
            base.OnRenderFrame(args);
        }
        // called every frame. All updating happens here

        int Timer = 0;
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            Timer++;
            MouseState mouse = MouseState;
            KeyboardState input = KeyboardState;
            Title = $"Ascannel's CatFish World 3D Score: " + map.score;
            base.OnUpdateFrame(args);
            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            player.ChangePosition(input, map, Timer);
            if (Timer == 15) Timer = 0;


            camera.Update(input, mouse, args);

        }
    }
}
