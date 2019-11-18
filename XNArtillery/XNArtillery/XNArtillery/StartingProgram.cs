using System;

namespace XNArtillery
{
    static class StartingProgram
    {
        static void Main(string[] args)
        {
            using (MyGame myGame = new MyGame(1358, 744))   //1358, 744
            {
                myGame.Run();
            }
        }
    }
}

