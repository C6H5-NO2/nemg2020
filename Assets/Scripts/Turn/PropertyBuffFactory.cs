using System;
using Property;
using Turn.Buff;
using UnityEngine;

namespace Turn {
    [Serializable]
    public struct PropertyBuffFactory {
        public PropertyBuffFactory(PropertyReprGroup prop, int activeTurns) {
            this.propertyGroup = prop;
            this.activeTurns = activeTurns;
        }

        [SerializeField] private PropertyReprGroup propertyGroup;
        [SerializeField] private int activeTurns;

        // DO NOT convert to auto property!
        public PropertyReprGroup PropertyGroup => propertyGroup;
        public int ActiveTurns => activeTurns;

        public PropertyBuff CreateBuff(float factor = 1) {
            return new PropertyBuff(propertyGroup * factor, activeTurns);
        }
    }


    public class PropertyBuff : BuffBase {
        public PropertyBuff(PropertyReprGroup prop, int activeTurns) : base(activeTurns) {
            propertyGroup = prop;
        }

        private readonly PropertyReprGroup propertyGroup;

        public override void OnApplied() {
            PropertyManager.Instance.AddProperty(propertyGroup);
        }

        public override void OnRemoved() {
            PropertyManager.Instance.SubtractProperty(propertyGroup);
        }
    }
}
