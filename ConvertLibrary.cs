﻿namespace StandartLibrary
{
    public static class ConvertLibrary
    {
        public static int Int_Parse(this string str) => Convert.ToInt32(str);

        public static double Double_Parse(this string str) => Convert.ToDouble(str);

        public static double Double_Parse(this char c) => char.GetNumericValue(c);
    }
}
