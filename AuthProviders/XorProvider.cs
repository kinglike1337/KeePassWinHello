﻿using System;
using System.Linq;
using System.Windows.Forms;

namespace KeePassWinHello
{
    class XorProvider : IAuthProvider, IWin32Window
    {
        private const byte _entropy = 42;

        public string Message { get; set; }
        public IntPtr Handle { get; set; }

        public byte[] Encrypt(byte[] data)
        {
            return data.Select(x => (byte)(x ^ _entropy)).ToArray();
        }

        public byte[] PromptToDecrypt(byte[] data)
        {
            var dlgRslt = MessageBox.Show(this, Message, "Test", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dlgRslt == DialogResult.OK)
            {
                return Encrypt(data);
            }
            else
            {
                throw new UnauthorizedAccessException("Canceled");
            }
        }
    }
}
