using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chainedlupine.tuatara
{
    class TuataraException: Exception
    {
        public TuataraException()
        {

        }

        public TuataraException (string msg): base(msg)
        {
            Logger.WriteLineError(string.Format ("!! EXCEPTION: {0}", msg));
        }

        public TuataraException (string msg, Exception inner): base (msg, inner)
        {
            Logger.WriteLineError(string.Format ("!! EXCEPTION: {0}", msg));
        }
    }
}
