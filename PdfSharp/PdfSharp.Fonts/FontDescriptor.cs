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

#if GDI
using System.Drawing;
using System.Drawing.Drawing2D;
#endif
#if WPF
#endif
using PdfSharp.Drawing;

namespace PdfSharp.Fonts
{
    /// <summary>
    /// Base class for all font descriptors.
    /// </summary>
    public class FontDescriptor
    {
        /// <summary>
        /// 
        /// </summary>
        public string FontFile
        {
            get { return fontFile; }
        }
        protected string fontFile;

        /// <summary>
        /// 
        /// </summary>
        public string FontType
        {
            get { return fontType; }
        }
        protected string fontType;

        /// <summary>
        /// 
        /// </summary>
        public string FontName
        {
            get { return fontName; }
        }
        protected string fontName;

        /// <summary>
        /// 
        /// </summary>
        public string FullName
        {
            get { return fullName; }
        }
        protected string fullName;

        /// <summary>
        /// 
        /// </summary>
        public string FamilyName
        {
            get { return familyName; }
        }
        protected string familyName;

        /// <summary>
        /// 
        /// </summary>
        public string Weight
        {
            get { return weight; }
        }
        protected string weight;

        /// <summary>
        /// Gets a value indicating whether this instance belongs to a bold font.
        /// </summary>
        public virtual bool IsBoldFace
        {
            get { return false; }
        }

        /// <summary>
        /// 
        /// </summary>
        public float ItalicAngle
        {
            get { return italicAngle; }
        }
        protected float italicAngle;

        /// <summary>
        /// Gets a value indicating whether this instance belongs to an italic font.
        /// </summary>
        public virtual bool IsItalicFace
        {
            get { return false; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int XMin
        {
            get { return xMin; }
        }
        protected int xMin;

        /// <summary>
        /// 
        /// </summary>
        public int YMin
        {
            get { return yMin; }
        }
        protected int yMin;

        /// <summary>
        /// 
        /// </summary>
        public int XMax
        {
            get { return xMax; }
        }
        protected int xMax;

        /// <summary>
        /// 
        /// </summary>
        public int YMax
        {
            get { return yMax; }
        }
        protected int yMax;

        /// <summary>
        /// 
        /// </summary>
        public bool IsFixedPitch
        {
            get { return isFixedPitch; }
        }
        protected bool isFixedPitch;

        //Rect FontBBox;

        /// <summary>
        /// 
        /// </summary>
        public int UnderlinePosition
        {
            get { return underlinePosition; }
        }
        protected int underlinePosition;

        /// <summary>
        /// 
        /// </summary>
        public int UnderlineThickness
        {
            get { return underlineThickness; }
        }
        protected int underlineThickness;

        /// <summary>
        /// 
        /// </summary>
        public int StrikeoutPosition
        {
            get { return strikeoutPosition; }
        }
        protected int strikeoutPosition;

        /// <summary>
        /// 
        /// </summary>
        public int StrikeoutSize
        {
            get { return strikeoutSize; }
        }
        protected int strikeoutSize;

        /// <summary>
        /// 
        /// </summary>
        public string Version
        {
            get { return version; }
        }
        protected string version;

        ///// <summary>
        ///// 
        ///// </summary>
        //public string Notice
        //{
        //  get { return this.Notice; }
        //}
        //protected string notice;

        /// <summary>
        /// 
        /// </summary>
        public string EncodingScheme
        {
            get { return encodingScheme; }
        }
        protected string encodingScheme;

        /// <summary>
        /// 
        /// </summary>
        public int UnitsPerEm
        {
            get { return unitsPerEm; }
        }
        protected int unitsPerEm;

        /// <summary>
        /// 
        /// </summary>
        public int CapHeight
        {
            get { return capHeight; }
        }
        protected int capHeight;

        /// <summary>
        /// 
        /// </summary>
        public int XHeight
        {
            get { return xHeight; }
        }
        protected int xHeight;

        /// <summary>
        /// 
        /// </summary>
        public int Ascender
        {
            get { return ascender; }
        }
        protected int ascender;

        /// <summary>
        /// 
        /// </summary>
        public int Descender
        {
            get { return descender; }
        }
        protected int descender;

        /// <summary>
        /// 
        /// </summary>
        public int Leading
        {
            get { return leading; }
        }
        protected int leading;

        /// <summary>
        /// 
        /// </summary>
        public int Flags
        {
            get { return flags; }
        }
        protected int flags;

        /// <summary>
        /// 
        /// </summary>
        public int StemV
        {
            get { return stemV; }
        }
        protected int stemV;

        /// <summary>
        /// Under Construction
        /// </summary>
        public XFontMetrics FontMetrics
        {
            get
            {
                fontMetrics ??= new XFontMetrics(fontName, unitsPerEm, ascender, descender, leading, capHeight,
                      xHeight, stemV, 0, 0, 0);
                return fontMetrics;
            }
        }

        private XFontMetrics fontMetrics;
    }
}