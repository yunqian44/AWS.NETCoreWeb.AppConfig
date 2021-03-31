using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWS.NETCoreWeb.AppConfig.Extension
{
    public static class MemoryStreamExtension
    {
        public static string DecodeMemoryStreamToString(this MemoryStream content)
        {
            string result = string.Empty;
            int count;
            UnicodeEncoding uniEncoding = new UnicodeEncoding();

            using (MemoryStream memoryStream = content)
            {
                memoryStream.Seek(0, SeekOrigin.Begin);
                byte[] byteArray = new byte[memoryStream.Length];
                count = memoryStream.Read(byteArray, 0, 20);

                while (count < memoryStream.Length)
                {
                    byteArray[count++] = Convert.ToByte(memoryStream.ReadByte());
                }

                char[] charArray = new char[uniEncoding.GetCharCount(byteArray, 0, count)];
                uniEncoding.GetDecoder().GetChars(byteArray, 0, count, charArray, 0);


                byte[] decodedBytes = Convert.FromBase64String(Convert.ToBase64String(System.Text.Encoding.Unicode.GetBytes(charArray)));
                result = System.Text.Encoding.UTF8.GetString(decodedBytes);
            }
            if (result.EndsWith("\n\0\0"))
            {
                return result.Replace("\n\0\0", "\n}");
            }
            else
            {
                return result;
            }
        }
    }
}
