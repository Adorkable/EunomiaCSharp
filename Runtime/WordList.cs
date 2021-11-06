using System;
using System.Collections.Generic;

namespace Eunomia
{
    [Serializable]
    public struct WordList
    {
        public IEnumerable<string> All;
        public List<List<string>> ByLength;

        public List<string> WithLength(int length)
        {
            // TODO: or should we have a list of a 0 length string :P
            var lengthStartingWith1 = length - 1;
            return lengthStartingWith1 < ByLength.Count
                ? this.ByLength[lengthStartingWith1]
                : null;
        }

        public WordList(IEnumerable<string> allWords)
        {
            this.All = allWords.FilterNullOrEmpty();
            this.ByLength = ProcessWordsByLength(this.All);
        }

        /// Processing
        private static List<List<string>> ProcessWordsByLength(IEnumerable<string> all)
        {
            var processValues = new List<string>(all.Clone());
            var result = new List<List<string>>();

            var length = 1;
            var maxLength = processValues.Reduce(
                (previous, current) => System.Math.Max(previous, current.Length), 0
            );

            while (processValues.Count > 0 && length <= maxLength)
            {
                var found = processValues.FindAll(
                    // ReSharper disable once SimplifyConditionalTernaryExpression
                    (test) => test != null
                        ? test.Length == length
                        : false
                );

                // TODO: specify index to match Length - 1 purposefully to be safe
                result.Add(found);
                processValues.RemoveAll(found);
                length++;
            }

            return result;
        }
    }
}