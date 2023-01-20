using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator
{
    internal class myElevator
    {
        Random rand = new Random();
        public enum Place { G, S, T1, T2 };
        const int capacity = 1;
        List<Agent> elevator;
        List<Agent> Floors;


        Semaphore semaphore;
        Semaphore floorsemaphore;

        public ManualResetEvent GoHomeSignal { get; private set; }

        public myElevator()
        {;
            semaphore = new Semaphore(capacity, capacity);
            elevator = new List<Agent>();
            Floors= new List<Agent>();
            GoHomeSignal = new ManualResetEvent(false);
        }

        public void getElevator()
        {
            Console.WriteLine("Agents in elevator: ");
            foreach(var e in elevator)
            {
                Console.Write(e);
            }
        }
        public void getFloor()
        {
            int i = 0;
            Console.WriteLine("Agents in floors: ");
            foreach (var e in Floors)
            {
                i++;
                Console.Write(i);
                Console.Write(" ");
            }
        }

        public bool TryEnter(Agent a)
        {
            if (semaphore.WaitOne())
            {
                lock (elevator) elevator.Add(a);
                return true;
            }
            else
                return false;
        }

      

        public void Leave(Agent a)
        {
            lock (elevator) elevator.Remove(a);
            semaphore.Release();
            //if elevator is empty it is automatically going to G floor
            if (elevator.Count == 0)
            {
                Thread.Sleep(200);
                Console.WriteLine("Elevator is going to G Floor");
            }
        }


        public bool FloorGButton(Agent a)
        {
            if (true)
            {
                Floors.Add(a);
                return true;
            }
            else
                return false;
        }

        public void GFloorLeave(Agent a)
        {
            Floors.Remove(a);
            Console.WriteLine("Agent left the floor");
        }

       

        public bool SecurityCheck(Agent a,Place place)
        {
            //G floor acces check
            if (place==Place.G)
            {
                Console.WriteLine("Agent left the elevator");
                return true;
            }

            //S floor access check
            if (place == Place.S)
            {
                if (a.level == Agent.SecurityLevel.Confidential)
                {
                    Console.WriteLine("Access denied");
                    return false;
                }
                else
                {
                    Console.WriteLine("Agent left the elevator");
                    return true;
                }
            }

            //T1 floor access check
            if (place == Place.T1 )
            {
                if (a.level == Agent.SecurityLevel.Confidential)
                {
                    Console.WriteLine("Access denied");
                    return false;
                }
                if (a.level == Agent.SecurityLevel.Secret)
                {
                    Console.WriteLine("Access denied");
                    return false;
                }
                else
                {
                    Console.WriteLine("Agent left the elevator");
                    return true;
                }
            }

            //T2 floor access check
            if (place == Place.T2)
            {
                if (a.level == Agent.SecurityLevel.Confidential)
                {
                    Console.WriteLine("Access denied");
                    return false;
                }
                if (a.level == Agent.SecurityLevel.Secret)
                {
                    Console.WriteLine("Access denied");
                    return false;
                }
                else
                {
                    Console.WriteLine("Agent left the elevator");
                    return true;
                }
            }
            Console.WriteLine("Access denied");
            return false;
        }
        public void Work()
        {
            Console.WriteLine("Area51 is open.");
            Thread.Sleep(1000);
            Console.WriteLine("Area51 is closing.");
            GoHomeSignal.Set();
        }

    }
}
