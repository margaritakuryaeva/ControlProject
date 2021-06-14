namespace Fourth_task
{
    // класс Производитель
    class Producer 
    {
        Store store;
        public Producer(Store store)
        {
            this.store = store;
        }
        public void run()
        {
            for (int i = 1; i < 6; i++) store.put();
        }
    }
}
