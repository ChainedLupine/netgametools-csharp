using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chainedlupine.UPnP
{
    class Logger
    {
        public delegate void DelegateLineWriter(string line);

        public static DelegateLineWriter WriteLine;
        public static DelegateLineWriter WriteLineError;
        public static DelegateLineWriter WriteLineWarn;
    }
}
