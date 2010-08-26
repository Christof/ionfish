using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Graphics
{
    public abstract class Game : IDisposable
    {
        private Stopwatch mStopwatch;
        private long mCounter;
        private long mLast;
        private readonly float mFrequency = Stopwatch.Frequency;
        protected Window Window { get; private set; }
        protected float Frametime { get; private set; }

        protected Game()
        {
            Window = new Window();
        }

        protected abstract void Initialize();
        protected abstract void OnFrame();

        public void Run()
        {
            Initialize();

            mStopwatch = new Stopwatch();
            mLast = 0;
            mStopwatch.Start();

            Application.Idle += Render;

            Window.Run();
        }
        
        private void Render(object sender, EventArgs args)
        {
            while (WindowsMessageLoop.HasNewMessages() == false && Window.IsClosing == false)
            {
                Window.ClearRenderTarget();

                OnFrame();

                Window.Present();
                Application.DoEvents();

                long now = mStopwatch.ElapsedTicks;
                Frametime = (now - mLast) / mFrequency;
                mLast = now;
                mCounter++;

                Window.SetCaption(string.Format("{0:000.000} ms | {1} FPS | {2} Avg FPS | {3} Frames", 
                    Frametime * 1000, 1.0f / Frametime, (mLast / (float)mCounter), mCounter));
            }
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