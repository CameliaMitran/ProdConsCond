using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ProducerConsumer
{
    class Program
    {

        private const int nrOfProducts = 100;
        private const int BufferSize = 20;


        static void Main(string[] args)
        {
            var prodCons = new ProdConsMutex(BufferSize, nrOfProducts);
            Thread prod = new Thread(new ThreadStart(prodCons.Produce));
            Thread cons = new Thread(new ThreadStart(prodCons.Consume));
            prod.Start();
            cons.Start();

            prod.Join();
            cons.Join();
           
        }
    }
}
