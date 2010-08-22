namespace Sandbox
{
    public class Program
    {
        static void Main()
        {
            using (var game = new SandboxGame())
            {
                game.Run();
            }
        }
    }
}