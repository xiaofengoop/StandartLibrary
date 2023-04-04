namespace StandartLibrary
{
    public static class Platform
    {
        public static int windows = 0;
        public static int Android = 1;
        public static int iOS = 2;

        public static Dictionary<int, string> PlatformNames = new()
        {
            {0, "windows" },
            {1, "Android" },
            {2, "iOS" }
        };
    }
}
