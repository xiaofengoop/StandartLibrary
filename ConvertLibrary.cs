using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandartLibrary
{
    public static class ConvertLibrary
    {
        public static int Int_Parse(this string str) => Convert.ToInt32(str);
    }
}
