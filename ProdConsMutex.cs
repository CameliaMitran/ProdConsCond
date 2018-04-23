using System;
using System.Threading;

namespace ProducerConsumer
{
    public class ProdConsMutex
    {
        private int bufferSize;
        private int nrOfProducts;
        

        public ProdConsMutex(int bufferSize, int nrOfProducts) {
            this.nrOfProducts = nrOfProducts;
            this.bufferSize = bufferSize;
        }

     
        int Available = 0;
        Mutex BufferIsFull = new Mutex(true);
        Mutex BufferIsEmpty = new Mutex(true);
        Mutex BufferLock = new Mutex(false);


        public void Produce()
        {

            for(int i = 0; i < nrOfProducts; i++ )
            {
                while(Available == bufferSize)
                {
                    Console.WriteLine("Producer wait ");
                    BufferIsFull.WaitOne(2000);
                }

                BufferLock.WaitOne();
                Available++;
                Console.WriteLine("Produced");
                BufferLock.ReleaseMutex();

                try
                {
                    BufferIsEmpty.ReleaseMutex();
                }
                catch { }


            }

        }

        public void Consume()
        {
            for (int i = 0; i < nrOfProducts; i++)
            {
                while (Available < 1)
                {
                    Console.WriteLine("Consumer wait ");
                    BufferIsEmpty.WaitOne(1000);
                }

                BufferLock.WaitOne();
                Available--;
                Console.WriteLine("Consumed");
                BufferLock.ReleaseMutex();

                try
                {
                    BufferIsFull.ReleaseMutex();
                }
                catch { }

            }
        }

    }
}
