using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Text;
using System;

namespace YNL.Extensions.Methods
{
    public static class MString
    {
        /// <summary>
        /// Remove all 'inputString' in string.
        /// </summary>
        public static string RemoveAll(this string inputString, string removedString) 
            => inputString.Replace(removedString, "");

        /// <summary>
        /// Return a string removed all the <b><i>removedString</i></b> string.
        /// </summary>
        public static string RemoveWord(this string inputString, string removedString)
            => inputString.Replace(removedString, "");

        /// <summary>
        /// Convert object to string with given format. <br></br>
        /// Source: <see href="https://gist.github.com/omgwtfgames/f917ca28581761b8100f"/>
        /// <code>
        /// string blaStr = aPerson.ToString("My name is {FirstName} {LastName}.")
        /// </code>
        /// </summary>
        public static string ToString(this object anObject, string aFormat, IFormatProvider formatProvider)
        {
            StringBuilder sb = new StringBuilder();
            Type type = anObject.GetType();
            Regex reg = new Regex(@"({)([^}]+)(})", RegexOptions.IgnoreCase);
            MatchCollection mc = reg.Matches(aFormat);
            int startIndex = 0;
            foreach (Match m in mc)
            {
                Group g = m.Groups[2]; // It's second in the match between { and }
                int length = g.Index - startIndex - 1;
                sb.Append(aFormat.Substring(startIndex, length));

                string toGet = string.Empty;
                string toFormat = string.Empty;
                int formatIndex = g.Value.IndexOf(":"); // Formatting would be to the right of a :
                if (formatIndex == -1) // No formatting, no worries
                {
                    toGet = g.Value;
                }
                else // Pickup the formatting
                {
                    toGet = g.Value.Substring(0, formatIndex);
                    toFormat = g.Value.Substring(formatIndex + 1);
                }

                // First try properties
                PropertyInfo retrievedProperty = type.GetProperty(toGet);
                Type retrievedType = null;
                object retrievedObject = null;
                if (retrievedProperty != null)
                {
                    retrievedType = retrievedProperty.PropertyType;
                    retrievedObject = retrievedProperty.GetValue(anObject, null);
                }
                else // Try fields
                {
                    FieldInfo retrievedField = type.GetField(toGet);
                    if (retrievedField != null)
                    {
                        retrievedType = retrievedField.FieldType;
                        retrievedObject = retrievedField.GetValue(anObject);
                    }
                }

                if (retrievedType != null) // Found something
                {
                    string result = string.Empty;
                    if (toFormat == string.Empty) // No format info
                    {
                        result = retrievedType.InvokeMember("ToString",
                            BindingFlags.Public | BindingFlags.NonPublic |
                            BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.IgnoreCase
                            , null, retrievedObject, null) as string;
                    }
                    else // Format info
                    {
                        result = retrievedType.InvokeMember("ToString",
                            BindingFlags.Public | BindingFlags.NonPublic |
                            BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.IgnoreCase
                            , null, retrievedObject, new object[] { toFormat, formatProvider }) as string;
                    }
                    sb.Append(result);
                }
                else // Didn't find a property with that name, so be gracious and put it back
                {
                    sb.Append("{");
                    sb.Append(g.Value);
                    sb.Append("}");
                }
                startIndex = g.Index + g.Length + 1;
            }
            if (startIndex < aFormat.Length) // Include the rest (end) of the string
            {
                sb.Append(aFormat.Substring(startIndex));
            }
            return sb.ToString();
        }

        /// <summary>
        /// Check if a string is null or empty.
        /// </summary>
        public static bool IsNullOrEmpty(this string input)
            => input == null || input == "" || input.Length < 1 ? true : false;

        /// <summary>
        /// Insert a substring to original string by index.
        /// </summary>
        public static string Insert(this string input, int index, string insert)
            => input.Substring(0, index) + insert + input.Substring(index);

        /// <summary>
        /// Truncates a string to a specified length and appends an ellipsis "..." to the end of the string.
        /// </summary>
        public static string Truncate(this string input, int maxLength)
            => string.IsNullOrEmpty(input) ? input : input.Length <= maxLength ? input : input.Substring(0, maxLength) + "...";

        /// <summary>
        /// Uppercase first character of all the words in a string. <br></br>
        /// Eg: hello world => Hello World
        /// </summary>
        public static string ToTitleCase(this string value)
            => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower()).Replace("'", "'");

        /// <summary>
        /// Reverse a string.
        /// </summary>
        public static string Reverse(this string value)
        {
            char[] charArray = value.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        /// <summary>
        /// Compare a string with another string, can ignore letter case.
        /// </summary>
        public static bool CompareTo(this string string1, string string2, bool ignoreCase = true)
        {
            bool compare = false;
            if (ignoreCase) compare = string.Compare(string1, string2, StringComparison.OrdinalIgnoreCase) == 0;
            else compare = string1 == string2;

            return compare;
        }

        /// <summary>
        /// Return true if input string contains one out of the strings in list.
        /// </summary>
        public static bool ContainsOneOf(this string input, List<string> contained)
        {
            foreach (var item in contained) if (input.Contains(item)) return true;
            return false;
        }

        /// <summary>
        /// Return true if the input string contains all elements in list.
        /// </summary>
        public static bool ContainsAll(this string input, List<string> contained)
        {
            foreach (var item in contained) if (!input.Contains(item)) return false;
            return true;
        }

        /// <summary>
        /// Colorize a string in HTML format.
        /// </summary>
        public static string Colorize(this string input, string color)
            => color.Contains("#") ? $"<{color}>{input}</color>" : $"<#{color}>{input}</color>";

        /// <summary>
        /// Decolorize a string in HTML format.
        /// </summary>
        public static string Decolorize(this string input)
            => input[1] == '#' ? input.Replace("</color>", "").Substring(9) : input.Replace("</color>", "").Substring(15);

        /// <summary>
        /// Remove all characters in a duplicated-character group and leave just one there.
        /// </summary>
        public static string RemoveDuplicatedChars(this string input, params char[] characters)
        {
            string pattern = "";
            string output = input;

            foreach (char character in characters)
            {
                if (character == '\\') pattern = @"[\\]+";
                else pattern = $"[{character}]+";
                output = Regex.Replace(output, pattern, $"{character}");
            }
            return output;
        }

        /// <summary>
        /// Fuzzy search return a list of string from an original list with a target string.
        /// </summary>
        public static string[] FuzzySearch(this List<string> list, string target, int maxDistance = 2)
        {
            return list.Where(i => LevenshteinDistance(i, target) <= maxDistance).ToArray();

            int LevenshteinDistance(string s, string t)
            {
                if (s == t) return 0;
                if (s.Length == 0) return t.Length;
                if (t.Length == 0) return s.Length;

                int[,] distance = new int[s.Length + 1, t.Length + 1];

                for (int i = 0; i <= s.Length; i++) distance[i, 0] = i;

                for (int j = 0; j <= t.Length; j++) distance[0, j] = j;

                for (int i = 1; i <= s.Length; i++)
                {
                    for (int j = 1; j <= t.Length; j++)
                    {
                        int cost = (s[i - 1] == t[j - 1]) ? 0 : 1;
                        distance[i, j] = Math.Min(Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1), distance[i - 1, j - 1] + cost);
                    }
                }

                return distance[s.Length, t.Length];
            }
        }
        public static string[] FuzzySearch(this string[] array, string target, int maxDistance = 2)
        {
            return array.Where(i => LevenshteinDistance(i, target) <= maxDistance).ToArray();

            int LevenshteinDistance(string s, string t)
            {
                if (s == t) return 0;
                if (s.Length == 0) return t.Length;
                if (t.Length == 0) return s.Length;

                int[,] distance = new int[s.Length + 1, t.Length + 1];

                for (int i = 0; i <= s.Length; i++) distance[i, 0] = i;

                for (int j = 0; j <= t.Length; j++) distance[0, j] = j;

                for (int i = 1; i <= s.Length; i++)
                {
                    for (int j = 1; j <= t.Length; j++)
                    {
                        int cost = (s[i - 1] == t[j - 1]) ? 0 : 1;
                        distance[i, j] = Math.Min(Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1), distance[i - 1, j - 1] + cost);
                    }
                }

                return distance[s.Length, t.Length];
            }
        }

        /// <summary>
        /// Get words that start with ...
        /// </summary>
        public static string[] GetWordsStartWith(this List<string> list, string target, bool ignoreCase = false)
        {
            if (ignoreCase) return list.Where(i => i.StartsWith(target, StringComparison.CurrentCultureIgnoreCase)).ToArray();
            else return list.Where(i => i.StartsWith(target)).ToArray();
        }
        public static string[] GetWordsStartWith(this string[] array, string target, bool ignoreCase = false)
        {
            if (ignoreCase) return array.Where(i => i.StartsWith(target, StringComparison.CurrentCultureIgnoreCase)).ToArray();
            else return array.Where(i => i.StartsWith(target)).ToArray();
        }

        /// <summary>
        /// Get words that contains ...
        /// </summary>
        public static string[] GetWordsContain(this List<string> list, string target, bool ignoreCase = false)
        {
            if (ignoreCase) return list.Where(i => i.Contains(target, StringComparison.CurrentCultureIgnoreCase)).ToArray();
            else return list.Where(i => i.Contains(target)).ToArray();
        }
        public static string[] GetWordsContain(this string[] array, string target, bool ignoreCase = false)
        {
            if (ignoreCase) return array.Where(i => i.Contains(target, StringComparison.CurrentCultureIgnoreCase)).ToArray();
            else return array.Where(i => i.Contains(target)).ToArray();
        }

        /// <summary>
        /// Add spaces between words (before capitaled letters). <br></br>
        /// Source: <see href="https://stackoverflow.com/questions/272633/add-spaces-before-capital-letters"/>
        /// </summary>
        public static string AddSpaces(this string text, bool preserveAcronyms = true)
        {
            if (preserveAcronyms) return Regex.Replace(text, @"((?<=\p{Ll})\p{Lu})|((?!\A)\p{Lu}(?>\p{Ll}))", " $0");
            else return Regex.Replace(text, "(?<=.)(?=[A-Z])", " ");
        }

        /// <summary>
        /// Check if a text contains a target string and ignore case.
        /// </summary>
        public static bool ContainsIgnoreCase(this string text, string target)
            => text.Contains(target, StringComparison.CurrentCultureIgnoreCase);

        /// <summary>
        /// Create a randomized string.
        /// </summary>
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789 ";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }

    public static class MSpecificString
    {
        /// <summary>
        /// Convert all words in a text that match the input word into color format, ignore letter case. <br></br><br></br>
        /// Example: "This is an example".ConvertToColorFormat("example", "#FFFFFF") <br></br>
        /// </summary>
        public static string ConvertToColorFormat(this string text, string word, string hexColor)
        {
            string pattern = $@"\b{word}\b";
            return Regex.Replace(text, pattern, $"<color={hexColor}><b>$&</b></color>", RegexOptions.IgnoreCase);
        }

    }
}