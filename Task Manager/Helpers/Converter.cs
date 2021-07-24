using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Manager.Helpers
{
    public static class Converter
    {
        private static char ToHexDigit(int i)
        {
            if (i < 10)
                return (char)(i + '0');
            return (char)(i - 10 + 'A');
        }
        public static string ToHexString(byte[] bytes)
        {
            var chars = new char[bytes.Length * 2 + 2];

            chars[0] = '0';
            chars[1] = 'x';

            for (int i = 0; i < bytes.Length; i++)
            {
                chars[2 * i + 2] = ToHexDigit(bytes[i] / 16);
                chars[2 * i + 3] = ToHexDigit(bytes[i] % 16);
            }

            return new string(chars);
        }

        public static byte[] ToByteArray(string str)
        {
            return Convert.FromBase64String(str);
        }

        public static string ToBase64(Stream ms)
        {
            BinaryReader br = new BinaryReader(ms);
            byte[] streamBytes = br.ReadBytes((int)ms.Length);
            return Convert.ToBase64String(streamBytes);
        }
    }
}
