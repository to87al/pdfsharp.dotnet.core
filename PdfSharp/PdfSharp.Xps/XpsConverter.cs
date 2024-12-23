﻿using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Xps.Rendering;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Xps.Packaging;
using IOPath = System.IO.Path;

namespace PdfSharp.Xps
{
    /// <summary>
    /// Main class that provides the functionallity to convert an XPS file into a PDF file.
    /// </summary>
    public class XpsConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XpsConverter"/> class.
        /// </summary>
        /// <param name="pdfDocument">The PDF document.</param>
        /// <param name="xpsDocument">The XPS document.</param>
        public XpsConverter(PdfDocument pdfDocument, XpsDocument xpsDocument)
        {
            ArgumentNullException.ThrowIfNull(pdfDocument);
            ArgumentNullException.ThrowIfNull(xpsDocument);

            this.pdfDocument = pdfDocument;
            this.xpsDocument = xpsDocument;

            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XpsConverter"/> class.
        /// </summary>
        /// <param name="pdfDocument">The PDF document.</param>
        /// <param name="xpsDocumentPath">The XPS document path.</param>
        public XpsConverter(PdfDocument pdfDocument, string xpsDocumentPath) // TODO: a constructor with an Uri
        {
            ArgumentNullException.ThrowIfNull(pdfDocument);
            if (String.IsNullOrEmpty(xpsDocumentPath))
                throw new ArgumentNullException(nameof(xpsDocumentPath));

            this.pdfDocument = pdfDocument;
            xpsDocument = new XpsDocument(xpsDocumentPath, FileAccess.Read);

            Initialize();
        }

        private void Initialize()
        {
            context = new DocumentRenderingContext(pdfDocument);
        }

        private DocumentRenderingContext context;

        /// <summary>
        /// Gets the PDF document of this converter.
        /// </summary>
        public PdfDocument PdfDocument
        {
            get { return pdfDocument; }
        }

        private readonly PdfDocument pdfDocument;

        /// <summary>
        /// Gets the XPS document of this converter.
        /// </summary>
        public XpsDocument XpsDocument
        {
            get { return xpsDocument; }
        }

        private readonly XpsDocument xpsDocument;

        /// <summary>
        /// Converts the specified PDF file into an XPS file. The new file is stored in the same directory.
        /// </summary>
        public static void Convert(string xpsFilename)
        {
            if (String.IsNullOrEmpty(xpsFilename))
                throw new ArgumentNullException(nameof(xpsFilename));

            if (!File.Exists(xpsFilename))
                throw new FileNotFoundException("File not found.", xpsFilename);

            string pdfFilename = xpsFilename;
            if (IOPath.HasExtension(pdfFilename))
                pdfFilename = pdfFilename[..pdfFilename.LastIndexOf('.')];
            pdfFilename += ".pdf";

            Convert(xpsFilename, pdfFilename, 0);
        }

        /// <summary>
        /// Implements the PDF file to XPS file conversion.
        /// </summary>
        public static void Convert(string xpsFilename, string pdfFilename, int docInde)
        {
            Convert(xpsFilename, pdfFilename, docInde, false);
        }

        /// <summary>
        /// Implements the PDF file to XPS file conversion.
        /// </summary>
        public static void Convert(XpsDocument xpsDocument, string pdfFilename, int docIndex)
        {
            var apartment = Thread.CurrentThread.GetApartmentState();
            if (apartment != ApartmentState.STA)
            {
                AutoResetEvent evCompleted = new(false);
                Thread t = new(new ParameterizedThreadStart((evt) =>
                {
                    try
                    {
                        DoConvert(xpsDocument, pdfFilename, docIndex);
                    }
                    finally
                    {
                        (evt as AutoResetEvent).Set();
                    }
                }));
                t.SetApartmentState(ApartmentState.STA);
                t.Start(evCompleted);
                evCompleted.WaitOne();
                return;
            }
            else
            {
                DoConvert(xpsDocument, pdfFilename, docIndex);
            }
        }

        /// <summary>
        /// Implements the PDF file to XPS file conversion.
        /// </summary>
        private static void DoConvert(XpsDocument xpsDocument, string pdfFilename, int docIndex)
        {
            ArgumentNullException.ThrowIfNull(xpsDocument);

            if (String.IsNullOrEmpty(pdfFilename))
                throw new ArgumentNullException(nameof(pdfFilename));

            var seq = xpsDocument.GetFixedDocumentSequence();
            var pdfDocument = new PdfDocument();
            var renderer = new PdfRenderer();

            int pageIndex = 0;

            var refs = seq.References;

            var docs = refs.Select(r => r.GetDocument(false));

            foreach (var fixedDocument in docs)
            {
                foreach (var page in fixedDocument.Pages)
                {
                    if (page == null)
                        continue;
                    var fixedPage = page.GetPageRoot(false);
                    if (fixedPage == null)
                        continue;
                    Debug.WriteLine($"  doc={docIndex}, page={pageIndex}");
                    PdfPage pdfPage = PdfRenderer.CreatePage(pdfDocument, fixedPage);
                    renderer.RenderPage(pdfPage, fixedPage);
                    pageIndex++;

#if DEBUG
          // stop at page...
          if (pageIndex == 50)
            break;
#endif
                }
            }
            pdfDocument.Save(pdfFilename);
        }
        public static void Convert(string xpsFilename, string pdfFilename, int docIndex, bool createComparisonDocument)
        {
            if (String.IsNullOrEmpty(xpsFilename))
                throw new ArgumentNullException(nameof(xpsFilename));

            if (String.IsNullOrEmpty(pdfFilename))
            {
                pdfFilename = xpsFilename;
                if (IOPath.HasExtension(pdfFilename))
                    pdfFilename = pdfFilename[..pdfFilename.LastIndexOf('.')];
                pdfFilename += ".pdf";
            }

            using (var xpsDocument = new XpsDocument(xpsFilename, FileAccess.Read))
            {
                Convert(xpsDocument, pdfFilename, docIndex);
            }
            if (!createComparisonDocument)
            {
                return;
            }

            try
            {
                using (var xpsDoc = new XpsDocument(xpsFilename, FileAccess.Read))
                {
                    var docSeq = xpsDoc.GetFixedDocumentSequence() ?? throw new InvalidOperationException("docSeq");
                    XPdfForm form = XPdfForm.FromFile(pdfFilename);
                    PdfDocument pdfComparisonDocument = new();


                    var pageIndex = 0;

                    var refs = docSeq.References;

                    var docs = refs.Select(r => r.GetDocument(false));

                    foreach (var doc in docs)
                    {
                        foreach (var page in doc.Pages)
                        {
                            Debug.WriteLine($"  doc={docIndex}, page={pageIndex}");

                            PdfPage pdfPage = pdfComparisonDocument.AddPage();
                            double width = page.Width;
                            double height = page.Height;
                            pdfPage.Width = page.Width * 2;
                            pdfPage.Height = page.Height;


                            DocumentPage docPage = docSeq.DocumentPaginator.GetPage(pageIndex);
                            //byte[] png = PngFromPage(docPage, 96);

                            BitmapSource bmsource = BitmapSourceFromPage(docPage, 96 * 2);
                            XImage image = XImage.FromBitmapSource(bmsource);

                            XGraphics gfx = XGraphics.FromPdfPage(pdfPage);
                            form.PageIndex = pageIndex;
                            gfx.DrawImage(form, 0, 0, width, height);
                            gfx.DrawImage(image, width, 0, width, height);

                            //renderer.RenderPage(pdfPage, page);
                            pageIndex++;

#if DEBUG
              // stop at page...
              if (pageIndex == 50)
                break;
#endif
                        }
                    }

                    string pdfComparisonFilename = pdfFilename;
                    if (IOPath.HasExtension(pdfComparisonFilename))
                        pdfComparisonFilename = pdfComparisonFilename[..pdfComparisonFilename.LastIndexOf('.')];
                    pdfComparisonFilename += "-comparison.pdf";

                    pdfComparisonDocument.ViewerPreferences.FitWindow = true;
                    //pdfComparisonDocument.PageMode = PdfPageMode.
                    pdfComparisonDocument.PageLayout = PdfPageLayout.SinglePage;
                    pdfComparisonDocument.Save(pdfComparisonFilename);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public static void SaveXpsPageToBitmap(string xpsFileName)
        {
            var xpsDoc = new XpsDocument(xpsFileName, FileAccess.Read);
            var docSeq = xpsDoc.GetFixedDocumentSequence();

            // You can get the total page count from docSeq.PageCount    
            for (int pageNum = 0; pageNum < docSeq.DocumentPaginator.PageCount; ++pageNum)
            {
                DocumentPage docPage = docSeq.DocumentPaginator.GetPage(pageNum);
                RenderTargetBitmap renderTarget =
                  new((int)docPage.Size.Width,
                    (int)docPage.Size.Height,
                    96, // WPF (Avalon) units are 96dpi based    
                    96,
                    PixelFormats.Default);

                renderTarget.Render(docPage.Visual);

                BitmapEncoder encoder = new BmpBitmapEncoder(); // Choose type here ie: JpegBitmapEncoder, etc   
                encoder.Frames.Add(BitmapFrame.Create(renderTarget));

                FileStream pageOutStream = new(xpsFileName + ".Page" + pageNum + ".bmp", FileMode.Create,
                  FileAccess.Write);
                encoder.Save(pageOutStream);
                pageOutStream.Close();
            }
        }

        static public BitmapSource BitmapSourceFromPage(DocumentPage docPage, double resolution)
        {
            double pixelWidth = docPage.Size.Width * resolution / 96;
            double pixelHeight = docPage.Size.Height * resolution / 96;
            RenderTargetBitmap renderTarget = new((int)pixelWidth, (int)pixelHeight, resolution, resolution, PixelFormats.Default);
            renderTarget.Render(docPage.Visual);

            return renderTarget;

            //PngBitmapEncoder encoder = new PngBitmapEncoder();  // Choose type here ie: JpegBitmapEncoder, etc   
            //encoder.Frames.Add(BitmapFrame.Create(renderTarget));

            //BitmapSource.Create(pageWidth, pageHeight, resolution, resolution, PixelFormats.)

            //return encoder.Preview;
            //encoder.
            //BitmapSource s = Xps;
            ////FileStream pageOutStream = new FileStream(xpsFileName + ".Page" + pageNum + ".bmp", FileMode.Create, FileAccess.Write);
            //MemoryStream memStream = new MemoryStream();
            //encoder.Save(memStream);
            //return memStream.ToArray();
        }

        //byte[] void PngFromPage(FixedDocument fixedDocument, int pageIndex, double resolution)
        //{
        //  if (fixedDocument==null)
        //    throw new ArgumentNullException("fixedDocument");
        //  if ( pageIndex<0|| pageIndex>= fixedDocument.PageCount)
        //    throw new ArgumentOutOfRangeException("pageIndex");

        //  FixedPage page = fixedDocument.Pages[pageIndex];
        //  double pageWidth = page.Width;
        //  double pageHeight= page.Height;

        //  // Create an appropirate render bitmap
        //  const int factor = 3;
        //  int width = (int)(WidthInPoint * factor);
        //  int height = (int)(HeightInPoint * factor);
        //  this.image = new RenderTargetBitmap(width, height, 72 * factor, 72 * factor, PixelFormats.Default);
        //  if (visual is UIElement)
        //  {
        //    // Perform layout on UIElement - otherwise nothing gets rendered
        //    UIElement element = visual as UIElement;
        //    Size size = new Size(WidthInPU, HeightInPU);
        //    element.Measure(size);
        //    element.Arrange(new Rect(new Point(), size));
        //    element.UpdateLayout();
        //  }
        //  this.image.Render(visual);

        //  // Save image as PNG
        //  FileStream stream = new FileStream(Path.Combine(OutputDirectory, Name + ".png"), FileMode.Create);
        //  PngBitmapEncoder encoder = new PngBitmapEncoder();
        //  //string author = encoder.CodecInfo.Author.ToString();
        //  encoder.Frames.Add(BitmapFrame.Create(this.image));
        //  encoder.Save(stream);
        //  stream.Close();
        //}
    }
}