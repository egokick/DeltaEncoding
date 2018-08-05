using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Threading;

namespace DeltaEncoding
{
    class Program
    {
        const int FRAMESIZE = 1000;

        static void Main(string[] args)
        {
            Random r = new Random();
            bool init = true;
            bool[] lastFrame = new bool[FRAMESIZE];

            while (true) {

                var frame = CreateFrame(r);
                if (init) lastFrame = frame;

                Console.ForegroundColor = ConsoleColor.Cyan;
                DisplayFrame(frame);
                Console.WriteLine("Frame Length = " +  frame.Length.ToString() + 
                    " True: " + frame.Count(x=>x).ToString()
                    + " False: " + frame.Count(x => !x).ToString());


                Console.ForegroundColor = ConsoleColor.Green;
                DisplayFrame(lastFrame);
                Console.WriteLine("Frame Length = " + lastFrame.Length.ToString() +
                    " True: " + lastFrame.Count(x => x).ToString()
                    + " False: " + lastFrame.Count(x => !x).ToString());

                Thread.Sleep(1000);
                Console.Clear();

                lastFrame = frame;
                if(init) init = false;
            }
        }

        public static bool[] CreateFrame(Random r)
        {
            bool[] frame = new bool[FRAMESIZE];
            for (int i = r.Next(0, frame.Length); i > 0; i--)
                frame[r.Next(0, frame.Length)] = true;

            return frame;
        }
       
        static void DisplayFrame(bool[] frame)
        {
            char[] ascii = new char[FRAMESIZE];
            //convert frame to ascii
            for (int i = 0; i < FRAMESIZE; i++)
            {
                if (frame[i]) ascii[i] = (char)35;
                else ascii[i] = (char)32;
            }

            Console.WriteLine(ascii);            
        }



    }
}
