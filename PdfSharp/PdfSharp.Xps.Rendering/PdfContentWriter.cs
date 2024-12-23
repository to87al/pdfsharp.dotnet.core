﻿using PdfSharp.Drawing;
using PdfSharp.Drawing.Pdf;
using PdfSharp.Pdf;
using PdfSharp.Pdf.Advanced;
using PdfSharp.Pdf.Internal;
using PdfSharp.Xps.XpsModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using MatrixTransform = PdfSharp.Xps.XpsModel.MatrixTransform;

#pragma warning disable 414, 169, 649 // incomplete code state

namespace PdfSharp.Xps.Rendering
{
    /// <summary>
    /// Provides the funtionality to write a PDF content stream for a PdfPage or an XForm.
    /// </summary>
    internal partial class PdfContentWriter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PdfContentWriter"/> class
        /// for creating a content stream of the specified page.
        /// </summary>
        public PdfContentWriter(DocumentRenderingContext context, PdfPage page) // , XGraphics gfx, XGraphicsPdfPageOptions options)
        {
            this.context = context;
            this.page = page;
            contentStreamDictionary = page;
            //this.colorMode = page.document.Options.ColorMode;
            //this.options = options;
            content = new StringBuilder();
            graphicsState = new PdfGraphicsState(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfContentWriter"/> class
        /// for creating a content stream of the specified form.
        /// </summary>
        public PdfContentWriter(DocumentRenderingContext context, XForm form, RenderMode renderMode) // , XGraphics gfx, XGraphicsPdfPageOptions options)
        {
            this.context = context;
            this.form = form;
            contentStreamDictionary = form;
            this.renderMode = renderMode;
            //this.colorMode = page.document.Options.ColorMode;
            //this.options = options;
            content = new StringBuilder();
            graphicsState = new PdfGraphicsState(this);
        }

        /// <summary>
        /// </summary>
        public PdfContentWriter(DocumentRenderingContext context, PdfDictionary contentDictionary)
        {
            if (contentDictionary is not IContentStream)
                throw new ArgumentException("contentDictionary must implement IContentStream.");
            this.context = context;
            this.contentDictionary = contentDictionary;
            contentStreamDictionary = (IContentStream)contentDictionary;
            renderMode = RenderMode.Default;
            //this.colorMode = page.document.Options.ColorMode;
            //this.options = options;
            content = new StringBuilder();
            graphicsState = new PdfGraphicsState(this);
        }

        internal PdfPage page;
        internal XForm form;
        internal PdfDictionary contentDictionary;
        internal IContentStream contentStreamDictionary;
        internal RenderMode renderMode;


        /// <summary>
        /// Gets the document rendering context this PDF contentwriter belongs to.
        /// </summary>
        public DocumentRenderingContext Context
        {
            get { return context; }
        }

        private readonly DocumentRenderingContext context;

        internal void CreateDefaultTransparencyGroup() // HACK
        {
            if (page != null)
                page.transparencyUsed = true;
        }

        /// <summary>
        /// Writes all elements of the collection to the content stream.
        /// </summary>
        public void WriteElements(UIElementCollection elements)
        {
            //elements.ForEach(new Action<XpsElement>(writee));
            foreach (UIElement element in elements)
                WriteElement(element);
        }
        /// <summary>
        /// Writes the specified element to the content stream.
        /// </summary>
        public void WriteElement(UIElement element)
        {
            //Comment comment;
            if (element is Canvas canvas)
            {
                BeginGraphic();
                WriteCanvas(canvas);
            }

            else if (element is Path path)
            {
                BeginGraphic();
                WritePath(path);
            }
            else if (element is Glyphs glyphs)
            {
                //BeginText();
                WriteGlyphs(glyphs);
            }
            else if (element is Visual visual)
            {
                Debug.Assert(false, "Shouldn't execute to here.");
                WriteVisual(visual);
                //foreach (XpsElement visualElement in visual.Content)
                //  WriteElement(visualElement);
            }
            // else if ((comment = element as Comment) != null)
            //{
            //DaSt : Comment?
            //WriteGlyphs(glyphs);
            // }
            else
                throw new InvalidOperationException("Invalid element type: " + element.GetType().Name);
        }

        /// <summary>
        /// Writes a Visual to the content stream.
        /// </summary>
        internal void WriteVisual(Visual visual) // Is internal for VisualBrush
        {
            WriteSaveState("begin Visual", null);
            WriteElement(visual as UIElement);
            WriteRestoreState("end Visual", null);
        }

        private void WriteCanvas(Canvas canvas)
        {
            WriteSaveState("begin Canvas", canvas.Name);

            // Transform also affects clipping and opacity mask
            bool transformed = canvas.RenderTransform != null;
            if (transformed)
            {
                var matrix = canvas.RenderTransform.Value;
                var transform = new MatrixTransform(matrix);
                MultiplyTransform(transform);
                WriteRenderTransform(transform);
            }

            bool clipped = canvas.Clip != null;
            if (clipped)
                WriteClip(canvas.Clip);

            if (canvas.Opacity < 1)
                MultiplyOpacity(canvas.Opacity);

            if (canvas.OpacityMask != null)
                WriteOpacityMask(canvas.OpacityMask);

            WriteElements(canvas.Children);
            // Must leave text mode at end of canvas
            BeginGraphic();
            WriteRestoreState("end Canvas", canvas.Name);
        }

#if in_other_file
    /// <summary>
    /// Writes a Glyphs to the content stream.
    /// </summary>
    private void WriteGlyphs(Glyphs glyphs)
    {
      WriteSaveState("begin Glyphs", glyphs.Name);

      // Transform also affects clipping and opacity mask
      bool transformed = glyphs.RenderTransform != null;
      if (transformed)
        WriteRenderTransform(glyphs.RenderTransform);

      bool clipped = glyphs.Clip != null;
      if (clipped)
        WriteClip(glyphs.Clip);

      if (glyphs.Opacity < 1)
        MultiplyOpacity(glyphs.Opacity);

      if (glyphs.OpacityMask != null)
        WriteOpacityMask(glyphs.OpacityMask);

      XForm xform = null;
      XImage ximage = null;
      RealizeFill(glyphs.Fill, ref xform, ref ximage);
      RealizeFont(glyphs);

      double x = glyphs.OriginX;
      double y = glyphs.OriginY;


      //switch (format.Alignment)
      //{
      //  case XStringAlignment.Near:
      //    // nothing to do
      //    break;

      //  case XStringAlignment.Center:
      //    x += (rect.Width - width) / 2;
      //    break;

      //  case XStringAlignment.Far:
      //    x += rect.Width - width;
      //    break;
      //}

      PdfFont realizedFont = this.graphicsState.realizedFont;
      Debug.Assert(realizedFont != null);
      realizedFont.AddChars(glyphs.UnicodeString);

      TrueTypeDescriptor descriptor = realizedFont.FontDescriptor.descriptor;

      //if (bold && !descriptor.IsBoldFace)
      //{
      //  // TODO: emulate bold by thicker outline
      //}

      //if (italic && !descriptor.IsBoldFace)
      //{
      //  // TODO: emulate italic by shearing transformation
      //}

      string s2 = "";
      string s = glyphs.UnicodeString;
      if (!String.IsNullOrEmpty(s))
      {
        int length = s.Length;
        for (int idx = 0; idx < length; idx++)
        {
          char ch = s[idx];
          int glyphID = 0;
          if (descriptor.fontData.cmap.symbol)
          {
            glyphID = (int)ch + (descriptor.fontData.os2.usFirstCharIndex & 0xFF00);
            glyphID = descriptor.CharCodeToGlyphIndex((char)glyphID);
          }
          else
            glyphID = descriptor.CharCodeToGlyphIndex(ch);
          s2 += (char)glyphID;
        }
      }
      s = s2;

      byte[] bytes = PdfEncoders.RawUnicodeEncoding.GetBytes(s);
      bytes = PdfEncoders.FormatStringLiteral(bytes, true, false, true, null);
      string text = PdfEncoders.RawEncoding.GetString(bytes);
      if (glyphs.IsSideways)
      {
        XMatrix textMatrix = new XMatrix();
        textMatrix.RotateAtPrepend(-90, new XPoint(x, y));
        XPoint pos = new XPoint(x, y);
        AdjustTextMatrix(ref pos);
        WriteTextTransform(textMatrix);
        WriteLiteral("{0} Tj\n", text);
      }
      else
      {
        XPoint pos = new XPoint(x, y);
        AdjustTextMatrix(ref pos);
        WriteLiteral("{0:0.###} {1:0.###} Td {2} Tj\n", pos.x, pos.y, text);
        //PdfEncoders.ToStringLiteral(s, PdfStringEncoding.RawEncoding, null));
      }
      WriteRestoreState("end Glyphs", glyphs.Name);
    }

    // ...on the way to handle Indices...
    private void WriteGlyphs_Indices1(Glyphs glyphs, string text)
    {
      GlyphIndicesComplexity complexity = glyphs.Indices.Complexity;
      complexity = GlyphIndicesComplexity.ClusterMapping;
      switch (complexity)
      {
        case GlyphIndicesComplexity.None:
          break;

        case GlyphIndicesComplexity.DistanceOnly:
          break;

        case GlyphIndicesComplexity.GlyphIndicesAndDistanceOnly:
          break;

        case GlyphIndicesComplexity.ClusterMapping:
          WriteGlyphs_ClusterMapping(glyphs, text);
          break;
      }
    }

    private void WriteGlyphs_None(Glyphs glyphs, string text)
    {
      // TODO:
    }

    private void WriteGlyphs_DistanceOnly(Glyphs glyphs, string text)
    {
      // TODO:
    }

    private void WriteGlyphs_GlyphIndicesAndDistanceOnly(Glyphs glyphs, string text)
    {
      // TODO:
    }

    private void WriteGlyphs_ClusterMapping(Glyphs glyphs, string text)
    {
      GlyphIndices indices = glyphs.Indices;
      int chCount = text.Length;
      //int chidx = 0;
      int glCont = indices.Count;
      int glidx = 0;
      bool stop = false;
      do
      {
        GlyphIndices.GlyphMapping mapping = indices[glidx];

//        if (mapping.HasClusterCount
      }
      while (!stop);
    }
#endif

        /// <summary>
        /// Writes a Path to the content stream.
        /// </summary>
        private void WritePath(Path path)
        {
            if (WriteSingleLineStrokeWithSpecialCaps(path))
                return;

            WriteSaveState("begin Path", path.Name);

            // Transform also affects clipping and opacity mask
            if (path.RenderTransform != null
              && !path.RenderTransform.Value.IsIdentity
              && renderMode == RenderMode.Default)
            {
                var matrix = path.RenderTransform.Value;
                var transform = new MatrixTransform(matrix);
                MultiplyTransform(transform);
                WriteRenderTransform(transform);
            }

            if (path.Clip != null && renderMode == RenderMode.Default)
                WriteClip(path.Clip);

            if (path.Opacity < 1)
                MultiplyOpacity(path.Opacity);

            if (path.OpacityMask != null)
                WriteOpacityMask(path.OpacityMask);

            if (path.Fill == null)
            {
                if (path.Stroke != null)
                {
#if true
                    WriteStrokeGeometry(path);
#else
          // Just stroke the path
          RealizeStroke(path);
          WriteGeometry(path.Data);
          WritePathFillStroke(path);
#endif
                }
                else
                    Debug.Assert(false, "??? Path with neither Stroke nor Fill encountered.");
            }
            else
            {
                if (path.Fill is SolidColorBrush sBrush)
                {
                    var color = sBrush.Color;
                    double opacity = Opacity * color.ScA;
                    if (opacity < 1)
                        RealizeFillOpacity(opacity);

                    WriteRgb(color, " rg\n");

                    if (path.Stroke != null)
                        RealizeStroke(path);

                    WriteGeometry(path.Data.GetFlattenedPathGeometry());
                    WritePathFillStroke(path);
                }
                else if (path.Fill is LinearGradientBrush lgBrush)
                {
                    // TODO: For better visual compatibility use a Shading Pattern if Opacity is not 1 and
                    // the Stroke Style is not solid. Acrobat 8 ignores this case.

                    var xgState = Context.PdfDocument.Internals.CreateIndirectObject<PdfExtGState>();
                    xgState.SetDefault1();

                    double opacity = Opacity * lgBrush.Opacity;
                    if (opacity < 1)
                    {
                        xgState.StrokeAlpha = opacity;
                        xgState.NonStrokeAlpha = opacity;
                    }
                    RealizeExtGState(xgState);

                    // 1st draw fill
                    if (GradientStopCollectionHelper.HasTransparency(lgBrush.GradientStops))
                    {
                        // Create a FormXObject with a soft mask
                        PdfFormXObject form = LinearShadingBuilder.BuildFormFromLinearGradientBrush(Context, lgBrush, path.Data.GetFlattenedPathGeometry());
                        string foName = Resources.AddForm(form);
                        WriteLiteral(foName + " Do\n");
                    }
                    else
                    {
                        // Create just a shading
                        PdfShading shading = LinearShadingBuilder.BuildShadingFromLinearGradientBrush(Context, lgBrush);
                        string shName = Resources.AddShading(shading);
                        WriteLiteral("q\n");
                        WriteClip(path.Data);
                        WriteLiteral("BX " + shName + " sh EX Q\n");
                    }

                    // 2nd draw stroke
                    if (path.Stroke != null)
                        WriteStrokeGeometry(path);
                }
                else if (path.Fill is RadialGradientBrush rgBrush)
                {
                    var xgState = Context.PdfDocument.Internals.CreateIndirectObject<PdfExtGState>();
                    xgState.SetDefault1();

                    double avGradientOpacity = rgBrush.GradientStops.GetAverageAlpha(); // HACK
                    double opacity = Opacity * rgBrush.Opacity * avGradientOpacity;
                    if (opacity < 1)
                    {
                        xgState.StrokeAlpha = opacity;
                        xgState.NonStrokeAlpha = opacity;
                    }
                    //RealizeExtGState(xgState);

#if true
                    XRect boundingBox = path.Data.Bounds;
                    // Always creates a pattern, because the background must be filled
                    var pattern = RadialShadingBuilder.BuildFromRadialGradientBrush(Context, rgBrush, boundingBox, Transform);
                    string paName = Resources.AddPattern(pattern);

                    // stream
                    // /CS0 cs /P0 scn
                    // /GS0 gs
                    // 0 480 180 -90 re
                    // f*
                    // endstream
                    // endobj
                    WriteLiteral("/Pattern cs " + paName + " scn\n");
                    // move to here: RealizeExtGState(xgState);
                    RealizeExtGState(xgState);
                    var geo = path.Data.GetFlattenedPathGeometry();
                    WriteGeometry(geo);
                    if (geo.FillRule == FillRule.Nonzero) // NonZero means Winding
                        WriteLiteral("f\n");
                    else
                        WriteLiteral("f*\n");
#else
#if true
          // 1st draw fill
          if (rgBrush.GradientStops.HasTransparentColors)
          {
            // Create a FormXObject with a soft mask
            PdfFormXObject form = ShadingBuilder.BuildFormFromRadialGradientBrush(Context, rgBrush, path.Data);
            string foName = Resources.AddForm(form);
            WriteLiteral(foName + " Do\n");
          }
          else
          {
            // Create just a shading
            PdfShading shading = ShadingBuilder.BuildShadingFromRadialGradientBrush(Context, rgBrush);
            string shName = Resources.AddShading(shading);
            WriteLiteral("q\n");
            WriteClip(path.Data);
            WriteLiteral("BX " + shName + " sh EX Q\n");
          }
#else
          // Establish graphic state dictionary
          PdfExtGState extGState = new PdfExtGState(Context.PdfDocument);
          Context.PdfDocument.Internals.AddObject(extGState);
          string gsName = Resources.AddExtGState(extGState);
          WriteLiteral(gsName + " gs\n");

          // 1st draw fill
          PdfShading shading = ShadingBuilder.BuildFormFromRadialGradientBrush(Context, rgBrush); //, extGState);
          string shName = Resources.AddShading(shading);

          WriteClip(path.Data);
          WriteLiteral("BX " + shName + " sh EX\n");
#endif
#endif

                    // 2nd draw stroke
                    if (path.Stroke != null)
                        WriteStrokeGeometry(path);
                }
                else if (path.Fill is ImageBrush iBrush)
                {
                    var xgState = Context.PdfDocument.Internals.CreateIndirectObject<PdfExtGState>();
                    xgState.SetDefault1();

                    double opacity = Opacity * iBrush.Opacity;
                    if (opacity <= 1)
                    {
                        xgState.StrokeAlpha = opacity;
                        xgState.NonStrokeAlpha = opacity;
                    }
                    RealizeExtGState(xgState);

                    // 1st draw fill
                    PdfTilingPattern pattern = TilingPatternBuilder.BuildFromImageBrush(Context, iBrush, Transform);
                    string name = Resources.AddPattern(pattern);

                    WriteLiteral("/Pattern cs " + name + " scn\n");
                    WriteGeometry(path.Data.GetFlattenedPathGeometry());
                    WritePathFillStroke(path);

                    // 2nd draw stroke
                    if (path.Stroke != null)
                        WriteStrokeGeometry(path);
                }
                else if (path.Fill is VisualBrush vBrush)
                {
                    var xgState = Context.PdfDocument.Internals.CreateIndirectObject<PdfExtGState>();
                    xgState.SetDefault1();

                    double opacity = Opacity * vBrush.Opacity;
                    if (opacity < 1)
                    {
                        xgState.StrokeAlpha = opacity;
                        xgState.NonStrokeAlpha = opacity;
                    }
                    RealizeExtGState(xgState);

                    // 1st draw fill
                    var pattern = TilingPatternBuilder.BuildFromVisualBrush(Context, vBrush, Transform);
                    string name = Resources.AddPattern(pattern);

                    WriteLiteral("/Pattern cs " + name + " scn\n");
                    WriteGeometry(path.Data.GetFlattenedPathGeometry());
                    WritePathFillStroke(path);

                    // 2nd draw stroke
                    if (path.Stroke != null)
                        WriteStrokeGeometry(path);
                }
                else
                {
                    Debug.Assert(false, "Unknown brush type encountered.");
                }
            }
            WriteRestoreState("end Path", path.Name);
        }

        /// <summary>
        /// Strokes the path geometry with the Stroke brush.
        /// </summary>
        private void WriteStrokeGeometry(Path path)
        {
            if (path.Stroke == null)
                return;

            //if (path.Stroke != null && this.renderMode == RenderMode.Default) // HACK

            if (path.Stroke is SolidColorBrush sBrush)
            {
                RealizeStroke(path);
                WriteGeometry(path.Data.GetFlattenedPathGeometry());
                WriteLiteral("S\n");
            }
            else if (path.Stroke is LinearGradientBrush lgBrush)
            {
                var xgState = Context.PdfDocument.Internals.CreateIndirectObject<PdfExtGState>();
                xgState.SetDefault1();

                double opacity = Opacity * lgBrush.Opacity;
                if (opacity < 1)
                {
                    xgState.StrokeAlpha = opacity;
                    xgState.NonStrokeAlpha = opacity;
                }
                RealizeExtGState(xgState);

                // /CS1 CS /P0 SCN
                // 7.5 w 
                // q 1 0 0 1 15.5 462.9 cm
                // 0 0 m
                // 153 0 l
                // 153 -93 l
                // 0 -93 l
                // h
                // S
                // Q
                if (lgBrush.GradientStops.HasTransparency())
                {
                    // TODO: Create Form
                    var pattern = LinearShadingBuilder.BuildPatternFromLinearGradientBrush(Context, lgBrush, Transform);
                    string paName = Resources.AddPattern(pattern);
                    WriteLiteral("/Pattern CS " + paName + " SCN\n");
                    WriteLiteral("q {0:0.###} w", path.StrokeThickness);
                    WriteGeometry(path.Data.GetFlattenedPathGeometry());
                    WriteLiteral("S Q\n");

                    //// Create a FormXObject with a soft mask
                    //PdfFormXObject form = LinearShadingBuilder.BuildFormFromLinearGradientBrush(Context, lgBrush, path.Data);
                    //string foName = Resources.AddForm(form);
                    //WriteLiteral(foName + " Do\n");
                }
                else
                {
                    PdfShadingPattern pattern = LinearShadingBuilder.BuildPatternFromLinearGradientBrush(Context, lgBrush, Transform);
                    string paName = Resources.AddPattern(pattern);
                    WriteLiteral("/Pattern CS " + paName + " SCN\n");
                    WriteLiteral("q {0:0.###} w", path.StrokeThickness);
                    WriteGeometry(path.Data.GetFlattenedPathGeometry());
                    WriteLiteral("S Q\n");
                }
            }
            else if (path.Stroke is RadialGradientBrush rgBrush)
            {
                // HACK
                WriteLiteral("/DeviceRGB CS 0 1 0 RG\n");
                WriteLiteral("q {0:0.###} w", path.StrokeThickness);
                WriteGeometry(path.Data.GetFlattenedPathGeometry());
                WriteLiteral("S Q\n");
            }
            else if (path.Stroke is ImageBrush iBrush)
            {
                // HACK
                WriteLiteral("/DeviceRGB CS 0 1 0 RG\n");
                WriteLiteral("q {0:0.###} w", path.StrokeThickness);
                WriteGeometry(path.Data.GetFlattenedPathGeometry());
                WriteLiteral("S Q\n");
            }
            else if (path.Stroke is VisualBrush vBrush)
            {
                // HACK
                WriteLiteral("/DeviceRGB CS 0 1 0 RG\n");
                WriteLiteral("q {0:0.###} w", path.StrokeThickness);
                WriteGeometry(path.Data.GetFlattenedPathGeometry());
                WriteLiteral("S Q\n");
            }
            else
                Debug.Assert(false);
        }

        /// <summary>
        /// If the path is a single line with different start and end caps, convert the line into an area.
        /// </summary>
        private static bool WriteSingleLineStrokeWithSpecialCaps(Path path)
        {
            if (path.StrokeStartLineCap == path.StrokeEndLineCap
              && path.StrokeStartLineCap != PenLineCap.Triangle)
                return false;
            var figures = path.Data.GetFlattenedPathGeometry().Figures;
            if (figures.Count != 1)
                return false;
            var figure = figures[0];
            if (figure.Segments.Count != 1)
                return false;
            var polyLineSegment = figure.Segments[0] as PolyLineSegment;
            if (polyLineSegment.Points.Count != 1)
                return false;

            // TODO: Create a new path that draws the line
            path.GetType();

            return false;
        }












        /// <summary>
        /// Gets the content created by this renderer.
        /// </summary>
        private string GetContent()
        {
            EndContent();
            return content.ToString();
        }

        //    public XGraphicsPdfPageOptions PageOptions
        //    {
        //      get { return this.options; }
        //    }

        /// <summary>
        /// Closes the underlying content stream.
        /// </summary>
        private void Close()
        {
            if (page != null)
            {
                PdfContent content = page.RenderContent;
                content.CreateStream(PdfEncoders.RawEncoding.GetBytes(GetContent()));

                //this.gfx = null;
                page.RenderContent.pdfRenderer = null;
                page.RenderContent = null;
                page = null;
            }
            else if (form != null)
            {
                form.pdfForm.CreateStream(PdfEncoders.RawEncoding.GetBytes(GetContent()));
                form.pdfRenderer = null;
                form = null;
            }
            else if (contentDictionary != null)
            {
                contentDictionary.CreateStream(PdfEncoders.RawEncoding.GetBytes(GetContent()));
                contentDictionary = null;
            }
            else
                Debug.Assert(false, "Undefined content target.");
        }

        //    #region Realizing graphical state

        /// <summary>
        /// Initializes the default view transformation, i.e. the transformation from the user page
        /// space to the PDF page space.
        /// </summary>
        public void BeginContent(bool hacks4softmask)
        {
            if (!contentInitialized)
            {
                //this.defaultViewMatrix = new XMatrix();  //XMatrix.Identity;
                //// Take TrimBox into account
                double pageHeight = Size.Height;
                //XPoint trimOffset = new XPoint();
                //if (this.page != null && this.page.TrimMargins.AreSet)
                //{
                //  pageHeight += this.page.TrimMargins.Top.Point + this.page.TrimMargins.Bottom.Point;
                //  trimOffset = new XPoint(this.page.TrimMargins.Left.Point, this.page.TrimMargins.Top.Point);
                //}

                //if (this.page != null && this.page.Elements.GetInteger("/Rotate") == 90)  // HACK for InDesign flyer
                //{
                //  defaultViewMatrix.RotatePrepend(90);
                //  defaultViewMatrix.ScalePrepend(1, -1);
                //}
                //else
                //{
                //  // Recall that the value of Height depends on Orientation.

                // Flip page horizontaly and mirror text.
                XMatrix defaultViewMatrix = new();
                if (!hacks4softmask)
                {
                    defaultViewMatrix.TranslatePrepend(0, pageHeight);
                    defaultViewMatrix.ScalePrepend(0.75, -0.75);
                }
                //if (!trimOffset.IsEmpty)
                //{
                //  Debug.Assert(this.gfx.PageUnit == XGraphicsUnit.Point, "With TrimMargins set the page units must be Point. Ohter cases nyi.");
                //  defaultViewMatrix.TranslatePrepend(trimOffset.x, trimOffset.y);
                //}

                // Save initial graphic state
                WriteSaveState("BeginContent", null);
                // Set page transformation
                WriteRenderTransform(defaultViewMatrix);
                graphicsState.DefaultPageTransform = defaultViewMatrix;
                MultiplyTransform(defaultViewMatrix);
                if (!hacks4softmask)
                    WriteLiteral("-100 Tz\n");
                contentInitialized = true;
            }
        }

        private bool contentInitialized;

        /// <summary>
        /// Just save current state.
        /// </summary>
        public void BeginContentRaw()
        {
            if (!contentInitialized)
            {
                // Save initial graphic state
                WriteSaveState("BeginContent", null);
                contentInitialized = true;
            }
        }

        ///// <summary>
        ///// Initializes the default view transformation, i.e. the transformation from the user page
        ///// space to the PDF page space.
        ///// Ends the content stream, i.e. ends the text mode and balances the graphic state stack.
        ///// </summary>
        ///// </summary>
        //public void EndPage()
        //{
        //  if (this.streamMode == StreamMode.Text)
        //  {
        //    WriteLiteral("ET\n");
        //    this.streamMode = StreamMode.Graphic;
        //  }

        //  while (this.graphicsStateStack.Count != 0)
        //    WriteRestoreState("EndPage");
        //}

        public void EndContent()
        {
            if (!pageFinished)
            {
                pageFinished = true;
                if (streamMode == StreamMode.Text)
                {
                    WriteLiteral("ET\n");
                    streamMode = StreamMode.Graphic;
                }
                WriteRestoreState("EndContent", null);
                Debug.Assert(graphicsStateStack.Count == 0);
                //while (this.graphicsStateStack.Count != 0)
                //  WriteRestoreState("EndPage");
                Close();
            }
        }

        private bool pageFinished;

        internal void WriteMoveStart(Point point)
        {
            WriteLiteral("{0:0.###} {1:0.###} m\n", point.X, point.Y);
            currentPoint = point;
        }

        private Point currentPoint = new();

        internal void WriteGeometry(PathGeometry geo)
        {
            BeginGraphic();

            // PathGeometry itself may have its own transform
            if (geo.Transform != null) // also check render mode?
            {
                var matrix = geo.Transform.Value;
                MatrixTransform transform = new(matrix);
                MultiplyTransform(transform);
                WriteRenderTransform(transform);
            }

            foreach (var figure in geo.Figures)
            {
                PolyLineSegment pseg;

                // And now for the most superfluous and unnecessary optimization within the whole PDFsharp library
                if (figure.IsClosed && figure.Segments.Count == 1)
                {
                    pseg = figure.Segments[0] as PolyLineSegment;
                    if (pseg != null && pseg.Points.Count == 4)//the last point is also the start point
                    {
                        // Identify rectangles
                        var pt0 = figure.StartPoint;
                        var pt1 = pseg.Points[0];
                        var pt2 = pseg.Points[1];
                        var pt3 = pseg.Points[2];
                        // This
                        //   M16,0 L24,0 24,144 16,144 Z
                        // becomes
                        //   16 0 m  24 0 l  24 144 l  16 144 l  h
                        // but shorter is this
                        //   16 0 8 144 re
                        if (pt0.X.Equals(pt3.X)
                            && pt0.Y.Equals(pt1.Y)
                            && pt1.X.Equals(pt2.X)
                            && pt2.Y.Equals(pt3.Y))
                        {
                            WriteLiteral("{0:0.###} {1:0.###} {2:0.###} {3:0.###} re \n", pt0.X, pt0.Y, pt2.X - pt0.X, pt2.Y - pt1.Y);
                            continue;
                        }
                    }
                }
                WriteMoveStart(figure.StartPoint);
                foreach (var seg in figure.Segments)
                {
                    if (seg is LineSegment lseg)
                    {
                        WriteSegment(lseg);
                    }
                    else if ((pseg = seg as PolyLineSegment) != null)
                        WriteSegment(pseg);
                    else if (seg is PolyBezierSegment bseg)
                        WriteSegment(bseg);
                    else if (seg is ArcSegment aseg)
                        WriteSegment(aseg);
                    else if (seg is PolyQuadraticBezierSegment qseg)
                        WriteSegment(qseg);
                }
                if (figure.IsClosed)
                    WriteLiteral("h\n"); // Close current figure
            }
        }

        private void WriteSegment(PolyLineSegment seg)
        {
#if DEBUG_
      // WPF uses zero lengh rounded line to draw circles. These circles PDF renders without antializing (Acrobat 8.1)
      // TODO: Bug in Acrobat or rendering issue?
      if (seg.Points.Count == 1)
      {
        if (seg.Points[0] == this.currentPoint)
          seg.Points[0] = new Point(this.currentPoint.X + 10, this.currentPoint.Y);
      }
#endif
            foreach (var point in seg.Points)
            {
                WriteLiteral("{0:0.###} {1:0.###} l\n", point.X, point.Y);
                currentPoint = point;
            }
        }

        private void WriteSegment(LineSegment seg)
        {
            var point = seg.Point;
            WriteLiteral("{0:0.###} {1:0.###} l\n", point.X, point.Y);
            currentPoint = point;
        }

        /// <summary>
        /// Writes the specified PolyBezierSegment to the content stream.
        /// </summary>
        internal void WriteSegment(PolyBezierSegment seg)
        {
            int count = seg.Points.Count;
            var points = seg.Points;
            for (int idx = 0; idx < count - 2; idx += 3)
            {
                WriteLiteral("{0:0.###} {1:0.###} {2:0.###} {3:0.###} {4:0.###} {5:0.###} c\n",
                  points[idx].X, points[idx].Y, points[idx + 1].X, points[idx + 1].Y, points[idx + 2].X, points[idx + 2].Y);
                var point = points[idx + 2];
                currentPoint = point;
            }
        }

        /// <summary>
        /// Writes the specified PolyQuadraticBezierSegment to the content stream.
        /// </summary>
        internal void WriteSegment(PolyQuadraticBezierSegment seg)
        {
#if true
            if (!DevHelper.FlattenPolyQuadraticBezierSegment)
            {
                int count = seg.Points.Count;
                var points = seg.Points;
                var pt0 = currentPoint;
                for (int idx = 0; idx < count - 1;)
                {
                    var pt1 = points[idx++];
                    var pt2 = points[idx++];
                    // Cannot believe it: I just guessed the formula and it works!
                    WriteLiteral("{0:0.####} {1:0.####} {2:0.####} {3:0.####} {4:0.####} {5:0.####} c\n",
                      pt1.X - ((pt1.X - pt0.X) / 3), pt1.Y - ((pt1.Y - pt0.Y) / 3),
                      pt1.X + ((pt2.X - pt1.X) / 3), pt1.Y + ((pt2.Y - pt1.Y) / 3),
                      pt2.X, pt2.Y);
                    currentPoint = pt2;
                    pt0 = currentPoint;
                }
            }
            else
            {
                var lseg = WpfUtils.FlattenSegment(currentPoint, seg);
                WriteSegment(lseg);
            }
#else
      // TODO: Convert quadratic Bezier curve in cubic Bézier curve
      PolyLineSegment lseg = WpfUtils.FlattenSegment(this.currentPoint, seg);
      WriteSegment(lseg);
#endif
        }

        /// <summary>
        /// Writes the specified ArcSegment to the content stream.
        /// </summary>
        internal void WriteSegment(ArcSegment seg)
        {
            if (!DevHelper.FlattenArcSegments)
            {
                PointCollection points =
                  GeometryHelper.ArcToBezier(currentPoint.X, currentPoint.Y, seg.Size.Width, seg.Size.Height, seg.RotationAngle, seg.IsLargeArc,
                    seg.SweepDirection == SweepDirection.Clockwise,
                    seg.Point.X, seg.Point.Y, out int pieces);
                if (pieces == 0)
                {
                    // just draw single line
                    WriteLiteral("{0:0.####} {1:0.####} l\n", seg.Point.X, seg.Point.Y);
                    currentPoint = seg.Point;
                    return;
                }
                else if (pieces < 0)
                    return;

                int count = points.Count;
                Debug.Assert(count % 3 == 0);
                for (int idx = 0; idx < count - 2; idx += 3)
                {
                    WriteLiteral("{0:0.####} {1:0.####} {2:0.####} {3:0.####} {4:0.####} {5:0.####} c\n",
                      points[idx].X, points[idx].Y, points[idx + 1].X, points[idx + 1].Y, points[idx + 2].X, points[idx + 2].Y);
                    currentPoint = new Point(points[idx + 2].X, points[idx + 2].Y);
                }
            }
            else
            {
                var lseg = WpfUtils.FlattenSegment(currentPoint, seg);
                WriteSegment(lseg);
            }
        }

        /// <summary>
        /// Writes the path fill and/or stroke operators to the content stream.
        /// </summary>
        internal void WritePathFillStroke(Path path)
        {
            var geo = path.Data.GetFlattenedPathGeometry();
            if (geo.FillRule == FillRule.Nonzero) // NonZero means Winding
            {
                if (path.Fill != null && path.Stroke != null)
                    WriteLiteral("B\n");
                else if (path.Stroke != null)
                    WriteLiteral("S\n");
                else
                    WriteLiteral("f\n");
            }
            else
            {
                if (path.Fill != null && path.Stroke != null)
                    WriteLiteral("B*\n");
                else if (path.Stroke != null)
                    WriteLiteral("S\n");
                else
                    WriteLiteral("f*\n");
            }
        }

        /// <summary>
        /// Begins the graphic mode (i.e. ends the text mode).
        /// </summary>
        internal void BeginGraphic()
        {
            if (streamMode != StreamMode.Graphic)
            {
                if (streamMode == StreamMode.Text)
                    WriteLiteral("ET\n");

                streamMode = StreamMode.Graphic;
            }
        }

        /// <summary>
        /// Begins the text mode.
        /// </summary>
        internal void BeginText()
        {
            if (streamMode != StreamMode.Text)
            {
                streamMode = StreamMode.Text;
                WriteLiteral("BT\n");
                // Text matrix is empty after BT
                graphicsState.realizedTextPosition = new XPoint();
            }
        }

        private StreamMode streamMode;

        //public void RenderTransform(MatrixTransform transform)
        //{
        //  BeginGraphic();
        //  AppendFormat("{0:0.####} {1:0.####} {2:0.####} {3:0.####} {4:0.####} {5:0.####} cm\n",
        //    transform.Matrix.m11, transform.Matrix.m12, transform.Matrix.m21, transform.Matrix.m22, transform.Matrix.offsetX, transform.Matrix.offsetY);
        //}

        /// <summary>
        /// Writes the specified transformation matrix to the content stream.
        /// </summary>
        public void WriteRenderTransform(XMatrix matrix)
        {
            BeginGraphic();
            WriteLiteral("{0:0.####} {1:0.####} {2:0.####} {3:0.####} {4:0.####} {5:0.####} cm\n",
              matrix.M11, matrix.M12, matrix.M21, matrix.M22, matrix.OffsetX, matrix.OffsetY);
            graphicsState.currentTransform.Prepend(matrix);
        }

        /// <summary>
        /// Writes the specified text transformation matrix to the content stream.
        /// </summary>
        public void WriteTextTransform(XMatrix matrix)
        {
            Debug.Assert(streamMode == StreamMode.Text, "Must be in text mode when setting text matrix.");
            WriteLiteral("{0:0.####} {1:0.####} {2:0.####} {3:0.####} {4:0.####} {5:0.####} Tm\n",
              matrix.M11, matrix.M12, matrix.M21, matrix.M22, matrix.OffsetX, matrix.OffsetY);
        }

        /// <summary>
        /// Writes the specified rectangle as intersection with the current clip path to the content stream.
        /// </summary>
        public void WriteClip(XRect rect)
        {
            BeginGraphic();
            WriteLiteral("{0:0.####} {1:0.####} {2:0.####} {3:0.####} re W n",
              rect.X, rect.Y, rect.X + rect.Width, rect.Y + rect.Height);
        }

        /// <summary>
        /// Writes the specified PathGeometry as intersection with the current clip path to the content stream.
        /// </summary>
        public void WriteClip(PathGeometry geo)
        {
            BeginGraphic();

            WriteGeometry(geo);

            if (geo.FillRule == FillRule.Nonzero)
                WriteLiteral("W n\n");
            else
                WriteLiteral("W* n\n");
        }

        public void WriteClip(Geometry geo)
        {
            var pathGeo = geo.GetFlattenedPathGeometry();
            BeginGraphic();

            WriteGeometry(pathGeo);

            if (pathGeo.FillRule == FillRule.Nonzero)
                WriteLiteral("W n\n");
            else
                WriteLiteral("W* n\n");
        }

        public static void WriteOpacityMask(Brush brush)
        {
            // TODO
        }

        /// <summary>
        /// Gets the default page transformation.
        /// </summary>
        public XMatrix DefaultPageTransform
        {
            get { return graphicsState.DefaultPageTransform; }
        }

        /// <summary>
        /// Gets the current transformation.
        /// </summary>
        public XMatrix Transform
        {
            get { return graphicsState.Transform; }
        }

        /// <summary>
        /// Muliplies the spcified transformation with the current transformation and returns the new value;
        /// </summary>
        public XMatrix MultiplyTransform(XMatrix matrix)
        {
            return graphicsState.MultiplyTransform(matrix);
        }

        /// <summary>
        /// Gets the current opacity value.
        /// </summary>
        public double Opacity
        {
            get { return graphicsState.Opacity; }
        }

        /// <summary>
        /// Muliplies the spcified opacity with the current opacity and returns the new value;
        /// </summary>
        public double MultiplyOpacity(double opacity)
        {
            return graphicsState.MuliplyOpacity(opacity);
        }

        ///// <summary>
        ///// Makes the specified pen and brush to the current graphics objects.
        ///// </summary>
        //void Realize(Pen pen, XBrush brush)
        //{
        //  BeginPage();
        //  BeginGraphic();
        //  RealizeTransform();

        //  if (pen != null)
        //    this.gfxState.RealizePen(pen, this.colorMode); // this.page.document.Options.ColorMode);

        //  if (brush != null)
        //    this.gfxState.RealizeBrush(brush, this.colorMode); // this.page.document.Options.ColorMode);
        //}

        /// <summary>
        /// Makes the specified brush to the current graphics object.
        /// </summary>
        public void RealizeFill(Brush brush, ref XForm xform, ref XImage ximage)
        {
            graphicsState.RealizeFill(brush, 1, ref xform, ref ximage);
        }

        /// <summary>
        /// Realizes the opacity for non-stroke operations.
        /// </summary>
        public void RealizeFillOpacity(double opacity)
        {
            graphicsState.RealizeFillOpacity(opacity);
        }

        /// <summary>
        /// Makes the specified pen to the current graphics object.
        /// </summary>
        public void RealizeStroke(Path path)
        {
            graphicsState.RealizeStroke(path);
        }

        /// <summary>
        /// Realizes the opacity for stroke operations.
        /// </summary>
        public void RealizeStrokeOpacity(double opacity)
        {
            graphicsState.RealizeStrokeOpacity(opacity);
        }

        /// <summary>
        /// Realizes the specified graphic state.
        /// </summary>
        public void RealizeExtGState(PdfExtGState xgState)
        {
            graphicsState.RealizeExtGState(xgState);
        }

        /// <summary>
        /// Makes the specified font and brush to the current graphics objects.
        /// </summary>
        private void RealizeFont(Glyphs glyphs)
        {
            if (streamMode != StreamMode.Text)
            {
                streamMode = StreamMode.Text;
                WriteLiteral("BT\n");
                // Text matrix is empty after BT
                graphicsState.realizedTextPosition = new XPoint();
            }
            graphicsState.RealizeFont(glyphs);
        }

        private void AdjustTextMatrix(ref XPoint pos)
        {
            XPoint posSave = pos;
            pos -= new XVector(graphicsState.realizedTextPosition.X, graphicsState.realizedTextPosition.Y);
            graphicsState.realizedTextPosition = posSave;
        }

        //    /// <summary>
        //    /// Makes the specified image to the current graphics object.
        //    /// </summary>
        //    string Realize(XImage image)
        //    {
        //      BeginPage();
        //      BeginGraphic();
        //      RealizeTransform();

        //      string imageName;
        //      if (image is XForm)
        //        imageName = GetFormName(image as XForm);
        //      else
        //        imageName = GetImageName(image);
        //      return imageName;
        //    }

        //    /// <summary>
        //    /// Realizes the current transformation matrix, if necessary.
        //    /// </summary>
        //    void RealizeTransform()
        //    {
        //      BeginPage();

        //      if (this.gfxState.Level == GraphicsStackLevelPageSpace)
        //      {
        //        BeginGraphic();
        //        SaveState();
        //      }

        //      if (gfxState.MustRealizeCtm)
        //      {
        //        BeginGraphic();
        //        this.gfxState.RealizeCtm();
        //      }
        //    }

        //    #endregion


        /// <summary>
        /// Gets the owning PdfDocument of this page or form.
        /// </summary>
        internal PdfDocument Owner
        {
            get
            {
                if (page != null)
                    return page.Owner;
                else if (form != null)
                    return form.Owner;
                else if (contentDictionary != null)
                    return contentDictionary.Owner;

                Debug.Assert(false, "Undefined conent target.");
                return null;
            }
        }

        /// <summary>
        /// Gets the PdfResources of this page or form.
        /// </summary>
        internal PdfResources Resources
        {
            get
            {
                if (page != null)
                    return page.Resources;
                else if (form != null)
                    return form.Resources;
                else if (contentStreamDictionary != null)
                    return contentStreamDictionary.Resources;

                Debug.Assert(false, "Undefined conent target.");
                return null;
            }
        }

        /// <summary>
        /// Gets the size of this page or form.
        /// </summary>
        internal XSize Size
        {
            get
            {
                if (page != null)
                    return new XSize(page.Width, page.Height);
                else if (form != null)
                    return form.Size;
                else if (contentDictionary != null)
                {
                    throw new NotImplementedException("Size");
                }


                Debug.Assert(false, "Undefined conent target.");
                return new XSize();
            }
        }

        /// <summary>
        /// Gets the resource name of the specified font within this page or form.
        /// </summary>
        internal string GetFontName(XFont font, out PdfFont pdfFont)
        {
            if (page != null)
                return page.GetFontName(font, out pdfFont);
            else if (form != null)
                return form.GetFontName(font, out pdfFont);
            else if (contentDictionary != null)
            {
                throw new NotImplementedException("GetFontName");
            }

            Debug.Assert(false, "Undefined conent target.");
            pdfFont = null;
            return null;
        }

        /// <summary>
        /// Tries to get the resource name of the specified font within this page or form.
        /// Returns null if no such font exists.
        /// </summary>
        internal string TryGetFontName(string idName, out PdfFont pdfFont)
        {
            if (page != null)
                return page.TryGetFontName(idName, out pdfFont);
            else if (form != null)
                return form.TryGetFontName(idName, out pdfFont);
            else if (contentDictionary != null)
            {
                throw new NotImplementedException("GetFontName");
            }

            Debug.Assert(false, "Undefined conent target.");
            pdfFont = null;
            return null;
        }

        /// <summary>
        /// Gets the resource name of the specified font within this page or form.
        /// </summary>
        internal string GetFontName(string idName, byte[] fontData, out PdfFont pdfFont)
        {
            if (page != null)
                return page.GetFontName(idName, fontData, out pdfFont);
            else if (form != null)
                return form.GetFontName(idName, fontData, out pdfFont);
            else if (contentDictionary != null)
            {
                throw new NotImplementedException("GetFontName");
            }

            Debug.Assert(false, "Undefined conent target.");
            pdfFont = null;
            return null;
        }

        /// <summary>
        /// Gets the resource name of the specified font within this page or form.
        /// </summary>
        internal string GetFontName(Font font)
        {
            PdfFont pdfFont;
            string name = null;
            if (page != null)
                name = page.GetFontName(font.Name, font.FontData, out pdfFont);
            else if (form != null)
                name = form.GetFontName(font.Name, font.FontData, out pdfFont);
            else if (contentDictionary != null)
            {
                Debug.Assert(contentStreamDictionary != null);
                name = contentStreamDictionary.GetFontName(font.Name, font.FontData, out pdfFont);
            }
            else
            {
                Debug.Assert(false, "Undefined conent target.");
                pdfFont = null;  // supress compiler warning
            }

            Debug.Assert(font.PdfFont == null || ReferenceEquals(font.PdfFont, pdfFont));
            font.PdfFont ??= pdfFont;
            return name;
        }

        //    /// <summary>
        //    /// Gets the resource name of the specified image within this page or form.
        //    /// </summary>
        //    internal string GetImageName(XImage image)
        //    {
        //      if (this.page != null)
        //        return this.page.GetImageName(image);
        //      else
        //        return this.form.GetImageName(image);
        //    }

        //    /// <summary>
        //    /// Gets the resource name of the specified form within this page or form.
        //    /// </summary>
        //    internal string GetFormName(XForm form)
        //    {
        //      if (this.page != null)
        //        return this.page.GetFormName(form);
        //      else
        //        return this.form.GetFormName(form);
        //    }

        //    internal PdfPage page;
        //    internal XForm form;
        //    internal PdfColorMode colorMode;
        //    XGraphicsPdfPageOptions options;



        ///// <summary>
        ///// Writes a comment to the content stream.
        ///// </summary>
        //public void WriteComment(string message)
        //{
        //  if (!String.IsNullOrEmpty(message) && this.traceLevel != PdfTraceLevel.None)
        //    Append("q % " + traceMessage + "\n");
        //}


        /// <summary>
        /// Saves the current graphical state and writes a push state operator to the content stream.
        /// </summary>
        public void WriteSaveState(string traceMessage, string elementName)
        {
            //Debug.Assert(this.streamMode == StreamMode.Graphic, "Cannot restore state in text mode.");

            if (traceMessage == null || traceLevel == PdfTraceLevel.None)
                WriteLiteral("q\n");
            else
            {
                if (!String.IsNullOrEmpty(elementName))
                    traceMessage = traceMessage + ": '" + elementName + "'";
                WriteLiteral("q % -- " + traceMessage + "\n");
            }
            graphicsStateStack.Push(graphicsState);
            graphicsState = graphicsState.Clone();
            graphicsState.Level = graphicsStateStack.Count;
        }

        /// <summary>
        /// Restores the previous graphical state and writes a pop state operator to the content stream.
        /// </summary>
        public void WriteRestoreState(string traceMessage, string elementName)
        {
            //Debug.Assert(this.streamMode == StreamMode.Graphic, "Cannot restore state in text mode.");
            BeginGraphic();
            graphicsState = (PdfGraphicsState)graphicsStateStack.Pop();

            if (traceMessage == null || traceLevel == PdfTraceLevel.None)
                WriteLiteral("Q\n");
            else
            {
                if (!String.IsNullOrEmpty(elementName))
                    traceMessage = traceMessage + ": '" + elementName + "'";
                WriteLiteral("Q % -- " + traceMessage + "\n");
            }
        }

        /// <summary>
        /// The current graphics state of the PDF content.
        /// </summary>
        private PdfGraphicsState graphicsState;

        /// <summary>
        /// The graphical state stack.
        /// </summary>
        private readonly Stack<PdfGraphicsState> graphicsStateStack = new();
    }
}
