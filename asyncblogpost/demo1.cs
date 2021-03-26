using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace asyncblogpost
{
    public class demo1
    {
        // So everything goes step by step and the elapsed time to accomplish all these functions is the sum of each function time to do the work.
        public void Demo1()
        {
            Stopwatch sw = new Stopwatch();

            int bookPlane = BookPlane();
            int bookHotel = BookHotel();
            int bookCar = BookCar();

            Console.WriteLine($"Finished in {sw.ElapsedMilliseconds / 1000.0}");
        }

        public void Demo2()
        {
            Stopwatch sw = new Stopwatch();

            // this finishes very fast because all these threads created here they come out of threading pool, because if main thread is closed the background threads are aborted.

            Task<int> carTask = Task.Factory.StartNew<int>(BookCar);
            Task<int> planeTask = Task.Factory.StartNew<int>(BookPlane);
            Task<int> hotelTask = Task.Factory.StartNew<int>(BookHotel);
            Task followupTask = hotelTask.ContinueWith(
                taskPrev => Console.WriteLine($"Adding view note for booking {taskPrev.Result}")
                );

            followupTask.Wait();

            // the important thing is that we have to wait the tasks to finish.
            // they work in pararell and finish by how long it takes them to finish
            // it takes 8 seconds because is the longest task
            Task.WaitAll(carTask, hotelTask, planeTask);

            // these results will have to wait till the tasks are done.
            Console.WriteLine($"Finished booking: carId={carTask.Result}, hotelId={hotelTask.Result},planeId={planeTask.Result}");
            Console.WriteLine($"Finished in {sw.ElapsedMilliseconds / 1000.0}");
        }

        static Random rand = new Random();

        private int BookCar()
        {
            Console.WriteLine("Booking car...");
            Thread.Sleep(5000);
            Console.WriteLine("Done with car...");
            return rand.Next(100);
        }

        private int BookHotel()
        {
            Console.WriteLine("Booking hotel...");
            Thread.Sleep(5000);
            Console.WriteLine("Done with hotel...");
            return rand.Next(100);
        }

        private int BookPlane()
        {
            Console.WriteLine("Booking plane...");
            Thread.Sleep(5000);
            Console.WriteLine("Done with plane...");
            return rand.Next(100);
        }
    }
}
