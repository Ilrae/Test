using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moisture.Lib
{
    /* Helper의 종류
     *  Shelper : Session Infomation Helper
     *  Chelper : Running Cursor Helper
     *  Ohelper : OpenAtTab Helper
     *  Fhelper : Form Layout Helper
     *  Rhelper : Run Helper
     *  Mhelper : Message Helper
     *  Lhelper : Multi-Lang Helper
     *  Uhelper : Result Helper
     *  Xhelper : Configration Helper
     *  Phelper : Print Helper
     *  Ehelper : Excel Helper
     *  Dhelper : Data Helper
     */

   

    public static class SHelper
    {
        public static bool IsTest { get; set; }
        public static string UserID { get; set; }
        public static string ServiceRoot { get; set; }
        public static string UserName { get; set; }
    }
}
