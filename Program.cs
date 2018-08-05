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

                #region Server side 
                Console.ForegroundColor = ConsoleColor.Cyan;
                DisplayFrame(frame);
                var frameDelta = FrameDelta(frame, lastFrame);
                Console.WriteLine("Frame Length = " +  frame.Length.ToString() + 
                    " True: " + frame.Count(x=>x).ToString()
                    + " False: " + frame.Count(x => !x).ToString());
                #endregion

                #region ClientSide
                Console.ForegroundColor = ConsoleColor.Green;               
                lastFrame = DisplayDeltaFrame(lastFrame, frameDelta);                
                Console.WriteLine("Delta Length = " + frameDelta.Count.ToString());
                #endregion

                Thread.Sleep(1000);
                Console.Clear();
                
                if(init) init = false;
            }
        }

        public static List<int> FrameDelta(bool[] currentFrame, bool[] lastFrame)
        {
            List<int> delta = new List<int>();

            for(int i = 0; i < lastFrame.Length; i++)
            {
                if (lastFrame[i] != currentFrame[i])
                    delta.Add(i);
            }
            return delta;
        }

        public static bool[] CreateFrame(Random r)
        {
            bool[] frame = new bool[FRAMESIZE];
            for (int i = r.Next(0, frame.Length); i > 0; i--)
                frame[r.Next(0, frame.Length)] = true;

            return frame;
        }

        static bool[] DisplayDeltaFrame(bool[] lastFrame, List<int> frameDelta)
        {
            var frame = lastFrame;

            // construct new frame 
            foreach (int i in frameDelta)
                frame[i] = !lastFrame[i];   

            char[] ascii = new char[FRAMESIZE];

            //convert frame to ascii
            for (int i = 0; i < FRAMESIZE; i++)
            {
                if (frame[i]) ascii[i] = (char)35;
                else ascii[i] = (char)32;
            }

            Console.WriteLine(ascii);

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
