using BSI.MACE;
using BSI.MACE.PlugInNS;
using BSI.SimulationLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SimulationSessionSummary_NS
{
    /// <summary>
    /// MACE Plug-in class
    /// </summary>
    /// <remarks></remarks>
    public class SimulationSessionSummary : IMACEPlugIn
    {
        #region "Private Member Variables"

        /// <summary>
        /// Reference to the IMACEPlugInHost interface received during plug-in intialiaziation.
        /// </summary>
        private IMACEPlugInHost _host;

        /// <summary>
        /// Reference to the currently loaded MACE mission and its associated run-time API.
        /// </summary>
        private IMission _mission;

        private SimulationSessionSummary _plugIn;


        #endregion

        #region "Properties"

        public System.IO.FileInfo SettingsFileInfo
        {
            get
            {
                String configFilePath = $"{MissionInstance.Host.SystemSettings.UserXMLDirectory}{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}-{_mission.Host.UniqueConfigName}.xml";
                return new System.IO.FileInfo(configFilePath);
            }
        }

        #endregion

        internal WindowConfigController WindowConfigController;
        private PluginSettings _settings;

        #region "IMACEPlugIn Interface Implementation"

        /// <summary>
        /// Text name for this plug-in.
        /// </summary>
        public string Name
        {
            get { return "NewPluginTemplate1"; }
        }

        /// <summary>
        /// Method invoked by MACE to initialize this plug-in and provide an interface to the MACE API.
        /// </summary>
        /// <param name="host">The initial MACE host interface.</param>
        /// <returns>Return true if plug-in initializes successfully, false if initialization has failed.</returns>
        /// <remarks></remarks>
        public bool Initialize(IMACEPlugInHost host)
        {
            bool result = false;

            try
            {
                _host = host;
                _mission = _host.Mission;

                // Add a button to the MACE toolbar, selecting this button from the toolbar will invoke the Show method.
                result = _host.AddButton(this, "Info/Status Windows", "", "NewPluginTemplate1 Tooltip", SimulationSessionSummary_NS.Properties.Resources.icon123);

                if (result)
                {
                    // Create a WindowConfigController to manage this plugin's forms
                    // and associate them w/ MACE window configurations
                    WindowConfigController = new WindowConfigController(this);

                    // Add the WindowConfigController to the PluginInManager
                    host.PlugInManager.WindowConfigControllers.Add(WindowConfigController);
                }

                // Legacy/alternate means of registering plugin w/ the MACE
                // window config controller.
                // Register local form classes w/ the window config contoller. This 
                // allows the window config contoller to create form instances when a 
                // config is restored, if required. 
                // WindowConfigController.RegisterFormClass(typeof(NewPluginTemplate1Form));

                // note that in its current form, your plugin form will only be created 
                // when you click the button. Alternatively, you 
                // can spawn your plugin form when MACE launches, but hide its visibility
                // this will subscribe to any events, etc. 

                // myform = new NewPluginTemplate1Form(_mission);
                // myform.Show();
                // myform.Visible = false;

                // if your requirements are such that you don't need a form at all, you can leverage all of the 
                // MACE event handlers detailed in this project's form code, subscribing here.
            }
            catch (Exception ex)
            {
                _mission.Logger.ErrorMessage(ex);
            }

            return result;

        }

        /// <summary>
        /// Load plugin specific settings
        /// </summary>
        public void LoadSettings()
        {
            try
            {
                System.IO.FileInfo settingsFileInfo = this.SettingsFileInfo;
                if (settingsFileInfo.Exists)
                {
                    XElement root = XElement.Load(settingsFileInfo.FullName);
                    _settings = root.Deserialize(typeof(PluginSettings)) as PluginSettings;
                }
            }
            catch (Exception ex)
            {
                _mission.Logger.ErrorMessage(ex);
            }
        }

        /// <summary>
        /// Save plugin specific settings
        /// </summary>
        public void SaveSettings()
        {
            try
            {
                XElement root = _settings.Serialize();
                root.Save(SettingsFileInfo.FullName);
            }
            catch (Exception ex)
            {
                _mission.Logger.ErrorMessage(ex);
            }
        }

        /// <summary>
        /// Show the form, if any, associated with this plug-in.
        /// </summary>
        /// <remarks></remarks>
        public void Show()
        {
            try
            {
                SimulationSessionSummaryForm myform = new SimulationSessionSummaryForm(this, _mission);
                myform.Show();

                // if you're spawning your plugin form immediately in Initialize(), 
                // you can forego the previous two lines and simply change the form 
                // visibility here: 

                // myform.Visible = true;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        /// <summary>
        /// Shutdown the plug-in.
        /// </summary>
        public void Close()
        {
            // if subscribing to any events locally, clean up here!!
        }

        #endregion
    }
}

