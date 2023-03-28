namespace StandartLibrary.MyExceptionClass
{
    public enum Language
    {
        ZH_CN,
        EN_WW
    }

    public class NoFindHandleExcept : ApplicationException
    {
        private static string _zh_language = "没有发现应用程序";
        private static string _en_language = "No found the app!";

        private static Dictionary<Language, string> msg = new()
        {
            {Language.ZH_CN, _zh_language },
            {Language.EN_WW, _en_language }
        };

        public NoFindHandleExcept(Language language) : base(msg[language]) { }
    }
}
