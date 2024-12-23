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


namespace PdfSharp.Pdf
{
    /// <summary>
    /// Holds information how to handle the document when it is saved as PDF stream.
    /// </summary>
    public sealed class PdfDocumentOptions
    {
        internal PdfDocumentOptions(PdfDocument document)
        {
            //this.deflateContents = true;
            //this.writeProcedureSets = true;
        }

        /// <summary>
        /// Gets or sets the color mode.
        /// </summary>
        public PdfColorMode ColorMode
        {
            get { return colorMode; }
            set { colorMode = value; }
        }

        private PdfColorMode colorMode;

        /// <summary>
        /// Gets or sets a value indicating whether to compress content streams of PDF pages.
        /// </summary>
        public bool CompressContentStreams
        {
            get { return compressContentStreams; }
            set { compressContentStreams = value; }
        }
#if DEBUG
        private bool compressContentStreams = false;
#else
        bool compressContentStreams = true;
#endif

        /// <summary>
        /// Gets or sets a value indicating that all objects are not compressed.
        /// </summary>
        public bool NoCompression
        {
            get { return noCompression; }
            set { noCompression = value; }
        }

        private bool noCompression;
    }
}