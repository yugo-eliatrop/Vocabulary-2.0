using System;
using System.Collections.Generic;

namespace Vocabulary
{
    public class WordComparer : IEqualityComparer<Word>
    {
        public bool Equals(Word w1, Word w2)
        {
            return w1.Id == w2.Id;
        }
        
        // override object.GetHashCode
        public int GetHashCode(Word word)
        {
            if (Object.ReferenceEquals(word, null)) return 0;
            int hashProductName = word.Eng == null ? 0 : word.Eng.GetHashCode();
            int hashProductCode = word.Rus.GetHashCode();
            return hashProductName ^ hashProductCode;
        }
    }
}
