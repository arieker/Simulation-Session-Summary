using System.ComponentModel;

namespace SimulationSessionSummary_NS
{
    public class WeaponObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _fired;
        private bool _hit;
        private bool _detonated;
        private bool _resultedInKill;

        public string Name { get; set; }
        public string Type { get; set; }
        [Browsable(false)]
        public string OwnshipName { get; set; }
        public string TargetName { get; set; }
        [Browsable(false)]
        public ulong InstanceID { get; set; }
        public double TargetLon { get; set; }
        public double TargetLat { get; set; }

        public bool Fired
        {
            get => _fired;
            set
            {
                if (_fired != value)
                {
                    _fired = value;
                    OnPropertyChanged(nameof(Fired));
                }
            }
        }

        public bool Hit
        {
            get => _hit;
            set
            {
                if (_hit != value)
                {
                    _hit = value;
                    OnPropertyChanged(nameof(Hit));
                }
            }
        }

        public bool Detonated
        {
            get => _detonated;
            set
            {
                if (_detonated != value)
                {
                    _detonated = value;
                    OnPropertyChanged(nameof(Detonated));
                }
            }
        }

        public bool ResultedInKill
        {
            get => _resultedInKill;
            set
            {
                if (_resultedInKill != value)
                {
                    _resultedInKill = value;
                    OnPropertyChanged(nameof(ResultedInKill));
                }
            }
        }

        // Parameterless constructor (required for serialization)
        public WeaponObject() { }

        public WeaponObject(string name, string type, string ownshipName, ulong instanceID)
        {
            Name = name;
            Type = type;
            OwnshipName = ownshipName;
            InstanceID = instanceID;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
