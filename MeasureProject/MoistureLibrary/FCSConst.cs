using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moisture.Lib.Const
{
    public enum FCSDataServer
    {
        DEFAULT, AQUA, EKPEDIA, FOCUSNET, FAXSERVER, WEIGHT
    }
    public static class FCSConst
    {
        public const int INT_MIN_VALUE = -2147483647;
        public const int INT_MAX_VALUE = 2147483647;
        public const long INT64_MIN_VALUE = -9223372036854775807;
        public const long INT64_MAX_VALUE = 9223372036854775807;

        public static bool IsRunmode { get; set; }

        public static FCSDataServer RunDataServer { get { return FCSDataServer.DEFAULT; } }

        public const string NO_DATA_FOUND = "*NOTFND";
        public const string NO_SELECT = "NOTSLT";
        public const string MO_MILTIPLE_SELECTION = "*NOTMSLT";
        public const string SUCCESS = "*SUCCESS";
    }
}
