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
            // Calculate kills
            int blueTeamKills = GetTeamKills(1);
            int redTeamKills = GetTeamKills(2);

            // Clear existing series
            chart1.Series.Clear();

            // Create a new series for kills, using vertical columns
            Series killsSeries = new Series("Team Kills")
            {
                ChartType = SeriesChartType.Column
            };

            Debug.WriteLine($"Blue kills: {blueTeamKills}, Red kills: {redTeamKills}");

            // Add data points
            killsSeries.Points.AddXY("Blue Team", blueTeamKills);
            killsSeries.Points.AddXY("Red Team", redTeamKills);

            killsSeries.Points[0].Color = Color.Blue;
            killsSeries.Points[1].Color = Color.Red;

            // Add the series to the chart
            chart1.Series.Add(killsSeries);

            // Configure axes (optional)
            chart1.ChartAreas[0].AxisY.Title = "Kills";
            chart1.ChartAreas[0].AxisX.Title = "Team";
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Interval = 1;

            chart1.Update();
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
                if (platformObject.Team == 1) // Blue Team
                {
                    tabControlTeamBluePlanes.TabPages.Add(tabPage);
                }
                else if (platformObject.Team == 2) // Red Team
                {
                    tabControlTeamRedPlanes.TabPages.Add(tabPage);
                }
                // note(anthony): Anything not on blue or red team won't have a tab created for it right now
                // The data in these tabs will automatically update, but it's not so simple

            }

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

                if(OurGunObject != null)
                {
                    OurGunObject.RemainingBullets--;
                    OurGunObject.ActiveBulletEntityIDs.Add(me.ID);
                    return;
                }



                // Should only get here if it's not a bullet
                WeaponObject OurWeaponObject = FindWeaponFromWeaponID(me.ID);

                OurWeaponObject.Fired = true;
                // note(anthony): While testing after a plane evaded a missile, it managed to shoot one back, but it failed to set a target on it which caused an error, it shouldn't now
                OurWeaponObject.TargetName = me.TargetAssigned?.Name ?? "None";
                OurWeaponObject.TargetLat = me.TargetLocationAssigned.Latitude_degrees;
                OurWeaponObject.TargetLon = me.TargetLocationAssigned.Longitude_degrees;
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
                {
                    GunObject OurGunObject = findGunFromBulletID(me.ID);
                    OurGunObject.ActiveBulletEntityIDs.Remove(me.ID);
                    OurGunObject.RegisterBulletHit(OurTargetEntity);
                    if (te.Health <= 0)
                    {
                        OurTargetEntity.Alive = false;
                        OurGunObject.KilledPlatforms.Add(OurTargetEntity);
                    }
                }
                else
                {
                    WeaponObject OurWeaponObject = FindWeaponFromWeaponID(me.ID);
                    OurWeaponObject.Hit = true;
                    if (te.Health <= 0)
                    {
                        OurTargetEntity.Alive = false;
                        OurWeaponObject.ResultedInKill = true;
                    }
                }

                
                
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
            _mission.WeaponDetonation += HandleWeaponDetonated;
            _mission.WeaponFire += HandleWeaponFire;
            _mission.WeaponDamage += HandleWeaponDamage;
        }

        private void buttonClearData_Click(object sender, EventArgs e)
        {
            platformObjects.Clear();
            foreach (TabPage tabPage in tabControlTeamBluePlanes.TabPages)
            {
                tabControlTeamBluePlanes.TabPages.Remove(tabPage);
            }
            foreach (TabPage tabPage in tabControlTeamRedPlanes.TabPages)
            {
                tabControlTeamRedPlanes.TabPages.Remove(tabPage);
            }
            dataGridViewMainPage.DataSource = null;

            // note(anthony): Clean up anything else such as the graphs tab that we currently don't use

            buttonStart.Enabled = true;
            buttonClearData.Enabled = false;
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

        
    }
}
