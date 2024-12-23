﻿using PdfSharp.Internal;
using System;
using System.Globalization;
using System.Windows.Documents;

namespace PdfSharp.Xps.XpsModel
{
    internal enum GlyphIndicesComplexity
    {
        None = 1,
        DistanceOnly = 2,
        GlyphIndicesAndDistanceOnly = 3,
        ClusterMapping = 4,
    }

    /// <summary>
    /// Represents parsed Indices attribute. See 5.1.3.
    /// </summary>
    internal class GlyphIndices
    {
        /// <summary>
        /// Initializes an empty GlyphMapping.
        /// </summary>
        public GlyphIndices()
        {
            glyphMapping = [];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Glyphs"/> class.
        /// </summary>
        public GlyphIndices(string indices)
        {
            glyphMapping = GlyphIndicesParser.Parse(indices);
        }

        /// <summary>
        /// Gets the number of GlyphMapping elements.
        /// </summary>
        public int Count
        {
            get { return glyphMapping.Length; }
        }

        /// <summary>
        /// Gets the <see cref="PdfSharp.Xps.XpsModel.GlyphIndices.GlyphMapping"/> with the specified idx.
        /// </summary>
        public GlyphMapping this[int idx]
        {
            get { return glyphMapping[idx]; }
        }

        private readonly GlyphMapping[] glyphMapping;

        public GlyphIndicesComplexity Complexity
        {
            get
            {
                if (complexity == 0)
                    complexity = CalcGlyphIndicesComplexity();
                return complexity;
            }
        }

        private GlyphIndicesComplexity complexity;

        /// <summary>
        /// Evaluates how complex the GlyphMapping is.
        /// </summary>
        private GlyphIndicesComplexity CalcGlyphIndicesComplexity()
        {
            GlyphIndicesComplexity result = GlyphIndicesComplexity.None;
            int count = glyphMapping != null ? glyphMapping.Length : 0;
            for (int idx = 0; idx < count; idx++)
            {
                GlyphMapping gm = glyphMapping[idx];

                if (gm.ClusterCodeUnitCount > 1 || gm.ClusterGlyphCount > 1)
                {
                    // Max. complexity -> break
                    result = GlyphIndicesComplexity.ClusterMapping;
                    break;
                }

                if (gm.GlyphIndex != -1 && (int)result < (int)GlyphIndicesComplexity.GlyphIndicesAndDistanceOnly)
                {
                    result = GlyphIndicesComplexity.GlyphIndicesAndDistanceOnly;
                    continue;
                }
                if ((int)result < (int)GlyphIndicesComplexity.GlyphIndicesAndDistanceOnly)
                {
                    if (!DoubleUtil.IsNaN(gm.AdvanceWidth) || !DoubleUtil.IsNaN(gm.UOffset) || !DoubleUtil.IsNaN(gm.VOffset))
                    {
                        result = GlyphIndicesComplexity.DistanceOnly;
                        continue;
                    }
                }
            }
            return result;
        }

        public struct GlyphMapping
        {
            /// <summary>
            /// Temporary to make code work: "empgy glyph mapping"
            /// </summary>
            public GlyphMapping(int dummy)
            {
                ClusterCodeUnitCount = 1;
                ClusterGlyphCount = 1;
                GlyphIndex = -1;
                AdvanceWidth = UOffset = VOffset = Double.NaN;
            }

            /// <summary>
            /// Number of UTF-16 code units that combine to form this cluster. One or more code units may be
            /// specified.
            /// Default value is 1. 
            /// </summary>
            public int ClusterCodeUnitCount;

            /// <summary>
            /// Number of glyph indices that combine to form this cluster. One or more indices may be specified.
            /// Default value is 1. 
            /// </summary>
            public int ClusterGlyphCount;

            /// <summary>
            ///Index of the glyph (16-bit) in the physical font. The entry MAY be empty [M2.72], in which case
            ///the glyph index is determined by looking up the UTF-16 code unit in the font character map table.
            ///If there is not a one-to-one mapping between code units and the glyph indices, this entry MUST
            ///be specified [M5.5]. In cases where character-to-glyph mappings are not one-to-one, a cluster
            ///mapping specification precedes the glyph index (further described below). 
            /// </summary>
            public int GlyphIndex;

            /// <summary>
            /// Gets a value indicating whether the glyph index is not empty.
            /// </summary>
            public readonly bool HasGlyphIndex
            {
                get { return GlyphIndex != -1; }
            }

            /// <summary>
            /// Advance width indicating placement for the subsequent glyph, relative to the origin of the current
            /// glyph. Measured in direction of advance as defined by the IsSideways and BidiLevel attributes.
            /// Base glyphs generally have a non-zero advance width and combining glyphs have a zero advance
            /// width.
            /// Advance width is measured in hundredths of the font em size. The default value is defined in the
            /// horizontal metrics font table (hmtx) if the IsSideways attribute is specified as false or the vertical
            /// metrics font table (vmtx) if the IsSideways attribute is specified as true. Advance width is a real
            /// number with units specified in hundredths of an em.  
            /// </summary>
            public double AdvanceWidth;

            /// <summary>
            /// Gets a value indicating whether the AdvanceWidth is defined.
            /// </summary>
            public readonly bool HasAdvanceWidth
            {
                get { return !DoubleUtil.IsNaN(AdvanceWidth); }
            }

            /// <summary>
            /// Offset in the effective coordinate space relative to glyph origin to move this glyph (x offset for
            /// uOffset and â€“y offset for vOffset. The sign of vOffset is reversed from the direction of the y
            /// axis. A positive vOffset value shifts the glyph by a negative y offset and vice versa.). Used to
            /// attach marks to base characters. The value is added to the nominal glyph origin calculated using
            /// the advance width to generate the actual origin for the glyph. The setting of the IsSideways
            /// attribute does not change the interpretation of uOffset and vOffset.
            /// Measured in hundredths of the font em size. The default offset values are 0.0,0.0. uOffset and
            /// vOffset are real numbers.
            /// Base glyphs generally have a glyph offset of 0.0,0.0. Combining glyphs generally have an offset
            /// that places them correctly on top of the nearest preceding base glyph.
            /// For left-to-right text, a positive uOffset value points to the right; for right-to-left text, a
            /// positive uOffset value points to the left. 
            /// </summary>
            public double UOffset, VOffset;

            /// <summary>
            /// Gets a value indicating whether the UOffset is defined.
            /// </summary>
            public readonly bool HasUOffset
            {
                get { return !DoubleUtil.IsNaN(UOffset); }
            }

            /// <summary>
            /// Gets a value indicating whether the VOffset is defined.
            /// </summary>
            public readonly bool HasVOffset
            {
                get { return !DoubleUtil.IsNaN(VOffset); }
            }

            /// <summary>
            /// Gets a value indicating whether at least one of AdvanceWidth, UOffset, or VOffset is defined.
            /// </summary>
            public readonly bool HasAdvanceWidthOrOffset
            {
                get { return !DoubleUtil.IsNaN(AdvanceWidth) || !DoubleUtil.IsNaN(UOffset) || !DoubleUtil.IsNaN(VOffset); }
            }
        }
    }

    /// <summary>
    /// Converts an indices string into a GlyphMapping array.
    /// </summary>
    internal class GlyphIndicesParser
    {
        public static GlyphIndices.GlyphMapping[] Parse(string indices)
        {
            GlyphIndicesParser parser = new(indices);
            return parser.Parse();
        }

        private GlyphIndicesParser(string indices)
        {
            this.indices = indices;
        }

        private readonly string indices;
        internal static readonly char[] separator = ['(', ')', ':'];

        private GlyphIndices.GlyphMapping[] Parse()
        {
            string[] parts = indices.Split([';']);
            int count = parts.Length;
            GlyphIndices.GlyphMapping[] glyphMapping = new GlyphIndices.GlyphMapping[count];

            for (int idx = 0; idx < count; idx++)
                glyphMapping[idx] = ParsePart(parts[idx]);

            return glyphMapping;
        }

        private static GlyphIndices.GlyphMapping ParsePart(string parts)
        {
            GlyphIndices.GlyphMapping mapping = new(42);
            //mapping.ClusterCodeUnitCount = 1;
            //mapping.ClusterGlyphCount = 1;
            //mapping.GlyphIndex = -1;
            //mapping.AdvanceWidth = Double.NaN;
            //mapping.UOffset = Double.NaN;
            //mapping.VOffset = Double.NaN;

            string[] commaSeparated = parts.Split(',');
            if (commaSeparated.Length > 0)
            {
                if (!String.IsNullOrEmpty(commaSeparated[0]))
                {
                    // Just split the numbers
                    // (a:b)c
                    // (a)c
                    // c
                    // (a:b)  - not possible
                    // (a)  - not possible
                    string[] tempStr = commaSeparated[0].Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    // Last number is always the index
                    mapping.GlyphIndex = int.Parse(tempStr[^1]);

                    // First and second (if available) are the code unit count and glyph count
                    if (tempStr.Length > 1)
                        mapping.ClusterCodeUnitCount = int.Parse(tempStr[0]);

                    if (tempStr.Length > 2)
                        mapping.ClusterGlyphCount = int.Parse(tempStr[1]);
                }

                if (commaSeparated.Length > 1 && !String.IsNullOrEmpty(commaSeparated[1]))
                    mapping.AdvanceWidth = ParseDouble(commaSeparated[1]);

                if (commaSeparated.Length > 2 && !String.IsNullOrEmpty(commaSeparated[2]))
                    mapping.UOffset = ParseDouble(commaSeparated[2]);

                if (commaSeparated.Length > 3 && !String.IsNullOrEmpty(commaSeparated[3]))
                    mapping.VOffset = ParseDouble(commaSeparated[3]);
            }
            return mapping;
        }

        /// <summary>
        /// Parses a double value element.
        /// </summary>
        internal static double ParseDouble(string value)
        {
            return Double.Parse(value.Replace(" ", ""), CultureInfo.InvariantCulture);
        }
    }
}
