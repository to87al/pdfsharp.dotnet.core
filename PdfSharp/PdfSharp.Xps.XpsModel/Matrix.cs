using PdfSharp.Drawing;
using System.Windows.Media;

namespace PdfSharp.Xps.XpsModel
{
    /// <summary>
    /// Represents a transformation matrix.
    /// </summary>
    internal static class MatrixHelper
    {
        public static XMatrix ToXMatrix(this Matrix matrix)
        {
            return new XMatrix(matrix.M11, matrix.M12,
              matrix.M21, matrix.M22,
              matrix.OffsetX, matrix.OffsetY);
        }
    }
}
