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
using System.Collections.Generic;
#if GDI
using System.Drawing;
using System.Drawing.Drawing2D;
#endif
#if WPF
#endif

namespace PdfSharp.Drawing
{
    /// <summary>
    /// Represents a stack of XGraphicsState and XGraphicsContainer objects.
    /// </summary>
    internal class GraphicsStateStack
    {
        public GraphicsStateStack(XGraphics gfx)
        {
            current = new InternalGraphicsState(gfx);
        }

        public int Count
        {
            get { return stack.Count; }
        }

        public void Push(InternalGraphicsState state)
        {
            stack.Push(state);
            InternalGraphicsState.Pushed();
        }

        public int Restore(InternalGraphicsState state)
        {
            if (!stack.Contains(state))
                throw new ArgumentException("State not on stack.", nameof(state));
            if (state.invalid)
                throw new ArgumentException("State already restored.", nameof(state));

            int count = 1;
            InternalGraphicsState top = (InternalGraphicsState)stack.Pop();
            top.Popped();
            while (top != state)
            {
                count++;
                state.invalid = true;
                top = (InternalGraphicsState)stack.Pop();
                top.Popped();
            }
            state.invalid = true;
            return count;
        }

        public InternalGraphicsState Current
        {
            get
            {
                if (stack.Count == 0)
                    return current;
                return stack.Peek();
            }
        }

        private readonly InternalGraphicsState current;
        private readonly Stack<InternalGraphicsState> stack = new();
    }
}