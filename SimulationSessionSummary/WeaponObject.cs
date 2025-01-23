using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationSessionSummary_NS
{
    public class WeaponObject
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string OwnshipName { get; set; }
        public string TargetName { get; set; }
        public string InstanceID { get; set; }
        public string TargetLat { get; set; }
        public string TargetLon { get; set; }
        public bool Detonated { get; set; }
        public bool Hit { get; set; }

        public WeaponObject(string name, string ownshipName, string instanceID)
        {
            Name = name;
            //Type = type;
            OwnshipName = ownshipName;
            //TargetName = targetName;
            InstanceID = instanceID;
            //TargetLat = targetLat;
            //TargetLon = targetLon;
            //Detonated = detonated;
            //Hit = hit;
        }
        /*
        public override string ToString()
        {
            return $"Name: {Name}, OwnshipName: {OwnshipName}, TargetName: {TargetName}, InstanceID: {InstanceID}, TargetLat: {TargetLat}, TargetLon: {TargetLon}, Hit: {Hit}";
        }*/
    }
}
