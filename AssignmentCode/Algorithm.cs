using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentCode {
    struct Hypothesis {
        public int[] conjunction;
        public int num_literals;

        public Hypothesis(int[] conjunction, int num_literals) {
            this.conjunction = conjunction;
            this.num_literals = num_literals;
        }
    }

    class Algorithm {
        static Random rand = new Random();
        static int instanceSize = 20;

        static void Main(string[] args) {
            int[] sample_sizes = { 50, 100, 150, 200, 210 };
            int idx = 0;

            double smaller = 0;
            double equal = 0;
            double total_val = 0;

            foreach (int sample_size in sample_sizes) {
                while (idx < 10000) {
                    List<int[]> sample = generate_sample(sample_size);
                    List<int[]> positive = label_sample(sample);

                    if (positive.Count > 0) {
                        Hypothesis hypothesis = learn_hypothesis(positive);
                        double true_loss = 1 - Math.Pow(0.5, hypothesis.num_literals) - (1 - Math.Pow(0.5, 4));

                        if (true_loss <= 0.05)
                            smaller++;

                        if (true_loss == 0)
                            equal++;

                        total_val += true_loss;
                        idx++;
                    }
                }

                Console.WriteLine("m = " + sample_size + " | " + smaller / 10000 + " | " + equal / 10000 + " | " + total_val / 10000);

                idx = 0;
                smaller = 0;
                equal = 0;
                total_val = 0;
            }

            Console.ReadKey();
        }

        private static List<int[]> generate_sample(int sampleSize) {
            List<int[]> sample = new List<int[]>(sampleSize);
            int[] instance;

            for (int idx = 0; idx < sampleSize; idx++) {
                instance = new int[instanceSize];

                for (int idy = 0; idy < instanceSize; idy++)
                    instance[idy] = rand.Next(0, 2);

                sample.Add(instance);
            }

            return sample;
        }

        private static List<int[]> label_sample(List<int[]> sample) {
            List<int[]> positive = new List<int[]>(sample.Count);

            foreach (int[] instance in sample) {
                if (instance[1] == 0)
                    continue;

                if (instance[3] == 1)
                    continue;

                if (instance[5] == 0)
                    continue;

                if (instance[7] == 0)
                    continue;

                positive.Add(instance);
            }

            return positive;
        }

        private static Hypothesis learn_hypothesis(List<int[]> positive) {
            int[] hypothesis = new int[instanceSize];
            positive[0].CopyTo(hypothesis, 0);

            int num_literals = 20;

            for (int idx = 1; idx < positive.Count; idx++) {
                int[] instance = positive[idx];

                for (int idy = 0; idy < instanceSize; idy++) {
                    if (hypothesis[idy] == -1) {
                        continue;
                    }
                    else if (hypothesis[idy] != instance[idy]) {
                        hypothesis[idy] = -1;
                        num_literals--;
                    }
                }
            }

            return new Hypothesis(hypothesis, num_literals);
        }

        private static void PrintInstance(int[] instance) {
            Console.Write("[");
            foreach (int i in instance)
                Console.Write(i == -1 ? "-" : i.ToString());
            Console.WriteLine("]");
        }
    }
}
