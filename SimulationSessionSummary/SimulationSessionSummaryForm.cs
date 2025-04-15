using BSI.MACE;
using BSI.MACE.AI;
using BSI.MACE.Equipment;
using BSI.MACE.PlugInNS;
using BSI.SignalGenerator;
using BSI.SimulationLibrary;
using BSI.SimulationLibrary.Formulas;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Collections.ObjectModel;
using System.Windows.Forms.DataVisualization.Charting;
using BSI.MACE.AI.Commands.EOB;
using FastColoredTextBoxNS;

namespace SimulationSessionSummary_NS
{
    public partial class SimulationSessionSummaryForm : Form
    {
        #region "Private Member Variables"

        /// <summary>
        /// Persist the interface to the current MACE scenario
        /// </summary>
        /// <remarks></remarks>

        private IMission _mission;
        private SimulationSessionSummary _plugin;
        private SortableBindingList<PhysicalEntityWrapper> _userSelectedEntities = new SortableBindingList<PhysicalEntityWrapper>();
        private BindingList<PlatformObject> platformObjects = new BindingList<PlatformObject>();

        //variables for charts
        private List<(DateTime Timestamp, int BlueKills, int RedKills)> killHistory = new List<(DateTime, int, int)>();
        public List<(DateTime Timestamp, int PlaneKillCount)> PerPlaneKillRecords { get; set; } = new List<(DateTime, int)>();

        private DateTime simulationStartTime;
        private Timer killHistoryTimer;

        private ComboBox comboBoxPlaneSelection;
        private ComboBox comboBoxPlaneTypeSelection;


        // These two lists are READ ONLY!!! And update automatically
        // The point of them is to be used as a very convenient DataSource for dataGridViews while still keeping one main list for convenience
        private List<PlatformObject> team1Platforms => platformObjects.Where(p => p.Team == 1).ToList();
        private List<PlatformObject> team2Platforms => platformObjects.Where(p => p.Team == 2).ToList();

        /// <summary>
        /// Local reference to the plugin class -- used for window configuration save/restore
        /// </summary>
        #endregion

        #region "DIS Type Key"
        /*
         * DIS entity type representation
         * Order of fields
         * Kind, Domain, Country, Category, SubCategory, Specific, Extra
         * Known Values
         * Kind: 1 = Platforms (planes, vehicles, ships)
         *       2 = Munitions (bullets, bombs, rockets, missles)
         *       3 = Lifeforms (Humans and animals)
         *       5 = Cultural (buildings, trees, other entities in cultural tab of mission builder)
         *       
         * Domain: 1 = Land
         *         2 = Air
         *         3 = Water (surface)
         *         4 = Water (undersurface)
         *         5 = Space
         * 
         * Country: Isn't important but USA is 225
         * Category: Unsure if imporant but can look into if needed
         * SubCategory: Most likely not needed
         * Specific: Hopefully not needed
         * Extra: Definitly not needed
         * 
         * There are Platform filters but there are 40 with at least half being important
         */
        #endregion

        #region "Instance Management"

        /// <summary>
        /// Disable default c'tor
        /// </summary>
        private SimulationSessionSummaryForm()
        {
            FormClosing += SimulationSessionSummaryForm_FormClosing;
            Load += SimulationSessionSummaryForm_Load;

            // if you need/want to do things when the form is shown/hidden, 
            // uncomment the following event register/subscriber: 
            // VisibleChanged += SimulationSessionSummaryForm_VisibleChanged;

            // This call is required by the designer.
            InitializeComponent();

            // Add any initialization after the InitializeComponent() call.
        }

        /// <summary>
        /// Create new instance of plug-in form passing in the instance of the MACE IMission
        /// </summary>
        /// <param name="mission">The MACE IMission instance</param>
        public SimulationSessionSummaryForm(IMACEPlugIn plugin, IMission mission)
            : this()
        {
            _plugin = (SimulationSessionSummary)plugin;
            _mission = mission;
            //dgvUserSelectedEntities.DataSource = _userSelectedEntities;

            // add this form instance to the window config controller as a managed form
            _plugin.WindowConfigController.AddManagedForm(this);
        }

        public class PlatformTypeGroup
        {
            public string Type { get; set; }
            public List<PlatformObject> Platforms { get; set; }

            public override string ToString()
            {
                return Type;
            }
        }

        #endregion

        #region "Our Helper Functions"
        // These are extremely simple, and really aren't necessary and honestly redundant, 
        // but it makes the code SO clean that I don't care how inefficient or dumb it is.

        private PlatformObject FindPlatformFromName(string name) =>
            platformObjects.FirstOrDefault(p => p.Name == name);

        private PlatformObject FindPlatformFromWeaponID(ulong weaponID) =>
            platformObjects.FirstOrDefault(p => p.WeaponObjects.Any(w => w.InstanceID == weaponID));

        private WeaponObject FindWeaponFromWeaponID(ulong weaponID) =>
            platformObjects.SelectMany(p => p.WeaponObjects).FirstOrDefault(w => w.InstanceID == weaponID);

        private Boolean isWeaponABullet(ulong weaponID) =>
            platformObjects.Any(p => p.GunObjects.Any(gun => gun.ActiveBulletEntityIDs.Contains(weaponID)));

        private GunObject findGunFromBulletID(ulong bulletID) =>
            platformObjects.SelectMany(p => p.GunObjects).First(gun => gun.ActiveBulletEntityIDs.Contains(bulletID));

        private GunObject findGunFromName(string name) =>
            platformObjects.SelectMany(p => p.GunObjects).FirstOrDefault(g => g._name == name);

        // Team related helper functions
        private int GetTeamKills(int team) =>
            platformObjects.Where(p => p.Team == team).Sum(p => p.Kills);

        // Weapons
        private List<WeaponObject> GetTeamAllWeaponsList(int team) =>
            platformObjects.Where(p => p.Team == team)
                           .SelectMany(p => p.WeaponObjects)
                           .ToList();

        private List<WeaponObject> GetTeamRemainingWeaponsList(int team) =>
            GetTeamAllWeaponsList(team).Where(w => !w.Fired).ToList();

        private List<WeaponObject> GetTeamExpendedWeapons(int team) =>
            GetTeamAllWeaponsList(team).Where(w => w.Fired).ToList();

        // Platforms
        private List<PlatformObject> GetTeamAllPlatformsList(int team) =>
            platformObjects.Where(p => p.Team == team).ToList();

        private List<PlatformObject> GetTeamAlivePlatformsList(int team) =>
            GetTeamAllPlatformsList(team).Where(p => p.Alive).ToList();

        private List<PlatformObject> GetTeamDeadPlatformsList(int team) =>
            GetTeamAllPlatformsList(team).Where(p => !p.Alive).ToList();

        private List<PlatformObject> GetTeamAliveGroundPlatformsList(int team) =>
            GetTeamAllPlatformsList(team).Where(p => p.Alive && p.Domain == "1").ToList();

        private List<PlatformObject> GetTeamGroundPlatformsList(int team) =>
            GetTeamAllPlatformsList(team).Where(p => p.Domain == "1").ToList();

        private List<PlatformObject> GetTeamAliveAirPlatformsList(int team) =>
            GetTeamAllPlatformsList(team).Where(p => p.Alive && p.Domain == "2").ToList();

        private List<PlatformObject> GetTeamAirPlatformsList(int team) =>
            GetTeamAllPlatformsList(team).Where(p => p.Domain == "2").ToList();

        private List<PlatformObject> GetTeamAliveSeaPlatformsList(int team) =>
            GetTeamAllPlatformsList(team).Where(p => p.Alive && p.Domain == "3" && p.Domain == "4").ToList();

        private List<PlatformObject> GetTeamSeaPlatformsList(int team) =>
            GetTeamAllPlatformsList(team).Where(p => p.Domain == "3" && p.Domain == "4").ToList();

        private List<PlatformObject> GetTeamAliveSpacePlatformsList(int team) =>
            GetTeamAllPlatformsList(team).Where(p => p.Alive && p.Domain == "5").ToList();

        private List<PlatformObject> GetTeamSpacePlatformsList(int team) =>
            GetTeamAllPlatformsList(team).Where(p => p.Domain == "5").ToList();

        #endregion


        #region "Our UI Functions"
        // These are related to refreshing or handling UI things and are necessary
        // In the future when we start adding dataGridViews and such these could likely just be updated to be .Length/.Count calls of the dataGridViews for these respective things
        private void updateMainStatistics()
        {
            // REFERENCE: Blue Team 1 | Red Team 2

            dataGridViewMainPage.Refresh();

            labelBlueTeamAliveEntities.Text = GetTeamAlivePlatformsList(1).Count.ToString();
            labelBlueTeamRemainingWeapons.Text = GetTeamRemainingWeaponsList(1).Count.ToString();

            labelRedTeamAliveEntities.Text = GetTeamAlivePlatformsList(2).Count.ToString();
            labelRedTeamRemainingWeapons.Text = GetTeamRemainingWeaponsList(2).Count.ToString();

            // TEMPORARY, Honestly, can reuse code later probably, but for now its just temporary cuz i want baseline MVP functionality, although this is just debug writelines so not really MVP
            Debug.WriteLine("Blue Team Remaining Forces as a %:");
            int totalBluePlatforms = GetTeamAllPlatformsList(1).Count;
            int aliveBluePlatforms = GetTeamAlivePlatformsList(1).Count;
            Debug.WriteLine(totalBluePlatforms > 0 ? (aliveBluePlatforms / (double)totalBluePlatforms) * 100 : 0);

            Debug.WriteLine("Red Team Remaining Forces as a %:");
            int totalRedPlatforms = GetTeamAllPlatformsList(2).Count;
            int aliveRedPlatforms = GetTeamAlivePlatformsList(2).Count;
            Debug.WriteLine(totalRedPlatforms > 0 ? (aliveRedPlatforms / (double)totalRedPlatforms) * 100 : 0);

            Debug.WriteLine("Blue Team Remaining Weapons as a %:");
            int totalBlueWeapons = GetTeamAllWeaponsList(1).Count;
            int remainingBlueWeapons = GetTeamRemainingWeaponsList(1).Count;
            Debug.WriteLine(totalBlueWeapons > 0 ? (remainingBlueWeapons / (double)totalBlueWeapons) * 100 : 0);

            Debug.WriteLine("Red Team Remaining Weapons as a %:");
            int totalRedWeapons = GetTeamAllWeaponsList(2).Count;
            int remainingRedWeapons = GetTeamRemainingWeaponsList(2).Count;
            Debug.WriteLine(totalRedWeapons > 0 ? (remainingRedWeapons / (double)totalRedWeapons) * 100 : 0);

            Debug.WriteLine("Blue Team Remaining Ground Forces as a %:");
            int aliveGroundBluePlatforms = GetTeamAliveGroundPlatformsList(1).Count;
            int groundBluePlatforms = GetTeamGroundPlatformsList(1).Count;
            Debug.WriteLine(groundBluePlatforms > 0 ? (aliveGroundBluePlatforms / (double)groundBluePlatforms) * 100 : 0);

            Debug.WriteLine("Red Team Remaining Ground Forces as a %:");
            int aliveGroundRedPlatforms = GetTeamAliveGroundPlatformsList(2).Count;
            int groundRedPlatforms = GetTeamGroundPlatformsList(2).Count;
            Debug.WriteLine(groundRedPlatforms > 0 ? (aliveGroundRedPlatforms / (double)groundRedPlatforms) * 100 : 0);

            Debug.WriteLine("Blue Team Remaining Air Forces as a %:");
            int aliveAirBluePlatforms = GetTeamAliveAirPlatformsList(1).Count;
            int airBluePlatforms = GetTeamAirPlatformsList(1).Count;
            Debug.WriteLine(airBluePlatforms > 0 ? (aliveAirBluePlatforms / (double)airBluePlatforms) * 100 : 0);

            Debug.WriteLine("Red Team Remaining Air Forces as a %:");
            int aliveAirRedPlatforms = GetTeamAliveAirPlatformsList(2).Count;
            int airRedPlatforms = GetTeamAirPlatformsList(2).Count;
            Debug.WriteLine(airRedPlatforms > 0 ? (aliveAirRedPlatforms / (double)airRedPlatforms) * 100 : 0);

            Debug.WriteLine("Blue Team Remaining Sea Forces as a %:");
            int aliveSeaBluePlatforms = GetTeamAliveSeaPlatformsList(1).Count;
            int seaBluePlatforms = GetTeamSeaPlatformsList(1).Count;
            Debug.WriteLine(seaBluePlatforms > 0 ? (aliveSeaBluePlatforms / (double)seaBluePlatforms) * 100 : 0);

            Debug.WriteLine("Red Team Remaining Sea Forces as a %:");
            int aliveSeaRedPlatforms = GetTeamAliveSeaPlatformsList(2).Count;
            int seaRedPlatforms = GetTeamSeaPlatformsList(2).Count;
            Debug.WriteLine(seaRedPlatforms > 0 ? (aliveSeaRedPlatforms / (double)seaRedPlatforms) * 100 : 0);

            Debug.WriteLine("Blue Team Remaining Space Forces as a %:");
            int aliveSpaceBluePlatforms = GetTeamAliveSpacePlatformsList(1).Count;
            int spaceBluePlatforms = GetTeamSpacePlatformsList(1).Count;
            Debug.WriteLine(spaceBluePlatforms > 0 ? (aliveSpaceBluePlatforms / (double)spaceBluePlatforms) * 100 : 0);

            Debug.WriteLine("Red Team Remaining Space Forces as a %:");
            int aliveSpaceRedPlatforms = GetTeamAliveSpacePlatformsList(2).Count;
            int spaceRedPlatforms = GetTeamSpacePlatformsList(2).Count;
            Debug.WriteLine(spaceRedPlatforms > 0 ? (aliveSpaceRedPlatforms / (double)spaceRedPlatforms) * 100 : 0);

            UpdateCharts();
        }

        private void UpdateCharts()
        {
            // Sanity-check
            if (tabControlTeamsGraphs == null || tabPageTeams == null || tabPageIndividuals == null || tabPageTypes == null)
                return;
            // Also ensure the chart controls are not null or disposed
            if (chart1 == null || chart2 == null || chart3Blue == null || chart3Red == null ||
                chart4 == null || chart5 == null || chartTypesKillsTime == null || chartTypesAccuracy == null)
            {
                return;
            }

            // ===============================================================
            // If the current tab is the TEAMS tab...
            // ===============================================================
            if (tabControlTeamsGraphs.SelectedTab == tabPageTeams)
            {
                // -----------------------------------------------------------
                // 1) chart1: TEAM KILLS (Bar Chart)
                // -----------------------------------------------------------
                chart1.Series.Clear();
                if (chart1.ChartAreas.Count == 0)
                    chart1.ChartAreas.Add(new ChartArea("TeamKillsArea"));

                chart1.Titles.Clear();
                chart1.Titles.Add("Team Kills (Bar Chart)");

                int blueTeamKills = GetTeamKills(1);
                int redTeamKills = GetTeamKills(2);

                Series killsSeries = new Series("Team Kills")
                {
                    ChartType = SeriesChartType.Column
                };
                killsSeries.Points.AddXY("Blue Team", blueTeamKills);
                killsSeries.Points.AddXY("Red Team", redTeamKills);
                // Optional: color differently
                killsSeries.Points[0].Color = Color.Blue;
                killsSeries.Points[1].Color = Color.Red;

                chart1.Series.Add(killsSeries);

                var areaTeamBar = chart1.ChartAreas[0];
                areaTeamBar.AxisX.Title = "Team";
                areaTeamBar.AxisY.Title = "Kills";
                areaTeamBar.AxisX.Interval = 1;

                int maxKills = Math.Max(blueTeamKills, redTeamKills);
                areaTeamBar.AxisY.Minimum = 0;
                areaTeamBar.AxisY.Maximum = (maxKills == 0) ? 1 : maxKills + 1;

                // Make the Y axis increment by integer values
                areaTeamBar.AxisY.Interval = 1;
                areaTeamBar.AxisY.LabelStyle.Format = "0";

                chart1.Invalidate();

                // -----------------------------------------------------------
                // 2) chart2: TEAM KILLS OVER TIME (StepLine)
                // -----------------------------------------------------------
                chart2.Series.Clear();
                if (chart2.ChartAreas.Count == 0)
                    chart2.ChartAreas.Add(new ChartArea("TeamKillsOverTimeArea"));

                chart2.Titles.Clear();
                chart2.Titles.Add("Team Kills Over Time (StepLine)");

                // Record the newest kill snapshot
                if (simulationStartTime != default(DateTime))
                {
                    // Add or update the killHistory
                    killHistory.Add((DateTime.Now, blueTeamKills, redTeamKills));
                }

                Series blueSeries = new Series("Blue Team")
                {
                    ChartType = SeriesChartType.StepLine,
                    Color = Color.Blue,
                    BorderWidth = 3
                };
                Series redSeries = new Series("Red Team")
                {
                    ChartType = SeriesChartType.StepLine,
                    Color = Color.Red,
                    BorderWidth = 3
                };

                // Populate from killHistory
                foreach (var record in killHistory)
                {
                    double timeSec = (record.Timestamp - simulationStartTime).TotalSeconds;
                    blueSeries.Points.AddXY(timeSec, record.BlueKills);
                    redSeries.Points.AddXY(timeSec, record.RedKills);
                }

                chart2.Series.Add(blueSeries);
                chart2.Series.Add(redSeries);

                var areaTeamLine = chart2.ChartAreas[0];
                areaTeamLine.AxisX.Minimum = 0;

                double lastTimeSec = killHistory.Any()
                    ? (killHistory.Last().Timestamp - simulationStartTime).TotalSeconds
                    : 0.0;
                double buffer = 5.0;
                areaTeamLine.AxisX.Maximum = Math.Max(lastTimeSec + buffer, 0.1);

                double step = areaTeamLine.AxisX.Maximum / 4.0;
                areaTeamLine.AxisX.Interval = step;
                areaTeamLine.AxisX.MajorGrid.Interval = step;
                areaTeamLine.AxisX.MajorTickMark.Interval = step;
                areaTeamLine.AxisX.LabelStyle.Format = "0";

                areaTeamLine.AxisX.Title = "Time (s)";
                areaTeamLine.AxisY.Title = "Kills";

                // Make the Y axis increment by integer values
                areaTeamLine.AxisY.Interval = 1;
                areaTeamLine.AxisY.LabelStyle.Format = "0";

                chart2.Invalidate();

                // -----------------------------------------------------------
                // 3) chart3Blue: BLUE TEAM ACCURACY (Pie Chart)
                // -----------------------------------------------------------
                chart3Blue.Series.Clear();
                chart3Blue.Titles.Clear();
                chart3Blue.ChartAreas.Clear();

                chart3Blue.ChartAreas.Add(new ChartArea("BlueAccuracyArea"));
                chart3Blue.Titles.Add("Blue Team Accuracy");

                var (blueShots, blueHits) = ComputeShotsForTeam(1);
                Series sBlue = chart3Blue.Series.Add("BlueAccuracy");
                sBlue.ChartType = SeriesChartType.Pie;


                // Show percentage labels
                sBlue.IsValueShownAsLabel = true;
                sBlue.Label = "#PERCENT{P0}";
           

                // Add points
                int idxBlueHits = sBlue.Points.AddXY("Hits", blueHits);
                sBlue.Points[idxBlueHits].Color = Color.Blue;
                // Force visible label with text
                sBlue.Points[idxBlueHits].Label = "Hits #PERCENT{P0}";

                int idxBlueMisses = sBlue.Points.AddXY("Misses", (blueShots - blueHits));
                sBlue.Points[idxBlueMisses].Color = Color.DarkGray;
                sBlue.Points[idxBlueMisses].Label = "Misses #PERCENT{P0}";

                chart3Blue.Invalidate();

                // -----------------------------------------------------------
                // 4) chart3Red: RED TEAM ACCURACY (Pie Chart)
                // -----------------------------------------------------------
                chart3Red.Series.Clear();
                chart3Red.Titles.Clear();
                chart3Red.ChartAreas.Clear();

                chart3Red.ChartAreas.Add(new ChartArea("RedAccuracyArea"));
                chart3Red.Titles.Add("Red Team Accuracy");

                var (redShots, redHits) = ComputeShotsForTeam(2);
                Series sRed = chart3Red.Series.Add("RedAccuracy");
                sRed.ChartType = SeriesChartType.Pie;

                // Show percentage labels
                sRed.IsValueShownAsLabel = true;
                sRed.Label = "#PERCENT{P0}";

                // Add points
                int idxRedHits = sRed.Points.AddXY("Hits", redHits);
                sRed.Points[idxRedHits].Color = Color.Red;
                sRed.Points[idxRedHits].Label = "Hits #PERCENT{P0}";

                int idxRedMisses = sRed.Points.AddXY("Misses", (redShots - redHits));
                sRed.Points[idxRedMisses].Color = Color.DarkGray;
                sRed.Points[idxRedMisses].Label = "Misses #PERCENT{P0}";

                chart3Red.Invalidate();
            }
            // ===============================================================
            // If the current tab is the INDIVIDUALS tab...
            // ===============================================================
            else if (tabControlTeamsGraphs.SelectedTab == tabPageIndividuals)
            {
                // -----------------------------------------------------------
                // 1) chart4: INDIVIDUAL KILLS OVER TIME (StepLine)
                // -----------------------------------------------------------
                chart4.Series.Clear();
                if (chart4.ChartAreas.Count == 0)
                    chart4.ChartAreas.Add(new ChartArea("IndividualKillsArea"));

                chart4.Titles.Clear();
                chart4.Titles.Add("Individual Kills Over Time");

                // Check if a plane is selected
                if (comboBoxPlaneSelection == null || comboBoxPlaneSelection.SelectedItem == null)
                {
                    // If no selection, just clear chart5 as well
                    chart4.Invalidate();

                    chart5.Series.Clear();
                    chart5.Titles.Clear();
                    chart5.ChartAreas.Clear();
                    return;
                }

                // We have a valid plane selection
                var selectedPlane = (PlatformObject)comboBoxPlaneSelection.SelectedItem;

                Series planeStepSeries = new Series($"{selectedPlane.Name} Kills")
                {
                    ChartType = SeriesChartType.StepLine,
                    Color = Color.Blue,
                    BorderWidth = 3
                };

                // Fill from plane's kill record
                foreach (var record in selectedPlane.PerPlaneKillRecords)
                {
                    double timeSec = (record.Timestamp - simulationStartTime).TotalSeconds;
                    planeStepSeries.Points.AddXY(timeSec, record.PlaneKillCount);
                }

                chart4.Series.Add(planeStepSeries);

                var areaIndividual = chart4.ChartAreas[0];
                areaIndividual.AxisX.Minimum = 0;

                double currentTimeSec = (DateTime.Now - simulationStartTime).TotalSeconds;
                double lastKillSec = selectedPlane.PerPlaneKillRecords.Any()
                    ? (selectedPlane.PerPlaneKillRecords.Last().Timestamp - simulationStartTime).TotalSeconds
                    : 0.0;

                double buffer = 5.0;
                double maxX = Math.Max(currentTimeSec, lastKillSec) + buffer;
                areaIndividual.AxisX.Maximum = Math.Max(maxX, 0.1);

                double stepPlane = areaIndividual.AxisX.Maximum / 4.0;
                areaIndividual.AxisX.Interval = stepPlane;
                areaIndividual.AxisX.MajorGrid.Interval = stepPlane;
                areaIndividual.AxisX.MajorTickMark.Interval = stepPlane;
                areaIndividual.AxisX.LabelStyle.Format = "0";

                areaIndividual.AxisX.Title = "Time (s)";
                areaIndividual.AxisY.Title = "Kills";

                // Make the Y axis increment by integer values
                areaIndividual.AxisY.Interval = 1;
                areaIndividual.AxisY.LabelStyle.Format = "0";

                chart4.Invalidate();

                // -----------------------------------------------------------
                // 2) chart5: INDIVIDUAL ACCURACY (Pie Chart)
                // -----------------------------------------------------------
                chart5.Series.Clear();
                chart5.Titles.Clear();
                chart5.ChartAreas.Clear();

                chart5.ChartAreas.Add(new ChartArea("IndividualAccuracyArea"));
                chart5.Titles.Add($"Accuracy for {selectedPlane.Name}");

                // Shots & hits for the selected plane
                var (shots, hits) = ComputeShotsForPlatform(selectedPlane);

                Series indivAccuracySeries = new Series("IndividualAccuracy")
                {
                    ChartType = SeriesChartType.Pie
                };

                // Show percentage labels
                indivAccuracySeries.IsValueShownAsLabel = true;
                indivAccuracySeries.Label = "#PERCENT{P0}";

                // Add points
                int idxIndHits = indivAccuracySeries.Points.AddXY("Hits", hits);
                indivAccuracySeries.Points[idxIndHits].Color = Color.Purple;
                indivAccuracySeries.Points[idxIndHits].Label = "Hits #PERCENT{P0}";

                int idxIndMisses = indivAccuracySeries.Points.AddXY("Misses", (shots - hits));
                indivAccuracySeries.Points[idxIndMisses].Color = Color.DarkGray;
                indivAccuracySeries.Points[idxIndMisses].Label = "Misses #PERCENT{P0}";

                chart5.Series.Add(indivAccuracySeries);
                chart5.Invalidate();
            }
            // ===============================================================
            // If the current tab is the TYPES tab...
            // ===============================================================
            else if (tabControlTeamsGraphs.SelectedTab == tabPageTypes)
            {
                // -----------------------------------------------------------
                // 1) chartTypesKillsTime: TYPES KILLS OVER TIME (StepLine)
                // -----------------------------------------------------------
                chartTypesKillsTime.Series.Clear();
                if (chartTypesKillsTime.ChartAreas.Count == 0)
                    chartTypesKillsTime.ChartAreas.Add(new ChartArea("TypesKillsArea"));

                chartTypesKillsTime.Titles.Clear();
                chartTypesKillsTime.Titles.Add("Types Kills Over Time");

                if (comboBoxPlaneTypeSelection == null || comboBoxPlaneTypeSelection.SelectedItem == null)
                {
                    chartTypesKillsTime.Invalidate();
                    chartTypesAccuracy.Series.Clear();
                    chartTypesAccuracy.Titles.Clear();
                    chartTypesAccuracy.ChartAreas.Clear();
                    return;
                }

                var selectedType = (PlatformTypeGroup)comboBoxPlaneTypeSelection.SelectedItem;

                Series planeStepSeries = new Series($"{selectedType.Type} Kills")
                {
                    ChartType = SeriesChartType.StepLine,
                    Color = Color.Blue,
                    BorderWidth = 3
                };

                double latestKillSec = double.MinValue;

                // Aggregate kills over time across all platforms of this type
                SortedDictionary<double, int> killsOverTime = new SortedDictionary<double, int>();

                foreach (PlatformObject platform in selectedType.Platforms)
                {
                    foreach (var record in platform.PerPlaneKillRecords)
                    {
                        double timeSec = (record.Timestamp - simulationStartTime).TotalSeconds;
                        if (!killsOverTime.ContainsKey(timeSec))
                            killsOverTime[timeSec] = 0;

                        killsOverTime[timeSec] += record.PlaneKillCount;

                        if (timeSec > latestKillSec)
                            latestKillSec = timeSec;
                    }
                }

                // Plot cumulative kills over time
                int cumulativeKills = 0;
                foreach (var kvp in killsOverTime.OrderBy(k => k.Key))
                {
                    cumulativeKills += kvp.Value;
                    planeStepSeries.Points.AddXY(kvp.Key, cumulativeKills);
                }

                chartTypesKillsTime.Series.Add(planeStepSeries);

                var areaType = chartTypesKillsTime.ChartAreas[0];
                areaType.AxisX.Minimum = 0;

                double currentTimeSec = (DateTime.Now - simulationStartTime).TotalSeconds;
                double buffer = 5.0;
                double maxX = Math.Max(currentTimeSec, latestKillSec) + buffer;
                areaType.AxisX.Maximum = Math.Max(maxX, 0.1);

                double stepPlane = areaType.AxisX.Maximum / 4.0;
                areaType.AxisX.Interval = stepPlane;
                areaType.AxisX.MajorGrid.Interval = stepPlane;
                areaType.AxisX.MajorTickMark.Interval = stepPlane;
                areaType.AxisX.LabelStyle.Format = "0";

                areaType.AxisX.Title = "Time (s)";
                areaType.AxisY.Title = "Kills";

                // Make the Y axis increment by integer values
                areaType.AxisY.Interval = 1;
                areaType.AxisY.LabelStyle.Format = "0";

                chartTypesKillsTime.Invalidate();

                // -----------------------------------------------------------
                // 2) chartTypesAccuracy: TYPES ACCURACY (Pie Chart)
                // -----------------------------------------------------------
                chartTypesAccuracy.Series.Clear();
                chartTypesAccuracy.Titles.Clear();
                chartTypesAccuracy.ChartAreas.Clear();

                chartTypesAccuracy.ChartAreas.Add(new ChartArea("IndividualAccuracyArea"));
                chartTypesAccuracy.Titles.Add($"Accuracy for {selectedType.Type}");

                int shotsTotal = 0;
                int hitsTotal = 0;

                foreach (PlatformObject platform in selectedType.Platforms)
                {
                    var (shots, hits) = ComputeShotsForPlatform(platform);
                    shotsTotal += shots;
                    hitsTotal += hits;
                }

                Series typeAccuracySeries = new Series("TypeAccuracy")
                {
                    ChartType = SeriesChartType.Pie,
                    IsValueShownAsLabel = true,
                    Label = "#PERCENT{P0}"
                };

                // Add points
                int idxTypeHits = typeAccuracySeries.Points.AddXY("Hits", hitsTotal);
                typeAccuracySeries.Points[idxTypeHits].Color = Color.Purple;
                typeAccuracySeries.Points[idxTypeHits].Label = "Hits #PERCENT{P0}";

                int idxTypeMisses = typeAccuracySeries.Points.AddXY("Misses", shotsTotal - hitsTotal);
                typeAccuracySeries.Points[idxTypeMisses].Color = Color.DarkGray;
                typeAccuracySeries.Points[idxTypeMisses].Label = "Misses #PERCENT{P0}";

                chartTypesAccuracy.Series.Add(typeAccuracySeries);
                chartTypesAccuracy.Invalidate();
            }
            // ===============================================================
            // If neither Teams nor Individuals nor Types tab...
            // ===============================================================
            else
            {
                // Optionally clear all charts if you're on an unrelated tab
                chart1.Series.Clear();
                chart2.Series.Clear();
                chart3Blue.Series.Clear();
                chart3Red.Series.Clear();
                chart4.Series.Clear();
                chart5.Series.Clear();
                chartTypesKillsTime.Series.Clear();
                chartTypesAccuracy.Series.Clear();
                comboBoxPlaneTypeSelection.DataSource = null;
                comboBoxPlaneTypeSelection = null;
            }
        }



        //used for kills over time graphs
        private void SetupKillHistoryTimer()
        {
            if (killHistoryTimer != null)
            {
                killHistoryTimer.Stop();
                killHistoryTimer.Dispose();
            }

            killHistoryTimer = new Timer();
            killHistoryTimer.Interval = 1000; // update every second
            killHistoryTimer.Tick += (s, e) =>
            {
                updateMainStatistics(); // This will now call UpdateCharts and update both chart1 and chart2
            };
            killHistoryTimer.Start();
        }

        /// <summary>
        /// Compute the total shots fired vs. total hits for a single platform.
        /// Includes both bullets (GunObjects) and missiles/rockets (WeaponObjects).
        /// </summary>
        private (int totalShots, int totalHits) ComputeShotsForPlatform(PlatformObject p)
        {
            // Sum bullet hits & misses
            int bulletHits = 0;
            int bulletMisses = 0;
            foreach (var gun in p.GunObjects)
            {
                bulletHits += gun.Hits;
                bulletMisses += gun.Misses;
            }
            int totalBulletShots = bulletHits + bulletMisses;

            // Sum missiles/bombs/rockets fired
            // Each WeaponObject = exactly one munition. If Fired == true, that's one shot.
            int missileShots = p.WeaponObjects.Count(w => w.Fired);
            int missileHits = p.WeaponObjects.Count(w => w.Hit);

            int totalShots = totalBulletShots + missileShots;
            int totalHits = bulletHits + missileHits;

            return (totalShots, totalHits);
        }

        /// <summary>
        /// Compute total shots fired & total hits for an entire team (all platforms).
        /// </summary>
        private (int totalShots, int totalHits) ComputeShotsForTeam(int team)
        {
            int sumShots = 0;
            int sumHits = 0;

            foreach (var platform in platformObjects.Where(p => p.Team == team))
            {
                var (platformShots, platformHits) = ComputeShotsForPlatform(platform);
                sumShots += platformShots;
                sumHits += platformHits;
            }

            return (sumShots, sumHits);
        }

        


        private void UpdateChartsByType()
        {
            string selectedType = comboBoxPlaneTypeSelection.SelectedItem as string;
            if (string.IsNullOrEmpty(selectedType))
                return;

            var platformsOfType = platformObjects.Where(p => p.Type == selectedType).ToList();

            // You can now aggregate data across `platformsOfType` and update charts.
            // For example, sum all weapons or average some values.
        }

        private void InitUI()
        {
            dataGridViewMainPage.DataSource = platformObjects.ToArray();

            foreach (PlatformObject platformObject in platformObjects)
            {
                TabPage tabPage = new TabPage(platformObject.Name);
                IndividualPlaneControl customControl = new IndividualPlaneControl(platformObject);
                customControl.Dock = DockStyle.Fill;
                tabPage.Controls.Add(customControl);
            }

            // For the individuals tab
            {
                comboBoxPlaneSelection = new ComboBox();
                comboBoxPlaneSelection.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBoxPlaneSelection.Location = new Point(10, 10); // pick a location on tabPageIndividuals
                comboBoxPlaneSelection.Width = 200;

                comboBoxPlaneSelection.DisplayMember = "Name";
                // Fill with all platforms (assuming 'platformObjects' is a valid list)
                comboBoxPlaneSelection.DataSource = platformObjects;

                // Whenever user picks a plane, re-draw the charts
                comboBoxPlaneSelection.SelectedIndexChanged += (s, e) => UpdateCharts();

                // Add it to the "Individuals" tab:
                tabPageIndividuals.Controls.Add(comboBoxPlaneSelection);
            }

            // For the types tab
            {
                comboBoxPlaneTypeSelection = new ComboBox();
                comboBoxPlaneTypeSelection.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBoxPlaneTypeSelection.Location = new Point(10, 10); // pick a location on tabPageIndividuals
                comboBoxPlaneTypeSelection.Width = 200;

                // get all of the types of platformObjects
                var groupedTypes = platformObjects.GroupBy(p => p.Type).Select(g => new PlatformTypeGroup { Type = g.Key, Platforms = g.ToList() }).ToList();


                // Fill with all platforms (assuming 'platformObjects' is a valid list)
                comboBoxPlaneTypeSelection.DataSource = groupedTypes;
                comboBoxPlaneTypeSelection.DisplayMember = "Type";
                //comboBoxPlaneTypeSelection.SelectedIndexChanged += (s, e) => UpdateChartsByType();

                // Whenever user picks a plane, re-draw the charts
                comboBoxPlaneTypeSelection.SelectedIndexChanged += (s, e) => UpdateCharts();

                // Add it to the "Individuals" tab:
                tabPageTypes.Controls.Add(comboBoxPlaneTypeSelection);
            }


            //logic for updating chart view based on tabs
            tabControlTeamsGraphs.SelectedIndexChanged += tabControlTeamsGraphs_SelectedIndexChanged;

            UpdateCharts();
            updateMainStatistics();
        }

        #endregion

        #region "MACE Event Handlers"
        /// <summary>
        /// This event is called whenever a weapon is launched/fired in MACE. Use this handler to get information about the
        /// weapon launch event. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void HandleWeaponFire(object sender, EventArgs e)
        {
            try
            {
                IMission.WeaponFireEventArgs args = e as IMission.WeaponFireEventArgs;
                IPhysicalEntity fe = args.FiringEntity;
                IPhysicalEntity me = args.MunitionEntity;
                PlatformObject OurFiringEntity = FindPlatformFromName(fe.Name);

                // Have to find out if it's a bullet instead of a missile right here, but it's not super easy
                // We can't use our helper function to figure it out, because it relies on the bullet entity already being in the object model
                // This is adding that entity to the object model, since bullet entities are only created when the gun is fired
                // Please do not touch this without asking me first -anthony

                GunObject OurGunObject = findGunFromName(me.Name);

                if (OurGunObject != null)
                {
                    // It's a bullet
                    OurGunObject.RemainingBullets--;
                    OurGunObject.ActiveBulletEntityIDs.Add(me.ID);

                    // NEW LINE: store the plane’s name in FiringPlatformName
                    OurGunObject.FiringPlatformName = fe.Name;
                    return;
                }

                // Should only get here if it's not a bullet
                WeaponObject OurWeaponObject = FindWeaponFromWeaponID(me.ID);

                OurWeaponObject.Fired = true;
                // note(anthony): While testing after a plane evaded a missile, it managed to shoot one back, but it failed to set a target on it which caused an error, it shouldn't now
                OurWeaponObject.TargetName = me.TargetAssigned?.Name ?? "None";
                OurWeaponObject.TargetLat = me.TargetLocationAssigned?.Latitude_degrees ?? 0;
                OurWeaponObject.TargetLon = me.TargetLocationAssigned?.Longitude_degrees ?? 0;

                // For non-bullet weapons, store the plane name in OwnshipName
                OurWeaponObject.OwnshipName = fe.Name;

                updateMainStatistics();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine("HandleWeaponFire");
                //_mission.Logger.ErrorMessage(ex);
            }
        }


        /// <summary>
        /// This event is called whenever weapon damage occurs in MACE. Use this handler to get information about the 
        /// weapon damage event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <summary>
        /// This event is called whenever weapon damage occurs in MACE. Use this handler to get information about the 
        /// weapon damage event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void HandleWeaponDamage(object sender, EventArgs e)
        {
            try
            {
                IMission.WeaponDamageEventArgs args = e as IMission.WeaponDamageEventArgs;
                IPhysicalEntity te = args.TargetEntity;
                IPhysicalEntity me = args.MunitionEntity;
                Double damage_percent = args.DamagePercent; // 0-1 so 50% is 0.5
                PlatformObject OurTargetEntity = FindPlatformFromName(te.Name);

                if (isWeaponABullet(me.ID))
                { // Bullet
                    GunObject OurGunObject = findGunFromBulletID(me.ID);
                    OurGunObject.ActiveBulletEntityIDs.Remove(me.ID);
                    OurGunObject.RegisterBulletHit(OurTargetEntity);

                    if (te.Health <= 0 && OurTargetEntity.Alive)
                    {
                        OurTargetEntity.Alive = false;
                        OurGunObject.KilledPlatforms.Add(OurTargetEntity);

                        // NEW CODE: Log the kill in the plane's PerPlaneKillRecords, using FiringPlatformName.
                        if (!string.IsNullOrEmpty(OurGunObject.FiringPlatformName))
                        {
                            PlatformObject firingPlane = FindPlatformFromName(OurGunObject.FiringPlatformName);
                            if (firingPlane != null)
                            {
                                firingPlane.PerPlaneKillRecords.Add(
                                    (DateTime.Now, firingPlane.Kills)
                                );
                            }
                        }
                    }
                }
                else
                { // Missile/Non-bullet
                    WeaponObject OurWeaponObject = FindWeaponFromWeaponID(me.ID);
                    OurWeaponObject.Hit = true;
                    if (te.Health <= 0 && OurTargetEntity.Alive)
                    {
                        OurTargetEntity.Alive = false;
                        OurWeaponObject.ResultedInKill = true;

                        // For missiles or bombs, we use OwnshipName
                        if (!string.IsNullOrEmpty(OurWeaponObject.OwnshipName))
                        {
                            PlatformObject firingPlane = FindPlatformFromName(OurWeaponObject.OwnshipName);
                            if (firingPlane != null)
                            {
                                firingPlane.PerPlaneKillRecords.Add(
                                    (DateTime.Now, firingPlane.Kills)
                                );
                            }
                        }
                    }
                }

                updateMainStatistics();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine("HandleWeaponDamage");
                // to log in the error log:
                _mission.Logger.ErrorMessage(ex);
            }

        }

        /// <summary>
        /// This event is called whenever a weapon detonates in MACE. Use this handler to get information about the 
        /// detonation event. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void HandleWeaponDetonated(object sender, EventArgs e)
        {
            try
            {
                // note(anthony): this is called after HandleWeaponDamage, or completely on its own if the weapon resulted in zero damage
                IMission.WeaponDetonationEventArgs args = e as IMission.WeaponDetonationEventArgs;
                IPhysicalEntity me = args.MunitionEntity;
                
                if (isWeaponABullet(me.ID))
                {
                    GunObject OurGunObject = findGunFromBulletID(me.ID);
                    OurGunObject.ActiveBulletEntityIDs.Remove(me.ID);
                    OurGunObject.Misses++;
                }
                else
                {
                    WeaponObject OurWeaponObject = FindWeaponFromWeaponID(me.ID);
                    OurWeaponObject.Detonated = true;
                }

                updateMainStatistics();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine("HandleWeaponDetonated");

                // to log in the error log:
                _mission.Logger.ErrorMessage(ex);
            }
        }

        /// <summary>
        /// Handle entity property changes. Add as a delegate to the PropertyChange
        /// event on IPhysicalEntity instances of interest. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void HandleEntityPropertyChanges(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                updateMainStatistics();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine("HandleEntityPropertyChanges");
                // to log in the error log:
                //_mission.Logger.ErrorMessage(ex);
            }
        }

        /// <summary>
        /// Handle the map draw complete event.  This event is handle on the UI/foreground thread. 
        /// Avoid doing computationally expense tasks in this event handler, as doing so 
        /// can adversely affect MACE performance. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void HandleMapDrawComplete(object sender, EventArgs e)
        {
            // attach this event handler with
            // _mission.Map.DrawingComplete += HandleWeaponDetonated;

            try
            {
                // YOUR CODE HERE
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                // to log in the error log:
                //_mission.Logger.ErrorMessage(ex);
            }
        }

        /// <summary>
        /// Handle the motion processing complete event. This event is associated w/ MACE's 
        /// platform processor, which is handled on a background thread. Any 
        /// processing that requires access to the elemnents on the foreground thread (such as the
        /// UI), should instead leverage IMap.DrawingComplete instead.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Current mission time in seconds.</param>
        protected void HandlePlatformMotionComplete(object sender, double e)
        {
            // attach this event handler with
            //_mission.PlatformMotionComplete += HandlePlatformMotionComplete;

            try
            {
                // this handler is called every frame when mace is done updating the platform motion, 
                // approximately 50Hz. This is a good place for handling general periodic updates 
                // in a manner similar to a game loop. This event is not processed on
                // the foreground thread.
                //
                // WARNING: DON'T PLACE UI UPDATES HERE!! Use the
                // IMap.DrawingComplete event instead. 

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                // to log in the error log:
                //_mission.Logger.ErrorMessage(ex);
            }
        }

        /// <summary>
        /// Handle the notification that the MACE scenario has changed state
        /// </summary>
        protected void HandleScenarioStateChanges(object sender, EventArgs e)
        {
            try
            {
                //IMission.StateChangedEventArgs args = e as IMission.StateChangedEventArgs;
                //this.State_Label.Text = Enum.GetName(typeof(IMission.StateEnum), _mission.State);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                // to log in the error log:
                //_mission.Logger.ErrorMessage(ex);
            }
        }

        /// <summary>
        /// Handle the collection changed notification of entity additions/removal from the mission.
        /// </summary>
        protected void HandleEntityChanges(object sender, NotifyCollectionChangedEventArgs args)
        {
            try
            {
                updateMainStatistics();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }


        #endregion

        #region "Form Event Handlers"

        /// <summary>
        /// Set initial status and register for events on form load
        /// </summary>
        private void SimulationSessionSummaryForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Set the initial MACE Mission state
                //this.State_Label.Text = Enum.GetName(typeof(IMission.StateEnum), _mission.State);
                //this.EntityCount_Label.Text = _mission.PhysicalEntities.Count.ToString();

                // Add a handler to be notified whenever our MACE mission changes state
                _mission.StateChanged += HandleScenarioStateChanges;

                // Add a handler to be notified whenever the a new entity is added or removed from the MACE mission.
                _mission.PhysicalEntities.CollectionChanged += HandleEntityChanges;

                // The following event handlers represent some commonly used events. 
                // There are event handlers in this template you can modify to use them. 
                // Simply uncomment any of the subscribers below that you may need or want.  
                // See the MACE API documentation for more information and to see other 
                // events you can leverage in your plugin.

                //_mission.PlatformMotionComplete += HandlePlatformMotionComplete;

                // note(anthony): uncomment these if issues are caused by them being in buttonStart_Click instead of originally here
                //_mission.WeaponDetonation += HandleWeaponDetonated;
                //_mission.WeaponFire += HandleWeaponFire;
                //_mission.WeaponDamage += HandleWeaponDamage;

                //_mission.Map.DrawingComplete += HandleMapDrawComplete;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        /// <summary>
        /// Form closing event handler. Remove event handlers when form is closing
        /// </summary>
        private void SimulationSessionSummaryForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            try
            {
                if (killHistoryTimer != null)
                {
                    killHistoryTimer.Stop();
                    killHistoryTimer.Dispose();
                    killHistoryTimer = null;
                }

                // Remove the handlers
                _mission.StateChanged -= HandleScenarioStateChanges;
                _mission.PhysicalEntities.CollectionChanged -= HandleEntityChanges;

                //_mission.PlatformMotionComplete -= HandlePlatformMotionComplete;
                _mission.WeaponDetonation -= HandleWeaponDetonated;
                _mission.WeaponFire -= HandleWeaponFire;
                _mission.WeaponDamage -= HandleWeaponDamage;

                // BE SURE TO CANCEL ANY THREADS AND TIMERS
                // OR CLEAN UP OBJECT ALLOCATIONS HERE!!

                // Remove the form instance from WindowController's managed forms
                _plugin.WindowConfigController.RemoveManagedForm(this);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Form visibility changed event handler.  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SimulationSessionSummaryForm_VisibleChanged(object sender, EventArgs e)
        {
            try
            {
                if (Visible == true)
                {
                    //
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        #endregion

        #region "WindowConfigChanged Event Handler"
        #endregion

        /// <summary>
        /// Handle a mouse click event on the MACE map.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleMapMouseDown(object sender, EventArgs e)
        {
            try
            {
                IMap.MouseEventArgs args = (IMap.MouseEventArgs)e;
                if (args.MouseEventArgs.Button == MouseButtons.Left)
                {
                    if (args.PhysicalEntity != null)
                    {
                        PhysicalEntityWrapper existing = _userSelectedEntities.FirstOrDefault(ent => ent.Entity == args.PhysicalEntity);
                        if (!_userSelectedEntities.Contains(existing))
                        {
                            _userSelectedEntities.Add(new PhysicalEntityWrapper(args.PhysicalEntity));
                            _userSelectedEntities.ResetBindings();
                        }
                        _mission.Map.CancelPlugInMapAction();
                    }
                }
                else if (args.MouseEventArgs.Button == MouseButtons.Right)
                {
                    _mission.Map.CancelPlugInMapAction();
                }
                _mission.Map.MouseDown -= HandleMapMouseDown;
            }
            catch (Exception ex)
            {
                _mission.Logger.ErrorMessage(ex);
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            // note(anthony): don't click this button when there are missiles active since those will be added as platforms which is obviously not going to play nice
            
            if (_mission.PhysicalEntities.Count == 0)
            {
                MessageBox.Show("Zero entities exist in the current mission!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }    

            foreach (KeyValuePair<ulong, IPhysicalEntity> kvp in _mission.PhysicalEntities)
            {
                var newEntity = kvp.Value;
                List<WeaponObject> weaponObjects = new List<WeaponObject>();
                List<GunObject> gunObjects = new List<GunObject>();

                if (newEntity.EntityType.Kind.ToString() == "1" || newEntity.EntityType.Kind.ToString() == "3")
                {
                    ulong startingBullets = 0;
                    if (newEntity.Weapons != null)
                    {
                        foreach (IWeaponModel weaponObject in newEntity.Weapons)
                        {
                            int tempType = (int)weaponObject.FunctionType;
                            if (tempType == 2 || tempType == 3 || tempType == 4)
                            {
                                // note(anthony): 4 is AirAndGroundWeapon, We can't safely assume that this is always the gun, but for now, we will
                                if (weaponObject.EquipmentWeapon.MountedMunitionsCount > 1)
                                {
                                    startingBullets = weaponObject.EquipmentWeapon.MountedMunitionsCount;
                                    GunObject gun = new GunObject(startingBullets, weaponObject.EquipmentWeapon.Name);
                                    gunObjects.Add(gun);
                                }
                                else
                                {
                                    WeaponObject weapon = new WeaponObject(weaponObject.Type, weaponObject.FunctionType.ToString(), weaponObject.ParentEntity.Name, weaponObject.ID);
                                    weaponObjects.Add(weapon);
                                }
                            }
                        }
                    }

                    PlatformObject newObject = new PlatformObject(newEntity.Name, newEntity.Type, (int)newEntity.TeamAffiliation, newEntity.Domain.ToString(), gunObjects, weaponObjects);
                    platformObjects.Add(newObject);
                }
            }

            InitUI();
            buttonStart.Enabled = false;
            buttonClearData.Enabled = true;

            simulationStartTime = DateTime.Now;
            killHistory.Clear();
            foreach (var plane in platformObjects)
            {
                plane.PerPlaneKillRecords.Add(
                    (simulationStartTime, 0)
                );
            }
            SetupKillHistoryTimer();

            _mission.WeaponDetonation += HandleWeaponDetonated;
            _mission.WeaponFire += HandleWeaponFire;
            _mission.WeaponDamage += HandleWeaponDamage;
        }

        private void buttonClearData_Click(object sender, EventArgs e)
        {
            platformObjects.Clear();
            dataGridViewMainPage.DataSource = null;

            // note(anthony): Clean up everything/anything

            buttonStart.Enabled = true;
            buttonClearData.Enabled = false;

            killHistoryTimer?.Stop();
            killHistoryTimer?.Dispose();
            killHistoryTimer = null;
            killHistory.Clear();
            chart2.Series.Clear();

            comboBoxPlaneTypeSelection.DataSource = null;

            updateMainStatistics();
        }

        private void buttonSaveXML_Click(object sender, EventArgs e)
        {
            // note(anthony): Some notes on serialization
            // Any and all [XmlIgnore] are because serialization will not work at all if included
            // It is possible to modify them to print anyway, for instance PlatformHitCounts can be turned into a list when serialized
            // I've not done that for now, since this is just for debugging
            // If we were to include this in the final release, it would probably be best to add [XmlIgnore] to a lot more properties
            //     and also maybe spit out a summary at the top if we don't support loading an xml and only saving one
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "XML Files (*.xml)|*.xml";
                saveFileDialog.Title = "Save Platform Objects";
                saveFileDialog.FileName = "platformObjects.xml";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(List<PlatformObject>));
                        using (TextWriter writer = new StreamWriter(saveFileDialog.FileName))
                        {
                            serializer.Serialize(writer, platformObjects.ToList());
                        }

                        MessageBox.Show("File saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error saving file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        
        private void buttonLoadXML_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "XML Files (*.xml)|*.xml";
                openFileDialog.Title = "Load Platform Objects";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        buttonClearData_Click(sender, e);
                        XmlSerializer serializer = new XmlSerializer(typeof(List<PlatformObject>));
                        using (TextReader reader = new StreamReader(openFileDialog.FileName))
                        {
                            List<PlatformObject> tempList = (List<PlatformObject>)serializer.Deserialize(reader);
                            platformObjects = new BindingList<PlatformObject>(tempList);  // Convert back to BindingList
                        }

                        MessageBox.Show("File loaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        InitUI();
                        buttonStart.Enabled = false;
                        buttonClearData.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void tabControlTeamsGraphs_SelectedIndexChanged(object sender, EventArgs e)
        {

            UpdateCharts();
    
        }
    }
}
