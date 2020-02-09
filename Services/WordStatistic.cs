using System;
using System.Linq;

namespace Vocabulary
{
    public struct WordStatistic
    {
        public int[] Groups;
        public int Count;

        public WordStatistic(int[] groups)
        {
            if (groups.Length != 6)
                throw new ArgumentException("WordStatistic must have 6 values");
            Groups = groups;
            Count = groups.Sum();
        }
    }
}