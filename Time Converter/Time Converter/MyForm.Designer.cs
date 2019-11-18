namespace Time_Converter
{
    partial class MyForm
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Liberare le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.milliseconds = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.hours = new System.Windows.Forms.TextBox();
            this.minutes = new System.Windows.Forms.TextBox();
            this.seconds = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.degrees = new System.Windows.Forms.TextBox();
            this.convert = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.radians = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // milliseconds
            // 
            this.milliseconds.Location = new System.Drawing.Point(85, 12);
            this.milliseconds.Name = "milliseconds";
            this.milliseconds.Size = new System.Drawing.Size(65, 20);
            this.milliseconds.TabIndex = 0;
            this.milliseconds.Text = "0";
            this.milliseconds.TextChanged += new System.EventHandler(this.milliseconds_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Milliseconds:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Time:";
            // 
            // hours
            // 
            this.hours.Location = new System.Drawing.Point(85, 38);
            this.hours.Name = "hours";
            this.hours.Size = new System.Drawing.Size(29, 20);
            this.hours.TabIndex = 2;
            this.hours.Text = "0";
            this.hours.TextChanged += new System.EventHandler(this.hours_TextChanged);
            // 
            // minutes
            // 
            this.minutes.Location = new System.Drawing.Point(120, 38);
            this.minutes.Name = "minutes";
            this.minutes.Size = new System.Drawing.Size(30, 20);
            this.minutes.TabIndex = 4;
            this.minutes.Text = "0";
            this.minutes.TextChanged += new System.EventHandler(this.minutes_TextChanged);
            // 
            // seconds
            // 
            this.seconds.Location = new System.Drawing.Point(156, 38);
            this.seconds.Name = "seconds";
            this.seconds.Size = new System.Drawing.Size(29, 20);
            this.seconds.TabIndex = 5;
            this.seconds.Text = "0";
            this.seconds.TextChanged += new System.EventHandler(this.seconds_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Degrees:";
            // 
            // degrees
            // 
            this.degrees.Location = new System.Drawing.Point(85, 64);
            this.degrees.Name = "degrees";
            this.degrees.Size = new System.Drawing.Size(100, 20);
            this.degrees.TabIndex = 6;
            this.degrees.Text = "0";
            this.degrees.TextChanged += new System.EventHandler(this.degrees_TextChanged);
            // 
            // convert
            // 
            this.convert.Location = new System.Drawing.Point(191, 12);
            this.convert.Name = "convert";
            this.convert.Size = new System.Drawing.Size(98, 98);
            this.convert.TabIndex = 8;
            this.convert.Text = "Convert!";
            this.convert.UseVisualStyleBackColor = true;
            this.convert.Click += new System.EventHandler(this.convert_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(156, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = ".000";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Radians:";
            // 
            // radians
            // 
            this.radians.Location = new System.Drawing.Point(85, 90);
            this.radians.Name = "radians";
            this.radians.Size = new System.Drawing.Size(100, 20);
            this.radians.TabIndex = 10;
            this.radians.Text = "0";
            this.radians.TextChanged += new System.EventHandler(this.radians_TextChanged);
            // 
            // MyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(301, 122);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.radians);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.convert);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.degrees);
            this.Controls.Add(this.seconds);
            this.Controls.Add(this.minutes);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.hours);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.milliseconds);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MyForm";
            this.Text = "ByloEngine Time Converter";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox milliseconds;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox hours;
        private System.Windows.Forms.TextBox minutes;
        private System.Windows.Forms.TextBox seconds;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox degrees;
        private System.Windows.Forms.Button convert;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox radians;
    }
}

