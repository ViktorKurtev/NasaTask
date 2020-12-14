using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Nasa.Services.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Converts a string to a friendly representation based on a split delegate and modify delegate. The split delegate is used to
        /// determine at what points the string should be split and the modify delegate is performed on each split string in order
        /// to change the way it looks. After which, the join separator string is used to stitch the string back together.
        /// </summary>
        /// <param name="stringToChange">String to change</param>
        /// <param name="joinSeparator">The join separator used in the string.Join</param>
        /// <param name="splitDelegate">The delegate used to split the string based on</param>
        /// <param name="modifyDelegate">The delegate used to modify each split string</param>
        /// <returns>The modified string</returns>
        public static string ToFriendlyString(this string stringToChange, string joinSeparator, Func<char, bool> splitDelegate, Func<string, string> modifyDelegate)
        {
            var splitString = stringToChange.SplitWithCondition(splitDelegate);

            return string.Join(joinSeparator, splitString.Select(a => modifyDelegate(a)));
        }

        /// <summary>
        /// Splits the input string based on a supplied delegate.
        /// </summary>
        /// <param name="input">String to split</param>
        /// <param name="splitCondition">The condition to evaluate each char against.</param>
        /// <returns>An array of the split string.</returns>
        public static string[] SplitWithCondition(this string input, Func<char, bool> splitCondition)
        {
            List<int> indices = new List<int>();

            for (int i = 0; i < input.Length - 1; i++)
            {
                if (splitCondition(input[i]))
                {
                    indices.Add(i);
                }
            }

            if (!indices.Any())
            {
                return new string[] { input };
            }

            string[] returnArray = new string[indices.Count + 1];

            var currentIndex = 0;

            for (int i = 0; i < indices.Count; i++)
            {
                var stringLength = i > 0 ? indices[i] - indices[i - 1] - 1 : indices[i];

                returnArray[i] = input.Substring(currentIndex, stringLength);

                currentIndex = indices[i] + 1;

                if (i == indices.Count - 1)
                {
                    if (currentIndex != input.Length -1 || !splitCondition(input[currentIndex]))
                    {
                        returnArray[i + 1] = input.Substring(currentIndex);
                    }
                    else
                    {
                        returnArray[i + 1] = "";
                    }
                }
            }

            return returnArray;
        }

        /// <summary>
        /// Capitalizes the first letter of a string.
        /// </summary>
        /// <param name="stringToChange">String to modify</param>
        /// <returns>The modified string</returns>
        public static string CapitalizeFirstLetter(this string stringToChange)
        {
            return stringToChange.First().ToString().ToUpper() + stringToChange.Substring(1);
        }
    }
}
