using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandartLibrary.异常类
{
    public class NoEnumExcept : Exception
    {
        public NoEnumExcept() : base("参数非枚举类型") { }
    }
}
