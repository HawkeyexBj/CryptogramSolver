/* TrieWithOneChild.cs
 * Julie Thornton
 * Modified By Jack Cannell
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptogramSolver
{
    /// <summary>
    /// Represents a trie with exactly one child
    /// </summary>
    public class TrieWithOneChild : ITrie
    {
        /// <summary>
        /// Whether this contains the empty string
        /// </summary>
        private bool _containsEmpty;

        /// <summary>
        /// This is the only child
        /// </summary>
        private ITrie _onlyChild;

        /// <summary>
        /// The label of the child
        /// </summary>
        private char _childLabel;

        public TrieWithOneChild(string s, bool empty)
        {
            if (s == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                int loc = s[0] - ITrie.AlphabetStart;
                if (loc < 0 || loc >= ITrie.AlphabetSize)
                {
                    throw new ArgumentException();
                }
                _containsEmpty = empty;
                _childLabel = s[0];
                _onlyChild = new TrieWithNoChildren().Add(s.Substring(1));
            }
        }

        /// <summary>
        /// This adds a new string to this trie
        /// </summary>
        /// <param name="s">The string to add</param>
        /// <returns>An Itrie with the string added</returns>
        /// <exception cref="ArgumentNullException">If the passed in string in null</exception>
        /// <exception cref="ArgumentException">If the first character is not in our alphabet</exception>
        public ITrie Add(string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException();
            }
            if (s == "")
            {
                _containsEmpty = true;
            }
            else
            {
                int loc = s[0] - ITrie.AlphabetStart;
                if (loc < 0 || loc >= ITrie.AlphabetSize)
                {
                    throw new ArgumentException();
                }
                else if (s[0] == _childLabel)
                {
                    _onlyChild = _onlyChild.Add(s.Substring(1));
                    return this;
                }
                else
                {
                    return new TrieWithManyChildren(s, _containsEmpty, _childLabel, _onlyChild);
                }
            }
            return this;
        }

        /// <summary>
        /// If this child contains the given string
        /// </summary>
        /// <param name="s">The string which to check</param>
        /// <returns>A bool of whether this string is in this trie</returns>
        /// <exception cref="ArgumentNullException">If the string is null</exception>
        public bool Contains(string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException();
            }
            if (s == "")
            {
                return _containsEmpty;
            }
            else
            {
                int loc = s[0] - ITrie.AlphabetStart;
                if (loc < 0 || loc >= ITrie.AlphabetSize)
                {
                    return false;
                }
                else if (s[0] == _childLabel)
                    return _onlyChild.Contains(s.Substring(1));
                else
                    return false;
            }


        }

        public bool WildcardSearch(string s)
        {
            if (s == null) throw new ArgumentNullException();
            else if (s == "") return _containsEmpty;
            else
            {
                if (s[0] == _childLabel || s[0] == '?')
                    return _onlyChild.WildcardSearch(s.Substring(1));
                else return false;
            }
        }
    }
}
