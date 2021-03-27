using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IntroToTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            AnotherExample.Example();
        }

        private static void Example1()
        {
            Task t = new Task(Speak);
            //t.Start();
            //Task.Factory.StartNew(Speak); // most frequen one because it allows some configuration
            Task.Run(new Action(Speak));

            Console.WriteLine("Waiting for completion");
            t.Wait(); // main thread is gonna wait until everything is finished
            Console.WriteLine("All done"); // this line waits 
        }

        private static void Example2()
        {
            // LongRunning option will destroy the thread at the end
            Task.Factory.StartNew(WhatTypeOfThread, TaskCreationOptions.LongRunning).Wait(); // it proves that its pulling from thread pool the thread
        }

        private static void Example3()
        {
            // we are supplying an action and the compiler will do the rest
            Task.Factory.StartNew(() => Speak("Hello world")).Wait();
        }

        private static void Example4()
        {
            Task<int> t = Task.Factory.StartNew(() => Add(1, 2));

            // when we call result it will wait till the result is complet
            Console.WriteLine(t.Result);
        }

        private static int Add(int x, int y)
        {
            return x + y;
        }

        // This will not show all the time because the thread pool has full control
        // Program starts and ends too quicly
        private static void Speak()
        {
            Console.WriteLine("hello world");
        }

        private static void Speak(string message)
        {
            Console.WriteLine(message);
        }

        private static void WhatTypeOfThread()
        {
            var threadPoolThread = Thread.CurrentThread.IsThreadPoolThread;

            Console.WriteLine($"I'm a thread of type threadpool: {threadPoolThread}");
        }
    }
}

// Foreground thread 
// -- Keeps the program its still working till its done

// Background thread
// -- Don't have the power to keep the program alive


// By default our CPU bound Tasks works on background Tasks