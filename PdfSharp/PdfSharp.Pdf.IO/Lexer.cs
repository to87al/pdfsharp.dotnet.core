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

using PdfSharp.Pdf.Internal;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;

namespace PdfSharp.Pdf.IO
{
    /// <summary>
    /// Lexical analyzer for PDF files. Technically a PDF file is a stream of bytes. Some chunks
    /// of bytes represent strings in several encodings. The actual encoding depends on the
    /// context where the string is used. Therefore the bytes are 'raw encoded' into characters,
    /// i.e. a character or token read by the lexer has always character values in the range from
    /// 0 to 255.
    /// </summary>
    internal class Lexer
    {
        /// <summary>
        /// Initializes a new instance of the Lexer class.
        /// </summary>
        public Lexer(Stream pdfInputStream)
        {
            pdf = pdfInputStream;
            pdfLength = (int)pdf.Length;
            idxChar = 0;
            Position = 0;
        }

        /// <summary>
        /// Initializes fields after position has changed.
        /// </summary>
        private void Initialize()
        {
            currChar = (char)pdf.ReadByte();
            nextChar = (char)pdf.ReadByte();
            token = new StringBuilder();
            //this.symbol = Symbol.None;
        }

        /// <summary>
        /// Gets or sets the position within the PDF stream.
        /// </summary>
        public int Position
        {
            get { return idxChar; }
            set
            {
                idxChar = value;
                pdf.Position = value;
                Initialize();
            }
        }

        /// <summary>
        /// Reads the next token and returns its type. If the token starts with a digit, the parameter
        /// testReference specifies how to treat it. If it is false, the lexer scans for a single integer.
        /// If it is true, the lexer checks if the digit is the prefix of a reference. If it is a reference,
        /// the token is set to the object ID followed by the generation number separated by a blank
        /// (the 'R' is omitted from the token).
        /// </summary>
        // /// <param name="testReference">Indicates whether to test the next token if it is a reference.</param>
        public Symbol ScanNextToken()
        {
        Again:
            token = new StringBuilder();
            char ch = MoveToNonWhiteSpace();
            switch (ch)
            {
                case '%':
                    // Eat comments, the parser doesn't handle them
                    //return this.symbol = ScanComment();
                    ScanComment();
                    goto Again;

                case '/':
                    return symbol = ScanName();

                //case 'R':
                //  if (Lexer.IsWhiteSpace(this.nextChar))
                //  {
                //    ScanNextChar();
                //    return Symbol.R;
                //  }
                //  break;

                case '+': //TODO is it so easy?
                case '-':
                    return symbol = ScanNumber();

                case '(':
                    return symbol = ScanLiteralString();

                case '[':
                    ScanNextChar();
                    return symbol = Symbol.BeginArray;

                case ']':
                    ScanNextChar();
                    return symbol = Symbol.EndArray;

                case '<':
                    if (nextChar == '<')
                    {
                        ScanNextChar();
                        ScanNextChar();
                        return symbol = Symbol.BeginDictionary;
                    }
                    return symbol = ScanHexadecimalString();

                case '>':
                    if (nextChar == '>')
                    {
                        ScanNextChar();
                        ScanNextChar();
                        return symbol = Symbol.EndDictionary;
                    }
                    Debug.Assert(false, ">???");
                    break;

                case '.':
                    return symbol = ScanNumber();
            }
            if (Char.IsDigit(ch))
                return symbol = ScanNumber();

            if (Char.IsLetter(ch))
                return symbol = ScanKeyword();

            if (ch == Chars.EOF)
                return symbol = Symbol.Eof;
            Debug.Assert(false, "not implemented");
            return symbol = Symbol.None;
        }

        ////public Symbol ScanNextToken(bool x)
        ////{
        ////  throw new NotImplementedException();
        ////}

        /// <summary>
        /// Reads the raw content of a stream.
        /// </summary>
        public byte[] ReadStream(int length)
        {
            int pos = 0;
            // Skip new line behind �stream�
            if (currChar == Chars.CR)
            {
                if (nextChar == Chars.LF)
                    pos = idxChar + 2;
                else
                    pos = idxChar + 1;
            }
            else
                pos = idxChar + 1;

            pdf.Position = pos;
            byte[] bytes = new byte[length];
            int read = pdf.Read(bytes, 0, length);
            Debug.Assert(read == length);
            // synchronize idxChar etc.
            Position = pos + length;
            return bytes;
        }

        /// <summary>
        /// 
        /// </summary>
        public String ReadRawString(int position, int length)
        {
            pdf.Position = position;
            byte[] bytes = new byte[length];
            pdf.ReadExactly(bytes, 0, length);
            return PdfEncoders.RawEncoding.GetString(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// Scans a comment line.
        /// </summary>
        public Symbol ScanComment()
        {
            Debug.Assert(currChar == Chars.Percent);

            token = new StringBuilder();
            while (AppendAndScanNextChar() != Chars.LF) ;
            // TODO: not correct
            if (token.ToString().StartsWith("%%EOF"))
                return Symbol.Eof;
            return symbol = Symbol.Comment;
        }

        /// <summary>
        /// Scans a name.
        /// </summary>
        public Symbol ScanName()
        {
            Debug.Assert(currChar == Chars.Slash);

            token = new StringBuilder();
            while (true)
            {
                char ch = AppendAndScanNextChar();
                if (IsWhiteSpace(ch) || IsDelimiter(ch))
                    return symbol = Symbol.Name;

                if (ch == '#')
                {
                    ScanNextChar();
                    char[] hex = [currChar, nextChar];
                    ScanNextChar();
                    // TODO Check syntax
                    ch = (char)(ushort)int.Parse(new string(hex), NumberStyles.AllowHexSpecifier);
                    currChar = ch;
                }
            }
        }

        public Symbol ScanNumber()
        {
            // I found a PDF file created with Acrobat 7 with this entry 
            //   /Checksum 2996984786
            // What is this? It is neither an integer nor a real.
            // I introduced an UInteger...
            bool period = false;
            bool sign = false;
            period.GetType();
            sign.GetType();

            token = new StringBuilder();
            char ch = currChar;
            if (ch is '+' or '-')
            {
                sign = true;
                token.Append(ch);
                ch = ScanNextChar();
            }
            while (true)
            {
                if (char.IsDigit(ch))
                {
                    token.Append(ch);
                }
                else if (ch == '.')
                {
                    if (period)
                        throw new PdfReaderException("More than one period in number.");
                    period = true;
                    token.Append(ch);
                }
                else
                    break;
                ch = ScanNextChar();
            }

            if (period)
                return Symbol.Real;
            long l = Int64.Parse(token.ToString(), CultureInfo.InvariantCulture);
            if (l is >= Int32.MinValue and <= Int32.MaxValue)
                return Symbol.Integer;
            if (l is > 0 and <= UInt32.MaxValue)
                return Symbol.UInteger;
            throw new PdfReaderException("Number exceeds integer range.");
        }

        public Symbol ScanKeyword()
        {
            token = new StringBuilder();
            char ch = currChar;
            // Scan token
            while (true)
            {
                if (char.IsLetter(ch))
                {
                    token.Append(ch);
                }
                else
                    break;
                ch = ScanNextChar();
            }

            // Check known tokens
            return token.ToString() switch
            {
                "obj" => symbol = Symbol.Obj,
                "endobj" => symbol = Symbol.EndObj,
                "null" => symbol = Symbol.Null,
                "true" or "false" => symbol = Symbol.Boolean,
                "R" => symbol = Symbol.R,
                "stream" => symbol = Symbol.BeginStream,
                "endstream" => symbol = Symbol.EndStream,
                "xref" => symbol = Symbol.XRef,
                "trailer" => symbol = Symbol.Trailer,
                "startxref" => symbol = Symbol.StartXRef,
                // Anything else is treated as a keyword. Samples are f or n in iref.
                _ => symbol = Symbol.Keyword,
            };
        }

        public Symbol ScanLiteralString()
        {
            Debug.Assert(currChar == Chars.ParenLeft);

#if DEBUG
      if (idxChar == 0x1b67aa)
        GetType();
#endif

            token = new StringBuilder();
            int parenLevel = 0;
            char ch = ScanNextChar();
            // Test UNICODE string
            if (ch == '\xFE' && nextChar == '\xFF')
            {
                // I'm not sure if the code is correct in any case.
                // ? Can a UNICODE character not start with ')' as hibyte
                // ? What about \# escape sequences

                // BUG: The code is not correct. I got a file containing the following sting:
                // (�����f��\r�F`:7.2.5 Acceptable daily intake \(ADI\) and other guideline levels)
                // It starts as unicode but ends as Ascii. No idea how to parse.
#if true
                //List<byte> bytes = new List<byte>();
                while (true)
                {
                SkipChar:
                    switch (ch)
                    {
                        case '(':  // is this possible in a Unicode string?
                            parenLevel++;
                            break;

                        case ')':
                            if (parenLevel == 0)
                            {
                                ScanNextChar();
                                return symbol = Symbol.String;
                            }
                            else
                                parenLevel--;
                            break;

                        case '\\':
                            {
                                ch = ScanNextChar();
                                switch (ch)
                                {
                                    case 'n':
                                        ch = Chars.LF;
                                        break;

                                    case 'r':
                                        ch = Chars.CR;
                                        break;

                                    case 't':
                                        ch = Chars.HT;
                                        break;

                                    case 'b':
                                        ch = Chars.BS;
                                        break;

                                    case 'f':
                                        ch = Chars.FF;
                                        break;

                                    case '(':
                                        ch = Chars.ParenLeft;
                                        break;

                                    case ')':
                                        ch = Chars.ParenRight;
                                        break;

                                    case '\\':
                                        ch = Chars.BackSlash;
                                        break;

                                    case Chars.LF:
                                        ch = ScanNextChar();
                                        goto SkipChar;

                                    default:
                                        if (Char.IsDigit(ch))
                                        {
                                            // Octal character code
                                            Debug.Assert(ch < '8', "Illegal octal digit.");
                                            int n = ch - '0';
                                            if (Char.IsDigit(nextChar))
                                            {
                                                Debug.Assert(nextChar < '8', "Illegal octal digit.");
                                                n = (n * 8) + ScanNextChar() - '0';
                                                if (Char.IsDigit(nextChar))
                                                {
                                                    Debug.Assert(nextChar < '8', "Illegal octal digit.");
                                                    n = (n * 8) + ScanNextChar() - '0';
                                                }
                                            }
                                            ch = (char)n;
                                        }
                                        else
                                        {
                                            //TODO
                                            Debug.Assert(false, "Not implemented; unknown escape character.");
                                        }
                                        break;
                                }
                                break;
                            }

                        // TODO ???
                        //case '#':
                        //  Debug.Assert(false, "Not yet implemented");
                        //  break;

                        default:
                            break;
                    }
                    token.Append(ch);
                    //chHi = ScanNextChar();
                    //if (chHi == ')')
                    //{
                    //  ScanNextChar();
                    //  return this.symbol = Symbol.String;
                    //}
                    //chLo = ScanNextChar();
                    //ch = (char)((int)chHi * 256 + (int)chLo);
                    ch = ScanNextChar();
                }
#else
        char chHi, chLo;
        ScanNextChar();
        chHi = ScanNextChar();
        if (chHi == ')')
        {
          // The empty unicode string...
          ScanNextChar();
          return this.symbol = Symbol.String;
        }
        chLo = ScanNextChar();
        ch = (char)((int)chHi * 256 + (int)chLo);
        while (true)
        {
        SkipChar:
          switch (ch)
          {
            case '(':
              parenLevel++;
              break;

            case ')':
              if (parenLevel == 0)
              {
                ScanNextChar();
                return this.symbol = Symbol.String;
              }
              else
                parenLevel--;
              break;

            case '\\':
              {
                // TODO: not sure that this is correct...
                ch = ScanNextChar();
                switch (ch)
                {
                  case 'n':
                    ch = Chars.LF;
                    break;

                  case 'r':
                    ch = Chars.CR;
                    break;

                  case 't':
                    ch = Chars.HT;
                    break;

                  case 'b':
                    ch = Chars.BS;
                    break;

                  case 'f':
                    ch = Chars.FF;
                    break;

                  case '(':
                    ch = Chars.ParenLeft;
                    break;

                  case ')':
                    ch = Chars.ParenRight;
                    break;

                  case '\\':
                    ch = Chars.BackSlash;
                    break;

                  case Chars.LF:
                    ch = ScanNextChar();
                    goto SkipChar;

                  default:
                    if (Char.IsDigit(ch))
                    {
                      // Octal character code
                      Debug.Assert(ch < '8', "Illegal octal digit.");
                      int n = ch - '0';
                      if (Char.IsDigit(this.nextChar))
                      {
                        Debug.Assert(this.nextChar < '8', "Illegal octal digit.");
                        n = n * 8 + ScanNextChar() - '0';
                        if (Char.IsDigit(this.nextChar))
                        {
                          Debug.Assert(this.nextChar < '8', "Illegal octal digit.");
                          n = n * 8 + ScanNextChar() - '0';
                        }
                      }
                      ch = (char)n;
                    }
                    else
                    {
                      //TODO
                      Debug.Assert(false, "Not implemented; unknown escape character.");
                    }
                    break;
                }
                break;
              }

            // TODO ???
            //case '#':
            //  Debug.Assert(false, "Not yet implemented");
            //  break;

            default:
              break;
          }
          this.token.Append(ch);
          chHi = ScanNextChar();
          if (chHi == ')')
          {
            ScanNextChar();
            return this.symbol = Symbol.String;
          }
          chLo = ScanNextChar();
          ch = (char)((int)chHi * 256 + (int)chLo);
        }
#endif
            }
            else
            {
                // 8-bit characters
                while (true)
                {
                SkipChar:
                    switch (ch)
                    {
                        case '(':
                            parenLevel++;
                            break;

                        case ')':
                            if (parenLevel == 0)
                            {
                                ScanNextChar();
                                return symbol = Symbol.String;
                            }
                            else
                                parenLevel--;
                            break;

                        case '\\':
                            {
                                ch = ScanNextChar();
                                switch (ch)
                                {
                                    case 'n':
                                        ch = Chars.LF;
                                        break;

                                    case 'r':
                                        ch = Chars.CR;
                                        break;

                                    case 't':
                                        ch = Chars.HT;
                                        break;

                                    case 'b':
                                        ch = Chars.BS;
                                        break;

                                    case 'f':
                                        ch = Chars.FF;
                                        break;

                                    case '(':
                                        ch = Chars.ParenLeft;
                                        break;

                                    case ')':
                                        ch = Chars.ParenRight;
                                        break;

                                    case '\\':
                                        ch = Chars.BackSlash;
                                        break;

                                    case Chars.LF:
                                        ch = ScanNextChar();
                                        goto SkipChar;

                                    default:
                                        if (char.IsDigit(ch))
                                        {
                                            // Octal character code
                                            Debug.Assert(ch < '8', "Illegal octal digit.");
                                            int n = ch - '0';
                                            if (Char.IsDigit(nextChar))
                                            {
                                                Debug.Assert(nextChar < '8', "Illegal octal digit.");
                                                n = (n * 8) + ScanNextChar() - '0';
                                                if (Char.IsDigit(nextChar))
                                                {
                                                    Debug.Assert(nextChar < '8', "Illegal octal digit.");
                                                    n = (n * 8) + ScanNextChar() - '0';
                                                }
                                            }
                                            ch = (char)n;
                                        }
                                        else
                                        {
                                            //TODO
                                            Debug.Assert(false, "Not implemented; unknown escape character.");
                                        }
                                        break;
                                }
                                break;
                            }

                        // TODO ???
                        //case '#':
                        //  Debug.Assert(false, "Not yet implemented");
                        //  break;

                        default:
                            break;
                    }
                    token.Append(ch);
                    ch = ScanNextChar();
                }
            }
            //      Debug.Assert(false, "Must never come here:");
        }

        public Symbol ScanHexadecimalString()
        {
            Debug.Assert(currChar == Chars.Less);

            token = new StringBuilder();
            char[] hex = new char[2];
            ScanNextChar();
            while (true)
            {
                MoveToNonWhiteSpace();
                if (currChar == '>')
                {
                    ScanNextChar();
                    break;
                }
                if (char.IsLetterOrDigit(currChar))
                {
                    hex[0] = char.ToUpper(currChar);
                    hex[1] = char.ToUpper(nextChar);
                    int ch = int.Parse(new string(hex), NumberStyles.AllowHexSpecifier);
                    token.Append(Convert.ToChar(ch));
                    ScanNextChar();
                    ScanNextChar();
                }
            }
            string chars = token.ToString();
            int count = chars.Length;
            if (count > 2 && chars[0] == (char)0xFE && chars[1] == (char)0xFF)
            {
                Debug.Assert(count % 2 == 0);
                token.Length = 0;
                for (int idx = 2; idx < count; idx += 2)
                    token.Append((char)((chars[idx] * 256) + chars[idx + 1]));
            }
            return symbol = Symbol.HexString;
        }

        /// <summary>
        /// Move current position one character further in PDF stream.
        /// </summary>
        internal char ScanNextChar()
        {
            if (pdfLength <= idxChar)
            {
                currChar = Chars.EOF;
                nextChar = Chars.EOF;
            }
            else
            {
                currChar = nextChar;
                nextChar = (char)pdf.ReadByte();
                idxChar++;
                if (currChar == Chars.CR)
                {
                    if (nextChar == Chars.LF)
                    {
                        // Treat CR LF as LF
                        currChar = nextChar;
                        nextChar = (char)pdf.ReadByte();
                        idxChar++;
                    }
                    else
                    {
                        // Treat single CR as LF
                        currChar = Chars.LF;
                    }
                }
            }
            return currChar;
        }

        /// <summary>
        /// Appends current character to the token and reads next one.
        /// </summary>
        internal char AppendAndScanNextChar()
        {
            token.Append(currChar);
            return ScanNextChar();
        }

        /// <summary>
        /// If the current character is not a white space, the function immediately returns it.
        /// Otherwise the PDF cursor is moved forward to the first non-white space or EOF.
        /// White spaces are NUL, HT, LF, FF, CR, and SP.
        /// </summary>
        public char MoveToNonWhiteSpace()
        {
            while (currChar != Chars.EOF)
            {
                switch (currChar)
                {
                    case Chars.NUL:
                    case Chars.HT:
                    case Chars.LF:
                    case Chars.FF:
                    case Chars.CR:
                    case Chars.SP:
                        ScanNextChar();
                        break;

                    default:
                        return currChar;
                }
            }
            return currChar;
        }

        /// <summary>
        /// Gets the current symbol.
        /// </summary>
        public Symbol Symbol
        {
            get { return symbol; }
            set { symbol = value; }
        }

        /// <summary>
        /// Gets the current token.
        /// </summary>
        internal string Token
        {
            get { return token.ToString(); }
        }

        /// <summary>
        /// Interprets current token as boolean literal.
        /// </summary>
        internal bool TokenToBoolean
        {
            get
            {
                Debug.Assert(token.ToString() == "true" || token.ToString() == "false");
                return token.ToString()[0] == 't';
            }
        }

        /// <summary>
        /// Interprets current token as integer literal.
        /// </summary>
        internal int TokenToInteger
        {
            get
            {
                //Debug.Assert(this.token.ToString().IndexOf('.') == -1);
                return Int32.Parse(token.ToString(), CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Interprets current token as unsigned integer literal.
        /// </summary>
        internal uint TokenToUInteger
        {
            get
            {
                //Debug.Assert(this.token.ToString().IndexOf('.') == -1);
                return UInt32.Parse(token.ToString(), CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Interpret current token as real or integer literal.
        /// </summary>
        internal double TokenToReal
        {
            get { return double.Parse(token.ToString(), CultureInfo.InvariantCulture); }
        }

        /// <summary>
        /// Indicates whether the specified character is a PDF white-space character.
        /// </summary>
        internal static bool IsWhiteSpace(char ch)
        {
            return ch switch
            {
                // 0 Null
                Chars.NUL or Chars.HT or Chars.LF or Chars.FF or Chars.CR or Chars.SP => true,
                _ => false,
            };
        }

        /// <summary>
        /// Indicates whether the specified character is a PDF delimiter character.
        /// </summary>
        internal static bool IsDelimiter(char ch)
        {
            return ch switch
            {
                '(' or ')' or '<' or '>' or '[' or ']' or '{' or '}' or '/' or '%' => true,
                _ => false,
            };
        }

        public int PdfLength
        {
            get { return pdfLength; }
        }

        private readonly int pdfLength;
        private int idxChar;
        private char currChar;
        private char nextChar;
        private StringBuilder token;
        private Symbol symbol = Symbol.None;

        private readonly Stream pdf;
    }
}
