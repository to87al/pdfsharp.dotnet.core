﻿using PdfSharp.Drawing;
using PdfSharp.Pdf.Advanced;
using PdfSharp.Pdf.Internal;
using PdfSharp.Xps.XpsModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using Color = PdfSharp.Xps.XpsModel.Color;

#pragma warning disable 414, 169 // incomplete code state

namespace PdfSharp.Xps.Rendering
{
    internal sealed partial class PdfGraphicsState : ICloneable
    {
        public PdfGraphicsState(PdfContentWriter writer)
        {
            this.writer = writer;
        }

        private readonly PdfContentWriter writer;

        internal XMatrix currentTransform = new();

        public PdfGraphicsState Clone()
        {
            PdfGraphicsState state = (PdfGraphicsState)MemberwiseClone();
            return state;
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        internal int Level;

        //internal InternalGraphicsState InternalState;

        //public void PushState()
        //{
        //  //BeginGraphic
        //  this.writer.Append("q/n");
        //}

        //public void PopState()
        //{
        //  //BeginGraphic
        //  this.writer.Append("Q/n");
        //}

        public XMatrix DefaultPageTransform
        {
            get { return defaultPageTransform; }
            set { defaultPageTransform = value; }
        }

        private XMatrix defaultPageTransform = new();

        public XMatrix Transform
        {
            get { return transform; }
        }

        private XMatrix transform = new();

        public XMatrix MultiplyTransform(XMatrix matrix)
        {
            transform.Prepend(matrix);
            return transform;
        }

        /// <summary>
        /// Gets the current opacity value.
        /// </summary>
        public double Opacity
        {
            get { return opacity; }
        }

        private double opacity = 1;

        /// <summary>
        /// Muliplies the spcified opacity with the current opacity and returns the new value;
        /// </summary>
        public double MuliplyOpacity(double opacity)
        {
            if (opacity is < 0 or > 1)
                throw new ArgumentException("opacity");

            this.opacity *= opacity;
            return this.opacity;
        }

        #region Stroke

        private double realizedLineWith = -1;
        private int realizedLineCap = -1;
        private int realizedLineJoin = -1;
        private double realizedMiterLimit = -1;
        private Color realizedStrokeColor = new();

        public void RealizeStroke(Path path) // XPen pen, PdfColorMode colorMode)
        {
            //XColor color = pen.Color;
            //color = ColorSpaceHelper.EnsureColorMode(colorMode, color);

            var brush = path.Stroke;
            if (brush is SolidColorBrush sBrush)
            {

                var thickness = path.StrokeThickness;

                // Adjust based on path geometry transform since XPS doesn't apply scale to stroke thickness
                if (path.Data.Transform != null && !path.Data.Transform.Value.IsIdentity)
                {
                    var actualThickness = thickness *
                      ((path.Data.Transform.Value.M11 + path.Data.Transform.Value.M12) + (path.Data.Transform.Value.M21 + path.Data.Transform.Value.M22)) / 2;
                    /*
                     * actual     requir
                     * ------  =  ------
                     * requir     necess
                     */
                    thickness = thickness * thickness / actualThickness;
                }

                //if (this.realizedLineWith != width)
                {
                    writer.WriteLiteral("{0:0.###} w\n", thickness);
                    realizedLineWith = thickness;
                }

                RealizeStrokeStyle(path);

                var color = sBrush.Color;

                double opacity = path.Opacity * color.ScA;
                if (opacity < 1)
                {
                    var extGState = writer.Owner.ExtGStateTable.GetExtGStateStroke(color.ScA);
                    string gs = writer.Resources.AddExtGState(extGState);
                    writer.WriteLiteral("{0} gs\n", gs);

                    // Must create transparany group
                    //if (color.ScA < 1)
                    writer.CreateDefaultTransparencyGroup();
                }
                writer.WriteRgb(color, " RG\n");

                realizedStrokeColor = color.ToXpsColor();
            }
            else
            {
                Debug.WriteLine("Stroke with non SolidColorBrush");
                //throw new NotImplementedException("Stroke with non SolidColorBrush");
            }




            //if (this.realizedLineCap != (int)pen.lineCap)
            //{
            //  this.renderer.AppendFormat("{0} J\n", (int)pen.lineCap);
            //  this.realizedLineCap = (int)pen.lineCap;
            //}

            //if (this.realizedLineJoin != (int)pen.lineJoin)
            //{
            //  this.renderer.AppendFormat("{0} j\n", (int)pen.lineJoin);
            //  this.realizedLineJoin = (int)pen.lineJoin;
            //}

            //if (this.realizedLineCap == (int)XLineJoin.Miter)
            //{
            //  if (this.realizedMiterLimit != (int)pen.miterLimit && (int)pen.miterLimit != 0)
            //  {
            //    this.renderer.AppendFormat("{0} M\n", (int)pen.miterLimit);
            //    this.realizedMiterLimit = (int)pen.miterLimit;
            //  }
            //}

            //if (this.realizedDashStyle != pen.dashStyle || pen.dashStyle == XDashStyle.Custom)
            //{
            //  double dot = pen.Width;
            //  double dash = 3 * dot;

            //  // Line width 0 is not recommended but valid
            //  XDashStyle dashStyle = pen.DashStyle;
            //  if (dot == 0)
            //    dashStyle = XDashStyle.Solid;

            //  switch (dashStyle)
            //  {
            //    case XDashStyle.Solid:
            //      this.renderer.Append("[]0 d\n");
            //      break;

            //    case XDashStyle.Dash:
            //      this.renderer.AppendFormat("[{0:0.##} {1:0.##}]0 d\n", dash, dot);
            //      break;

            //    case XDashStyle.Dot:
            //      this.renderer.AppendFormat("[{0:0.##}]0 d\n", dot);
            //      break;

            //    case XDashStyle.DashDot:
            //      this.renderer.AppendFormat("[{0:0.##} {1:0.##} {1:0.##} {1:0.##}]0 d\n", dash, dot);
            //      break;

            //    case XDashStyle.DashDotDot:
            //      this.renderer.AppendFormat("[{0:0.##} {1:0.##} {1:0.##} {1:0.##} {1:0.##} {1:0.##}]0 d\n", dash, dot);
            //      break;

            //    case XDashStyle.Custom:
            //      {
            //        StringBuilder pdf = new StringBuilder("[", 256);
            //        int len = pen.dashPattern == null ? 0 : pen.dashPattern.Length;
            //        for (int idx = 0; idx < len; idx++)
            //        {
            //          if (idx > 0)
            //            pdf.Append(' ');
            //          pdf.Append(PdfEncoders.ToString(pen.dashPattern[idx] * pen.width));
            //        }
            //        // Make an even number of values look like in GDI+
            //        if (len > 0 && len % 2 == 1)
            //        {
            //          pdf.Append(' ');
            //          pdf.Append(PdfEncoders.ToString(0.2 * pen.width));
            //        }
            //        pdf.AppendFormat(CultureInfo.InvariantCulture, "]{0:0.###} d\n", pen.dashOffset * pen.width);
            //        string pattern = pdf.ToString();

            //        // BUG: drice2@ageone.de reported a realizing problem
            //        // HACK: I romove the if clause
            //        //if (this.realizedDashPattern != pattern)
            //        {
            //          this.realizedDashPattern = pattern;
            //          this.renderer.Append(pattern);
            //        }
            //      }
            //      break;
            //  }
            //  this.realizedDashStyle = dashStyle;
            //}

            //if (colorMode != PdfColorMode.Cmyk)
            //{
            //if (this.realizedStrokeColor.Rgb != color.Rgb)
            //{
            //  this.renderer.Append(PdfEncoders.ToString(color, PdfColorMode.Rgb));
            //  this.renderer.Append(" RG\n");
            //}
            //}
            //else
            //{
            //  if (!ColorSpaceHelper.IsEqualCmyk(this.realizedStrokeColor, color))
            //  {
            //    this.renderer.Append(PdfEncoders.ToString(color, PdfColorMode.Cmyk));
            //    this.renderer.Append(" K\n");
            //  }
            //}

            //if (this.renderer.Owner.Version >= 14 && this.realizedStrokeColor.A != color.A)
            //{
            //  PdfExtGState extGState = this.renderer.Owner.ExtGStateTable.GetExtGStateStroke(color.A);
            //  string gs = this.renderer.Resources.AddExtGState(extGState);
            //  this.renderer.AppendFormat("{0} gs\n", gs);

            //  // Must create transparany group
            //  if (this.renderer.page != null && color.A < 1)
            //    this.renderer.page.transparencyUsed = true;
            //}
            //this.realizedStrokeColor = color;
        }

        private void RealizeStrokeStyle(Path path)
        {
            if (realizedLineCap != (int)path.StrokeStartLineCap)
            {
                // HACK: Set Triangle to Square
                int[] pdfValue = [0, 1, 2, 2];  // Flat, Round, Square, Triangle,
                                                  //int[] pdfValue = { 1, 1, 1, 1 };  // Flat, Round, Square, Triangle,

                int value = pdfValue[(int)path.StrokeStartLineCap];
                writer.WriteLiteral("{0} J\n", value);
                realizedLineCap = value;
            }

            if (realizedLineJoin != (int)path.StrokeLineJoin)
            {
                int[] pdfValue = [0, 2, 1];  // Miter, Bevel, Round

                int value = pdfValue[(int)path.StrokeLineJoin];
                writer.WriteLiteral("{0} j\n", value);
                realizedLineJoin = value;

                // TODO: Check implementation in PDFsharp PDF renderer!
                if (path.StrokeLineJoin == PenLineJoin.Miter)
                {
                    if (!(realizedMiterLimit.Equals(path.StrokeMiterLimit))
                      && !(path.StrokeMiterLimit.Equals(0)))
                    {
                        writer.WriteLiteral("{0:0.##} M\n", path.StrokeMiterLimit);
                        realizedMiterLimit = path.StrokeMiterLimit;
                    }
                }
            }

            if (path.StrokeDashArray.Any())
            {
                int count = path.StrokeDashArray.Count;

                // Line width 0 is not recommended but valid
                //XDashStyle dashStyle = pen.DashStyle;
                //if (dot == 0)
                //  dashStyle = XDashStyle.Solid;

                double dot = path.StrokeThickness;
                double dash = 3 * path.StrokeThickness;
                if (dot > 0)
                {
#if old
          //switch (dashStyle)
          //{
          //  case XDashStyle.Solid:
          //    this.renderer.Append("[]0 d\n");
          //    break;

          //  case XDashStyle.Dash:
          //    this.renderer.AppendFormat("[{0:0.##} {1:0.##}]0 d\n", dash, dot);
          //    break;

          //  case XDashStyle.Dot:
          //    this.renderer.AppendFormat("[{0:0.##}]0 d\n", dot);
          //    break;

          //  case XDashStyle.DashDot:
          //    this.renderer.AppendFormat("[{0:0.##} {1:0.##} {1:0.##} {1:0.##}]0 d\n", dash, dot);
          //    break;

          //  case XDashStyle.DashDotDot:
          //    this.renderer.AppendFormat("[{0:0.##} {1:0.##} {1:0.##} {1:0.##} {1:0.##} {1:0.##}]0 d\n", dash, dot);
          //    break;

          //  case XDashStyle.Custom:
          //    {
          //      StringBuilder pdf = new StringBuilder("[", 256);
          //      int len = pen.dashPattern == null ? 0 : pen.dashPattern.Length;
          //      for (int idx = 0; idx < len; idx++)
          //      {
          //        if (idx > 0)
          //          pdf.Append(' ');
          //        pdf.Append(PdfEncoders.ToString(pen.dashPattern[idx] * pen.width));
          //      }
          //      // Make an even number of values look like in GDI+
          //      if (len > 0 && len % 2 == 1)
          //      {
          //        pdf.Append(' ');
          //        pdf.Append(PdfEncoders.ToString(0.2 * pen.width));
          //      }
          //      pdf.AppendFormat(CultureInfo.InvariantCulture, "]{0:0.###} d\n", pen.dashOffset * pen.width);
          //      string pattern = pdf.ToString();

          //      // BUG: drice2@ageone.de reported a realizing problem
          //      // HACK: I romove the if clause
          //      //if (this.realizedDashPattern != pattern)
          //      {
          //        this.realizedDashPattern = pattern;
          //        this.renderer.Append(pattern);
          //      }
          //    }
          //    break;
          //}
          //this.realizedDashStyle = dashStyle;
#endif
                    StringBuilder pdf = new("[", 256);
                    for (int idx = 0; idx < count; idx++)
                    {
                        if (idx > 0)
                            pdf.Append(' ');
                        pdf.Append(PdfEncoders.ToString(path.StrokeDashArray[idx] * path.StrokeThickness));
                    }

                    // TODO: ??
                    // Make an even number of values look like in GDI+  
                    if (count > 0 && count % 2 == 1)
                    {
                        pdf.Append(' ');
                        pdf.Append(PdfEncoders.ToString(0.2 * path.StrokeThickness));
                    }
                    pdf.AppendFormat(CultureInfo.InvariantCulture, "]{0:0.###} d\n", path.StrokeDashOffset * path.StrokeThickness);
                    string pattern = pdf.ToString();

                    //// BUG: drice2@ageone.de reported a realizing problem
                    //// HACK: I romove the if clause
                    ////if (this.realizedDashPattern != pattern)
                    //{
                    //  this.realizedDashPattern = pattern;
                    writer.WriteLiteral(pattern);
                    //}
                }
            }
        }

        /// <summary>
        /// Realizes the opacity for stroke operations.
        /// </summary>
        public void RealizeStrokeOpacity(double opacity)
        {
            PdfExtGState extGState = writer.Owner.ExtGStateTable.GetExtGStateStroke(opacity);
            string gsName = writer.Resources.AddExtGState(extGState);
            writer.WriteLiteral(gsName + " gs\n");
        }

        /// <summary>
        /// Realizes the specified graphic state.
        /// </summary>
        public void RealizeExtGState(PdfExtGState xgState)
        {
            string gsName = writer.Resources.AddExtGState(xgState);
            writer.WriteLiteral(gsName + " gs\n");
        }

        #endregion

        #region Fill

        private Color realizedFillColor = new();

        public void RealizeFill(Brush brush, double opacity, ref XForm xform, ref XImage ximage) // PdfColorMode colorMode)
        {
            if (brush is SolidColorBrush sbrush)
            {
                Color color = sbrush.Color.ToXpsColor();
                //color = ColorSpaceHelper.EnsureColorMode(colorMode, color);

                writer.WriteRgb(color, " rg\n");

                //if (this.renderer.Owner.Version >= 14 && this.realizedStrokeColor.A != color.A)
                //if (this.realizedFillColor.ScA != color.ScA)
                {
                    PdfExtGState extGState = writer.Owner.ExtGStateTable.GetExtGStateNonStroke(color.ScA);
                    string gs = writer.Resources.AddExtGState(extGState);
                    writer.WriteLiteral("{0} gs\n", gs);

                    // Must create transparany group
                    if (color.ScA < 1)
                        writer.CreateDefaultTransparencyGroup();
                }
                realizedFillColor = color;

                //if (colorMode != PdfColorMode.Cmyk)
                //{
                //  if (this.realizedFillColor.Rgb != color.Rgb)
                //  {
                //    this.renderer.Append(PdfEncoders.ToString(color, PdfColorMode.Rgb));
                //    this.renderer.Append(" rg\n");
                //  }
                //}
                //else
                //{
                //  if (!ColorSpaceHelper.IsEqualCmyk(this.realizedFillColor, color))
                //  {
                //    this.renderer.Append(PdfEncoders.ToString(color, PdfColorMode.Cmyk));
                //    this.renderer.Append(" k\n");
                //  }
                //}

                //if (this.renderer.Owner.Version >= 14 && this.realizedFillColor.A != color.A)
                //{
                //  PdfExtGState extGState = this.renderer.Owner.ExtGStateTable.GetExtGStateNonStroke(color.A);
                //  string gs = this.renderer.Resources.AddExtGState(extGState);
                //  this.renderer.AppendFormat("{0} gs\n", gs);

                //  // Must create transparany group
                //  if (this.renderer.page != null && color.A < 1)
                //    this.renderer.page.transparencyUsed = true;
                //}
                //this.realizedFillColor = color;
            }
            else if (brush is LinearGradientBrush lbrush)
            {
                // NOT IN USE ANYMORE
                //RealizeLinearGradientBrush(lbrush, xform);
            }
            else if (brush is RadialGradientBrush rbrush)
            {
                // NOT IN USE ANYMORE
                //RealizeRadialGradientBrush(rbrush, xform);
            }
            else if (brush is ImageBrush ibrush)
            {
                // NOT IN USE ANYMORE
                //RealizeImageBrush(ibrush, ref xform, ref ximage);
            }
            else if (brush is VisualBrush vbrush)
            {
                // NOT IN USE ANYMORE
                //RealizeVisualBrush(vbrush, ref xform);
            }
            else
            {
                //return new SolidColorBrush(Colors//
                //Debugger.Break();
            }
        }

        /// <summary>
        /// Realizes the opacity for non-stroke operations.
        /// </summary>
        public void RealizeFillOpacity(double opacity)
        {
            PdfExtGState extGState = writer.Owner.ExtGStateTable.GetExtGStateNonStroke(opacity);
            string gsName = writer.Resources.AddExtGState(extGState);
            writer.WriteLiteral(gsName + " gs\n");
        }

        #endregion
        //    #region Text

        internal PdfFont realizedFont;
        //    string realizedFontName = String.Empty;
        //    double realizedFontSize = 0;

        /// <summary>
        /// AbsURI, nameInPdf, PdfFont'
        /// </summary>
        private readonly Dictionary<string, KeyValuePair<string, PdfFont>> _fonts =
          [];

        private static int fontCount = 0;

        public void RealizeFont(Glyphs glyphs)
        {
            var absURI = glyphs.FontUri;
            if (!glyphs.FontUri.IsAbsoluteUri)
            {
                System.Windows.Markup.IUriContext uriCtx = glyphs;

                var host = uriCtx.BaseUri.Host;
                absURI = new Uri("pack://" + host + glyphs.FontUri.OriginalString, UriKind.Absolute);
            }

            var nameInPdf = "";
            if (_fonts.TryGetValue(absURI.OriginalString, out KeyValuePair<string, PdfFont> exist))
            {
                realizedFont = exist.Value;
                nameInPdf = exist.Key;
            }
            else
            {
                GlyphTypeface typeFace =
                  new(absURI);

                string name = $"XPS-Font-{++fontCount:00}";
                name = PdfFont.CreateEmbeddedFontSubsetName(name);

                var fontStream = typeFace.GetFontStream();
                var memStream = new System.IO.MemoryStream();
                var buff = new byte[512];
                int len = 0;
                while ((len = fontStream.Read(buff, 0, buff.Length)) > 0)
                {
                    memStream.Write(buff, 0, len);
                }
                var fontData = memStream.ToArray();
                Font font = new(name, fontData);

                nameInPdf = writer.GetFontName(font);
                _fonts.Add(absURI.OriginalString, new KeyValuePair<string, PdfFont>(
                  nameInPdf, font.PdfFont));
                realizedFont = font.PdfFont;
            }
            //if (fontName != this.realizedFontName || this.realizedFontSize != font.Size)
            {
                //this.writer.WriteLiteral("{0} {1:0.###} Tf\n", fontName, -glyphs.FontRenderingEmSize);
                writer.WriteLiteral("{0} {1:0.###} Tf\n", nameInPdf, -glyphs.FontRenderingEmSize);

                //this.realizedFontName = fontName;
                //this.realizedFontSize = font.Size;
            }
        }

        public XPoint realizedTextPosition = new();

        //    #endregion

        //    #region Transformation

        //    /// <summary>
        //    /// The realized current transformation matrix.
        //    /// </summary>
        //    XMatrix realizedCtm = new XMatrix();  //XMatrix.Identity;

        //    /// <summary>
        //    /// The unrealized current transformation matrix.
        //    /// </summary>
        //    XMatrix unrealizedCtm = new XMatrix();  //XMatrix.Identity;

        //    /// <summary>
        //    /// A flag indicating whether the CTM must be realized.
        //    /// </summary>
        //    public bool MustRealizeCtm;

        //    public XMatrix Transform
        //    {
        //      get
        //      {
        //        if (this.MustRealizeCtm)
        //        {
        //          XMatrix matrix = this.realizedCtm;
        //          matrix.MultiplyPrepend(this.unrealizedCtm);
        //          return matrix;
        //        }
        //        return this.realizedCtm;
        //      }
        //      set
        //      {
        //        XMatrix matrix = this.realizedCtm;
        //        matrix.Invert();
        //        matrix.MultiplyPrepend(value);
        //        this.unrealizedCtm = matrix;
        //        this.MustRealizeCtm = !this.unrealizedCtm.IsIdentity;
        //      }
        //    }

        //    /// <summary>
        //    /// Modifies the current transformation matrix.
        //    /// </summary>
        //    public void MultiplyTransform(XMatrix matrix, XMatrixOrder order)
        //    {
        //      if (!matrix.IsIdentity)
        //      {
        //        this.MustRealizeCtm = true;
        //        this.unrealizedCtm.Multiply(matrix, order);
        //      }
        //    }

        //    /// <summary>
        //    /// Realizes the CTM.
        //    /// </summary>
        //    public void RealizeCtm()
        //    {
        //      if (this.MustRealizeCtm)
        //      {
        //        Debug.Assert(!this.unrealizedCtm.IsIdentity, "mrCtm is unnecessarily set.");

        //        double[] matrix = this.unrealizedCtm.Elements;
        //        // Up to six decimal digits to prevent round up problems
        //        this.renderer.AppendFormat("{0:0.######} {1:0.######} {2:0.######} {3:0.######} {4:0.######} {5:0.######} cm\n",
        //          matrix[0], matrix[1], matrix[2], matrix[3], matrix[4], matrix[5]);

        //        this.realizedCtm.MultiplyPrepend(this.unrealizedCtm);
        //        this.unrealizedCtm = new XMatrix();  //XMatrix.Identity;
        //        this.MustRealizeCtm = false;
        //      }
        //    }

        //    #endregion

        //    #region Clip Path

        //    public void SetAndRealizeClipRect(XRect clipRect)
        //    {
        //      XGraphicsPath clipPath = new XGraphicsPath();
        //      clipPath.AddRectangle(clipRect);
        //      RealizeClipPath(clipPath);
        //    }

        //    public void SetAndRealizeClipPath(XGraphicsPath clipPath)
        //    {
        //      RealizeClipPath(clipPath);
        //    }

        //    void RealizeClipPath(XGraphicsPath clipPath)
        //    {
        //      this.renderer.BeginGraphic();
        //      RealizeCtm();
        //#if GDI
        //      this.renderer.AppendPath(clipPath.gdipPath);
        //#endif
        //#if WPF
        //      this.renderer.AppendPath(clipPath.pathGeometry);
        //#endif
        //      if (clipPath.FillMode == XFillMode.Winding)
        //        this.renderer.Append("W n\n");
        //      else
        //        this.renderer.Append("W* n\n");
        //    }

        //    #endregion
    }
}