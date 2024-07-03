using UnityEngine;

namespace TamilEncoderTest
{
    public enum TamilFontEncodingTest
    {
        TSCII,
        Unicode_ISCII,
        TACE16
    }

    public sealed class TamilEncodingTest
    {
        /*public void Main(Text text)
        {
            List<string> b = TamilCharater.TamilUnicode;
            List<string> c = TamilCharater.TSCII;
            
            string str = text.text;

            Debug.Log(b.Count);
            Debug.Log(str);
            for (int i = 0; i < b.Count; i++)
                str = str.Replace(b[i], c[i]);

            Debug.Log(str);  
            text.text = str;
        }*/

        public static string Convert(TamilFontEncodingTest oldFontEncode, TamilFontEncodingTest newFontEncode, string text)
        {
            if (newFontEncode == TamilFontEncodingTest.TACE16 || oldFontEncode == TamilFontEncodingTest.TACE16)
                Debug.LogWarning("TACE16 font are not currently support, check for updates or use TSCII font to render tamil text in Unity!");

            return (oldFontEncode, newFontEncode) switch
            {
                (TamilFontEncodingTest.Unicode_ISCII, TamilFontEncodingTest.TSCII) => UnicodeToTSCII(text),
                (TamilFontEncodingTest.TSCII, TamilFontEncodingTest.Unicode_ISCII) => TSCIIToUnicode(text),
                (TamilFontEncodingTest.Unicode_ISCII, TamilFontEncodingTest.Unicode_ISCII) => text,             
                (TamilFontEncodingTest.TSCII, TamilFontEncodingTest.TSCII) => text,
                _ => text
            };
        }

        public static string UnicodeToTSCII(string text)
        {
            string tamilTxt = text;
            for (int i = 0; i < TamilCharaterTest.TamilUnicode.Length; i++)
                tamilTxt = tamilTxt.Replace(TamilCharaterTest.TamilUnicode[i], TamilCharaterTest.TSCII[i]);

            return tamilTxt;
        }

        public static string TSCIIToUnicode(string text)
        {
            string tamilTxt = text;
            for (int i = 0; i < TamilCharaterTest.TamilUnicode.Length; i++)
                tamilTxt = tamilTxt.Replace(TamilCharaterTest.TSCII[i], TamilCharaterTest.TamilUnicode[i]);

            return tamilTxt;
        }

        public static string Convert(string unicode, TamilFontEncodingTest newFontEncode = TamilFontEncodingTest.TSCII)
        {
            return Convert(TamilFontEncodingTest.Unicode_ISCII, newFontEncode, unicode);
        }
    }
}
