/*
 * UserInterface
 * Author: Rodney Howell 
 * Modified by Jack Cannell
 */
using System.Collections.Generic;
using System.Windows.Forms;

namespace CryptogramSolver
{
    /// <summary>
    /// THe GUI
    /// </summary>
    public partial class UserInterface : Form
    {
        /// <summary>
        /// A cipher object which can do a lot of things
        /// </summary>
        SubstitutionCipher _cipher = new SubstitutionCipher();
        public UserInterface()
        {
            InitializeComponent();
            try { _cipher.ReadDictionary("..\\..\\..\\data\\dictionary.txt"); }
            catch (Exception e) { MessageBox.Show(e.ToString()); }

        }

        /// <summary>
        /// What happens when the user clickes encrypt
        /// </summary>
        /// <param name="sender">The object signaling the event.</param>
        /// <param name="e">Information about the event.</param>
        private void Encrypt_Click(object sender, EventArgs e)
        {
            if (_cipher.ContainsInvalid(uxMessage.Text))
            {
                uxResult.Text = "Error: Invalid characters. Only lowercase letters and spaces allowed.";
            }
            else if (!_cipher.AllWords(uxMessage.Text))
            {
                uxResult.Text = "Error: not all words are in the dictionary.";
            }
            else
            {
                uxResult.Text = _cipher.Encrypt(uxMessage.Text);
            }
        }

        /// <summary>
        /// What happens when the user tries to decrypt
        /// </summary>
        /// <param name="sender">The object signaling the event.</param>
        /// <param name="e">Information about the event.</param>
        private void Decrypt_Click(object sender, EventArgs e)
        {
            if (_cipher.ContainsInvalid(uxMessage.Text))
            {
                uxResult.Text = "Error: Invalid characters. Only lowercase letters and spaces allowed.";
            }
            else
            {
                bool solved = false;
                string result = _cipher.Decrypt(uxMessage.Text, out solved);
                if (solved)
                {
                    uxResult.Text = result;
                }
                else
                {
                    uxResult.Text = "No solution exists.";
                }
            }

        }
    }
}