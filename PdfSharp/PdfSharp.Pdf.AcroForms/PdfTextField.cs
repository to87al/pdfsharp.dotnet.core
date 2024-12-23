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
using PdfSharp.Pdf.Annotations;

namespace PdfSharp.Pdf.AcroForms
{
    /// <summary>
    /// Represents the text field.
    /// </summary>
    public sealed class PdfTextField : PdfAcroField
    {
        /// <summary>
        /// Initializes a new instance of PdfTextField.
        /// </summary>
        internal PdfTextField(PdfDocument document)
          : base(document)
        {
        }

        internal PdfTextField(PdfDictionary dict)
          : base(dict)
        {
        }

        /// <summary>
        /// Gets or sets the text value of the text field.
        /// </summary>
        public string Text
        {
            get { return Elements.GetString(PdfAcroField.Keys.V); }
            set { Elements.SetString(PdfAcroField.Keys.V, value); RenderAppearance(); } //HACK in PdfTextField
        }

        /// <summary>
        /// Gets or sets the font used to draw the text of the field.
        /// </summary>
        public XFont Font
        {
            get { return font; }
            set { font = value; }
        }

        private XFont font = new("Courier New", 10);

        /// <summary>
        /// Gets or sets the foreground color of the field.
        /// </summary>
        public XColor ForeColor
        {
            get { return foreColor; }
            set { foreColor = value; }
        }

        private XColor foreColor = XColors.Black;

        /// <summary>
        /// Gets or sets the background color of the field.
        /// </summary>
        public XColor BackColor
        {
            get { return backColor; }
            set { backColor = value; }
        }

        private XColor backColor = XColor.Empty;

        /// <summary>
        /// Gets or sets the maximum length of the field.
        /// </summary>
        /// <value>The length of the max.</value>
        public int MaxLength
        {
            get { return Elements.GetInteger(Keys.MaxLen); }
            set { Elements.SetInteger(Keys.MaxLen, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the field has multiple lines.
        /// </summary>
        public bool MultiLine
        {
            get { return (Flags & PdfAcroFieldFlags.Multiline) != 0; }
            set
            {
                if (value)
                    SetFlags |= PdfAcroFieldFlags.Multiline;
                else
                    SetFlags &= ~PdfAcroFieldFlags.Multiline;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this field is used for passwords.
        /// </summary>
        public bool Password
        {
            get { return (Flags & PdfAcroFieldFlags.Password) != 0; }
            set
            {
                if (value)
                    SetFlags |= PdfAcroFieldFlags.Password;
                else
                    SetFlags &= ~PdfAcroFieldFlags.Password;
            }
        }

        /// <summary>
        /// Creates the normal appearance form X object for the annotation that represents
        /// this acro form text field.
        /// </summary>
        private void RenderAppearance()
        {
            PdfRectangle rect = Elements.GetRectangle(PdfAnnotation.Keys.Rect);
            XForm form = new(document, rect.Size);
            XGraphics gfx = XGraphics.FromForm(form);

            if (backColor != XColor.Empty)
                gfx.DrawRectangle(new XSolidBrush(BackColor), rect.ToXRect() - rect.Location);

            string text = Text;
            if (text.Length > 0)
                gfx.DrawString(Text, Font, new XSolidBrush(ForeColor),
                  rect.ToXRect() - rect.Location + new XPoint(2, 0), XStringFormats.TopLeft);

            form.DrawingFinished();

            // Get existing or create new appearance dictionary
            if (Elements[PdfAnnotation.Keys.AP] is not PdfDictionary ap)
            {
                ap = new PdfDictionary(document);
                Elements[PdfAnnotation.Keys.AP] = ap;
            }

            // Set XRef to normal state
            ap.Elements["/N"] = form.PdfForm.Reference;
        }

        internal override void PrepareForSave()
        {
            base.PrepareForSave();
            RenderAppearance();
        }

        /// <summary>
        /// Predefined keys of this dictionary. 
        /// The description comes from PDF 1.4 Reference.
        /// </summary>
        public new class Keys : PdfAcroField.Keys
        {
            /// <summary>
            /// (Optional; inheritable) The maximum length of the field�s text, in characters.
            /// </summary>
            [KeyInfo(KeyType.Integer | KeyType.Optional)]
            public const string MaxLen = "/MaxLen";

            /// <summary>
            /// Gets the KeysMeta for these keys.
            /// </summary>
            internal static DictionaryMeta Meta
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
