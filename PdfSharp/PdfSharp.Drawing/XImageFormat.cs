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

namespace PdfSharp.Drawing
{
    /// <summary>
    /// Specifies the format of the image.
    /// </summary>
    public sealed class XImageFormat
    {
        private readonly Guid guid;

        private XImageFormat(Guid guid)
        {
            this.guid = guid;
        }

        internal Guid Guid
        {
            get { return guid; }
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is not XImageFormat format)
                return false;
            return guid == format.guid;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        public override int GetHashCode()
        {
            return guid.GetHashCode();
        }

        /// <summary>
        /// Gets the Portable Network Graphics (PNG) image format. 
        /// </summary>
        public static XImageFormat Png
        {
            get { return png; }
        }

        /// <summary>
        /// Gets the Graphics Interchange Format (GIF) image format.
        /// </summary>
        public static XImageFormat Gif
        {
            get { return gif; }
        }

        /// <summary>
        /// Gets the Joint Photographic Experts Group (JPEG) image format.
        /// </summary>
        public static XImageFormat Jpeg
        {
            get { return jpeg; }
        }

        /// <summary>
        /// Gets the Tag Image File Format (TIFF) image format.
        /// </summary>
        public static XImageFormat Tiff
        {
            get { return tiff; }
        }

        /// <summary>
        /// Gets the Portable Document Format (PDF) image format
        /// </summary>
        public static XImageFormat Pdf
        {
            get { return pdf; }
        }

        /// <summary>
        /// Gets the Windows icon image format.
        /// </summary>
        public static XImageFormat Icon
        {
            get { return icon; }
        }

        static XImageFormat()
        {
            //ImageFormat.memoryBMP = new ImageFormat(new Guid("{b96b3caa-0728-11d3-9d7b-0000f81ef32e}"));
            //ImageFormat.bmp = new ImageFormat(new Guid("{b96b3cab-0728-11d3-9d7b-0000f81ef32e}"));
            //ImageFormat.emf = new ImageFormat(new Guid("{b96b3cac-0728-11d3-9d7b-0000f81ef32e}"));
            //ImageFormat.wmf = new ImageFormat(new Guid("{b96b3cad-0728-11d3-9d7b-0000f81ef32e}"));
            //ImageFormat.jpeg = new ImageFormat(new Guid("{b96b3cae-0728-11d3-9d7b-0000f81ef32e}"));
            //ImageFormat.png = new ImageFormat(new Guid("{b96b3caf-0728-11d3-9d7b-0000f81ef32e}"));
            //ImageFormat.gif = new ImageFormat(new Guid("{b96b3cb0-0728-11d3-9d7b-0000f81ef32e}"));
            //ImageFormat.tiff = new ImageFormat(new Guid("{b96b3cb1-0728-11d3-9d7b-0000f81ef32e}"));
            //ImageFormat.exif = new ImageFormat(new Guid("{b96b3cb2-0728-11d3-9d7b-0000f81ef32e}"));
            //ImageFormat.photoCD = new ImageFormat(new Guid("{b96b3cb3-0728-11d3-9d7b-0000f81ef32e}"));
            //ImageFormat.flashPIX = new ImageFormat(new Guid("{b96b3cb4-0728-11d3-9d7b-0000f81ef32e}"));
            //ImageFormat.icon = new ImageFormat(new Guid("{b96b3cb5-0728-11d3-9d7b-0000f81ef32e}"));
        }

        private static readonly XImageFormat png = new(new Guid("{B96B3CAF-0728-11D3-9D7B-0000F81EF32E}"));
        private static readonly XImageFormat gif = new(new Guid("{B96B3CB0-0728-11D3-9D7B-0000F81EF32E}"));
        private static readonly XImageFormat jpeg = new(new Guid("{B96B3CAE-0728-11D3-9D7B-0000F81EF32E}"));
        private static readonly XImageFormat tiff = new(new Guid("{B96B3CB1-0728-11D3-9D7B-0000F81EF32E}"));
        private static readonly XImageFormat icon = new(new Guid("{B96B3CB5-0728-11D3-9D7B-0000F81EF32E}"));
        // not GDI+ conform
        private static readonly XImageFormat pdf = new(new Guid("{84570158-DBF0-4C6B-8368-62D6A3CA76E0}"));
    }
}