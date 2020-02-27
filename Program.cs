using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Palindromes
{
    class Program
    {
        // no test, no performance 
        static void Main(string[] args)
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();
            List<DateTime> palindromes = FindPalindromes();
            watch.Stop();
            Console.WriteLine($"Found palindromes in {watch.ElapsedMilliseconds} milliseconds");

            watch.Reset();
            watch.Start();
            List<KeyValuePair<DateTime, DateTime>> anagrams = FindAnagrams(palindromes);
            watch.Stop();
            Console.WriteLine($"Found anagrams in {watch.ElapsedMilliseconds} milliseconds");

            watch.Reset();
            watch.Start();
            Dictionary<KeyValuePair<DateTime, DateTime>, int> result = FindResult(anagrams);
            watch.Stop();
            Console.WriteLine($"Found result in {watch.ElapsedMilliseconds} milliseconds");

            foreach (var kvp in result)
            {
                Console.WriteLine($"{ToString(kvp.Key.Key)} - {ToString(kvp.Key.Value)} :: {kvp.Value}");
            }
        }

        private static Dictionary<KeyValuePair<DateTime, DateTime>, int> FindResult(List<KeyValuePair<DateTime, DateTime>> anagrams)
        {
            Dictionary<KeyValuePair<DateTime, DateTime>, int> result = new Dictionary<KeyValuePair<DateTime, DateTime>, int>();
            foreach (var kvp in anagrams)
            {
                var diff = kvp.Value - kvp.Key;
                int days = (int)diff.TotalDays;
                if (IsPalindrome(days.ToString()))
                    result.Add(kvp, days);
            }

            return result;
        }

        private static List<KeyValuePair<DateTime, DateTime>> FindAnagrams(List<DateTime> palindromes)
        {
            List<KeyValuePair<DateTime, DateTime>> anagrams = new List<KeyValuePair<DateTime, DateTime>>();
            for (int i = 0; i < palindromes.Count; ++i)
            {
                for (int j = i + 1; j < palindromes.Count; ++j)
                {
                    if (AreAnagram(palindromes[i], palindromes[j]))
                        anagrams.Add(new KeyValuePair<DateTime, DateTime>(palindromes[i], palindromes[j]));
                }
            }

            return anagrams;
        }

        private static List<DateTime> FindPalindromes()
        {
            List<DateTime> palindroms = new List<DateTime>();
            DateTime start = new DateTime(1000, 01, 01);
            DateTime stop = new DateTime(9999, 12, 31);
            while (start < stop)
            {
                if (IsPalindrome(start))
                    palindroms.Add(start);
                start = start.AddDays(1);
            }

            return palindroms;
        }

        static bool IsPalindrome(DateTime t)
        {
            int day, month, year;
            t.GetDatePart(out year, out month, out day);
            int d0 = day < 10 ? 0 : day / 10;
            if (d0 != year % 10)
                return false;
            int d1 = day % 10;
            if (d1 != (year / 10) % 10)
                return false;
            int m0 = month < 10 ? 0 : month / 10;
            if (m0 != (year / 100) % 10)
                return false;
            int m1 = month % 10;
            if (m1 != year / 1000)
                return false;

            return true;
        }

        static bool AreAnagram(DateTime a, DateTime b)
        {
            int[] hista = ToHistogram(a);
            int[] histb = ToHistogram(b);

            for (int i = 0; i < 10; ++i)
            {
                if (hista[i] != histb[i])
                    return false;
            }

            return true;
        }

        static int[] ToHistogram(DateTime d)
        {
            int day, month, year;
            d.GetDatePart(out year, out month, out day);
            int[] result = new int[10];
            result[day < 10 ? 0 : day / 10] += 1;
            result[day % 10] += 1;
            result[month < 10 ? 0 : month / 10] += 1;
            result[month % 10] += 1;
            result[year / 1000] += 1;
            result[(year / 100) % 10] += 1;
            result[(year / 10) % 10] += 1;
            result[year % 10] += 1;
            return result;
        }

        private static string ToString(DateTime t)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(t.Day.ToString("D2")).Append(t.Month.ToString("D2")).Append(t.Year);
            return stringBuilder.ToString();
        }

        static bool IsPalindrome(string s)
        {
            for (int i = 0; i <= s.Length / 2; ++i)
            {
                if (s[i] != s[s.Length - i - 1])
                    return false;
            }

            return true;
        }
    }
}
