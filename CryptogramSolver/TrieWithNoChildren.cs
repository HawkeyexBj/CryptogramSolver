/* TrieWithNoChildren.cs
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
    /// Represents a trie with no children
    /// </summary>
    public class TrieWithNoChildren : ITrie
    {
        /// <summary>
        /// Whether the empty string is stored here
        /// </summary>
        private bool _containsEmpty = false;

        /// <summary>
        /// We are adding a given string to this node
        /// </summary>
        /// <param name="s">The string to add</param>
        /// <returns>A new ITrie with the string added</returns>
        /// <exception cref="ArgumentNullException">If the string is null</exception>
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
                return new TrieWithOneChild(s, _containsEmpty);
            }
            return this;
        }

        /// <summary>
        /// Checks whether the given string is within the node
        /// </summary>
        /// <param name="s">The string to check</param>
        /// <returns>A bool of whether the string is found</returns>
        /// <exception cref="ArgumentNullException">If the given string is null</exception>
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
                return false;
        }

        /// <summary>
        /// This searchest for a random letter
        /// </summary>
        /// <param name="s">The string which should be searched</param>
        /// <returns>A bool for whether it was found</returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool WildcardSearch(string s)
        {
            if (s == null) throw new ArgumentNullException();
            else if (s == "") return _containsEmpty;
            else return false;
        }
    }
}
