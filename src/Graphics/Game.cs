using System;
using System.Windows.Forms;

namespace Graphics
{
    public abstract class Game : IDisposable
    {
        protected Window Window { get; private set; }

        protected Game()
        {
            Window = new Window();
        }

        protected abstract void Initialize();
        protected abstract void OnFrame();

        public void Run()
        {
            Initialize();

            Application.Idle +=
                delegate
                {
                    Window.ClearRenderTarget();

                    OnFrame();

                    Window.Present();
                    Application.DoEvents();
                };

            Window.Run();
        }

        public void Exit()
        {
            Application.Exit();
        }

        public void Dispose()
        {
            Window.Dispose();
        }
    }
}