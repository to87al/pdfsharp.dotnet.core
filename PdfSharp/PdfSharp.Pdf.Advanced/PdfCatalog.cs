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

using PdfSharp.Pdf.AcroForms;
using PdfSharp.Pdf.IO;
using System;

namespace PdfSharp.Pdf.Advanced
{
    /// <summary>
    /// Represents the catalog dictionary.
    /// </summary>
    public sealed class PdfCatalog : PdfDictionary
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PdfCatalog"/> class.
        /// </summary>
        public PdfCatalog(PdfDocument document)
          : base(document)
        {
            Elements.SetName(Keys.Type, "/Catalog");

            version = "1.4";  // HACK in PdfCatalog
        }

        /// <summary>
        /// Get or sets the version of the PDF specification to which the document conforms.
        /// </summary>
        public string Version
        {
            get { return version; }
            set
            {
                version = value switch
                {
                    "1.0" or "1.1" or "1.2" => throw new InvalidOperationException("Unsupported PDF version."),
                    "1.3" or "1.4" => value,
                    "1.5" or "1.6" => throw new InvalidOperationException("Unsupported PDF version."),
                    _ => throw new ArgumentException("Invalid version."),
                };
            }
        }

        private string version = "1.3";

        /// <summary>
        /// Gets the pages collection of this document.
        /// </summary>
        public PdfPages Pages
        {
            get
            {
                if (pages == null)
                {
                    pages = (PdfPages)Elements.GetValue(Keys.Pages, VCF.CreateIndirect);
                    if (Owner.IsImported)
                        pages.FlattenPageTree();
                }
                return pages;
            }
        }

        private PdfPages pages;

        /// <summary>
        /// Implementation of PdfDocument.PageLayout.
        /// </summary>
        internal PdfPageLayout PageLayout
        {
            get { return (PdfPageLayout)Elements.GetEnumFromName(Keys.PageLayout, PdfPageLayout.SinglePage); }
            set { Elements.SetEnumAsName(Keys.PageLayout, value); }
        }

        /// <summary>
        /// Implementation of PdfDocument.PageMode.
        /// </summary>
        internal PdfPageMode PageMode
        {
            get { return (PdfPageMode)Elements.GetEnumFromName(Keys.PageMode, PdfPageMode.UseNone); }
            set { Elements.SetEnumAsName(Keys.PageMode, value); }
        }

        /// <summary>
        /// Implementation of PdfDocument.ViewerPreferences.
        /// </summary>
        internal PdfViewerPreferences ViewerPreferences
        {
            get
            {
                viewerPreferences ??= (PdfViewerPreferences)Elements.GetValue(Keys.ViewerPreferences, VCF.CreateIndirect);
                return viewerPreferences;
            }
        }

        private PdfViewerPreferences viewerPreferences;

        /// <summary>
        /// Implementation of PdfDocument.Outlines.
        /// </summary>
        internal PdfOutline.PdfOutlineCollection Outlines
        {
            get
            {
                outline ??= (PdfOutline)Elements.GetValue(Keys.Outlines, VCF.CreateIndirect);
                return outline.Outlines;
            }
        }

        private PdfOutline outline;

        /// <summary>
        /// Gets the AcroForm dictionary of this document.
        /// </summary>
        public PdfAcroForm AcroForm
        {
            get
            {
                acroForm ??= (PdfAcroForm)Elements.GetValue(Keys.AcroForm);
                return acroForm;
            }
        }

        private PdfAcroForm acroForm;

        /// <summary>
        /// Implementation of PdfDocument.PageMode.
        /// </summary>
        public string Language
        {
            get { return Elements.GetString(Keys.Lang); }
            set
            {
                if (value == null)
                    Elements.Remove(Keys.Lang);
                else
                    Elements.SetString(Keys.Lang, value);
            }
        }

        /// <summary>
        /// Dispatches PrepareForSave to the objects that need it.
        /// </summary>
        internal override void PrepareForSave()
        {
            pages?.PrepareForSave();

            if (outline != null && outline.Outlines.Count > 0)
            {
                if (Elements[Keys.PageMode] == null)
                    PageMode = PdfPageMode.UseOutlines;
                outline.PrepareForSave();
            }
        }

        internal override void WriteObject(PdfWriter writer)
        {
            if (outline != null && outline.Outlines.Count > 0)
            {
                if (Elements[Keys.PageMode] == null)
                    PageMode = PdfPageMode.UseOutlines;
            }
            base.WriteObject(writer);
        }

        /// <summary>
        /// Predefined keys of this dictionary.
        /// </summary>
        internal sealed class Keys : KeysBase
        {
            /// <summary>
            /// (Required) The type of PDF object that this dictionary describes; 
            /// must be Catalog for the catalog dictionary.
            /// </summary>
            [KeyInfo(KeyType.Name | KeyType.Required, FixedValue = "Catalog")]
            public const string Type = "/Type";

            /// <summary>
            /// (Optional; PDF 1.4) The version of the PDF specification to which the document
            /// conforms (for example, 1.4) if later than the version specified in the file�s header.
            /// If the header specifies a later version, or if this entry is absent, the document 
            /// conforms to the version specified in the header. This entry enables a PDF producer 
            /// application to update the version using an incremental update.
            /// </summary>
            [KeyInfo("1.4", KeyType.Name | KeyType.Optional)]
            public const string Version = "/Version";

            /// <summary>
            /// (Required; must be an indirect reference) The page tree node that is the root of 
            /// the document�s page tree.
            /// </summary>
            [KeyInfo(KeyType.Dictionary | KeyType.Required | KeyType.MustBeIndirect, typeof(PdfPages))]
            public const string Pages = "/Pages";

            /// <summary>
            /// (Optional; PDF 1.3) A number tree defining the page labeling for the document. 
            /// The keys in this tree are page indices; the corresponding values are page label dictionaries.
            /// Each page index denotes the first page in a labeling range to which the specified page 
            /// label dictionary applies. The tree must include a value for pageindex 0.
            /// </summary>
            [KeyInfo("1.3", KeyType.NumberTree | KeyType.Optional)]
            public const string PageLabels = "/PageLabels";

            /// <summary>
            /// (Optional; PDF 1.2) The document�s name dictionary.
            /// </summary>
            [KeyInfo("1.2", KeyType.Dictionary | KeyType.Optional)]
            public const string Names = "/Names";

            /// <summary>
            /// (Optional; PDF 1.1; must be an indirect reference) A dictionary of names and 
            /// corresponding destinations.
            /// </summary>
            [KeyInfo("1.1", KeyType.Dictionary | KeyType.Optional)]
            public const string Dests = "/Dests";

            /// <summary>
            /// (Optional; PDF 1.2) A viewer preferences dictionary specifying the way the document 
            /// is to be displayed on the screen. If this entry is absent, applications should use
            /// their own current user preference settings.
            /// </summary>
            [KeyInfo("1.2", KeyType.Dictionary | KeyType.Optional, typeof(PdfViewerPreferences))]
            public const string ViewerPreferences = "/ViewerPreferences";

            /// <summary>
            /// (Optional) A name object specifying the page layout to be used when the document is 
            /// opened:
            /// SinglePage - Display one page at a time.
            /// OneColumn - Display the pages in one column.
            /// TwoColumnLeft - Display the pages in two columns, with oddnumbered pages on the left.
            /// TwoColumnRight - Display the pages in two columns, with oddnumbered pages on the right.
            /// TwoPageLeft - (PDF 1.5) Display the pages two at a time, with odd-numbered pages on the left
            /// TwoPageRight - (PDF 1.5) Display the pages two at a time, with odd-numbered pages on the right.
            /// </summary>
            [KeyInfo(KeyType.Name | KeyType.Optional)]
            public const string PageLayout = "/PageLayout";

            /// <summary>
            /// (Optional) A name object specifying how the document should be displayed when opened:
            /// UseNone - Neither document outline nor thumbnail images visible.
            /// UseOutlines - Document outline visible.
            /// UseThumbs - Thumbnail images visible.
            /// FullScreen - Full-screen mode, with no menu bar, windowcontrols, or any other window visible.
            /// UseOC - (PDF 1.5) Optional content group panel visible.
            /// UseAttachments (PDF 1.6) Attachments panel visible.
            /// Default value: UseNone.
            /// </summary>
            [KeyInfo(KeyType.Name | KeyType.Optional)]
            public const string PageMode = "/PageMode";

            /// <summary>
            /// (Optional; must be an indirect reference) The outline dictionary that is the root 
            /// of the document�s outline hierarchy.
            /// </summary>
            [KeyInfo(KeyType.Dictionary | KeyType.Optional, typeof(PdfOutline))]
            public const string Outlines = "/Outlines";

            /// <summary>
            /// (Optional; PDF 1.1; must be an indirect reference) An array of thread dictionaries 
            /// representing the document�s article threads.
            /// </summary>
            [KeyInfo("1.1", KeyType.Array | KeyType.Optional)]
            public const string Threads = "/Threads";

            /// <summary>
            /// (Optional; PDF 1.1) A value specifying a destination to be displayed or an action to be 
            /// performed when the document is opened. The value is either an array defining a destination 
            /// or an action dictionary representing an action. If this entry is absent, the document
            /// should be opened to the top of the first page at the default magnification factor.
            /// </summary>
            [KeyInfo("1.1", KeyType.ArrayOrDictionary | KeyType.Optional)]
            public const string OpenAction = "/OpenAction";

            /// <summary>
            /// (Optional; PDF 1.4) An additional-actions dictionary defining the actions to be taken 
            /// in response to various trigger events affecting the document as a whole.
            /// </summary>
            [KeyInfo("1.4", KeyType.Dictionary | KeyType.Optional)]
            public const string AA = "/AA";

            /// <summary>
            /// (Optional; PDF 1.1) A URI dictionary containing document-level information for URI 
            /// (uniform resource identifier) actions.
            /// </summary>
            [KeyInfo("1.1", KeyType.Dictionary | KeyType.Optional)]
            public const string URI = "/URI";

            /// <summary>
            /// (Optional; PDF 1.2) The document�s interactive form (AcroForm) dictionary.
            /// </summary>
            [KeyInfo("1.2", KeyType.Dictionary | KeyType.Optional, typeof(PdfAcroForm))]
            public const string AcroForm = "/AcroForm";

            /// <summary>
            /// (Optional; PDF 1.4; must be an indirect reference) A metadata stream 
            /// containing metadata  for the document.
            /// </summary>
            [KeyInfo("1.4", KeyType.Dictionary | KeyType.Optional | KeyType.MustBeIndirect)]
            public const string Metadata = "/Metadata";

            /// <summary>
            /// (Optional; PDF 1.3) The document�s structure tree root dictionary.
            /// </summary>
            [KeyInfo("1.3", KeyType.Dictionary | KeyType.Optional)]
            public const string StructTreeRoot = "/StructTreeRoot";

            /// <summary>
            /// (Optional; PDF 1.4) A mark information dictionary containing information
            /// about the document�s usage of Tagged PDF conventions.
            /// </summary>
            [KeyInfo("1.4", KeyType.Dictionary | KeyType.Optional)]
            public const string MarkInfo = "/MarkInfo";

            /// <summary>
            /// (Optional; PDF 1.4) A language identifier specifying the natural language for all 
            /// text in the document except where overridden by language specifications for structure 
            /// elements or marked content. If this entry is absent, the language is considered unknown.
            /// </summary>
            [KeyInfo("1.4", KeyType.String | KeyType.Optional)]
            public const string Lang = "/Lang";

            /// <summary>
            /// (Optional; PDF 1.3) A Web Capture information dictionary containing state information
            /// used by the Acrobat Web Capture (AcroSpider) plugin extension.
            /// </summary>
            [KeyInfo("1.3", KeyType.Dictionary | KeyType.Optional)]
            public const string SpiderInfo = "/SpiderInfo";

            /// <summary>
            /// (Optional; PDF 1.4) An array of output intent dictionaries describing the color 
            /// characteristics of output devices on which the document might be rendered.
            /// </summary>
            [KeyInfo("1.4", KeyType.Array | KeyType.Optional)]
            public const string OutputIntents = "/OutputIntents";

            /// <summary>
            /// (Optional; PDF 1.4) A page-piece dictionary associated with the document.
            /// </summary>
            [KeyInfo("1.4", KeyType.Dictionary | KeyType.Optional)]
            public const string PieceInfo = "/PieceInfo";

            /// <summary>
            /// (Optional; PDF 1.5; required if a document contains optional content) The document�s 
            /// optional content properties dictionary.
            /// </summary>
            [KeyInfo("1.5", KeyType.Dictionary | KeyType.Optional)]
            public const string OCProperties = "/OCProperties";

            /// <summary>
            /// (Optional; PDF 1.5) A permissions dictionary that specifies user access permissions 
            /// for the document.
            /// </summary>
            [KeyInfo("1.5", KeyType.Dictionary | KeyType.Optional)]
            public const string Perms = "/Perms";

            /// <summary>
            /// (Optional; PDF 1.5) A dictionary containing attestations regarding the content of a 
            /// PDF document, as it relates to the legality of digital signatures.
            /// </summary>
            [KeyInfo("1.5", KeyType.Dictionary | KeyType.Optional)]
            public const string Legal = "/Legal";

            /// <summary>
            /// Gets the KeysMeta for these keys.
            /// </summary>
            public static DictionaryMeta Meta
            {
                get
                {
                    meta ??= CreateMeta(typeof(Keys));
                    return meta;
                }
            }

            private static DictionaryMeta meta;
        }

        /// <summary>
        /// Gets the KeysMeta of this dictionary type.
        /// </summary>
        internal override DictionaryMeta Meta
        {
            get { return Keys.Meta; }
        }
    }
}
