
namespace SimulationSessionSummary_NS
{
    partial class SimulationSessionSummaryForm
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
            this.EntityCount_Label = new System.Windows.Forms.Label();
            this.State_Label = new System.Windows.Forms.Label();
            this.EntityCountText_Label = new System.Windows.Forms.Label();
            this.StateText_Label = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.entitiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createNewEntityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.start_button = new System.Windows.Forms.Button();
            this.buttonSaveXML = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // EntityCount_Label
            // 
            this.EntityCount_Label.AutoSize = true;
            this.EntityCount_Label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.EntityCount_Label.Location = new System.Drawing.Point(124, 52);
            this.EntityCount_Label.Name = "EntityCount_Label";
            this.EntityCount_Label.Size = new System.Drawing.Size(13, 13);
            this.EntityCount_Label.TabIndex = 11;
            this.EntityCount_Label.Text = "0";
            // 
            // State_Label
            // 
            this.State_Label.AutoSize = true;
            this.State_Label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.State_Label.Location = new System.Drawing.Point(118, 25);
            this.State_Label.Name = "State_Label";
            this.State_Label.Size = new System.Drawing.Size(56, 13);
            this.State_Label.TabIndex = 10;
            this.State_Label.Text = "Undefined";
            // 
            // EntityCountText_Label
            // 
            this.EntityCountText_Label.AutoSize = true;
            this.EntityCountText_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EntityCountText_Label.Location = new System.Drawing.Point(51, 52);
            this.EntityCountText_Label.Name = "EntityCountText_Label";
            this.EntityCountText_Label.Size = new System.Drawing.Size(67, 13);
            this.EntityCountText_Label.TabIndex = 9;
            this.EntityCountText_Label.Text = "Entity Count:";
            // 
            // StateText_Label
            // 
            this.StateText_Label.AutoSize = true;
            this.StateText_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StateText_Label.Location = new System.Drawing.Point(6, 25);
            this.StateText_Label.Name = "StateText_Label";
            this.StateText_Label.Size = new System.Drawing.Size(106, 13);
            this.StateText_Label.TabIndex = 8;
            this.StateText_Label.Text = "MACE Mission State:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.StateText_Label);
            this.groupBox1.Controls.Add(this.EntityCount_Label);
            this.groupBox1.Controls.Add(this.EntityCountText_Label);
            this.groupBox1.Controls.Add(this.State_Label);
            this.groupBox1.Location = new System.Drawing.Point(12, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(476, 76);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Scenario Statistics";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.entitiesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(506, 24);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // entitiesToolStripMenuItem
            // 
            this.entitiesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createNewEntityToolStripMenuItem});
            this.entitiesToolStripMenuItem.Name = "entitiesToolStripMenuItem";
            this.entitiesToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.entitiesToolStripMenuItem.Text = "Entities";
            // 
            // createNewEntityToolStripMenuItem
            // 
            this.createNewEntityToolStripMenuItem.Name = "createNewEntityToolStripMenuItem";
            this.createNewEntityToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.createNewEntityToolStripMenuItem.Text = "Create New Entity";
            this.createNewEntityToolStripMenuItem.Click += new System.EventHandler(this.btnCreateEntity_Click);
            // 
            // start_button
            // 
            this.start_button.Location = new System.Drawing.Point(202, 4);
            this.start_button.Name = "start_button";
            this.start_button.Size = new System.Drawing.Size(75, 23);
            this.start_button.TabIndex = 16;
            this.start_button.Text = "Start";
            this.start_button.UseVisualStyleBackColor = true;
            this.start_button.Click += new System.EventHandler(this.start_button_Click);
            // 
            // buttonSaveXML
            // 
            this.buttonSaveXML.Location = new System.Drawing.Point(13, 116);
            this.buttonSaveXML.Name = "buttonSaveXML";
            this.buttonSaveXML.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveXML.TabIndex = 17;
            this.buttonSaveXML.Text = "Save XML";
            this.buttonSaveXML.UseVisualStyleBackColor = true;
            this.buttonSaveXML.Click += new System.EventHandler(this.buttonSaveXML_Click);
            // 
            // SimulationSessionSummaryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 303);
            this.Controls.Add(this.buttonSaveXML);
            this.Controls.Add(this.start_button);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SimulationSessionSummaryForm";
            this.Text = "SimulationSessionSummary";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label EntityCount_Label;
        internal System.Windows.Forms.Label State_Label;
        internal System.Windows.Forms.Label EntityCountText_Label;
        internal System.Windows.Forms.Label StateText_Label;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem entitiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createNewEntityToolStripMenuItem;
        private System.Windows.Forms.Button start_button;
        private System.Windows.Forms.Button buttonSaveXML;
    }
}