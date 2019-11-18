using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ByloEngine;

namespace ByloEngine_Tester
{
    class Program
    {
        private const int maxLoops = 50;

        static void Main(string[] args)
        {
            string choose;

            float min;
            float max;
            float loops;

            do
            {
                Console.Write("Si desidera generare un numero (I)ntero, (D)ecimale oppure (U)scire? ");
                choose = Console.ReadLine();
                Console.WriteLine();

                Console.Write("Da che numero e' necessario generare? ");
                min = Convert.ToSingle(Console.ReadLine());
                Console.Write("Fino a che numero e' necessario generare? ");
                max = Convert.ToSingle(Console.ReadLine());

                Console.Write("Quanti numeri si intende generare? ");
                loops = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();

                switch (choose)
                {
                    case "i":
                    case "I":

                        for (int i = 0; i < loops; i += 1)
                        {
                            Console.WriteLine("{0}o numero generato: {1}", i + 1, Randomize.Integer(min, max));
                        }

                        break;
                    case "d":
                    case "D":

                        for (int i = 0; i < loops; i += 1)
                        {
                            Console.WriteLine("Numero generato: {0}", Randomize.Decimal(min, max));
                        }

                        break;
                }

                Console.Write("\n\rPremere un tasto per continuare...");
                Console.ReadLine();

                Console.Clear();
            }
            while ((choose != "u") & (choose != "U"));
        }
    }
}
