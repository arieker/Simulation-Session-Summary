using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

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

        public int Misses = 0;

        public Dictionary<PlatformObject, int> PlatformHitCounts { get; set; } = new Dictionary<PlatformObject, int>();

        public List<PlatformObject> KilledPlatforms { get; set; } = new List<PlatformObject>();

        public int Kills => KilledPlatforms.Count;

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

        public GunObject(ulong startingBullets)
        {
            StartingBullets = startingBullets;
            RemainingBullets = startingBullets;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
