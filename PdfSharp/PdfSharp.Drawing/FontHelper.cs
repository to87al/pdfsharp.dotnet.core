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

using System;
using System.Collections.Generic;
using System.Globalization;
//#if GDI
//using System.Drawing;
//using System.Drawing.Drawing2D;
//using System.Drawing.Imaging;
//#endif
#if WPF
using System.Windows;
using System.Windows.Media;
#endif
using PdfSharp.Fonts;

namespace PdfSharp.Drawing
{
#if WPF
    /// <summary>
    /// The Get WPF Value flags.
    /// </summary>
    internal enum GWV
    {
        GetCellAscent,
        GetCellDescent,
        GetEmHeight,
        GetLineSpacing,
        //IsStyleAvailable
    }

    /// <summary>
    /// Helper class for fonts.
    /// </summary>
    internal static class FontHelper
    {
        //private const string testFontName = "Times New Roman";
        //const string testFontName = "Segoe Condensed";
        //const string testFontName = "Frutiger LT 45 Light";

        //static FontHelper()
        //{
        //  FontFamily fontFamily = new FontFamily(testFontName);
        //  s_typefaces = new List<Typeface>(fontFamily.GetTypefaces());
        //}

        //private static List<Typeface> s_typefaces;

        /// <summary>
        /// Creates a typeface.
        /// </summary>
        public static Typeface CreateTypeface(FontFamily family, XFontStyle style)
        {
            // BUG: does not work with fonts that have others than the four default styles
            FontStyle fontStyle = FontStyleFromStyle(style);
            FontWeight fontWeight = FontWeightFromStyle(style);
            Typeface typeface = new(family, fontStyle, fontWeight, FontStretches.Normal);

            //List<Typeface> typefaces = new List<Typeface>(fontFamily.GetTypefaces());
            //typefaces.GetType();
            //Typeface typeface = typefaces[3];

            return typeface;
        }

#if !SILVERLIGHT
        /// <summary>
        /// Creates the formatted text.
        /// </summary>
        public static FormattedText CreateFormattedText(string text, Typeface typeface, double emSize, Brush brush)
        {
            //FontFamily fontFamily = new FontFamily(testFontName);
            //typeface = new Typeface(fontFamily, FontStyles.Normal, FontWeights.Bold, FontStretches.Condensed);
            //List<Typeface> typefaces = new List<Typeface>(fontFamily.GetTypefaces());
            //typefaces.GetType();
            //typeface = s_typefaces[0];

            // BUG: does not work with fonts that have others than the four default styles
            FormattedText formattedText = new(text, new CultureInfo("en-us"), FlowDirection.LeftToRight, typeface, emSize, brush, 1.25);
            //formattedText.SetFontWeight(FontWeights.Bold);
            //formattedText.SetFontStyle(FontStyles.Oblique);
            //formattedText.SetFontStretch(FontStretches.Condensed);
            return formattedText;
        }
#endif

#if SILVERLIGHT
    /// <summary>
    /// Creates the TextBlock.
    /// </summary>
    public static TextBlock CreateTextBlock(string text, Typeface typeface, double emSize, Brush brush)
    {
      TextBlock textBlock = new TextBlock();
      textBlock.FontFamily = new FontFamily("Verdana");
      textBlock.FontSize = emSize;
      textBlock.Foreground = brush;
      textBlock.Text = text;

      return textBlock;
    }
#endif

        /// <summary>
        /// Simple hack to make it work...
        /// </summary>
        public static FontStyle FontStyleFromStyle(XFontStyle style)
        {
            return (style & XFontStyle.BoldItalic) switch  // mask out Underline and Strikeout
            {
                XFontStyle.Regular => FontStyles.Normal,
                XFontStyle.Bold => FontStyles.Normal,
                XFontStyle.Italic => FontStyles.Italic,
                XFontStyle.BoldItalic => FontStyles.Italic,
                _ => FontStyles.Normal,
            };
        }

        /// <summary>
        /// Simple hack to make it work...
        /// </summary>
        public static FontWeight FontWeightFromStyle(XFontStyle style)
        {
            return style switch
            {
                XFontStyle.Regular => FontWeights.Normal,
                XFontStyle.Bold => FontWeights.Bold,
                XFontStyle.Italic => FontWeights.Normal,
                XFontStyle.BoldItalic => FontWeights.Bold,
                _ => FontWeights.Normal,
            };
        }

        public static int GetWpfValue(XFontFamily family, XFontStyle style, GWV value)
        {
            FontDescriptor descriptor = FontDescriptorStock.Global.CreateDescriptor(family, style);
            XFontMetrics metrics = descriptor.FontMetrics;

            return value switch
            {
                GWV.GetCellAscent => metrics.Ascent,
                GWV.GetCellDescent => Math.Abs(metrics.Descent),
                GWV.GetEmHeight => metrics.UnitsPerEm,//return (int)metrics.CapHeight;
                GWV.GetLineSpacing => metrics.Ascent + Math.Abs(metrics.Descent) + metrics.Leading,
                _ => throw new InvalidOperationException("Unknown GWV value."),
            };
        }

        /// <summary>
        /// Determines whether the style is available as a glyph type face in the specified font family, i.e. the specified style is not simulated.
        /// </summary>
        public static bool IsStyleAvailable(XFontFamily family, XFontStyle style)
        {
#if !SILVERLIGHT
            // TODOWPF: check for correctness
            FontDescriptor descriptor = FontDescriptorStock.Global.CreateDescriptor(family, style);
            XFontMetrics metrics = descriptor.FontMetrics;

            style &= XFontStyle.Regular | XFontStyle.Bold | XFontStyle.Italic | XFontStyle.BoldItalic; // same as XFontStyle.BoldItalic
            List<Typeface> typefaces = new(family.wpfFamily.GetTypefaces());
            foreach (Typeface typeface in typefaces)
            {
                bool available = false;
                if (typeface.TryGetGlyphTypeface(out GlyphTypeface glyphTypeface))
                {
#if DEBUG
          glyphTypeface.GetType();
#endif
                    available = true;
                }

#if DEBUG_ // 
        int weightClass = typeface.Weight.ToOpenTypeWeight();
        switch (style)
        {
          case XFontStyle.Regular:
            //if (typeface.TryGetGlyphTypeface(.Style == FontStyles.Normal && typeface.Weight== FontWeights.Normal.)
            break;

          case XFontStyle.Bold:
            break;

          case XFontStyle.Italic:
            break;

          case XFontStyle.BoldItalic:
            break;
        }
#endif
                if (available)
                    return true;
            }
            return false;
#else
      return true; // AGHACK
#endif
        }
    }
#endif
}