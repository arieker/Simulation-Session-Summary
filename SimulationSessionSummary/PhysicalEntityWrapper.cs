using BSI.MACE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationSessionSummary_NS
{
    internal class PhysicalEntityWrapper
    {
        private IPhysicalEntity _entity;

        [DisplayNameAttribute("Callsign")]
        public string Name
        {
            get
            {
                return _entity.Name;
            }
        }

        [DisplayNameAttribute("Health (%)")]
        public int Health
        {
            get
            {
                return (int)_entity.Health * 100;
            }
            set
            {
                _entity.Health = ((double)value / 100);
            }
        }

        [Browsable(false)]
        public IPhysicalEntity Entity
        {
            get
            {
                return _entity;
            }
        }

        public PhysicalEntityWrapper(IPhysicalEntity entity)
        {
            _entity = entity;
        }
    }
}
