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
    /// Represents a PDF resource object.
    /// </summary>
    public sealed class PdfResources : PdfDictionary
    {
        // Resource management works roughly like this:
        // When the user creates an XFont and uses it in the XGraphics of a PdfPage, then at the first time
        // a PdfFont is created and cached in the document global font table. If the user creates a new
        // XFont object for an exisisting PdfFont, the PdfFont object is reused. When the PdfFont is added
        // to the resources of a PdfPage for the first time, it is added to the page local PdfResourceMap for 
        // fonts and automatically associated with a local resource name.

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfResources"/> class.
        /// </summary>
        /// <param name="document">The document.</param>
        public PdfResources(PdfDocument document)
          : base(document)
        {
            Elements[Keys.ProcSet] = new PdfLiteral("[/PDF/Text/ImageB/ImageC/ImageI]");
        }

        internal PdfResources(PdfDictionary dict)
          : base(dict)
        { }

        /// <summary>
        /// Adds the specified font to this resource dictionary and returns its local resource name.
        /// </summary>
        public string AddFont(PdfFont font)
        {
            if (!resources.TryGetValue(font, out string name))
            {
                name = NextFontName;
                resources[font] = name;
                if (font.Reference == null)
                    Owner.irefTable.Add(font);
                Fonts.Elements[name] = font.Reference;
            }
            return name;
        }

        /// <summary>
        /// Adds the specified image to this resource dictionary
        /// and returns its local resource name.
        /// </summary>
        public string AddImage(PdfImage image)
        {
            if (!resources.TryGetValue(image, out string name))
            {
                name = NextImageName;
                resources[image] = name;
                if (image.Reference == null)
                    Owner.irefTable.Add(image);
                XObjects.Elements[name] = image.Reference;
            }
            return name;
        }

        /// <summary>
        /// Adds the specified form object to this resource dictionary
        /// and returns its local resource name.
        /// </summary>
        public string AddForm(PdfFormXObject form)
        {
            if (!resources.TryGetValue(form, out string name))
            {
                name = NextFormName;
                resources[form] = name;
                if (form.Reference == null)
                    Owner.irefTable.Add(form);
                XObjects.Elements[name] = form.Reference;
            }
            return name;
        }

        /// <summary>
        /// Adds the specified graphics state to this resource dictionary
        /// and returns its local resource name.
        /// </summary>
        public string AddExtGState(PdfExtGState extGState)
        {
            if (!resources.TryGetValue(extGState, out string name))
            {
                name = NextExtGStateName;
                resources[extGState] = name;
                if (extGState.Reference == null)
                    Owner.irefTable.Add(extGState);
                ExtGStates.Elements[name] = extGState.Reference;
            }
            return name;
        }

        /// <summary>
        /// Adds the specified pattern to this resource dictionary
        /// and returns its local resource name.
        /// </summary>
        public string AddPattern(PdfShadingPattern pattern)
        {
            if (!resources.TryGetValue(pattern, out string name))
            {
                name = NextPatternName;
                resources[pattern] = name;
                if (pattern.Reference == null)
                    Owner.irefTable.Add(pattern);
                Patterns.Elements[name] = pattern.Reference;
            }
            return name;
        }

        /// <summary>
        /// Adds the specified pattern to this resource dictionary
        /// and returns its local resource name.
        /// </summary>
        public string AddPattern(PdfTilingPattern pattern)
        {
            if (!resources.TryGetValue(pattern, out string name))
            {
                name = NextPatternName;
                resources[pattern] = name;
                if (pattern.Reference == null)
                    Owner.irefTable.Add(pattern);
                Patterns.Elements[name] = pattern.Reference;
            }
            return name;
        }

        /// <summary>
        /// Adds the specified shading to this resource dictionary
        /// and returns its local resource name.
        /// </summary>
        public string AddShading(PdfShading shading)
        {
            if (!resources.TryGetValue(shading, out string name))
            {
                name = NextShadingName;
                resources[shading] = name;
                if (shading.Reference == null)
                    Owner.irefTable.Add(shading);
                Shadings.Elements[name] = shading.Reference;
            }
            return name;
        }

        /// <summary>
        /// Gets the fonts map.
        /// </summary>
        internal PdfResourceMap Fonts
        {
            get
            {
                fonts ??= (PdfResourceMap)Elements.GetValue(Keys.Font, VCF.Create);
                return fonts;
            }
        }

        private PdfResourceMap fonts;

        /// <summary>
        /// Gets the external objects map.
        /// </summary>
        internal PdfResourceMap XObjects
        {
            get
            {
                xObjects ??= (PdfResourceMap)Elements.GetValue(Keys.XObject, VCF.Create);
                return xObjects;
            }
        }

        private PdfResourceMap xObjects;

        // TODO: make own class
        internal PdfResourceMap ExtGStates
        {
            get
            {
                extGStates ??= (PdfResourceMap)Elements.GetValue(Keys.ExtGState, VCF.Create);
                return extGStates;
            }
        }

        private PdfResourceMap extGStates;

        // TODO: make own class
        internal PdfResourceMap ColorSpaces
        {
            get
            {
                colorSpaces ??= (PdfResourceMap)Elements.GetValue(Keys.ColorSpace, VCF.Create);
                return colorSpaces;
            }
        }

        private PdfResourceMap colorSpaces;

        // TODO: make own class
        internal PdfResourceMap Patterns
        {
            get
            {
                patterns ??= (PdfResourceMap)Elements.GetValue(Keys.Pattern, VCF.Create);
                return patterns;
            }
        }

        private PdfResourceMap patterns;

        // TODO: make own class
        internal PdfResourceMap Shadings
        {
            get
            {
                shadings ??= (PdfResourceMap)Elements.GetValue(Keys.Shading, VCF.Create);
                return shadings;
            }
        }

        private PdfResourceMap shadings;

        // TODO: make own class
        internal PdfResourceMap Properties
        {
            get
            {
                properties ??= (PdfResourceMap)Elements.GetValue(Keys.Properties, VCF.Create);
                return properties;
            }
        }

        private PdfResourceMap properties;

        /// <summary>
        /// Gets a new local name for this resource.
        /// </summary>
        private string NextFontName
        {
            get
            {
                string name;
                while (ExistsResourceNames(name = $"/F{fontNumber++}")) { }
                return name;
            }
        }

        private int fontNumber;

        /// <summary>
        /// Gets a new local name for this resource.
        /// </summary>
        private string NextImageName
        {
            get
            {
                string name;
                while (ExistsResourceNames(name = $"/I{imageNumber++}")) { }
                return name;
            }
        }

        private int imageNumber;

        /// <summary>
        /// Gets a new local name for this resource.
        /// </summary>
        private string NextFormName
        {
            get
            {
                string name;
                while (ExistsResourceNames(name = $"/Fm{formNumber++}")) { }
                return name;
            }
        }

        private int formNumber;

        /// <summary>
        /// Gets a new local name for this resource.
        /// </summary>
        private string NextExtGStateName
        {
            get
            {
                string name;
                while (ExistsResourceNames(name = $"/GS{ExtGStateNumber++}")) { }
                return name;
            }
        }

        private int ExtGStateNumber;

        /// <summary>
        /// Gets a new local name for this resource.
        /// </summary>
        private string NextPatternName
        {
            get
            {
                string name;
                while (ExistsResourceNames(name = $"/Pa{PatternNumber++}")) ;
                return name;
            }
        }

        private int PatternNumber;

        /// <summary>
        /// Gets a new local name for this resource.
        /// </summary>
        private string NextShadingName
        {
            get
            {
                string name;
                while (ExistsResourceNames(name = $"/Sh{ShadingNumber++}")) ;
                return name;
            }
        }

        private int ShadingNumber;

        /// <summary>
        /// Check whether a resource name is already used in the context of this resource dictionary.
        /// PDF4NET uses GUIDs as resource names, but I think this weapon is to heavy.
        /// </summary>
        internal bool ExistsResourceNames(string name)
        {
            // TODO: more precise: is this page imported and is PageOptions != Replace
            // BUG: 
            //if (!this.Owner.IsImported)
            //  return false;

            // Collect all resouce names of all imported resources.
            if (importedResourceNames == null)
            {
                importedResourceNames = [];

                if (Elements[Keys.Font] != null)
                    Fonts.CollectResourceNames(importedResourceNames);

                if (Elements[Keys.XObject] != null)
                    XObjects.CollectResourceNames(importedResourceNames);

                if (Elements[Keys.ExtGState] != null)
                    ExtGStates.CollectResourceNames(importedResourceNames);

                if (Elements[Keys.ColorSpace] != null)
                    ColorSpaces.CollectResourceNames(importedResourceNames);

                if (Elements[Keys.Pattern] != null)
                    Patterns.CollectResourceNames(importedResourceNames);

                if (Elements[Keys.Shading] != null)
                    Shadings.CollectResourceNames(importedResourceNames);

                if (Elements[Keys.Properties] != null)
                    Properties.CollectResourceNames(importedResourceNames);
            }
            return importedResourceNames.ContainsKey(name);
            // This is superfluous because PDFsharp resource names cannot be double.
            // this.importedResourceNames.Add(name, null);
        }

        /// <summary>
        /// All the names of imported resources.
        /// </summary>
        private Dictionary<string, object> importedResourceNames;

        /// <summary>
        /// Maps all PDFsharp resources to their local resource names.
        /// </summary>
        private readonly Dictionary<PdfObject, string> resources = [];

        /// <summary>
        /// Predefined keys of this dictionary.
        /// </summary>
        public sealed class Keys : KeysBase
        {
            /// <summary>
            /// (Optional) A dictionary that maps resource names to graphics state 
            /// parameter dictionaries.
            /// </summary>
            [KeyInfo(KeyType.Dictionary | KeyType.Optional, typeof(PdfResourceMap))]
            public const string ExtGState = "/ExtGState";

            /// <summary>
            /// (Optional) A dictionary that maps each resource name to either the name of a
            /// device-dependent color space or an array describing a color space.
            /// </summary>
            [KeyInfo(KeyType.Dictionary | KeyType.Optional, typeof(PdfResourceMap))]
            public const string ColorSpace = "/ColorSpace";

            /// <summary>
            /// (Optional) A dictionary that maps each resource name to either the name of a
            /// device-dependent color space or an array describing a color space.
            /// </summary>
            [KeyInfo(KeyType.Dictionary | KeyType.Optional, typeof(PdfResourceMap))]
            public const string Pattern = "/Pattern";

            /// <summary>
            /// (Optional; PDF 1.3) A dictionary that maps resource names to shading dictionaries.
            /// </summary>
            [KeyInfo("1.3", KeyType.Dictionary | KeyType.Optional, typeof(PdfResourceMap))]
            public const string Shading = "/Shading";

            /// <summary>
            /// (Optional) A dictionary that maps resource names to external objects.
            /// </summary>
            [KeyInfo(KeyType.Dictionary | KeyType.Optional, typeof(PdfResourceMap))]
            public const string XObject = "/XObject";

            /// <summary>
            /// (Optional) A dictionary that maps resource names to font dictionaries.
            /// </summary>
            [KeyInfo(KeyType.Dictionary | KeyType.Optional, typeof(PdfResourceMap))]
            public const string Font = "/Font";

            /// <summary>
            /// (Optional) An array of predefined procedure set names.
            /// </summary>
            [KeyInfo(KeyType.Array | KeyType.Optional)]
            public const string ProcSet = "/ProcSet";

            /// <summary>
            /// (Optional; PDF 1.2) A dictionary that maps resource names to property list
            /// dictionaries for marked content.
            /// </summary>
            [KeyInfo(KeyType.Dictionary | KeyType.Optional, typeof(PdfResourceMap))]
            public const string Properties = "/Properties";

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
