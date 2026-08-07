using System;

namespace Iyrilai.TamilEncoder
{
    public sealed class TamilEncoding
    {
        public static int TotalTamilCharater => TamilCharater.TamilUnicode.Length;

        public static string Convert(string text, TamilFontEncoding currentFontEncoding, TamilFontEncoding newFontEncoding)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            if (currentFontEncoding == newFontEncoding) return text;

            string[] currectChar = GetCharSet(currentFontEncoding);
            string[] newChar = GetCharSet(newFontEncoding);

            for (int i = 0; i < TotalTamilCharater; i++)
                text = text.Replace(currectChar[i], newChar[i]);

            return text;
        }

        public static string ConvertFromUnicode(string unicodeCharaters, TamilFontEncoding newFontEncode)
        {
            return Convert(unicodeCharaters, TamilFontEncoding.Unicode, newFontEncode);
        }

        public static string[] GetCharSet(TamilFontEncoding encoding)
        {
            return encoding switch
            {
                TamilFontEncoding.Unicode => TamilCharater.TamilUnicode,
                TamilFontEncoding.TSCII => TamilCharater.TSCII,
                TamilFontEncoding.TACE16 => TamilCharater.TACE16,
                _ => throw new Exception("Charater Encoding Not Found")
            };
        }
    }
}