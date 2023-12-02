/* TrieWithManyChildren.cs
 * Julie Thornton
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptogramSolver
{
    /// <summary>
    /// Represents a trie with more than one child
    /// </summary>
    public class TrieWithManyChildren : ITrie
    {
        /// <summary>
        /// This is the start of the alphabet
        /// </summary>
        private const char _alphabetStart = ITrie.AlphabetStart;

        /// <summary>
        /// This is the size of the alphabet
        /// </summary>
        private const int _alphabetSize = ITrie.AlphabetSize;

        /// <summary>
        /// Indicates whether the trie rooted at this node contains the empty string.
        /// </summary>
        private bool _containsEmpty = false;

        /// <summary>
        /// This node's children.
        /// </summary>
        private ITrie?[] _children = new ITrie?[_alphabetSize];


        /// <summary>
        /// Constructs a trie containing the given string and having the given child at the given label.
        /// If s contains any characters other than lower-case English letters,
        /// throws an ArgumentException.
        /// If childLabel is not a lower-case English letter, throws an ArgumentException.
        /// </summary>
        /// <param name="s">The string to include.</param>
        /// <param name="hasEmpty">Indicates whether this trie should contain the empty string.</param>
        /// <param name="childLabel">The label of the child.</param>
        /// <param name="child">The child labeled childLabel.</param>
        public TrieWithManyChildren(string s, bool hasEmpty, char childLabel, ITrie child)
        {
            if (s == null || child == null)
            {
                throw new ArgumentNullException();
            }
            if (childLabel < _alphabetStart || childLabel >= _alphabetStart + _alphabetSize)
            {
                throw new ArgumentException();
            }
            _containsEmpty = hasEmpty;
            _children[childLabel - _alphabetStart] = child;
            Add(s);
        }


        /// <summary>
        /// Determines whether the trie rooted at this node contains the given string.
        /// </summary>
        /// <param name="s">The string to look up.</param>
        /// <returns>Whether the trie at this node contains s.</returns>
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
                int loc = s[0] - _alphabetStart;
                if (loc < 0 || loc >= _alphabetSize)
                {
                    return false;
                }
                else
                {
                    ITrie? child = _children[loc];
                    if (child == null)
                    {
                        return false;
                    }
                    return child.Contains(s.Substring(1));
                }
            }
        }

        /// <summary>
        /// This adds a nwe word to the trie
        /// </summary>
        /// <param name="s">The remaining letters to add to the trie</param>
        /// <returns>A new ITrie with the word added</returns>
        /// <exception cref="ArgumentNullException">If the s passed in in null</exception>
        /// <exception cref="ArgumentException">If the letter in s is not in o</exception>
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
                int loc = s[0] - _alphabetStart;
                if (loc < 0 || loc >= _alphabetSize)
                {
                    throw new ArgumentException();
                }
                ITrie? child = _children[loc];
                if (child == null)
                {
                    child = new TrieWithNoChildren();
                }

                _children[loc] = child.Add(s.Substring(1));
            }
            return this;
        }

        public bool WildcardSearch(string s)
        {
            if (s == null) throw new ArgumentNullException();
            else if (s == "") return _containsEmpty;
            else
            {
                bool holder = false;

                if (s[0] == '?')
                {
                    for (int i = 0; i < ITrie.AlphabetSize; i++)
                    {
                        if (_children[i] != null)
                            holder = _children[i]!.WildcardSearch(s.Substring(1));
                        if (holder)
                            return true;
                    }
                    return holder;
                }
                else if (_children[s[0] - ITrie.AlphabetStart] != null)
                    return _children[s[0] - ITrie.AlphabetStart]!.WildcardSearch(s.Substring(1));
                else
                    return false;
            }
        }
    }
}
