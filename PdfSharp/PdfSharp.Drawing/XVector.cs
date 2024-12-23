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
using System.Runtime.InteropServices;
#if GDI
using System.Drawing;
#endif
#if WPF
#endif

#pragma warning disable 1591

namespace PdfSharp.Drawing
{
    /// <summary>
    /// Represents a two-dimensional vector specified by x- and y-coordinates.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct XVector : IFormattable
    {
        public XVector(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public static bool operator ==(XVector vector1, XVector vector2)
        {
            return vector1.x == vector2.x && vector1.y == vector2.y;
        }

        public static bool operator !=(XVector vector1, XVector vector2)
        {
            return vector1.x != vector2.x || vector1.y != vector2.y;
        }

        public static bool Equals(XVector vector1, XVector vector2)
        {
            if (vector1.X.Equals(vector2.X))
                return vector1.Y.Equals(vector2.Y);
            return false;
        }

        public override readonly bool Equals(object o)
        {
            if (o is null or not XVector)
                return false;
            XVector vector = (XVector)o;
            return Equals(this, vector);
        }

        public readonly bool Equals(XVector value)
        {
            return Equals(this, value);
        }

        public override readonly int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode();
        }

        //public static XVector Parse(string source)
        //{
        //  IFormatProvider cultureInfo = CultureInfo.GetCultureInfo("en-us");
        //  TokenizerHelper helper = new TokenizerHelper(source, cultureInfo);
        //  string str = helper.NextTokenRequired();
        //  XVector vector = new XVector(Convert.ToDouble(str, cultureInfo), Convert.ToDouble(helper.NextTokenRequired(), cultureInfo));
        //  helper.LastTokenRequired();
        //  return vector;
        //}

        public double X
        {
            readonly get { return x; }
            set { x = value; }
        }

        public double Y
        {
            readonly get { return y; }
            set { y = value; }
        }

        public override string ToString()
        {
            return ConvertToString(null, null);
        }

        public string ToString(IFormatProvider provider)
        {
            return ConvertToString(null, provider);
        }

        string IFormattable.ToString(string format, IFormatProvider provider)
        {
            return ConvertToString(format, provider);
        }

        internal readonly string ConvertToString(string format, IFormatProvider provider)
        {
            const char numericListSeparator = ',';
            return string.Format(provider, "{1:" + format + "}{0}{2:" + format + "}", [numericListSeparator, x, y]);
        }

        public readonly double Length
        {
            get { return Math.Sqrt((x * x) + (y * y)); }
        }

        public readonly double LengthSquared
        {
            get { return (x * x) + (y * y); }
        }

        public void Normalize()
        {
            this /= Math.Max(Math.Abs(x), Math.Abs(y));
            this /= Length;
        }

        public static double CrossProduct(XVector vector1, XVector vector2)
        {
            return (vector1.x * vector2.y) - (vector1.y * vector2.x);
        }

        public static double AngleBetween(XVector vector1, XVector vector2)
        {
            double y = (vector1.x * vector2.y) - (vector2.x * vector1.y);
            double x = (vector1.x * vector2.x) + (vector1.y * vector2.y);
            return (Math.Atan2(y, x) * 57.295779513082323);
        }

        public static XVector operator -(XVector vector)
        {
            return new XVector(-vector.x, -vector.y);
        }

        public void Negate()
        {
            x = -x;
            y = -y;
        }

        public static XVector operator +(XVector vector1, XVector vector2)
        {
            return new XVector(vector1.x + vector2.x, vector1.y + vector2.y);
        }

        public static XVector Add(XVector vector1, XVector vector2)
        {
            return new XVector(vector1.x + vector2.x, vector1.y + vector2.y);
        }

        public static XVector operator -(XVector vector1, XVector vector2)
        {
            return new XVector(vector1.x - vector2.x, vector1.y - vector2.y);
        }

        public static XVector Subtract(XVector vector1, XVector vector2)
        {
            return new XVector(vector1.x - vector2.x, vector1.y - vector2.y);
        }

        public static XPoint operator +(XVector vector, XPoint point)
        {
            return new XPoint(point.x + vector.x, point.y + vector.y);
        }

        public static XPoint Add(XVector vector, XPoint point)
        {
            return new XPoint(point.x + vector.x, point.y + vector.y);
        }

        public static XVector operator *(XVector vector, double scalar)
        {
            return new XVector(vector.x * scalar, vector.y * scalar);
        }

        public static XVector Multiply(XVector vector, double scalar)
        {
            return new XVector(vector.x * scalar, vector.y * scalar);
        }

        public static XVector operator *(double scalar, XVector vector)
        {
            return new XVector(vector.x * scalar, vector.y * scalar);
        }

        public static XVector Multiply(double scalar, XVector vector)
        {
            return new XVector(vector.x * scalar, vector.y * scalar);
        }

        public static XVector operator /(XVector vector, double scalar)
        {
            return vector * (1.0 / scalar);
        }

        public static XVector Divide(XVector vector, double scalar)
        {
            return vector * (1.0 / scalar);
        }

        public static XVector operator *(XVector vector, XMatrix matrix)
        {
            return matrix.Transform(vector);
        }

        public static XVector Multiply(XVector vector, XMatrix matrix)
        {
            return matrix.Transform(vector);
        }

        public static double operator *(XVector vector1, XVector vector2)
        {
            return (vector1.x * vector2.x) + (vector1.y * vector2.y);
        }

        public static double Multiply(XVector vector1, XVector vector2)
        {
            return (vector1.x * vector2.x) + (vector1.y * vector2.y);
        }

        public static double Determinant(XVector vector1, XVector vector2)
        {
            return (vector1.x * vector2.y) - (vector1.y * vector2.x);
        }

        public static explicit operator XSize(XVector vector)
        {
            return new XSize(Math.Abs(vector.x), Math.Abs(vector.y));
        }

        public static explicit operator XPoint(XVector vector)
        {
            return new XPoint(vector.x, vector.y);
        }

        internal double x;
        internal double y;
    }
}
