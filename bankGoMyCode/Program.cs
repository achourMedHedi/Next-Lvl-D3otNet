using System;
using System.Collections.Generic;
using bankGoMyCode.Account;
using bankGoMyCode.Client;
namespace bankGoMyCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Client<IAccount<int>, int> client = new Client<IAccount<int>, int>(1450,"sfsf");
            Console.WriteLine(client.Cin);
            
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
