using Modeling_laba2;
using System;


namespace ModelingLaba2
{
    public class Programm
    {
        static void Main(string[] args)
        {
            Tanker t1 = new Tanker(TankerType.First);
            Tanker t2 = new Tanker(TankerType.Second);
            Pier p = new Pier(1);

            Console.WriteLine(t1.GetInformation());
            Console.WriteLine(t2.GetInformation());
            Console.WriteLine();

            for (int i = 0; i < 60; i++)
            {
                if(i == 0)
                {
                    p.TakeTanker(t1);
                }
                p.UpdateState();
                if(i == 10)
                {
                    p.TakeTanker(t2);
                }
                if (p.Occupation == PierOccupationType.Free)
                {
                    p.TakeTanker(t1);
                }
                if (p.CurrentTanker != null)
                {
                    Console.WriteLine(p.CurrentTanker.GetInformation());
                }
                else
                {
                    Console.WriteLine("Free");
                }
            }

        }
    }
}
