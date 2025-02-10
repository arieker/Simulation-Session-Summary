namespace SimulationSessionSummary_NS
{
    public class WeaponObject
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string OwnshipName { get; set; }
        public string TargetName { get; set; }
        public ulong InstanceID { get; set; }
        public double TargetLat { get; set; }
        public double TargetLon { get; set; }
        public bool Fired { get; set; }
        public bool Hit { get; set; }
        public bool Detonated { get; set; }
        public bool ResultedInKill { get; set; }

        // Parameterless constructor (required for serialization)
        public WeaponObject() { }

        public WeaponObject(string name, string type, string ownshipName, ulong instanceID)
        {
            Name = name;
            Type = type;
            OwnshipName = ownshipName;
            InstanceID = instanceID;
        }
    }
}
