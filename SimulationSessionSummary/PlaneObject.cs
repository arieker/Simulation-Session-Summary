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
        public string Domain { get; set; }
        public bool Alive { get; set; }
        public List<WeaponObject> weaponObjects { get; set; }

        public PlatformObject(string name, string type, int team, string domain, List<WeaponObject> weaponObjects)
        {
            Name = name;
            Type = type;
            Team = team;
            Domain = domain;
            this.weaponObjects = weaponObjects;
        }
    }
}
