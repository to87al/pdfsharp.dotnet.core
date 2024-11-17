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
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace PdfSharp.Pdf.Internal
{
    /// <summary>
    /// Provides a thread-local cache for large objects.
    /// </summary>
    internal class ThreadLocalStorage
    {
        public ThreadLocalStorage()
        {
            importedDocuments = new Dictionary<string, PdfDocument.DocumentHandle>(StringComparer.InvariantCultureIgnoreCase);
        }

        public void AddDocument(string path, PdfDocument document)
        {
            importedDocuments.Add(path, document.Handle);
        }

        public void RemoveDocument(string path)
        {
            importedDocuments.Remove(path);
        }

        public PdfDocument GetDocument(string path)
        {
            Debug.Assert(path.StartsWith('*') || Path.IsPathRooted(path), "Path must be full qualified.");

            PdfDocument document = null;
            if (importedDocuments.TryGetValue(path, out PdfDocument.DocumentHandle handle))
            {
                document = handle.Target;
                if (document == null)
                    RemoveDocument(path);
            }
            if (document == null)
            {
                document = PdfReader.Open(path, PdfDocumentOpenMode.Import);
                importedDocuments.Add(path, document.Handle);
            }
            return document;
        }

        public PdfDocument[] Documents
        {
            get
            {
                List<PdfDocument> list = [];
                foreach (PdfDocument.DocumentHandle handle in importedDocuments.Values)
                {
                    if (handle.IsAlive)
                        list.Add(handle.Target);
                }
                return [.. list];
            }
        }

        public void DetachDocument(PdfDocument.DocumentHandle handle)
        {
            if (handle.IsAlive)
            {
                foreach (String path in importedDocuments.Keys)
                {
                    if (importedDocuments[path] == handle)
                    {
                        importedDocuments.Remove(path);
                        break;
                    }
                }
            }

            // Clean table
            bool itemRemoved = true;
            while (itemRemoved)
            {
                itemRemoved = false;
                foreach (String path in importedDocuments.Keys)
                {
                    if (!importedDocuments[path].IsAlive)
                    {
                        importedDocuments.Remove(path);
                        itemRemoved = true;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Maps path to document handle.
        /// </summary>
        private readonly Dictionary<string, PdfDocument.DocumentHandle> importedDocuments;
    }
}