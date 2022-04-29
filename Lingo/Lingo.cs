
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
using System.Timers;

namespace Lingo
{
    public partial class Lingo : Form
    {
        /// <summary>
        /// The index of the word to guess
        /// </summary>
        private int wordIndex;

        /// <summary>
        /// The list of words in the puzzle
        /// </summary>
        private List<string> words = new List<string>();
        /// <summary>
        /// The index of the last word added to the hisory list
        /// </summary>
        private int historyIndex;

        /// <summary>
        /// The list of words attempted in a game
        /// </summary>
        private List<string> history = new List<string>();
        private Font bold = new Font("Georgia", 12F, FontStyle.Bold);
        private Font regular = new Font("Georgia", 12F, FontStyle.Regular);
        /// <summary>
        /// The user's score
        /// </summary>
        int score;

        /// <summary>
        /// The timer for the bonus score
        /// </summary>
        System.Timers.Timer bonusTimer = new System.Timers.Timer();

        /// <summary>
        /// The bonus score available
        /// </summary>
        int bonusScore;

        /// <summary>
        /// Delegate to enable displaying the bonus score
        /// in a thread safe manner
        /// </summary>
        delegate void PerformActivityCallback();

        /// <summary>
        /// The maximum possible bonus at a time
        /// </summary>
        const int MaximumBonusScore = 1000;

        /// <summary>
        /// The bonus timer interval
        /// </summary>
        const int BonusTimerInterval = 100;

        /// <summary>
        /// A color map
        /// </summary>
        Color[] ColorMap = new Color[100];

        /// <summary>
        /// Color map index factor used to set
        /// the color of the score
        /// </summary>
        float ColorMapIndexFactor = 0.1F;

        /// <summary>
        /// The length of a word
        /// </summary>
        private const int WordLength = 5;

        /// <summary>
        /// Holds which all letters have been solved so far
        /// </summary>
        private bool[] solved = new bool[WordLength];

        /// <summary>
        /// Indicates whether a letter has been accounted for
        /// while highlighting letters in the guess that are not
        /// in the correct position
        /// </summary>
        private bool[] marked = new bool[WordLength];

        /// <summary>
        /// The state of the current attempt
        /// </summary>
        public int currentAttempt;

        static class GameState
        {
            /// <summary>
            /// Indicates the last attempt at solving a word
            /// </summary>
            public const int LastAttempt = 4;

            /// <summary>
            /// Indicates the first attempt at solving a word
            /// </summary>
            public const int FirstAttempt = 0;

            /// <summary>
            /// The maximum number of attempts
            /// </summary>
            public const int MaximumAttemptsOver = 5;

            /// <summary>
            /// Indicates the original or solved word is displayed
            /// </summary>
            public const int ViewingWord = 6;

            /// <summary>
            /// The game is diplaying the initial screen
            /// </summary>
            public const int InitialDisplay = -1;
        }
        /// <summary>
        /// The difficulty levels of the game
        /// </summary>
        enum DifficultyLevel
        {
            Easy,
            Medium,
            Hard
        }

        /// <summary>
        /// The current game difficulty level
        /// </summary>
        private DifficultyLevel gameDifficulty = DifficultyLevel.Easy;

        private const string EasyWordPrefix = "e:";
        private const string MediumWordPrefix = "m:";
        private const string HardWordPrefix = "h:";

        /// <summary>
        /// The help timer waits to and checks if you
        /// are having difficulty with a word. It provides
        /// an extra letter for your convenience
        /// </summary>
        System.Timers.Timer helpLetterTimer = new System.Timers.Timer();

        /// <summary>
        /// The time interval for the help timer
        /// </summary>
        const int HelpTimerInterval = 60000;

        /// <summary>
        /// Determines if a help letter was given
        /// </summary>
        bool helpGiven = false;
        private int randomPos1;
        private int randomPos2;

        /// <summary>
        /// The current puzzle word to be solved
        /// </summary>
        private string CurrentPuzzleWord
        {
            get
            {
                return _currentPuzzleWord;
            }
        }
        private string _currentPuzzleWord;

        /// <summary>
        /// Default constructor
        /// Starts off the game engine and window
        /// </summary>
        public Lingo()
        {
            InitializeComponent();
            InitiailizeForm();
            InitializeWords();
            DisplayLingo();            
        }

        /// <summary>
        /// Displays the starting Lingo screen
        /// </summary>
        private void DisplayLingo()
        {
            currentAttempt = GameState.InitialDisplay;
            string Lingo = "Wordl";
            for (int i = 0; i < WordLength; i++)
            {
                FadeLabel fl = initLayout.Controls[i] as FadeLabel;
                fl.Text = Lingo[i] + "";
                fl.Font = bold;
                fl.FadeFromBackColor = Label.DefaultBackColor;
                fl.FadeFromForeColor = Label.DefaultBackColor;
                if (i == 4)
                    fl.FadeToBackColor = Color.Orange;
                else
                    fl.FadeToBackColor = Color.CornflowerBlue;
                fl.FadeToForeColor = Color.WhiteSmoke;
                fl.FadeDuration = 1000;
                fl.Fade();
            }
            guess.Text = "Press ENTER";
            guess.SelectionStart = guess.Text.Length;
            guess.SelectionLength = 0;
        }

        /// <summary>
        /// Starts a new game
        /// </summary>
        private void StartGame()
        {
            Random r = new Random(DateTime.Now.Millisecond);

            // Determine two random positions in the word to guess
            // NOTE: I had earlier used a naive approach to get the random
            // positions. However, that method meant randomPos2 = randomPos1 + 1
            // 20% of the time. The following snippet improves it to 16%.
            randomPos1 = r.Next(0, WordLength);
            randomPos2 = r.Next(0, WordLength - 1);
            if (randomPos2 == randomPos1)
            {
                randomPos2++;
            }

            // Get the random word to guess making sure its of
            // the right difficulty
            wordIndex = r.Next(0, words.Count);
            while (!IsWordMatchForDifficultyLevel(words[wordIndex], gameDifficulty))
            {
                wordIndex = r.Next(0, words.Count);
            }
            _currentPuzzleWord = words[wordIndex].Substring(2);

            // Reset the grid and display the inital random letters
            ResetGrid();

            initLayout.Controls[randomPos1].Text = CurrentPuzzleWord[randomPos1] + "";
            initLayout.Controls[randomPos2].Text = CurrentPuzzleWord[randomPos2] + "";

            // Set the tooltips for these letters
            SetLetterToolTip(initLayout.Controls[randomPos1], true);
            SetLetterToolTip(initLayout.Controls[randomPos2], true);

            // Highlight the first two letters
            FadeLabel fl = initLayout.Controls[randomPos1] as FadeLabel;
            fl.FadeToForeColor = Color.White;
            fl.FadeToBackColor = Color.CornflowerBlue;
            fl.Font = bold;
            fl.FadeDuration = fl.MaxTransitions;
            fl.Fade();
            fl = initLayout.Controls[randomPos2] as FadeLabel;
            fl.FadeToForeColor = Color.White;
            fl.FadeToBackColor = Color.CornflowerBlue;
            fl.Font = bold;
            fl.FadeDuration = fl.MaxTransitions;
            fl.Fade();

            currentAttempt = GameState.FirstAttempt;
            guess.Text = "";

            // Reset the history to an empty text entry
            history.Clear();
            history.Add(guess.Text);
            historyIndex = 1;

            // Mark the inital two letters as solved
            solved[randomPos1] = solved[randomPos2] = true;

            definitionLink.Visible = false;

            // Set up the bonus score and start the timer
            bonusScore = MaximumBonusScore;
            bonusTimer.Start();

            // Reset help given status
            helpGiven = false;

            // Reset the font color for the text box
            guess.ForeColor = TextBox.DefaultForeColor;
        }

        /// <summary>
        /// Loads the word list from the resource file
        /// </summary>
        private void InitializeWords()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            StreamReader r = new StreamReader(assembly.GetManifestResourceStream("Lingo.Words.Lingo.txt"));
            while (!r.EndOfStream)
            {
                words.Add(r.ReadLine());
            }
            r.Close();
        }

        /// <summary>
        /// The core game processing logic
        /// in a monolithic block of code
        /// Handles the key press event on the textbox
        /// where a guess is entered. The game logic is driven by
        /// the key that is pressed
        /// </summary>
        private void ProcessNormalKeys(object sender, KeyPressEventArgs e)
        {
            // We handle most of the key presses
            e.Handled = true;

            // Reset history index to the last position
            historyIndex = history.Count;

            // If we are displaying the initial Lingo screen
            // wait until we get an enter key press
            if (currentAttempt == GameState.InitialDisplay)
            {
                if (e.KeyChar != (char)Keys.Enter)
                    return;
                else
                {
                    StartGame();
                    return;
                }
            }

            // If escape was pressed once, display the actual word
            // If presesed twice, close the form
            if (e.KeyChar == (char)Keys.Escape)
            {
                if (currentAttempt == GameState.ViewingWord)
                {
                    this.Close();
                    return;
                }
                else
                {
                    helpGiven = true;
                    helpLetterTimer.Stop();
                    UpdateScoreWithPenalty();
                    DisplayOriginalWord();
                    return;
                }
            }

            // If escape ' key was pressed display the 
            // word to be guessed [Silly easter egg ;)]
            if (e.KeyChar == '7')
            {
                guess.Text = CurrentPuzzleWord;
                return;
            }

            // If the user has finished off all the chances,
            // do not allow to proceed without clicking the enter key
            if (currentAttempt > GameState.LastAttempt && (e.KeyChar != (char)Keys.Enter))
            {
                guess.ForeColor = Color.Gray;
                guess.Text = "Press ENTER";
                return;
            }

            // If enter key has been pressed without entering a five letter word
            // or backspace has been pressed, just return
            if ((e.KeyChar == (char)Keys.Enter && guess.Text.Length != WordLength && currentAttempt < GameState.MaximumAttemptsOver) || e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
                return;
            }

            // Ignore any keys other than a-z and the enter key at this stage
            if ((e.KeyChar.CompareTo('a') < 0 || e.KeyChar.CompareTo('z') > 0) && e.KeyChar != (char)Keys.Enter)
            {
                return;
            }
            // User has pressed a key after entering a five letter word
            // and has chances left to be guessing
            if (guess.Text.Length == WordLength && currentAttempt < GameState.MaximumAttemptsOver)
            {
                // If the key was not enter, return
                if (e.KeyChar != (char)Keys.Enter && guess.SelectionLength == 0)
                {
                    return;
                }
                if (guess.SelectionLength != 0)
                {
                    e.Handled = false;
                    return;
                }

                // Check if the word is valid
                if (!IsValidWord(guess.Text))
                {
                    // If the word is not valid, the player
                    // will miss a chance. The letters in the word
                    // will not be processed
                    errorLabel.Visible = true;
                    guess.SelectAll();
                    messageLabel.Text = null;

                    for (int i = 0; i < WordLength; i++)
                    {
                        layout.Controls[currentAttempt * WordLength + i].Text = guess.Text[i] + "";
                        layout.Controls[currentAttempt * WordLength + i].BackColor = Color.Gainsboro;
                        if (solved[i] && currentAttempt < GameState.LastAttempt)
                        {
                            layout.Controls[(currentAttempt + 1) * WordLength + i].Text = CurrentPuzzleWord[i] + "";
                        }
                    }
                    currentAttempt++;

                    return;
                }
                else
                {
                    // The word entered is valid
                    // save in the history
                    errorLabel.Visible = false;

                    // If the current word is not the same
                    // as the one entered, add it to the
                    // history list
                    if (history[historyIndex - 1] != guess.Text)
                    {
                        history.Add(guess.Text);
                        historyIndex = history.Count;
                    }
                }

                // Display the letters that were correct
                DisplayCorrectLetters();

                // Display the letters that are not in the right position
                DisplayProbableLetters();

                // Increment the count of attempts
                currentAttempt++;

                // If the word is correct, display it is
                // and start the game all over again
                if (guess.Text == CurrentPuzzleWord)
                {
                    guess.Text = "Correct!";
                    for (int i = 0; i < WordLength && currentAttempt < GameState.MaximumAttemptsOver; i++)
                    {
                        layout.Controls[currentAttempt * WordLength + i].Text = "";
                    }
                    definitionLink.Text = "What's " + CurrentPuzzleWord + "?";
                    definitionLink.Visible = true;

                    UpdateScore();

                    currentAttempt = GameState.ViewingWord;
                    helpLetterTimer.Stop();
                    return;
                }

                // Clear the current entry
                guess.Text = "";
                helpLetterTimer.Start();
                return;
            }
            else if (currentAttempt == GameState.MaximumAttemptsOver)
            {
                UpdateScoreWithPenalty();
                // If all guesses have been exhausted, display what the
                // right word was
                DisplayOriginalWord();

                return;
            }
            else if (currentAttempt > GameState.MaximumAttemptsOver)
            {
                // Start off a new game
                StartGame();
                return;
            }
            e.Handled = false;
        }

        /// <summary>
        /// Event handler to control up and down key presses to display
        /// attempt history
        /// </summary>
        private void ProcessPreviewKeys(object sender, PreviewKeyDownEventArgs e)
        {
            // Make sure we are in a game
            if (currentAttempt<GameState.FirstAttempt || currentAttempt > GameState.LastAttempt)
                return;

            // If the up or down arrow keys are pressed 
            // populate from history
            if (e.KeyCode == Keys.Up)
            {
                historyIndex = (history.Count + historyIndex - 1) % history.Count;
                guess.Text = history[historyIndex];
                guess.SelectionStart = guess.Text.Length;
                return;
            }
            if (e.KeyCode == Keys.Down)
            {
                historyIndex = (historyIndex + 1) % history.Count;
                guess.Text = history[historyIndex];
                guess.SelectionStart = guess.Text.Length;
                return;
            }
        }

        /// <summary>
        /// The moment a key up is detected, the help timer
        /// will be restarted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guess_KeyUp(object sender, KeyEventArgs e)
        {
            if (!helpGiven && currentAttempt > GameState.FirstAttempt)
            {
                helpLetterTimer.Stop();
                helpLetterTimer.Start();
            }
        }

        /// <summary>
        /// Routine to mark letters in the guessed word that are
        /// also in the original word, but not in the exact position
        /// </summary>
        private void DisplayProbableLetters()
        {
            // If the current letter is present elsewhere in
            // the word to be guessed, we mark them out in red
            int labelPosition = 0;
            for (int i = 0; i < WordLength; i++)
            {
                if (marked[i] && guess.Text[i] == CurrentPuzzleWord[i])
                {
                    continue;
                }
                for (int j = 0; j < WordLength; j++)
                {
                    if (guess.Text[i] == CurrentPuzzleWord[j])
                    {
                        //if (marked[i] && guess.Text[i] == words[wordIndex][i])
                        //{
                        //    break;
                        //}
                        if (!marked[j])
                        {
                            marked[j] = true;
                            labelPosition = currentAttempt * WordLength + i;
                            FadeLabel fl = layout.Controls[labelPosition] as FadeLabel;
                            fl.FadeToBackColor = Color.OrangeRed;
                            fl.FadeToForeColor = Color.WhiteSmoke;
                            fl.FadeFromForeColor = Label.DefaultBackColor;
                            fl.FadeFromBackColor = Label.DefaultBackColor;
                            fl.Fade();
                            layout.Controls[labelPosition].Font = bold;
                            SetLetterToolTip(layout.Controls[labelPosition], false);
                            break;
                        }
                        else if (CurrentPuzzleWord.Substring(j + 1).IndexOf(guess.Text[i]) >= 0)
                        {
                            continue;
                        }
                        else
                            break;
                    }
                }
            }
        }
        private void DisplayCorrectLetters()
        {
            // First scan all the letters and determine all that have been solved
            int labelPosition = 0;
            lock (solved)
            {
                for (int i = 0; i < WordLength; i++)
                {
                    labelPosition = currentAttempt * WordLength + i;

                    // Display the word guessed at the correct place in the grid
                    layout.Controls[labelPosition].Text = guess.Text[i] + "";

                    // If the letter matches
                    if (guess.Text[i] == CurrentPuzzleWord[i])
                    {
                        // Mark that letter has been solved
                        solved[i] = true;

                        // Show the solved letter in green
                        FadeLabel fl = layout.Controls[labelPosition] as FadeLabel;
                        fl.FadeFromForeColor = Label.DefaultBackColor;
                        fl.FadeFromBackColor = Label.DefaultBackColor;
                        fl.FadeToBackColor = Color.GreenYellow;
                        fl.FadeFromForeColor = Label.DefaultForeColor;
                        fl.Fade();
                        layout.Controls[labelPosition].Font = bold;
                        SetLetterToolTip(layout.Controls[labelPosition], true);
                    }

                    // Now display the letter that were solved so 
                    // far in the next line
                    if (currentAttempt < GameState.LastAttempt && solved[i])
                    {
                        layout.Controls[labelPosition + WordLength].Text = CurrentPuzzleWord[i] + "";
                        SetLetterToolTip(layout.Controls[labelPosition + WordLength], true);
                    }

                    // Mark only if the letters are the same
                    // It may be solved, but not marked
                    marked[i] = (guess.Text[i] == CurrentPuzzleWord[i]);
                }
            }
        }
        private void SetLetterToolTip(Control control, bool? isPositionCorrect)
        {
            if (null == isPositionCorrect)
            {
                letterTip.SetToolTip(control, null);
                return;
            }

            string message;
            if (isPositionCorrect.Value)
            {
                message = string.Format("'{0}' is in the right position", control.Text);
            }
            else
            {
                message = string.Format("'{0}' appears elsewhere", control.Text);
            }
            letterTip.SetToolTip(control, message);
        }

        /// <summary>
        /// Initializes the main form
        /// </summary>
        private void InitiailizeForm()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Lingo));
            layout.Controls.Clear();
            
            // Set up the grid
            for (int x = 0; x < WordLength * WordLength; x++)
            {
                FadeLabel l = new FadeLabel();
                l.Width = 27;
                l.Text = "";
                l.FadeDuration = 50;
                l.TextAlign = ContentAlignment.MiddleCenter;
                l.BackColor = ControlPaint.LightLight(Label.DefaultBackColor);
                l.Margin = new Padding(0);
                l.Padding = new Padding(0);
                l.FadeFromBackColor = Label.DefaultBackColor;
                l.FadeFromForeColor = Label.DefaultBackColor;
                l.Font = regular;
                layout.Controls.Add(l);
            }

            // Set up the top container
            for (int x = 0; x < WordLength; x++)
            {
                FadeLabel l = new FadeLabel();
                l.Width = 26;
                l.Text = "";
                l.TextAlign = ContentAlignment.MiddleCenter;
                l.BackColor = ControlPaint.LightLight(Label.DefaultBackColor);
                l.Margin = new Padding(1, 0, 1, 0);
                l.Font = regular;
                initLayout.Controls.Add(l);
            }

            // Assign the context menu
            ContextMenuStrip = contextMenuStrip;

            // Hook up a bonus timer handler
            bonusTimer.Elapsed += new ElapsedEventHandler(ReduceBonusScore);
            bonusTimer.Interval = BonusTimerInterval;


            // Create the color map
            int ColorMapFactor = (int)(255F / (float)ColorMap.Length);
            for (int i = 0; i < ColorMap.Length; i++)
            {
                ColorMap[i] = Color.FromArgb(
                    255 - i * ColorMapFactor,
                    20 + i * ColorMapFactor,
                    0);
            }
        }
        
        private void ResetGrid()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Lingo));
            for (int x = 0; x < WordLength; x++)
            {
                // Clear off the solved and marked flags
                solved[x] = marked[x] = false;

                // Clear off the top labels
                FadeLabel fl = initLayout.Controls[x] as FadeLabel;
                fl.Text = "";
                fl.ForeColor = Label.DefaultBackColor;
                fl.BackColor = Label.DefaultBackColor;
                fl.FadeToBackColor = Label.DefaultBackColor;
                fl.FadeToForeColor = Label.DefaultBackColor;
                fl.Font = regular;
                fl.Fade();

                // Clear off the grid
                for (int y = 0; y < WordLength; y++)
                {
                    FadeLabel l = layout.Controls[x * WordLength + y] as FadeLabel;
                    l.Text = "";
                    l.ForeColor = Label.DefaultForeColor;
                    l.BackColor = Label.DefaultBackColor;
                    l.FadeToBackColor = Label.DefaultBackColor;
                    l.FadeToForeColor = Label.DefaultForeColor;
                    l.Fade();
                    l.Font = regular;
                    SetLetterToolTip(l, null);
                }
            }
        }
        private void QuitGame(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ChangeDifficultyLevel(object sender, EventArgs e)
        {
            // Determine the game difficulty level that was chosen
            string option = ((ToolStripMenuItem)sender).Tag as string;
            DifficultyLevel newLevel = DifficultyLevel.Easy;
            switch (option)
            {
                case "Easy":
                    newLevel = DifficultyLevel.Easy;
                    break;
                case "Medium":
                    newLevel = DifficultyLevel.Medium;
                    break;
                case "Hard":
                    newLevel = DifficultyLevel.Hard;
                    break;
            }

            // If a new difficulty level was chosen,
            // we have to start a new game
            if (gameDifficulty != newLevel)
            {
                gameDifficulty = newLevel;

                // Check the appropriate difficulty level
                easyToolStripMenuItem.Checked = (gameDifficulty == DifficultyLevel.Easy);
                mediumToolStripMenuItem.Checked = (gameDifficulty == DifficultyLevel.Medium);
                hardToolStripMenuItem.Checked = (gameDifficulty == DifficultyLevel.Hard);

                StartGame();
            }
        }
        private void DisplayAbout(object sender, EventArgs e)
        {
            AboutLingo aboutLingo = new AboutLingo();
            aboutLingo.ShowDialog(this);
        }
        private void definitionLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (definitionLink.Text == "What's Wordl?")
                {
                    DisplayAbout(sender, e);
                    return;
                }
            }
            catch { }
            finally
            {
                guess.Focus();
            }
        }
        private void DisplayOriginalWord()
        {
            guess.ForeColor = TextBox.DefaultForeColor;
            guess.Text = "Word was: " + CurrentPuzzleWord;
            definitionLink.Text = "What's " + CurrentPuzzleWord + "?";
            definitionLink.Visible = true;
            errorLabel.Visible = false;
            messageLabel.Text = null;
            currentAttempt = GameState.ViewingWord;
            bonusTimer.Stop();
        }
        private bool IsValidWord(string word)
        {
            if (!words.Contains(EasyWordPrefix + word) &&
                !words.Contains(MediumWordPrefix + word) &&
                !words.Contains(HardWordPrefix + word))
            {
                return false;
            }
            return true;
        }
        private bool IsWordMatchForDifficultyLevel(string word, DifficultyLevel difficultyLevel)
        {
            if (word.StartsWith(EasyWordPrefix) && difficultyLevel == DifficultyLevel.Easy)
            {
                return true;
            }
            if (word.StartsWith(MediumWordPrefix) && difficultyLevel == DifficultyLevel.Medium)
            {
                return true;
            }
            if (word.StartsWith(HardWordPrefix) && difficultyLevel == DifficultyLevel.Hard)
            {
                return true;
            }
            return false;
        }
        private void UpdateScoreWithPenalty()
        {
            // Update and display the score
            score -= (int)(score * 0.10); // heavy penalty :)
            score = score < 0 ? 0 : score;
            scoreLabel.Text = string.Format("{0:#,###,###;No Score;No Score}", score);
        }
        private void UpdateScore()
        {
            // Update and display the score
            score += (GameState.MaximumAttemptsOver - currentAttempt + 1) * 100 + bonusScore;
            scoreLabel.Text = string.Format("{0:#,###,###}", score);
            
            // Stop the bonus timer
            bonusTimer.Stop();

            // Clear off any messages
            messageLabel.Text = null;
        }
        void ReduceBonusScore(object sender, ElapsedEventArgs e)
        {
            bonusScore--;
            if (bonusScore == 0)
            {
                bonusTimer.Stop();
            }
            // Set the bonus score in a thread safe manner
            try
            {
                if (this.InvokeRequired)
                {
                    PerformActivityCallback d = new PerformActivityCallback(SetBonusScore);
                    this.Invoke(d);
                }
                else
                {
                    SetBonusScore();
                }
            }
            // bad way to handle, but when closing the form, its thrown
            catch (ObjectDisposedException) { }
        }
        void SetBonusScore()
        {
            bonusScoreLabel.Text = string.Format("{0:000}", bonusScore);

            // Set the color to a vary as a gradient from green
            // to red as bonus score gets reduced
            bonusScoreLabel.ForeColor = ColorMap[(int)(bonusScore * ColorMapIndexFactor)];
        }
    }
}
