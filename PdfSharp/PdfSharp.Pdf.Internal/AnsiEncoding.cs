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
using System.Diagnostics;
using System.Text;

namespace PdfSharp.Pdf.Internal
{
    /// <summary>
    /// An encoder for PDF AnsiEncoding.
    /// </summary>
    internal sealed class AnsiEncoding : Encoding
    {
        public AnsiEncoding()
        {
        }

        public override int GetByteCount(char[] chars, int index, int count)
        {
            return PdfEncoders.WinAnsiEncoding.GetByteCount(chars, index, count);
        }

        public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
        {
            byte[] ansi = PdfEncoders.WinAnsiEncoding.GetBytes(chars, charIndex, charCount);
            //for (int idx = 0, count = ansi.Length; count > 0; idx++, byteIndex++, count--)
            //  bytes[byteIndex] = AnsiToUnicode[ansi[idx]];
            return ansi.Length;
        }

        public override int GetCharCount(byte[] bytes, int index, int count)
        {
            //return PdfEncoders.WinAnsiEncoding.GetCharCount(bytes, index, count);
            Debug.Assert(PdfEncoders.WinAnsiEncoding.GetCharCount(bytes, index, count) == count);
            return count;
        }

        public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
        {
            throw new NotImplementedException("GetChars");
            //for (; byteCount > 0; byteIndex++, charIndex++, byteCount--)
            //  chars[charIndex] = '\ubytes[byteIndex];
            //return byteCount;
        }

        public override int GetMaxByteCount(int charCount)
        {
            return charCount;
        }

        public override int GetMaxCharCount(int byteCount)
        {
            return byteCount;
        }

        /// <summary>
        /// Indicates whether the specified Unicode character is available in the ANSI code page 1252.
        /// </summary>
        public static bool IsAnsi1252Char(char ch)
        {
            if (ch < '\u00FF') // HACK? 
                return true;
            return ch switch
            {
                '\u20AC' or '\u0081' or '\u201A' or '\u0192' or '\u201E' or '\u2026' or '\u2020' or '\u2021' or '\u02C6' or '\u2030' or '\u0160' or '\u2039' or '\u0152' or '\u008D' or '\u017D' or '\u008F' or '\u0090' or '\u2018' or '\u2019' or '\u201C' or '\u201D' or '\u2022' or '\u2013' or '\u2014' or '\u02DC' or '\u2122' or '\u0161' or '\u203A' or '\u0153' or '\u009D' or '\u017E' or '\u0178' => true,
                _ => false,
            };
        }

        /// <summary>
        /// Converts WinAnsi to Unicode characters.
        /// </summary>
        static readonly char[] AnsiToUnicode =
        [
      //          00        01        02        03        04        05        06        07        08        09        0A        0B        0C        0D        0E        0F
      /* 00 */ '\u0000', '\u0001', '\u0002', '\u0003', '\u0004', '\u0005', '\u0006', '\u0007', '\u0008', '\u0009', '\u000A', '\u000B', '\u000C', '\u000D', '\u000E', '\u000F',
      /* 10 */ '\u0010', '\u0011', '\u0012', '\u0013', '\u0014', '\u0015', '\u0016', '\u0017', '\u0018', '\u0019', '\u001A', '\u001B', '\u001C', '\u001D', '\u001E', '\u001F',
      /* 20 */ '\u0020', '\u0021', '\u0022', '\u0023', '\u0024', '\u0025', '\u0026', '\u0027', '\u0028', '\u0029', '\u002A', '\u002B', '\u002C', '\u002D', '\u002E', '\u002F',
      /* 30 */ '\u0030', '\u0031', '\u0032', '\u0033', '\u0034', '\u0035', '\u0036', '\u0037', '\u0038', '\u0039', '\u003A', '\u003B', '\u003C', '\u003D', '\u003E', '\u003F',
      /* 40 */ '\u0040', '\u0041', '\u0042', '\u0043', '\u0044', '\u0045', '\u0046', '\u0047', '\u0048', '\u0049', '\u004A', '\u004B', '\u004C', '\u004D', '\u004E', '\u004F',
      /* 50 */ '\u0050', '\u0051', '\u0052', '\u0053', '\u0054', '\u0055', '\u0056', '\u0057', '\u0058', '\u0059', '\u005A', '\u005B', '\u005C', '\u005D', '\u005E', '\u005F',
      /* 60 */ '\u0060', '\u0061', '\u0062', '\u0063', '\u0064', '\u0065', '\u0066', '\u0067', '\u0068', '\u0069', '\u006A', '\u006B', '\u006C', '\u006D', '\u006E', '\u006F',
      /* 70 */ '\u0070', '\u0071', '\u0072', '\u0073', '\u0074', '\u0075', '\u0076', '\u0077', '\u0078', '\u0079', '\u007A', '\u007B', '\u007C', '\u007D', '\u007E', '\u007F',
      /* 80 */ '\u20AC', '\u0081', '\u201A', '\u0192', '\u201E', '\u2026', '\u2020', '\u2021', '\u02C6', '\u2030', '\u0160', '\u2039', '\u0152', '\u008D', '\u017D', '\u008F',
      /* 90 */ '\u0090', '\u2018', '\u2019', '\u201C', '\u201D', '\u2022', '\u2013', '\u2014', '\u02DC', '\u2122', '\u0161', '\u203A', '\u0153', '\u009D', '\u017E', '\u0178',
      /* A0 */ '\u00A0', '\u00A1', '\u00A2', '\u00A3', '\u00A4', '\u00A5', '\u00A6', '\u00A7', '\u00A8', '\u00A9', '\u00AA', '\u00AB', '\u00AC', '\u00AD', '\u00AE', '\u00AF',
      /* B0 */ '\u00B0', '\u00B1', '\u00B2', '\u00B3', '\u00B4', '\u00B5', '\u00B6', '\u00B7', '\u00B8', '\u00B9', '\u00BA', '\u00BB', '\u00BC', '\u00BD', '\u00BE', '\u00BF',
      /* C0 */ '\u00C0', '\u00C1', '\u00C2', '\u00C3', '\u00C4', '\u00C5', '\u00C6', '\u00C7', '\u00C8', '\u00C9', '\u00CA', '\u00CB', '\u00CC', '\u00CD', '\u00CE', '\u00CF',
      /* D0 */ '\u00D0', '\u00D1', '\u00D2', '\u00D3', '\u00D4', '\u00D5', '\u00D6', '\u00D7', '\u00D8', '\u00D9', '\u00DA', '\u00DB', '\u00DC', '\u00DD', '\u00DE', '\u00DF',
      /* E0 */ '\u00E0', '\u00E1', '\u00E2', '\u00E3', '\u00E4', '\u00E5', '\u00E6', '\u00E7', '\u00E8', '\u00E9', '\u00EA', '\u00EB', '\u00EC', '\u00ED', '\u00EE', '\u00EF',
      /* F0 */ '\u00F0', '\u00F1', '\u00F2', '\u00F3', '\u00F4', '\u00F5', '\u00F6', '\u00F7', '\u00F8', '\u00F9', '\u00FA', '\u00FB', '\u00FC', '\u00FD', '\u00FE', '\u00FF',
        ];
    }
}