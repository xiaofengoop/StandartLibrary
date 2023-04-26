namespace StandartLibrary.MyResolutionClass
{
    
    static class Resolution
    {
        public enum ResolutionOptions
        {
            _1920_1080,
            _1680_1050,
            _1600_900,
            _1440_1050,
            _1440_900,
            _1366_768,
            _1360_768,
            _1280_1024,
            _1280_960,
            _1280_800,
            _1280_768,
            _1280_720,
            _1280_600,
            _1152_864,
            _1024_768,
            _800_600
        }

        private static Dictionary<ResolutionOptions, ResolutionSize> keyValuePairs = new()
        {
            {ResolutionOptions._1920_1080, new ResolutionSize(){ Width = 1920, Height = 1080 } },
            {ResolutionOptions._1680_1050, new ResolutionSize(){ Width = 1680, Height = 1050 } },
            {ResolutionOptions._1600_900, new ResolutionSize(){ Width = 1600, Height = 960 } },
            {ResolutionOptions._1440_1050, new ResolutionSize(){ Width = 1440, Height = 1050 } },
            {ResolutionOptions._1440_900, new ResolutionSize(){ Width = 1440, Height = 900 } },
            {ResolutionOptions._1366_768, new ResolutionSize(){ Width = 1366, Height = 768 } },
            {ResolutionOptions._1360_768, new ResolutionSize(){ Width = 1360, Height = 768 } },
            {ResolutionOptions._1280_1024, new ResolutionSize(){ Width = 1280, Height = 1024 } },
            {ResolutionOptions._1280_960, new ResolutionSize(){ Width = 1280, Height = 960 } },
            {ResolutionOptions._1280_800, new ResolutionSize(){ Width = 1280, Height = 800 } },
            {ResolutionOptions._1280_768, new ResolutionSize(){ Width = 1280, Height = 768 } },
            {ResolutionOptions._1280_720, new ResolutionSize(){ Width = 1280, Height = 720 } },
            {ResolutionOptions._1280_600, new ResolutionSize(){ Width = 1280, Height = 600 } },
            {ResolutionOptions._1152_864, new ResolutionSize(){ Width = 1152, Height = 864 } },
            {ResolutionOptions._1024_768, new ResolutionSize(){ Width = 1024, Height = 768 } },
            {ResolutionOptions._800_600, new ResolutionSize(){ Width = 800, Height = 600 } }
        };

        public static ResolutionSize GetResolutionSize(ResolutionOptions option)
        {
            return keyValuePairs[option];
        }
    }

    public class ResolutionSize
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
