using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG
{
    class InputHandler
    {
        static InputHandler instance;
        InputHandler()
        {

        }

        public static InputHandler getInstance()
        {
            if (instance == null)
            {
                instance = new InputHandler();
            }
            return instance;
        }


        public int InputHandlerForInt(int min, int max)
        {
            string input = "";
            int p = 0;
            bool abort = false;
            while (!abort)
            {
                input = Console.ReadLine();
                int.TryParse(input, out p);
                if (p == 0)
                {
                    Console.WriteLine($"Please enter a valid number! [{min}-{max}]");
                }
                else
                {
                    if (p < min || p > max)
                    {
                        Console.WriteLine($"Please enter a valid number! [{min}-{max}]");
                    }
                    else
                    {
                        abort = true;
                    }

                }
            }
            return p;
        }

        public string InputHandlerForString(int max)
        {
            string input = "";
            bool abort = false; 
            while(!abort)
            {
                input = Console.ReadLine();
                if(input.Length > max)
                {
                    Console.WriteLine($"Please enter a shorter message! [0-{max}]");
                } else
                {
                    abort = true; 
                }
            }
            return input; 
        }


    }
}
