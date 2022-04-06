class Consumer
{
    Thread thread;

    public Consumer()
    {
        thread = new Thread(Consume);
        thread.Start();
    }

    void Consume()
    {

    Consuming:
        {
            lock (Program.row)
            {
                if (Program.row.Count == 0)
                {
                    goto Consuming;
                }

                else
                {
                    Program.row.Dequeue();
                    Thread.Sleep(1000);
                    goto Consuming;
                }
            }
        }
    }
}

class Producer
{
    Thread thread;

    public Producer()
    {
        thread = new Thread(Produce);
        thread.Start();
    }
    void Produce()
    {

    Producing:
        {
            lock (Program.row)
            {
                if (Program.row.Count >= 100)
                {
                    goto Producing;
                }
                else
                {
                    Random rand = new Random();
                    int insertion = rand.Next(1, 100);

                    Thread.Sleep(1000);
                    Program.row.Enqueue(insertion);
                    goto Producing;
                }
            }
        }
    }
}
class Program
{
    static public Queue<int> row = new Queue<int>();

    static void Summon()
    {
    Summoning:
        {
            lock (row)
            {
                Console.Clear();

                foreach (int item in row)
                {
                    Console.Write(item + " ");
                }

                Console.WriteLine();
                Console.WriteLine("\n\t\t\t Нажмите q для остановки производителей.");
            }

            Thread.Sleep(1000);
            goto Summoning;
        }
    }

    static public void Main()
    {
        Producer producer1 = new Producer();
        Producer producer2 = new Producer();
        Producer producer3 = new Producer();

        Consumer consumer1 = new Consumer();
        Consumer consumer2 = new Consumer();

        Thread launch = new Thread(Summon);
        launch.Start();

        while (true)
        {
            ConsoleKey key = Console.ReadKey().Key;
            if (key == ConsoleKey.Q)
            {
                Environment.Exit(0);
            }
        }
    }
}