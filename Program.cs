using System;
using System.Collections.Generic;
using System.Text;

namespace Palindromes
{
    class Program
    {
        // no test, no performance 
        static void Main(string[] args)
        {
            List<DateTime> palindroms = new List<DateTime>();
            DateTime start = new DateTime(1000, 01, 01);
            DateTime stop = new DateTime(9999, 12, 31);
            while (start < stop)
            {
                if (IsPalindrom(start))
                    palindroms.Add(start);
                start = start.AddDays(1);
            }

            List<KeyValuePair<DateTime, DateTime>> anagrams = new List<KeyValuePair<DateTime, DateTime>>();
            for(int i = 0; i < palindroms.Count / 2; ++i)
            {
                for (int j = i + 1; j < palindroms.Count; ++j)
                {
                    if (AreAnagram(ToString(palindroms[i]), ToString(palindroms[j])))
                        anagrams.Add(new KeyValuePair<DateTime, DateTime>(palindroms[i], palindroms[j]));
                }
            }

            Dictionary<KeyValuePair<DateTime, DateTime>, int> result = new Dictionary<KeyValuePair<DateTime, DateTime>, int>();
            foreach(var kvp in anagrams)
            {
                var diff = kvp.Value - kvp.Key;
                int days = (int) diff.TotalDays;
                if (IsPalindrom(days.ToString()))
                    result.Add(kvp, days);
            }

            foreach(var kvp in result)
            {
                Console.WriteLine($"{ToString(kvp.Key.Key)} - {ToString(kvp.Key.Value)} :: {kvp.Value}");
            }
        }

        static bool IsPalindrom(DateTime t)
        {
            return IsPalindrom(ToString(t));
        }

        static bool AreAnagram(string a, string b)
        {
            if (a.Length != b.Length)
                return false;
            if (a.Length == 0) // we don't want those
                return false;

            Dictionary<char, int> ca = ToHistogram(a);
            Dictionary<char, int> cb = ToHistogram(b);
            if (ca.Count != cb.Count)
                return false;
            foreach(var kvp in ca)
            {
                if (!cb.ContainsKey(kvp.Key))
                    return false;
                if (kvp.Value != cb[kvp.Key])
                    return false;
            }

            return true;
        }


        static Dictionary<char, int> ToHistogram(string s)
        {
            Dictionary<char, int> result = new Dictionary<char, int>();
            foreach(char c in s)
            {
                if (!result.ContainsKey(c))
                    result.Add(c, 0);
                result[c] += 1;
            }

            return result;
        }

        private static string ToString(DateTime t)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(t.Day.ToString("D2")).Append(t.Month.ToString("D2")).Append(t.Year);
            return stringBuilder.ToString();
        }

        static bool IsPalindrom(string s)
        {
            for(int i = 0; i <= s.Length / 2; ++i)
            {
                if (s[i] != s[s.Length - i - 1])
                    return false;
            }

            return true;
        }
    }
}
