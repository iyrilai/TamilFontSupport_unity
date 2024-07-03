using System;

namespace TamilEncoder
{
    public enum TamilFontEncoding
    {
        TSCII,
        Unicode_ISCII,
        TACE16
    }

    public sealed class TamilEncoding
    {
        public static int TotalTamilCharater { get => TamilCharater.TamilUnicode.Length; }

        public static string Convert(TamilFontEncoding currentFontEncoding, TamilFontEncoding newFontEncoding, string text)
        {
            if (currentFontEncoding == newFontEncoding) return text;

            string[] currectChar = GetCharater(currentFontEncoding);
            string[] newChar = GetCharater(newFontEncoding);

            for (int i = 0; i < TotalTamilCharater; i++)
                text = text.Replace(currectChar[i], newChar[i]);

            return text;
        }

        public static string ConvertFromUnicode(string unicodeCharaters, TamilFontEncoding newFontEncode = TamilFontEncoding.TSCII)
        {
            return Convert(TamilFontEncoding.Unicode_ISCII, newFontEncode, unicodeCharaters);
        }

        static string[] GetCharater(TamilFontEncoding encoding)
        {
            return encoding switch
            {
                TamilFontEncoding.Unicode_ISCII => TamilCharater.TamilUnicode,
                TamilFontEncoding.TSCII => TamilCharater.TSCII,
                TamilFontEncoding.TACE16 => TamilCharater.TACE16,
                _ => throw new Exception("Charater Encoding Not Found")
            };
        }




        //Obsolete
        [Obsolete("This is old method. Use TamilEncoding.ConvertFromUnicode()")]
        public static string Convert(string unicodeCharaters)
        {
            return Convert(TamilFontEncoding.Unicode_ISCII, TamilFontEncoding.TSCII, unicodeCharaters);
        }

        [Obsolete("This is old method. Use TamilEncoding.Convert()")]
        public static string UnicodeToTSCII(string text)
        {
            string tamilTxt = text;
            for (int i = 0; i < TotalTamilCharater; i++)
                tamilTxt = tamilTxt.Replace(TamilCharater.TamilUnicode[i], TamilCharater.TSCII[i]);

            return tamilTxt;
        }

        [Obsolete("This is old method. Use TamilEncoding.Convert()")]
        public static string TSCIIToUnicode(string text)
        {
            string tamilTxt = text;
            for (int i = 0; i < TotalTamilCharater; i++)
                tamilTxt = tamilTxt.Replace(TamilCharater.TSCII[i], TamilCharater.TamilUnicode[i]);

            return tamilTxt;
        }
    }
}