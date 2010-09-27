using Graphics;

namespace Sandbox
{
    public class Program
    {
        static void Main()
        {
            Game game = new TextureDemo();
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