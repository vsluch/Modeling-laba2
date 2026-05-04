using Modeling_laba2;
using System;


namespace ModelingLaba2
{
    public class Programm
    {
        static void Main(string[] args)
        {
            Port port = new Port();

            for(int i = 0; i < port._hoursInYear; i++)
            {
                port.Hour();
            }
            port.PrintReport();

            Port port2 = new Port();
            for (int i = 0; i < port2._hoursInYear; i++)
            {
                port2.Hour();
            }
            port2.PrintReport();
        }
    }
}
