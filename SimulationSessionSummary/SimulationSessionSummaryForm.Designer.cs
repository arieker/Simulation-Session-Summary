
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tabPageGraphs = new System.Windows.Forms.TabPage();
            this.tabControlTeamsGraphs = new System.Windows.Forms.TabControl();
            this.tabPageTeams = new System.Windows.Forms.TabPage();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPageIndividuals = new System.Windows.Forms.TabPage();
            this.chart4 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPageMain = new System.Windows.Forms.TabPage();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.labelLoadXML = new System.Windows.Forms.Label();
            this.buttonClearData = new System.Windows.Forms.Button();
            this.buttonLoadXML = new System.Windows.Forms.Button();
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
            this.dataGridViewMainPage = new System.Windows.Forms.DataGridView();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageGraphs.SuspendLayout();
            this.tabControlTeamsGraphs.SuspendLayout();
            this.tabPageTeams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.tabPageIndividuals.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart4)).BeginInit();
            this.tabPageMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.groupBoxMainStatistics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMainPage)).BeginInit();
            this.tabControlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPageGraphs
            // 
            this.tabPageGraphs.Controls.Add(this.tabControlTeamsGraphs);
            this.tabPageGraphs.Location = new System.Drawing.Point(4, 29);
            this.tabPageGraphs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageGraphs.Name = "tabPageGraphs";
            this.tabPageGraphs.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageGraphs.Size = new System.Drawing.Size(1440, 1033);
            this.tabPageGraphs.TabIndex = 1;
            this.tabPageGraphs.Text = "Graphs";
            this.tabPageGraphs.UseVisualStyleBackColor = true;
            // 
            // tabControlTeamsGraphs
            // 
            this.tabControlTeamsGraphs.Controls.Add(this.tabPageTeams);
            this.tabControlTeamsGraphs.Controls.Add(this.tabPageIndividuals);
            this.tabControlTeamsGraphs.Location = new System.Drawing.Point(0, 0);
            this.tabControlTeamsGraphs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabControlTeamsGraphs.Name = "tabControlTeamsGraphs";
            this.tabControlTeamsGraphs.SelectedIndex = 0;
            this.tabControlTeamsGraphs.Size = new System.Drawing.Size(1442, 1032);
            this.tabControlTeamsGraphs.TabIndex = 1;
            this.tabControlTeamsGraphs.SelectedIndexChanged += new System.EventHandler(this.tabControlTeamsGraphs_SelectedIndexChanged);
            // 
            // tabPageTeams
            // 
            this.tabPageTeams.Controls.Add(this.chart2);
            this.tabPageTeams.Controls.Add(this.chart1);
            this.tabPageTeams.Location = new System.Drawing.Point(4, 29);
            this.tabPageTeams.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageTeams.Name = "tabPageTeams";
            this.tabPageTeams.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageTeams.Size = new System.Drawing.Size(1434, 999);
            this.tabPageTeams.TabIndex = 0;
            this.tabPageTeams.Text = "Teams";
            this.tabPageTeams.UseVisualStyleBackColor = true;
            // 
            // chart2
            // 
            chartArea1.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart2.Legends.Add(legend1);
            this.chart2.Location = new System.Drawing.Point(509, 9);
            this.chart2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chart2.Name = "chart2";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart2.Series.Add(series1);
            this.chart2.Size = new System.Drawing.Size(450, 462);
            this.chart2.TabIndex = 2;
            this.chart2.Text = "chart2";
            // 
            // chart1
            // 
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(9, 9);
            this.chart1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chart1.Name = "chart1";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(450, 462);
            this.chart1.TabIndex = 1;
            this.chart1.Text = "chart2";
            // 
            // tabPageIndividuals
            // 
            this.tabPageIndividuals.Controls.Add(this.chart4);
            this.tabPageIndividuals.Location = new System.Drawing.Point(4, 29);
            this.tabPageIndividuals.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageIndividuals.Name = "tabPageIndividuals";
            this.tabPageIndividuals.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageIndividuals.Size = new System.Drawing.Size(1434, 999);
            this.tabPageIndividuals.TabIndex = 1;
            this.tabPageIndividuals.Text = "Individuals";
            this.tabPageIndividuals.UseVisualStyleBackColor = true;
            this.tabPageIndividuals.Click += new System.EventHandler(this.tabPageIndividuals_Click);
            // 
            // chart4
            // 
            chartArea3.Name = "ChartArea1";
            this.chart4.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chart4.Legends.Add(legend3);
            this.chart4.Location = new System.Drawing.Point(9, 9);
            this.chart4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chart4.Name = "chart4";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chart4.Series.Add(series3);
            this.chart4.Size = new System.Drawing.Size(450, 462);
            this.chart4.TabIndex = 2;
            this.chart4.Text = "chart2";
            // 
            // tabPageMain
            // 
            this.tabPageMain.Controls.Add(this.splitContainerMain);
            this.tabPageMain.Location = new System.Drawing.Point(4, 29);
            this.tabPageMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageMain.Name = "tabPageMain";
            this.tabPageMain.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageMain.Size = new System.Drawing.Size(1440, 1033);
            this.tabPageMain.TabIndex = 0;
            this.tabPageMain.Text = "Main";
            this.tabPageMain.UseVisualStyleBackColor = true;
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerMain.Location = new System.Drawing.Point(4, 5);
            this.splitContainerMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.labelLoadXML);
            this.splitContainerMain.Panel1.Controls.Add(this.buttonClearData);
            this.splitContainerMain.Panel1.Controls.Add(this.buttonLoadXML);
            this.splitContainerMain.Panel1.Controls.Add(this.buttonStart);
            this.splitContainerMain.Panel1.Controls.Add(this.buttonSaveXML);
            this.splitContainerMain.Panel1.Controls.Add(this.groupBoxMainStatistics);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.dataGridViewMainPage);
            this.splitContainerMain.Size = new System.Drawing.Size(1432, 1023);
            this.splitContainerMain.SplitterDistance = 250;
            this.splitContainerMain.SplitterWidth = 6;
            this.splitContainerMain.TabIndex = 21;
            // 
            // labelLoadXML
            // 
            this.labelLoadXML.AutoSize = true;
            this.labelLoadXML.Location = new System.Drawing.Point(16, 94);
            this.labelLoadXML.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelLoadXML.Name = "labelLoadXML";
            this.labelLoadXML.Size = new System.Drawing.Size(341, 20);
            this.labelLoadXML.TabIndex = 21;
            this.labelLoadXML.Text = "Loading XML Data is for viewing purposes only!";
            // 
            // buttonClearData
            // 
            this.buttonClearData.Enabled = false;
            this.buttonClearData.Location = new System.Drawing.Point(8, 54);
            this.buttonClearData.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonClearData.Name = "buttonClearData";
            this.buttonClearData.Size = new System.Drawing.Size(112, 35);
            this.buttonClearData.TabIndex = 20;
            this.buttonClearData.Text = "Clear Data";
            this.buttonClearData.UseVisualStyleBackColor = true;
            this.buttonClearData.Click += new System.EventHandler(this.buttonClearData_Click);
            // 
            // buttonLoadXML
            // 
            this.buttonLoadXML.Location = new System.Drawing.Point(250, 54);
            this.buttonLoadXML.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonLoadXML.Name = "buttonLoadXML";
            this.buttonLoadXML.Size = new System.Drawing.Size(112, 35);
            this.buttonLoadXML.TabIndex = 19;
            this.buttonLoadXML.Text = "Load XML";
            this.buttonLoadXML.UseVisualStyleBackColor = true;
            this.buttonLoadXML.Click += new System.EventHandler(this.buttonLoadXML_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(8, 9);
            this.buttonStart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(112, 35);
            this.buttonStart.TabIndex = 16;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonSaveXML
            // 
            this.buttonSaveXML.Location = new System.Drawing.Point(250, 9);
            this.buttonSaveXML.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonSaveXML.Name = "buttonSaveXML";
            this.buttonSaveXML.Size = new System.Drawing.Size(112, 35);
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
            this.groupBoxMainStatistics.Location = new System.Drawing.Point(8, 118);
            this.groupBoxMainStatistics.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxMainStatistics.Name = "groupBoxMainStatistics";
            this.groupBoxMainStatistics.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxMainStatistics.Size = new System.Drawing.Size(342, 271);
            this.groupBoxMainStatistics.TabIndex = 18;
            this.groupBoxMainStatistics.TabStop = false;
            this.groupBoxMainStatistics.Text = "Main Statistics";
            // 
            // labelRedTeamRemainingWeapons
            // 
            this.labelRedTeamRemainingWeapons.AutoSize = true;
            this.labelRedTeamRemainingWeapons.Location = new System.Drawing.Point(204, 171);
            this.labelRedTeamRemainingWeapons.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelRedTeamRemainingWeapons.Name = "labelRedTeamRemainingWeapons";
            this.labelRedTeamRemainingWeapons.Size = new System.Drawing.Size(18, 20);
            this.labelRedTeamRemainingWeapons.TabIndex = 7;
            this.labelRedTeamRemainingWeapons.Text = "0";
            // 
            // labelBlueTeamRemainingWeapons
            // 
            this.labelBlueTeamRemainingWeapons.AutoSize = true;
            this.labelBlueTeamRemainingWeapons.Location = new System.Drawing.Point(14, 171);
            this.labelBlueTeamRemainingWeapons.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelBlueTeamRemainingWeapons.Name = "labelBlueTeamRemainingWeapons";
            this.labelBlueTeamRemainingWeapons.Size = new System.Drawing.Size(18, 20);
            this.labelBlueTeamRemainingWeapons.TabIndex = 6;
            this.labelBlueTeamRemainingWeapons.Text = "0";
            // 
            // labelRemainingWeapons
            // 
            this.labelRemainingWeapons.AutoSize = true;
            this.labelRemainingWeapons.Location = new System.Drawing.Point(64, 112);
            this.labelRemainingWeapons.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelRemainingWeapons.Name = "labelRemainingWeapons";
            this.labelRemainingWeapons.Size = new System.Drawing.Size(157, 20);
            this.labelRemainingWeapons.TabIndex = 5;
            this.labelRemainingWeapons.Text = "Remaining Weapons";
            // 
            // labelRedTeamAliveEntities
            // 
            this.labelRedTeamAliveEntities.AutoSize = true;
            this.labelRedTeamAliveEntities.Location = new System.Drawing.Point(204, 72);
            this.labelRedTeamAliveEntities.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelRedTeamAliveEntities.Name = "labelRedTeamAliveEntities";
            this.labelRedTeamAliveEntities.Size = new System.Drawing.Size(18, 20);
            this.labelRedTeamAliveEntities.TabIndex = 4;
            this.labelRedTeamAliveEntities.Text = "0";
            // 
            // labelBlueTeamAliveEntities
            // 
            this.labelBlueTeamAliveEntities.AutoSize = true;
            this.labelBlueTeamAliveEntities.Location = new System.Drawing.Point(14, 72);
            this.labelBlueTeamAliveEntities.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelBlueTeamAliveEntities.Name = "labelBlueTeamAliveEntities";
            this.labelBlueTeamAliveEntities.Size = new System.Drawing.Size(18, 20);
            this.labelBlueTeamAliveEntities.TabIndex = 3;
            this.labelBlueTeamAliveEntities.Text = "0";
            // 
            // labelAliveEntities
            // 
            this.labelAliveEntities.AutoSize = true;
            this.labelAliveEntities.Location = new System.Drawing.Point(94, 45);
            this.labelAliveEntities.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAliveEntities.Name = "labelAliveEntities";
            this.labelAliveEntities.Size = new System.Drawing.Size(99, 20);
            this.labelAliveEntities.TabIndex = 2;
            this.labelAliveEntities.Text = "Alive Entities";
            // 
            // labelRedTeam
            // 
            this.labelRedTeam.AutoSize = true;
            this.labelRedTeam.Location = new System.Drawing.Point(204, 25);
            this.labelRedTeam.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelRedTeam.Name = "labelRedTeam";
            this.labelRedTeam.Size = new System.Drawing.Size(83, 20);
            this.labelRedTeam.TabIndex = 1;
            this.labelRedTeam.Text = "Red Team";
            // 
            // labelBlueTeam
            // 
            this.labelBlueTeam.AutoSize = true;
            this.labelBlueTeam.Location = new System.Drawing.Point(9, 25);
            this.labelBlueTeam.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelBlueTeam.Name = "labelBlueTeam";
            this.labelBlueTeam.Size = new System.Drawing.Size(85, 20);
            this.labelBlueTeam.TabIndex = 0;
            this.labelBlueTeam.Text = "Blue Team";
            // 
            // dataGridViewMainPage
            // 
            this.dataGridViewMainPage.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewMainPage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMainPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewMainPage.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewMainPage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGridViewMainPage.Name = "dataGridViewMainPage";
            this.dataGridViewMainPage.ReadOnly = true;
            this.dataGridViewMainPage.RowHeadersWidth = 62;
            this.dataGridViewMainPage.Size = new System.Drawing.Size(1176, 1023);
            this.dataGridViewMainPage.TabIndex = 20;
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageMain);
            this.tabControlMain.Controls.Add(this.tabPageGraphs);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(1448, 1066);
            this.tabControlMain.TabIndex = 19;
            // 
            // SimulationSessionSummaryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1448, 1066);
            this.Controls.Add(this.tabControlMain);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "SimulationSessionSummaryForm";
            this.Text = "Simulation Session Summary";
            this.tabPageGraphs.ResumeLayout(false);
            this.tabControlTeamsGraphs.ResumeLayout(false);
            this.tabPageTeams.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.tabPageIndividuals.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart4)).EndInit();
            this.tabPageMain.ResumeLayout(false);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel1.PerformLayout();
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.groupBoxMainStatistics.ResumeLayout(false);
            this.groupBoxMainStatistics.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMainPage)).EndInit();
            this.tabControlMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPageGraphs;
        private System.Windows.Forms.TabPage tabPageMain;
        private System.Windows.Forms.DataGridView dataGridViewMainPage;
        private System.Windows.Forms.Button buttonSaveXML;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.GroupBox groupBoxMainStatistics;
        private System.Windows.Forms.Label labelRedTeamRemainingWeapons;
        private System.Windows.Forms.Label labelBlueTeamRemainingWeapons;
        private System.Windows.Forms.Label labelRemainingWeapons;
        private System.Windows.Forms.Label labelRedTeamAliveEntities;
        private System.Windows.Forms.Label labelBlueTeamAliveEntities;
        private System.Windows.Forms.Label labelAliveEntities;
        private System.Windows.Forms.Label labelRedTeam;
        private System.Windows.Forms.Label labelBlueTeam;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabControl tabControlTeamsGraphs;
        private System.Windows.Forms.TabPage tabPageTeams;
        private System.Windows.Forms.TabPage tabPageIndividuals;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart4;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.Button buttonLoadXML;
        private System.Windows.Forms.Button buttonClearData;
        private System.Windows.Forms.Label labelLoadXML;
    }
}