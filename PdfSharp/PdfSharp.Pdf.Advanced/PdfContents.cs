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

using PdfSharp.Pdf.IO;
using System;
using System.Diagnostics;

namespace PdfSharp.Pdf.Advanced
{
    /// <summary>
    /// Represents an array of PDF content streams of a page.
    /// </summary>
    public sealed class PdfContents : PdfArray
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PdfContents"/> class.
        /// </summary>
        /// <param name="document">The document.</param>
        public PdfContents(PdfDocument document)
          : base(document)
        {
        }

        internal PdfContents(PdfArray array)
          : base(array)
        {
            int count = Elements.Count;
            for (int idx = 0; idx < count; idx++)
            {
                // Convert the references from PdfDictionary to PdfContent
                PdfItem item = Elements[idx];
                if (item is PdfReference iref && iref.Value is PdfDictionary)
                {
                    // The following line is correct!
                    new PdfContent((PdfDictionary)iref.Value);
                }
                else
                    throw new InvalidOperationException("Unexpected item in a content stream array.");
            }
        }

        /// <summary>
        /// Appends a new content stream and returns it.
        /// </summary>
        public PdfContent AppendContent()
        {
            Debug.Assert(Owner != null);

            SetModified();
            PdfContent content = new(Owner);
            Owner.irefTable.Add(content);
            Debug.Assert(content.Reference != null);
            Elements.Add(content.Reference);
            return content;
        }

        /// <summary>
        /// Prepends a new content stream and returns it.
        /// </summary>
        public PdfContent PrependContent()
        {
            Debug.Assert(Owner != null);

            SetModified();
            PdfContent content = new(Owner);
            Owner.irefTable.Add(content);
            Debug.Assert(content.Reference != null);
            Elements.Insert(0, content.Reference);
            return content;
        }

        /// <summary>
        /// Creates a single content stream with the bytes from the array of the content streams.
        /// This operation does not modify any of the content streams in this array.
        /// </summary>
        public PdfContent CreateSingleContent()
        {
            byte[] bytes = [];
            byte[] bytes1;
            byte[] bytes2;
            foreach (PdfItem iref in Elements)
            {
                PdfDictionary cont = (PdfDictionary)((PdfReference)iref).Value;
                bytes1 = bytes;
                bytes2 = cont.Stream.UnfilteredValue;
                bytes = new byte[bytes1.Length + bytes2.Length + 1];
                bytes1.CopyTo(bytes, 0);
                bytes[bytes1.Length] = (byte)'\n';
                bytes2.CopyTo(bytes, bytes1.Length + 1);
            }
            PdfContent content = new(Owner);
            content.Stream = new PdfDictionary.PdfStream(bytes, content);
            return content;
        }

        private void SetModified()
        {
            if (!modified)
            {
                modified = true;
                int count = Elements.Count;

                if (count == 1)
                {
                    PdfContent content = (PdfContent)((PdfReference)Elements[0]).Value;
                    content.PreserveGraphicsState();
                }
                else if (count > 1)
                {
                    // Surround content streams with q/Q operations
                    byte[] value;
                    int length;
                    PdfContent content = (PdfContent)((PdfReference)Elements[0]).Value;
                    if (content != null && content.Stream != null)
                    {
                        length = content.Stream.Length;
                        value = new byte[length + 2];
                        value[0] = (byte)'q';
                        value[1] = (byte)'\n';
                        Array.Copy(content.Stream.Value, 0, value, 2, length);
                        content.Stream.Value = value;
                        content.Elements.SetInteger("/Length", length + 2);
                    }
                    content = (PdfContent)((PdfReference)Elements[count - 1]).Value;
                    if (content != null && content.Stream != null)
                    {
                        length = content.Stream.Length;
                        value = new byte[length + 3];
                        Array.Copy(content.Stream.Value, 0, value, 0, length);
                        value[length] = (byte)' ';
                        value[length + 1] = (byte)'Q';
                        value[length + 2] = (byte)'\n';
                        content.Stream.Value = value;
                        content.Elements.SetInteger("/Length", length + 3);
                    }
                }
            }
        }

        private bool modified;

        internal override void WriteObject(PdfWriter writer)
        {
            // Save two bytes in PDF stream...
            if (Elements.Count == 1)
                Elements[0].WriteObject(writer);
            else
                base.WriteObject(writer);
        }
    }
}
