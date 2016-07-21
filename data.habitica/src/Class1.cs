using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trellabit.data.habitica
{
    /// <summary>
    /// Using this as a quick test of NLog in a code library (dll). 
    /// This is not a permanent class.
    /// TODO: Delete this once I have actual code.
    /// </summary>
    public class Class1
    {
        static Logger logger = LogManager.GetCurrentClassLogger();

        public static void LogSomething(string theThing)
        {
            logger.Debug(theThing);
            logger.Warn(theThing);
        }
    }
}
