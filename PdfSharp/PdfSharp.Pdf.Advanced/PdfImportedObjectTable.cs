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

namespace PdfSharp.Pdf.Advanced
{
    /// <summary>
    /// Represents the imported objects of an external document. Used to cache objects that are
    /// already imported when a PdfFormXObject is added to a page.
    /// </summary>
    internal sealed class PdfImportedObjectTable
    {
        /// <summary>
        /// Initializes a new instance of this class with the document the objects are imported from.
        /// </summary>
        public PdfImportedObjectTable(PdfDocument owner, PdfDocument externalDocument)
        {
            ArgumentNullException.ThrowIfNull(owner);
            ArgumentNullException.ThrowIfNull(externalDocument);
            this.owner = owner;
            externalDocumentHandle = externalDocument.Handle;
            xObjects = new PdfFormXObject[externalDocument.PageCount];
        }

        private readonly PdfFormXObject[] xObjects;

        /// <summary>
        /// Gets the document this table belongs to.
        /// </summary>
        public PdfDocument Owner
        {
            get { return owner; }
        }

        private readonly PdfDocument owner;

        /// <summary>
        /// Gets the external document, or null, if the external document is garbage collected.
        /// </summary>
        public PdfDocument ExternalDocument
        {
            get
            {
                if (externalDocumentHandle.IsAlive)
                    return externalDocumentHandle.Target;
                return null;
            }
        }

        private readonly PdfDocument.DocumentHandle externalDocumentHandle;

        public PdfFormXObject GetXObject(int pageNumber)
        {
            return xObjects[pageNumber - 1];
        }

        public void SetXObject(int pageNumber, PdfFormXObject xObject)
        {
            xObjects[pageNumber - 1] = xObject;
        }

        /// <summary>
        /// Indicates whether the specified object is already imported.
        /// </summary>
        public bool Contains(PdfObjectID externalID)
        {
            return externalIDs.ContainsKey(externalID.ToString());
        }

        /// <summary>
        /// Adds a cloned object to this table.
        /// </summary>
        /// <param name="externalID">The object identifier in the foreign object.</param>
        /// <param name="iref">The cross reference to the clone of the foreign object, which belongs to
        /// this document. In general the clone has a different object identifier.</param>
        public void Add(PdfObjectID externalID, PdfReference iref)
        {
            externalIDs[externalID.ToString()] = iref;
        }

        /// <summary>
        /// Gets the cloned object that corresponds to the specified external identifier.
        /// </summary>
        public PdfReference this[PdfObjectID externalID]
        {
            get { return (PdfReference)externalIDs[externalID.ToString()]; }
        }

        /// <summary>
        /// Maps external object identifiers to cross reference entries of the importing document
        /// {PdfObjectID -> PdfReference}.
        /// </summary>
        private readonly Dictionary<string, PdfReference> externalIDs = [];
    }
}
