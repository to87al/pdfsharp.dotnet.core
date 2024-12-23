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

using PdfSharp.Drawing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace PdfSharp.Pdf.Advanced
{
    /// <summary>
    /// Contains all used images of a document.
    /// </summary>
    internal sealed class PdfImageTable : PdfResourceTable
    {
        /// <summary>
        /// Initializes a new instance of this class, which is a singleton for each document.
        /// </summary>
        public PdfImageTable(PdfDocument document)
          : base(document)
        { }

        /// <summary>
        /// Gets a PdfImage from an XImage. If no PdfImage already exists, a new one is created.
        /// </summary>
        public PdfImage GetImage(XImage image)
        {
            ImageSelector selector = image.selector;
            if (selector == null)
            {
                selector = new ImageSelector(image);
                image.selector = selector;
            }
            if (!images.TryGetValue(selector, out PdfImage pdfImage))
            {
                pdfImage = new PdfImage(owner, image);
                //pdfImage.Document = this.document;
                Debug.Assert(pdfImage.Owner == owner);
                images[selector] = pdfImage;
                //if (this.document.EarlyWrite)
                //{
                //  //pdfFont.Close(); delete 
                //  //pdfFont.AssignObjID(ref this.document.ObjectID); // BUG just test code!!!!
                //  //pdfFont.WriteObject(null);
                //}
            }
            return pdfImage;
        }

        /// <summary>
        /// Map from ImageSelector to PdfImage.
        /// </summary>
        private readonly Dictionary<ImageSelector, PdfImage> images = [];

        /// <summary>
        /// A collection of information that uniquely identifies a particular PdfImage.
        /// </summary>
        public class ImageSelector
        {
            /// <summary>
            /// Initializes a new instance of ImageSelector from an XImage.
            /// </summary>
            public ImageSelector(XImage image)
            {
                // HACK: implement a way to identify images when they are reused
                image.path ??= Guid.NewGuid().ToString();

                // HACK: just use full path to identify
                path = image.path.ToLower(CultureInfo.InvariantCulture);
            }

            public string Path
            {
                get { return path; }
                set { path = value; }
            }

            private string path;

            public override bool Equals(object obj)
            {
                ImageSelector selector = obj as ImageSelector;
                if (obj == null)
                    return false;
                return path == selector.path;
            }

            public override int GetHashCode()
            {
                return path.GetHashCode();
            }
        }
    }
}