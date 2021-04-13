using System;
using System.Collections.Generic;

namespace Eunomia
{
    [Serializable]
    public struct WordList
    {
        public List<string> All;
        public List<List<string>> ByLength;
        public List<string> WithLength(int length)
        {
            // TODO: or should we have a list of a 0 length string :P
            var lengthStartingWith1 = length - 1;
            if (lengthStartingWith1 >= ByLength.Count)
            {
                return null;
            }

            return this.ByLength[lengthStartingWith1];
        }

        public WordList(List<string> allWords)
        {
            this.All = allWords.FilterEmpty();
            this.ByLength = processWordsByLength(this.All);
        }

        /// Processing
        private static List<List<string>> processWordsByLength(List<string> all)
        {
            var processValues = new List<string>(all.Clone());
            List<List<string>> result = new List<List<string>>();

            int length = 1;
            int maxLength = processValues.Reduce((previous, current) =>
            {
                return System.Math.Max(previous, current.Length);
            }, 0);
            while (processValues.Count > 0 && length <= maxLength)
            {
                var found = processValues.FindAll(test => test.Length == length);
                // TODO: specify index to match Length - 1 purposefully to be safe
                result.Add(found);
                processValues.RemoveAll(found);
                length++;
            }

            return result;
        }
    }
}