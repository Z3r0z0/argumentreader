using ArgumentsLib;
using System;
using ArgumentException = ArgumentMarshalerLib.ArgumentException;

namespace Arguments
{
    class Program
    {
        static void Main(string[] args)
        {
			try
			{
                Argument arg = new Argument(@"C:\Users\lwnmut1\source\repos\ArgumentsExtended\Arguments\Marshaler", "a, b*, c#", args);

                Console.WriteLine($"{arg.GetValue<bool>("a")} {arg.GetValue<string>("b")} {arg.GetValue<int>("c")}");
			}
			catch (ArgumentException ex)
			{
                Console.WriteLine(ex.ErrorMessage());
			}

            Console.ReadKey();
        }
    }
}
