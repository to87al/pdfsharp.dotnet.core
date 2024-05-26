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
                XBrushes.aliceBlue ??= new XSolidBrush(XColors.AliceBlue, true);
                return XBrushes.aliceBlue;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush AntiqueWhite
        {
            get
            {
                XBrushes.antiqueWhite ??= new XSolidBrush(XColors.AntiqueWhite, true);
                return XBrushes.antiqueWhite;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Aqua
        {
            get
            {
                XBrushes.aqua ??= new XSolidBrush(XColors.Aqua, true);
                return XBrushes.aqua;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Aquamarine
        {
            get
            {
                XBrushes.aquamarine ??= new XSolidBrush(XColors.Aquamarine, true);
                return XBrushes.aquamarine;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Azure
        {
            get
            {
                XBrushes.azure ??= new XSolidBrush(XColors.Azure, true);
                return XBrushes.azure;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Beige
        {
            get
            {
                XBrushes.beige ??= new XSolidBrush(XColors.Beige, true);
                return XBrushes.beige;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Bisque
        {
            get
            {
                XBrushes.bisque ??= new XSolidBrush(XColors.Bisque, true);
                return XBrushes.bisque;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Black
        {
            get
            {
                XBrushes.black ??= new XSolidBrush(XColors.Black, true);
                return XBrushes.black;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush BlanchedAlmond
        {
            get
            {
                XBrushes.blanchedAlmond ??= new XSolidBrush(XColors.BlanchedAlmond, true);
                return XBrushes.blanchedAlmond;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Blue
        {
            get
            {
                XBrushes.blue ??= new XSolidBrush(XColors.Blue, true);
                return XBrushes.blue;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush BlueViolet
        {
            get
            {
                XBrushes.blueViolet ??= new XSolidBrush(XColors.BlueViolet, true);
                return XBrushes.blueViolet;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Brown
        {
            get
            {
                XBrushes.brown ??= new XSolidBrush(XColors.Brown, true);
                return XBrushes.brown;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush BurlyWood
        {
            get
            {
                XBrushes.burlyWood ??= new XSolidBrush(XColors.BurlyWood, true);
                return XBrushes.burlyWood;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush CadetBlue
        {
            get
            {
                XBrushes.cadetBlue ??= new XSolidBrush(XColors.CadetBlue, true);
                return XBrushes.cadetBlue;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Chartreuse
        {
            get
            {
                XBrushes.chartreuse ??= new XSolidBrush(XColors.Chartreuse, true);
                return XBrushes.chartreuse;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Chocolate
        {
            get
            {
                XBrushes.chocolate ??= new XSolidBrush(XColors.Chocolate, true);
                return XBrushes.chocolate;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Coral
        {
            get
            {
                XBrushes.coral ??= new XSolidBrush(XColors.Coral, true);
                return XBrushes.coral;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush CornflowerBlue
        {
            get
            {
                XBrushes.cornflowerBlue ??= new XSolidBrush(XColors.CornflowerBlue, true);
                return XBrushes.cornflowerBlue;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Cornsilk
        {
            get
            {
                XBrushes.cornsilk ??= new XSolidBrush(XColors.Cornsilk, true);
                return XBrushes.cornsilk;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Crimson
        {
            get
            {
                XBrushes.crimson ??= new XSolidBrush(XColors.Crimson, true);
                return XBrushes.crimson;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Cyan
        {
            get
            {
                XBrushes.cyan ??= new XSolidBrush(XColors.Cyan, true);
                return XBrushes.cyan;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush DarkBlue
        {
            get
            {
                XBrushes.darkBlue ??= new XSolidBrush(XColors.DarkBlue, true);
                return XBrushes.darkBlue;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush DarkCyan
        {
            get
            {
                XBrushes.darkCyan ??= new XSolidBrush(XColors.DarkCyan, true);
                return XBrushes.darkCyan;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush DarkGoldenrod
        {
            get
            {
                XBrushes.darkGoldenrod ??= new XSolidBrush(XColors.DarkGoldenrod, true);
                return XBrushes.darkGoldenrod;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush DarkGray
        {
            get
            {
                XBrushes.darkGray ??= new XSolidBrush(XColors.DarkGray, true);
                return XBrushes.darkGray;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush DarkGreen
        {
            get
            {
                XBrushes.darkGreen ??= new XSolidBrush(XColors.DarkGreen, true);
                return XBrushes.darkGreen;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush DarkKhaki
        {
            get
            {
                XBrushes.darkKhaki ??= new XSolidBrush(XColors.DarkKhaki, true);
                return XBrushes.darkKhaki;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush DarkMagenta
        {
            get
            {
                XBrushes.darkMagenta ??= new XSolidBrush(XColors.DarkMagenta, true);
                return XBrushes.darkMagenta;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush DarkOliveGreen
        {
            get
            {
                XBrushes.darkOliveGreen ??= new XSolidBrush(XColors.DarkOliveGreen, true);
                return XBrushes.darkOliveGreen;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush DarkOrange
        {
            get
            {
                XBrushes.darkOrange ??= new XSolidBrush(XColors.DarkOrange, true);
                return XBrushes.darkOrange;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush DarkOrchid
        {
            get
            {
                XBrushes.darkOrchid ??= new XSolidBrush(XColors.DarkOrchid, true);
                return XBrushes.darkOrchid;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush DarkRed
        {
            get
            {
                XBrushes.darkRed ??= new XSolidBrush(XColors.DarkRed, true);
                return XBrushes.darkRed;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush DarkSalmon
        {
            get
            {
                XBrushes.darkSalmon ??= new XSolidBrush(XColors.DarkSalmon, true);
                return XBrushes.darkSalmon;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush DarkSeaGreen
        {
            get
            {
                XBrushes.darkSeaGreen ??= new XSolidBrush(XColors.DarkSeaGreen, true);
                return XBrushes.darkSeaGreen;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush DarkSlateBlue
        {
            get
            {
                XBrushes.darkSlateBlue ??= new XSolidBrush(XColors.DarkSlateBlue, true);
                return XBrushes.darkSlateBlue;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush DarkSlateGray
        {
            get
            {
                XBrushes.darkSlateGray ??= new XSolidBrush(XColors.DarkSlateGray, true);
                return XBrushes.darkSlateGray;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush DarkTurquoise
        {
            get
            {
                XBrushes.darkTurquoise ??= new XSolidBrush(XColors.DarkTurquoise, true);
                return XBrushes.darkTurquoise;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush DarkViolet
        {
            get
            {
                XBrushes.darkViolet ??= new XSolidBrush(XColors.DarkViolet, true);
                return XBrushes.darkViolet;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush DeepPink
        {
            get
            {
                XBrushes.deepPink ??= new XSolidBrush(XColors.DeepPink, true);
                return XBrushes.deepPink;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush DeepSkyBlue
        {
            get
            {
                XBrushes.deepSkyBlue ??= new XSolidBrush(XColors.DeepSkyBlue, true);
                return XBrushes.deepSkyBlue;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush DimGray
        {
            get
            {
                XBrushes.dimGray ??= new XSolidBrush(XColors.DimGray, true);
                return XBrushes.dimGray;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush DodgerBlue
        {
            get
            {
                XBrushes.dodgerBlue ??= new XSolidBrush(XColors.DodgerBlue, true);
                return XBrushes.dodgerBlue;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Firebrick
        {
            get
            {
                XBrushes.firebrick ??= new XSolidBrush(XColors.Firebrick, true);
                return XBrushes.firebrick;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush FloralWhite
        {
            get
            {
                XBrushes.floralWhite ??= new XSolidBrush(XColors.FloralWhite, true);
                return XBrushes.floralWhite;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush ForestGreen
        {
            get
            {
                XBrushes.forestGreen ??= new XSolidBrush(XColors.ForestGreen, true);
                return XBrushes.forestGreen;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Fuchsia
        {
            get
            {
                XBrushes.fuchsia ??= new XSolidBrush(XColors.Fuchsia, true);
                return XBrushes.fuchsia;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Gainsboro
        {
            get
            {
                XBrushes.gainsboro ??= new XSolidBrush(XColors.Gainsboro, true);
                return XBrushes.gainsboro;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush GhostWhite
        {
            get
            {
                XBrushes.ghostWhite ??= new XSolidBrush(XColors.GhostWhite, true);
                return XBrushes.ghostWhite;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Gold
        {
            get
            {
                XBrushes.gold ??= new XSolidBrush(XColors.Gold, true);
                return XBrushes.gold;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Goldenrod
        {
            get
            {
                XBrushes.goldenrod ??= new XSolidBrush(XColors.Goldenrod, true);
                return XBrushes.goldenrod;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Gray
        {
            get
            {
                XBrushes.gray ??= new XSolidBrush(XColors.Gray, true);
                return XBrushes.gray;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Green
        {
            get
            {
                XBrushes.green ??= new XSolidBrush(XColors.Green, true);
                return XBrushes.green;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush GreenYellow
        {
            get
            {
                XBrushes.greenYellow ??= new XSolidBrush(XColors.GreenYellow, true);
                return XBrushes.greenYellow;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Honeydew
        {
            get
            {
                XBrushes.honeydew ??= new XSolidBrush(XColors.Honeydew, true);
                return XBrushes.honeydew;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush HotPink
        {
            get
            {
                XBrushes.hotPink ??= new XSolidBrush(XColors.HotPink, true);
                return XBrushes.hotPink;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush IndianRed
        {
            get
            {
                XBrushes.indianRed ??= new XSolidBrush(XColors.IndianRed, true);
                return XBrushes.indianRed;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Indigo
        {
            get
            {
                XBrushes.indigo ??= new XSolidBrush(XColors.Indigo, true);
                return XBrushes.indigo;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Ivory
        {
            get
            {
                XBrushes.ivory ??= new XSolidBrush(XColors.Ivory, true);
                return XBrushes.ivory;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Khaki
        {
            get
            {
                XBrushes.khaki ??= new XSolidBrush(XColors.Khaki, true);
                return XBrushes.khaki;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Lavender
        {
            get
            {
                XBrushes.lavender ??= new XSolidBrush(XColors.Lavender, true);
                return XBrushes.lavender;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush LavenderBlush
        {
            get
            {
                XBrushes.lavenderBlush ??= new XSolidBrush(XColors.LavenderBlush, true);
                return XBrushes.lavenderBlush;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush LawnGreen
        {
            get
            {
                XBrushes.lawnGreen ??= new XSolidBrush(XColors.LawnGreen, true);
                return XBrushes.lawnGreen;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush LemonChiffon
        {
            get
            {
                XBrushes.lemonChiffon ??= new XSolidBrush(XColors.LemonChiffon, true);
                return XBrushes.lemonChiffon;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush LightBlue
        {
            get
            {
                XBrushes.lightBlue ??= new XSolidBrush(XColors.LightBlue, true);
                return XBrushes.lightBlue;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush LightCoral
        {
            get
            {
                XBrushes.lightCoral ??= new XSolidBrush(XColors.LightCoral, true);
                return XBrushes.lightCoral;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush LightCyan
        {
            get
            {
                XBrushes.lightCyan ??= new XSolidBrush(XColors.LightCyan, true);
                return XBrushes.lightCyan;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush LightGoldenrodYellow
        {
            get
            {
                XBrushes.lightGoldenrodYellow ??= new XSolidBrush(XColors.LightGoldenrodYellow, true);
                return XBrushes.lightGoldenrodYellow;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush LightGray
        {
            get
            {
                XBrushes.lightGray ??= new XSolidBrush(XColors.LightGray, true);
                return XBrushes.lightGray;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush LightGreen
        {
            get
            {
                XBrushes.lightGreen ??= new XSolidBrush(XColors.LightGreen, true);
                return XBrushes.lightGreen;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush LightPink
        {
            get
            {
                XBrushes.lightPink ??= new XSolidBrush(XColors.LightPink, true);
                return XBrushes.lightPink;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush LightSalmon
        {
            get
            {
                XBrushes.lightSalmon ??= new XSolidBrush(XColors.LightSalmon, true);
                return XBrushes.lightSalmon;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush LightSeaGreen
        {
            get
            {
                XBrushes.lightSeaGreen ??= new XSolidBrush(XColors.LightSeaGreen, true);
                return XBrushes.lightSeaGreen;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush LightSkyBlue
        {
            get
            {
                XBrushes.lightSkyBlue ??= new XSolidBrush(XColors.LightSkyBlue, true);
                return XBrushes.lightSkyBlue;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush LightSlateGray
        {
            get
            {
                XBrushes.lightSlateGray ??= new XSolidBrush(XColors.LightSlateGray, true);
                return XBrushes.lightSlateGray;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush LightSteelBlue
        {
            get
            {
                XBrushes.lightSteelBlue ??= new XSolidBrush(XColors.LightSteelBlue, true);
                return XBrushes.lightSteelBlue;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush LightYellow
        {
            get
            {
                XBrushes.lightYellow ??= new XSolidBrush(XColors.LightYellow, true);
                return XBrushes.lightYellow;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Lime
        {
            get
            {
                XBrushes.lime ??= new XSolidBrush(XColors.Lime, true);
                return XBrushes.lime;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush LimeGreen
        {
            get
            {
                XBrushes.limeGreen ??= new XSolidBrush(XColors.LimeGreen, true);
                return XBrushes.limeGreen;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Linen
        {
            get
            {
                XBrushes.linen ??= new XSolidBrush(XColors.Linen, true);
                return XBrushes.linen;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Magenta
        {
            get
            {
                XBrushes.magenta ??= new XSolidBrush(XColors.Magenta, true);
                return XBrushes.magenta;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Maroon
        {
            get
            {
                XBrushes.maroon ??= new XSolidBrush(XColors.Maroon, true);
                return XBrushes.maroon;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush MediumAquamarine
        {
            get
            {
                XBrushes.mediumAquamarine ??= new XSolidBrush(XColors.MediumAquamarine, true);
                return XBrushes.mediumAquamarine;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush MediumBlue
        {
            get
            {
                XBrushes.mediumBlue ??= new XSolidBrush(XColors.MediumBlue, true);
                return XBrushes.mediumBlue;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush MediumOrchid
        {
            get
            {
                XBrushes.mediumOrchid ??= new XSolidBrush(XColors.MediumOrchid, true);
                return XBrushes.mediumOrchid;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush MediumPurple
        {
            get
            {
                XBrushes.mediumPurple ??= new XSolidBrush(XColors.MediumPurple, true);
                return XBrushes.mediumPurple;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush MediumSeaGreen
        {
            get
            {
                XBrushes.mediumSeaGreen ??= new XSolidBrush(XColors.MediumSeaGreen, true);
                return XBrushes.mediumSeaGreen;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush MediumSlateBlue
        {
            get
            {
                XBrushes.mediumSlateBlue ??= new XSolidBrush(XColors.MediumSlateBlue, true);
                return XBrushes.mediumSlateBlue;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush MediumSpringGreen
        {
            get
            {
                XBrushes.mediumSpringGreen ??= new XSolidBrush(XColors.MediumSpringGreen, true);
                return XBrushes.mediumSpringGreen;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush MediumTurquoise
        {
            get
            {
                XBrushes.mediumTurquoise ??= new XSolidBrush(XColors.MediumTurquoise, true);
                return XBrushes.mediumTurquoise;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush MediumVioletRed
        {
            get
            {
                XBrushes.mediumVioletRed ??= new XSolidBrush(XColors.MediumVioletRed, true);
                return XBrushes.mediumVioletRed;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush MidnightBlue
        {
            get
            {
                XBrushes.midnightBlue ??= new XSolidBrush(XColors.MidnightBlue, true);
                return XBrushes.midnightBlue;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush MintCream
        {
            get
            {
                XBrushes.mintCream ??= new XSolidBrush(XColors.MintCream, true);
                return XBrushes.mintCream;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush MistyRose
        {
            get
            {
                XBrushes.mistyRose ??= new XSolidBrush(XColors.MistyRose, true);
                return XBrushes.mistyRose;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Moccasin
        {
            get
            {
                XBrushes.moccasin ??= new XSolidBrush(XColors.Moccasin, true);
                return XBrushes.moccasin;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush NavajoWhite
        {
            get
            {
                XBrushes.navajoWhite ??= new XSolidBrush(XColors.NavajoWhite, true);
                return XBrushes.navajoWhite;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Navy
        {
            get
            {
                XBrushes.navy ??= new XSolidBrush(XColors.Navy, true);
                return XBrushes.navy;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush OldLace
        {
            get
            {
                XBrushes.oldLace ??= new XSolidBrush(XColors.OldLace, true);
                return XBrushes.oldLace;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Olive
        {
            get
            {
                XBrushes.olive ??= new XSolidBrush(XColors.Olive, true);
                return XBrushes.olive;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush OliveDrab
        {
            get
            {
                XBrushes.oliveDrab ??= new XSolidBrush(XColors.OliveDrab, true);
                return XBrushes.oliveDrab;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Orange
        {
            get
            {
                XBrushes.orange ??= new XSolidBrush(XColors.Orange, true);
                return XBrushes.orange;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush OrangeRed
        {
            get
            {
                XBrushes.orangeRed ??= new XSolidBrush(XColors.OrangeRed, true);
                return XBrushes.orangeRed;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Orchid
        {
            get
            {
                XBrushes.orchid ??= new XSolidBrush(XColors.Orchid, true);
                return XBrushes.orchid;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush PaleGoldenrod
        {
            get
            {
                XBrushes.paleGoldenrod ??= new XSolidBrush(XColors.PaleGoldenrod, true);
                return XBrushes.paleGoldenrod;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush PaleGreen
        {
            get
            {
                XBrushes.paleGreen ??= new XSolidBrush(XColors.PaleGreen, true);
                return XBrushes.paleGreen;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush PaleTurquoise
        {
            get
            {
                XBrushes.paleTurquoise ??= new XSolidBrush(XColors.PaleTurquoise, true);
                return XBrushes.paleTurquoise;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush PaleVioletRed
        {
            get
            {
                XBrushes.paleVioletRed ??= new XSolidBrush(XColors.PaleVioletRed, true);
                return XBrushes.paleVioletRed;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush PapayaWhip
        {
            get
            {
                XBrushes.papayaWhip ??= new XSolidBrush(XColors.PapayaWhip, true);
                return XBrushes.papayaWhip;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush PeachPuff
        {
            get
            {
                XBrushes.peachPuff ??= new XSolidBrush(XColors.PeachPuff, true);
                return XBrushes.peachPuff;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Peru
        {
            get
            {
                XBrushes.peru ??= new XSolidBrush(XColors.Peru, true);
                return XBrushes.peru;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Pink
        {
            get
            {
                XBrushes.pink ??= new XSolidBrush(XColors.Pink, true);
                return XBrushes.pink;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Plum
        {
            get
            {
                XBrushes.plum ??= new XSolidBrush(XColors.Plum, true);
                return XBrushes.plum;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush PowderBlue
        {
            get
            {
                XBrushes.powderBlue ??= new XSolidBrush(XColors.PowderBlue, true);
                return XBrushes.powderBlue;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Purple
        {
            get
            {
                XBrushes.purple ??= new XSolidBrush(XColors.Purple, true);
                return XBrushes.purple;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Red
        {
            get
            {
                XBrushes.red ??= new XSolidBrush(XColors.Red, true);
                return XBrushes.red;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush RosyBrown
        {
            get
            {
                XBrushes.rosyBrown ??= new XSolidBrush(XColors.RosyBrown, true);
                return XBrushes.rosyBrown;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush RoyalBlue
        {
            get
            {
                XBrushes.royalBlue ??= new XSolidBrush(XColors.RoyalBlue, true);
                return XBrushes.royalBlue;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush SaddleBrown
        {
            get
            {
                XBrushes.saddleBrown ??= new XSolidBrush(XColors.SaddleBrown, true);
                return XBrushes.saddleBrown;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Salmon
        {
            get
            {
                XBrushes.salmon ??= new XSolidBrush(XColors.Salmon, true);
                return XBrushes.salmon;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush SandyBrown
        {
            get
            {
                XBrushes.sandyBrown ??= new XSolidBrush(XColors.SandyBrown, true);
                return XBrushes.sandyBrown;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush SeaGreen
        {
            get
            {
                XBrushes.seaGreen ??= new XSolidBrush(XColors.SeaGreen, true);
                return XBrushes.seaGreen;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush SeaShell
        {
            get
            {
                XBrushes.seaShell ??= new XSolidBrush(XColors.SeaShell, true);
                return XBrushes.seaShell;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Sienna
        {
            get
            {
                XBrushes.sienna ??= new XSolidBrush(XColors.Sienna, true);
                return XBrushes.sienna;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Silver
        {
            get
            {
                XBrushes.silver ??= new XSolidBrush(XColors.Silver, true);
                return XBrushes.silver;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush SkyBlue
        {
            get
            {
                XBrushes.skyBlue ??= new XSolidBrush(XColors.SkyBlue, true);
                return XBrushes.skyBlue;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush SlateBlue
        {
            get
            {
                XBrushes.slateBlue ??= new XSolidBrush(XColors.SlateBlue, true);
                return XBrushes.slateBlue;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush SlateGray
        {
            get
            {
                XBrushes.slateGray ??= new XSolidBrush(XColors.SlateGray, true);
                return XBrushes.slateGray;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Snow
        {
            get
            {
                XBrushes.snow ??= new XSolidBrush(XColors.Snow, true);
                return XBrushes.snow;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush SpringGreen
        {
            get
            {
                XBrushes.springGreen ??= new XSolidBrush(XColors.SpringGreen, true);
                return XBrushes.springGreen;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush SteelBlue
        {
            get
            {
                XBrushes.steelBlue ??= new XSolidBrush(XColors.SteelBlue, true);
                return XBrushes.steelBlue;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Tan
        {
            get
            {
                XBrushes.tan ??= new XSolidBrush(XColors.Tan, true);
                return XBrushes.tan;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Teal
        {
            get
            {
                XBrushes.teal ??= new XSolidBrush(XColors.Teal, true);
                return XBrushes.teal;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Thistle
        {
            get
            {
                XBrushes.thistle ??= new XSolidBrush(XColors.Thistle, true);
                return XBrushes.thistle;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Tomato
        {
            get
            {
                XBrushes.tomato ??= new XSolidBrush(XColors.Tomato, true);
                return XBrushes.tomato;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Transparent
        {
            get
            {
                XBrushes.transparent ??= new XSolidBrush(XColors.Transparent, true);
                return XBrushes.transparent;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Turquoise
        {
            get
            {
                XBrushes.turquoise ??= new XSolidBrush(XColors.Turquoise, true);
                return XBrushes.turquoise;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Violet
        {
            get
            {
                XBrushes.violet ??= new XSolidBrush(XColors.Violet, true);
                return XBrushes.violet;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Wheat
        {
            get
            {
                XBrushes.wheat ??= new XSolidBrush(XColors.Wheat, true);
                return XBrushes.wheat;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush White
        {
            get
            {
                XBrushes.white ??= new XSolidBrush(XColors.White, true);
                return XBrushes.white;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush WhiteSmoke
        {
            get
            {
                XBrushes.whiteSmoke ??= new XSolidBrush(XColors.WhiteSmoke, true);
                return XBrushes.whiteSmoke;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush Yellow
        {
            get
            {
                XBrushes.yellow ??= new XSolidBrush(XColors.Yellow, true);
                return XBrushes.yellow;
            }
        }

        /// <summary>Gets a pre-defined XBrush object.</summary>
        public static XSolidBrush YellowGreen
        {
            get
            {
                XBrushes.yellowGreen ??= new XSolidBrush(XColors.YellowGreen, true);
                return XBrushes.yellowGreen;
            }
        }

        static XSolidBrush aliceBlue;
        static XSolidBrush antiqueWhite;
        static XSolidBrush aqua;
        static XSolidBrush aquamarine;
        static XSolidBrush azure;
        static XSolidBrush beige;
        static XSolidBrush bisque;
        static XSolidBrush black;
        static XSolidBrush blanchedAlmond;
        static XSolidBrush blue;
        static XSolidBrush blueViolet;
        static XSolidBrush brown;
        static XSolidBrush burlyWood;
        static XSolidBrush cadetBlue;
        static XSolidBrush chartreuse;
        static XSolidBrush chocolate;
        static XSolidBrush coral;
        static XSolidBrush cornflowerBlue;
        static XSolidBrush cornsilk;
        static XSolidBrush crimson;
        static XSolidBrush cyan;
        static XSolidBrush darkBlue;
        static XSolidBrush darkCyan;
        static XSolidBrush darkGoldenrod;
        static XSolidBrush darkGray;
        static XSolidBrush darkGreen;
        static XSolidBrush darkKhaki;
        static XSolidBrush darkMagenta;
        static XSolidBrush darkOliveGreen;
        static XSolidBrush darkOrange;
        static XSolidBrush darkOrchid;
        static XSolidBrush darkRed;
        static XSolidBrush darkSalmon;
        static XSolidBrush darkSeaGreen;
        static XSolidBrush darkSlateBlue;
        static XSolidBrush darkSlateGray;
        static XSolidBrush darkTurquoise;
        static XSolidBrush darkViolet;
        static XSolidBrush deepPink;
        static XSolidBrush deepSkyBlue;
        static XSolidBrush dimGray;
        static XSolidBrush dodgerBlue;
        static XSolidBrush firebrick;
        static XSolidBrush floralWhite;
        static XSolidBrush forestGreen;
        static XSolidBrush fuchsia;
        static XSolidBrush gainsboro;
        static XSolidBrush ghostWhite;
        static XSolidBrush gold;
        static XSolidBrush goldenrod;
        static XSolidBrush gray;
        static XSolidBrush green;
        static XSolidBrush greenYellow;
        static XSolidBrush honeydew;
        static XSolidBrush hotPink;
        static XSolidBrush indianRed;
        static XSolidBrush indigo;
        static XSolidBrush ivory;
        static XSolidBrush khaki;
        static XSolidBrush lavender;
        static XSolidBrush lavenderBlush;
        static XSolidBrush lawnGreen;
        static XSolidBrush lemonChiffon;
        static XSolidBrush lightBlue;
        static XSolidBrush lightCoral;
        static XSolidBrush lightCyan;
        static XSolidBrush lightGoldenrodYellow;
        static XSolidBrush lightGray;
        static XSolidBrush lightGreen;
        static XSolidBrush lightPink;
        static XSolidBrush lightSalmon;
        static XSolidBrush lightSeaGreen;
        static XSolidBrush lightSkyBlue;
        static XSolidBrush lightSlateGray;
        static XSolidBrush lightSteelBlue;
        static XSolidBrush lightYellow;
        static XSolidBrush lime;
        static XSolidBrush limeGreen;
        static XSolidBrush linen;
        static XSolidBrush magenta;
        static XSolidBrush maroon;
        static XSolidBrush mediumAquamarine;
        static XSolidBrush mediumBlue;
        static XSolidBrush mediumOrchid;
        static XSolidBrush mediumPurple;
        static XSolidBrush mediumSeaGreen;
        static XSolidBrush mediumSlateBlue;
        static XSolidBrush mediumSpringGreen;
        static XSolidBrush mediumTurquoise;
        static XSolidBrush mediumVioletRed;
        static XSolidBrush midnightBlue;
        static XSolidBrush mintCream;
        static XSolidBrush mistyRose;
        static XSolidBrush moccasin;
        static XSolidBrush navajoWhite;
        static XSolidBrush navy;
        static XSolidBrush oldLace;
        static XSolidBrush olive;
        static XSolidBrush oliveDrab;
        static XSolidBrush orange;
        static XSolidBrush orangeRed;
        static XSolidBrush orchid;
        static XSolidBrush paleGoldenrod;
        static XSolidBrush paleGreen;
        static XSolidBrush paleTurquoise;
        static XSolidBrush paleVioletRed;
        static XSolidBrush papayaWhip;
        static XSolidBrush peachPuff;
        static XSolidBrush peru;
        static XSolidBrush pink;
        static XSolidBrush plum;
        static XSolidBrush powderBlue;
        static XSolidBrush purple;
        static XSolidBrush red;
        static XSolidBrush rosyBrown;
        static XSolidBrush royalBlue;
        static XSolidBrush saddleBrown;
        static XSolidBrush salmon;
        static XSolidBrush sandyBrown;
        static XSolidBrush seaGreen;
        static XSolidBrush seaShell;
        static XSolidBrush sienna;
        static XSolidBrush silver;
        static XSolidBrush skyBlue;
        static XSolidBrush slateBlue;
        static XSolidBrush slateGray;
        static XSolidBrush snow;
        static XSolidBrush springGreen;
        static XSolidBrush steelBlue;
        static XSolidBrush tan;
        static XSolidBrush teal;
        static XSolidBrush thistle;
        static XSolidBrush tomato;
        static XSolidBrush transparent;
        static XSolidBrush turquoise;
        static XSolidBrush violet;
        static XSolidBrush wheat;
        static XSolidBrush white;
        static XSolidBrush whiteSmoke;
        static XSolidBrush yellow;
        static XSolidBrush yellowGreen;
    }
}
