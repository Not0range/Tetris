namespace Tetris
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.SettingsButton = new System.Windows.Forms.Button();
            this.RulesButton = new System.Windows.Forms.Button();
            this.SoundSwitcher = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.PlayGame = new System.Windows.Forms.Button();
            this.LeaderButton = new System.Windows.Forms.Button();
            this.Grid = new System.Windows.Forms.PictureBox();
            this.PreviewGrid = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.FallTick = new System.Windows.Forms.Timer(this.components);
            this.ScoreLabel = new System.Windows.Forms.Label();
            this.TimeLabel = new System.Windows.Forms.Label();
            this.Score = new System.Windows.Forms.Label();
            this.Time = new System.Windows.Forms.Label();
            this.Counter = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PreviewGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // SettingsButton
            // 
            this.SettingsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SettingsButton.BackColor = System.Drawing.Color.LightBlue;
            this.SettingsButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("SettingsButton.BackgroundImage")));
            this.SettingsButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.SettingsButton.Location = new System.Drawing.Point(526, 13);
            this.SettingsButton.Margin = new System.Windows.Forms.Padding(4);
            this.SettingsButton.Name = "SettingsButton";
            this.SettingsButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.SettingsButton.Size = new System.Drawing.Size(45, 45);
            this.SettingsButton.TabIndex = 3;
            this.toolTip1.SetToolTip(this.SettingsButton, "Настройки");
            this.SettingsButton.UseMnemonic = false;
            this.SettingsButton.UseVisualStyleBackColor = false;
            this.SettingsButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // RulesButton
            // 
            this.RulesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RulesButton.BackColor = System.Drawing.Color.LightBlue;
            this.RulesButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("RulesButton.BackgroundImage")));
            this.RulesButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.RulesButton.Location = new System.Drawing.Point(526, 66);
            this.RulesButton.Margin = new System.Windows.Forms.Padding(4);
            this.RulesButton.Name = "RulesButton";
            this.RulesButton.Size = new System.Drawing.Size(45, 45);
            this.RulesButton.TabIndex = 4;
            this.toolTip1.SetToolTip(this.RulesButton, "Правила");
            this.RulesButton.UseMnemonic = false;
            this.RulesButton.UseVisualStyleBackColor = false;
            this.RulesButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // SoundSwitcher
            // 
            this.SoundSwitcher.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SoundSwitcher.BackColor = System.Drawing.Color.LightBlue;
            this.SoundSwitcher.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("SoundSwitcher.BackgroundImage")));
            this.SoundSwitcher.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.SoundSwitcher.Location = new System.Drawing.Point(526, 119);
            this.SoundSwitcher.Margin = new System.Windows.Forms.Padding(4);
            this.SoundSwitcher.Name = "SoundSwitcher";
            this.SoundSwitcher.Size = new System.Drawing.Size(45, 45);
            this.SoundSwitcher.TabIndex = 5;
            this.toolTip1.SetToolTip(this.SoundSwitcher, "Звук");
            this.SoundSwitcher.UseMnemonic = false;
            this.SoundSwitcher.UseVisualStyleBackColor = false;
            this.SoundSwitcher.Click += new System.EventHandler(this.button3_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.BackColor = System.Drawing.Color.LightBlue;
            this.toolTip1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.toolTip1.IsBalloon = true;
            this.toolTip1.StripAmpersands = true;
            // 
            // PlayGame
            // 
            this.PlayGame.ForeColor = System.Drawing.Color.Black;
            this.PlayGame.Location = new System.Drawing.Point(526, 172);
            this.PlayGame.Name = "PlayGame";
            this.PlayGame.Size = new System.Drawing.Size(45, 45);
            this.PlayGame.TabIndex = 13;
            this.PlayGame.Text = "GO";
            this.toolTip1.SetToolTip(this.PlayGame, "Начать игру");
            this.PlayGame.UseVisualStyleBackColor = true;
            this.PlayGame.Click += new System.EventHandler(this.PlayGame_Click);
            // 
            // LeaderButton
            // 
            this.LeaderButton.ForeColor = System.Drawing.Color.Black;
            this.LeaderButton.Location = new System.Drawing.Point(526, 223);
            this.LeaderButton.Name = "LeaderButton";
            this.LeaderButton.Size = new System.Drawing.Size(45, 45);
            this.LeaderButton.TabIndex = 14;
            this.LeaderButton.Text = "L";
            this.toolTip1.SetToolTip(this.LeaderButton, "Таблица лидеров");
            this.LeaderButton.UseVisualStyleBackColor = true;
            this.LeaderButton.Click += new System.EventHandler(this.LeaderButton_Click);
            // 
            // Grid
            // 
            this.Grid.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Grid.Location = new System.Drawing.Point(13, 13);
            this.Grid.Name = "Grid";
            this.Grid.Size = new System.Drawing.Size(300, 600);
            this.Grid.TabIndex = 6;
            this.Grid.TabStop = false;
            this.Grid.Paint += new System.Windows.Forms.PaintEventHandler(this.Grid_Paint);
            // 
            // PreviewGrid
            // 
            this.PreviewGrid.Location = new System.Drawing.Point(330, 463);
            this.PreviewGrid.Name = "PreviewGrid";
            this.PreviewGrid.Size = new System.Drawing.Size(210, 150);
            this.PreviewGrid.TabIndex = 7;
            this.PreviewGrid.TabStop = false;
            this.PreviewGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.PreviewGrid_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(325, 434);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(205, 26);
            this.label1.TabIndex = 8;
            this.label1.Text = "Следующая фигура:";
            // 
            // FallTick
            // 
            this.FallTick.Interval = 1500;
            this.FallTick.Tick += new System.EventHandler(this.Fall_Tick);
            // 
            // ScoreLabel
            // 
            this.ScoreLabel.AutoSize = true;
            this.ScoreLabel.ForeColor = System.Drawing.Color.Blue;
            this.ScoreLabel.Location = new System.Drawing.Point(319, 13);
            this.ScoreLabel.Name = "ScoreLabel";
            this.ScoreLabel.Size = new System.Drawing.Size(69, 26);
            this.ScoreLabel.TabIndex = 9;
            this.ScoreLabel.Text = "Очки:";
            // 
            // TimeLabel
            // 
            this.TimeLabel.AutoSize = true;
            this.TimeLabel.ForeColor = System.Drawing.Color.Blue;
            this.TimeLabel.Location = new System.Drawing.Point(319, 97);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(77, 26);
            this.TimeLabel.TabIndex = 10;
            this.TimeLabel.Text = "Время:";
            // 
            // Score
            // 
            this.Score.AutoSize = true;
            this.Score.Font = new System.Drawing.Font("Palatino Linotype", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Score.ForeColor = System.Drawing.Color.Blue;
            this.Score.Location = new System.Drawing.Point(324, 39);
            this.Score.Name = "Score";
            this.Score.Size = new System.Drawing.Size(27, 32);
            this.Score.TabIndex = 11;
            this.Score.Text = "0";
            // 
            // Time
            // 
            this.Time.AutoSize = true;
            this.Time.Font = new System.Drawing.Font("Palatino Linotype", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Time.ForeColor = System.Drawing.Color.Blue;
            this.Time.Location = new System.Drawing.Point(324, 132);
            this.Time.Name = "Time";
            this.Time.Size = new System.Drawing.Size(69, 32);
            this.Time.TabIndex = 12;
            this.Time.Text = "00:00";
            // 
            // Counter
            // 
            this.Counter.Interval = 1000;
            this.Counter.Tick += new System.EventHandler(this.Counter_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 26F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(584, 662);
            this.Controls.Add(this.LeaderButton);
            this.Controls.Add(this.PlayGame);
            this.Controls.Add(this.Time);
            this.Controls.Add(this.Score);
            this.Controls.Add(this.TimeLabel);
            this.Controls.Add(this.ScoreLabel);
            this.Controls.Add(this.SettingsButton);
            this.Controls.Add(this.RulesButton);
            this.Controls.Add(this.SoundSwitcher);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PreviewGrid);
            this.Controls.Add(this.Grid);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Palatino Linotype", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Тетрис";
            this.TransparencyKey = System.Drawing.Color.White;
            this.Load += new System.EventHandler(this.Form1_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PreviewGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SettingsButton;
        private System.Windows.Forms.Button RulesButton;
        private System.Windows.Forms.Button SoundSwitcher;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.PictureBox Grid;
        private System.Windows.Forms.PictureBox PreviewGrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer FallTick;
        private System.Windows.Forms.Label ScoreLabel;
        private System.Windows.Forms.Label TimeLabel;
        private System.Windows.Forms.Label Score;
        private System.Windows.Forms.Label Time;
        private System.Windows.Forms.Timer Counter;
        private System.Windows.Forms.Button PlayGame;
        private System.Windows.Forms.Button LeaderButton;
    }
}

