using System;
using System.Net;

namespace CovidStat
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(ToInt("176.212.181.60"));
        }
        
        static long ToInt(string addr)
        {
            // careful of sign extension: convert to uint first;
            // unsigned NetworkToHostOrder ought to be provided.
            return (long) (uint) IPAddress.NetworkToHostOrder(
                (int) IPAddress.Parse(addr).Address);
        }
    }
}