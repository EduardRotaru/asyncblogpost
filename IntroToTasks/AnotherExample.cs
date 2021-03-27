using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace IntroToTasks
{
    public static class AnotherExample
    {
        public static void Example()
        {
            TimeIt(SequencialChangesToWin);
            TimeIt(TaskBasedChancesToWin);
        }

        private static void TimeIt(Action action)
        {
            Stopwatch timer = Stopwatch.StartNew();
            action();
            Console.WriteLine(timer.Elapsed);
        }

        private static void SequencialChangesToWin()
        {
            BigInteger n = 49000;
            BigInteger r = 600;

            BigInteger part1 = Factorial(n);
            BigInteger part2 = Factorial(n - r);
            BigInteger part3 = Factorial(r);

            BigInteger chances = part1 / (part2 * part3);

            Console.WriteLine(chances);
        }

        private static void TaskBasedChancesToWin()
        {
            BigInteger n = 49000;
            BigInteger r = 600;

            // doing them at the same time
            // when we call result the thread will wait the result
            Task<BigInteger> part1 = Task<BigInteger>.Factory.StartNew(() => Factorial(n));
            Task<BigInteger> part2 = Task<BigInteger>.Factory.StartNew(() => Factorial(n - r));
            Task<BigInteger> part3 = Task<BigInteger>.Factory.StartNew(() => Factorial(r));

            // with result we are getting the value of a task
            BigInteger chances = part1.Result / (part2.Result * part3.Result);

            Console.WriteLine(chances);

        }

        private static BigInteger Factorial(BigInteger n)
        {
            BigInteger factorial = 1;

            for (BigInteger i = 1; i <= n; i++)
            {
                factorial *= i;
            }

            return factorial;
        }
    }
}
