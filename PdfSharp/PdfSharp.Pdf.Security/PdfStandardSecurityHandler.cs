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

using PdfSharp.Pdf.Advanced;
using PdfSharp.Pdf.Internal;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;

namespace PdfSharp.Pdf.Security
{
    /// <summary>
    /// Represents the standard PDF security handler.
    /// </summary>
    public sealed class PdfStandardSecurityHandler : PdfSecurityHandler
    {
        internal PdfStandardSecurityHandler(PdfDocument document)
          : base(document)
        { }

        internal PdfStandardSecurityHandler(PdfDictionary dict)
          : base(dict)
        { }

        /// <summary>
        /// Sets the user password of the document. Setting a password automatically sets the
        /// PdfDocumentSecurityLevel to PdfDocumentSecurityLevel.Encrypted128Bit if its current
        /// value is PdfDocumentSecurityLevel.None.
        /// </summary>
        public string UserPassword
        {
            set
            {
                if (document.securitySettings.DocumentSecurityLevel == PdfDocumentSecurityLevel.None)
                    document.securitySettings.DocumentSecurityLevel = PdfDocumentSecurityLevel.Encrypted128Bit;
                userPassword = value;
            }
        }
        internal string userPassword;

        /// <summary>
        /// Sets the owner password of the document. Setting a password automatically sets the
        /// PdfDocumentSecurityLevel to PdfDocumentSecurityLevel.Encrypted128Bit if its current
        /// value is PdfDocumentSecurityLevel.None.
        /// </summary>
        public string OwnerPassword
        {
            set
            {
                if (document.securitySettings.DocumentSecurityLevel == PdfDocumentSecurityLevel.None)
                    document.securitySettings.DocumentSecurityLevel = PdfDocumentSecurityLevel.Encrypted128Bit;
                ownerPassword = value;
            }
        }
        internal string ownerPassword;

        /// <summary>
        /// Gets or sets the user access permission represented as an integer in the P key.
        /// </summary>
        internal PdfUserAccessPermission Permission
        {
            get
            {
                PdfUserAccessPermission permission = (PdfUserAccessPermission)Elements.GetInteger(Keys.P);
                if ((int)permission == 0)
                    permission = PdfUserAccessPermission.PermitAll;
                return permission;
            }
            set { Elements.SetInteger(Keys.P, (int)value); }
        }

        /// <summary>
        /// Encrypts the whole document.
        /// </summary>
        public void EncryptDocument()
        {
            foreach (PdfReference iref in document.irefTable.AllReferences)
            {
                if (!ReferenceEquals(iref.Value, this))
                    EncryptObject(iref.Value);
            }
        }

        /// <summary>
        /// Encrypts an indirect object.
        /// </summary>
        internal void EncryptObject(PdfObject value)
        {
            Debug.Assert(value.Reference != null);

            SetHashKey(value.ObjectID);
            if (value is PdfDictionary dict)
                EncryptDictionary(dict);
            else if (value is PdfArray array)
                EncryptArray(array);
            else if (value is PdfStringObject str)
            {
                if (str.Length != 0)
                {
                    byte[] bytes = str.EncryptionValue;
                    PrepareKey();
                    EncryptRC4(bytes);
                    str.EncryptionValue = bytes;
                }
            }
        }

        /// <summary>
        /// Encrypts a dictionary.
        /// </summary>
        private void EncryptDictionary(PdfDictionary dict)
        {
            foreach (KeyValuePair<string, PdfItem> item in dict.Elements)
            {
                if (item.Value is PdfString value1)
                    EncryptString(value1);
                else if (item.Value is PdfDictionary value2)
                    EncryptDictionary(value2);
                else if (item.Value is PdfArray value3)
                    EncryptArray(value3);
            }
            if (dict.Stream != null)
            {
                byte[] bytes = dict.Stream.Value;
                if (bytes.Length != 0)
                {
                    PrepareKey();
                    EncryptRC4(bytes);
                    dict.Stream.Value = bytes;
                }
            }
        }

        /// <summary>
        /// Encrypts an array.
        /// </summary>
        private void EncryptArray(PdfArray array)
        {
            int count = array.Elements.Count;
            for (int idx = 0; idx < count; idx++)
            {
                PdfItem item = array.Elements[idx];
                if (item is PdfString value1)
                    EncryptString(value1);
                else if (item is PdfDictionary value2)
                    EncryptDictionary(value2);
                else if (item is PdfArray value3)
                    EncryptArray(value3);
            }
        }

        /// <summary>
        /// Encrypts a string.
        /// </summary>
        private void EncryptString(PdfString value)
        {
            if (value.Length != 0)
            {
                byte[] bytes = value.EncryptionValue;
                PrepareKey();
                EncryptRC4(bytes);
                value.EncryptionValue = bytes;
            }
        }

        /// <summary>
        /// Encrypts an array.
        /// </summary>
        internal byte[] EncryptBytes(byte[] bytes)
        {
            if (bytes != null && bytes.Length != 0)
            {
                PrepareKey();
                EncryptRC4(bytes);
            }
            return bytes;
        }

        #region Encryption Algorithms

        /// <summary>
        /// Checks the password.
        /// </summary>
        /// <param name="inputPassword">Password or null if no password is provided.</param>
        public PasswordValidity ValidatePassword(string inputPassword)
        {
            // We can handle 40 and 128 bit standard encryption
            string filter = Elements.GetName(PdfSecurityHandler.Keys.Filter);
            int v = Elements.GetInteger(PdfSecurityHandler.Keys.V);
            if (filter != "/Standard" || !(v >= 1 && v <= 3))
                throw new PdfReaderException(PSSR.UnknownEncryption);

            byte[] documentID = PdfEncoders.RawEncoding.GetBytes(Owner.Internals.FirstDocumentID);
            byte[] oValue = PdfEncoders.RawEncoding.GetBytes(Elements.GetString(Keys.O));
            byte[] uValue = PdfEncoders.RawEncoding.GetBytes(Elements.GetString(Keys.U));
            int pValue = Elements.GetInteger(Keys.P);
            int rValue = Elements.GetInteger(Keys.R);

            inputPassword ??= "";

            bool strongEncryption = rValue == 3;
            int keyLength = strongEncryption ? 16 : 32;

#if true
            // Try owner password first
            byte[] password = PdfEncoders.RawEncoding.GetBytes(inputPassword);
            InitWidhOwnerPassword(documentID, inputPassword, oValue, pValue, strongEncryption);
            if (EqualsKey(uValue, keyLength))
            {
                document.SecuritySettings.hasOwnerPermissions = true;
                return PasswordValidity.OwnerPassword;
            }
            document.SecuritySettings.hasOwnerPermissions = false;

            // Now try user password
            password = PdfEncoders.RawEncoding.GetBytes(inputPassword);
            InitWidhUserPassword(documentID, inputPassword, oValue, pValue, strongEncryption);
            if (!EqualsKey(uValue, keyLength))
                return PasswordValidity.Invalid;
            return PasswordValidity.UserPassword;
#else
      password = PdfEncoders.RawEncoding.GetBytes(inputPassword);
      InitWidhUserPassword(documentID, inputPassword, oValue, pValue, strongEncryption);

      this.document.SecuritySettings.hasOwnerPermissions = false;

      if (!EqualsKey(uValue, keyLength))
      {
        password = PdfEncoders.RawEncoding.GetBytes(inputPassword);

        // Compare owner password
        InitWidhOwnerPassword(documentID, inputPassword, oValue, pValue, strongEncryption);

        if (!EqualsKey(uValue, keyLength))
        {
          //Compare user password
          InitWidhUserPassword(documentID, inputPassword, oValue, pValue, strongEncryption);
          if (!EqualsKey(uValue, keyLength))
            return 0;
          return 1;
        }
        this.document.SecuritySettings.hasOwnerPermissions = true;
        return 2;
      }
      return 1;
#endif
        }

        /// <summary>
        /// Pads a password to a 32 byte array.
        /// </summary>
        private static byte[] PadPassword(string password)
        {
            byte[] padded = new byte[32];
            if (password == null)
                Array.Copy(passwordPadding, 0, padded, 0, 32);
            else
            {
                int length = password.Length;
                Array.Copy(PdfEncoders.RawEncoding.GetBytes(password), 0, padded, 0, Math.Min(length, 32));
                if (length < 32)
                    Array.Copy(passwordPadding, 0, padded, length, 32 - length);
            }
            return padded;
        }

        private static readonly byte[] passwordPadding = // 32 bytes password padding defined by Adobe
        [
      0x28, 0xBF, 0x4E, 0x5E, 0x4E, 0x75, 0x8A, 0x41, 0x64, 0x00, 0x4E, 0x56, 0xFF, 0xFA, 0x01, 0x08,
      0x2E, 0x2E, 0x00, 0xB6, 0xD0, 0x68, 0x3E, 0x80, 0x2F, 0x0C, 0xA9, 0xFE, 0x64, 0x53, 0x69, 0x7A,
    ];

        /// <summary>
        /// Generates the user key based on the padded user password.
        /// </summary>
        private void InitWidhUserPassword(byte[] documentID, string userPassword, byte[] ownerKey, int permissions, bool strongEncryption)
        {
            InitEncryptionKey(documentID, PadPassword(userPassword), ownerKey, permissions, strongEncryption);
            SetupUserKey(documentID);
        }

        /// <summary>
        /// Generates the user key based on the padded owner password.
        /// </summary>
        private void InitWidhOwnerPassword(byte[] documentID, string ownerPassword, byte[] ownerKey, int permissions, bool strongEncryption)
        {
            byte[] userPad = ComputeOwnerKey(ownerKey, PadPassword(ownerPassword), strongEncryption);
            InitEncryptionKey(documentID, userPad, ownerKey, permissions, strongEncryption);
            SetupUserKey(documentID);
        }

        /// <summary>
        /// Computes the padded user password from the padded owner password.
        /// </summary>
        private byte[] ComputeOwnerKey(byte[] userPad, byte[] ownerPad, bool strongEncryption)
        {
            byte[] ownerKey = new byte[32];
#if !SILVERLIGHT
            byte[] digest = md5.ComputeHash(ownerPad);
            if (strongEncryption)
            {
                byte[] mkey = new byte[16];
                // Hash the pad 50 times
                for (int idx = 0; idx < 50; idx++)
                    digest = md5.ComputeHash(digest);
                Array.Copy(userPad, 0, ownerKey, 0, 32);
                // Encrypt the key
                for (int i = 0; i < 20; i++)
                {
                    for (int j = 0; j < mkey.Length; ++j)
                        mkey[j] = (byte)(digest[j] ^ i);
                    PrepareRC4Key(mkey);
                    EncryptRC4(ownerKey);
                }
            }
            else
            {
                PrepareRC4Key(digest, 0, 5);
                EncryptRC4(userPad, ownerKey);
            }
#endif
            return ownerKey;
        }

        /// <summary>
        /// Computes the encryption key.
        /// </summary>
        private void InitEncryptionKey(byte[] documentID, byte[] userPad, byte[] ownerKey, int permissions, bool strongEncryption)
        {
#if !SILVERLIGHT
            this.ownerKey = ownerKey;
            encryptionKey = new byte[strongEncryption ? 16 : 5];

            md5.Initialize();
            md5.TransformBlock(userPad, 0, userPad.Length, userPad, 0);
            md5.TransformBlock(ownerKey, 0, ownerKey.Length, ownerKey, 0);

            // Split permission into 4 bytes
            byte[] permission =
            [
                (byte)permissions,
                (byte)(permissions >> 8),
                (byte)(permissions >> 16),
                (byte)(permissions >> 24),
            ];
            md5.TransformBlock(permission, 0, 4, permission, 0);
            md5.TransformBlock(documentID, 0, documentID.Length, documentID, 0);
            md5.TransformFinalBlock(permission, 0, 0);
            byte[] digest = md5.Hash;
            md5.Initialize();
            // Create the hash 50 times (only for 128 bit)
            if (encryptionKey.Length == 16)
            {
                for (int idx = 0; idx < 50; idx++)
                {
                    digest = md5.ComputeHash(digest);
                    md5.Initialize();
                }
            }
            Array.Copy(digest, 0, encryptionKey, 0, encryptionKey.Length);
#endif
        }

        /// <summary>
        /// Computes the user key.
        /// </summary>
        private void SetupUserKey(byte[] documentID)
        {
#if !SILVERLIGHT
            if (encryptionKey.Length == 16)
            {
                md5.TransformBlock(passwordPadding, 0, passwordPadding.Length, passwordPadding, 0);
                md5.TransformFinalBlock(documentID, 0, documentID.Length);
                byte[] digest = md5.Hash;
                md5.Initialize();
                Array.Copy(digest, 0, userKey, 0, 16);
                for (int idx = 16; idx < 32; idx++)
                    userKey[idx] = 0;
                //Encrypt the key
                for (int i = 0; i < 20; i++)
                {
                    for (int j = 0; j < encryptionKey.Length; j++)
                        digest[j] = (byte)(encryptionKey[j] ^ i);
                    PrepareRC4Key(digest, 0, encryptionKey.Length);
                    EncryptRC4(userKey, 0, 16);
                }
            }
            else
            {
                PrepareRC4Key(encryptionKey);
                EncryptRC4(passwordPadding, userKey);
            }
#endif
        }

        /// <summary>
        /// Prepare the encryption key.
        /// </summary>
        private void PrepareKey()
        {
            PrepareRC4Key(key, 0, keySize);
        }

        /// <summary>
        /// Prepare the encryption key.
        /// </summary>
        private void PrepareRC4Key(byte[] key)
        {
            PrepareRC4Key(key, 0, key.Length);
        }

        /// <summary>
        /// Prepare the encryption key.
        /// </summary>
        private void PrepareRC4Key(byte[] key, int offset, int length)
        {
            int idx1 = 0;
            int idx2 = 0;
            for (int idx = 0; idx < 256; idx++)
                state[idx] = (byte)idx;
            byte tmp;
            for (int idx = 0; idx < 256; idx++)
            {
                idx2 = (key[idx1 + offset] + state[idx] + idx2) & 255;
                tmp = state[idx];
                state[idx] = state[idx2];
                state[idx2] = tmp;
                idx1 = (idx1 + 1) % length;
            }
        }

        /// <summary>
        /// Encrypts the data.
        /// </summary>
        private void EncryptRC4(byte[] data)
        {
            EncryptRC4(data, 0, data.Length, data);
        }

        /// <summary>
        /// Encrypts the data.
        /// </summary>
        private void EncryptRC4(byte[] data, int offset, int length)
        {
            EncryptRC4(data, offset, length, data);
        }

        /// <summary>
        /// Encrypts the data.
        /// </summary>
        private void EncryptRC4(byte[] inputData, byte[] outputData)
        {
            EncryptRC4(inputData, 0, inputData.Length, outputData);
        }

        /// <summary>
        /// Encrypts the data.
        /// </summary>
        private void EncryptRC4(byte[] inputData, int offset, int length, byte[] outputData)
        {
            length += offset;
            int x = 0, y = 0;
            byte b;
            for (int idx = offset; idx < length; idx++)
            {
                x = (x + 1) & 255;
                y = (state[x] + y) & 255;
                b = state[x];
                state[x] = state[y];
                state[y] = b;
                outputData[idx] = (byte)(inputData[idx] ^ state[(state[x] + state[y]) & 255]);
            }
        }

        /// <summary>
        /// Checks whether the calculated key correct.
        /// </summary>
        private bool EqualsKey(byte[] value, int length)
        {
            for (int idx = 0; idx < length; idx++)
            {
                if (userKey[idx] != value[idx])
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Set the hash key for the specified object.
        /// </summary>
        internal void SetHashKey(PdfObjectID id)
        {
#if !SILVERLIGHT
            byte[] objectId = new byte[5];
            md5.Initialize();
            // Split the object number and generation
            objectId[0] = (byte)id.ObjectNumber;
            objectId[1] = (byte)(id.ObjectNumber >> 8);
            objectId[2] = (byte)(id.ObjectNumber >> 16);
            objectId[3] = (byte)id.GenerationNumber;
            objectId[4] = (byte)(id.GenerationNumber >> 8);
            md5.TransformBlock(encryptionKey, 0, encryptionKey.Length, encryptionKey, 0);
            md5.TransformFinalBlock(objectId, 0, objectId.Length);
            key = md5.Hash;
            md5.Initialize();
            keySize = encryptionKey.Length + 5;
            if (keySize > 16)
                keySize = 16;
#endif
        }

        /// <summary>
        /// Prepares the security handler for encrypting the document.
        /// </summary>
        public void PrepareEncryption()
        {
#if !SILVERLIGHT
            Debug.Assert(document.securitySettings.DocumentSecurityLevel != PdfDocumentSecurityLevel.None);
            int permissions = (int)Permission;
            bool strongEncryption = document.securitySettings.DocumentSecurityLevel == PdfDocumentSecurityLevel.Encrypted128Bit;

            PdfInteger vValue;
            PdfInteger length;
            PdfInteger rValue;

            if (strongEncryption)
            {
                vValue = new PdfInteger(2);
                length = new PdfInteger(128);
                rValue = new PdfInteger(3);
            }
            else
            {
                vValue = new PdfInteger(1);
                length = new PdfInteger(40);
                rValue = new PdfInteger(2);
            }

            if (String.IsNullOrEmpty(userPassword))
                userPassword = "";
            // Use user password twice if no owner password provided.
            if (String.IsNullOrEmpty(ownerPassword))
                ownerPassword = userPassword;

            // Correct permission bits
            permissions |= (int)(strongEncryption ? (uint)0xfffff0c0 : (uint)0xffffffc0);
            permissions &= unchecked((int)0xfffffffc);

            PdfInteger pValue = new(permissions);

            Debug.Assert(ownerPassword.Length > 0, "Empty owner password.");
            byte[] userPad = PadPassword(userPassword);
            byte[] ownerPad = PadPassword(ownerPassword);

            md5.Initialize();
            ownerKey = ComputeOwnerKey(userPad, ownerPad, strongEncryption);
            byte[] documentID = PdfEncoders.RawEncoding.GetBytes(document.Internals.FirstDocumentID);
            InitWidhUserPassword(documentID, userPassword, ownerKey, permissions, strongEncryption);

            PdfString oValue = new(PdfEncoders.RawEncoding.GetString(ownerKey));
            PdfString uValue = new(PdfEncoders.RawEncoding.GetString(userKey));

            Elements[PdfSecurityHandler.Keys.Filter] = new PdfName("/Standard");
            Elements[PdfSecurityHandler.Keys.V] = vValue;
            Elements[PdfSecurityHandler.Keys.Length] = length;
            Elements[Keys.R] = rValue;
            Elements[Keys.O] = oValue;
            Elements[Keys.U] = uValue;
            Elements[Keys.P] = pValue;
#endif
        }

        /// <summary>
        /// The global encryption key.
        /// </summary>
        private byte[] encryptionKey;

#if !SILVERLIGHT
        /// <summary>
        /// The message digest algorithm MD5.
        /// </summary>
        private readonly MD5 md5 = MD5.Create();
#endif

        /// <summary>
        /// Bytes used for RC4 encryption.
        /// </summary>
        private readonly byte[] state = new byte[256];

        /// <summary>
        /// The encryption key for the owner.
        /// </summary>
        private byte[] ownerKey = new byte[32];

        /// <summary>
        /// The encryption key for the user.
        /// </summary>
        private readonly byte[] userKey = new byte[32];

        /// <summary>
        /// The encryption key for a particular object/generation.
        /// </summary>
        private byte[] key;

        /// <summary>
        /// The encryption key length for a particular object/generation.
        /// </summary>
        private int keySize;

        #endregion

        internal override void WriteObject(PdfWriter writer)
        {
            // Don't encypt myself
            PdfStandardSecurityHandler securityHandler = writer.SecurityHandler;
            writer.SecurityHandler = null;
            base.WriteObject(writer);
            writer.SecurityHandler = securityHandler;
        }

        #region Keys
        /// <summary>
        /// Predefined keys of this dictionary.
        /// </summary>
        internal sealed new class Keys : PdfSecurityHandler.Keys
        {
            /// <summary>
            /// (Required) A number specifying which revision of the standard security handler
            /// should be used to interpret this dictionary:
            /// � 2 if the document is encrypted with a V value less than 2 and does not have any of
            ///   the access permissions set (by means of the P entry, below) that are designated 
            ///   "Revision 3 or greater".
            /// � 3 if the document is encrypted with a V value of 2 or 3, or has any "Revision 3 or 
            ///   greater" access permissions set.
            /// � 4 if the document is encrypted with a V value of 4
            /// </summary>
            [KeyInfo(KeyType.Integer | KeyType.Required)]
            public const string R = "/R";

            /// <summary>
            /// (Required) A 32-byte string, based on both the owner and user passwords, that is
            /// used in computing the encryption key and in determining whether a valid owner
            /// password was entered.
            /// </summary>
            [KeyInfo(KeyType.String | KeyType.Required)]
            public const string O = "/O";

            /// <summary>
            /// (Required) A 32-byte string, based on the user password, that is used in determining
            /// whether to prompt the user for a password and, if so, whether a valid user or owner 
            /// password was entered.
            /// </summary>
            [KeyInfo(KeyType.String | KeyType.Required)]
            public const string U = "/U";

            /// <summary>
            /// (Required) A set of flags specifying which operations are permitted when the document
            /// is opened with user access.
            /// </summary>
            [KeyInfo(KeyType.Integer | KeyType.Required)]
            public const string P = "/P";

            /// <summary>
            /// (Optional; meaningful only when the value of V is 4; PDF 1.5) Indicates whether
            /// the document-level metadata stream is to be encrypted. Applications should respect this value.
            /// Default value: true.
            /// </summary>
            [KeyInfo(KeyType.Boolean | KeyType.Optional)]
            public const string EncryptMetadata = "/EncryptMetadata";

            /// <summary>
            /// Gets the KeysMeta for these keys.
            /// </summary>
            public static DictionaryMeta Meta
            {
                get
                {
                    meta ??= CreateMeta(typeof(Keys));
                    return meta;
                }
            }

            private static DictionaryMeta meta;
        }

        /// <summary>
        /// Gets the KeysMeta of this dictionary type.
        /// </summary>
        internal override DictionaryMeta Meta
        {
            get { return Keys.Meta; }
        }
        #endregion
    }
}
