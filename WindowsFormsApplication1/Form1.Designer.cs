namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
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
            this.connectBtn = new System.Windows.Forms.Button();
            this.portBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ipBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.idBox = new System.Windows.Forms.TextBox();
            this.tempBox = new System.Windows.Forms.TextBox();
            this.humBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.presBox = new System.Windows.Forms.TextBox();
            this.personBox = new System.Windows.Forms.TextBox();
            this.personLabel = new System.Windows.Forms.Label();
            this.activityIndex = new System.Windows.Forms.TextBox();
            this.activityLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // connectBtn
            // 
            this.connectBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.connectBtn.Location = new System.Drawing.Point(498, 209);
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.Size = new System.Drawing.Size(110, 37);
            this.connectBtn.TabIndex = 0;
            this.connectBtn.Text = "START CONNECTION";
            this.connectBtn.UseVisualStyleBackColor = true;
            this.connectBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // portBox
            // 
            this.portBox.Location = new System.Drawing.Point(78, 49);
            this.portBox.Name = "portBox";
            this.portBox.Size = new System.Drawing.Size(100, 20);
            this.portBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "PORT";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "IP";
            // 
            // ipBox
            // 
            this.ipBox.Location = new System.Drawing.Point(78, 11);
            this.ipBox.Name = "ipBox";
            this.ipBox.ReadOnly = true;
            this.ipBox.Size = new System.Drawing.Size(100, 20);
            this.ipBox.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(340, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "ID";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(340, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Temperatura";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(340, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Umidità";
            // 
            // idBox
            // 
            this.idBox.Location = new System.Drawing.Point(447, 11);
            this.idBox.Name = "idBox";
            this.idBox.ReadOnly = true;
            this.idBox.Size = new System.Drawing.Size(117, 20);
            this.idBox.TabIndex = 8;
            // 
            // tempBox
            // 
            this.tempBox.Location = new System.Drawing.Point(447, 44);
            this.tempBox.Name = "tempBox";
            this.tempBox.ReadOnly = true;
            this.tempBox.Size = new System.Drawing.Size(34, 20);
            this.tempBox.TabIndex = 9;
            // 
            // humBox
            // 
            this.humBox.Location = new System.Drawing.Point(447, 83);
            this.humBox.Name = "humBox";
            this.humBox.ReadOnly = true;
            this.humBox.Size = new System.Drawing.Size(34, 20);
            this.humBox.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(504, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(18, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "°C";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(507, 90);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "%";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(340, 120);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Pressione";
            // 
            // presBox
            // 
            this.presBox.Location = new System.Drawing.Point(447, 117);
            this.presBox.Name = "presBox";
            this.presBox.ReadOnly = true;
            this.presBox.Size = new System.Drawing.Size(34, 20);
            this.presBox.TabIndex = 14;
            // 
            // personBox
            // 
            this.personBox.Location = new System.Drawing.Point(447, 149);
            this.personBox.Name = "personBox";
            this.personBox.ReadOnly = true;
            this.personBox.Size = new System.Drawing.Size(34, 20);
            this.personBox.TabIndex = 16;
            this.personBox.Visible = false;
            // 
            // personLabel
            // 
            this.personLabel.AutoSize = true;
            this.personLabel.Location = new System.Drawing.Point(340, 152);
            this.personLabel.Name = "personLabel";
            this.personLabel.Size = new System.Drawing.Size(60, 13);
            this.personLabel.TabIndex = 15;
            this.personLabel.Text = "ID Persona";
            this.personLabel.Visible = false;
            // 
            // activityIndex
            // 
            this.activityIndex.Location = new System.Drawing.Point(447, 182);
            this.activityIndex.Name = "activityIndex";
            this.activityIndex.ReadOnly = true;
            this.activityIndex.Size = new System.Drawing.Size(34, 20);
            this.activityIndex.TabIndex = 18;
            this.activityIndex.Visible = false;
            // 
            // activityLabel
            // 
            this.activityLabel.AutoSize = true;
            this.activityLabel.Location = new System.Drawing.Point(340, 185);
            this.activityLabel.Name = "activityLabel";
            this.activityLabel.Size = new System.Drawing.Size(66, 13);
            this.activityLabel.TabIndex = 17;
            this.activityLabel.Text = "Attività fisica";
            this.activityLabel.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 258);
            this.Controls.Add(this.activityIndex);
            this.Controls.Add(this.activityLabel);
            this.Controls.Add(this.personBox);
            this.Controls.Add(this.personLabel);
            this.Controls.Add(this.presBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.humBox);
            this.Controls.Add(this.tempBox);
            this.Controls.Add(this.idBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ipBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.portBox);
            this.Controls.Add(this.connectBtn);
            this.HelpButton = true;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Server";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button connectBtn;
        private System.Windows.Forms.TextBox portBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ipBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox idBox;
        private System.Windows.Forms.TextBox tempBox;
        private System.Windows.Forms.TextBox humBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox presBox;
        private System.Windows.Forms.TextBox personBox;
        private System.Windows.Forms.Label personLabel;
        private System.Windows.Forms.TextBox activityIndex;
        private System.Windows.Forms.Label activityLabel;
    }
}

