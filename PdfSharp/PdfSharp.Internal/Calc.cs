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
#if GDI
using System.Drawing;
#endif
#if WPF
#endif
using PdfSharp.Drawing;

namespace PdfSharp.Internal
{
    /// <summary>
    /// Some static helper functions for calculations.
    /// </summary>
    internal static class Calc
    {
        /// <summary>
        /// Degree to radiant factor.
        /// </summary>
        public const double Deg2Rad = Math.PI / 180;

        /// <summary>
        /// Half of pi.
        /// </summary>
        public const double πHalf = Math.PI / 2;
        // α - β κ

        /// <summary>
        /// Get page size in point from specified PageSize.
        /// </summary>
        public static XSize PageSizeToSize(PageSize value)
        {
            return value switch
            {
                PageSize.A0 => new XSize(2380, 3368),
                PageSize.A1 => new XSize(1684, 2380),
                PageSize.A2 => new XSize(1190, 1684),
                PageSize.A3 => new XSize(842, 1190),
                PageSize.A4 => new XSize(595, 842),
                PageSize.A5 => new XSize(420, 595),
                PageSize.B4 => new XSize(729, 1032),
                PageSize.B5 => new XSize(516, 729),
                // The strange sizes from overseas...
                PageSize.Letter => new XSize(612, 792),
                PageSize.Legal => new XSize(612, 1008),
                PageSize.Tabloid => new XSize(792, 1224),
                PageSize.Ledger => new XSize(1224, 792),
                PageSize.Statement => new XSize(396, 612),
                PageSize.Executive => new XSize(540, 720),
                PageSize.Folio => new XSize(612, 936),
                PageSize.Quarto => new XSize(610, 780),
                PageSize.Size10x14 => new XSize(720, 1008),
                _ => throw new ArgumentException("Invalid PageSize."),
            };
        }
    }
}