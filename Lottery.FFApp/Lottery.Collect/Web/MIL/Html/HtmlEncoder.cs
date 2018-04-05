// Decompiled with JetBrains decompiler
// Type: Web.MIL.Html.HtmlEncoder
// Assembly: Lottery.Collect, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 916E4E87-E8A0-4A21-8438-E89468303682
// Assembly location: C:\Users\usd\Desktop\bin\bin\Lottery.Collect.dll

using System.IO;
using System.Text;

namespace Web.MIL.Html
{
    public abstract class HtmlEncoder
    {
        public static string DecodeValue(string value)
        {
            StringBuilder stringBuilder1 = new StringBuilder();
            StringReader stringReader = new StringReader(value);
            int num1 = stringReader.Read();
            while (num1 != -1)
            {
                StringBuilder stringBuilder2 = new StringBuilder();
                for (; num1 != 38 && num1 != -1; num1 = stringReader.Read())
                    stringBuilder2.Append((char)num1);
                stringBuilder1.Append(stringBuilder2.ToString());
                if (num1 == 38)
                {
                    StringBuilder stringBuilder3 = new StringBuilder();
                    for (; num1 != 59 && num1 != -1; num1 = stringReader.Read())
                        stringBuilder3.Append((char)num1);
                    if (num1 == 59)
                    {
                        num1 = stringReader.Read();
                        stringBuilder3.Append(';');
                        if ((int)stringBuilder3[1] == 35)
                        {
                            try
                            {
                                int num2 = int.Parse(stringBuilder3.ToString().Substring(2, stringBuilder3.Length - 3));
                                stringBuilder1.Append((char)num2);
                            }
                            catch
                            {
                            }
                        }
                        else
                        {
                            switch (stringBuilder3.ToString())
                            {
                                case "&lt;":
                                    stringBuilder1.Append("<");
                                    break;
                                case "&gt;":
                                    stringBuilder1.Append(">");
                                    break;
                                case "&quot;":
                                    stringBuilder1.Append("\"");
                                    break;
                                case "&amp;":
                                    stringBuilder1.Append("&");
                                    break;
                                case "&Aacute;":
                                    stringBuilder1.Append('Á');
                                    break;
                                case "&aacute;":
                                    stringBuilder1.Append('á');
                                    break;
                                case "&Acirc;":
                                    stringBuilder1.Append('Â');
                                    break;
                                case "&acirc;":
                                    stringBuilder1.Append('â');
                                    break;
                                case "&acute;":
                                    stringBuilder1.Append('´');
                                    break;
                                case "&AElig;":
                                    stringBuilder1.Append('Æ');
                                    break;
                                case "&aelig;":
                                    stringBuilder1.Append('æ');
                                    break;
                                case "&Agrave;":
                                    stringBuilder1.Append('À');
                                    break;
                                case "&agrave;":
                                    stringBuilder1.Append('à');
                                    break;
                                case "&alefsym;":
                                    stringBuilder1.Append('ℵ');
                                    break;
                                case "&Alpha;":
                                    stringBuilder1.Append('Α');
                                    break;
                                case "&alpha;":
                                    stringBuilder1.Append('α');
                                    break;
                                case "&and;":
                                    stringBuilder1.Append('∧');
                                    break;
                                case "&ang;":
                                    stringBuilder1.Append('∠');
                                    break;
                                case "&Aring;":
                                    stringBuilder1.Append('Å');
                                    break;
                                case "&aring;":
                                    stringBuilder1.Append('å');
                                    break;
                                case "&asymp;":
                                    stringBuilder1.Append('≈');
                                    break;
                                case "&Atilde;":
                                    stringBuilder1.Append('Ã');
                                    break;
                                case "&atilde;":
                                    stringBuilder1.Append('ã');
                                    break;
                                case "&Auml;":
                                    stringBuilder1.Append('Ä');
                                    break;
                                case "&auml;":
                                    stringBuilder1.Append('ä');
                                    break;
                                case "&bdquo;":
                                    stringBuilder1.Append('„');
                                    break;
                                case "&Beta;":
                                    stringBuilder1.Append('Β');
                                    break;
                                case "&beta;":
                                    stringBuilder1.Append('β');
                                    break;
                                case "&brvbar;":
                                    stringBuilder1.Append('¦');
                                    break;
                                case "&bull;":
                                    stringBuilder1.Append('•');
                                    break;
                                case "&cap;":
                                    stringBuilder1.Append('∩');
                                    break;
                                case "&Ccedil;":
                                    stringBuilder1.Append('Ç');
                                    break;
                                case "&ccedil;":
                                    stringBuilder1.Append('ç');
                                    break;
                                case "&cedil;":
                                    stringBuilder1.Append('¸');
                                    break;
                                case "&cent;":
                                    stringBuilder1.Append('¢');
                                    break;
                                case "&Chi;":
                                    stringBuilder1.Append('Χ');
                                    break;
                                case "&chi;":
                                    stringBuilder1.Append('χ');
                                    break;
                                case "&circ;":
                                    stringBuilder1.Append('ˆ');
                                    break;
                                case "&clubs;":
                                    stringBuilder1.Append('♣');
                                    break;
                                case "&cong;":
                                    stringBuilder1.Append('≅');
                                    break;
                                case "&copy;":
                                    stringBuilder1.Append('©');
                                    break;
                                case "&crarr;":
                                    stringBuilder1.Append('↵');
                                    break;
                                case "&cup;":
                                    stringBuilder1.Append('∪');
                                    break;
                                case "&curren;":
                                    stringBuilder1.Append('¤');
                                    break;
                                case "&dagger;":
                                    stringBuilder1.Append('†');
                                    break;
                                case "&Dagger;":
                                    stringBuilder1.Append('‡');
                                    break;
                                case "&darr;":
                                    stringBuilder1.Append('↓');
                                    break;
                                case "&dArr;":
                                    stringBuilder1.Append('⇓');
                                    break;
                                case "&deg;":
                                    stringBuilder1.Append('°');
                                    break;
                                case "&Delta;":
                                    stringBuilder1.Append('Δ');
                                    break;
                                case "&delta;":
                                    stringBuilder1.Append('δ');
                                    break;
                                case "&diams;":
                                    stringBuilder1.Append('♦');
                                    break;
                                case "&divide;":
                                    stringBuilder1.Append('÷');
                                    break;
                                case "&Eacute;":
                                    stringBuilder1.Append('É');
                                    break;
                                case "&eacute;":
                                    stringBuilder1.Append('é');
                                    break;
                                case "&Ecirc;":
                                    stringBuilder1.Append('Ê');
                                    break;
                                case "&ecirc;":
                                    stringBuilder1.Append('ê');
                                    break;
                                case "&Egrave;":
                                    stringBuilder1.Append('È');
                                    break;
                                case "&egrave;":
                                    stringBuilder1.Append('è');
                                    break;
                                case "&empty;":
                                    stringBuilder1.Append('∅');
                                    break;
                                case "&emsp;":
                                    stringBuilder1.Append(' ');
                                    break;
                                case "&Epsilon;":
                                    stringBuilder1.Append('Ε');
                                    break;
                                case "&epsilon;":
                                    stringBuilder1.Append('ε');
                                    break;
                                case "&equiv;":
                                    stringBuilder1.Append('≡');
                                    break;
                                case "&Eta;":
                                    stringBuilder1.Append('Η');
                                    break;
                                case "&eta;":
                                    stringBuilder1.Append('η');
                                    break;
                                case "&ETH;":
                                    stringBuilder1.Append('Ð');
                                    break;
                                case "&eth;":
                                    stringBuilder1.Append('ð');
                                    break;
                                case "&Euml;":
                                    stringBuilder1.Append('Ë');
                                    break;
                                case "&euml;":
                                    stringBuilder1.Append('ë');
                                    break;
                                case "&euro;":
                                    stringBuilder1.Append('\x0080');
                                    break;
                                case "&exist;":
                                    stringBuilder1.Append('∃');
                                    break;
                                case "&fnof;":
                                    stringBuilder1.Append('ƒ');
                                    break;
                                case "&forall;":
                                    stringBuilder1.Append('∀');
                                    break;
                                case "&frac12;":
                                    stringBuilder1.Append('\x00BD');
                                    break;
                                case "&frac14;":
                                    stringBuilder1.Append('\x00BC');
                                    break;
                                case "&frac34;":
                                    stringBuilder1.Append('\x00BE');
                                    break;
                                case "&fras1;":
                                    stringBuilder1.Append('⁄');
                                    break;
                                case "&Gamma;":
                                    stringBuilder1.Append('Γ');
                                    break;
                                case "&gamma;":
                                    stringBuilder1.Append('γ');
                                    break;
                                case "&ge;":
                                    stringBuilder1.Append('≥');
                                    break;
                                case "&harr;":
                                    stringBuilder1.Append('↔');
                                    break;
                                case "&hArr;":
                                    stringBuilder1.Append('⇔');
                                    break;
                                case "&hearts;":
                                    stringBuilder1.Append('♥');
                                    break;
                                case "&hellip;":
                                    stringBuilder1.Append('…');
                                    break;
                                case "&Iacute;":
                                    stringBuilder1.Append('Í');
                                    break;
                                case "&iacute;":
                                    stringBuilder1.Append('í');
                                    break;
                                case "&Icirc;":
                                    stringBuilder1.Append('Î');
                                    break;
                                case "&icirc;":
                                    stringBuilder1.Append('î');
                                    break;
                                case "&iexcl;":
                                    stringBuilder1.Append('¡');
                                    break;
                                case "&Igrave;":
                                    stringBuilder1.Append('Ì');
                                    break;
                                case "&igrave;":
                                    stringBuilder1.Append('ì');
                                    break;
                                case "&image;":
                                    stringBuilder1.Append('ℑ');
                                    break;
                                case "&infin;":
                                    stringBuilder1.Append('∞');
                                    break;
                                case "&int;":
                                    stringBuilder1.Append('∫');
                                    break;
                                case "&Iota;":
                                    stringBuilder1.Append('Ι');
                                    break;
                                case "&iota;":
                                    stringBuilder1.Append('ι');
                                    break;
                                case "&iquest;":
                                    stringBuilder1.Append('¿');
                                    break;
                                case "&isin;":
                                    stringBuilder1.Append('∈');
                                    break;
                                case "&Iuml;":
                                    stringBuilder1.Append('Ï');
                                    break;
                                case "&iuml;":
                                    stringBuilder1.Append('ï');
                                    break;
                                case "&Kappa;":
                                    stringBuilder1.Append('Κ');
                                    break;
                                case "&kappa;":
                                    stringBuilder1.Append('κ');
                                    break;
                                case "&Lambda;":
                                    stringBuilder1.Append('Λ');
                                    break;
                                case "&lambda;":
                                    stringBuilder1.Append('λ');
                                    break;
                                case "&lang;":
                                    stringBuilder1.Append('〈');
                                    break;
                                case "&laquo;":
                                    stringBuilder1.Append('«');
                                    break;
                                case "&larr;":
                                    stringBuilder1.Append('←');
                                    break;
                                case "&lArr;":
                                    stringBuilder1.Append('⇐');
                                    break;
                                case "&lceil;":
                                    stringBuilder1.Append('⌈');
                                    break;
                                case "&ldquo;":
                                    stringBuilder1.Append('“');
                                    break;
                                case "&le;":
                                    stringBuilder1.Append('≤');
                                    break;
                                case "&lfloor;":
                                    stringBuilder1.Append('⌊');
                                    break;
                                case "&lowast;":
                                    stringBuilder1.Append('∗');
                                    break;
                                case "&loz;":
                                    stringBuilder1.Append('◊');
                                    break;
                                case "&lrm;":
                                    stringBuilder1.Append('\x200E');
                                    break;
                                case "&lsaquo;":
                                    stringBuilder1.Append('‹');
                                    break;
                                case "&lsquo;":
                                    stringBuilder1.Append('‘');
                                    break;
                                case "&macr;":
                                    stringBuilder1.Append('¯');
                                    break;
                                case "&mdash;":
                                    stringBuilder1.Append('—');
                                    break;
                                case "&micro;":
                                    stringBuilder1.Append('µ');
                                    break;
                                case "&middot;":
                                    stringBuilder1.Append('·');
                                    break;
                                case "&minus;":
                                    stringBuilder1.Append('−');
                                    break;
                                case "&Mu;":
                                    stringBuilder1.Append('Μ');
                                    break;
                                case "&mu;":
                                    stringBuilder1.Append('μ');
                                    break;
                                case "&nabla;":
                                    stringBuilder1.Append('∇');
                                    break;
                                case "&nbsp;":
                                    stringBuilder1.Append(' ');
                                    break;
                                case "&ndash;":
                                    stringBuilder1.Append('–');
                                    break;
                                case "&ne;":
                                    stringBuilder1.Append('≠');
                                    break;
                                case "&ni;":
                                    stringBuilder1.Append('∋');
                                    break;
                                case "&not;":
                                    stringBuilder1.Append('¬');
                                    break;
                                case "&notin;":
                                    stringBuilder1.Append('∉');
                                    break;
                                case "&nsub;":
                                    stringBuilder1.Append('⊄');
                                    break;
                                case "&Ntilde;":
                                    stringBuilder1.Append('Ñ');
                                    break;
                                case "&ntilde;":
                                    stringBuilder1.Append('ñ');
                                    break;
                                case "&Nu;":
                                    stringBuilder1.Append('Ν');
                                    break;
                                case "&nu;":
                                    stringBuilder1.Append('ν');
                                    break;
                                case "&Oacute;":
                                    stringBuilder1.Append('Ó');
                                    break;
                                case "&oacute;":
                                    stringBuilder1.Append('ó');
                                    break;
                                case "&Ocirc;":
                                    stringBuilder1.Append('Ô');
                                    break;
                                case "&ocirc;":
                                    stringBuilder1.Append('ô');
                                    break;
                                case "&OElig;":
                                    stringBuilder1.Append('Œ');
                                    break;
                                case "&oelig;":
                                    stringBuilder1.Append('œ');
                                    break;
                                case "&Ograve;":
                                    stringBuilder1.Append('Ò');
                                    break;
                                case "&ograve;":
                                    stringBuilder1.Append('ò');
                                    break;
                                case "&oline;":
                                    stringBuilder1.Append('‾');
                                    break;
                                case "&Omega;":
                                    stringBuilder1.Append('Ω');
                                    break;
                                case "&omega;":
                                    stringBuilder1.Append('ω');
                                    break;
                                case "&Omicron;":
                                    stringBuilder1.Append('Ο');
                                    break;
                                case "&omicron;":
                                    stringBuilder1.Append('ο');
                                    break;
                                case "&oplus;":
                                    stringBuilder1.Append('⊕');
                                    break;
                                case "&or;":
                                    stringBuilder1.Append('∨');
                                    break;
                                case "&ordf;":
                                    stringBuilder1.Append('ª');
                                    break;
                                case "&ordm;":
                                    stringBuilder1.Append('º');
                                    break;
                                case "&Oslash;":
                                    stringBuilder1.Append('Ø');
                                    break;
                                case "&oslash;":
                                    stringBuilder1.Append('ø');
                                    break;
                                case "&Otilde;":
                                    stringBuilder1.Append('Õ');
                                    break;
                                case "&otilde;":
                                    stringBuilder1.Append('õ');
                                    break;
                                case "&otimes;":
                                    stringBuilder1.Append('⊗');
                                    break;
                                case "&Ouml;":
                                    stringBuilder1.Append('Ö');
                                    break;
                                case "&ouml;":
                                    stringBuilder1.Append('ö');
                                    break;
                                case "&para;":
                                    stringBuilder1.Append('¶');
                                    break;
                                case "&part;":
                                    stringBuilder1.Append('∂');
                                    break;
                                case "&permil;":
                                    stringBuilder1.Append('‰');
                                    break;
                                case "&perp;":
                                    stringBuilder1.Append('⊥');
                                    break;
                                case "&Phi;":
                                    stringBuilder1.Append('Φ');
                                    break;
                                case "&phi;":
                                    stringBuilder1.Append('φ');
                                    break;
                                case "&Pi;":
                                    stringBuilder1.Append('Π');
                                    break;
                                case "&pi;":
                                    stringBuilder1.Append('π');
                                    break;
                                case "&piv;":
                                    stringBuilder1.Append('ϖ');
                                    break;
                                case "&plusmn;":
                                    stringBuilder1.Append('±');
                                    break;
                                case "&pound;":
                                    stringBuilder1.Append('£');
                                    break;
                                case "&prime;":
                                    stringBuilder1.Append('′');
                                    break;
                                case "&Prime;":
                                    stringBuilder1.Append('″');
                                    break;
                                case "&prod;":
                                    stringBuilder1.Append('∏');
                                    break;
                                case "&prop;":
                                    stringBuilder1.Append('∝');
                                    break;
                                case "&Psi;":
                                    stringBuilder1.Append('Ψ');
                                    break;
                                case "&psi;":
                                    stringBuilder1.Append('ψ');
                                    break;
                                case "&radic;":
                                    stringBuilder1.Append('√');
                                    break;
                                case "&rang;":
                                    stringBuilder1.Append('〉');
                                    break;
                                case "&raquo;":
                                    stringBuilder1.Append('»');
                                    break;
                                case "&rarr;":
                                    stringBuilder1.Append('→');
                                    break;
                                case "&rArr;":
                                    stringBuilder1.Append('⇒');
                                    break;
                                case "&rceil;":
                                    stringBuilder1.Append('⌉');
                                    break;
                                case "&rdquo;":
                                    stringBuilder1.Append('”');
                                    break;
                                case "&real;":
                                    stringBuilder1.Append('ℜ');
                                    break;
                                case "&reg;":
                                    stringBuilder1.Append('®');
                                    break;
                                case "&rfloor;":
                                    stringBuilder1.Append('⌋');
                                    break;
                                case "&Rho;":
                                    stringBuilder1.Append('Ρ');
                                    break;
                                case "&rho;":
                                    stringBuilder1.Append('ρ');
                                    break;
                                case "&rlm;":
                                    stringBuilder1.Append('\x200F');
                                    break;
                                case "&rsaquo;":
                                    stringBuilder1.Append('›');
                                    break;
                                case "&rsquo;":
                                    stringBuilder1.Append('’');
                                    break;
                                case "&sbquo;":
                                    stringBuilder1.Append('‚');
                                    break;
                                case "&Scaron;":
                                    stringBuilder1.Append('Š');
                                    break;
                                case "&scaron;":
                                    stringBuilder1.Append('š');
                                    break;
                                case "&sdot;":
                                    stringBuilder1.Append('⋅');
                                    break;
                                case "&sect;":
                                    stringBuilder1.Append('§');
                                    break;
                                case "&shy;":
                                    stringBuilder1.Append('\x00AD');
                                    break;
                                case "&Sigma;":
                                    stringBuilder1.Append('Σ');
                                    break;
                                case "&sigma;":
                                    stringBuilder1.Append('σ');
                                    break;
                                case "&sigmaf;":
                                    stringBuilder1.Append('ς');
                                    break;
                                case "&sim;":
                                    stringBuilder1.Append('∼');
                                    break;
                                case "&spades;":
                                    stringBuilder1.Append('♠');
                                    break;
                                case "&sub;":
                                    stringBuilder1.Append('⊂');
                                    break;
                                case "&sube;":
                                    stringBuilder1.Append('⊆');
                                    break;
                                case "&sum;":
                                    stringBuilder1.Append('∑');
                                    break;
                                case "&sup;":
                                    stringBuilder1.Append('⊃');
                                    break;
                                case "&sup1;":
                                    stringBuilder1.Append('\x00B9');
                                    break;
                                case "&sup2;":
                                    stringBuilder1.Append('\x00B2');
                                    break;
                                case "&sup3;":
                                    stringBuilder1.Append('\x00B3');
                                    break;
                                case "&supe;":
                                    stringBuilder1.Append('⊇');
                                    break;
                                case "&szlig;":
                                    stringBuilder1.Append('ß');
                                    break;
                                case "&Tau;":
                                    stringBuilder1.Append('Τ');
                                    break;
                                case "&tau;":
                                    stringBuilder1.Append('τ');
                                    break;
                                case "&there4;":
                                    stringBuilder1.Append('∴');
                                    break;
                                case "&Theta;":
                                    stringBuilder1.Append('Θ');
                                    break;
                                case "&theta;":
                                    stringBuilder1.Append('θ');
                                    break;
                                case "&thetasym;":
                                    stringBuilder1.Append('ϑ');
                                    break;
                                case "&thinsp;":
                                    stringBuilder1.Append(' ');
                                    break;
                                case "&THORN;":
                                    stringBuilder1.Append('Þ');
                                    break;
                                case "&thorn;":
                                    stringBuilder1.Append('þ');
                                    break;
                                case "&tilde;":
                                    stringBuilder1.Append('˜');
                                    break;
                                case "&times;":
                                    stringBuilder1.Append('×');
                                    break;
                                case "&trade;":
                                    stringBuilder1.Append('™');
                                    break;
                                case "&Uacute;":
                                    stringBuilder1.Append('Ú');
                                    break;
                                case "&uacute;":
                                    stringBuilder1.Append('ú');
                                    break;
                                case "&uarr;":
                                    stringBuilder1.Append('↑');
                                    break;
                                case "&uArr;":
                                    stringBuilder1.Append('⇑');
                                    break;
                                case "&Ucirc;":
                                    stringBuilder1.Append('Û');
                                    break;
                                case "&ucirc;":
                                    stringBuilder1.Append('û');
                                    break;
                                case "&Ugrave;":
                                    stringBuilder1.Append('Ù');
                                    break;
                                case "&ugrave;":
                                    stringBuilder1.Append('ù');
                                    break;
                                case "&uml;":
                                    stringBuilder1.Append('¨');
                                    break;
                                case "&upsih;":
                                    stringBuilder1.Append('ϒ');
                                    break;
                                case "&Upsilon;":
                                    stringBuilder1.Append('Υ');
                                    break;
                                case "&upsilon;":
                                    stringBuilder1.Append('υ');
                                    break;
                                case "&Uuml;":
                                    stringBuilder1.Append('Ü');
                                    break;
                                case "&uuml;":
                                    stringBuilder1.Append('ü');
                                    break;
                                case "&weierp;":
                                    stringBuilder1.Append('℘');
                                    break;
                                case "&Xi;":
                                    stringBuilder1.Append('Ξ');
                                    break;
                                case "&xi;":
                                    stringBuilder1.Append('ξ');
                                    break;
                                case "&Yacute;":
                                    stringBuilder1.Append('Ý');
                                    break;
                                case "&yacute;":
                                    stringBuilder1.Append('ý');
                                    break;
                                case "&yen;":
                                    stringBuilder1.Append('¥');
                                    break;
                                case "&Yuml;":
                                    stringBuilder1.Append('Ÿ');
                                    break;
                                case "&yuml;":
                                    stringBuilder1.Append('ÿ');
                                    break;
                                case "&Zeta;":
                                    stringBuilder1.Append('Ζ');
                                    break;
                                case "&zeta;":
                                    stringBuilder1.Append('ζ');
                                    break;
                                case "&zwj;":
                                    stringBuilder1.Append('\x200D');
                                    break;
                                case "&zwnj;":
                                    stringBuilder1.Append('\x200C');
                                    break;
                                default:
                                    stringBuilder1.Append(stringBuilder3.ToString());
                                    break;
                            }
                        }
                    }
                    else
                        stringBuilder1.Append(stringBuilder3.ToString());
                }
            }
            return stringBuilder1.ToString();
        }

        public static string EncodeValue(string value)
        {
            StringBuilder stringBuilder = new StringBuilder();
            StringReader stringReader = new StringReader(value);
            int num = stringReader.Read();
            while (true)
            {
                switch (num)
                {
                    case -1:
                        goto label_258;
                    case 9001:
                        stringBuilder.Append("&lang;");
                        break;
                    case 9002:
                        stringBuilder.Append("&rang;");
                        break;
                    case 9674:
                        stringBuilder.Append("&loz;");
                        break;
                    case 9824:
                        stringBuilder.Append("&spades;");
                        break;
                    case 9827:
                        stringBuilder.Append("&clubs;");
                        break;
                    case 9829:
                        stringBuilder.Append("&hearts;");
                        break;
                    case 9830:
                        stringBuilder.Append("&diams;");
                        break;
                    case 8869:
                        stringBuilder.Append("&perp;");
                        break;
                    case 8901:
                        stringBuilder.Append("&sdot;");
                        break;
                    case 8968:
                        stringBuilder.Append("&lceil;");
                        break;
                    case 8969:
                        stringBuilder.Append("&rceil;");
                        break;
                    case 8970:
                        stringBuilder.Append("&lfloor;");
                        break;
                    case 8971:
                        stringBuilder.Append("&rfloor;");
                        break;
                    case 8800:
                        stringBuilder.Append("&ne;");
                        break;
                    case 8801:
                        stringBuilder.Append("&equiv;");
                        break;
                    case 8804:
                        stringBuilder.Append("&le;");
                        break;
                    case 8805:
                        stringBuilder.Append("&ge;");
                        break;
                    case 8834:
                        stringBuilder.Append("&sub;");
                        break;
                    case 8835:
                        stringBuilder.Append("&sup;");
                        break;
                    case 8836:
                        stringBuilder.Append("&nsub;");
                        break;
                    case 8838:
                        stringBuilder.Append("&sube;");
                        break;
                    case 8839:
                        stringBuilder.Append("&supe;");
                        break;
                    case 8853:
                        stringBuilder.Append("&oplus;");
                        break;
                    case 8855:
                        stringBuilder.Append("&otimes;");
                        break;
                    case 8773:
                        stringBuilder.Append("&cong;");
                        break;
                    case 8776:
                        stringBuilder.Append("&asymp;");
                        break;
                    case 8743:
                        stringBuilder.Append("&and;");
                        break;
                    case 8744:
                        stringBuilder.Append("&or;");
                        break;
                    case 8745:
                        stringBuilder.Append("&cap;");
                        break;
                    case 8746:
                        stringBuilder.Append("&cup;");
                        break;
                    case 8747:
                        stringBuilder.Append("&int;");
                        break;
                    case 8756:
                        stringBuilder.Append("&there4;");
                        break;
                    case 8764:
                        stringBuilder.Append("&sim;");
                        break;
                    case 8727:
                        stringBuilder.Append("&lowast;");
                        break;
                    case 8730:
                        stringBuilder.Append("&radic;");
                        break;
                    case 8733:
                        stringBuilder.Append("&prop;");
                        break;
                    case 8734:
                        stringBuilder.Append("&infin;");
                        break;
                    case 8736:
                        stringBuilder.Append("&ang;");
                        break;
                    case 8629:
                        stringBuilder.Append("&crarr;");
                        break;
                    case 8656:
                        stringBuilder.Append("&lArr;");
                        break;
                    case 8657:
                        stringBuilder.Append("&uArr;");
                        break;
                    case 8658:
                        stringBuilder.Append("&rArr;");
                        break;
                    case 8659:
                        stringBuilder.Append("&dArr;");
                        break;
                    case 8660:
                        stringBuilder.Append("&hArr;");
                        break;
                    case 8704:
                        stringBuilder.Append("&forall;");
                        break;
                    case 8706:
                        stringBuilder.Append("&part;");
                        break;
                    case 8707:
                        stringBuilder.Append("&exist;");
                        break;
                    case 8709:
                        stringBuilder.Append("&empty;");
                        break;
                    case 8711:
                        stringBuilder.Append("&nabla;");
                        break;
                    case 8712:
                        stringBuilder.Append("&isin;");
                        break;
                    case 8713:
                        stringBuilder.Append("&notin;");
                        break;
                    case 8715:
                        stringBuilder.Append("&ni;");
                        break;
                    case 8719:
                        stringBuilder.Append("&prod;");
                        break;
                    case 8721:
                        stringBuilder.Append("&sum;");
                        break;
                    case 8722:
                        stringBuilder.Append("&minus;");
                        break;
                    case 8501:
                        stringBuilder.Append("&alefsym;");
                        break;
                    case 8592:
                        stringBuilder.Append("&larr;");
                        break;
                    case 8593:
                        stringBuilder.Append("&uarr;");
                        break;
                    case 8594:
                        stringBuilder.Append("&rarr;");
                        break;
                    case 8595:
                        stringBuilder.Append("&darr;");
                        break;
                    case 8596:
                        stringBuilder.Append("&harr;");
                        break;
                    case 8472:
                        stringBuilder.Append("&weierp;");
                        break;
                    case 8476:
                        stringBuilder.Append("&real;");
                        break;
                    case 8482:
                        stringBuilder.Append("&trade;");
                        break;
                    case 8260:
                        stringBuilder.Append("&fras1;");
                        break;
                    case 8465:
                        stringBuilder.Append("&image;");
                        break;
                    case 8240:
                        stringBuilder.Append("&permil;");
                        break;
                    case 8242:
                        stringBuilder.Append("&prime;");
                        break;
                    case 8243:
                        stringBuilder.Append("&Prime;");
                        break;
                    case 8249:
                        stringBuilder.Append("&lsaquo;");
                        break;
                    case 8250:
                        stringBuilder.Append("&rsaquo;");
                        break;
                    case 8254:
                        stringBuilder.Append("&oline;");
                        break;
                    case 8195:
                        stringBuilder.Append("&emsp;");
                        break;
                    case 8201:
                        stringBuilder.Append("&thinsp;");
                        break;
                    case 8204:
                        stringBuilder.Append("&zwnj;");
                        break;
                    case 8205:
                        stringBuilder.Append("&zwj;");
                        break;
                    case 8206:
                        stringBuilder.Append("&lrm;");
                        break;
                    case 8207:
                        stringBuilder.Append("&rlm;");
                        break;
                    case 8211:
                        stringBuilder.Append("&ndash;");
                        break;
                    case 8212:
                        stringBuilder.Append("&mdash;");
                        break;
                    case 8216:
                        stringBuilder.Append("&lsquo;");
                        break;
                    case 8217:
                        stringBuilder.Append("&rsquo;");
                        break;
                    case 8218:
                        stringBuilder.Append("&sbquo;");
                        break;
                    case 8220:
                        stringBuilder.Append("&ldquo;");
                        break;
                    case 8221:
                        stringBuilder.Append("&rdquo;");
                        break;
                    case 8222:
                        stringBuilder.Append("&bdquo;");
                        break;
                    case 8224:
                        stringBuilder.Append("&dagger;");
                        break;
                    case 8225:
                        stringBuilder.Append("&Dagger;");
                        break;
                    case 8226:
                        stringBuilder.Append("&bull;");
                        break;
                    case 8230:
                        stringBuilder.Append("&hellip;");
                        break;
                    case 710:
                        stringBuilder.Append("&circ;");
                        break;
                    case 732:
                        stringBuilder.Append("&tilde;");
                        break;
                    case 913:
                        stringBuilder.Append("&Alpha;");
                        break;
                    case 914:
                        stringBuilder.Append("&Beta;");
                        break;
                    case 915:
                        stringBuilder.Append("&Gamma;");
                        break;
                    case 916:
                        stringBuilder.Append("&Delta;");
                        break;
                    case 917:
                        stringBuilder.Append("&Epsilon;");
                        break;
                    case 918:
                        stringBuilder.Append("&Zeta;");
                        break;
                    case 919:
                        stringBuilder.Append("&Eta;");
                        break;
                    case 920:
                        stringBuilder.Append("&Theta;");
                        break;
                    case 921:
                        stringBuilder.Append("&Iota;");
                        break;
                    case 922:
                        stringBuilder.Append("&Kappa;");
                        break;
                    case 923:
                        stringBuilder.Append("&Lambda;");
                        break;
                    case 924:
                        stringBuilder.Append("&Mu;");
                        break;
                    case 925:
                        stringBuilder.Append("&Nu;");
                        break;
                    case 926:
                        stringBuilder.Append("&Xi;");
                        break;
                    case 927:
                        stringBuilder.Append("&Omicron;");
                        break;
                    case 928:
                        stringBuilder.Append("&Pi;");
                        break;
                    case 929:
                        stringBuilder.Append("&Rho;");
                        break;
                    case 931:
                        stringBuilder.Append("&Sigma;");
                        break;
                    case 932:
                        stringBuilder.Append("&Tau;");
                        break;
                    case 933:
                        stringBuilder.Append("&Upsilon;");
                        break;
                    case 934:
                        stringBuilder.Append("&Phi;");
                        break;
                    case 935:
                        stringBuilder.Append("&Chi;");
                        break;
                    case 936:
                        stringBuilder.Append("&Psi;");
                        break;
                    case 937:
                        stringBuilder.Append("&Omega;");
                        break;
                    case 945:
                        stringBuilder.Append("&alpha;");
                        break;
                    case 946:
                        stringBuilder.Append("&beta;");
                        break;
                    case 947:
                        stringBuilder.Append("&gamma;");
                        break;
                    case 948:
                        stringBuilder.Append("&delta;");
                        break;
                    case 949:
                        stringBuilder.Append("&epsilon;");
                        break;
                    case 950:
                        stringBuilder.Append("&zeta;");
                        break;
                    case 951:
                        stringBuilder.Append("&eta;");
                        break;
                    case 952:
                        stringBuilder.Append("&theta;");
                        break;
                    case 953:
                        stringBuilder.Append("&iota;");
                        break;
                    case 954:
                        stringBuilder.Append("&kappa;");
                        break;
                    case 955:
                        stringBuilder.Append("&lambda;");
                        break;
                    case 956:
                        stringBuilder.Append("&mu;");
                        break;
                    case 957:
                        stringBuilder.Append("&nu;");
                        break;
                    case 958:
                        stringBuilder.Append("&xi;");
                        break;
                    case 959:
                        stringBuilder.Append("&omicron;");
                        break;
                    case 960:
                        stringBuilder.Append("&pi;");
                        break;
                    case 961:
                        stringBuilder.Append("&rho;");
                        break;
                    case 962:
                        stringBuilder.Append("&sigmaf;");
                        break;
                    case 963:
                        stringBuilder.Append("&sigma;");
                        break;
                    case 964:
                        stringBuilder.Append("&tau;");
                        break;
                    case 965:
                        stringBuilder.Append("&upsilon;");
                        break;
                    case 966:
                        stringBuilder.Append("&phi;");
                        break;
                    case 967:
                        stringBuilder.Append("&chi;");
                        break;
                    case 968:
                        stringBuilder.Append("&psi;");
                        break;
                    case 969:
                        stringBuilder.Append("&omega;");
                        break;
                    case 977:
                        stringBuilder.Append("&thetasym;");
                        break;
                    case 978:
                        stringBuilder.Append("&upsih;");
                        break;
                    case 982:
                        stringBuilder.Append("&piv;");
                        break;
                    case 376:
                        stringBuilder.Append("&Yuml;");
                        break;
                    case 402:
                        stringBuilder.Append("&fnof;");
                        break;
                    case 60:
                        stringBuilder.Append("&lt;");
                        break;
                    case 62:
                        stringBuilder.Append("&gt;");
                        break;
                    case 128:
                        stringBuilder.Append("&euro;");
                        break;
                    case 160:
                        stringBuilder.Append("&nbsp;");
                        break;
                    case 161:
                        stringBuilder.Append("&iexcl;");
                        break;
                    case 162:
                        stringBuilder.Append("&cent;");
                        break;
                    case 163:
                        stringBuilder.Append("&pound;");
                        break;
                    case 164:
                        stringBuilder.Append("&curren;");
                        break;
                    case 165:
                        stringBuilder.Append("&yen;");
                        break;
                    case 166:
                        stringBuilder.Append("&brvbar;");
                        break;
                    case 167:
                        stringBuilder.Append("&sect;");
                        break;
                    case 168:
                        stringBuilder.Append("&uml;");
                        break;
                    case 169:
                        stringBuilder.Append("&copy;");
                        break;
                    case 170:
                        stringBuilder.Append("&ordf;");
                        break;
                    case 171:
                        stringBuilder.Append("&laquo;");
                        break;
                    case 172:
                        stringBuilder.Append("&not;");
                        break;
                    case 173:
                        stringBuilder.Append("&shy;");
                        break;
                    case 174:
                        stringBuilder.Append("&reg;");
                        break;
                    case 175:
                        stringBuilder.Append("&macr;");
                        break;
                    case 176:
                        stringBuilder.Append("&deg;");
                        break;
                    case 177:
                        stringBuilder.Append("&plusmn;");
                        break;
                    case 178:
                        stringBuilder.Append("&sup2;");
                        break;
                    case 179:
                        stringBuilder.Append("&sup3;");
                        break;
                    case 180:
                        stringBuilder.Append("&acute;");
                        break;
                    case 181:
                        stringBuilder.Append("&micro;");
                        break;
                    case 182:
                        stringBuilder.Append("&para;");
                        break;
                    case 183:
                        stringBuilder.Append("&middot;");
                        break;
                    case 184:
                        stringBuilder.Append("&cedil;");
                        break;
                    case 185:
                        stringBuilder.Append("&sup1;");
                        break;
                    case 186:
                        stringBuilder.Append("&ordm;");
                        break;
                    case 187:
                        stringBuilder.Append("&raquo;");
                        break;
                    case 188:
                        stringBuilder.Append("&frac14;");
                        break;
                    case 189:
                        stringBuilder.Append("&frac12;");
                        break;
                    case 190:
                        stringBuilder.Append("&frac34;");
                        break;
                    case 191:
                        stringBuilder.Append("&iquest;");
                        break;
                    case 192:
                        stringBuilder.Append("&Agrave;");
                        break;
                    case 193:
                        stringBuilder.Append("&Aacute;");
                        break;
                    case 194:
                        stringBuilder.Append("&Acirc;");
                        break;
                    case 195:
                        stringBuilder.Append("&Atilde;");
                        break;
                    case 196:
                        stringBuilder.Append("&Auml;");
                        break;
                    case 197:
                        stringBuilder.Append("&Aring;");
                        break;
                    case 198:
                        stringBuilder.Append("&AElig;");
                        break;
                    case 199:
                        stringBuilder.Append("&Ccedil;");
                        break;
                    case 200:
                        stringBuilder.Append("&Egrave;");
                        break;
                    case 201:
                        stringBuilder.Append("&Eacute;");
                        break;
                    case 202:
                        stringBuilder.Append("&Ecirc;");
                        break;
                    case 203:
                        stringBuilder.Append("&Euml;");
                        break;
                    case 204:
                        stringBuilder.Append("&Igrave;");
                        break;
                    case 205:
                        stringBuilder.Append("&Iacute;");
                        break;
                    case 206:
                        stringBuilder.Append("&Icirc;");
                        break;
                    case 207:
                        stringBuilder.Append("&Iuml;");
                        break;
                    case 208:
                        stringBuilder.Append("&ETH;");
                        break;
                    case 209:
                        stringBuilder.Append("&Ntilde;");
                        break;
                    case 210:
                        stringBuilder.Append("&Ograve;");
                        break;
                    case 211:
                        stringBuilder.Append("&Oacute;");
                        break;
                    case 212:
                        stringBuilder.Append("&Ocirc;");
                        break;
                    case 213:
                        stringBuilder.Append("&Otilde;");
                        break;
                    case 214:
                        stringBuilder.Append("&Ouml;");
                        break;
                    case 215:
                        stringBuilder.Append("&times;");
                        break;
                    case 216:
                        stringBuilder.Append("&Oslash;");
                        break;
                    case 217:
                        stringBuilder.Append("&Ugrave;");
                        break;
                    case 218:
                        stringBuilder.Append("&Uacute;");
                        break;
                    case 219:
                        stringBuilder.Append("&Ucirc;");
                        break;
                    case 220:
                        stringBuilder.Append("&Uuml;");
                        break;
                    case 221:
                        stringBuilder.Append("&Yacute;");
                        break;
                    case 222:
                        stringBuilder.Append("&THORN;");
                        break;
                    case 223:
                        stringBuilder.Append("&szlig;");
                        break;
                    case 224:
                        stringBuilder.Append("&agrave;");
                        break;
                    case 225:
                        stringBuilder.Append("&aacute;");
                        break;
                    case 226:
                        stringBuilder.Append("&acirc;");
                        break;
                    case 227:
                        stringBuilder.Append("&atilde;");
                        break;
                    case 228:
                        stringBuilder.Append("&auml;");
                        break;
                    case 229:
                        stringBuilder.Append("&aring;");
                        break;
                    case 230:
                        stringBuilder.Append("&aelig;");
                        break;
                    case 231:
                        stringBuilder.Append("&ccedil;");
                        break;
                    case 232:
                        stringBuilder.Append("&egrave;");
                        break;
                    case 233:
                        stringBuilder.Append("&eacute;");
                        break;
                    case 234:
                        stringBuilder.Append("&ecirc;");
                        break;
                    case 235:
                        stringBuilder.Append("&euml;");
                        break;
                    case 236:
                        stringBuilder.Append("&igrave;");
                        break;
                    case 237:
                        stringBuilder.Append("&iacute;");
                        break;
                    case 238:
                        stringBuilder.Append("&icirc;");
                        break;
                    case 239:
                        stringBuilder.Append("&iuml;");
                        break;
                    case 240:
                        stringBuilder.Append("&eth;");
                        break;
                    case 241:
                        stringBuilder.Append("&ntilde;");
                        break;
                    case 242:
                        stringBuilder.Append("&ograve;");
                        break;
                    case 243:
                        stringBuilder.Append("&oacute;");
                        break;
                    case 244:
                        stringBuilder.Append("&ocirc;");
                        break;
                    case 245:
                        stringBuilder.Append("&otilde;");
                        break;
                    case 246:
                        stringBuilder.Append("&ouml;");
                        break;
                    case 247:
                        stringBuilder.Append("&divide;");
                        break;
                    case 248:
                        stringBuilder.Append("&oslash;");
                        break;
                    case 249:
                        stringBuilder.Append("&ugrave;");
                        break;
                    case 250:
                        stringBuilder.Append("&uacute;");
                        break;
                    case 251:
                        stringBuilder.Append("&ucirc;");
                        break;
                    case 252:
                        stringBuilder.Append("&uuml;");
                        break;
                    case 253:
                        stringBuilder.Append("&yacute;");
                        break;
                    case 254:
                        stringBuilder.Append("&thorn;");
                        break;
                    case (int)byte.MaxValue:
                        stringBuilder.Append("&yuml;");
                        break;
                    case 338:
                        stringBuilder.Append("&OElig;");
                        break;
                    case 339:
                        stringBuilder.Append("&oelig;");
                        break;
                    case 352:
                        stringBuilder.Append("&Scaron;");
                        break;
                    case 353:
                        stringBuilder.Append("&scaron;");
                        break;
                    case 34:
                        stringBuilder.Append("&quot;");
                        break;
                    case 38:
                        stringBuilder.Append("&amp;");
                        break;
                    default:
                        if (num <= (int)sbyte.MaxValue)
                            stringBuilder.Append((char)num);
                        else
                            stringBuilder.Append("&#" + (object)num + ";");
                        break;
                }
                num = stringReader.Read();
            }
            label_258:
            return stringBuilder.ToString();
        }
    }
}
