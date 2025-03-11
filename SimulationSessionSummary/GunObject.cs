using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;

namespace SimulationSessionSummary_NS
{
    public class GunObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ulong _startingBullets;
        private ulong _remainingBullets;

        public ulong StartingBullets
        {
            get => _startingBullets;
            set
            {
                if (_startingBullets != value)
                {
                    _startingBullets = value;
                    OnPropertyChanged(nameof(StartingBullets));
                }
            }
        }

        public ulong RemainingBullets
        {
            get => _remainingBullets;
            set
            {
                if (_remainingBullets != value)
                {
                    _remainingBullets = value;
                    OnPropertyChanged(nameof(RemainingBullets));
                }
            }
        }

        public int Hits => PlatformHitCounts.Values.Sum();

        public int Misses { get; set; }

        [Browsable(false)]
        [XmlIgnore]
        public Dictionary<PlatformObject, int> PlatformHitCounts { get; set; } = new Dictionary<PlatformObject, int>();

        [Browsable(false)]
        [XmlIgnore]
        public List<PlatformObject> KilledPlatforms { get; set; } = new List<PlatformObject>();

        public int Kills => KilledPlatforms.Count;

        [Browsable(false)]
        public List<ulong> ActiveBulletEntityIDs { get; set; } = new List<ulong>();

        public void RegisterBulletHit(PlatformObject hitPlatform)
        {
            if (PlatformHitCounts.ContainsKey(hitPlatform))
            {
                PlatformHitCounts[hitPlatform]++;
            }
            else
            {
                PlatformHitCounts[hitPlatform] = 1;
            }

            OnPropertyChanged(nameof(Hits));
            OnPropertyChanged(nameof(PlatformHitCounts));
        }

        // Parameterless constructor (required for serialization)
        public GunObject() { }

        public GunObject(ulong startingBullets)
        {
            StartingBullets = startingBullets;
            RemainingBullets = startingBullets;
            Misses = 0;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
