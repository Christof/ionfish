namespace Sandbox
{
    public class Program
    {
        static void Main()
        {
            using (var game = new AISandboxGame())
            {
                game.Run();
            }
        }
    }
}