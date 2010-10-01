using System;
using Graphics;
using System.IO;

namespace Sandbox
{
    public class Program
    {
        static void Main()
        {
            try
            {
                Game game = new SandboxGame();
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
            catch (Exception exception)
            {
                using (var writer = new StreamWriter("ionfish.log"))
                {
                    writer.Write(exception.Message);
                    writer.Write(exception.StackTrace);
                }
                throw;
            }
        }
    }
}