
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
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonSaveXML = new System.Windows.Forms.Button();
            this.groupBoxMainStatistics = new System.Windows.Forms.GroupBox();
            this.labelRedTeamRemainingWeapons = new System.Windows.Forms.Label();
            this.labelBlueTeamRemainingWeapons = new System.Windows.Forms.Label();
            this.labelRemainingWeapons = new System.Windows.Forms.Label();
            this.labelRedTeamAliveEntities = new System.Windows.Forms.Label();
            this.labelBlueTeamAliveEntities = new System.Windows.Forms.Label();
            this.labelAliveEntities = new System.Windows.Forms.Label();
            this.labelRedTeam = new System.Windows.Forms.Label();
            this.labelBlueTeam = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBoxMainStatistics.SuspendLayout();
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
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(12, 115);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 16;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonSaveXML
            // 
            this.buttonSaveXML.Location = new System.Drawing.Point(93, 115);
            this.buttonSaveXML.Name = "buttonSaveXML";
            this.buttonSaveXML.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveXML.TabIndex = 17;
            this.buttonSaveXML.Text = "Save XML";
            this.buttonSaveXML.UseVisualStyleBackColor = true;
            this.buttonSaveXML.Click += new System.EventHandler(this.buttonSaveXML_Click);
            // 
            // groupBoxMainStatistics
            // 
            this.groupBoxMainStatistics.Controls.Add(this.labelRedTeamRemainingWeapons);
            this.groupBoxMainStatistics.Controls.Add(this.labelBlueTeamRemainingWeapons);
            this.groupBoxMainStatistics.Controls.Add(this.labelRemainingWeapons);
            this.groupBoxMainStatistics.Controls.Add(this.labelRedTeamAliveEntities);
            this.groupBoxMainStatistics.Controls.Add(this.labelBlueTeamAliveEntities);
            this.groupBoxMainStatistics.Controls.Add(this.labelAliveEntities);
            this.groupBoxMainStatistics.Controls.Add(this.labelRedTeam);
            this.groupBoxMainStatistics.Controls.Add(this.labelBlueTeam);
            this.groupBoxMainStatistics.Location = new System.Drawing.Point(288, 115);
            this.groupBoxMainStatistics.Name = "groupBoxMainStatistics";
            this.groupBoxMainStatistics.Size = new System.Drawing.Size(200, 176);
            this.groupBoxMainStatistics.TabIndex = 18;
            this.groupBoxMainStatistics.TabStop = false;
            this.groupBoxMainStatistics.Text = "Main Statistics";
            // 
            // labelRedTeamRemainingWeapons
            // 
            this.labelRedTeamRemainingWeapons.AutoSize = true;
            this.labelRedTeamRemainingWeapons.Location = new System.Drawing.Point(136, 111);
            this.labelRedTeamRemainingWeapons.Name = "labelRedTeamRemainingWeapons";
            this.labelRedTeamRemainingWeapons.Size = new System.Drawing.Size(13, 13);
            this.labelRedTeamRemainingWeapons.TabIndex = 7;
            this.labelRedTeamRemainingWeapons.Text = "0";
            // 
            // labelBlueTeamRemainingWeapons
            // 
            this.labelBlueTeamRemainingWeapons.AutoSize = true;
            this.labelBlueTeamRemainingWeapons.Location = new System.Drawing.Point(9, 111);
            this.labelBlueTeamRemainingWeapons.Name = "labelBlueTeamRemainingWeapons";
            this.labelBlueTeamRemainingWeapons.Size = new System.Drawing.Size(13, 13);
            this.labelBlueTeamRemainingWeapons.TabIndex = 6;
            this.labelBlueTeamRemainingWeapons.Text = "0";
            // 
            // labelRemainingWeapons
            // 
            this.labelRemainingWeapons.AutoSize = true;
            this.labelRemainingWeapons.Location = new System.Drawing.Point(43, 73);
            this.labelRemainingWeapons.Name = "labelRemainingWeapons";
            this.labelRemainingWeapons.Size = new System.Drawing.Size(106, 13);
            this.labelRemainingWeapons.TabIndex = 5;
            this.labelRemainingWeapons.Text = "Remaining Weapons";
            // 
            // labelRedTeamAliveEntities
            // 
            this.labelRedTeamAliveEntities.AutoSize = true;
            this.labelRedTeamAliveEntities.Location = new System.Drawing.Point(136, 47);
            this.labelRedTeamAliveEntities.Name = "labelRedTeamAliveEntities";
            this.labelRedTeamAliveEntities.Size = new System.Drawing.Size(13, 13);
            this.labelRedTeamAliveEntities.TabIndex = 4;
            this.labelRedTeamAliveEntities.Text = "0";
            // 
            // labelBlueTeamAliveEntities
            // 
            this.labelBlueTeamAliveEntities.AutoSize = true;
            this.labelBlueTeamAliveEntities.Location = new System.Drawing.Point(9, 47);
            this.labelBlueTeamAliveEntities.Name = "labelBlueTeamAliveEntities";
            this.labelBlueTeamAliveEntities.Size = new System.Drawing.Size(13, 13);
            this.labelBlueTeamAliveEntities.TabIndex = 3;
            this.labelBlueTeamAliveEntities.Text = "0";
            // 
            // labelAliveEntities
            // 
            this.labelAliveEntities.AutoSize = true;
            this.labelAliveEntities.Location = new System.Drawing.Point(63, 29);
            this.labelAliveEntities.Name = "labelAliveEntities";
            this.labelAliveEntities.Size = new System.Drawing.Size(67, 13);
            this.labelAliveEntities.TabIndex = 2;
            this.labelAliveEntities.Text = "Alive Entities";
            // 
            // labelRedTeam
            // 
            this.labelRedTeam.AutoSize = true;
            this.labelRedTeam.Location = new System.Drawing.Point(136, 16);
            this.labelRedTeam.Name = "labelRedTeam";
            this.labelRedTeam.Size = new System.Drawing.Size(57, 13);
            this.labelRedTeam.TabIndex = 1;
            this.labelRedTeam.Text = "Red Team";
            // 
            // labelBlueTeam
            // 
            this.labelBlueTeam.AutoSize = true;
            this.labelBlueTeam.Location = new System.Drawing.Point(6, 16);
            this.labelBlueTeam.Name = "labelBlueTeam";
            this.labelBlueTeam.Size = new System.Drawing.Size(58, 13);
            this.labelBlueTeam.TabIndex = 0;
            this.labelBlueTeam.Text = "Blue Team";
            // 
            // SimulationSessionSummaryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 303);
            this.Controls.Add(this.groupBoxMainStatistics);
            this.Controls.Add(this.buttonSaveXML);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SimulationSessionSummaryForm";
            this.Text = "SimulationSessionSummary";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBoxMainStatistics.ResumeLayout(false);
            this.groupBoxMainStatistics.PerformLayout();
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
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonSaveXML;
        private System.Windows.Forms.GroupBox groupBoxMainStatistics;
        private System.Windows.Forms.Label labelAliveEntities;
        private System.Windows.Forms.Label labelRedTeam;
        private System.Windows.Forms.Label labelBlueTeam;
        private System.Windows.Forms.Label labelRedTeamRemainingWeapons;
        private System.Windows.Forms.Label labelBlueTeamRemainingWeapons;
        private System.Windows.Forms.Label labelRemainingWeapons;
        private System.Windows.Forms.Label labelRedTeamAliveEntities;
        private System.Windows.Forms.Label labelBlueTeamAliveEntities;
    }
}