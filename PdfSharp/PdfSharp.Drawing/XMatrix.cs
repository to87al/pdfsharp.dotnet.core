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
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
#if GDI
using System.Drawing;
using System.Drawing.Drawing2D;
#endif
#if WPF
#endif
using PdfSharp.Internal;

// ReSharper disable RedundantNameQualifier
namespace PdfSharp.Drawing
{
    /// <summary>
    /// Represents a 3-by-3 matrix that represents an affine 2D transformation.
    /// </summary>
    [DebuggerDisplay("({M11}, {M12}, {M21}, {M22}, {OffsetX}, {OffsetY})")]
    [Serializable, StructLayout(LayoutKind.Sequential)] //, TypeConverter(typeof(MatrixConverter)), ValueSerializer(typeof(MatrixValueSerializer))]
    public struct XMatrix : IFormattable
    {
        [Flags]
        internal enum XMatrixTypes
        {
            Identity = 0,
            Translation = 1,
            Scaling = 2,
            Unknown = 4
        }

        /// <summary>
        /// Initializes a new instance of the XMatrix struct.
        /// </summary>
        public XMatrix(double m11, double m12, double m21, double m22, double offsetX, double offsetY)
        {
            this.m11 = m11;
            this.m12 = m12;
            this.m21 = m21;
            this.m22 = m22;
            this.offsetX = offsetX;
            this.offsetY = offsetY;
            type = XMatrixTypes.Unknown;
            padding = 0;
            DeriveMatrixType();
        }

        /// <summary>
        /// Gets a value that indicates whether this matrix is an identity matrix. 
        /// </summary>
        public static XMatrix Identity
        {
            get { return s_identity; }
        }

        /// <summary>
        /// Sets this matrix into an identity matrix.
        /// </summary>
        public void SetIdentity()
        {
            type = XMatrixTypes.Identity;
        }

        /// <summary>
        /// Fixes a bug that XMatrixTypes.Identity is not handled correctly in some cases.
        /// </summary>
        // HACK: Fixes a bug that XMatrixTypes.Identity is not handled correctly in some cases.
        // TODO: Eliminate this function.
        private void InitIdentity()
        {
            Debug.Assert(type == XMatrixTypes.Identity);
            m11 = 1;
            m22 = 1;
            Debug.Assert(m12 == 0);
            Debug.Assert(m21 == 0);
            Debug.Assert(offsetX == 0);
            Debug.Assert(offsetY == 0);
        }

        /// <summary>
        /// Gets a value indicating whether this matrix instance is the identity matrix.
        /// </summary>
        public bool IsIdentity
        {
            get
            {
                if (type == XMatrixTypes.Identity)
                    return true;
                if (m11 == 1.0 && m12 == 0 && m21 == 0 && m22 == 1.0 && offsetX == 0 && offsetY == 0)
                {
                    type = XMatrixTypes.Identity;
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Gets an array of double values that represents the elements of this matrix.
        /// </summary>
        [Obsolete("Use GetElements().")]
        public double[] Elements
        {
            get { return GetElements(); }
        }

        /// <summary>
        /// Gets an array of double values that represents the elements of this matrix.
        /// </summary>
        public readonly double[] GetElements()
        {
            if (type == XMatrixTypes.Identity)
                return [1, 0, 0, 1, 0, 0];
            return [m11, m12, m21, m22, offsetX, offsetY];
        }

        /// <summary>
        /// Multiplies two matrices.
        /// </summary>
        public static XMatrix operator *(XMatrix trans1, XMatrix trans2)
        {
            MatrixHelper.MultiplyMatrix(ref trans1, ref trans2);
            return trans1;
        }

        /// <summary>
        /// Multiplies two matrices.
        /// </summary>
        public static XMatrix Multiply(XMatrix trans1, XMatrix trans2)
        {
            MatrixHelper.MultiplyMatrix(ref trans1, ref trans2);
            return trans1;
        }

        /// <summary>
        /// Appends the specified matrix to this matrix. 
        /// </summary>
        public void Append(XMatrix matrix)
        {
            this *= matrix;
        }

        /// <summary>
        /// Prepends the specified matrix to this matrix. 
        /// </summary>
        public void Prepend(XMatrix matrix)
        {
            this = matrix * this;
        }

        /// <summary>
        /// Appends the specified matrix to this matrix. 
        /// </summary>
        [Obsolete("Use Append.")]
        public void Multiply(XMatrix matrix)
        {
            Append(matrix);
        }

        /// <summary>
        /// Prepends the specified matrix to this matrix. 
        /// </summary>
        [Obsolete("Use Prepend.")]
        public void MultiplyPrepend(XMatrix matrix)
        {
            Prepend(matrix);
        }

        /// <summary>
        /// Multiplies this matrix with the specified matrix.
        /// </summary>
        public void Multiply(XMatrix matrix, XMatrixOrder order)
        {
            // HACK in Multiply
            if (type == XMatrixTypes.Identity)
                InitIdentity();

            // Must use properties, the fields can be invalid if the matrix is identity matrix.
            double t11 = M11;
            double t12 = M12;
            double t21 = M21;
            double t22 = M22;
            double tdx = OffsetX;
            double tdy = OffsetY;

            if (order == XMatrixOrder.Append)
            {
                m11 = (t11 * matrix.M11) + (t12 * matrix.M21);
                m12 = (t11 * matrix.M12) + (t12 * matrix.M22);
                m21 = (t21 * matrix.M11) + (t22 * matrix.M21);
                m22 = (t21 * matrix.M12) + (t22 * matrix.M22);
                offsetX = (tdx * matrix.M11) + (tdy * matrix.M21) + matrix.OffsetX;
                offsetY = (tdx * matrix.M12) + (tdy * matrix.M22) + matrix.OffsetY;
            }
            else
            {
                m11 = (t11 * matrix.M11) + (t21 * matrix.M12);
                m12 = (t12 * matrix.M11) + (t22 * matrix.M12);
                m21 = (t11 * matrix.M21) + (t21 * matrix.M22);
                m22 = (t12 * matrix.M21) + (t22 * matrix.M22);
                offsetX = (t11 * matrix.OffsetX) + (t21 * matrix.OffsetY) + tdx;
                offsetY = (t12 * matrix.OffsetX) + (t22 * matrix.OffsetY) + tdy;
            }
            DeriveMatrixType();
        }

        /// <summary>
        /// Appends a translation of the specified offsets to this matrix.
        /// </summary>
        [Obsolete("Use TranslateAppend or TranslatePrepend explicitly, because in GDI+ and WPF the defaults are contrary.", true)]
        public void Translate(double offsetX, double offsetY)
        {
            throw new InvalidOperationException("Temporarily out of order.");
            //if (this.type == XMatrixTypes.Identity)
            //{
            //  this.SetMatrix(1.0, 0, 0, 1.0, offsetX, offsetY, XMatrixTypes.Translation);
            //}
            //else if (this.type == XMatrixTypes.Unknown)
            //{
            //  this.offsetX += offsetX;
            //  this.offsetY += offsetY;
            //}
            //else
            //{
            //  this.offsetX += offsetX;
            //  this.offsetY += offsetY;
            //  this.type |= XMatrixTypes.Translation;
            //}
        }

        /// <summary>
        /// Appends a translation of the specified offsets to this matrix.
        /// </summary>
        public void TranslateAppend(double offsetX, double offsetY) // TODO: will become default
        {
            if (type == XMatrixTypes.Identity)
            {
                SetMatrix(1, 0, 0, 1, offsetX, offsetY, XMatrixTypes.Translation);
            }
            else if (type == XMatrixTypes.Unknown)
            {
                this.offsetX += offsetX;
                this.offsetY += offsetY;
            }
            else
            {
                this.offsetX += offsetX;
                this.offsetY += offsetY;
                type |= XMatrixTypes.Translation;
            }
        }

        /// <summary>
        /// Prepends a translation of the specified offsets to this matrix.
        /// </summary>
        public void TranslatePrepend(double offsetX, double offsetY)
        {
            this = CreateTranslation(offsetX, offsetY) * this;
        }

        /// <summary>
        /// Translates the matrix with the specified offsets.
        /// </summary>
        public void Translate(double offsetX, double offsetY, XMatrixOrder order)
        {
            // HACK in Translate
            if (type == XMatrixTypes.Identity)
                InitIdentity();

            if (order == XMatrixOrder.Append)
            {
                this.offsetX += offsetX;
                this.offsetY += offsetY;
            }
            else
            {
                this.offsetX += (offsetX * m11) + (offsetY * m21);
                this.offsetY += (offsetX * m12) + (offsetY * m22);
            }
            DeriveMatrixType();
        }

        /// <summary>
        /// Appends the specified scale vector to this matrix.
        /// </summary>
        [Obsolete("Use ScaleAppend or ScalePrepend explicitly, because in GDI+ and WPF the defaults are contrary.", true)]
        public void Scale(double scaleX, double scaleY)
        {
            throw new InvalidOperationException("Temporarily out of order.");
            //this *= CreateScaling(scaleX, scaleY);
        }

        /// <summary>
        /// Appends the specified scale vector to this matrix.
        /// </summary>
        public void ScaleAppend(double scaleX, double scaleY)  // TODO: will become default
        {
            this *= CreateScaling(scaleX, scaleY);
        }

        /// <summary>
        /// Prepends the specified scale vector to this matrix.
        /// </summary>
        public void ScalePrepend(double scaleX, double scaleY)
        {
            this = CreateScaling(scaleX, scaleY) * this;
        }

        /// <summary>
        /// Scales the matrix with the specified scalars.
        /// </summary>
        public void Scale(double scaleX, double scaleY, XMatrixOrder order)
        {
            // HACK in Scale
            if (type == XMatrixTypes.Identity)
                InitIdentity();

            if (order == XMatrixOrder.Append)
            {
                m11 *= scaleX;
                m12 *= scaleY;
                m21 *= scaleX;
                m22 *= scaleY;
                offsetX *= scaleX;
                offsetY *= scaleY;
            }
            else
            {
                m11 *= scaleX;
                m12 *= scaleX;
                m21 *= scaleY;
                m22 *= scaleY;
            }
            DeriveMatrixType();
        }

        /// <summary>
        /// Scales the matrix with the specified scalar.
        /// </summary>
        [Obsolete("Use ScaleAppend or ScalePrepend explicitly, because in GDI+ and WPF the defaults are contrary.", true)]
        public void Scale(double scaleXY)
        {
            throw new InvalidOperationException("Temporarily out of order.");
            //Scale(scaleXY, scaleXY, XMatrixOrder.Prepend);
        }

        /// <summary>
        /// Appends the specified scale vector to this matrix.
        /// </summary>
        public void ScaleAppend(double scaleXY)
        {
            Scale(scaleXY, scaleXY, XMatrixOrder.Append);
        }

        /// <summary>
        /// Prepends the specified scale vector to this matrix.
        /// </summary>
        public void ScalePrepend(double scaleXY)
        {
            Scale(scaleXY, scaleXY, XMatrixOrder.Prepend);
        }

        /// <summary>
        /// Scales the matrix with the specified scalar.
        /// </summary>
        public void Scale(double scaleXY, XMatrixOrder order)
        {
            Scale(scaleXY, scaleXY, order);
        }

        /// <summary>
        /// Function is obsolete.
        /// </summary>
        [Obsolete("Use ScaleAtAppend or ScaleAtPrepend explicitly, because in GDI+ and WPF the defaults are contrary.", true)]
        public void ScaleAt(double scaleX, double scaleY, double centerX, double centerY)
        {
            throw new InvalidOperationException("Temporarily out of order.");
            //this *= CreateScaling(scaleX, scaleY, centerX, centerY);
        }

        /// <summary>
        /// Apppends the specified scale about the specified point of this matrix.
        /// </summary>
        public void ScaleAtAppend(double scaleX, double scaleY, double centerX, double centerY) // TODO: will become default
        {
            this *= CreateScaling(scaleX, scaleY, centerX, centerY);
        }

        /// <summary>
        /// Prepends the specified scale about the specified point of this matrix.
        /// </summary>
        public void ScaleAtPrepend(double scaleX, double scaleY, double centerX, double centerY)
        {
            this = CreateScaling(scaleX, scaleY, centerX, centerY) * this;
        }

        /// <summary>
        /// Function is obsolete.
        /// </summary>
        [Obsolete("Use RotateAppend or RotatePrepend explicitly, because in GDI+ and WPF the defaults are contrary.", true)]
        public void Rotate(double angle)
        {
            throw new InvalidOperationException("Temporarily out of order.");
            //angle = angle % 360.0;
            //this *= CreateRotationRadians(angle * 0.017453292519943295);
        }

        /// <summary>
        /// Appends a rotation of the specified angle to this matrix.
        /// </summary>
        public void RotateAppend(double angle) // TODO: will become default Rotate
        {
            angle %= 360.0;
            this *= CreateRotationRadians(angle * 0.017453292519943295);
        }

        /// <summary>
        /// Prepends a rotation of the specified angle to this matrix.
        /// </summary>
        public void RotatePrepend(double angle)
        {
            angle %= 360.0;
            this = CreateRotationRadians(angle * 0.017453292519943295) * this;
        }

        /// <summary>
        /// Rotates the matrix with the specified angle.
        /// </summary>
        public void Rotate(double angle, XMatrixOrder order)
        {
            // HACK in Rotate
            if (type == XMatrixTypes.Identity)
                InitIdentity();

            angle *= Calc.Deg2Rad;
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);
            if (order == XMatrixOrder.Append)
            {
                double t11 = m11;
                double t12 = m12;
                double t21 = m21;
                double t22 = m22;
                double tdx = offsetX;
                double tdy = offsetY;
                m11 = (t11 * cos) - (t12 * sin);
                m12 = (t11 * sin) + (t12 * cos);
                m21 = (t21 * cos) - (t22 * sin);
                m22 = (t21 * sin) + (t22 * cos);
                offsetX = (tdx * cos) - (tdy * sin);
                offsetY = (tdx * sin) + (tdy * cos);
            }
            else
            {
                double t11 = m11;
                double t12 = m12;
                double t21 = m21;
                double t22 = m22;
                m11 = (t11 * cos) + (t21 * sin);
                m12 = (t12 * cos) + (t22 * sin);
                m21 = (-t11 * sin) + (t21 * cos);
                m22 = (-t12 * sin) + (t22 * cos);
            }
            DeriveMatrixType();
        }

        /// <summary>
        /// Function is obsolete.
        /// </summary>
        [Obsolete("Use RotateAtAppend or RotateAtPrepend explicitly, because in GDI+ and WPF the defaults are contrary.", true)]
        public void RotateAt(double angle, double centerX, double centerY)
        {
            throw new InvalidOperationException("Temporarily out of order.");
            //angle = angle % 360.0;
            //this *= CreateRotationRadians(angle * 0.017453292519943295, centerX, centerY);
        }

        /// <summary>
        /// Appends a rotation of the specified angle at the specified point to this matrix.
        /// </summary>
        public void RotateAtAppend(double angle, double centerX, double centerY)  // TODO: will become default
        {
            angle %= 360.0;
            this *= CreateRotationRadians(angle * 0.017453292519943295, centerX, centerY);
        }

        /// <summary>
        /// Prepends a rotation of the specified angle at the specified point to this matrix.
        /// </summary>
        public void RotateAtPrepend(double angle, double centerX, double centerY)
        {
            angle %= 360.0;
            this = CreateRotationRadians(angle * 0.017453292519943295, centerX, centerY) * this;
        }

        /// <summary>
        /// Rotates the matrix with the specified angle at the specified point.
        /// </summary>
        [Obsolete("Use RotateAtAppend or RotateAtPrepend explicitly, because in GDI+ and WPF the defaults are contrary.", true)]
        public void RotateAt(double angle, XPoint point)
        {
            throw new InvalidOperationException("Temporarily out of order.");
            //RotateAt(angle, point, XMatrixOrder.Prepend);
        }

        /// <summary>
        /// Appends a rotation of the specified angle at the specified point to this matrix.
        /// </summary>
        public void RotateAtAppend(double angle, XPoint point)
        {
            RotateAt(angle, point, XMatrixOrder.Append);
        }

        /// <summary>
        /// Prepends a rotation of the specified angle at the specified point to this matrix.
        /// </summary>
        public void RotateAtPrepend(double angle, XPoint point)
        {
            RotateAt(angle, point, XMatrixOrder.Prepend);
        }

        /// <summary>
        /// Rotates the matrix with the specified angle at the specified point.
        /// </summary>
        public void RotateAt(double angle, XPoint point, XMatrixOrder order)
        {
            if (order == XMatrixOrder.Append)
            {
                angle %= 360.0;
                this *= CreateRotationRadians(angle * 0.017453292519943295, point.x, point.y);

                //this.Translate(point.X, point.Y, order);
                //this.Rotate(angle, order);
                //this.Translate(-point.X, -point.Y, order);
            }
            else
            {
                angle %= 360.0;
                this = CreateRotationRadians(angle * 0.017453292519943295, point.x, point.y) * this;
            }
            DeriveMatrixType();
        }

        /// <summary>
        /// Function is obsolete.
        /// </summary>
        [Obsolete("Use ShearAppend or ShearPrepend explicitly, because in GDI+ and WPF the defaults are contrary.", true)]
        public void Shear(double shearX, double shearY)
        {
            throw new InvalidOperationException("Temporarily out of order.");
            //Shear(shearX, shearY, XMatrixOrder.Prepend);
        }

        /// <summary>
        /// Appends a skew of the specified degrees in the x and y dimensions to this matrix.
        /// </summary>
        public void ShearAppend(double shearX, double shearY) // TODO: will become default
        {
            Shear(shearX, shearY, XMatrixOrder.Append);
        }

        /// <summary>
        /// Prepends a skew of the specified degrees in the x and y dimensions to this matrix.
        /// </summary>
        public void ShearPrepend(double shearX, double shearY)
        {
            Shear(shearX, shearY, XMatrixOrder.Prepend);
        }

        /// <summary>
        /// Shears the matrix with the specified scalars.
        /// </summary>
        public void Shear(double shearX, double shearY, XMatrixOrder order)
        {
            // HACK in Shear
            if (type == XMatrixTypes.Identity)
                InitIdentity();

            double t11 = m11;
            double t12 = m12;
            double t21 = m21;
            double t22 = m22;
            double tdx = offsetX;
            double tdy = offsetY;
            if (order == XMatrixOrder.Append)
            {
                m11 += shearX * t12;
                m12 += shearY * t11;
                m21 += shearX * t22;
                m22 += shearY * t21;
                offsetX += shearX * tdy;
                offsetY += shearY * tdx;
            }
            else
            {
                m11 += shearY * t21;
                m12 += shearY * t22;
                m21 += shearX * t11;
                m22 += shearX * t12;
            }
            DeriveMatrixType();
        }

        /// <summary>
        /// Function is obsolete.
        /// </summary>
        [Obsolete("Use SkewAppend or SkewPrepend explicitly, because in GDI+ and WPF the defaults are contrary.", true)]
        public void Skew(double skewX, double skewY)
        {
            throw new InvalidOperationException("Temporarily out of order.");
            //skewX = skewX % 360.0;
            //skewY = skewY % 360.0;
            //this *= CreateSkewRadians(skewX * 0.017453292519943295, skewY * 0.017453292519943295);
        }

        /// <summary>
        /// Appends a skew of the specified degrees in the x and y dimensions to this matrix.
        /// </summary>
        public void SkewAppend(double skewX, double skewY)
        {
            skewX %= 360.0;
            skewY %= 360.0;
            this *= CreateSkewRadians(skewX * 0.017453292519943295, skewY * 0.017453292519943295);
        }

        /// <summary>
        /// Prepends a skew of the specified degrees in the x and y dimensions to this matrix.
        /// </summary>
        public void SkewPrepend(double skewX, double skewY)
        {
            skewX %= 360.0;
            skewY %= 360.0;
            this = CreateSkewRadians(skewX * 0.017453292519943295, skewY * 0.017453292519943295) * this;
        }

        /// <summary>
        /// Transforms the specified point by this matrix and returns the result.
        /// </summary>
        public XPoint Transform(XPoint point)
        {
            XPoint point2 = point;
            MultiplyPoint(ref point2.x, ref point2.y);
            return point2;
        }

        /// <summary>
        /// Transforms the specified points by this matrix. 
        /// </summary>
        public void Transform(XPoint[] points)
        {
            if (points != null)
            {
                int count = points.Length;
                for (int idx = 0; idx < count; idx++)
                    MultiplyPoint(ref points[idx].x, ref points[idx].y);
            }
        }

        /// <summary>
        /// Multiplies all points of the specified array with the this matrix.
        /// </summary>
        public void TransformPoints(XPoint[] points)
        {
            ArgumentNullException.ThrowIfNull(points);

            if (IsIdentity)
                return;

            int count = points.Length;
            for (int idx = 0; idx < count; idx++)
            {
                double x = points[idx].X;
                double y = points[idx].Y;
                points[idx].X = (x * m11) + (y * m21) + offsetX;
                points[idx].Y = (x * m12) + (y * m22) + offsetY;
            }
        }

#if GDI
    /// <summary>
    /// Multiplies all points of the specified array with the this matrix.
    /// </summary>
    public void TransformPoints(System.Drawing.Point[] points)
    {
      if (points == null)
        throw new ArgumentNullException("points");

      if (IsIdentity)
        return;

      int count = points.Length;
      for (int idx = 0; idx < count; idx++)
      {
        double x = points[idx].X;
        double y = points[idx].Y;
        points[idx].X = (int)(x * this.m11 + y * this.m21 + this.offsetX);
        points[idx].Y = (int)(x * this.m12 + y * this.m22 + this.offsetY);
      }
    }
#endif

#if WPF
        /// <summary>
        /// Multiplies all points of the specified array with the this matrix.
        /// </summary>
        public void TransformPoints(System.Windows.Point[] points)
        {
            ArgumentNullException.ThrowIfNull(points);

            if (IsIdentity)
                return;

            int count = points.Length;
            for (int idx = 0; idx < count; idx++)
            {
                double x = points[idx].X;
                double y = points[idx].Y;
                points[idx].X = (int)((x * m11) + (y * m21) + offsetX);
                points[idx].Y = (int)((x * m12) + (y * m22) + offsetY);
            }
        }
#endif

        /// <summary>
        /// Transforms the specified vector by this Matrix and returns the result.
        /// </summary>
        public XVector Transform(XVector vector)
        {
            XVector vector2 = vector;
            MultiplyVector(ref vector2.x, ref vector2.y);
            return vector2;
        }

        /// <summary>
        /// Transforms the specified vectors by this matrix.
        /// </summary>
        public void Transform(XVector[] vectors)
        {
            if (vectors != null)
            {
                int count = vectors.Length;
                for (int idx = 0; idx < count; idx++)
                    MultiplyVector(ref vectors[idx].x, ref vectors[idx].y);
            }
        }

#if GDI
    /// <summary>
    /// Multiplies all vectors of the specified array with the this matrix. The translation elements 
    /// of this matrix (third row) are ignored.
    /// </summary>
    public void TransformVectors(PointF[] points)
    {
      if (points == null)
        throw new ArgumentNullException("points");

      if (IsIdentity)
        return;

      int count = points.Length;
      for (int idx = 0; idx < count; idx++)
      {
        double x = points[idx].X;
        double y = points[idx].Y;
        points[idx].X = (float)(x * this.m11 + y * this.m21 + this.offsetX);
        points[idx].Y = (float)(x * this.m12 + y * this.m22 + this.offsetY);
      }
    }
#endif

        /// <summary>
        /// Gets the determinant of this matrix.
        /// </summary>
        public readonly double Determinant
        {
            get
            {
                return type switch
                {
                    XMatrixTypes.Identity or XMatrixTypes.Translation => 1.0,
                    XMatrixTypes.Scaling or XMatrixTypes.Scaling | XMatrixTypes.Translation => m11 * m22,
                    _ => (m11 * m22) - (m12 * m21),
                };
            }
        }

        /// <summary>
        /// Gets a value that indicates whether this matrix is invertible.
        /// </summary>
        public bool HasInverse
        {
            get { return !DoubleUtil.IsZero(Determinant); }
        }

        /// <summary>
        /// Inverts the matrix.
        /// </summary>
        public void Invert()
        {
            double determinant = Determinant;
            if (DoubleUtil.IsZero(determinant))
                throw new InvalidOperationException("NotInvertible"); //SR.Get(SRID.Transform_NotInvertible, new object[0]));

            switch (type)
            {
                case XMatrixTypes.Identity:
                    break;

                case XMatrixTypes.Translation:
                    offsetX = -offsetX;
                    offsetY = -offsetY;
                    return;

                case XMatrixTypes.Scaling:
                    m11 = 1.0 / m11;
                    m22 = 1.0 / m22;
                    return;

                case XMatrixTypes.Scaling | XMatrixTypes.Translation:
                    m11 = 1.0 / m11;
                    m22 = 1.0 / m22;
                    offsetX = -offsetX * m11;
                    offsetY = -offsetY * m22;
                    return;

                default:
                    {
                        double detInvers = 1.0 / determinant;
                        SetMatrix(m22 * detInvers, -m12 * detInvers, -m21 * detInvers, m11 * detInvers, ((m21 * offsetY) - (offsetX * m22)) * detInvers, ((offsetX * m12) - (m11 * offsetY)) * detInvers, XMatrixTypes.Unknown);
                        break;
                    }
            }
        }

        /// <summary>
        /// Gets or sets the value of the first row and first column of this matrix.
        /// </summary>
        public double M11
        {
            readonly get
            {
                if (type == XMatrixTypes.Identity)
                    return 1.0;
                return m11;
            }
            set
            {
                if (type == XMatrixTypes.Identity)
                    SetMatrix(value, 0, 0, 1, 0, 0, XMatrixTypes.Scaling);
                else
                {
                    m11 = value;
                    if (type != XMatrixTypes.Unknown)
                        type |= XMatrixTypes.Scaling;
                }
            }
        }

        /// <summary>
        /// Gets or sets the value of the first row and second column of this matrix.
        /// </summary>
        public double M12
        {
            readonly get
            {
                if (type == XMatrixTypes.Identity)
                    return 0;
                return m12;
            }
            set
            {
                if (type == XMatrixTypes.Identity)
                    SetMatrix(1, value, 0, 1, 0, 0, XMatrixTypes.Unknown);
                else
                {
                    m12 = value;
                    type = XMatrixTypes.Unknown;
                }
            }
        }

        /// <summary>
        /// Gets or sets the value of the second row and first column of this matrix.
        /// </summary>
        public double M21
        {
            readonly get
            {
                if (type == XMatrixTypes.Identity)
                    return 0;
                return m21;
            }
            set
            {
                if (type == XMatrixTypes.Identity)
                    SetMatrix(1, 0, value, 1, 0, 0, XMatrixTypes.Unknown);
                else
                {
                    m21 = value;
                    type = XMatrixTypes.Unknown;
                }
            }
        }

        /// <summary>
        /// Gets or sets the value of the second row and second column of this matrix.
        /// </summary>
        public double M22
        {
            readonly get
            {
                if (type == XMatrixTypes.Identity)
                    return 1.0;
                return m22;
            }
            set
            {
                if (type == XMatrixTypes.Identity)
                    SetMatrix(1, 0, 0, value, 0, 0, XMatrixTypes.Scaling);
                else
                {
                    m22 = value;
                    if (type != XMatrixTypes.Unknown)
                    {
                        type |= XMatrixTypes.Scaling;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the value of the third row and first column of this matrix.
        /// </summary>
        public double OffsetX
        {
            readonly get
            {
                if (type == XMatrixTypes.Identity)
                    return 0;
                return offsetX;
            }
            set
            {
                if (type == XMatrixTypes.Identity)
                    SetMatrix(1, 0, 0, 1, value, 0, XMatrixTypes.Translation);
                else
                {
                    offsetX = value;
                    if (type != XMatrixTypes.Unknown)
                        type |= XMatrixTypes.Translation;
                }
            }
        }

        /// <summary>
        /// Gets or sets the value of the third row and second  column of this matrix.
        /// </summary>
        public double OffsetY
        {
            readonly get
            {
                if (type == XMatrixTypes.Identity)
                    return 0;
                return offsetY;
            }
            set
            {
                if (type == XMatrixTypes.Identity)
                    SetMatrix(1, 0, 0, 1, 0, value, XMatrixTypes.Translation);
                else
                {
                    offsetY = value;
                    if (type != XMatrixTypes.Unknown)
                        type |= XMatrixTypes.Translation;
                }
            }
        }

#if GDI
    /// <summary>
    /// Converts this matrix to a System.Drawing.Drawing2D.Matrix object.
    /// </summary>
    public System.Drawing.Drawing2D.Matrix ToGdiMatrix()
    {
      return new System.Drawing.Drawing2D.Matrix((float)this.m11, (float)this.m12, (float)this.m21, (float)this.m22,
        (float)this.offsetX, (float)this.offsetY);
    }

    /// <summary>
    /// Obsolete, will be deleted.
    /// </summary>
    [Obsolete("Use ToGdiMatrix.")]
    public System.Drawing.Drawing2D.Matrix ToGdipMatrix()
    {
      return new System.Drawing.Drawing2D.Matrix((float)this.m11, (float)this.m12, (float)this.m21, (float)this.m22,
        (float)this.offsetX, (float)this.offsetY);
    }
#endif

#if WPF
        /// <summary>
        /// Converts this matrix to a System.Drawing.Drawing2D.Matrix object.
        /// </summary>
        public readonly System.Windows.Media.Matrix ToWpfMatrix()
        {
            return new System.Windows.Media.Matrix(m11, m12, m21, m22, offsetX, offsetY);
        }
#endif

#if GDI
    /// <summary>
    /// Explicitly converts a XMatrix to a Matrix.
    /// </summary>
    public static explicit operator System.Drawing.Drawing2D.Matrix(XMatrix matrix)
    {
      if (matrix.IsIdentity)
        return new System.Drawing.Drawing2D.Matrix();

      return new System.Drawing.Drawing2D.Matrix(
        (float)matrix.m11, (float)matrix.m12,
        (float)matrix.m21, (float)matrix.m22,
        (float)matrix.offsetX, (float)matrix.offsetY);
    }
#endif

#if WPF
        /// <summary>
        /// Explicitly converts an XMatrix to a Matrix.
        /// </summary>
        public static explicit operator System.Windows.Media.Matrix(XMatrix matrix)
        {
            if (matrix.IsIdentity)
                return new System.Windows.Media.Matrix();

            return new System.Windows.Media.Matrix(
              matrix.m11, matrix.m12,
              matrix.m21, matrix.m22,
              matrix.offsetX, matrix.offsetY);
        }
#endif

#if GDI
    /// <summary>
    /// Implicitly converts a Matrix to an XMatrix.
    /// </summary>
    public static implicit operator XMatrix(System.Drawing.Drawing2D.Matrix matrix)
    {
      float[] elements = matrix.Elements;
      return new XMatrix(elements[0], elements[1], elements[2], elements[3], elements[4], elements[5]);
    }
#endif

#if WPF
        /// <summary>
        /// Implicitly converts a Matrix to an XMatrix.
        /// </summary>
        public static implicit operator XMatrix(System.Windows.Media.Matrix matrix)
        {
            return new XMatrix(matrix.M11, matrix.M12, matrix.M21, matrix.M22, matrix.OffsetX, matrix.OffsetY);
        }
#endif

        /// <summary>
        /// Determines whether the two matrices are equal.
        /// </summary>
        public static bool operator ==(XMatrix matrix1, XMatrix matrix2)
        {
            if (matrix1.IsDistinguishedIdentity || matrix2.IsDistinguishedIdentity)
                return (matrix1.IsIdentity == matrix2.IsIdentity);

            return matrix1.M11 == matrix2.M11 && matrix1.M12 == matrix2.M12 && matrix1.M21 == matrix2.M21 && matrix1.M22 == matrix2.M22 &&
              matrix1.OffsetX == matrix2.OffsetX && matrix1.OffsetY == matrix2.OffsetY;
        }

        /// <summary>
        /// Determines whether the two matrices are not equal.
        /// </summary>
        public static bool operator !=(XMatrix matrix1, XMatrix matrix2)
        {
            return !(matrix1 == matrix2);
        }

        /// <summary>
        /// Determines whether the two matrices are equal.
        /// </summary>
        public static bool Equals(XMatrix matrix1, XMatrix matrix2)
        {
            if (matrix1.IsDistinguishedIdentity || matrix2.IsDistinguishedIdentity)
                return matrix1.IsIdentity == matrix2.IsIdentity;

            return matrix1.M11.Equals(matrix2.M11) && matrix1.M12.Equals(matrix2.M12) &&
              matrix1.M21.Equals(matrix2.M21) && matrix1.M22.Equals(matrix2.M22) &&
              matrix1.OffsetX.Equals(matrix2.OffsetX) && matrix1.OffsetY.Equals(matrix2.OffsetY);
        }

        /// <summary>
        /// Determines whether this matrix is equal to the specified object.
        /// </summary>
        public override readonly bool Equals(object o)
        {
            if (o is null or not XMatrix)
                return false;
            XMatrix matrix = (XMatrix)o;
            return Equals(this, matrix);
        }

        /// <summary>
        /// Determines whether this matrix is equal to the specified matrix.
        /// </summary>
        public readonly bool Equals(XMatrix value)
        {
            return Equals(this, value);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        public override int GetHashCode()
        {
            if (IsDistinguishedIdentity)
                return 0;
            return M11.GetHashCode() ^ M12.GetHashCode() ^ M21.GetHashCode() ^ M22.GetHashCode() ^ OffsetX.GetHashCode() ^ OffsetY.GetHashCode();
        }

        /// <summary>
        /// Parses a matrix from a string.
        /// </summary>
        public static XMatrix Parse(string source)
        {
            XMatrix identity;
            IFormatProvider cultureInfo = CultureInfo.InvariantCulture; //.GetCultureInfo("en-us");
            TokenizerHelper helper = new(source, cultureInfo);
            string str = helper.NextTokenRequired();
            identity = str == "Identity" ? Identity :
              new XMatrix(Convert.ToDouble(str, cultureInfo), Convert.ToDouble(helper.NextTokenRequired(), cultureInfo), Convert.ToDouble(helper.NextTokenRequired(), cultureInfo), Convert.ToDouble(helper.NextTokenRequired(), cultureInfo), Convert.ToDouble(helper.NextTokenRequired(), cultureInfo), Convert.ToDouble(helper.NextTokenRequired(), cultureInfo));
            helper.LastTokenRequired();
            return identity;
        }

        /// <summary>
        /// Converts this XMatrix to a human readable string.
        /// </summary>
        public override string ToString()
        {
            return ConvertToString(null, null);
        }

        /// <summary>
        /// Converts this XMatrix to a human readable string.
        /// </summary>
        public string ToString(IFormatProvider provider)
        {
            return ConvertToString(null, provider);
        }

        /// <summary>
        /// Converts this XMatrix to a human readable string.
        /// </summary>
        string IFormattable.ToString(string format, IFormatProvider provider)
        {
            return ConvertToString(format, provider);
        }

        internal string ConvertToString(string format, IFormatProvider provider)
        {
            if (IsIdentity)
                return "Identity";

            char numericListSeparator = TokenizerHelper.GetNumericListSeparator(provider);
            return string.Format(provider, "{1:" + format + "}{0}{2:" + format + "}{0}{3:" + format + "}{0}{4:" + format + "}{0}{5:" + format + "}{0}{6:" + format + "}", [numericListSeparator, m11, m12, m21, m22, offsetX, offsetY]);
        }

        internal readonly void MultiplyVector(ref double x, ref double y)
        {
            switch (type)
            {
                case XMatrixTypes.Identity:
                case XMatrixTypes.Translation:
                    return;

                case XMatrixTypes.Scaling:
                case XMatrixTypes.Scaling | XMatrixTypes.Translation:
                    x *= m11;
                    y *= m22;
                    return;
            }
            double d1 = y * m21;
            double d2 = x * m12;
            x *= m11;
            x += d1;
            y *= m22;
            y += d2;
        }

        internal readonly void MultiplyPoint(ref double x, ref double y)
        {
            switch (type)
            {
                case XMatrixTypes.Identity:
                    return;

                case XMatrixTypes.Translation:
                    x += offsetX;
                    y += offsetY;
                    return;

                case XMatrixTypes.Scaling:
                    x *= m11;
                    y *= m22;
                    return;

                case (XMatrixTypes.Scaling | XMatrixTypes.Translation):
                    x *= m11;
                    x += offsetX;
                    y *= m22;
                    y += offsetY;
                    return;
            }
            double d1 = (y * m21) + offsetX;
            double d2 = (x * m12) + offsetY;
            x *= m11;
            x += d1;
            y *= m22;
            y += d2;
        }

        internal static XMatrix CreateRotationRadians(double angle)
        {
            return CreateRotationRadians(angle, 0, 0);
        }

        internal static XMatrix CreateRotationRadians(double angle, double centerX, double centerY)
        {
            XMatrix matrix = new();
            double sin = Math.Sin(angle);
            double cos = Math.Cos(angle);
            double offsetX = (centerX * (1.0 - cos)) + (centerY * sin);
            double offsetY = (centerY * (1.0 - cos)) - (centerX * sin);
            matrix.SetMatrix(cos, sin, -sin, cos, offsetX, offsetY, XMatrixTypes.Unknown);
            return matrix;
        }

        internal static XMatrix CreateScaling(double scaleX, double scaleY, double centerX, double centerY)
        {
            XMatrix matrix = new();
            matrix.SetMatrix(scaleX, 0, 0, scaleY, centerX - (scaleX * centerX), centerY - (scaleY * centerY), XMatrixTypes.Scaling | XMatrixTypes.Translation);
            return matrix;
        }

        internal static XMatrix CreateScaling(double scaleX, double scaleY)
        {
            XMatrix matrix = new();
            matrix.SetMatrix(scaleX, 0, 0, scaleY, 0, 0, XMatrixTypes.Scaling);
            return matrix;
        }

        internal static XMatrix CreateSkewRadians(double skewX, double skewY)
        {
            XMatrix matrix = new();
            matrix.SetMatrix(1, Math.Tan(skewY), Math.Tan(skewX), 1, 0, 0, XMatrixTypes.Unknown);
            return matrix;
        }

        internal static XMatrix CreateTranslation(double offsetX, double offsetY)
        {
            XMatrix matrix = new();
            matrix.SetMatrix(1, 0, 0, 1, offsetX, offsetY, XMatrixTypes.Translation);
            return matrix;
        }

        private static XMatrix CreateIdentity()
        {
            XMatrix matrix = new();
            matrix.SetMatrix(1, 0, 0, 1.0, 0, 0, XMatrixTypes.Identity);
            return matrix;
        }

        /// <summary>
        /// Sets the matrix.
        /// </summary>
        // ReSharper disable ParameterHidesMember
        private void SetMatrix(double m11, double m12, double m21, double m22, double offsetX, double offsetY, XMatrixTypes type)
        // ReSharper restore ParameterHidesMember
        {
            this.m11 = m11;
            this.m12 = m12;
            this.m21 = m21;
            this.m22 = m22;
            this.offsetX = offsetX;
            this.offsetY = offsetY;
            this.type = type;
        }

        private void DeriveMatrixType()
        {
            type = XMatrixTypes.Identity;
            if (m21 != 0 || m12 != 0)
            {
                type = XMatrixTypes.Unknown;
            }
            else
            {
                if (m11 != 1 || m22 != 1)
                    type = XMatrixTypes.Scaling;

                if (offsetX != 0 || offsetY != 0)
                    type |= XMatrixTypes.Translation;

                if ((type & (XMatrixTypes.Scaling | XMatrixTypes.Translation)) == XMatrixTypes.Identity)
                    type = XMatrixTypes.Identity;
            }
        }

        private readonly bool IsDistinguishedIdentity
        {
            get { return (type == XMatrixTypes.Identity); }
        }

        static XMatrix()
        {
            s_identity = CreateIdentity();
        }

        // Keep the fields private and force using the properties.
        // This prevents using m11 and m22 by mistake when the matrix is identity.
        private double m11;
        private double m12;
        private double m21;
        private double m22;
        private double offsetX;
        private double offsetY;
        private XMatrixTypes type;
        private readonly int padding;
        private static readonly XMatrix s_identity;

        /// <summary>
        /// Internal matrix helper.
        /// </summary>
        internal static class MatrixHelper
        {
            // Fast multiplication taking matrix type into account. Reflectored from WPF.
            internal static void MultiplyMatrix(ref XMatrix matrix1, ref XMatrix matrix2)
            {
                XMatrixTypes type1 = matrix1.type;
                XMatrixTypes type2 = matrix2.type;
                if (type2 != XMatrixTypes.Identity)
                {
                    if (type1 == XMatrixTypes.Identity)
                        matrix1 = matrix2;
                    else if (type2 == XMatrixTypes.Translation)
                    {
                        matrix1.offsetX += matrix2.offsetX;
                        matrix1.offsetY += matrix2.offsetY;
                        if (type1 != XMatrixTypes.Unknown)
                            matrix1.type |= XMatrixTypes.Translation;
                    }
                    else if (type1 == XMatrixTypes.Translation)
                    {
                        double num = matrix1.offsetX;
                        double num2 = matrix1.offsetY;
                        matrix1 = matrix2;
                        matrix1.offsetX = (num * matrix2.m11) + (num2 * matrix2.m21) + matrix2.offsetX;
                        matrix1.offsetY = (num * matrix2.m12) + (num2 * matrix2.m22) + matrix2.offsetY;
                        if (type2 == XMatrixTypes.Unknown)
                            matrix1.type = XMatrixTypes.Unknown;
                        else
                            matrix1.type = XMatrixTypes.Scaling | XMatrixTypes.Translation;
                    }
                    else
                    {
                        switch ((((int)type1) << 4) | (int)type2)
                        {
                            case 0x22:
                                matrix1.m11 *= matrix2.m11;
                                matrix1.m22 *= matrix2.m22;
                                return;

                            case 0x23:
                                matrix1.m11 *= matrix2.m11;
                                matrix1.m22 *= matrix2.m22;
                                matrix1.offsetX = matrix2.offsetX;
                                matrix1.offsetY = matrix2.offsetY;
                                matrix1.type = XMatrixTypes.Scaling | XMatrixTypes.Translation;
                                return;

                            case 0x24:
                            case 0x34:
                            case 0x42:
                            case 0x43:
                            case 0x44:
                                matrix1 = new XMatrix(
                                  (matrix1.m11 * matrix2.m11) + (matrix1.m12 * matrix2.m21),
                                  (matrix1.m11 * matrix2.m12) + (matrix1.m12 * matrix2.m22),
                                  (matrix1.m21 * matrix2.m11) + (matrix1.m22 * matrix2.m21),
                                  (matrix1.m21 * matrix2.m12) + (matrix1.m22 * matrix2.m22),
                                  (matrix1.offsetX * matrix2.m11) + (matrix1.offsetY * matrix2.m21) + matrix2.offsetX,
                                  (matrix1.offsetX * matrix2.m12) + (matrix1.offsetY * matrix2.m22) + matrix2.offsetY);
                                return;

                            case 50:
                                matrix1.m11 *= matrix2.m11;
                                matrix1.m22 *= matrix2.m22;
                                matrix1.offsetX *= matrix2.m11;
                                matrix1.offsetY *= matrix2.m22;
                                return;

                            case 0x33:
                                matrix1.m11 *= matrix2.m11;
                                matrix1.m22 *= matrix2.m22;
                                matrix1.offsetX = (matrix2.m11 * matrix1.offsetX) + matrix2.offsetX;
                                matrix1.offsetY = (matrix2.m22 * matrix1.offsetY) + matrix2.offsetY;
                                return;
                        }
                    }
                }
            }

            internal static void PrependOffset(ref XMatrix matrix, double offsetX, double offsetY)
            {
                if (matrix.type == XMatrixTypes.Identity)
                {
                    matrix = new XMatrix(1, 0, 0, 1, offsetX, offsetY);
                    matrix.type = XMatrixTypes.Translation;
                }
                else
                {
                    matrix.offsetX += (matrix.m11 * offsetX) + (matrix.m21 * offsetY);
                    matrix.offsetY += (matrix.m12 * offsetX) + (matrix.m22 * offsetY);
                    if (matrix.type != XMatrixTypes.Unknown)
                        matrix.type |= XMatrixTypes.Translation;
                }
            }

            internal static void TransformRect(ref XRect rect, ref XMatrix matrix)
            {
                if (!rect.IsEmpty)
                {
                    XMatrixTypes types = matrix.type;
                    if (types != XMatrixTypes.Identity)
                    {
                        if ((types & XMatrixTypes.Scaling) != XMatrixTypes.Identity)
                        {
                            rect.x *= matrix.m11;
                            rect.y *= matrix.m22;
                            rect.width *= matrix.m11;
                            rect.height *= matrix.m22;
                            if (rect.width < 0)
                            {
                                rect.x += rect.width;
                                rect.width = -rect.width;
                            }
                            if (rect.height < 0)
                            {
                                rect.y += rect.height;
                                rect.height = -rect.height;
                            }
                        }
                        if ((types & XMatrixTypes.Translation) != XMatrixTypes.Identity)
                        {
                            rect.x += matrix.offsetX;
                            rect.y += matrix.offsetY;
                        }
                        if (types == XMatrixTypes.Unknown)
                        {
                            XPoint point = matrix.Transform(rect.TopLeft);
                            XPoint point2 = matrix.Transform(rect.TopRight);
                            XPoint point3 = matrix.Transform(rect.BottomRight);
                            XPoint point4 = matrix.Transform(rect.BottomLeft);
                            rect.x = Math.Min(Math.Min(point.X, point2.X), Math.Min(point3.X, point4.X));
                            rect.y = Math.Min(Math.Min(point.Y, point2.Y), Math.Min(point3.Y, point4.Y));
                            rect.width = Math.Max(Math.Max(point.X, point2.X), Math.Max(point3.X, point4.X)) - rect.x;
                            rect.height = Math.Max(Math.Max(point.Y, point2.Y), Math.Max(point3.Y, point4.Y)) - rect.y;
                        }
                    }
                }
            }
        }
    }
}
