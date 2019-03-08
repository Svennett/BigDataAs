using RDotNet;
using RDotNet.NativeLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentCode
{
    class Algorithm
    {
        static List<int[]> instances = new List<int[]>();
        static Random rand = new Random();
        static int instanceSize = 20;
        static int sampleSize = 50;

        static void Main(string[] args)
        {
            Console.WriteLine("Lets start");

            int[] conjunction = SampleLearningHypothesis();

            foreach (int[] instance in instances)
                PrintInstance(instance);

            Console.WriteLine("------------------------------------ Hd --");
            PrintInstance(conjunction);


            //REngine.SetEnvironmentVariables("C:/Program Files/R/R-3.4.3/bin/i386", "C:/Program Files/R/R-3.4.3");
            //REngine engine = REngine.GetInstance();
            //engine.Initialize();

            //engine.Evaluate("x <- rbinom(20,1,0.5)");
            //var df = engine.Evaluate("x <- rbinom(20,1,0.5)").AsDataFrame();

            Console.ReadKey();
        }

        private static int[] SampleLearningHypothesis()
        {
            int[] hype = new int[] { -1 };

            for (int i = 0; i < sampleSize; i++)
            {
                int[] instance = GenerateInstance();
                hype = CreateHypothesis(hype, instance);
            }
            return hype;
        }



        private static int[] CreateHypothesis(int[] ch, int[] nd)
        {
            int[] originalCH = new int[ch.Length];
            ch.CopyTo(originalCH, 0);

            int[] ch2 = new int[ch.Length];
            ch.CopyTo(ch2, 0);

            if (ch2.All(x => x == -1))
                ch2 = nd;
            else
                for (int i = 0; i < ch2.Length; i++)
                {
                    if (ch2[i] == -1)
                        continue;
                    else if (ch2[i] != nd[i])
                        ch2[i] = -1;
                }
            return (ch2.Any(x => x != -1)) ? ch2 : originalCH ;
        }

        private static int[] GenerateInstance()
        {
            int[] instance = new int[instanceSize];
            
            for (int i = 0; i < instanceSize; i++)
            {
                int rndmNr = rand.Next(0, 2);
                if (rndmNr > 1)
                    rndmNr = 1;

                instance[i] = rndmNr;
            }

            instances.Add(instance);
            return instance;
        }


        private static void PrintInstance(int[] instance)
        {
            Console.Write("[");
            foreach (int i in instance)
                Console.Write(i == -1 ? "-" : i.ToString());
            Console.WriteLine("]");
        }
    }
}
