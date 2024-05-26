using PdfSharp.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PdfSharp.Xps.Rendering
{
    class ImageBuilder : BuilderBase
    {
        ImageBuilder(DocumentRenderingContext context) :
          base(context)
        {
        }

        public static XImage FromImageBrush(DocumentRenderingContext context,
          ImageBrush brush)
        {
            ImageBuilder ib = new(context);
            XImage xpImage = Build(brush);
            return xpImage;
        }

        private static XImage Build(ImageBrush brush)
        {
            XpsImage xpImage = new(brush.ImageSource as BitmapSource);
            return xpImage;
        }
    }

    class XpsImage : XImage
    {
        public XpsImage(BitmapSource src)
        {
            base.wpfImage = src;
            var t = typeof(XImage);
            var fmtProperty = t.GetField("format", BindingFlags.Instance | BindingFlags.NonPublic);
            fmtProperty.SetValue(this, XImageFormat.Png);
        }

        public override bool IsCmyk
        {
            get
            {
                return false;
            }
        }

        public override bool IsJpeg
        {
            get
            {
                return false;
            }
        }

        private MemoryStream memory;

        public override MemoryStream Memory
        {
            get
            {
                if (memory != null)
                {
                    return memory;
                }
                memory = new MemoryStream();
                BitmapEncoder enc = new PngBitmapEncoder();
                if (wpfImage is BitmapFrame)
                {
                    var frame = wpfImage as BitmapFrame;
                    enc.Frames.Add(frame);
                }
                else
                {
                    enc.Frames.Add(BitmapFrame.Create(wpfImage));
                }
                enc.Save(memory);
                return memory;
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            memory.Close();
        }
    }
}
