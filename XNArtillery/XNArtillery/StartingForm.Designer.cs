namespace XNArtillery
{
    partial class StartingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartingForm));
            this.start = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.name2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.name1 = new System.Windows.Forms.TextBox();
            this.ability1 = new System.Windows.Forms.TrackBar();
            this.demo = new System.Windows.Forms.RadioButton();
            this.multi = new System.Windows.Forms.RadioButton();
            this.single = new System.Windows.Forms.RadioButton();
            this.windPower = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ability2 = new System.Windows.Forms.TrackBar();
            this.resolution = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.isFullScreen = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.level = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ability1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.windPower)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ability2)).BeginInit();
            this.SuspendLayout();
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(12, 40);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(100, 50);
            this.start.TabIndex = 0;
            this.start.Text = "Avvia la partita";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.startGame);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.level);
            this.groupBox1.Controls.Add(this.isFullScreen);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.resolution);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.ability2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.windPower);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.name2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.name1);
            this.groupBox1.Controls.Add(this.ability1);
            this.groupBox1.Controls.Add(this.demo);
            this.groupBox1.Controls.Add(this.multi);
            this.groupBox1.Controls.Add(this.single);
            this.groupBox1.Location = new System.Drawing.Point(118, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(470, 187);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Impostazioni";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(308, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Abilità G1:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(121, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Nickname G2:";
            // 
            // name2
            // 
            this.name2.Enabled = false;
            this.name2.Location = new System.Drawing.Point(202, 108);
            this.name2.Name = "name2";
            this.name2.Size = new System.Drawing.Size(100, 20);
            this.name2.TabIndex = 6;
            this.name2.Text = "CPU";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(121, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Nickname G1:";
            // 
            // name1
            // 
            this.name1.Location = new System.Drawing.Point(202, 82);
            this.name1.Name = "name1";
            this.name1.Size = new System.Drawing.Size(100, 20);
            this.name1.TabIndex = 4;
            this.name1.Text = "Giocatore";
            // 
            // ability1
            // 
            this.ability1.Enabled = false;
            this.ability1.Location = new System.Drawing.Point(365, 85);
            this.ability1.Maximum = 100;
            this.ability1.Minimum = 1;
            this.ability1.Name = "ability1";
            this.ability1.Size = new System.Drawing.Size(100, 45);
            this.ability1.TabIndex = 3;
            this.ability1.TickFrequency = 10;
            this.ability1.Value = 50;
            // 
            // demo
            // 
            this.demo.AutoSize = true;
            this.demo.Location = new System.Drawing.Point(6, 135);
            this.demo.Name = "demo";
            this.demo.Size = new System.Drawing.Size(96, 17);
            this.demo.TabIndex = 2;
            this.demo.Text = "Modalità Demo";
            this.demo.UseVisualStyleBackColor = true;
            this.demo.CheckedChanged += new System.EventHandler(this.demo_CheckedChanged);
            // 
            // multi
            // 
            this.multi.AutoSize = true;
            this.multi.Location = new System.Drawing.Point(6, 109);
            this.multi.Name = "multi";
            this.multi.Size = new System.Drawing.Size(91, 17);
            this.multi.TabIndex = 1;
            this.multi.Text = "Multigiocatore";
            this.multi.UseVisualStyleBackColor = true;
            this.multi.CheckedChanged += new System.EventHandler(this.multi_CheckedChanged);
            // 
            // single
            // 
            this.single.AutoSize = true;
            this.single.Checked = true;
            this.single.Location = new System.Drawing.Point(6, 83);
            this.single.Name = "single";
            this.single.Size = new System.Drawing.Size(109, 17);
            this.single.TabIndex = 0;
            this.single.TabStop = true;
            this.single.Text = "Giocatore Singolo";
            this.single.UseVisualStyleBackColor = true;
            this.single.CheckedChanged += new System.EventHandler(this.single_CheckedChanged);
            // 
            // windPower
            // 
            this.windPower.Location = new System.Drawing.Point(214, 32);
            this.windPower.Maximum = 100;
            this.windPower.Name = "windPower";
            this.windPower.Size = new System.Drawing.Size(250, 45);
            this.windPower.TabIndex = 11;
            this.windPower.TickFrequency = 10;
            this.windPower.Value = 50;
            this.windPower.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(298, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Influenza vento:";
            this.label4.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(308, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Abilità G2:";
            // 
            // ability2
            // 
            this.ability2.Location = new System.Drawing.Point(365, 111);
            this.ability2.Maximum = 100;
            this.ability2.Minimum = 1;
            this.ability2.Name = "ability2";
            this.ability2.Size = new System.Drawing.Size(100, 45);
            this.ability2.TabIndex = 16;
            this.ability2.TickFrequency = 10;
            this.ability2.Value = 50;
            // 
            // resolution
            // 
            this.resolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.resolution.FormattingEnabled = true;
            this.resolution.Items.AddRange(new object[] {
            "800 x 600",
            "1024 x 768",
            "1152 x 864",
            "1176 x 664",
            "1280 x 720",
            "1280 x 768",
            "1280 x 800",
            "1280 x 960",
            "1280 x 1024",
            "1360 x 768",
            "1366 x 768",
            "1600 x 900",
            "1600 x 1024",
            "1680 x 1050",
            "1768 x 992",
            "1920 x 1080"});
            this.resolution.Location = new System.Drawing.Point(213, 160);
            this.resolution.Name = "resolution";
            this.resolution.Size = new System.Drawing.Size(85, 21);
            this.resolution.TabIndex = 18;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(143, 163);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Risoluzione:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 122);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 50);
            this.button1.TabIndex = 2;
            this.button1.Text = "Esci dal gioco";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // isFullScreen
            // 
            this.isFullScreen.AutoSize = true;
            this.isFullScreen.Location = new System.Drawing.Point(366, 162);
            this.isFullScreen.Name = "isFullScreen";
            this.isFullScreen.Size = new System.Drawing.Size(98, 17);
            this.isFullScreen.TabIndex = 20;
            this.isFullScreen.Text = "Schermo Intero";
            this.isFullScreen.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Livello:";
            // 
            // level
            // 
            this.level.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.level.FormattingEnabled = true;
            this.level.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.level.Items.AddRange(new object[] {
            "Collina (Lv. 1)",
            "Altopiano a destra (Lv. 2)",
            "Altopiano a sinistra (Lv. 3)",
            "Montagna (Lv. 4)"});
            this.level.Location = new System.Drawing.Point(51, 32);
            this.level.Name = "level";
            this.level.Size = new System.Drawing.Size(145, 21);
            this.level.TabIndex = 21;
            // 
            // StartingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 211);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.start);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "StartingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "XNArtillery";
            this.Load += new System.EventHandler(this.StartingForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ability1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.windPower)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ability2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button start;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox name1;
        private System.Windows.Forms.TrackBar ability1;
        private System.Windows.Forms.RadioButton demo;
        private System.Windows.Forms.RadioButton multi;
        private System.Windows.Forms.RadioButton single;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox name2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar windPower;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox resolution;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar ability2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox isFullScreen;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox level;
    }
}