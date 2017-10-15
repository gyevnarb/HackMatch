using System.Collections.Generic;

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
    }
}