
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBoxMainStatistics = new System.Windows.Forms.GroupBox();
            this.labelBlueTeam = new System.Windows.Forms.Label();
            this.labelRedTeam = new System.Windows.Forms.Label();
            this.labelAliveEntities = new System.Windows.Forms.Label();
            this.labelBlueTeamAliveEntities = new System.Windows.Forms.Label();
            this.labelRedTeamAliveEntities = new System.Windows.Forms.Label();
            this.labelRemainingWeapons = new System.Windows.Forms.Label();
            this.labelBlueTeamRemainingWeapons = new System.Windows.Forms.Label();
            this.labelRedTeamRemainingWeapons = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.State_Label = new System.Windows.Forms.Label();
            this.EntityCountText_Label = new System.Windows.Forms.Label();
            this.EntityCount_Label = new System.Windows.Forms.Label();
            this.StateText_Label = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonSaveXML = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.groupBoxMainStatistics.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.chart1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(994, 696);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Other";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(8, 6);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(300, 300);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView3);
            this.tabPage1.Controls.Add(this.dataGridView2);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Controls.Add(this.buttonSaveXML);
            this.tabPage1.Controls.Add(this.buttonStart);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.groupBoxMainStatistics);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(994, 696);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBoxMainStatistics
            // 
            this.groupBoxMainStatistics.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxMainStatistics.Controls.Add(this.labelRedTeamRemainingWeapons);
            this.groupBoxMainStatistics.Controls.Add(this.labelBlueTeamRemainingWeapons);
            this.groupBoxMainStatistics.Controls.Add(this.labelRemainingWeapons);
            this.groupBoxMainStatistics.Controls.Add(this.labelRedTeamAliveEntities);
            this.groupBoxMainStatistics.Controls.Add(this.labelBlueTeamAliveEntities);
            this.groupBoxMainStatistics.Controls.Add(this.labelAliveEntities);
            this.groupBoxMainStatistics.Controls.Add(this.labelRedTeam);
            this.groupBoxMainStatistics.Controls.Add(this.labelBlueTeam);
            this.groupBoxMainStatistics.Location = new System.Drawing.Point(758, 115);
            this.groupBoxMainStatistics.Name = "groupBoxMainStatistics";
            this.groupBoxMainStatistics.Size = new System.Drawing.Size(228, 176);
            this.groupBoxMainStatistics.TabIndex = 18;
            this.groupBoxMainStatistics.TabStop = false;
            this.groupBoxMainStatistics.Text = "Main Statistics";
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
            // labelRedTeam
            // 
            this.labelRedTeam.AutoSize = true;
            this.labelRedTeam.Location = new System.Drawing.Point(136, 16);
            this.labelRedTeam.Name = "labelRedTeam";
            this.labelRedTeam.Size = new System.Drawing.Size(57, 13);
            this.labelRedTeam.TabIndex = 1;
            this.labelRedTeam.Text = "Red Team";
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
            // labelBlueTeamAliveEntities
            // 
            this.labelBlueTeamAliveEntities.AutoSize = true;
            this.labelBlueTeamAliveEntities.Location = new System.Drawing.Point(9, 47);
            this.labelBlueTeamAliveEntities.Name = "labelBlueTeamAliveEntities";
            this.labelBlueTeamAliveEntities.Size = new System.Drawing.Size(13, 13);
            this.labelBlueTeamAliveEntities.TabIndex = 3;
            this.labelBlueTeamAliveEntities.Text = "0";
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
            // labelRemainingWeapons
            // 
            this.labelRemainingWeapons.AutoSize = true;
            this.labelRemainingWeapons.Location = new System.Drawing.Point(43, 73);
            this.labelRemainingWeapons.Name = "labelRemainingWeapons";
            this.labelRemainingWeapons.Size = new System.Drawing.Size(106, 13);
            this.labelRemainingWeapons.TabIndex = 5;
            this.labelRemainingWeapons.Text = "Remaining Weapons";
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
            // labelRedTeamRemainingWeapons
            // 
            this.labelRedTeamRemainingWeapons.AutoSize = true;
            this.labelRedTeamRemainingWeapons.Location = new System.Drawing.Point(136, 111);
            this.labelRedTeamRemainingWeapons.Name = "labelRedTeamRemainingWeapons";
            this.labelRedTeamRemainingWeapons.Size = new System.Drawing.Size(13, 13);
            this.labelRedTeamRemainingWeapons.TabIndex = 7;
            this.labelRedTeamRemainingWeapons.Text = "0";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.StateText_Label);
            this.groupBox1.Controls.Add(this.EntityCount_Label);
            this.groupBox1.Controls.Add(this.EntityCountText_Label);
            this.groupBox1.Controls.Add(this.State_Label);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(980, 76);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Scenario Statistics";
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
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(6, 88);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 16;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonSaveXML
            // 
            this.buttonSaveXML.Location = new System.Drawing.Point(87, 88);
            this.buttonSaveXML.Name = "buttonSaveXML";
            this.buttonSaveXML.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveXML.TabIndex = 17;
            this.buttonSaveXML.Text = "Save XML";
            this.buttonSaveXML.UseVisualStyleBackColor = true;
            this.buttonSaveXML.Click += new System.EventHandler(this.buttonSaveXML_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 115);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(746, 228);
            this.dataGridView1.TabIndex = 19;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(6, 402);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.Size = new System.Drawing.Size(240, 150);
            this.dataGridView2.TabIndex = 20;
            // 
            // dataGridView3
            // 
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(252, 402);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.ReadOnly = true;
            this.dataGridView3.Size = new System.Drawing.Size(240, 150);
            this.dataGridView3.TabIndex = 21;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1002, 722);
            this.tabControl.TabIndex = 19;
            // 
            // SimulationSessionSummaryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 722);
            this.Controls.Add(this.tabControl);
            this.Name = "SimulationSessionSummaryForm";
            this.Text = "SimulationSessionSummary";
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.groupBoxMainStatistics.ResumeLayout(false);
            this.groupBoxMainStatistics.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button buttonSaveXML;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.GroupBox groupBox1;
        internal System.Windows.Forms.Label StateText_Label;
        internal System.Windows.Forms.Label EntityCount_Label;
        internal System.Windows.Forms.Label EntityCountText_Label;
        internal System.Windows.Forms.Label State_Label;
        private System.Windows.Forms.GroupBox groupBoxMainStatistics;
        private System.Windows.Forms.Label labelRedTeamRemainingWeapons;
        private System.Windows.Forms.Label labelBlueTeamRemainingWeapons;
        private System.Windows.Forms.Label labelRemainingWeapons;
        private System.Windows.Forms.Label labelRedTeamAliveEntities;
        private System.Windows.Forms.Label labelBlueTeamAliveEntities;
        private System.Windows.Forms.Label labelAliveEntities;
        private System.Windows.Forms.Label labelRedTeam;
        private System.Windows.Forms.Label labelBlueTeam;
        private System.Windows.Forms.TabControl tabControl;
    }
}