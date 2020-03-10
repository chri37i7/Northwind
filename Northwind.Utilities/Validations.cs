using System;
using System.Linq;

namespace Northwind.Utilities
{
    public static class Validations
    {
        public static bool TextOnly(string s)
        {
            return string.IsNullOrWhiteSpace(s) ? false : s.All(c => char.IsLetter(c));
        }

        public static bool TextOnlySentence(string s)
        {
            if(string.IsNullOrWhiteSpace(s))
            {
                return false;
            }
            foreach(string word in s.Split(' ', '\t'))
            {
                if(!TextOnly(word))
                {
                    return false;
                }
            }
            return true;
        }

        // Demonstrates the function like syntax for a method, and the ternary decision operator ? : and the LINQ ext. All() applied to an IEnummerable (in this case a String array).
        // Same result as above, just with a different and more compact syntax.
        public static bool TextOnlySentence2(string s)
            => string.IsNullOrWhiteSpace(s) ? false : s.Split(' ', '\t').All(word => TextOnly(word));
    }
}