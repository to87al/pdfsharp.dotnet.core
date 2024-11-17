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
    /// Brushes for all the pre-defined colors.
    /// </summary>
    public static class XBrushes
    {
        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush AliceBlue
        {
            get
            {
                aliceBlue ??= new XSolidBrush(XColors.AliceBlue, true);
                return aliceBlue;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush AntiqueWhite
        {
            get
            {
                antiqueWhite ??= new XSolidBrush(XColors.AntiqueWhite, true);
                return antiqueWhite;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Aqua
        {
            get
            {
                aqua ??= new XSolidBrush(XColors.Aqua, true);
                return aqua;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Aquamarine
        {
            get
            {
                aquamarine ??= new XSolidBrush(XColors.Aquamarine, true);
                return aquamarine;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Azure
        {
            get
            {
                azure ??= new XSolidBrush(XColors.Azure, true);
                return azure;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Beige
        {
            get
            {
                beige ??= new XSolidBrush(XColors.Beige, true);
                return beige;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Bisque
        {
            get
            {
                bisque ??= new XSolidBrush(XColors.Bisque, true);
                return bisque;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Black
        {
            get
            {
                black ??= new XSolidBrush(XColors.Black, true);
                return black;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush BlanchedAlmond
        {
            get
            {
                blanchedAlmond ??= new XSolidBrush(XColors.BlanchedAlmond, true);
                return blanchedAlmond;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Blue
        {
            get
            {
                blue ??= new XSolidBrush(XColors.Blue, true);
                return blue;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush BlueViolet
        {
            get
            {
                blueViolet ??= new XSolidBrush(XColors.BlueViolet, true);
                return blueViolet;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Brown
        {
            get
            {
                brown ??= new XSolidBrush(XColors.Brown, true);
                return brown;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush BurlyWood
        {
            get
            {
                burlyWood ??= new XSolidBrush(XColors.BurlyWood, true);
                return burlyWood;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush CadetBlue
        {
            get
            {
                cadetBlue ??= new XSolidBrush(XColors.CadetBlue, true);
                return cadetBlue;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Chartreuse
        {
            get
            {
                chartreuse ??= new XSolidBrush(XColors.Chartreuse, true);
                return chartreuse;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Chocolate
        {
            get
            {
                chocolate ??= new XSolidBrush(XColors.Chocolate, true);
                return chocolate;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Coral
        {
            get
            {
                coral ??= new XSolidBrush(XColors.Coral, true);
                return coral;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush CornflowerBlue
        {
            get
            {
                cornflowerBlue ??= new XSolidBrush(XColors.CornflowerBlue, true);
                return cornflowerBlue;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Cornsilk
        {
            get
            {
                cornsilk ??= new XSolidBrush(XColors.Cornsilk, true);
                return cornsilk;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Crimson
        {
            get
            {
                crimson ??= new XSolidBrush(XColors.Crimson, true);
                return crimson;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Cyan
        {
            get
            {
                cyan ??= new XSolidBrush(XColors.Cyan, true);
                return cyan;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush DarkBlue
        {
            get
            {
                darkBlue ??= new XSolidBrush(XColors.DarkBlue, true);
                return darkBlue;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush DarkCyan
        {
            get
            {
                darkCyan ??= new XSolidBrush(XColors.DarkCyan, true);
                return darkCyan;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush DarkGoldenrod
        {
            get
            {
                darkGoldenrod ??= new XSolidBrush(XColors.DarkGoldenrod, true);
                return darkGoldenrod;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush DarkGray
        {
            get
            {
                darkGray ??= new XSolidBrush(XColors.DarkGray, true);
                return darkGray;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush DarkGreen
        {
            get
            {
                darkGreen ??= new XSolidBrush(XColors.DarkGreen, true);
                return darkGreen;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush DarkKhaki
        {
            get
            {
                darkKhaki ??= new XSolidBrush(XColors.DarkKhaki, true);
                return darkKhaki;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush DarkMagenta
        {
            get
            {
                darkMagenta ??= new XSolidBrush(XColors.DarkMagenta, true);
                return darkMagenta;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush DarkOliveGreen
        {
            get
            {
                darkOliveGreen ??= new XSolidBrush(XColors.DarkOliveGreen, true);
                return darkOliveGreen;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush DarkOrange
        {
            get
            {
                darkOrange ??= new XSolidBrush(XColors.DarkOrange, true);
                return darkOrange;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush DarkOrchid
        {
            get
            {
                darkOrchid ??= new XSolidBrush(XColors.DarkOrchid, true);
                return darkOrchid;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush DarkRed
        {
            get
            {
                darkRed ??= new XSolidBrush(XColors.DarkRed, true);
                return darkRed;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush DarkSalmon
        {
            get
            {
                darkSalmon ??= new XSolidBrush(XColors.DarkSalmon, true);
                return darkSalmon;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush DarkSeaGreen
        {
            get
            {
                darkSeaGreen ??= new XSolidBrush(XColors.DarkSeaGreen, true);
                return darkSeaGreen;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush DarkSlateBlue
        {
            get
            {
                darkSlateBlue ??= new XSolidBrush(XColors.DarkSlateBlue, true);
                return darkSlateBlue;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush DarkSlateGray
        {
            get
            {
                darkSlateGray ??= new XSolidBrush(XColors.DarkSlateGray, true);
                return darkSlateGray;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush DarkTurquoise
        {
            get
            {
                darkTurquoise ??= new XSolidBrush(XColors.DarkTurquoise, true);
                return darkTurquoise;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush DarkViolet
        {
            get
            {
                darkViolet ??= new XSolidBrush(XColors.DarkViolet, true);
                return darkViolet;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush DeepPink
        {
            get
            {
                deepPink ??= new XSolidBrush(XColors.DeepPink, true);
                return deepPink;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush DeepSkyBlue
        {
            get
            {
                deepSkyBlue ??= new XSolidBrush(XColors.DeepSkyBlue, true);
                return deepSkyBlue;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush DimGray
        {
            get
            {
                dimGray ??= new XSolidBrush(XColors.DimGray, true);
                return dimGray;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush DodgerBlue
        {
            get
            {
                dodgerBlue ??= new XSolidBrush(XColors.DodgerBlue, true);
                return dodgerBlue;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Firebrick
        {
            get
            {
                firebrick ??= new XSolidBrush(XColors.Firebrick, true);
                return firebrick;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush FloralWhite
        {
            get
            {
                floralWhite ??= new XSolidBrush(XColors.FloralWhite, true);
                return floralWhite;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush ForestGreen
        {
            get
            {
                forestGreen ??= new XSolidBrush(XColors.ForestGreen, true);
                return forestGreen;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Fuchsia
        {
            get
            {
                fuchsia ??= new XSolidBrush(XColors.Fuchsia, true);
                return fuchsia;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Gainsboro
        {
            get
            {
                gainsboro ??= new XSolidBrush(XColors.Gainsboro, true);
                return gainsboro;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush GhostWhite
        {
            get
            {
                ghostWhite ??= new XSolidBrush(XColors.GhostWhite, true);
                return ghostWhite;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Gold
        {
            get
            {
                gold ??= new XSolidBrush(XColors.Gold, true);
                return gold;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Goldenrod
        {
            get
            {
                goldenrod ??= new XSolidBrush(XColors.Goldenrod, true);
                return goldenrod;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Gray
        {
            get
            {
                gray ??= new XSolidBrush(XColors.Gray, true);
                return gray;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Green
        {
            get
            {
                green ??= new XSolidBrush(XColors.Green, true);
                return green;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush GreenYellow
        {
            get
            {
                greenYellow ??= new XSolidBrush(XColors.GreenYellow, true);
                return greenYellow;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Honeydew
        {
            get
            {
                honeydew ??= new XSolidBrush(XColors.Honeydew, true);
                return honeydew;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush HotPink
        {
            get
            {
                hotPink ??= new XSolidBrush(XColors.HotPink, true);
                return hotPink;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush IndianRed
        {
            get
            {
                indianRed ??= new XSolidBrush(XColors.IndianRed, true);
                return indianRed;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Indigo
        {
            get
            {
                indigo ??= new XSolidBrush(XColors.Indigo, true);
                return indigo;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Ivory
        {
            get
            {
                ivory ??= new XSolidBrush(XColors.Ivory, true);
                return ivory;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Khaki
        {
            get
            {
                khaki ??= new XSolidBrush(XColors.Khaki, true);
                return khaki;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Lavender
        {
            get
            {
                lavender ??= new XSolidBrush(XColors.Lavender, true);
                return lavender;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush LavenderBlush
        {
            get
            {
                lavenderBlush ??= new XSolidBrush(XColors.LavenderBlush, true);
                return lavenderBlush;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush LawnGreen
        {
            get
            {
                lawnGreen ??= new XSolidBrush(XColors.LawnGreen, true);
                return lawnGreen;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush LemonChiffon
        {
            get
            {
                lemonChiffon ??= new XSolidBrush(XColors.LemonChiffon, true);
                return lemonChiffon;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush LightBlue
        {
            get
            {
                lightBlue ??= new XSolidBrush(XColors.LightBlue, true);
                return lightBlue;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush LightCoral
        {
            get
            {
                lightCoral ??= new XSolidBrush(XColors.LightCoral, true);
                return lightCoral;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush LightCyan
        {
            get
            {
                lightCyan ??= new XSolidBrush(XColors.LightCyan, true);
                return lightCyan;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush LightGoldenrodYellow
        {
            get
            {
                lightGoldenrodYellow ??= new XSolidBrush(XColors.LightGoldenrodYellow, true);
                return lightGoldenrodYellow;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush LightGray
        {
            get
            {
                lightGray ??= new XSolidBrush(XColors.LightGray, true);
                return lightGray;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush LightGreen
        {
            get
            {
                lightGreen ??= new XSolidBrush(XColors.LightGreen, true);
                return lightGreen;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush LightPink
        {
            get
            {
                lightPink ??= new XSolidBrush(XColors.LightPink, true);
                return lightPink;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush LightSalmon
        {
            get
            {
                lightSalmon ??= new XSolidBrush(XColors.LightSalmon, true);
                return lightSalmon;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush LightSeaGreen
        {
            get
            {
                lightSeaGreen ??= new XSolidBrush(XColors.LightSeaGreen, true);
                return lightSeaGreen;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush LightSkyBlue
        {
            get
            {
                lightSkyBlue ??= new XSolidBrush(XColors.LightSkyBlue, true);
                return lightSkyBlue;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush LightSlateGray
        {
            get
            {
                lightSlateGray ??= new XSolidBrush(XColors.LightSlateGray, true);
                return lightSlateGray;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush LightSteelBlue
        {
            get
            {
                lightSteelBlue ??= new XSolidBrush(XColors.LightSteelBlue, true);
                return lightSteelBlue;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush LightYellow
        {
            get
            {
                lightYellow ??= new XSolidBrush(XColors.LightYellow, true);
                return lightYellow;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Lime
        {
            get
            {
                lime ??= new XSolidBrush(XColors.Lime, true);
                return lime;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush LimeGreen
        {
            get
            {
                limeGreen ??= new XSolidBrush(XColors.LimeGreen, true);
                return limeGreen;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Linen
        {
            get
            {
                linen ??= new XSolidBrush(XColors.Linen, true);
                return linen;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Magenta
        {
            get
            {
                magenta ??= new XSolidBrush(XColors.Magenta, true);
                return magenta;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Maroon
        {
            get
            {
                maroon ??= new XSolidBrush(XColors.Maroon, true);
                return maroon;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush MediumAquamarine
        {
            get
            {
                mediumAquamarine ??= new XSolidBrush(XColors.MediumAquamarine, true);
                return mediumAquamarine;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush MediumBlue
        {
            get
            {
                mediumBlue ??= new XSolidBrush(XColors.MediumBlue, true);
                return mediumBlue;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush MediumOrchid
        {
            get
            {
                mediumOrchid ??= new XSolidBrush(XColors.MediumOrchid, true);
                return mediumOrchid;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush MediumPurple
        {
            get
            {
                mediumPurple ??= new XSolidBrush(XColors.MediumPurple, true);
                return mediumPurple;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush MediumSeaGreen
        {
            get
            {
                mediumSeaGreen ??= new XSolidBrush(XColors.MediumSeaGreen, true);
                return mediumSeaGreen;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush MediumSlateBlue
        {
            get
            {
                mediumSlateBlue ??= new XSolidBrush(XColors.MediumSlateBlue, true);
                return mediumSlateBlue;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush MediumSpringGreen
        {
            get
            {
                mediumSpringGreen ??= new XSolidBrush(XColors.MediumSpringGreen, true);
                return mediumSpringGreen;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush MediumTurquoise
        {
            get
            {
                mediumTurquoise ??= new XSolidBrush(XColors.MediumTurquoise, true);
                return mediumTurquoise;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush MediumVioletRed
        {
            get
            {
                mediumVioletRed ??= new XSolidBrush(XColors.MediumVioletRed, true);
                return mediumVioletRed;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush MidnightBlue
        {
            get
            {
                midnightBlue ??= new XSolidBrush(XColors.MidnightBlue, true);
                return midnightBlue;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush MintCream
        {
            get
            {
                mintCream ??= new XSolidBrush(XColors.MintCream, true);
                return mintCream;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush MistyRose
        {
            get
            {
                mistyRose ??= new XSolidBrush(XColors.MistyRose, true);
                return mistyRose;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Moccasin
        {
            get
            {
                moccasin ??= new XSolidBrush(XColors.Moccasin, true);
                return moccasin;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush NavajoWhite
        {
            get
            {
                navajoWhite ??= new XSolidBrush(XColors.NavajoWhite, true);
                return navajoWhite;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Navy
        {
            get
            {
                navy ??= new XSolidBrush(XColors.Navy, true);
                return navy;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush OldLace
        {
            get
            {
                oldLace ??= new XSolidBrush(XColors.OldLace, true);
                return oldLace;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Olive
        {
            get
            {
                olive ??= new XSolidBrush(XColors.Olive, true);
                return olive;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush OliveDrab
        {
            get
            {
                oliveDrab ??= new XSolidBrush(XColors.OliveDrab, true);
                return oliveDrab;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Orange
        {
            get
            {
                orange ??= new XSolidBrush(XColors.Orange, true);
                return orange;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush OrangeRed
        {
            get
            {
                orangeRed ??= new XSolidBrush(XColors.OrangeRed, true);
                return orangeRed;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Orchid
        {
            get
            {
                orchid ??= new XSolidBrush(XColors.Orchid, true);
                return orchid;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush PaleGoldenrod
        {
            get
            {
                paleGoldenrod ??= new XSolidBrush(XColors.PaleGoldenrod, true);
                return paleGoldenrod;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush PaleGreen
        {
            get
            {
                paleGreen ??= new XSolidBrush(XColors.PaleGreen, true);
                return paleGreen;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush PaleTurquoise
        {
            get
            {
                paleTurquoise ??= new XSolidBrush(XColors.PaleTurquoise, true);
                return paleTurquoise;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush PaleVioletRed
        {
            get
            {
                paleVioletRed ??= new XSolidBrush(XColors.PaleVioletRed, true);
                return paleVioletRed;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush PapayaWhip
        {
            get
            {
                papayaWhip ??= new XSolidBrush(XColors.PapayaWhip, true);
                return papayaWhip;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush PeachPuff
        {
            get
            {
                peachPuff ??= new XSolidBrush(XColors.PeachPuff, true);
                return peachPuff;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Peru
        {
            get
            {
                peru ??= new XSolidBrush(XColors.Peru, true);
                return peru;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Pink
        {
            get
            {
                pink ??= new XSolidBrush(XColors.Pink, true);
                return pink;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Plum
        {
            get
            {
                plum ??= new XSolidBrush(XColors.Plum, true);
                return plum;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush PowderBlue
        {
            get
            {
                powderBlue ??= new XSolidBrush(XColors.PowderBlue, true);
                return powderBlue;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Purple
        {
            get
            {
                purple ??= new XSolidBrush(XColors.Purple, true);
                return purple;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Red
        {
            get
            {
                red ??= new XSolidBrush(XColors.Red, true);
                return red;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush RosyBrown
        {
            get
            {
                rosyBrown ??= new XSolidBrush(XColors.RosyBrown, true);
                return rosyBrown;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush RoyalBlue
        {
            get
            {
                royalBlue ??= new XSolidBrush(XColors.RoyalBlue, true);
                return royalBlue;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush SaddleBrown
        {
            get
            {
                saddleBrown ??= new XSolidBrush(XColors.SaddleBrown, true);
                return saddleBrown;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Salmon
        {
            get
            {
                salmon ??= new XSolidBrush(XColors.Salmon, true);
                return salmon;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush SandyBrown
        {
            get
            {
                sandyBrown ??= new XSolidBrush(XColors.SandyBrown, true);
                return sandyBrown;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush SeaGreen
        {
            get
            {
                seaGreen ??= new XSolidBrush(XColors.SeaGreen, true);
                return seaGreen;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush SeaShell
        {
            get
            {
                seaShell ??= new XSolidBrush(XColors.SeaShell, true);
                return seaShell;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Sienna
        {
            get
            {
                sienna ??= new XSolidBrush(XColors.Sienna, true);
                return sienna;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Silver
        {
            get
            {
                silver ??= new XSolidBrush(XColors.Silver, true);
                return silver;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush SkyBlue
        {
            get
            {
                skyBlue ??= new XSolidBrush(XColors.SkyBlue, true);
                return skyBlue;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush SlateBlue
        {
            get
            {
                slateBlue ??= new XSolidBrush(XColors.SlateBlue, true);
                return slateBlue;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush SlateGray
        {
            get
            {
                slateGray ??= new XSolidBrush(XColors.SlateGray, true);
                return slateGray;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Snow
        {
            get
            {
                snow ??= new XSolidBrush(XColors.Snow, true);
                return snow;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush SpringGreen
        {
            get
            {
                springGreen ??= new XSolidBrush(XColors.SpringGreen, true);
                return springGreen;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush SteelBlue
        {
            get
            {
                steelBlue ??= new XSolidBrush(XColors.SteelBlue, true);
                return steelBlue;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Tan
        {
            get
            {
                tan ??= new XSolidBrush(XColors.Tan, true);
                return tan;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Teal
        {
            get
            {
                teal ??= new XSolidBrush(XColors.Teal, true);
                return teal;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Thistle
        {
            get
            {
                thistle ??= new XSolidBrush(XColors.Thistle, true);
                return thistle;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Tomato
        {
            get
            {
                tomato ??= new XSolidBrush(XColors.Tomato, true);
                return tomato;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Transparent
        {
            get
            {
                transparent ??= new XSolidBrush(XColors.Transparent, true);
                return transparent;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Turquoise
        {
            get
            {
                turquoise ??= new XSolidBrush(XColors.Turquoise, true);
                return turquoise;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Violet
        {
            get
            {
                violet ??= new XSolidBrush(XColors.Violet, true);
                return violet;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Wheat
        {
            get
            {
                wheat ??= new XSolidBrush(XColors.Wheat, true);
                return wheat;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush White
        {
            get
            {
                white ??= new XSolidBrush(XColors.White, true);
                return white;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush WhiteSmoke
        {
            get
            {
                whiteSmoke ??= new XSolidBrush(XColors.WhiteSmoke, true);
                return whiteSmoke;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Yellow
        {
            get
            {
                yellow ??= new XSolidBrush(XColors.Yellow, true);
                return yellow;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush YellowGreen
        {
            get
            {
                yellowGreen ??= new XSolidBrush(XColors.YellowGreen, true);
                return yellowGreen;
            }
        }

        private static XSolidBrush aliceBlue;
        private static XSolidBrush antiqueWhite;
        private static XSolidBrush aqua;
        private static XSolidBrush aquamarine;
        private static XSolidBrush azure;
        private static XSolidBrush beige;
        private static XSolidBrush bisque;
        private static XSolidBrush black;
        private static XSolidBrush blanchedAlmond;
        private static XSolidBrush blue;
        private static XSolidBrush blueViolet;
        private static XSolidBrush brown;
        private static XSolidBrush burlyWood;
        private static XSolidBrush cadetBlue;
        private static XSolidBrush chartreuse;
        private static XSolidBrush chocolate;
        private static XSolidBrush coral;
        private static XSolidBrush cornflowerBlue;
        private static XSolidBrush cornsilk;
        private static XSolidBrush crimson;
        private static XSolidBrush cyan;
        private static XSolidBrush darkBlue;
        private static XSolidBrush darkCyan;
        private static XSolidBrush darkGoldenrod;
        private static XSolidBrush darkGray;
        private static XSolidBrush darkGreen;
        private static XSolidBrush darkKhaki;
        private static XSolidBrush darkMagenta;
        private static XSolidBrush darkOliveGreen;
        private static XSolidBrush darkOrange;
        private static XSolidBrush darkOrchid;
        private static XSolidBrush darkRed;
        private static XSolidBrush darkSalmon;
        private static XSolidBrush darkSeaGreen;
        private static XSolidBrush darkSlateBlue;
        private static XSolidBrush darkSlateGray;
        private static XSolidBrush darkTurquoise;
        private static XSolidBrush darkViolet;
        private static XSolidBrush deepPink;
        private static XSolidBrush deepSkyBlue;
        private static XSolidBrush dimGray;
        private static XSolidBrush dodgerBlue;
        private static XSolidBrush firebrick;
        private static XSolidBrush floralWhite;
        private static XSolidBrush forestGreen;
        private static XSolidBrush fuchsia;
        private static XSolidBrush gainsboro;
        private static XSolidBrush ghostWhite;
        private static XSolidBrush gold;
        private static XSolidBrush goldenrod;
        private static XSolidBrush gray;
        private static XSolidBrush green;
        private static XSolidBrush greenYellow;
        private static XSolidBrush honeydew;
        private static XSolidBrush hotPink;
        private static XSolidBrush indianRed;
        private static XSolidBrush indigo;
        private static XSolidBrush ivory;
        private static XSolidBrush khaki;
        private static XSolidBrush lavender;
        private static XSolidBrush lavenderBlush;
        private static XSolidBrush lawnGreen;
        private static XSolidBrush lemonChiffon;
        private static XSolidBrush lightBlue;
        private static XSolidBrush lightCoral;
        private static XSolidBrush lightCyan;
        private static XSolidBrush lightGoldenrodYellow;
        private static XSolidBrush lightGray;
        private static XSolidBrush lightGreen;
        private static XSolidBrush lightPink;
        private static XSolidBrush lightSalmon;
        private static XSolidBrush lightSeaGreen;
        private static XSolidBrush lightSkyBlue;
        private static XSolidBrush lightSlateGray;
        private static XSolidBrush lightSteelBlue;
        private static XSolidBrush lightYellow;
        private static XSolidBrush lime;
        private static XSolidBrush limeGreen;
        private static XSolidBrush linen;
        private static XSolidBrush magenta;
        private static XSolidBrush maroon;
        private static XSolidBrush mediumAquamarine;
        private static XSolidBrush mediumBlue;
        private static XSolidBrush mediumOrchid;
        private static XSolidBrush mediumPurple;
        private static XSolidBrush mediumSeaGreen;
        private static XSolidBrush mediumSlateBlue;
        private static XSolidBrush mediumSpringGreen;
        private static XSolidBrush mediumTurquoise;
        private static XSolidBrush mediumVioletRed;
        private static XSolidBrush midnightBlue;
        private static XSolidBrush mintCream;
        private static XSolidBrush mistyRose;
        private static XSolidBrush moccasin;
        private static XSolidBrush navajoWhite;
        private static XSolidBrush navy;
        private static XSolidBrush oldLace;
        private static XSolidBrush olive;
        private static XSolidBrush oliveDrab;
        private static XSolidBrush orange;
        private static XSolidBrush orangeRed;
        private static XSolidBrush orchid;
        private static XSolidBrush paleGoldenrod;
        private static XSolidBrush paleGreen;
        private static XSolidBrush paleTurquoise;
        private static XSolidBrush paleVioletRed;
        private static XSolidBrush papayaWhip;
        private static XSolidBrush peachPuff;
        private static XSolidBrush peru;
        private static XSolidBrush pink;
        private static XSolidBrush plum;
        private static XSolidBrush powderBlue;
        private static XSolidBrush purple;
        private static XSolidBrush red;
        private static XSolidBrush rosyBrown;
        private static XSolidBrush royalBlue;
        private static XSolidBrush saddleBrown;
        private static XSolidBrush salmon;
        private static XSolidBrush sandyBrown;
        private static XSolidBrush seaGreen;
        private static XSolidBrush seaShell;
        private static XSolidBrush sienna;
        private static XSolidBrush silver;
        private static XSolidBrush skyBlue;
        private static XSolidBrush slateBlue;
        private static XSolidBrush slateGray;
        private static XSolidBrush snow;
        private static XSolidBrush springGreen;
        private static XSolidBrush steelBlue;
        private static XSolidBrush tan;
        private static XSolidBrush teal;
        private static XSolidBrush thistle;
        private static XSolidBrush tomato;
        private static XSolidBrush transparent;
        private static XSolidBrush turquoise;
        private static XSolidBrush violet;
        private static XSolidBrush wheat;
        private static XSolidBrush white;
        private static XSolidBrush whiteSmoke;
        private static XSolidBrush yellow;
        private static XSolidBrush yellowGreen;
    }
}
