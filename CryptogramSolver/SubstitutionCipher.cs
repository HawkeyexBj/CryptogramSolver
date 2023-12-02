
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;

namespace CryptogramSolver
{
    /// <summary>
    /// This creates substitution searches.
    /// </summary>
    public class SubstitutionCipher
    {
        /// <summary>
        /// This creates random numbers
        /// </summary>
        private Random _generator = new Random();

        /// <summary>
        /// Holds the words
        /// </summary>
        private ITrie _words = new TrieWithNoChildren();

        /// <summary>
        /// This checks to make sure all words within the message are in the dictionary
        /// </summary>
        /// <param name="msg">THe message of words</param>
        /// <returns>A bool of whether all words are in the dictionary</returns>
        public bool AllWords(string msg)
        {
            string[] words = msg.Split(" ");
            bool isThere = true;
            foreach (string word in words) 
            {
                if(!_words.Contains(word)) 
                {
                    isThere = false;
                }
            }
            return isThere;
        }
        
        /// <summary>
        /// This creates a new encryption and encrypts a given message with said encryption
        /// </summary>
        /// <param name="msg">The message that needs to be encrypted</param>
        /// <returns>A string of the newly encrypted message</returns>
        public string Encrypt(string msg)
        {
            Dictionary<char, char> dic = new Dictionary<char, char>();
            List<char> plainLetter = new List<char>();
            List<char> encLetter = new List<char>();
            StringBuilder sb = new StringBuilder();
            int plainIndex;
            int encIndex;
            bool isThere = false;
            char encVal;
            for (int i = 0; i < ITrie.AlphabetSize; i++) 
            {
                plainLetter.Add((char)('a' + i));
                encLetter.Add((char)('a' + i));
            }
            for (int i = 0;i < ITrie.AlphabetSize; i++)
            {
                plainIndex = _generator.Next(ITrie.AlphabetSize - i);
                encIndex = _generator.Next(ITrie.AlphabetSize - i);
                dic.Add(plainLetter[plainIndex], encLetter[encIndex]);
                plainLetter.RemoveAt(plainIndex);
                encLetter.RemoveAt(encIndex);
            }
            for (int i = 0; i < msg.Length; i++) 
            {
                isThere = dic.TryGetValue(msg[i], out encVal);
                if (isThere) 
                {
                    sb.Append(encVal);
                }
                else
                {
                    sb.Append(' ');
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// This reads a dictionary of words in and stores them in the _words trie
        /// </summary>
        /// <param name="fileName">The file of dictionary words</param>
        /// <returns>A bool of whether the dictionary was read in properly</returns>
        public bool ReadDictionary (string fileName)
        {
            try
            {
                using (StreamReader input = File.OpenText(fileName))
                {
                    while (!input.EndOfStream)
                    {
                        // Because input is not at EndOfStream, ReadLine shouldn't return null.
                        string word = input.ReadLine()!;
                        _words = _words.Add(word);

                    }
                }
                return true;
            }
            catch  
            {
                return false;
            }
            
        }

        /// <summary>
        /// This finds any invalid letters in message
        /// </summary>
        /// <param name="msg">The message to search</param>
        /// <returns>Whether or not there are invalide characters</returns>
        public bool ContainsInvalid(string msg)
        {
            for (int i = 0; i < msg.Length; i++)
            {
                if (!(msg[i].Equals(' ')))
                {
                    if (msg[i] - ITrie.AlphabetStart < 0 || msg[i] - ITrie.AlphabetStart >= ITrie.AlphabetSize)
                        return true;
                }
                
            }
            return false;
        }

        /// <summary>
        /// Whether a possible completion is found
        /// </summary>
        /// <param name="plain">The string of words for which a possible completion needs to be found</param>
        /// <returns>Whether or not a completion is found</returns>
        private bool PossibleCompletion(StringBuilder[] plain)
        {
            foreach(StringBuilder word in plain) 
            {
                if(!(_words.WildcardSearch(word.ToString())))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Whether or not the given array of stringbuilders have been solved
        /// </summary>
        /// <param name="plain">The array to search</param>
        /// <returns>A bool of whether or not plain is solved</returns>
        public bool Solved(StringBuilder[] plain)
        {
            foreach (StringBuilder word in plain)
            {
                for(int i = 0; i < word.Length; i++) 
                {
                    if (word[i].Equals('?')) return false;
                }
            }
            return PossibleCompletion(plain);
        }

        /// <summary>
        /// The next position of an unknown character
        /// </summary>
        /// <param name="plain">The array which may have an unknown character</param>
        /// <returns>A length 2 array describing the position of the unknown character</returns>
        private int[]? NextPosition(StringBuilder[] plain)
        {
            int plainCount = 0;
            int[] returner = new int[2];
            foreach (StringBuilder word in plain)
            {
                for (int i = 0; i < word.Length; i++)
                {
                    if (word[i].Equals('?'))
                    { 
                        returner[0] = plainCount;
                        returner[1] = i;
                        return returner;
                    }
                }
                plainCount++;
            }
            return null;
        }

        /// <summary>
        /// Substitutes a given character with a new one assuming the origional is found in cipher
        /// </summary>
        /// <param name="orig">The origional character to look for</param>
        /// <param name="replace">What to replace the origional character with</param>
        /// <param name="cipher">The words to look for the origional character with</param>
        /// <param name="plain">The place to put the new character</param>
        private void Substitute(char orig, char replace, string[] cipher, StringBuilder[] plain)
        {
            for (int i = 0; i < cipher.Length; i++)
            {
                for (int j = 0; j < cipher[i].Length; j++)
                {
                    if (cipher[i][j].Equals(orig))
                    {
                        plain[i][j] = replace;
                    }
                }
            }
        }

        /// <summary>
        /// This performs a recursive search to find the appropriate letters to replace the cipher with
        /// </summary>
        /// <param name="cipher">The unknown cipher which needs to be decoded</param>
        /// <param name="partial">The partial completion</param>
        /// <param name="alphaUsed">Whether or not a letter has been used</param>
        /// <returns>A bool of whether the search is complete</returns>
        private bool DecryptionSearch(string[] cipher, StringBuilder[] partial, bool[] alphaUsed)
        {
            if(Solved(partial)) 
            {
                return true;
            }
            else
            {
                if (!PossibleCompletion(partial)) return false;

                //If partial was solved, the first statement would be true
                int[] holder = NextPosition(partial)!;
                char v = cipher[holder[0]][holder[1]];
                bool found;
                for (int i = 0; i < ITrie.AlphabetSize; i++)
                {
                    if (!alphaUsed[i]) 
                    {
                        alphaUsed[i] = true;
                        Substitute(v, (char)('a' + i), cipher, partial);
                        found = DecryptionSearch(cipher, partial, alphaUsed);
                        if (found && NextPosition(partial) == null)
                        {
                            return true;
                        }
                        else if (!found)
                        {
                            alphaUsed[i] = false;
                            Substitute(v, '?', cipher, partial);
                        }
                    }
                }
                return false;
            }
            
        }

        /// <summary>
        /// Decrypts a given message
        /// </summary>
        /// <param name="msg">The message to decrypt</param>
        /// <param name="solved">Whether or not it was decrypted</param>
        /// <returns>A string of the decrypted message</returns>
        public string Decrypt(string msg, out bool solved)
        {
            string[] words = msg.Split(' ');
            bool[] used = new bool[26];
            StringBuilder[] sb = new StringBuilder[words.Length];
            int counter = 0;
            foreach (string hold in words)
            {
                sb[counter] = new StringBuilder();
                for (int i = 0; i < hold.Length; i++)
                    sb[counter].Append("?");
                counter++;
            }

            solved = DecryptionSearch(words, sb, used);
            StringBuilder sb2 = new StringBuilder();
            foreach(StringBuilder word in sb)
            {
                sb2.Append(word.ToString() + " ");
            }
            return sb2.ToString();
        }
    }
}
