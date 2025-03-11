using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace SimulationSessionSummary_NS
{
    public partial class IndividualPlaneControl : UserControl
    {
        private PlatformObject _platformObject;

        public IndividualPlaneControl(PlatformObject platformObject)
        {
            _platformObject = platformObject;
            InitializeComponent();

            dataGridViewWeapons.DataSource = _platformObject.weaponObjects;

            foreach (var weapon in _platformObject.weaponObjects)
            {
                weapon.PropertyChanged += Weapon_PropertyChanged;
            }

            labelWeaponsRemaining.DataBindings.Add("Text", _platformObject, "RemainingWeaponsCount");
            platformObject.PropertyChanged += Platform_PropertyChanged;
        }

        private void Weapon_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            dataGridViewWeapons.Refresh();
        }

        private void Platform_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            labelWeaponsRemainingText.Refresh();
        }
    }
}
