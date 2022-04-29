/*********************************************
 * Lingo
 * A simple word game
 * 
 * Benzi K. Ahamed
 * 2007
 * http://www.codeproject.com/useritems/Lingo__a_simple_word_game.asp
 * ******************************************/

namespace Lingo
{
    partial class Lingo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Lingo));
            this.layout = new System.Windows.Forms.TableLayoutPanel();
            this.guess = new System.Windows.Forms.TextBox();
            this.errorLabel = new System.Windows.Forms.Label();
            this.guessTip = new System.Windows.Forms.ToolTip(this.components);
            this.letterTip = new System.Windows.Forms.ToolTip(this.components);
            this.definitionLink = new System.Windows.Forms.LinkLabel();
            this.scoreLabel = new System.Windows.Forms.Label();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.difficultyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.easyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mediumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.initLayout = new System.Windows.Forms.TableLayoutPanel();
            this.bonusScoreLabel = new System.Windows.Forms.Label();
            this.bonusLabel = new System.Windows.Forms.Label();
            this.gridBox = new System.Windows.Forms.GroupBox();
            this.guessBox = new System.Windows.Forms.GroupBox();
            this.messageLabel = new System.Windows.Forms.Label();
            this.contextMenuStrip.SuspendLayout();
            this.gridBox.SuspendLayout();
            this.guessBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // layout
            // 
            this.layout.AutoSize = true;
            this.layout.BackColor = System.Drawing.SystemColors.Control;
            this.layout.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.layout.ColumnCount = 5;
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.layout.Cursor = System.Windows.Forms.Cursors.Default;
            this.layout.Location = new System.Drawing.Point(12, 62);
            this.layout.Margin = new System.Windows.Forms.Padding(0);
            this.layout.Name = "layout";
            this.layout.RowCount = 5;
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layout.Size = new System.Drawing.Size(141, 121);
            this.layout.TabIndex = 0;
            // 
            // guess
            // 
            this.guess.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.guess.BackColor = System.Drawing.SystemColors.Window;
            this.guess.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.guess.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.guess.Font = new System.Drawing.Font("Georgia", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guess.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.guess.Location = new System.Drawing.Point(12, 19);
            this.guess.Name = "guess";
            this.guess.Size = new System.Drawing.Size(141, 22);
            this.guess.TabIndex = 1;
            this.guess.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.guessTip.SetToolTip(this.guess, "Enter a five letter word\r\nhere as your guess");
            this.guess.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.ProcessPreviewKeys);
            this.guess.KeyUp += new System.Windows.Forms.KeyEventHandler(this.guess_KeyUp);
            this.guess.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ProcessNormalKeys);
            // 
            // errorLabel
            // 
            this.errorLabel.AutoSize = true;
            this.errorLabel.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.errorLabel.Location = new System.Drawing.Point(51, 45);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(102, 16);
            this.errorLabel.TabIndex = 3;
            this.errorLabel.Text = "That\'s not a word!";
            this.errorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.errorLabel.Visible = false;
            // 
            // guessTip
            // 
            this.guessTip.IsBalloon = true;
            this.guessTip.ToolTipTitle = "Enter you guess here";
            // 
            // definitionLink
            // 
            this.definitionLink.ActiveLinkColor = System.Drawing.Color.Red;
            this.definitionLink.AutoSize = true;
            this.definitionLink.Cursor = System.Windows.Forms.Cursors.Help;
            this.definitionLink.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.definitionLink.LinkColor = System.Drawing.Color.CornflowerBlue;
            this.definitionLink.Location = new System.Drawing.Point(71, 44);
            this.definitionLink.Name = "definitionLink";
            this.definitionLink.Size = new System.Drawing.Size(82, 16);
            this.definitionLink.TabIndex = 5;
            this.definitionLink.TabStop = true;
            this.definitionLink.Text = "What\'s Lingo?";
            this.definitionLink.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.letterTip.SetToolTip(this.definitionLink, "Click here for a definition");
            this.definitionLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.definitionLink_LinkClicked);
            // 
            // scoreLabel
            // 
            this.scoreLabel.AutoSize = true;
            this.scoreLabel.Font = new System.Drawing.Font("Lucida Fax", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scoreLabel.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.scoreLabel.Location = new System.Drawing.Point(94, 12);
            this.scoreLabel.Margin = new System.Windows.Forms.Padding(0);
            this.scoreLabel.MinimumSize = new System.Drawing.Size(45, 0);
            this.scoreLabel.Name = "scoreLabel";
            this.scoreLabel.Size = new System.Drawing.Size(65, 16);
            this.scoreLabel.TabIndex = 4;
            this.scoreLabel.Text = "No Score";
            this.scoreLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.difficultyToolStripMenuItem,
            this.toolStripSeparator1,
            this.aboutToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(123, 76);
            // 
            // difficultyToolStripMenuItem
            // 
            this.difficultyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.easyToolStripMenuItem,
            this.mediumToolStripMenuItem,
            this.hardToolStripMenuItem});
            this.difficultyToolStripMenuItem.Name = "difficultyToolStripMenuItem";
            this.difficultyToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.difficultyToolStripMenuItem.Text = "Difficulty";
            // 
            // easyToolStripMenuItem
            // 
            this.easyToolStripMenuItem.Checked = true;
            this.easyToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.easyToolStripMenuItem.Image = global::Lingo.Properties.Resources.emoticon_smile;
            this.easyToolStripMenuItem.Name = "easyToolStripMenuItem";
            this.easyToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.easyToolStripMenuItem.Tag = "Easy";
            this.easyToolStripMenuItem.Text = "Easy";
            this.easyToolStripMenuItem.Click += new System.EventHandler(this.ChangeDifficultyLevel);
            // 
            // mediumToolStripMenuItem
            // 
            this.mediumToolStripMenuItem.Image = global::Lingo.Properties.Resources.emoticon_surprised;
            this.mediumToolStripMenuItem.Name = "mediumToolStripMenuItem";
            this.mediumToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.mediumToolStripMenuItem.Tag = "Medium";
            this.mediumToolStripMenuItem.Text = "Medium";
            this.mediumToolStripMenuItem.Click += new System.EventHandler(this.ChangeDifficultyLevel);
            // 
            // hardToolStripMenuItem
            // 
            this.hardToolStripMenuItem.Image = global::Lingo.Properties.Resources.emoticon_unhappy;
            this.hardToolStripMenuItem.Name = "hardToolStripMenuItem";
            this.hardToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.hardToolStripMenuItem.Tag = "Hard";
            this.hardToolStripMenuItem.Text = "Hard";
            this.hardToolStripMenuItem.Click += new System.EventHandler(this.ChangeDifficultyLevel);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(119, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Image = global::Lingo.Properties.Resources.house;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.DisplayAbout);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Image = global::Lingo.Properties.Resources.control_stop_blue;
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.QuitGame);
            // 
            // initLayout
            // 
            this.initLayout.ColumnCount = 5;
            this.initLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.initLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.initLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.initLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.initLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 141F));
            this.initLayout.Location = new System.Drawing.Point(12, 39);
            this.initLayout.Margin = new System.Windows.Forms.Padding(0);
            this.initLayout.Name = "initLayout";
            this.initLayout.RowCount = 1;
            this.initLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 141F));
            this.initLayout.Size = new System.Drawing.Size(141, 25);
            this.initLayout.TabIndex = 6;
            // 
            // bonusScoreLabel
            // 
            this.bonusScoreLabel.AutoSize = true;
            this.bonusScoreLabel.Font = new System.Drawing.Font("Microsoft YaHei", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bonusScoreLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.bonusScoreLabel.Location = new System.Drawing.Point(127, 186);
            this.bonusScoreLabel.Name = "bonusScoreLabel";
            this.bonusScoreLabel.Size = new System.Drawing.Size(13, 14);
            this.bonusScoreLabel.TabIndex = 7;
            this.bonusScoreLabel.Text = "0";
            // 
            // bonusLabel
            // 
            this.bonusLabel.AutoSize = true;
            this.bonusLabel.Font = new System.Drawing.Font("Microsoft YaHei", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bonusLabel.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.bonusLabel.Location = new System.Drawing.Point(94, 186);
            this.bonusLabel.Margin = new System.Windows.Forms.Padding(0);
            this.bonusLabel.Name = "bonusLabel";
            this.bonusLabel.Size = new System.Drawing.Size(37, 14);
            this.bonusLabel.TabIndex = 8;
            this.bonusLabel.Text = "Bonus:";
            // 
            // gridBox
            // 
            this.gridBox.Controls.Add(this.layout);
            this.gridBox.Controls.Add(this.bonusScoreLabel);
            this.gridBox.Controls.Add(this.initLayout);
            this.gridBox.Controls.Add(this.bonusLabel);
            this.gridBox.Controls.Add(this.scoreLabel);
            this.gridBox.Location = new System.Drawing.Point(13, 7);
            this.gridBox.Name = "gridBox";
            this.gridBox.Size = new System.Drawing.Size(166, 209);
            this.gridBox.TabIndex = 10;
            this.gridBox.TabStop = false;
            // 
            // guessBox
            // 
            this.guessBox.Controls.Add(this.messageLabel);
            this.guessBox.Controls.Add(this.guess);
            this.guessBox.Controls.Add(this.definitionLink);
            this.guessBox.Controls.Add(this.errorLabel);
            this.guessBox.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guessBox.Location = new System.Drawing.Point(13, 222);
            this.guessBox.Name = "guessBox";
            this.guessBox.Size = new System.Drawing.Size(166, 64);
            this.guessBox.TabIndex = 11;
            this.guessBox.TabStop = false;
            this.guessBox.Text = "Your guess:";
            // 
            // messageLabel
            // 
            this.messageLabel.AutoSize = true;
            this.messageLabel.Font = new System.Drawing.Font("Microsoft YaHei", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messageLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.messageLabel.Location = new System.Drawing.Point(12, 44);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(0, 14);
            this.messageLabel.TabIndex = 6;
            // 
            // Lingo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(191, 296);
            this.Controls.Add(this.gridBox);
            this.Controls.Add(this.guessBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Lingo";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lingo";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ProcessNormalKeys);
            this.contextMenuStrip.ResumeLayout(false);
            this.gridBox.ResumeLayout(false);
            this.gridBox.PerformLayout();
            this.guessBox.ResumeLayout(false);
            this.guessBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel layout;
        private System.Windows.Forms.TextBox guess;
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.ToolTip guessTip;
        private System.Windows.Forms.ToolTip letterTip;
        private System.Windows.Forms.Label scoreLabel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem difficultyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem easyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mediumToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.LinkLabel definitionLink;
        private System.Windows.Forms.TableLayoutPanel initLayout;
        private System.Windows.Forms.Label bonusScoreLabel;
        private System.Windows.Forms.Label bonusLabel;
        private System.Windows.Forms.GroupBox gridBox;
        private System.Windows.Forms.GroupBox guessBox;
        private System.Windows.Forms.Label messageLabel;
    }
}

