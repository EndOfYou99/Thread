using Elevator;

class Program
{
    static void Main()
    {
        myElevator elevator = new myElevator();
        Agent agent = new Agent(elevator);
        var agentThreads = new List<Thread>();
        List<Agent> agentlist = new List<Agent>();


        Thread barThread = new Thread(elevator.Work);
        barThread.Start();

        for (int i = 0; i < 100; i++)
        {
            Agent a = new Agent(elevator)
            {
                Id = i.ToString()
            };
            agentlist.Add(a);
            var t = new Thread(a.GoWork);
            t.Start();
            agentThreads.Add(t);
        }


        foreach (var t in agentThreads) t.Join();
        Console.WriteLine("Workday is off.");


    }
}
