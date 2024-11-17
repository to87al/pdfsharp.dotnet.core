using PdfSharp.Drawing;
using PdfSharp.Internal;
using System;
using System.Globalization;

#pragma warning disable 414, 169, 649 // incomplete code state

namespace PdfSharp.Xps.XpsModel
{
    internal struct Color
    {
        public byte A
        {
            readonly get { return sRgbColor.a; }
            set
            {
                sRgbColor.a = value;
                scRgbColor.a = ((float)value) / 255f;
            }
        }

        public byte R
        {
            readonly get { return sRgbColor.r; }
            set
            {
                sRgbColor.r = value;
                scRgbColor.r = ColorHelper.sRgbToScRgb(value);
            }
        }

        public byte G
        {
            readonly get { return sRgbColor.g; }
            set
            {
                sRgbColor.g = value;
                scRgbColor.g = ColorHelper.sRgbToScRgb(value);
            }
        }

        public byte B
        {
            readonly get { return sRgbColor.b; }
            set
            {
                sRgbColor.b = value;
                scRgbColor.b = ColorHelper.sRgbToScRgb(value);
            }
        }

        public float ScA
        {
            readonly get { return scRgbColor.a; }
            set
            {
                scRgbColor.a = value;
                if (value < 0f)
                    sRgbColor.a = 0;
                else if (value > 1f)
                    sRgbColor.a = 0xff;
                else
                    sRgbColor.a = (byte)(value * 255f);
            }
        }

        public float ScR
        {
            readonly get { return scRgbColor.r; }
            set
            {
                scRgbColor.r = value;
                sRgbColor.r = ColorHelper.ScRgbTosRgb(value);
            }
        }

        public float ScG
        {
            readonly get { return scRgbColor.g; }
            set
            {
                scRgbColor.g = value;
                sRgbColor.g = ColorHelper.ScRgbTosRgb(value);
            }
        }

        public float ScB
        {
            readonly get { return scRgbColor.b; }
            set
            {
                scRgbColor.b = value;
                sRgbColor.b = ColorHelper.ScRgbTosRgb(value);
            }
        }

        public static implicit operator XColor(Color clr)
        {
            return XColor.FromArgb(clr.A, clr.R, clr.G, clr.B);
        }

        internal static Color FromArgb(byte a, byte r, byte g, byte b)
        {
            Color clr = new();
            clr.A = a;
            clr.R = r;
            clr.G = g;
            clr.B = b;
            return clr;
        }

        internal static Color Parse(string value)
        {
            Color clr = new();

            int length = value.Length;
            if (value.StartsWith('#'))
            {
                if (length == 7)
                {
                    clr.colorType = ColorType.scRGB;
                    uint val = UInt32.Parse(value[1..], NumberStyles.HexNumber);
                    clr.A = 0xFF;
                    clr.R = (byte)((val >> 16) & 0xFF);
                    clr.G = (byte)((val >> 8) & 0xFF);
                    clr.B = (byte)(val & 0xFF);
                }
                else if (length == 9)
                {
                    clr.colorType = ColorType.scRGBwithAlpha;
                    uint val = UInt32.Parse(value[1..], NumberStyles.HexNumber);
                    clr.A = (byte)((val >> 24) & 0xFF);
                    clr.R = (byte)((val >> 16) & 0xFF);
                    clr.G = (byte)((val >> 8) & 0xFF);
                    clr.B = (byte)(val & 0xFF);
                }
            }
            else
            {
                // TODO
                if (value.StartsWith("{StaticResource"))
                {
                    DevHelper.NotImplemented("Color StaticResource: " + value);
                    // HACK: just continue
                    return FromArgb(255, 0, 128, 0);
                }
                else if (value.StartsWith("sc#"))
                {
                    string[] xx = value[3..].Split(',');
                    if (xx.Length == 3)
                    {
                        clr.ScR = float.Parse(xx[0], CultureInfo.InvariantCulture);
                        clr.ScG = float.Parse(xx[1], CultureInfo.InvariantCulture);
                        clr.ScB = float.Parse(xx[2], CultureInfo.InvariantCulture);
                    }
                    else if (xx.Length == 4)
                    {
                        clr.ScA = float.Parse(xx[0], CultureInfo.InvariantCulture);
                        clr.ScR = float.Parse(xx[1], CultureInfo.InvariantCulture);
                        clr.ScG = float.Parse(xx[2], CultureInfo.InvariantCulture);
                        clr.ScB = float.Parse(xx[3], CultureInfo.InvariantCulture);
                    }
                    else
                        throw new NotImplementedException("Color type format.");
                }
                else if (value.StartsWith("ContextColor"))
                {
                    DevHelper.NotImplemented("Color profile: " + value);
                    // HACK: just continue
                    return FromArgb(255, 0, 128, 0);
                }
                else
                    throw new NotImplementedException("Color type.");
            }
            return clr;
        }

        private ColorType colorType;
        private SColor sRgbColor;
        private SCColor scRgbColor;
    }

    internal static class ColorConverter
    {
        public static XColor ToXColor(this System.Windows.Media.Color c)
        {
            return XColor.FromArgb(c.A, c.R, c.G, c.B);
        }

        public static Color ToXpsColor(this System.Windows.Media.Color c)
        {
            return Color.FromArgb(c.A, c.R, c.G, c.B);
        }

        public static System.Windows.Media.Color AlphaToGray(this System.Windows.Media.Color color)
        {
            return System.Windows.Media.Color.FromArgb(255, color.A, color.A, color.A);
        }
    }
}