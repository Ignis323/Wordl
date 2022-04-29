using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.InteropServices;


namespace Lingo
{
    /// <summary>
    /// The lingo game form
    /// </summary>
    public partial class Lingo : Form
    {
        /// <summary>
        /// The list of words
        /// </summary>
        private List<string> words = new List<string>();

        private int randomPos1;
        private int randomPos2;

        /// <summary>
        /// The index of the word to guess
        /// </summary>
        private int wordIndex;

        /// <summary>
        /// The number of attempts
        /// </summary>
        private int attempts;


        private Font bold = new Font("Courier New", 12F, FontStyle.Bold);
        private Font regular = new Font("Courier New", 12F, FontStyle.Regular);

        private bool[] solved = new bool[5];
        private bool[] marked = new bool[5];

        public Lingo()
        {
            InitializeComponent();
            InitiailizeGrid();
            InitializeWords();
            StartGame();
        }

        /// <summary>
        /// Starts a new game
        /// </summary>
        private void StartGame()
        {
            Random r = new Random(DateTime.Now.Millisecond);

            randomPos1 = r.Next(0, 5);
            randomPos2 = r.Next(0, 5);
            if (randomPos2 == randomPos1)
            {
                randomPos2 = (randomPos2 + 1) % 5;
            }

            wordIndex = r.Next(0, words.Count);

            ResetGrid();
            ((Label)layout.Controls[randomPos1]).Text = words[wordIndex][randomPos1]+"";
            ((Label)layout.Controls[randomPos2]).Text = words[wordIndex][randomPos2]+"";

            // Mark the inital two letters as solved
            solved[randomPos1] = solved[randomPos2] = true;

            attempts = 0;
            guess.Text = "";
        }

        /// <summary>
        /// Resets the grid
        /// </summary>
        private void ResetGrid()
        {
            for (int x = 0; x < 5; x++)
            {
                solved[x] = marked[x] = false;
                for (int y = 0; y < 5; y++)
                {
                    Label b = layout.Controls[x * 5 + y] as Label;
                    b.Text = "";
                    b.ForeColor = Color.Gray;                    
                    b.Font = regular;
                }
            }
        }

        /// <summary>
        /// Loads the word list file from the resource
        /// </summary>
        private void InitializeWords()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            StreamReader r = new StreamReader(assembly.GetManifestResourceStream("Lingo.Data.Lingo.txt"));
            while (!r.EndOfStream)
            {
                words.Add(r.ReadLine());
            }
            r.Close();
        }

        /// <summary>
        /// Initializes the main grid by adding new labels to it
        /// </summary>
        private void InitiailizeGrid()
        {
            layout.Controls.Clear();
            for(int x=1; x<6; x++)
                for (int y = 1; y < 6; y++)
                {
                    Label l = new Label();
                    l.Width = 20;                    
                    l.Text = "";
                    l.ForeColor = Color.Gray;
                    l.Font = regular;
                    layout.Controls.Add(l);                    
                }
            // Position the window to the right bottom corner
            this.Left = Screen.PrimaryScreen.WorkingArea.Right - this.Width;
            this.Top = Screen.PrimaryScreen.WorkingArea.Bottom  - this.Height;
        }

        /// <summary>
        /// The core game processing logic
        /// in a monolithic block of code
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtGuess_KeyPress(object sender, KeyPressEventArgs e)
        {
            // If the user has guessed a word correctly, we require
            // an enter key press to move on to the new game
            if (e.KeyChar == (char)Keys.Enter && guess.Text=="Correct!")
            {
                e.Handled = true;
                StartGame();
                return;
            }
            
            // If escape was pressed close the form
            if (e.KeyChar == (char)Keys.Escape)
            {
                e.Handled = true;
                this.Close();
                return;
            }
            
            // If escape ' key was pressed display the 
            // word to be guessed
            if (e.KeyChar == '\'')
            {
                e.Handled = true;
                guess.Text = words[wordIndex];
                return;
            }

            // If the user has guessed all the chances, force to press
            // enter to proceed
            if (attempts > 4 && (e.KeyChar != (char)Keys.Enter))
            {
                e.Handled = true;
                return;
            }

            // If enter key has been pressed without entering a five letter word
            // or backspace has been pressed, just return
            if ((e.KeyChar == (char)Keys.Enter && guess.Text.Length != 5 && attempts<5) || e.KeyChar == (char)Keys.Back)
            {
                return;
            }

            // Ignore any keys other than a-z and the enter key at this stage
            if ((e.KeyChar.CompareTo('a') < 0  || e.KeyChar.CompareTo('z')>0) && e.KeyChar!=(char)Keys.Enter)
            {
                e.Handled = true;
                return;
            } 
                // User has pressed a key after entering a five letter word
                // and has chances left to be guessing
            else if (guess.Text.Length == 5 && attempts<5)
            {
                e.Handled = true;
                // If the key was not enter, return
                if (e.KeyChar != (char)Keys.Enter)
                {
                    return;
                }
                // Check if the word is valid
                if (words.BinarySearch(guess.Text) < 0)
                {
                    lblError.Visible = true;
                    return;
                }
                else
                {
                    lblError.Visible = false;
                }

                // First scan all the letters and determine all that have been solved
                for (int i = 0; i < 5; i++)
                {
                    // Display the word guessed at the correct place in the grid
                    layout.Controls[attempts * 5 + i].Text = guess.Text[i] + "";

                    // If the letter matches
                    if (guess.Text[i] == words[wordIndex][i])
                    {
                        // Mark that letter has been solved
                        solved[i] = true;                        

                        // Show the solved letter in green
                        layout.Controls[attempts * 5 + i].ForeColor = Color.DarkGreen;
                        layout.Controls[attempts * 5 + i].Font = bold;
                    }

                    // Now display the letter that were solved
                    if (attempts < 4 && solved[i])
                    {
                        layout.Controls[(attempts + 1) * 5 + i].Text = words[wordIndex][i] + "";
                    }

                    // Mark only if the letters are the same
                    // It may be solved, but not marked
                    marked[i] = (guess.Text[i] == words[wordIndex][i]);
                }
                // If the current letter is present elsewhere in
                // the word to be guessed, we mark them out in red
                for (int i = 0; i < 5; i++)
                {
                    if (solved[i] && marked[i]) continue;
                    for (int j = 0; j < 5; j++)
                    {
                        if (guess.Text[i] == words[wordIndex][j])
                        {
                            if (marked[j]) continue;
                            else
                            {
                                marked[j] = true;
                                layout.Controls[attempts * 5 + i].ForeColor = Color.DarkRed;
                                layout.Controls[attempts * 5 + i].Font = bold;
                                break;
                            }
                        }
                    }
                }
                // Increment the count of attempts
                attempts++;

                // If the word is correct, display it is
                // and start the game all over again
                if (guess.Text == words[wordIndex])
                {
                    guess.Text = "Correct!";
                    return;
                }
                
                // Clear the current entry
                guess.Text = "";
            }
            else if (attempts == 5)
            {
                // If all guesses have been exhausted, display what the
                // right word was
                guess.Text = "Word was: " + words[wordIndex];                
                attempts++;
            }
            else if (attempts > 5)
            {
                // Start off a new game
                StartGame();
            }            
        }
    }
}
