namespace SimulationSessionSummary_NS
{
    partial class IndividualPlaneControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridViewWeapons = new System.Windows.Forms.DataGridView();
            this.labelWeaponsRemainingText = new System.Windows.Forms.Label();
            this.labelWeaponsRemaining = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWeapons)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewWeapons
            // 
            this.dataGridViewWeapons.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewWeapons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridViewWeapons.Location = new System.Drawing.Point(0, 275);
            this.dataGridViewWeapons.Name = "dataGridViewWeapons";
            this.dataGridViewWeapons.ReadOnly = true;
            this.dataGridViewWeapons.Size = new System.Drawing.Size(943, 370);
            this.dataGridViewWeapons.TabIndex = 0;
            // 
            // labelWeaponsRemainingText
            // 
            this.labelWeaponsRemainingText.AutoSize = true;
            this.labelWeaponsRemainingText.Location = new System.Drawing.Point(25, 83);
            this.labelWeaponsRemainingText.Name = "labelWeaponsRemainingText";
            this.labelWeaponsRemainingText.Size = new System.Drawing.Size(109, 13);
            this.labelWeaponsRemainingText.TabIndex = 2;
            this.labelWeaponsRemainingText.Text = "Weapons Remaining:";
            // 
            // labelWeaponsRemaining
            // 
            this.labelWeaponsRemaining.AutoSize = true;
            this.labelWeaponsRemaining.Location = new System.Drawing.Point(46, 100);
            this.labelWeaponsRemaining.Name = "labelWeaponsRemaining";
            this.labelWeaponsRemaining.Size = new System.Drawing.Size(13, 13);
            this.labelWeaponsRemaining.TabIndex = 3;
            this.labelWeaponsRemaining.Text = "0";
            // 
            // IndividualPlaneControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelWeaponsRemaining);
            this.Controls.Add(this.labelWeaponsRemainingText);
            this.Controls.Add(this.dataGridViewWeapons);
            this.Name = "IndividualPlaneControl";
            this.Size = new System.Drawing.Size(943, 645);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWeapons)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewWeapons;
        private System.Windows.Forms.Label labelWeaponsRemainingText;
        private System.Windows.Forms.Label labelWeaponsRemaining;
    }
}
