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

namespace PdfSharp.Drawing.Layout
{
    /// <summary>
    /// Represents a very simple text formatter.
    /// If this class does not satisfy your needs on formatting paragraphs I recommend to take a look
    /// at MigraDoc Foundation. Alternatively you should copy this class in your own source code and modify it.
    /// </summary>
    public class XTextFormatter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XTextFormatter"/> class.
        /// </summary>
        public XTextFormatter(XGraphics gfx)
        {
            ArgumentNullException.ThrowIfNull(gfx);
            this.gfx = gfx;
        }

        private readonly XGraphics gfx;

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        private string text;

        /// <summary>
        /// Gets or sets the font.
        /// </summary>
        public XFont Font
        {
            get { return font; }
            set
            {
                ArgumentNullException.ThrowIfNull(value);
                font = value;

                lineSpace = font.GetHeight(gfx);
                cyAscent = lineSpace * font.cellAscent / font.cellSpace;
                cyDescent = lineSpace * font.cellDescent / font.cellSpace;

                // HACK in XTextFormatter
                spaceWidth = XGraphics.MeasureString("x�x", value).width;
                spaceWidth -= XGraphics.MeasureString("xx", value).width;
            }
        }

        private XFont font;
        private double lineSpace;
        private double cyAscent;
        private double cyDescent;
        private double spaceWidth;

        /// <summary>
        /// Gets or sets the bounding box of the layout.
        /// </summary>
        public XRect LayoutRectangle
        {
            get { return layoutRectangle; }
            set { layoutRectangle = value; }
        }

        private XRect layoutRectangle;

        /// <summary>
        /// Gets or sets the alignment of the text.
        /// </summary>
        public XParagraphAlignment Alignment
        {
            get { return alignment; }
            set { alignment = value; }
        }

        private XParagraphAlignment alignment = XParagraphAlignment.Left;

        /// <summary>
        /// Draws the text.
        /// </summary>
        /// <param name="text">The text to be drawn.</param>
        /// <param name="font">The font.</param>
        /// <param name="brush">The text brush.</param>
        /// <param name="layoutRectangle">The layout rectangle.</param>
        public void DrawString(string text, XFont font, XBrush brush, XRect layoutRectangle)
        {
            DrawString(text, font, brush, layoutRectangle, XStringFormats.TopLeft);
        }

        /// <summary>
        /// Draws the text.
        /// </summary>
        /// <param name="text">The text to be drawn.</param>
        /// <param name="font">The font.</param>
        /// <param name="brush">The text brush.</param>
        /// <param name="layoutRectangle">The layout rectangle.</param>
        /// <param name="format">The format. Must be <c>XStringFormat.TopLeft</c></param>
        public void DrawString(string text, XFont font, XBrush brush, XRect layoutRectangle, XStringFormat format)
        {
            ArgumentNullException.ThrowIfNull(text);
            ArgumentNullException.ThrowIfNull(font);
            ArgumentNullException.ThrowIfNull(brush);
            if (format.Alignment != XStringAlignment.Near || format.LineAlignment != XLineAlignment.Near)
                throw new ArgumentException("Only TopLeft alignment is currently implemented.");

            Text = text;
            Font = font;
            LayoutRectangle = layoutRectangle;

            if (text.Length == 0)
                return;

            CreateBlocks();

            CreateLayout();

            double dx = layoutRectangle.Location.x;
            double dy = layoutRectangle.Location.y + cyAscent;
            int count = blocks.Count;
            for (int idx = 0; idx < count; idx++)
            {
                Block block = (Block)blocks[idx];
                if (block.Stop)
                    break;
                if (block.Type == BlockType.LineBreak)
                    continue;
                gfx.DrawString(block.Text, font, brush, dx + block.Location.x, dy + block.Location.y);
            }
        }

        private void CreateBlocks()
        {
            blocks.Clear();
            int length = text.Length;
            bool inNonWhiteSpace = false;
            int startIndex = 0, blockLength = 0;
            for (int idx = 0; idx < length; idx++)
            {
                char ch = text[idx];

                // Treat CR and CRLF as LF
                if (ch == Chars.CR)
                {
                    if (idx < length - 1 && text[idx + 1] == Chars.LF)
                        idx++;
                    ch = Chars.LF;
                }
                if (ch == Chars.LF)
                {
                    if (blockLength != 0)
                    {
                        string token = text.Substring(startIndex, blockLength);
                        blocks.Add(new Block(token, BlockType.Text,
                          XGraphics.MeasureString(token, font).Width));
                    }
                    startIndex = idx + 1;
                    blockLength = 0;
                    blocks.Add(new Block(BlockType.LineBreak));
                }
                else if (Char.IsWhiteSpace(ch))
                {
                    if (inNonWhiteSpace)
                    {
                        string token = text.Substring(startIndex, blockLength);
                        blocks.Add(new Block(token, BlockType.Text,
                          XGraphics.MeasureString(token, font).Width));
                        startIndex = idx + 1;
                        blockLength = 0;
                    }
                    else
                    {
                        blockLength++;
                    }
                }
                else
                {
                    inNonWhiteSpace = true;
                    blockLength++;
                }
            }
            if (blockLength != 0)
            {
                string token = text.Substring(startIndex, blockLength);
                blocks.Add(new Block(token, BlockType.Text,
                  XGraphics.MeasureString(token, font).Width));
            }
        }

        private void CreateLayout()
        {
            double rectWidth = layoutRectangle.width;
            double rectHeight = layoutRectangle.height - cyAscent - cyDescent;
            int firstIndex = 0;
            double x = 0, y = 0;
            int count = blocks.Count;
            for (int idx = 0; idx < count; idx++)
            {
                Block block = (Block)blocks[idx];
                if (block.Type == BlockType.LineBreak)
                {
                    if (Alignment == XParagraphAlignment.Justify)
                        ((Block)blocks[firstIndex]).Alignment = XParagraphAlignment.Left;
                    AlignLine(firstIndex, idx - 1, rectWidth);
                    firstIndex = idx + 1;
                    x = 0;
                    y += lineSpace;
                }
                else
                {
                    double width = block.Width; //!!!modTHHO 19.11.09 don't add this.spaceWidth here
                    if ((x + width <= rectWidth || x == 0) && block.Type != BlockType.LineBreak)
                    {
                        block.Location = new XPoint(x, y);
                        x += width + spaceWidth; //!!!modTHHO 19.11.09 add this.spaceWidth here
                    }
                    else
                    {
                        AlignLine(firstIndex, idx - 1, rectWidth);
                        firstIndex = idx;
                        y += lineSpace;
                        if (y > rectHeight)
                        {
                            block.Stop = true;
                            break;
                        }
                        block.Location = new XPoint(0, y);
                        x = width + spaceWidth; //!!!modTHHO 19.11.09 add this.spaceWidth here
                    }
                }
            }
            if (firstIndex < count && Alignment != XParagraphAlignment.Justify)
                AlignLine(firstIndex, count - 1, rectWidth);
        }

        /// <summary>
        /// Align center, right or justify.
        /// </summary>
        private void AlignLine(int firstIndex, int lastIndex, double layoutWidth)
        {
            XParagraphAlignment blockAlignment = ((Block)(blocks[firstIndex])).Alignment;
            if (alignment == XParagraphAlignment.Left || blockAlignment == XParagraphAlignment.Left)
                return;

            int count = lastIndex - firstIndex + 1;
            if (count == 0)
                return;

            double totalWidth = -spaceWidth;
            for (int idx = firstIndex; idx <= lastIndex; idx++)
                totalWidth += ((Block)(blocks[idx])).Width + spaceWidth;

            double dx = Math.Max(layoutWidth - totalWidth, 0);
            //Debug.Assert(dx >= 0);
            if (alignment != XParagraphAlignment.Justify)
            {
                if (alignment == XParagraphAlignment.Center)
                    dx /= 2;
                for (int idx = firstIndex; idx <= lastIndex; idx++)
                {
                    Block block = (Block)blocks[idx];
                    block.Location += new XSize(dx, 0);
                }
            }
            else if (count > 1) // case: justify
            {
                dx /= count - 1;
                for (int idx = firstIndex + 1, i = 1; idx <= lastIndex; idx++, i++)
                {
                    Block block = (Block)blocks[idx];
                    block.Location += new XSize(dx * i, 0);
                }
            }
        }

        private readonly List<Block> blocks = [];

        private enum BlockType
        {
            Text, Space, Hyphen, LineBreak,
        }

        /// <summary>
        /// Represents a single word.
        /// </summary>
        private class Block
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Block"/> class.
            /// </summary>
            /// <param name="text">The text of the block.</param>
            /// <param name="type">The type of the block.</param>
            /// <param name="width">The width of the text.</param>
            public Block(string text, BlockType type, double width)
            {
                Text = text;
                Type = type;
                Width = width;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="Block"/> class.
            /// </summary>
            /// <param name="type">The type.</param>
            public Block(BlockType type)
            {
                Type = type;
            }

            /// <summary>
            /// The text represented by this block.
            /// </summary>
            public string Text;

            /// <summary>
            /// The type of the block.
            /// </summary>
            public BlockType Type;

            /// <summary>
            /// The width of the text.
            /// </summary>
            public double Width;

            /// <summary>
            /// The location relative to the upper left corner of the layout rectangle.
            /// </summary>
            public XPoint Location;

            /// <summary>
            /// The alignment of this line.
            /// </summary>
            public XParagraphAlignment Alignment;

            /// <summary>
            /// A flag indicating that this is the last bock that fits in the layout rectangle.
            /// </summary>
            public bool Stop;
        }
        // TODO:
        // - more XStringFormat variations
        // - calculate bounding box
        // - left and right indent
        // - first line indent
        // - margins and paddings
        // - background color
        // - text background color
        // - border style
        // - hyphens, soft hyphens, hyphenation
        // - kerning
        // - change font, size, text color etc.
        // - line spacing
        // - underine and strike-out variation
        // - super- and sub-script
        // - ...
    }
}
