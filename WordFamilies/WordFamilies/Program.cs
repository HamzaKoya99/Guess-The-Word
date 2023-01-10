using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace WordFamilies
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool sessionOpen = true;

            while (sessionOpen)
            {
                MainGame g = new MainGame();
                g.Initialise();
                g.MainLoop();
                sessionOpen = g.PlayAgain();
            }
        }
    }
}
