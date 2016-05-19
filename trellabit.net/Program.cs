using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace trellabit.net
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("{0} {1}",
                Assembly.GetExecutingAssembly().GetName().Name,
                Assembly.GetExecutingAssembly().GetName().Version);
        }
    }
}
