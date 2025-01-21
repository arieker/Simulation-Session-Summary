using BSI.MACE;
using BSI.MACE.AI;
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
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        private List<IPhysicalEntity> ourEntityList = new List<IPhysicalEntity>();
        private List<PlatformObject> platformObjects = new List<PlatformObject>();

        /// <summary>
        /// Local reference to the plugin class -- used for window configuration save/restore
        /// </summary>
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
            dgvUserSelectedEntities.DataSource = _userSelectedEntities;

            // add this form instance to the window config controller as a managed form
            _plugin.WindowConfigController.AddManagedForm(this);
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
                // get more information about the fire event
                IMission.WeaponFireEventArgs args = e as IMission.WeaponFireEventArgs;
                //note(anthony): THESE TWO THINGS ARE BASICALLY EXACTLY!! WHAT WE NEED!
                IPhysicalEntity fe = args.FiringEntity;
                IPhysicalEntity me = args.MunitionEntity;
                //note(anthony): find out which plane fired this weapon (check the name of the ownship and reference this with our general list of all planes)
                // once we find which plane fired it, create a WeaponObject and add it to that planes list of weaponobjects
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
                IPhysicalEntity target = args.TargetEntity;
                IPhysicalEntity weapon = args.MunitionEntity;
                Double damage_percent = args.DamagePercent;
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
                // interpret the event args as a weapon detonation event and get more information about 
                // the type of weapon that detonated.

                IMission.WeaponDetonationEventArgs args = e as IMission.WeaponDetonationEventArgs;
                IPhysicalEntity weap = args.MunitionEntity;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine("3");

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
                /* note(anthony): yeah this is actually pointless since ourEntityList seems to be a list of pointers which updates automatically
                if (sender is IPhysicalEntity entity)
                {
                    for (int i = 0; i < ourEntityList.Count; i++)
                    {
                        if (ourEntityList[i].ID == entity.ID)
                        {
                            ourEntityList[i] = entity; // Update the existing entity
                            break;
                        }
                    }
                }
                */
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
                this.State_Label.Text = Enum.GetName(typeof(IMission.StateEnum), _mission.State);
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
                this.EntityCount_Label.Text = _mission.PhysicalEntities.Count.ToString();
                //note(anthony): so like, this may be completely useless? if we can just use _mission.PhysicalEntities for everything?? Why make anything custom at all?
                // we can just keep track of weapon detonation events and do all of our stuff when the mission ends or when a button is pressed in the winform
                // then we can make all our custom nice formatted lists and display the nice simulation session summary at the end
                // Removing
                if (args.Action == NotifyCollectionChangedAction.Remove && args.OldItems != null)
                {
                    foreach (KeyValuePair<ulong, IPhysicalEntity> kvp in args.OldItems)
                    {
                        var removedEntity = kvp.Value;

                        var ew = _userSelectedEntities.FirstOrDefault(ent => ent.Entity == removedEntity);
                        if (ew != null)
                        {
                            _userSelectedEntities.Remove(ew);
                            _userSelectedEntities.ResetBindings();
                        }

                        var entityToRemove = ourEntityList.FirstOrDefault(ent => ent.ID == kvp.Key);
                        if (entityToRemove != null)
                        {
                            ourEntityList.Remove(entityToRemove);
                        }
                    }
                }

                // Adding
                if (args.Action == NotifyCollectionChangedAction.Add && args.NewItems != null)
                {
                    foreach (KeyValuePair<ulong, IPhysicalEntity> kvp in args.NewItems)
                    {
                        var newEntity = kvp.Value;

                        // note(anthony): last sanity check to make sure it doesnt exist
                        if (!ourEntityList.Any(ent => ent.ID == kvp.Key))
                        {
                            ourEntityList.Add(newEntity);
                        }
                    }
                }

                // note(anthony): this may not be necessary if we never need to use that event but lets just subscribe to it anyway why not
                foreach (IPhysicalEntity entity in _mission.PhysicalEntities.Values)
                {
                    entity.PropertyChanged += HandleEntityPropertyChanges;
                }
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
                this.State_Label.Text = Enum.GetName(typeof(IMission.StateEnum), _mission.State);
                this.EntityCount_Label.Text = _mission.PhysicalEntities.Count.ToString();

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
                //note(anthony) NOTEWORTHY BELOW EVENTS LOOK INTO THEM IN THE FUTURE!
                _mission.WeaponDetonation += HandleWeaponDetonated;
                _mission.WeaponFire += HandleWeaponFire;
                _mission.WeaponDamage += HandleWeaponDamage;
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
                //_mission.WeaponDetonation -= HandleWeaponDetonated;
                //_mission.WeaponFire -= HandleWeaponFire;

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

        /// <summary>
        /// Create entity button click event handler. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateEntity_Click(object sender, EventArgs e)
        {
            try
            {
                // It's not an uncommon aim of plugin developers to create entities or to modify 
                // entity parameters when certain events or user input occur. This
                // button handler spawns an Airbus at the mission centroid on click. 

                IGeoPoint mapCenter = _mission.Map.Centroid;

                IMissionCommands.ModelAndTypeStructure aircraftMACEType;
                aircraftMACEType.Model = IMissionCommands.ModelAndTypeStructure.ModelTypes.Platform;
                aircraftMACEType.Type = "Airbus";

                IPhysicalEntity newPlatform = _mission.MissionCommands.CreateEntity(aircraftMACEType, mapCenter, "");

                // you can control parameters for the new entity by casting it to an IPhysicalEntityController. 
                // It's generally good practice to ensure the previous action was successful.

                IPhysicalEntityController ipc = (IPhysicalEntityController)newPlatform;

                // adjust the altitude to 20,000', using the double extension in BSI.SimulationLibrary.Formulas
                // to convert from feet to meters. 

                ipc.AdjustAltitude_m((20000.0).FtToMeters());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                // or write to mission log: 
                _mission.Logger.ErrorMessage(ex);
            }
        }
        #endregion

        #region "WindowConfigChanged Event Handler"
        #endregion

        private void btnAddMapEntity_Click(object sender, EventArgs e)
        {
            try
            {
                _mission.Map.MouseDown -= HandleMapMouseDown;
                _mission.Map.MouseDown += HandleMapMouseDown;
                _mission.Map.SetPlugInCustomCursor("Alternate Select.cur");
                _mission.Map.SetPlugInMapAction();
            }
            catch (Exception ex)
            {
                _mission.Logger.ErrorMessage(ex);
            }
        }

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

        private void start_button_Click(object sender, EventArgs e)
        {
            foreach (KeyValuePair<ulong, IPhysicalEntity>  kvp in _mission.PhysicalEntities) {
                var newEntity = kvp.Value;

                PlatformObject newObject = new PlatformObject(newEntity.Name, newEntity.Type, (int)newEntity.TeamAffiliation, (int)newEntity.Domain); 

                platformObjects.Add(newObject);
                
            }
        }
    }
}
