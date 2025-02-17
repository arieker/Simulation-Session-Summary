using System.Collections.Generic;
using System.Linq;

namespace SimulationSessionSummary_NS
{
    public class PlatformObject
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int Team { get; set; }
        public string Domain { get; set; }
        public bool Alive { get; set; }
        public int Kills => weaponObjects.Count(w => w.ResultedInKill); // updates automatically
        public int RemainingWeapons => weaponObjects.Count(w => !w.Fired); // updates automatically
        public int FiredWeapons => weaponObjects.Count(w => w.Fired); // updates automatically

        public List<WeaponObject> weaponObjects { get; set; }

        // Parameterless constructor (required for serialization)
        public PlatformObject()
        {
            weaponObjects = new List<WeaponObject>();
        }

        public PlatformObject(string name, string type, int team, string domain, List<WeaponObject> weaponObjects)
        {
            Name = name;
            Type = type;
            Team = team;
            Domain = domain;
            this.weaponObjects = weaponObjects;
            Alive = true;
        }
    }
}
