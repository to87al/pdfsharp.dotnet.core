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
using System.Globalization;

namespace PdfSharp.Drawing
{
    /// <summary>
    /// Represents a value and its unit of measure. The structure converts implicitly from and to
    /// double with a value measured in point.
    /// </summary>
    public struct XUnit : IFormattable
    {
        internal const double PointFactor = 1;
        internal const double InchFactor = 72;
        internal const double MillimeterFactor = 72 / 25.4;
        internal const double CentimeterFactor = 72 / 2.54;
        internal const double PresentationFactor = 72 / 96.0;

        internal const double PointFactorWpf = 96 / 72.0;
        internal const double InchFactorWpf = 96;
        internal const double MillimeterFactorWpf = 96 / 25.4;
        internal const double CentimeterFactorWpf = 96 / 2.54;
        internal const double PresentationFactorWpf = 1;

        /// <summary>
        /// Initializes a new instance of the XUnit class with type set to point.
        /// </summary>
        public XUnit(double point)
        {
            value = point;
            type = XGraphicsUnit.Point;
        }

        /// <summary>
        /// Initializes a new instance of the XUnit class.
        /// </summary>
        public XUnit(double value, XGraphicsUnit type)
        {
            if (!Enum.IsDefined(typeof(XGraphicsUnit), type))
#if !SILVERLIGHT
                throw new System.ComponentModel.InvalidEnumArgumentException("type");
#else
        throw new ArgumentException("type");
#endif

            this.value = value;
            this.type = type;
        }

        /// <summary>
        /// Gets the raw value of the object without any conversion.
        /// To determine the XGraphicsUnit use property <code>Type</code>.
        /// To get the value in point use the implicit conversion to double.
        /// </summary>
        public readonly double Value
        {
            get { return value; }
        }

        /// <summary>
        /// Gets the unit of measure.
        /// </summary>
        public readonly XGraphicsUnit Type
        {
            get { return type; }
        }

        /// <summary>
        /// Gets or sets the value in point.
        /// </summary>
        public double Point
        {
            readonly get
            {
                return type switch
                {
                    XGraphicsUnit.Point => value,
                    XGraphicsUnit.Inch => value * 72,
                    XGraphicsUnit.Millimeter => value * 72 / 25.4,
                    XGraphicsUnit.Centimeter => value * 72 / 2.54,
                    XGraphicsUnit.Presentation => value * 72 / 96,
                    _ => throw new InvalidCastException(),
                };
            }
            set
            {
                this.value = value;
                type = XGraphicsUnit.Point;
            }
        }

        /// <summary>
        /// Gets or sets the value in inch.
        /// </summary>
        public double Inch
        {
            readonly get
            {
                return type switch
                {
                    XGraphicsUnit.Point => value / 72,
                    XGraphicsUnit.Inch => value,
                    XGraphicsUnit.Millimeter => value / 25.4,
                    XGraphicsUnit.Centimeter => value / 2.54,
                    XGraphicsUnit.Presentation => value / 96,
                    _ => throw new InvalidCastException(),
                };
            }
            set
            {
                this.value = value;
                type = XGraphicsUnit.Inch;
            }
        }

        /// <summary>
        /// Gets or sets the value in millimeter.
        /// </summary>
        public double Millimeter
        {
            readonly get
            {
                return type switch
                {
                    XGraphicsUnit.Point => value * 25.4 / 72,
                    XGraphicsUnit.Inch => value * 25.4,
                    XGraphicsUnit.Millimeter => value,
                    XGraphicsUnit.Centimeter => value * 10,
                    XGraphicsUnit.Presentation => value * 25.4 / 96,
                    _ => throw new InvalidCastException(),
                };
            }
            set
            {
                this.value = value;
                type = XGraphicsUnit.Millimeter;
            }
        }

        /// <summary>
        /// Gets or sets the value in centimeter.
        /// </summary>
        public double Centimeter
        {
            readonly get
            {
                return type switch
                {
                    XGraphicsUnit.Point => value * 2.54 / 72,
                    XGraphicsUnit.Inch => value * 2.54,
                    XGraphicsUnit.Millimeter => value / 10,
                    XGraphicsUnit.Centimeter => value,
                    XGraphicsUnit.Presentation => value * 2.54 / 96,
                    _ => throw new InvalidCastException(),
                };
            }
            set
            {
                this.value = value;
                type = XGraphicsUnit.Centimeter;
            }
        }

        /// <summary>
        /// Gets or sets the value in presentation units (1/96 inch).
        /// </summary>
        public double Presentation
        {
            readonly get
            {
                return type switch
                {
                    XGraphicsUnit.Point => value * 96 / 72,
                    XGraphicsUnit.Inch => value * 96,
                    XGraphicsUnit.Millimeter => value * 96 / 25.4,
                    XGraphicsUnit.Centimeter => value * 96 / 2.54,
                    XGraphicsUnit.Presentation => value,
                    _ => throw new InvalidCastException(),
                };
            }
            set
            {
                this.value = value;
                type = XGraphicsUnit.Point;
            }
        }

        /// <summary>
        /// Returns the object as string using the format information.
        /// The unit of measure is appended to the end of the string.
        /// </summary>
        public string ToString(IFormatProvider formatProvider)
        {
            string valuestring;
            valuestring = value.ToString(formatProvider) + GetSuffix();
            return valuestring;
        }

        /// <summary>
        /// Returns the object as string using the specified format and format information.
        /// The unit of measure is appended to the end of the string.
        /// </summary>
        string IFormattable.ToString(string format, IFormatProvider formatProvider)
        {
            string valuestring;
            valuestring = value.ToString(format, formatProvider) + GetSuffix();
            return valuestring;
        }

        /// <summary>
        /// Returns the object as string. The unit of measure is appended to the end of the string.
        /// </summary>
        public override string ToString()
        {
            string valuestring;
            valuestring = value.ToString(CultureInfo.InvariantCulture) + GetSuffix();
            return valuestring;
        }

        /// <summary>
        /// Returns the unit of measure of the object as a string like 'pt', 'cm', or 'in'.
        /// </summary>
        private readonly string GetSuffix()
        {
            return type switch
            {
                XGraphicsUnit.Point => "pt",
                XGraphicsUnit.Inch => "in",
                XGraphicsUnit.Millimeter => "mm",
                XGraphicsUnit.Centimeter => "cm",
                XGraphicsUnit.Presentation => "pu",
                //case XGraphicsUnit.Pica:
                //  return "pc";
                //case XGraphicsUnit.Line:
                //  return "li";
                _ => throw new InvalidCastException(),
            };
        }

        /// <summary>
        /// Returns an XUnit object. Sets type to point.
        /// </summary>
        public static XUnit FromPoint(double value)
        {
            XUnit unit;
            unit.value = value;
            unit.type = XGraphicsUnit.Point;
            return unit;
        }

        /// <summary>
        /// Returns an XUnit object. Sets type to inch.
        /// </summary>
        public static XUnit FromInch(double value)
        {
            XUnit unit;
            unit.value = value;
            unit.type = XGraphicsUnit.Inch;
            return unit;
        }

        /// <summary>
        /// Returns an XUnit object. Sets type to millimeters.
        /// </summary>
        public static XUnit FromMillimeter(double value)
        {
            XUnit unit;
            unit.value = value;
            unit.type = XGraphicsUnit.Millimeter;
            return unit;
        }

        /// <summary>
        /// Returns an XUnit object. Sets type to centimeters.
        /// </summary>
        public static XUnit FromCentimeter(double value)
        {
            XUnit unit;
            unit.value = value;
            unit.type = XGraphicsUnit.Centimeter;
            return unit;
        }

        /// <summary>
        /// Returns an XUnit object. Sets type to Presentation.
        /// </summary>
        public static XUnit FromPresentation(double value)
        {
            XUnit unit;
            unit.value = value;
            unit.type = XGraphicsUnit.Presentation;
            return unit;
        }

#if deferred
    ///// <summary>
    ///// Returns an XUnit object. Sets type to pica.
    ///// </summary>
    //public static XUnit FromPica(double val)
    //{
    //  XUnit unit;
    //  unit.val = val;
    //  unit.type = XGraphicsUnit.Pica;
    //  return unit;
    //}
    //
    ///// <summary>
    ///// Returns an XUnit object. Sets type to line.
    ///// </summary>
    //public static XUnit FromLine(double val)
    //{
    //  XUnit unit;
    //  unit.val = val;
    //  unit.type = XGraphicsUnit.Line;
    //  return unit;
    //}
#endif

        /// <summary>
        /// Converts a string to an XUnit object.
        /// If the string contains a suffix like 'cm' or 'in' the object will be converted
        /// to the appropriate type, otherwise point is assumed.
        /// </summary>
        public static implicit operator XUnit(string value)
        {
            XUnit unit;
            value = value.Trim();

            // HACK for Germans...
            value = value.Replace(',', '.');

            int count = value.Length;
            int valLen = 0;
            for (; valLen < count;)
            {
                char ch = value[valLen];
                if (ch == '.' || ch == '-' || ch == '+' || Char.IsNumber(ch))
                    valLen++;
                else
                    break;
            }

            try
            {
                unit.value = Double.Parse(value[..valLen].Trim(), CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                unit.value = 1;
                string message = $"String '{value}' is not a valid value for structure 'XUnit'.";
                throw new ArgumentException(message, ex);
            }

            string typeStr = value[valLen..].Trim().ToLower();
            unit.type = XGraphicsUnit.Point;
            unit.type = typeStr switch
            {
                "cm" => XGraphicsUnit.Centimeter,
                "in" => XGraphicsUnit.Inch,
                "mm" => XGraphicsUnit.Millimeter,
                //case "pc":
                //  unit.type = XGraphicsUnit.Pica;
                //  break;
                //
                //case "li":
                //  unit.type = XGraphicsUnit.Line;
                //  break;
                "" or "pt" => XGraphicsUnit.Point,
                // presentation units
                "pu" => XGraphicsUnit.Presentation,
                _ => throw new ArgumentException("Unknown unit type: '" + typeStr + "'"),
            };
            return unit;
        }

        /// <summary>
        /// Converts an int to an XUnit object with type set to point.
        /// </summary>
        public static implicit operator XUnit(int value)
        {
            XUnit unit;
            unit.value = value;
            unit.type = XGraphicsUnit.Point;
            return unit;
        }

        /// <summary>
        /// Converts a double to an XUnit object with type set to point.
        /// </summary>
        public static implicit operator XUnit(double value)
        {
            XUnit unit;
            unit.value = value;
            unit.type = XGraphicsUnit.Point;
            return unit;
        }

        /// <summary>
        /// Returns a double value as point.
        /// </summary>
        public static implicit operator double(XUnit value)
        {
            return value.Point;
        }

        /// <summary>
        /// Memberwise comparison. To compare by value, 
        /// use code like Math.Abs(a.Pt - b.Pt) &lt; 1e5.
        /// </summary>
        public static bool operator ==(XUnit value1, XUnit value2)
        {
            return value1.type == value2.type && value1.value == value2.value;
        }

        /// <summary>
        /// Memberwise comparison. To compare by value, 
        /// use code like Math.Abs(a.Pt - b.Pt) &lt; 1e5.
        /// </summary>
        public static bool operator !=(XUnit value1, XUnit value2)
        {
            return !(value1 == value2);
        }

        /// <summary>
        /// Calls base class Equals.
        /// </summary>
        public override readonly bool Equals(Object obj)
        {
            if (obj is XUnit)
                return this == (XUnit)obj;
            return false;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        public override readonly int GetHashCode()
        {
            return value.GetHashCode() ^ type.GetHashCode();
        }

        /// <summary>
        /// This member is intended to be used by XmlDomainObjectReader only.
        /// </summary>
        public static XUnit Parse(string value)
        {
            XUnit unit = value;
            return unit;
        }

        /// <summary>
        /// Converts an existing object from one unit into another unit type.
        /// </summary>
        public void ConvertType(XGraphicsUnit type)
        {
            if (this.type == type)
                return;

            switch (type)
            {
                case XGraphicsUnit.Point:
                    value = Point;
                    this.type = XGraphicsUnit.Point;
                    break;

                case XGraphicsUnit.Inch:
                    value = Inch;
                    this.type = XGraphicsUnit.Inch;
                    break;

                case XGraphicsUnit.Centimeter:
                    value = Centimeter;
                    this.type = XGraphicsUnit.Centimeter;
                    break;

                case XGraphicsUnit.Millimeter:
                    value = Millimeter;
                    this.type = XGraphicsUnit.Millimeter;
                    break;

                case XGraphicsUnit.Presentation:
                    value = Presentation;
                    this.type = XGraphicsUnit.Presentation;
                    break;

                //        case XGraphicsUnit.Pica:
                //          this.value = this.Pc;
                //          this.type = XGraphicsUnit.Pica;
                //          break;
                //        
                //        case XGraphicsUnit.Line:
                //          this.value = this.Li;
                //          this.type = XGraphicsUnit.Line;
                //          break;

                default:
                    throw new ArgumentException("Unknown unit type: '" + type + "'");
            }
        }

        /// <summary>
        /// Represents a unit with all values zero.
        /// </summary>
        public static readonly XUnit Zero;

        private double value;
        private XGraphicsUnit type;

#if true_
    /// <summary>
    /// Some test code.
    /// </summary>
    [Conditional("DEBUG")]
    public static void TestIt()
    {
      double v;
      XUnit u1 = 1000;
      v = u1;
      v = u1.Point;
      v = u1.Inch;
      v = u1.Millimeter;
      v = u1.Centimeter;
      v = u1.Presentation;
      u1 = "10cm";
      v = u1.Point;
      v.GetType();
    }
#endif
    }
}