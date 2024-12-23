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
using System.Diagnostics;
using System.Reflection;

namespace PdfSharp.Pdf
{
    /// <summary>
    /// Hold information about the value of a key in a dictionary. This information is used to create
    /// and interpret this value.
    /// </summary>
    internal sealed class KeyDescriptor
    {
        /// <summary>
        /// Initializes a new instance of KeyDescriptor from the specified attribute during a KeysMeta
        /// initializes itself using reflection.
        /// </summary>
        public KeyDescriptor(KeyInfoAttribute attribute)
        {
            version = attribute.Version;
            keyType = attribute.KeyType;
            fixedValue = attribute.FixedValue;
            objectType = attribute.ObjectType;

            if (version == "")
                version = "1.0";
        }

        /// <summary>
        /// Gets or sets the PDF version starting with the availability of the described key.
        /// </summary>
        public string Version
        {
            get { return version; }
            set { version = value; }
        }

        private string version;

        public KeyType KeyType
        {
            get { return keyType; }
            set { keyType = value; }
        }

        private KeyType keyType;

        public string KeyValue
        {
            get { return keyValue; }
            set { keyValue = value; }
        }

        private string keyValue;

        public string FixedValue
        {
            get { return fixedValue; }
        }

        private readonly string fixedValue;

        public Type ObjectType
        {
            get { return objectType; }
            set { objectType = value; }
        }

        private Type objectType;

        public bool CanBeIndirect
        {
            get { return (keyType & KeyType.MustNotBeIndirect) == 0; }
        }

        /// <summary>
        /// Returns the type of the object to be created as value for the described key.
        /// </summary>
        public Type GetValueType()
        {
            Type type = objectType;
            if (type == null)
            {
                // If we have no ObjectType specified, use the KeyType enumeration.
                switch (keyType & KeyType.TypeMask)
                {
                    case KeyType.Name:
                        type = typeof(PdfName);
                        break;

                    case KeyType.String:
                        type = typeof(PdfString);
                        break;

                    case KeyType.Boolean:
                        type = typeof(PdfBoolean);
                        break;

                    case KeyType.Integer:
                        type = typeof(PdfInteger);
                        break;

                    case KeyType.Real:
                        type = typeof(PdfReal);
                        break;

                    case KeyType.Date:
                        type = typeof(PdfDate);
                        break;

                    case KeyType.Rectangle:
                        type = typeof(PdfRectangle);
                        break;

                    case KeyType.Array:
                        type = typeof(PdfArray);
                        break;

                    case KeyType.Dictionary:
                    case KeyType.Stream:
                        type = typeof(PdfDictionary);
                        break;

                    // The following types are not yet used

                    case KeyType.NumberTree:
                        throw new NotImplementedException("KeyType.NumberTree");

                    case KeyType.NameOrArray:
                        throw new NotImplementedException("KeyType.NameOrArray");

                    case KeyType.ArrayOrDictionary:
                        throw new NotImplementedException("KeyType.ArrayOrDictionary");

                    case KeyType.StreamOrArray:
                        throw new NotImplementedException("KeyType.StreamOrArray");

                    case KeyType.ArrayOrNameOrString:
                        throw new NotImplementedException("KeyType.ArrayOrNameOrString");

                    default:
                        Debug.Assert(false, "Invalid KeyType: " + keyType);
                        break;
                }
            }
            return type;
        }
    }

    /// <summary>
    /// Contains meta information about all keys of a PDF dictionary.
    /// </summary>
    internal class DictionaryMeta
    {
        public DictionaryMeta(Type type)
        {
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
            foreach (FieldInfo field in fields)
            {
                object[] attributes = field.GetCustomAttributes(typeof(KeyInfoAttribute), false);
                if (attributes.Length == 1)
                {
                    KeyInfoAttribute attribute = (KeyInfoAttribute)attributes[0];
                    KeyDescriptor descriptor = new(attribute);
                    descriptor.KeyValue = (string)field.GetValue(null);
                    keyDescriptors[descriptor.KeyValue] = descriptor;
                }
            }
        }

        public KeyDescriptor this[string key]
        {
            get { return keyDescriptors[key]; }
        }

        private readonly Dictionary<string, KeyDescriptor> keyDescriptors = [];
    }
}
