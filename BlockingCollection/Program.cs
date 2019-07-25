using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace BlockingCollection
{
    class Program
    {
        static void Main(string[] args)
        {

            //Blocking collection that can hold 5 items
            BlockingCollection<int> data = new BlockingCollection<int>(5);

            Task.Run(() =>
            {
                //attempt to add 10 items to the colletion - bloks after 5th
                for(int i=0;i<11;i++)
                {
                    data.Add(i);
                    Console.WriteLine("Data {0} added successfully.", i);
                }
                //indicate we have no more to add
                data.CompleteAdding();
            });

            Console.ReadKey();
            Console.WriteLine("Reading collection.");

            Task.Run(() =>
            {
                while (!data.IsCompleted)
                {
                    try
                    {
                        int v = data.Take();
                        Console.WriteLine("Data {0} taken successfully.", v);

                    }
                    catch (InvalidOperationException)
                    {
                    }
                }
            });
            Console.ReadKey();
        }
    }
}
