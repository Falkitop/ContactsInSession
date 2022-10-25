using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseX;

namespace NeosDllLoader
{
    internal class Logger
    {
        public static void Log(object input)
        {
            BaseX.UniLog.Log("[DLLLoader ]"+input);
        }
    }
}
