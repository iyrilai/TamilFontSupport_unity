﻿using System.Collections.Generic;

namespace TamilEncoderTest
{
    internal sealed class TamilCharaterTest
    {
        readonly static string[] unicode = 
        {
           //Tamil UIR EZHUTHUKAL In Unicode
           "அ", "ஆ", "இ", "ஈ", "உ", "ஊ", "எ", "ஏ","ஐ","ஒ","ஓ", "ஔ",

           //Tamil MEI EZHUTHUKAL In Unicode
           "க்", "ங்", "ச்", "ஞ்", "ட்","ண்","த்","ந்","ப்","ம்","ய்","ர்","ல்","வ்","ழ்","ள்","ற்", "ன்",

           //Tamil UIR MEI EZHUTHUKAL In Unicode
           "கா","கி","கீ","கு","கூ","கெ","கே","கை","கொ","கோ","கௌ",
           "ஙா","ஙி","ஙீ","ஙு","ஙூ","ஙெ","ஙே","ஙை","ஙொ","ஙோ","ஙௌ",
           "சா","சி","சீ","சு","சூ","செ","சே","சை","சொ","சோ","சௌ",
           "ஞா","ஞி","ஞீ","ஞு","ஞூ","ஞெ","ஞே","ஞை","ஞொ","ஞோ","ஞௌ",
           "டா","டி","டீ","டு","டூ","டெ","டே","டை","டொ","டோ","டௌ",
           "ணா","ணி","ணீ","ணு","ணூ","ணெ","ணே","ணை","ணொ","ணோ","ணௌ",
           "தா","தி","தீ","து","தூ","தெ","தே","தை","தொ","தோ","தௌ",
           "நா","நி","நீ","நு","நூ","நெ","நே","நை","நொ","நோ","நௌ",
           "பா","பி","பீ","பு","பூ","பெ","பே","பை","பொ","போ","பௌ",
           "மா","மி","மீ","மு","மூ","மெ","மே","மை","மொ","மோ","மௌ",
           "யா","யி","யீ","யு","யூ","யெ","யே","யை","யொ","யோ","யௌ",
           "ரா","ரி","ரீ","ரு","ரூ","ரெ","ரே","ரை","ரொ","ரோ","ரௌ",
           "லா","லி","லீ","லு","லூ","லெ","லே","லை","லொ","லோ","லௌ",
           "வா","வி","வீ","வு","வூ","வெ","வே","வை","வொ","வோ","வௌ",
           "ழா","ழி","ழீ","ழு","ழூ","ழெ","ழே","ழை","ழொ","ழோ","ழௌ",
           "ளா","ளி","ளீ","ளு","ளூ","ளெ","ளே","ளை","ளொ","ளோ","ளௌ",
           "றா","றி","றீ","று","றூ","றெ","றே","றை","றொ","றோ","றௌ",
           "னா","னி","னீ","னு","னூ","னெ","னே","னை","னொ","னோ","னௌ",

           "க","ங","ச","ஞ","ட", "ண","த","ந","ப", "ம", "ய","ர", "ல","வ","ழ","ள","ற","ன",

           "ஃ", //Aitha ezhuthu in Unicode

           //Tamil enkal(numbers)

           "௦", "௧", "௨", "௫", "௬", "௭", "௮", "௯", "௰", "௲",

           //Vada mozhi Unicode

           "ஸ்ரீ",

           "ஜ்", "ஷ்", "ஸ்", "ஹ்", "க்ஷ்", "ஶ்",

           "ஜா","ஜி","ஜீ","ஜு","ஜூ","ஜெ","ஜே","ஜை","ஜொ","ஜோ","ஜௌ",
           "ஷா","ஷி","ஷீ","ஷு","ஷூ","ஷெ","ஷே","ஷை","ஷொ","ஷோ","ஷௌ",
           "ஸா","ஸி","ஸீ","ஸு","ஸூ","ஸெ","ஸே","ஸை","ஸொ","ஸோ","ஸௌ",
           "ஹா","ஹி","ஹீ","ஹு","ஹூ","ஹெ","ஹே","ஹை","ஹொ","ஹோ","ஹௌ",
           "க்ஷா","க்ஷி","க்ஷீ","க்ஷு","க்ஷூ","க்ஷெ","க்ஷே","க்ஷை","க்ஷொ","க்ஷோ","க்ஷௌ",

           "ஜ","ஷ","ஸ","ஹ","க்ஷ",

        };

        readonly static string[] tscii = 
        {
           //Tamil UIR EZHUTHUKAL In TSCII
           "«", "¬", "þ","®","¯","°","±","²","³","´","µ","¶",

           //Tamil MEI EZHUTHUKAL In TSCII
           "ì", "í", "î", "ï", "ð" ,"ñ", "ò", "ó", "ô", "õ", "ö", "÷", "ø", "ù", "ú", "û", "ü", "ý",

           //Tamil UIR MEI EZHUTHUKAL In TSCII
           "¸¡", "¸¢", "¸£", "Ì", "Ü", "¦¸", "§¸", "¨¸", "¦¸¡", "§¸¡", "¦¸ª",
           "¹¡", "¹¢", "¹£", "ÿ", "›", "¦¹", "§¹", "¨¹", "¦¹¡", "§¹¡", "¦¹ª",
           "º¡", "º¢", "º£", "Í", "Ý", "¦º", "§º", "¨º", "¦º¡", "§º¡", "¦ºª",
           "»¡", "»¢", "»£", "š", "œ", "¦»", "§»", "¨»", "¦»¡", "§»¡", "¦»ª",
           "¼¡",  "Ê",  "Ë", "Î", "Þ", "¦¼", "§¼", "¨¼", "¦¼¡", "§¼¡", "¦¼ª",
           "½¡", "½¢", "½£", "Ï", "ß", "¦½", "§½", "¨½", "¦½¡", "§½¡", "¦½ª",
           "¾¡", "¾¢", "¾£", "Ð", "à", "¦¾", "§¾", "¨¾", "¦¾¡", "§¾¡", "¦¾ª",
           "¿¡", "¿¢", "¿£", "Ñ", "á", "¦¿", "§¿", "¨¿", "¦¿¡", "§¿¡", "¦¿ª",
           "À¡", "À¢", "À£", "Ò", "â", "¦À", "§À", "¨À", "¦À¡", "§À¡", "¦Àª",
           "Á¡", "Á¢", "Á£", "Ó", "ã", "¦Á", "§Á", "¨Á", "¦Á¡", "§Á¡", "¦Áª",
           "Â¡", "Â¢", "Â£", "Ô", "ä", "¦Â", "§Â", "¨Â", "¦Â¡", "§Â¡", "¦Âª",
           "Ã¡", "¡¢", "¡£", "Õ", "å", "¦Ã", "§Ã", "¨Ã", "¦Ã¡", "§Ã¡", "¦Ãª",
           "Ä¡", "Ä¢", "Ä£", "Ö", "æ", "¦Ä", "§Ä", "¨Ä", "¦Ä¡", "§Ä¡", "¦Äª",
           "Å¡", "Å¢", "Å£", "×", "ç", "¦Å", "§Å", "¨Å", "¦Å¡", "§Å¡", "¦Åª",
           "Æ¡", "Æ¢", "Æ£", "Ø", "è", "¦Æ", "§Æ", "¨Æ", "¦Æ¡", "§Æ¡", "¦Æª",
           "Ç¡", "Ç¢", "Ç£", "Ù", "é", "¦Ç", "§Ç", "¨Ç", "¦Ç¡", "§Ç¡", "¦Çª",
           "È¡", "È¢", "È£", "Ú", "ê", "¦È", "§È", "¨È", "¦È¡", "§È¡", "¦Èª",
           "É¡", "É¢", "É£", "Û", "ë", "¦É", "§É", "¨É", "¦É¡", "§É¡", "¦Éª",

           "¸", "¹","º", "»","¼", "½","¾","¿", "À","Á", "Â","Ã", "Ä", "Å", "Æ", "Ç", "È", "É",

           "·", //Aitha ezhuthu TSCII

           //Tamil numbers TSCII
           "0", "¸", "¯","Õ","•","±","«","˜","Â","Ÿ",


           //Vada mozhi TSCII
           "‚", // sri

           "ˆ", "‰", "Š", "‹", "Œ", "‰",

           "ę¡", "ę¢", "ę£", "ę¤", "ę¥", "¦ę", "§ę", "¨ę", "¦ę¡", "§ę¡", "¦ęª",
           "„¡", "„¢", "„£", "„¤", "„¥", "¦„", "§„", "¨„", "¦„¡", "§„¡", "¦„ª",
           "…¡", "…¢", "…£", "…¤", "…¥", "¦…", "§…", "¨…", "¦…¡", "§…¡", "¦…ª",
           "†¡", "†¢", "†£", "†¤", "†¥", "¦†", "§†", "¨†", "¦†¡", "§†¡", "¦†ª",
           "‡¡", "‡¢", "‡£", "‡¤", "‡¥", "¦‡", "§‡", "¨‡", "¦‡¡", "§‡¡", "¦‡ª",

           "ę","„","…","†","‡",

        };

        public static string[] TamilUnicode { get => unicode; }

        public static string[] TSCII { get => tscii; }
    }
}