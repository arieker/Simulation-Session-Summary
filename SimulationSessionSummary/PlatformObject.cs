using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;

namespace SimulationSessionSummary_NS
{
    public class PlatformObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _name;
        private string _type;
        private int _team;
        private string _domain;
        private bool _alive;
        private List<WeaponObject> _weaponObjects;
        private GunObject _gun;

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public string Type
        {
            get => _type;
            set
            {
                if (_type != value)
                {
                    _type = value;
                    OnPropertyChanged(nameof(Type));
                }
            }
        }

        public int Team
        {
            get => _team;
            set
            {
                if (_team != value)
                {
                    _team = value;
                    OnPropertyChanged(nameof(Team));
                }
            }
        }

        public string Domain
        {
            get => _domain;
            set
            {
                if (_domain != value)
                {
                    _domain = value;
                    OnPropertyChanged(nameof(Domain));
                }
            }
        }

        public bool Alive
        {
            get => _alive;
            set
            {
                if (_alive != value)
                {
                    _alive = value;
                    OnPropertyChanged(nameof(Alive));
                }
            }
        }

        public int Kills => WeaponObjects.Count(w => w.ResultedInKill) + Gun.KilledPlatforms.Count;

        public int RemainingWeaponsCount => WeaponObjects.Count(w => !w.Fired);

        public int FiredWeaponsCount => WeaponObjects.Count(w => w.Fired);

        [Browsable(false)]
        public GunObject Gun
        {
            get => _gun;
            set
            {
                if (_gun != value)
                {
                    _gun = value;
                    OnPropertyChanged(nameof(Gun));
                    OnPropertyChanged(nameof(Kills));
                }
            }
        }

        public List<WeaponObject> WeaponObjects
        {
            get => _weaponObjects;
            set
            {
                if (_weaponObjects != value)
                {
                    _weaponObjects = value;
                    OnPropertyChanged(nameof(WeaponObjects));
                }
            }
        }

        private void WeaponObject_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(WeaponObject.Fired) ||
                e.PropertyName == nameof(WeaponObject.Hit) ||
                e.PropertyName == nameof(WeaponObject.Detonated) ||
                e.PropertyName == nameof(WeaponObject.ResultedInKill))
            {
                OnPropertyChanged(nameof(RemainingWeaponsCount));
                OnPropertyChanged(nameof(FiredWeaponsCount));
                OnPropertyChanged(nameof(Kills));
            }
        }

        // Parameterless constructor (required for serialization)
        public PlatformObject()
        {
            WeaponObjects = new List<WeaponObject>();
        }

        public PlatformObject(string name, string type, int team, string domain, ulong startingBullets, List<WeaponObject> weaponObjects)
        {
            Name = name;
            Type = type;
            Team = team;
            Domain = domain;
            Gun = new GunObject(startingBullets);
            this.WeaponObjects = weaponObjects;
            foreach (WeaponObject weaponObject in weaponObjects)
            {
                weaponObject.PropertyChanged += WeaponObject_PropertyChanged;
            }
            Alive = true;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
