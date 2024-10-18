using BSI.MACE.PlugInNS;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.

// BSI offers some extended assembly attributes that are read by the 
// plugin manager in MACE and provide useful information to the user. 

[assembly: AssemblyTitle("SimulationSessionSummary")]

// The plugin description shows up in the plugin manager
[assembly: AssemblyDescription("YOUR DESCRIPTION HERE")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]

// Don't change the assembly product
[assembly: AssemblyProduct("MACEPlugIn")]
[assembly: AssemblyCopyright("")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// path to the plugin icon w/in the namespace
[assembly: AssemblyIconPath("SimulationSessionSummary_NS.Resources.PluginIcon.ico")]

// describe where the user can find the plugin icon, if it exists
[assembly: AssemblyUILocation("Analyis -> Info/Status Windows")]

// The following assembly directive can be used to auto enable your plugin on first load.
// Not recommended for most installation.
//[assembly: AssemblyAutoEnabled("true")]

// list any library dependencies here
[assembly: AssemblyDependencies("")]

// The binding path is used to resolve any library dependencies. Define it relative to the 
// plugin .dll location (usually "C:\Users\Public\Documents\MACE\PlugIns")

[assembly: AssemblyDependencyBindingPath("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("47be6f5a-6f12-483d-b005-cb5d0f2b19e8")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// The convention BSI uses for extensions and plugins is for the build number to
// match the release of MACE for which the supporting libraries in the plugin can 
// be reasonably expected to work. For instance the following
// version number is matched to work w/ MACE 2020R1

[assembly: AssemblyVersion("1.0.20.*")]
