using System.Collections.Generic;
using System.IO;
using System;

namespace HackMatch
{
    public static class Utils
    {
        public static string DictionaryToString<K, V>(Dictionary<K, V> dict)
        {
            string ret = "";
            int i = 1;
            foreach (var item in dict)
            {
                ret += $"{i++}. {item.Key}: {item.Value}\n";
            }
            return ret;
        }

        public static byte[] ToByteArray(this Stream stream)
        {
            stream.Position = 0;
            byte[] buffer = new byte[stream.Length];
            for (int totalBytesCopied = 0; totalBytesCopied < stream.Length;)
                totalBytesCopied += stream.Read(buffer, totalBytesCopied, Convert.ToInt32(stream.Length) - totalBytesCopied);
            return buffer;
        }
    }
}