﻿using System.Windows.Media;

namespace PdfSharp.Xps.Rendering
{
    /// <summary>
    /// Base class for all builder classes.
    /// </summary>
    internal class BuilderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuilderBase"/> class.
        /// </summary>
        protected BuilderBase(DocumentRenderingContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets the document rendering context this builder is associated with.
        /// </summary>
        protected DocumentRenderingContext Context
        {
            get { return context; }
        }

        private readonly DocumentRenderingContext context;
    }

    /// <summary>
    /// Base class for TilingPatternBuilder and ShadingBuilder.
    /// </summary>
    internal class PatternOrShadingBuilder : BuilderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuilderBase"/> class.
        /// </summary>
        protected PatternOrShadingBuilder(DocumentRenderingContext context)
          : base(context)
        { }

        protected static bool CanOptimizeForTwoColors(GradientStopCollection gradients)
        {
            return gradients.Count == 2 && gradients[0].Offset == 0 && gradients[1].Offset == 1;
        }

    }
}
