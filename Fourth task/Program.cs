using System.Threading;

namespace Fourth_task
{
    class Program
    {
        static void Main(string[] args)
        {
            Store store = new Store();
            Producer producer = new Producer(store);
            Consumer consumer = new Consumer(store);
            new Thread(producer.run).Start();
            new Thread(consumer.run).Start();
        }
    }
}
