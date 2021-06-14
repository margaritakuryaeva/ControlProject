using System;
using System.Threading;

namespace Fourth_task
{
    // Класс Магазин, хранящий произведенные товары
    class Store
    {
        private int product = 0;
        private object locker = new object();
        public void get()
        {
            Monitor.Enter(locker);
            while (product<1)
                try
                {
                    Monitor.Wait(locker);
                }
                catch (Exception)
                {
                }

            product--;
            Console.WriteLine("Покупатель купил 1 товар");
            Console.WriteLine("Товаров на складе: " + product.ToString());
            Monitor.Pulse(locker);
            Monitor.Exit(locker);
        }
        public void put()
        {
            Monitor.Enter(locker);
            while (product >= 3)
                try
                {
                    Monitor.Wait(locker);
                }
                catch (Exception)
                {
                }
            
            product++;
            Console.WriteLine("Производитель добавил 1 товар");
            Console.WriteLine("Товаров на складе: " + product.ToString());
            Monitor.Pulse(locker);
            Monitor.Exit(locker);
        }
    }
}
