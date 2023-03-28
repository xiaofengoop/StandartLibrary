namespace StandartLibrary.MyExceptionClass
{
    public class NoEnumExcept : Exception
    {
        public NoEnumExcept() : base("参数非枚举类型") { }
    }
}
