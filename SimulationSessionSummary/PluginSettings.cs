using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationSessionSummary_NS
{
    [Serializable]
    internal class PluginSettings
    {
        public string SomeSetting { get; set; }

        /// <summary>
        /// parameterless constructor for serialization/deserialization
        /// </summary>
        public PluginSettings()
        {
        }
    }

}
