#region PDFsharp - A .NET library for processing PDF
//
// Authors:
//   Stefan Lange (mailto:Stefan.Lange@pdfsharp.com)
//
// Copyright (c) 2005-2009 empira Software GmbH, Cologne (Germany)
//
// http://www.pdfsharp.com
// http://sourceforge.net/projects/pdfsharp
//
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included
// in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.
#endregion


namespace PdfSharp.Drawing
{
    /// <summary>
    /// Pens for all the pre-defined colors.
    /// </summary>
    public static class XPens
    {
        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen AliceBlue
        {
            get
            {
                aliceBlue ??= new XPen(XColors.AliceBlue, 1, true);
                return aliceBlue;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen AntiqueWhite
        {
            get
            {
                antiqueWhite ??= new XPen(XColors.AntiqueWhite, 1, true);
                return antiqueWhite;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Aqua
        {
            get
            {
                aqua ??= new XPen(XColors.Aqua, 1, true);
                return aqua;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Aquamarine
        {
            get
            {
                aquamarine ??= new XPen(XColors.Aquamarine, 1, true);
                return aquamarine;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Azure
        {
            get
            {
                azure ??= new XPen(XColors.Azure, 1, true);
                return azure;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Beige
        {
            get
            {
                beige ??= new XPen(XColors.Beige, 1, true);
                return beige;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Bisque
        {
            get
            {
                bisque ??= new XPen(XColors.Bisque, 1, true);
                return bisque;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Black
        {
            get
            {
                black ??= new XPen(XColors.Black, 1, true);
                return black;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen BlanchedAlmond
        {
            get
            {
                blanchedAlmond ??= new XPen(XColors.BlanchedAlmond, 1, true);
                return blanchedAlmond;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Blue
        {
            get
            {
                blue ??= new XPen(XColors.Blue, 1, true);
                return blue;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen BlueViolet
        {
            get
            {
                blueViolet ??= new XPen(XColors.BlueViolet, 1, true);
                return blueViolet;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Brown
        {
            get
            {
                brown ??= new XPen(XColors.Brown, 1, true);
                return brown;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen BurlyWood
        {
            get
            {
                burlyWood ??= new XPen(XColors.BurlyWood, 1, true);
                return burlyWood;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen CadetBlue
        {
            get
            {
                cadetBlue ??= new XPen(XColors.CadetBlue, 1, true);
                return cadetBlue;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Chartreuse
        {
            get
            {
                chartreuse ??= new XPen(XColors.Chartreuse, 1, true);
                return chartreuse;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Chocolate
        {
            get
            {
                chocolate ??= new XPen(XColors.Chocolate, 1, true);
                return chocolate;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Coral
        {
            get
            {
                coral ??= new XPen(XColors.Coral, 1, true);
                return coral;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen CornflowerBlue
        {
            get
            {
                cornflowerBlue ??= new XPen(XColors.CornflowerBlue, 1, true);
                return cornflowerBlue;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Cornsilk
        {
            get
            {
                cornsilk ??= new XPen(XColors.Cornsilk, 1, true);
                return cornsilk;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Crimson
        {
            get
            {
                crimson ??= new XPen(XColors.Crimson, 1, true);
                return crimson;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Cyan
        {
            get
            {
                cyan ??= new XPen(XColors.Cyan, 1, true);
                return cyan;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen DarkBlue
        {
            get
            {
                darkBlue ??= new XPen(XColors.DarkBlue, 1, true);
                return darkBlue;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen DarkCyan
        {
            get
            {
                darkCyan ??= new XPen(XColors.DarkCyan, 1, true);
                return darkCyan;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen DarkGoldenrod
        {
            get
            {
                darkGoldenrod ??= new XPen(XColors.DarkGoldenrod, 1, true);
                return darkGoldenrod;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen DarkGray
        {
            get
            {
                darkGray ??= new XPen(XColors.DarkGray, 1, true);
                return darkGray;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen DarkGreen
        {
            get
            {
                darkGreen ??= new XPen(XColors.DarkGreen, 1, true);
                return darkGreen;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen DarkKhaki
        {
            get
            {
                darkKhaki ??= new XPen(XColors.DarkKhaki, 1, true);
                return darkKhaki;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen DarkMagenta
        {
            get
            {
                darkMagenta ??= new XPen(XColors.DarkMagenta, 1, true);
                return darkMagenta;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen DarkOliveGreen
        {
            get
            {
                darkOliveGreen ??= new XPen(XColors.DarkOliveGreen, 1, true);
                return darkOliveGreen;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen DarkOrange
        {
            get
            {
                darkOrange ??= new XPen(XColors.DarkOrange, 1, true);
                return darkOrange;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen DarkOrchid
        {
            get
            {
                darkOrchid ??= new XPen(XColors.DarkOrchid, 1, true);
                return darkOrchid;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen DarkRed
        {
            get
            {
                darkRed ??= new XPen(XColors.DarkRed, 1, true);
                return darkRed;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen DarkSalmon
        {
            get
            {
                darkSalmon ??= new XPen(XColors.DarkSalmon, 1, true);
                return darkSalmon;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen DarkSeaGreen
        {
            get
            {
                darkSeaGreen ??= new XPen(XColors.DarkSeaGreen, 1, true);
                return darkSeaGreen;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen DarkSlateBlue
        {
            get
            {
                darkSlateBlue ??= new XPen(XColors.DarkSlateBlue, 1, true);
                return darkSlateBlue;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen DarkSlateGray
        {
            get
            {
                darkSlateGray ??= new XPen(XColors.DarkSlateGray, 1, true);
                return darkSlateGray;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen DarkTurquoise
        {
            get
            {
                darkTurquoise ??= new XPen(XColors.DarkTurquoise, 1, true);
                return darkTurquoise;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen DarkViolet
        {
            get
            {
                darkViolet ??= new XPen(XColors.DarkViolet, 1, true);
                return darkViolet;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen DeepPink
        {
            get
            {
                deepPink ??= new XPen(XColors.DeepPink, 1, true);
                return deepPink;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen DeepSkyBlue
        {
            get
            {
                deepSkyBlue ??= new XPen(XColors.DeepSkyBlue, 1, true);
                return deepSkyBlue;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen DimGray
        {
            get
            {
                dimGray ??= new XPen(XColors.DimGray, 1, true);
                return dimGray;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen DodgerBlue
        {
            get
            {
                dodgerBlue ??= new XPen(XColors.DodgerBlue, 1, true);
                return dodgerBlue;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Firebrick
        {
            get
            {
                firebrick ??= new XPen(XColors.Firebrick, 1, true);
                return firebrick;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen FloralWhite
        {
            get
            {
                floralWhite ??= new XPen(XColors.FloralWhite, 1, true);
                return floralWhite;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen ForestGreen
        {
            get
            {
                forestGreen ??= new XPen(XColors.ForestGreen, 1, true);
                return forestGreen;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Fuchsia
        {
            get
            {
                fuchsia ??= new XPen(XColors.Fuchsia, 1, true);
                return fuchsia;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Gainsboro
        {
            get
            {
                gainsboro ??= new XPen(XColors.Gainsboro, 1, true);
                return gainsboro;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen GhostWhite
        {
            get
            {
                ghostWhite ??= new XPen(XColors.GhostWhite, 1, true);
                return ghostWhite;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Gold
        {
            get
            {
                gold ??= new XPen(XColors.Gold, 1, true);
                return gold;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Goldenrod
        {
            get
            {
                goldenrod ??= new XPen(XColors.Goldenrod, 1, true);
                return goldenrod;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Gray
        {
            get
            {
                gray ??= new XPen(XColors.Gray, 1, true);
                return gray;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Green
        {
            get
            {
                green ??= new XPen(XColors.Green, 1, true);
                return green;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen GreenYellow
        {
            get
            {
                greenYellow ??= new XPen(XColors.GreenYellow, 1, true);
                return greenYellow;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Honeydew
        {
            get
            {
                honeydew ??= new XPen(XColors.Honeydew, 1, true);
                return honeydew;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen HotPink
        {
            get
            {
                hotPink ??= new XPen(XColors.HotPink, 1, true);
                return hotPink;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen IndianRed
        {
            get
            {
                indianRed ??= new XPen(XColors.IndianRed, 1, true);
                return indianRed;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Indigo
        {
            get
            {
                indigo ??= new XPen(XColors.Indigo, 1, true);
                return indigo;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Ivory
        {
            get
            {
                ivory ??= new XPen(XColors.Ivory, 1, true);
                return ivory;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Khaki
        {
            get
            {
                khaki ??= new XPen(XColors.Khaki, 1, true);
                return khaki;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Lavender
        {
            get
            {
                lavender ??= new XPen(XColors.Lavender, 1, true);
                return lavender;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen LavenderBlush
        {
            get
            {
                lavenderBlush ??= new XPen(XColors.LavenderBlush, 1, true);
                return lavenderBlush;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen LawnGreen
        {
            get
            {
                lawnGreen ??= new XPen(XColors.LawnGreen, 1, true);
                return lawnGreen;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen LemonChiffon
        {
            get
            {
                lemonChiffon ??= new XPen(XColors.LemonChiffon, 1, true);
                return lemonChiffon;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen LightBlue
        {
            get
            {
                lightBlue ??= new XPen(XColors.LightBlue, 1, true);
                return lightBlue;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen LightCoral
        {
            get
            {
                lightCoral ??= new XPen(XColors.LightCoral, 1, true);
                return lightCoral;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen LightCyan
        {
            get
            {
                lightCyan ??= new XPen(XColors.LightCyan, 1, true);
                return lightCyan;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen LightGoldenrodYellow
        {
            get
            {
                lightGoldenrodYellow ??= new XPen(XColors.LightGoldenrodYellow, 1, true);
                return lightGoldenrodYellow;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen LightGray
        {
            get
            {
                lightGray ??= new XPen(XColors.LightGray, 1, true);
                return lightGray;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen LightGreen
        {
            get
            {
                lightGreen ??= new XPen(XColors.LightGreen, 1, true);
                return lightGreen;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen LightPink
        {
            get
            {
                lightPink ??= new XPen(XColors.LightPink, 1, true);
                return lightPink;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen LightSalmon
        {
            get
            {
                lightSalmon ??= new XPen(XColors.LightSalmon, 1, true);
                return lightSalmon;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen LightSeaGreen
        {
            get
            {
                lightSeaGreen ??= new XPen(XColors.LightSeaGreen, 1, true);
                return lightSeaGreen;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen LightSkyBlue
        {
            get
            {
                lightSkyBlue ??= new XPen(XColors.LightSkyBlue, 1, true);
                return lightSkyBlue;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen LightSlateGray
        {
            get
            {
                lightSlateGray ??= new XPen(XColors.LightSlateGray, 1, true);
                return lightSlateGray;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen LightSteelBlue
        {
            get
            {
                lightSteelBlue ??= new XPen(XColors.LightSteelBlue, 1, true);
                return lightSteelBlue;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen LightYellow
        {
            get
            {
                lightYellow ??= new XPen(XColors.LightYellow, 1, true);
                return lightYellow;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Lime
        {
            get
            {
                lime ??= new XPen(XColors.Lime, 1, true);
                return lime;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen LimeGreen
        {
            get
            {
                limeGreen ??= new XPen(XColors.LimeGreen, 1, true);
                return limeGreen;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Linen
        {
            get
            {
                linen ??= new XPen(XColors.Linen, 1, true);
                return linen;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Magenta
        {
            get
            {
                magenta ??= new XPen(XColors.Magenta, 1, true);
                return magenta;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Maroon
        {
            get
            {
                maroon ??= new XPen(XColors.Maroon, 1, true);
                return maroon;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen MediumAquamarine
        {
            get
            {
                mediumAquamarine ??= new XPen(XColors.MediumAquamarine, 1, true);
                return mediumAquamarine;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen MediumBlue
        {
            get
            {
                mediumBlue ??= new XPen(XColors.MediumBlue, 1, true);
                return mediumBlue;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen MediumOrchid
        {
            get
            {
                mediumOrchid ??= new XPen(XColors.MediumOrchid, 1, true);
                return mediumOrchid;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen MediumPurple
        {
            get
            {
                mediumPurple ??= new XPen(XColors.MediumPurple, 1, true);
                return mediumPurple;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen MediumSeaGreen
        {
            get
            {
                mediumSeaGreen ??= new XPen(XColors.MediumSeaGreen, 1, true);
                return mediumSeaGreen;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen MediumSlateBlue
        {
            get
            {
                mediumSlateBlue ??= new XPen(XColors.MediumSlateBlue, 1, true);
                return mediumSlateBlue;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen MediumSpringGreen
        {
            get
            {
                mediumSpringGreen ??= new XPen(XColors.MediumSpringGreen, 1, true);
                return mediumSpringGreen;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen MediumTurquoise
        {
            get
            {
                mediumTurquoise ??= new XPen(XColors.MediumTurquoise, 1, true);
                return mediumTurquoise;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen MediumVioletRed
        {
            get
            {
                mediumVioletRed ??= new XPen(XColors.MediumVioletRed, 1, true);
                return mediumVioletRed;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen MidnightBlue
        {
            get
            {
                midnightBlue ??= new XPen(XColors.MidnightBlue, 1, true);
                return midnightBlue;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen MintCream
        {
            get
            {
                mintCream ??= new XPen(XColors.MintCream, 1, true);
                return mintCream;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen MistyRose
        {
            get
            {
                mistyRose ??= new XPen(XColors.MistyRose, 1, true);
                return mistyRose;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Moccasin
        {
            get
            {
                moccasin ??= new XPen(XColors.Moccasin, 1, true);
                return moccasin;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen NavajoWhite
        {
            get
            {
                navajoWhite ??= new XPen(XColors.NavajoWhite, 1, true);
                return navajoWhite;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Navy
        {
            get
            {
                navy ??= new XPen(XColors.Navy, 1, true);
                return navy;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen OldLace
        {
            get
            {
                oldLace ??= new XPen(XColors.OldLace, 1, true);
                return oldLace;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Olive
        {
            get
            {
                olive ??= new XPen(XColors.Olive, 1, true);
                return olive;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen OliveDrab
        {
            get
            {
                oliveDrab ??= new XPen(XColors.OliveDrab, 1, true);
                return oliveDrab;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Orange
        {
            get
            {
                orange ??= new XPen(XColors.Orange, 1, true);
                return orange;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen OrangeRed
        {
            get
            {
                orangeRed ??= new XPen(XColors.OrangeRed, 1, true);
                return orangeRed;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Orchid
        {
            get
            {
                orchid ??= new XPen(XColors.Orchid, 1, true);
                return orchid;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen PaleGoldenrod
        {
            get
            {
                paleGoldenrod ??= new XPen(XColors.PaleGoldenrod, 1, true);
                return paleGoldenrod;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen PaleGreen
        {
            get
            {
                paleGreen ??= new XPen(XColors.PaleGreen, 1, true);
                return paleGreen;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen PaleTurquoise
        {
            get
            {
                paleTurquoise ??= new XPen(XColors.PaleTurquoise, 1, true);
                return paleTurquoise;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen PaleVioletRed
        {
            get
            {
                paleVioletRed ??= new XPen(XColors.PaleVioletRed, 1, true);
                return paleVioletRed;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen PapayaWhip
        {
            get
            {
                papayaWhip ??= new XPen(XColors.PapayaWhip, 1, true);
                return papayaWhip;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen PeachPuff
        {
            get
            {
                peachPuff ??= new XPen(XColors.PeachPuff, 1, true);
                return peachPuff;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Peru
        {
            get
            {
                peru ??= new XPen(XColors.Peru, 1, true);
                return peru;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Pink
        {
            get
            {
                pink ??= new XPen(XColors.Pink, 1, true);
                return pink;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Plum
        {
            get
            {
                plum ??= new XPen(XColors.Plum, 1, true);
                return plum;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen PowderBlue
        {
            get
            {
                powderBlue ??= new XPen(XColors.PowderBlue, 1, true);
                return powderBlue;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Purple
        {
            get
            {
                purple ??= new XPen(XColors.Purple, 1, true);
                return purple;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Red
        {
            get
            {
                red ??= new XPen(XColors.Red, 1, true);
                return red;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen RosyBrown
        {
            get
            {
                rosyBrown ??= new XPen(XColors.RosyBrown, 1, true);
                return rosyBrown;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen RoyalBlue
        {
            get
            {
                royalBlue ??= new XPen(XColors.RoyalBlue, 1, true);
                return royalBlue;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen SaddleBrown
        {
            get
            {
                saddleBrown ??= new XPen(XColors.SaddleBrown, 1, true);
                return saddleBrown;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Salmon
        {
            get
            {
                salmon ??= new XPen(XColors.Salmon, 1, true);
                return salmon;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen SandyBrown
        {
            get
            {
                sandyBrown ??= new XPen(XColors.SandyBrown, 1, true);
                return sandyBrown;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen SeaGreen
        {
            get
            {
                seaGreen ??= new XPen(XColors.SeaGreen, 1, true);
                return seaGreen;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen SeaShell
        {
            get
            {
                seaShell ??= new XPen(XColors.SeaShell, 1, true);
                return seaShell;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Sienna
        {
            get
            {
                sienna ??= new XPen(XColors.Sienna, 1, true);
                return sienna;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Silver
        {
            get
            {
                silver ??= new XPen(XColors.Silver, 1, true);
                return silver;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen SkyBlue
        {
            get
            {
                skyBlue ??= new XPen(XColors.SkyBlue, 1, true);
                return skyBlue;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen SlateBlue
        {
            get
            {
                slateBlue ??= new XPen(XColors.SlateBlue, 1, true);
                return slateBlue;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen SlateGray
        {
            get
            {
                slateGray ??= new XPen(XColors.SlateGray, 1, true);
                return slateGray;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Snow
        {
            get
            {
                snow ??= new XPen(XColors.Snow, 1, true);
                return snow;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen SpringGreen
        {
            get
            {
                springGreen ??= new XPen(XColors.SpringGreen, 1, true);
                return springGreen;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen SteelBlue
        {
            get
            {
                steelBlue ??= new XPen(XColors.SteelBlue, 1, true);
                return steelBlue;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Tan
        {
            get
            {
                tan ??= new XPen(XColors.Tan, 1, true);
                return tan;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Teal
        {
            get
            {
                teal ??= new XPen(XColors.Teal, 1, true);
                return teal;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Thistle
        {
            get
            {
                thistle ??= new XPen(XColors.Thistle, 1, true);
                return thistle;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Tomato
        {
            get
            {
                tomato ??= new XPen(XColors.Tomato, 1, true);
                return tomato;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Transparent
        {
            get
            {
                transparent ??= new XPen(XColors.Transparent, 1, true);
                return transparent;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Turquoise
        {
            get
            {
                turquoise ??= new XPen(XColors.Turquoise, 1, true);
                return turquoise;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Violet
        {
            get
            {
                violet ??= new XPen(XColors.Violet, 1, true);
                return violet;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Wheat
        {
            get
            {
                wheat ??= new XPen(XColors.Wheat, 1, true);
                return wheat;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen White
        {
            get
            {
                white ??= new XPen(XColors.White, 1, true);
                return white;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen WhiteSmoke
        {
            get
            {
                whiteSmoke ??= new XPen(XColors.WhiteSmoke, 1, true);
                return whiteSmoke;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen Yellow
        {
            get
            {
                yellow ??= new XPen(XColors.Yellow, 1, true);
                return yellow;
            }
        }

        /// <summary>Gets a pre-defined XPen object.</summary>
        public static XPen YellowGreen
        {
            get
            {
                yellowGreen ??= new XPen(XColors.YellowGreen, 1, true);
                return yellowGreen;
            }
        }


        private static XPen aliceBlue;
        private static XPen antiqueWhite;
        private static XPen aqua;
        private static XPen aquamarine;
        private static XPen azure;
        private static XPen beige;
        private static XPen bisque;
        private static XPen black;
        private static XPen blanchedAlmond;
        private static XPen blue;
        private static XPen blueViolet;
        private static XPen brown;
        private static XPen burlyWood;
        private static XPen cadetBlue;
        private static XPen chartreuse;
        private static XPen chocolate;
        private static XPen coral;
        private static XPen cornflowerBlue;
        private static XPen cornsilk;
        private static XPen crimson;
        private static XPen cyan;
        private static XPen darkBlue;
        private static XPen darkCyan;
        private static XPen darkGoldenrod;
        private static XPen darkGray;
        private static XPen darkGreen;
        private static XPen darkKhaki;
        private static XPen darkMagenta;
        private static XPen darkOliveGreen;
        private static XPen darkOrange;
        private static XPen darkOrchid;
        private static XPen darkRed;
        private static XPen darkSalmon;
        private static XPen darkSeaGreen;
        private static XPen darkSlateBlue;
        private static XPen darkSlateGray;
        private static XPen darkTurquoise;
        private static XPen darkViolet;
        private static XPen deepPink;
        private static XPen deepSkyBlue;
        private static XPen dimGray;
        private static XPen dodgerBlue;
        private static XPen firebrick;
        private static XPen floralWhite;
        private static XPen forestGreen;
        private static XPen fuchsia;
        private static XPen gainsboro;
        private static XPen ghostWhite;
        private static XPen gold;
        private static XPen goldenrod;
        private static XPen gray;
        private static XPen green;
        private static XPen greenYellow;
        private static XPen honeydew;
        private static XPen hotPink;
        private static XPen indianRed;
        private static XPen indigo;
        private static XPen ivory;
        private static XPen khaki;
        private static XPen lavender;
        private static XPen lavenderBlush;
        private static XPen lawnGreen;
        private static XPen lemonChiffon;
        private static XPen lightBlue;
        private static XPen lightCoral;
        private static XPen lightCyan;
        private static XPen lightGoldenrodYellow;
        private static XPen lightGray;
        private static XPen lightGreen;
        private static XPen lightPink;
        private static XPen lightSalmon;
        private static XPen lightSeaGreen;
        private static XPen lightSkyBlue;
        private static XPen lightSlateGray;
        private static XPen lightSteelBlue;
        private static XPen lightYellow;
        private static XPen lime;
        private static XPen limeGreen;
        private static XPen linen;
        private static XPen magenta;
        private static XPen maroon;
        private static XPen mediumAquamarine;
        private static XPen mediumBlue;
        private static XPen mediumOrchid;
        private static XPen mediumPurple;
        private static XPen mediumSeaGreen;
        private static XPen mediumSlateBlue;
        private static XPen mediumSpringGreen;
        private static XPen mediumTurquoise;
        private static XPen mediumVioletRed;
        private static XPen midnightBlue;
        private static XPen mintCream;
        private static XPen mistyRose;
        private static XPen moccasin;
        private static XPen navajoWhite;
        private static XPen navy;
        private static XPen oldLace;
        private static XPen olive;
        private static XPen oliveDrab;
        private static XPen orange;
        private static XPen orangeRed;
        private static XPen orchid;
        private static XPen paleGoldenrod;
        private static XPen paleGreen;
        private static XPen paleTurquoise;
        private static XPen paleVioletRed;
        private static XPen papayaWhip;
        private static XPen peachPuff;
        private static XPen peru;
        private static XPen pink;
        private static XPen plum;
        private static XPen powderBlue;
        private static XPen purple;
        private static XPen red;
        private static XPen rosyBrown;
        private static XPen royalBlue;
        private static XPen saddleBrown;
        private static XPen salmon;
        private static XPen sandyBrown;
        private static XPen seaGreen;
        private static XPen seaShell;
        private static XPen sienna;
        private static XPen silver;
        private static XPen skyBlue;
        private static XPen slateBlue;
        private static XPen slateGray;
        private static XPen snow;
        private static XPen springGreen;
        private static XPen steelBlue;
        private static XPen tan;
        private static XPen teal;
        private static XPen thistle;
        private static XPen tomato;
        private static XPen transparent;
        private static XPen turquoise;
        private static XPen violet;
        private static XPen wheat;
        private static XPen white;
        private static XPen whiteSmoke;
        private static XPen yellow;
        private static XPen yellowGreen;
    }
}
