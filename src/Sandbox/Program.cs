using System;
using Graphics;
using System.IO;

namespace Sandbox
{
    public class Program
    {
        static void Main()
        {
#if DEBUG
            Run();
#else
            try
            {
                Run();
            }
            catch (Exception exception)
            {
                using (var writer = new StreamWriter("ionfish.log"))
                {
                    writer.Write(exception.Message);
                    writer.Write(exception.StackTrace);
                }
                throw;
            }
#endif
        }

        private static void Run()
        {
            Game game = new WriteDemo();
            while (true)
            {
                using (game)
                {
                    game.Run();
                }

                using (var demoSeleciton = new DemoSelection())
                {
                    demoSeleciton.ShowDialog();
                    if (demoSeleciton.CloseAll)
                    {
                        return;
                    }
                    game = demoSeleciton.Game;
                }
            }
        }
    }
}