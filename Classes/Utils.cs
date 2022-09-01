using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note_Keeper
{
    class Utils
    {
        public static string GetIdPath(int id)
        {
            return Path.Combine("data", id.ToString());
        }

    }
}
