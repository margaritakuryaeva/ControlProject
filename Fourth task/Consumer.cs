namespace Fourth_task
{
    // Класс Потребитель
    class Consumer
    {
        Store store;
        public Consumer(Store store)
        {
            this.store = store;
        }
        public void run()
        {
            for (int i = 1; i < 6; i++) store.get();
        }
    }
}
