using System;
namespace XNArtillery
{
    static class Program
    {
        static void Main(string[] args)
        {
            using (MyGame runningGame = new MyGame())
            {
                runningGame.Run();
            }
        }
    }
}

