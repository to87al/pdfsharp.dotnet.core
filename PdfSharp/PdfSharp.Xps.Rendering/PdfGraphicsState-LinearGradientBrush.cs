using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.Advanced;
using PdfSharp.Pdf.Internal;
using PdfSharp.Xps.XpsModel;
using System;
using System.Diagnostics;
using System.Text;
using System.Windows.Media;

#pragma warning disable 414, 169 // incomplete code state

namespace PdfSharp.Xps.Rendering
{
    partial class PdfGraphicsState
    {

        //void RealizeImageBrush(ImageBrush brush, XForm xform)
        //{
        //}

        //void RealizeVisualBrush(VisualBrush brush, XForm xform)
        //{
        //}

        /// <summary>
        /// Builds the shading function of the specified gradient stop collection.
        /// </summary>
        PdfDictionary BuildShadingFunction(GradientStopCollection gradients, PdfColorMode colorMode)
        {
            bool softMask = this.writer.renderMode == RenderMode.SoftMask;
            PdfDictionary func = new();
            int count = gradients.Count;
            Debug.Assert(count >= 2);
            if (count == 2)
            {
                // Build one Type 2 function
                func.Elements["/FunctionType"] = new PdfInteger(2); // Type 2 - Exponential Interpolation Function
                var clr0 = gradients[0].Color;
                var clr1 = gradients[1].Color;
                if (softMask)
                {
                    clr0 = clr0.AlphaToGray();
                    clr1 = clr1.AlphaToGray();
                }
                func.Elements["/C0"] = new PdfLiteral("[" + PdfEncoders.ToString(clr0.ToXColor(), colorMode) + "]");
                func.Elements["/C1"] = new PdfLiteral("[" + PdfEncoders.ToString(clr1.ToXColor(), colorMode) + "]");
                func.Elements["/Domain"] = new PdfLiteral("[0 1]");
                func.Elements["/N"] = new PdfInteger(1); // be linear
            }
            else
            {
                // Build a Type 3 function with an array of n-1 Type 2 functions
                func.Elements["/FunctionType"] = new PdfInteger(3); // Type 3 - Stitching Function
                func.Elements["/Domain"] = new PdfLiteral("[0 1]");
                PdfArray fnarray = new();
                func.Elements["/Functions"] = fnarray;

                StringBuilder bounds = new("[");
                StringBuilder encode = new("[");

                for (int idx = 1; idx < count; idx++)
                {
                    PdfDictionary fn2 = new();
                    fn2.Elements["/FunctionType"] = new PdfInteger(2);
                    var clr0 = gradients[idx - 1].Color;
                    var clr1 = gradients[idx].Color;
                    if (softMask)
                    {
                        clr0 = clr0.AlphaToGray();
                        clr1 = clr1.AlphaToGray();
                    }
                    fn2.Elements["/C0"] = new PdfLiteral("[" + PdfEncoders.ToString(clr0.ToXColor(), colorMode) + "]");
                    fn2.Elements["/C1"] = new PdfLiteral("[" + PdfEncoders.ToString(clr1.ToXColor(), colorMode) + "]");
                    fn2.Elements["/Domain"] = new PdfLiteral("[0 1]");
                    fn2.Elements["/N"] = new PdfInteger(1);
                    //this.renderer.Owner.Internals.AddObject(fn2);
                    //fnarray.Elements.Add(fn2.Reference);
                    fnarray.Elements.Add(fn2);
                    if (idx > 1)
                    {
                        bounds.Append(' ');
                        encode.Append(' ');
                    }
                    if (idx < count - 1)
                        bounds.Append(PdfEncoders.ToString(gradients[idx].Offset));
                    encode.Append("0 1");
                }
                bounds.Append(']');
                //encode.Append(" 0 1");
                encode.Append(']');
                func.Elements["/Bounds"] = new PdfLiteral(bounds.ToString());
                func.Elements["/Encode"] = new PdfLiteral(encode.ToString());
            }
            return func;
        }
    }
}