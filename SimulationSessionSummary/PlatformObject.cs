using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationSessionSummary_NS
{
    public class PlatformObject
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int Team { get; set; }
        public int Domain { get; set; }
        public List<int> weaponObjects { get; set; }

        public PlatformObject(string name, string type, int team, int domain, List<int> weaponObjects)
        {
            Name = name;
            Type = type;
            Team = team;
            Domain = domain;
            this.weaponObjects = weaponObjects;
        }
    }
}
