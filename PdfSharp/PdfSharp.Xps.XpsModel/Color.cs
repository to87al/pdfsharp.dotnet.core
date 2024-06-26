﻿using PdfSharp.Drawing;
using PdfSharp.Internal;
using System;
using System.Globalization;

#pragma warning disable 414, 169, 649 // incomplete code state

namespace PdfSharp.Xps.XpsModel
{
    struct Color
    {
        public byte A
        {
            readonly get { return this.sRgbColor.a; }
            set
            {
                this.sRgbColor.a = value;
                this.scRgbColor.a = ((float)value) / 255f;
            }
        }

        public byte R
        {
            readonly get { return this.sRgbColor.r; }
            set
            {
                this.sRgbColor.r = value;
                this.scRgbColor.r = ColorHelper.sRgbToScRgb(value);
            }
        }

        public byte G
        {
            readonly get { return this.sRgbColor.g; }
            set
            {
                this.sRgbColor.g = value;
                this.scRgbColor.g = ColorHelper.sRgbToScRgb(value);
            }
        }

        public byte B
        {
            readonly get { return this.sRgbColor.b; }
            set
            {
                this.sRgbColor.b = value;
                this.scRgbColor.b = ColorHelper.sRgbToScRgb(value);
            }
        }

        public float ScA
        {
            readonly get { return this.scRgbColor.a; }
            set
            {
                this.scRgbColor.a = value;
                if (value < 0f)
                    this.sRgbColor.a = 0;
                else if (value > 1f)
                    this.sRgbColor.a = 0xff;
                else
                    this.sRgbColor.a = (byte)(value * 255f);
            }
        }

        public float ScR
        {
            readonly get { return this.scRgbColor.r; }
            set
            {
                this.scRgbColor.r = value;
                this.sRgbColor.r = ColorHelper.ScRgbTosRgb(value);
            }
        }

        public float ScG
        {
            readonly get { return this.scRgbColor.g; }
            set
            {
                this.scRgbColor.g = value;
                this.sRgbColor.g = ColorHelper.ScRgbTosRgb(value);
            }
        }

        public float ScB
        {
            readonly get { return this.scRgbColor.b; }
            set
            {
                this.scRgbColor.b = value;
                this.sRgbColor.b = ColorHelper.ScRgbTosRgb(value);
            }
        }

        public static implicit operator PdfSharp.Drawing.XColor(Color clr)
        {
            return PdfSharp.Drawing.XColor.FromArgb(clr.A, clr.R, clr.G, clr.B);
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
                    return Color.FromArgb(255, 0, 128, 0);
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
                    return Color.FromArgb(255, 0, 128, 0);
                }
                else
                    throw new NotImplementedException("Color type.");
            }
            return clr;
        }

        ColorType colorType;
        SColor sRgbColor;
        SCColor scRgbColor;
    }

    static class ColorConverter
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