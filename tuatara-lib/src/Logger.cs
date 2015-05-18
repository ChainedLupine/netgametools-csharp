using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace chainedlupine.tuatara
{
    public class Logger
    {
        public delegate void DelegateLineWriter(string line);

        public static DelegateLineWriter srcWriteLine = null;
        public static DelegateLineWriter srcWriteLineError = null;
        public static DelegateLineWriter srcWriteLineWarn = null;

        public static void WriteLine(string line)
        {
            if (srcWriteLine != null)
                srcWriteLine(line);
            else
                Debug.WriteLine(line);
        }

        public static void WriteLineWarn(string line)
        {
            if (srcWriteLine != null)
                srcWriteLine(line);
            else
                Debug.WriteLine(line);
        }

        public static void WriteLineError(string line)
        {
            if (srcWriteLine != null)
                srcWriteLine(line);
            else
                Debug.WriteLine(line);
        }
    
    }
}
