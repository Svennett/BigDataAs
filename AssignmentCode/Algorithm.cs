using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentCode
{
    class Algorithm
    {
        static void Main(string[] args)
        {
            Console.WriteLine("vul een reeks nummber in waardes {0,1}");

            int[] conjunction = new int[] { -1 };

            bool b = true;
            while (b)
            {
                string readedLine = Console.ReadLine();
                if (readedLine.Equals("q"))
                    break;

                int[] newData = readedLine.Select(x => (int)char.GetNumericValue(x)).ToArray();
                conjunction = CreateHypothesis(conjunction, newData);

                Console.Write("Conjunctions: ");
                foreach (int i in conjunction)
                    Console.Write(i == -1 ? "-" : i.ToString());
                Console.WriteLine();
            }

            Console.ReadKey();
        }



        private static int[] CreateHypothesis(int[] ch, int[] nd)
        {
            int[] originalCH = new int[ch.Length];
            ch.CopyTo(originalCH, 0);

            if (ch.All(x => x == -1))
                ch = nd;
            else
                for (int i = 0; i < ch.Length; i++)
                {
                    if (ch[i] == -1)
                        continue;
                    else if (ch[i] != nd[i])
                        ch[i] = -1;
                }
            return (ch.Any(x => x != -1)) ? ch : originalCH ;
        }

    }
}
